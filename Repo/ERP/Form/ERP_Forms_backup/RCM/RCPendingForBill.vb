Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRCPendingForBill
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5
    Private Const ColCustCode As Short = 6
    Private Const ColCustName As Short = 7
    Private Const ColCustBillNo As Short = 8
    Private Const ColCustBillDate As Short = 9
    Private Const ColHSNCode As Short = 10
    Private Const ColInvAmount As Short = 11
    Private Const ColTotalGSTAmount As Short = 12
    Private Const ColCGST As Short = 13
    Private Const ColSGST As Short = 14
    Private Const ColIGST As Short = 15
    Private Const ColBookCode As Short = 15
    Private Const ColBookType As Short = 16
    Private Const ColBookSubType As Short = 17



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColBookSubType

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY, 12)
            .ColHidden = True

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColVNo, 9)
            .ColHidden = False

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColVDate, 9)
            .ColHidden = False

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColBillNo, 9)
            .ColHidden = False

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColBillDate, 9)
            .ColHidden = False

            .ColsFrozen = ColBillNo

            .Col = ColCustCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustCode, 10)
            .ColHidden = True

            .Col = ColCustName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustName, 20)
            .ColHidden = False

            .Col = ColCustBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustBillNo, 9)
            .ColHidden = False

            .Col = ColCustBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustBillDate, 9)
            .ColHidden = False

            For cntCol = ColInvAmount To ColIGST
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
                .ColHidden = False
            Next

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColHSNCode, 6)

            For cntCol = ColBookCode To ColBookSubType
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = False
                .set_ColWidth(ColCustName, 20)
                .ColHidden = True
            Next

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColBookSubType)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        '    SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack			
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume			
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKEY"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColVDate
            .Text = "VDate"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColCustCode
            .Text = "Customer Code"

            .Col = ColCustName
            .Text = "Customer Name"

            .Col = ColCustBillNo
            .Text = "Supplier Bill No"

            .Col = ColCustBillDate
            .Text = "Supplier Bill Date"

            .Col = ColHSNCode
            .Text = "HSN Code"

            .Col = ColTotalGSTAmount
            .Text = "Taxable Amount"

            .Col = ColInvAmount
            .Text = "Inv Amount"

            .Col = ColCGST
            .Text = "CGST Amount"

            .Col = ColSGST
            .Text = "SGST Amount"

            .Col = ColIGST
            .Text = "IGST Amount"

            .Col = ColBookCode
            .Text = "Book Code"

            .Col = ColBookType
            .Text = "Book Type"

            .Col = ColBookSubType
            .Text = "Book SubType"


            .set_RowHeight(0, 20)
        End With
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr

        Clear1()

        Show1()
        Call FormatSprdMain(-1)

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub frmRCPendingForBill_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        cboShow.Enabled = True ' IIf(lblView.Caption = "V", True, False)			
        '    If PubSuperUser = "S" Or PubSuperUser = "A" Then			
        '        cboShow.Enabled = True			
        '    End If			

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmRCPendingForBill_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmRCPendingForBill_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection			
        'PvtDBCn.Open StrConn			

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Compelete")
        cboShow.Items.Add("Pending")
        cboShow.SelectedIndex = 2

        cboType.Items.Add("ALL")
        cboType.Items.Add("Reverse Charge Service")
        cboType.Items.Add("Un-Register Dealer")
        cboType.SelectedIndex = 1


        ADDMode = False
        MODIFYMode = False

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdSearch.Enabled = False


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmRCPendingForBill_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        FraFront.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr


        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVerification() = False Then GoTo LedgError

        '    If lblBookType.Caption = "G" Then			
        SqlStr = MakeSQLMRR()
        '    Else			
        '        SqlStr = MakeSQLJV			
        '    End If			
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************			
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus			
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus			
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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

    Private Function MakeSQLMRR() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean
        Dim mAccountCode As String
        Dim mTRNType As Double
        Dim mFromDate As String

        MakeSQLMRR = " SELECT IH.MKEY, IH.VNO, IH.VDATE, ID.SALEBILL_NO,  ID.SALEBILLDATE," & vbCrLf _
            & " CMST.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY'), " & vbCrLf _
            & " ID.HSNCODE, ID.GSTABLE_AMT, ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT AS GST_AMOUNT, " & vbCrLf _
            & " ID.CGST_AMOUNT , ID.SGST_AMOUNT , ID.IGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.BOOKCODE) AS BOOKCODE, IH.BOOKTYPE, IH.BOOKSUBTYPE "

        ''''FROM CLAUSE...			
        MakeSQLMRR = MakeSQLMRR & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST"

        ''''WHERE CLAUSE...			
        MakeSQLMRR = MakeSQLMRR & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND (IH.ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y')"

        MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND IH.ISFINALPOST='Y'  AND IH.CANCELLED='N'"

        If cboShow.SelectedIndex = 1 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND (ID.SALEBILL_NO<>'' OR ID.SALEBILL_NO IS NOT NULL)"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND (ID.SALEBILL_NO='' OR ID.SALEBILL_NO IS NULL)"
        End If

        If cboType.SelectedIndex = 1 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND ID.HSNCODE IN (SELECT HSN_CODE FROM GEN_HSN_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REVERSE_CHARGE_APP='Y')"
        ElseIf cboType.SelectedIndex = 2 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND CMST.GST_REGD='N' AND ID.HSNCODE IN (SELECT HSN_CODE FROM GEN_HSN_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REVERSE_CHARGE_APP='N')"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If


        MakeSQLMRR = MakeSQLMRR & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        MakeSQLMRR = MakeSQLMRR & vbCrLf & " UNION ALL"

        MakeSQLMRR = MakeSQLMRR & vbCrLf & " SELECT IH.MKEY, IH.VNO, IH.VDATE, ID.SALEBILL_NO,  ID.SALEBILLDATE," & vbCrLf _
            & " CMST.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, '', '', " & vbCrLf _
            & " ID.SAC AS HSNCODE, ID.AMOUNT AS GSTABLE_AMT, ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT AS GST_AMOUNT, " & vbCrLf _
            & " ID.CGST_AMOUNT , ID.SGST_AMOUNT , ID.IGST_AMOUNT, " & vbCrLf & " IH.BOOKCODE, IH.BOOKTYPE, IH.BOOKSUBTYPE" & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.COMPANYCODE=CMST.COMPANY_CODE AND ID.ACCOUNTCODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.REVERSE_CHARGE_APP='Y'  AND IH.CANCELLED='N'"


        If cboShow.SelectedIndex = 1 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND (ID.SALEBILL_NO<>'' OR ID.SALEBILL_NO IS NOT NULL)"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND (ID.SALEBILL_NO='' OR ID.SALEBILL_NO IS NULL)"
        End If

        If cboType.SelectedIndex = 1 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND ID.SAC IN (SELECT HSN_CODE FROM GEN_HSN_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REVERSE_CHARGE_APP='Y')"
        ElseIf cboType.SelectedIndex = 2 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND CMST.GST_REGD='N' AND ID.SAC IN (SELECT HSN_CODE FROM GEN_HSN_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REVERSE_CHARGE_APP='N')"
        End If

        MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND IH.CANCELLED='N'"

        MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND (ID.SAC<>'' OR ID.SAC IS NOT NULL)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If


        MakeSQLMRR = MakeSQLMRR & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '''''ORDER CLAUSE...			
        MakeSQLMRR = MakeSQLMRR & vbCrLf & "ORDER BY 2,1"

        Exit Function
ERR1:
        '    Resume			
        MsgInformation(Err.Description)
    End Function

    Private Sub frmRCPendingForBill_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close			
        'Set PvtDBCn = Nothing			
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        'MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)			
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S','2')"
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

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S','2')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
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
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdSearch.Enabled = True
        End If

    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged

        'MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)			
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateFrom.Text) = "" Then
            txtDateFrom.Focus()
            MsgBox("As on Date cann't be Blank.", MsgBoxStyle.Critical)
            Cancel = True
        End If

        If Not IsDate(txtDateFrom.Text) Then
            MsgBox("Not a Valid Date.", MsgBoxStyle.Critical)
            txtDateFrom.Focus()
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged

        'MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)			
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateTo.Text) = "" Then
            txtDateTo.Focus()
            MsgBox("As on Date cann't be Blank.", MsgBoxStyle.Critical)
            Cancel = True
        End If

        If Not IsDate(txtDateTo.Text) Then
            MsgBox("Not a Valid Date.", MsgBoxStyle.Critical)
            txtDateTo.Focus()
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub
End Class
