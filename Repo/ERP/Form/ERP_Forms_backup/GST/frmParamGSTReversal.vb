Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGSTReversal
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mSearchStartRow As Integer

    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColVNo As Short = 4
    Private Const ColVDate As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColCustCode As Short = 8
    Private Const ColCustGSTNo As Short = 9
    Private Const ColCustName As Short = 10
    Private Const ColOClaimNo As Short = 11
    Private Const ColOClaimDate As Short = 12
    Private Const ColInvAmount As Short = 13
    Private Const ColTaxableAmount As Short = 14
    Private Const ColOCGST As Short = 15
    Private Const ColOSGST As Short = 16
    Private Const ColOIGST As Short = 17
    Private Const ColTotalOGSTAmount As Short = 18
    Private Const ColReversalAmount As Short = 19
    Private Const ColReversalCGST As Short = 20
    Private Const ColReversalSGST As Short = 21
    Private Const ColReversalIGST As Short = 22
    Private Const ColInterestAmount As Short = 23
    Private Const ColReversalRule As Short = 24
    Private Const colRemarks As Short = 25
    Private Const ColDebitAccount As Short = 26
    Private Const ColJVNO As Short = 27

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColJVNO

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY, 12)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefNo, 6)
            .ColHidden = False

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefDate, 9)
            .ColHidden = False

            .Col = ColOClaimNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColOClaimNo, 9)
            .ColHidden = False

            .Col = ColOClaimDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColOClaimDate, 9)
            .ColHidden = False

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

            .Col = ColCustGSTNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustGSTNo, 10)

            .Col = ColCustName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustName, 20)
            .ColHidden = False


            For cntCol = ColInvAmount To ColInterestAmount
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

            .Col = ColReversalRule
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColReversalRule, 12)
            .ColHidden = False

            .Col = colRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(colRemarks, 15)
            .ColHidden = False

            .Col = ColDebitAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColDebitAccount, 20)
            .ColHidden = False

            .Col = ColJVNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColJVNO, 6)
            .ColHidden = False

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColJVNO)
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

            .Col = ColRefNo
            .Text = "Claim No"

            .Col = ColRefDate
            .Text = "Claim Date"

            .Col = ColVNo
            .Text = "Original VNo"

            .Col = ColVDate
            .Text = "Original VDate"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColCustCode
            .Text = "Supplier Code"

            .Col = ColCustGSTNo
            .Text = "Supplier GST NO"

            .Col = ColCustName
            .Text = "Supplier Name"

            .Col = ColOClaimNo
            .Text = "Original Claim No"

            .Col = ColOClaimDate
            .Text = "Original Claim Date"

            .Col = ColInvAmount
            .Text = "Original Bill Amount"

            .Col = ColTaxableAmount
            .Text = "Original Taxable Amount"

            .Col = ColOCGST
            .Text = "Original CGST Amount"

            .Col = ColOSGST
            .Text = "Original SGST Amount"

            .Col = ColOIGST
            .Text = "Original IGST Amount"

            .Col = ColTotalOGSTAmount
            .Text = "Total Original Claim Amount"


            .Col = ColReversalAmount
            .Text = "Reversal Taxable Amount"

            .Col = ColReversalCGST
            .Text = "Reversal CGST Claim Amount"

            .Col = ColReversalSGST
            .Text = "Reversal SGST Claim Amount"

            .Col = ColReversalIGST
            .Text = "Reversal IGST Claim Amount"

            .Col = ColInterestAmount
            .Text = "Interest Amount"

            .Col = ColReversalRule
            .Text = "Reversal Under Rule"

            .Col = colRemarks
            .Text = "Remarks"

            .Col = ColDebitAccount
            .Text = "Debit Account Name"

            .Col = ColJVNO
            .Text = "JV No"


            .set_RowHeight(0, 20)
        End With
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo LedgError

        Clear1()
        If FieldsVerification = False Then GoTo LedgError

        Show1()

        Call FormatSprdMain(-1)

        Exit Sub
LedgError:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdFind_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFind.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim i As Integer
        Dim mCol As Integer


        '    mSearchStartRow = 0
        If mSearchStartRow = SprdMain.MaxRows Then mSearchStartRow = 0

        If OptSearch(0).Checked = True Then
            mCol = ColOClaimNo
        ElseIf OptSearch(1).Checked = True Then
            mCol = ColBillNo
        ElseIf OptSearch(2).Checked = True Then
            mCol = ColVNo
        End If

        mSearchItem = Trim(txtSearch.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For i = counter To .MaxRows
                .Row = i

                .Col = mCol
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, i, mCol)
                    mSearchStartRow = i + 1
                    GoTo NextRec
                End If
                '
                '            .Col = ColItemName
                '            mFindItemName = Trim(.Text)
                '
                ''            If mSearchItem = mFindItemName Then
                '            If InStr(1, mFindItemName, mSearchItem, vbTextCompare) > 0 Then
                '                MainClass.SetFocusToCell SprdMain, I, ColItemCode
                '                mSearchStartRow = I + 1
                '                GoTo NextRec:
                '            End If
            Next
            mSearchStartRow = 1
