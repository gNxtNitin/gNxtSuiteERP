Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPOWiseBillWise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColMRRNo As Short = 3
    Private Const ColMRRDate As Short = 4
    Private Const ColBillDate As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemName As Short = 8
    Private Const ColQty As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColBillAmount As Short = 11
    Private Const ColMKEY As Short = 12

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdsearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchPO.Click
        SearchPO()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        FormatSprdMain(-1)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPOWiseBillWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "PO Wise - Bill Wise Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPOWiseBillWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        lblTrnType.Text = CStr(-1)
        TxtAccount.Enabled = True
        cmdsearch.Enabled = True
        Call PrintStatus(True)
        '    txtDateFrom = Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        '    txtDateTo = Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamPOWiseBillWise_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamPOWiseBillWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchPO()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mPartyCode As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(TxtAccount.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = Trim(MasterNo)
                SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        ''MainClass.SearchMaster txtPONO, "PUR_PURCHASE_HDR", "AUTO_KEY_PO", sqlstr
        MainClass.SearchGridMaster(txtPONO.Text, "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "SUPP_CUST_CODE", "PUR_ORD_DATE", , SqlStr)
        If AcName <> "" Then
            txtPONo.Text = AcName

            If MainClass.ValidateWithMasterTable(AcName1, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                TxtAccount.Text = MasterNo
            End If

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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

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

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 8)
            .ColHidden = False

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRNo, 8)

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRDate, 8)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 15)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatSepChar = Asc(",")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatSepChar = Asc(",")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 8)

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatSepChar = Asc(",")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBillAmount, 8)

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

        ''''SELECT CLAUSE...

        MakeSQL = " SELECT '',IH.VNO, TO_CHAR(IH.AUTO_KEY_MRR), TO_CHAR(IH.MRRDATE), " & vbCrLf & " IH.INVOICE_DATE, IH.BILLNO, ID.ITEM_CODE, ID.ITEM_DESC, TO_CHAR(ID.ITEM_QTY),TO_CHAR(ID.ITEM_RATE), " & vbCrLf & " ID.ITEM_AMT, IH.MKEY "

        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID"

        ''''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        If Trim(TxtAccount.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = Trim(MasterNo)
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND ID.CUST_REF_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'"

        MakeSQL = MakeSQL & vbCrLf & " UNION "

        MakeSQL = MakeSQL & vbCrLf & " SELECT '',IH.VNO, '', '', " & vbCrLf & " TRN.BILLDATE, TRN.BILLNO, '','', '','', " & vbCrLf & " TRN.BILLAMOUNT, IH.MKEY " & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_BILLDETAILS_TRN TRN" & vbCrLf & " WHERE IH.MKEY=TRN.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(TxtAccount.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = Trim(MasterNo)
                MakeSQL = MakeSQL & vbCrLf & "AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND TRN.PONO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'"


        ''''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY 5, 6"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        '    If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus

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

        If Trim(txtPONo.Text) = "" Then
            MsgInformation("Invaild PO No.")
            txtPONo.Focus()
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

        Dim mBillAmount As Double
        Dim RsTemp As ADODB.Recordset
        Dim pMKey As Double
        Dim SqlStr As String

        If txtPONo.Text <> "" Then

            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If MainClass.ValidateWithMasterTable(txtPONo.Text, "AUTO_KEY_PO", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pMKey = MasterNo

                SqlStr = "SELECT SUM(GROSS_AMT) AS GROSS_AMT From PUR_PURCHASE_DET WHERE MKEY = " & pMKey & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtPOAmount.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GROSS_AMT").Value), 0, RsTemp.Fields("GROSS_AMT").Value), "0.00")
                End If
            End If
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColBillAmount
                mBillAmount = mBillAmount + Val(.Text)

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColBillNo)
            .Col = ColBillDate
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

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMkey = Me.SprdMain.Text

        SprdMain.Col = ColVNo
        xVNo = Me.SprdMain.Text

        Call ShowTrn(xMkey, xVDate, "", xVNo, "P", "", Me)

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONO.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPONO_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONO.DoubleClick
        SearchPO()
    End Sub


    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONO.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPONO_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONO.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPO()
    End Sub


    Private Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONO.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mPartyCode As String
        Dim RsTemp As ADODB.Recordset
        Dim pMKey As String

        If txtPONo.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(TxtAccount.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mPartyCode = Trim(MasterNo)
                SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            Else
                MsgInformation("No Such Account in Account Master")
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtPONo.Text, "AUTO_KEY_PO", "SUPP_CUST_CODE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
            mPartyCode = Trim(MasterNo)
            If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                TxtAccount.Text = Trim(MasterNo)
            End If

            If MainClass.ValidateWithMasterTable(txtPONo.Text, "AUTO_KEY_PO", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
                pMKey = MasterNo
            End If

            SqlStr = "SELECT SUM(GROSS_AMT) AS GROSS_AMT From PUR_PURCHASE_DET WHERE MKEY = " & pMKey & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                txtPOAmount.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GROSS_AMT").Value), 0, RsTemp.Fields("GROSS_AMT").Value), "0.00")
            End If
        Else
            MsgInformation("Invail PO.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True


        '''''Select Record for print...

        SqlStr = ""

        SqlStr = MakeSQL

        mTitle = "POWISE-BILLWISE DETAIL"
        '    mSubTitle = UCase(TxtAccount.Text)
        mReportFileName = "PO_BILLWISE.Rpt"

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
        MainClass.AssignCRptFormulas(Report1, "AccountName=""" & TxtAccount.Text & """")
        MainClass.AssignCRptFormulas(Report1, "PONO=""" & txtPONo.Text & """")
        MainClass.AssignCRptFormulas(Report1, "POAmount=""" & txtPOAmount.Text & """")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