NextRec:
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamGSTReversal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Me.Text = "GST Claim Reversal Register"

        '    cboShow.Enabled = True ' IIf(lblView.Caption = "V", True, False)

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamGSTReversal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    If KeyAscii = vbKeyReturn Then SendKeys "{Tab}"
        '    If KeyAscii = vbKeyF1 Then txtSearch.SetFocus

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamGSTReversal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        If RsCompany.Fields("FYEAR").Value <= 2017 Then
            txtDateFrom.Text = VB6.Format(PubGSTApplicableDate, "DD/MM/YYYY")
        Else
            txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        End If
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        '    cboShow.AddItem "ALL"
        '    cboShow.AddItem "Compelete"
        '    cboShow.AddItem "Claim Only"
        '    cboShow.AddItem "Approval (Other Than Claim)"
        '    cboShow.AddItem "Pending"
        '    cboShow.ListIndex = 4

        ADDMode = False
        MODIFYMode = False


        optOrderBy(1).Checked = True
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdSearch.Enabled = False


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamGSTReversal_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
        Dim SqlStr As String = ""
        Dim mAccountCode As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT IH.MKEY, IH.REFNO, TO_CHAR(IH.REFDATE,'DD/MM/YYYY') AS REFDATE," & vbCrLf & " IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
            & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.GST_RGN_NO, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " O_GST_CLAIM_NO, TO_CHAR(IH.O_GST_CLAIM_DATE,'DD/MM/YYYY') AS O_GST_CLAIM_DATE, " & vbCrLf _
            & " TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.BILLAMOUNT)) AS BILLAMOUNT, TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.TAXABLEAMOUNT)) AS TAXABLEAMOUNT," & vbCrLf & " TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.CGST_AMOUNT)) AS CGST_AMOUNT, TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.SGST_AMOUNT)) AS SGST_AMOUNT," & vbCrLf & " TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.IGST_AMOUNT)) AS IGST_AMOUNT, TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.TOTALGST_AMOUNT)) AS TOTALGST_AMOUNT," & vbCrLf & " TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.TOTAL_REVERSAL_AMOUNT)) AS TOTAL_REVERSAL_AMOUNT, TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.REVERSAL_CGST_AMOUNT)) AS REVERSAL_CGST_AMOUNT," & vbCrLf & " TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.REVERSAL_SGST_AMOUNT)) AS REVERSAL_SGST_AMOUNT, TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.REVERSAL_IGST_AMOUNT)) AS REVERSAL_IGST_AMOUNT," & vbCrLf & " TO_CHAR(DECODE(IH.CANCELLED,'Y',0,IH.INTEREST_AMOUNT)) AS INTEREST_AMOUNT," & vbCrLf & " CASE WHEN REVERSAL_RULE='a' THEN 'Rule 37(2)'" & vbCrLf & " WHEN REVERSAL_RULE='b' THEN 'Rule 42(1)m'" & vbCrLf & " WHEN REVERSAL_RULE='c' THEN 'Rule 43(1)h'" & vbCrLf & " WHEN REVERSAL_RULE='d' THEN 'Rule 42(2)a'" & vbCrLf & " WHEN REVERSAL_RULE='e' THEN 'Rule 42(2)b'" & vbCrLf & " WHEN REVERSAL_RULE='f' THEN 'Rule 39(1)(j)(ii)'" & vbCrLf & " END AS REVERSAL_RULE, " & vbCrLf & " IH.REMARKS," & vbCrLf & " DMST.SUPP_CUST_NAME," & vbCrLf & " TRN.VNO"


        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_GSTREVERSAL_TRN IH,  FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST DMST, FIN_VOUCHER_HDR TRN"
        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=DMST.COMPANY_CODE" & vbCrLf & " AND IH.ACCOUNT_CODE=DMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND IH.JVMKEY=TRN.MKEY" & vbCrLf & " AND IH.CANCELLED='N' AND IH.ISFINALPOST='Y' AND TRN.BOOKTYPE='J'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.REFDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.REFDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        ''ORDER CLAUSE...

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.REFDATE, IH.REFNO"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
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
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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


    Private Sub frmParamGSTReversal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub


    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""
        Dim xBookSubType As String

        '
        '    SprdMain.Row = SprdMain.ActiveRow
        '
        '    SprdMain.Col = ColBillDate
        '    xVDate = Me.SprdMain.Text
        '
        '
        '
        '    SprdMain.Col = ColMKEY
        '    xMKey = Me.SprdMain.Text
        '
        '    SprdMain.Col = ColVNo
        '    xVNo = Me.SprdMain.Text
        '
        '    If lblBookType.Caption = "M" Then
        '        If Not IsDate(xVDate) Then Exit Sub
        '        If CDate(xVDate) >= CDate(RsCompany!Start_Date) And CDate(xVDate) <= CDate(RsCompany!END_DATE) Then
        '            Call ShowTrn(xMKey, xVDate, "", xVNo, "P", "")
        '        End If
        ''    Else
        ''        Call ShowTrn(xMkey, xVDate, "", xVNo, "J", "")
        '    End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F2 Then txtSearch.Focus()
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Or eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then cmdFind_Click(cmdFind, New System.EventArgs())
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S','2')"
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

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S','2')"

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

        MainClass.SaveStatus(Me.cmdShow, ADDMode, MODIFYMode)
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

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged

        MainClass.SaveStatus(Me.cmdShow, ADDMode, MODIFYMode)
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

    Private Sub txtSearch_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSearch.TextChanged
        mSearchStartRow = 0
    End Sub

    Private Sub txtSearch_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Or KeyCode = System.Windows.Forms.Keys.Return Then cmdFind_Click(cmdFind, New System.EventArgs())
    End Sub

    Private Sub txtSearch_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSearch.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSearch.Text) = "" Then GoTo EventExitSub

        '    If OptSearch(0).Value = True Then
        '        If Len(txtSearch.Text) < 6 Then
        '            txtSearch.Text = Val(txtSearch.Text) & vb6.Format(RsCompany.Fields("FYEAR").Value, "0000") & vb6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        '        End If
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
