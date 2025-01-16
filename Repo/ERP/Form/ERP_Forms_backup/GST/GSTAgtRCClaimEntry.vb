Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTAgtRCClaimEntry
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim mAuthorised As Boolean
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5
    Private Const ColCustCode As Short = 6
    Private Const ColCustName As Short = 7
    Private Const ColSACCode As Short = 8
    Private Const ColCreditApplicable As Short = 9
    Private Const ColInvAmount As Short = 10
    Private Const ColTotalGSTAmount As Short = 11
    Private Const ColCGST As Short = 12
    Private Const ColSGST As Short = 13
    Private Const ColIGST As Short = 14
    Private Const ColBookCode As Short = 15
    Private Const ColBookType As Short = 16
    Private Const ColBookSubType As Short = 17
    Private Const ColGoodServ As Short = 18
    Private Const ColAmount As Short = 19
    Private Const ColDiv As Short = 20
    Private Const ColSACDesc As Short = 21
    Private Const ColStatus As Short = 22
    Private Const ColApproved As Short = 23


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColApproved

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
            .set_ColWidth(ColRefNo, 9)
            .ColHidden = False

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefDate, 9)
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

            .Col = ColSACCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColSACCode, 8)
            .ColHidden = False

            .Col = ColCreditApplicable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCreditApplicable, 6)
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

            For cntCol = ColBookCode To ColGoodServ
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 20)
                .ColHidden = True
            Next

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 8)
            .ColHidden = False

            For cntCol = ColDiv To ColSACDesc
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 20)
                .ColHidden = True
            Next

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbChecked

            .ColHidden = IIf(lblView.Text = "V", True, False)

            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditMultiLine = False
            .set_ColWidth(ColStatus, 6)
            ''        .ColHidden = True

            .Col = ColApproved
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbChecked

            .ColHidden = IIf(lblView.Text = "V", True, False)

            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditMultiLine = False
            .set_ColWidth(ColApproved, 6)
            ''        .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColSACDesc)
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

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColCustCode
            .Text = "Customer Code"

            .Col = ColCustName
            .Text = "Customer Name"

            .Col = ColSACCode
            .Text = "SAC Code"

            .Col = ColCreditApplicable
            .Text = "Credit Applicable"

            .Col = ColInvAmount
            .Text = "Inv Amount"

            .Col = ColTotalGSTAmount
            .Text = "Tax Amount"

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

            .Col = ColGoodServ
            .Text = "Goods / Services"

            .Col = ColAmount
            .Text = "Taxable Amount"

            .Col = ColDiv
            .Text = "Division"

            .Col = ColSACDesc
            .Text = "SAC Description"

            .Col = ColStatus
            .Text = "Claim Status"

            .Col = ColApproved
            .Text = "Approved"

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
        Me.hide()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrPart
        Dim mFromDate As String
        Dim i As Integer
        Dim mStatus As String
        Dim mBillDate As String
        Dim mPartyName As String
        Dim mBillNo As String
        Dim mCreditApp As String

        Dim mApproved As String
        Dim mClaimValue As String
        Dim mLockDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mFromDate = txtClaimDate.Text '' DateAdd("m", -1, txtClaimDate.Text)

        If Not IsDate(txtClaimDate.Text) Then
            MsgInformation("Please Enter Claim Date.")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If CDate(txtClaimDate.Text) > CDate(PubCurrDate) Then
            MsgInformation("Claim Date Cann't be greater than Current Date.")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If CDate(txtClaimDate.Text) > CDate(RsCompany.Fields("END_DATE").Value) Then
            If MsgQuestion("Claim Date is greater than FY ending Date. Are you want to continue.. ? ") = CStr(MsgBoxResult.No) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If

        mLockDate = ""
        If mAuthorised = False Then
            If CheckDateValidation(mLockDate) = False Then
                MsgInformation("Book is Lock before " & mLockDate & " Date. Please change the Claim date")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If


        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                mClaimValue = mStatus

                .Col = ColApproved
                mApproved = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "A", "N")

                mClaimValue = IIf(mClaimValue = "N", mApproved, mClaimValue)

                .Col = ColCustName
                mPartyName = Trim(.Text)

                .Col = ColBillNo
                mBillNo = Trim(.Text)

                .Col = ColBillDate
                mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCreditApplicable
                mCreditApp = Trim(UCase(VB.Left(.Text, 1)))

                If mCreditApp = "" Then
                    MsgInformation("Claim Applicable is blank for " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If

                '            If mClaimValue = "Y" Then
                '                If Format(mBillDate, "MMYYYY") > Format(mFromDate, "MMYYYY") Then
                '                    MsgInformation "You Cann't be take Credit before one Month. For Party Name : " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate
                '                    Screen.MousePointer = vbDefault
                '                    Exit Sub
                '                End If
                If mApproved = "N" And mStatus = "N" Then

                Else
                    If mStatus = "Y" And mCreditApp = "N" Then
                        MsgInformation("You Cann't be take Credit of " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    ElseIf mStatus = "N" And mCreditApp = "Y" Then
                        MsgInformation("You have to take Credit of " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If
                End If
                '            End If
            Next
        End With


        If Update1 = False Then GoTo ErrPart


        cmdSave.Enabled = False

        '    Call cmdShow_Click

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        If Err.Number <> 0 Then
            ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        End If
        cmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function CheckDateValidation(ByRef mLockDate As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing


        mSqlStr = "SELECT MAX(GST_CLAIM_DATE) AS GST_CLAIM_DATE  " & vbCrLf & " FROM FIN_GST_NEWSEQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND GST_CLAIM_DATE>TO_DATE('" & VB6.Format(txtClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        mSqlStr = mSqlStr & vbCrLf & "HAVING MAX(GST_CLAIM_DATE) IS NOT NULL"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mLockDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GST_CLAIM_DATE").Value), "", RsTemp.Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
            CheckDateValidation = False
        Else
            mLockDate = ""
            CheckDateValidation = True
        End If

        Exit Function
ErrPart:
        If Err.Number <> 0 Then
            ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        End If
        CheckDateValidation = False
    End Function

    Private Sub frmGSTAgtRCClaimEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblView.Text = "V" Then
            Me.Text = "GST Against Reverse Charge Claim (Goods) Register"
        Else
            Me.Text = "GST Against Reverse Charge Claim (Goods) Entry"
        End If

        cboShow.Enabled = True ' IIf(lblView.text = "V", True, False)
        '    If PubSuperUser = "S" Or PubSuperUser = "A" Then
        '        cboShow.Enabled = True
        '    End If

        If lblView.Text = "V" Then
            txtClaimDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        End If

        cboShow.Enabled = IIf(lblView.Text = "V", True, False)
        cmdSave.Enabled = IIf(lblView.Text = "V", False, True)

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmGSTAgtRCClaimEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGSTAgtRCClaimEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        mAuthorised = IIf(InStr(1, XRIGHT, "S") > 0, True, False)

        MainClass.SetControlsColor(Me)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Compelete")
        cboShow.Items.Add("Pending")
        cboShow.SelectedIndex = 2

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
    Private Sub frmGSTAgtRCClaimEntry_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

        cmdSave.Enabled = IIf(lblView.Text = "V", False, True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVerification() = False Then GoTo LedgError


        SqlStr = MakeSQLMRR()

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

        If txtClaimDate.Text = "__/__/____" Or txtClaimDate.Text = "" Then
            MsgInformation("Please Enter the Claim Date First.")
            txtClaimDate.Focus()
            FieldsVerification = False
            Exit Function
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

        'NVL(HMST.GST_APP,'N')

        MakeSQLMRR = " SELECT IH.MKEY, " & vbCrLf _
            & " IH.GST_CLAIM_RC_NO, IH.GST_CLAIM_RC_DATE, " & vbCrLf _
            & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " IH.SAC_CODE, IH.GST_CLAIM_APP AS GST_APP, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETVALUE)) AS INV_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETCGST_AMOUNT + IH.NETSGST_AMOUNT + IH.NETIGST_AMOUNT)) AS GST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETCGST_AMOUNT)) AS CGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETSGST_AMOUNT)) AS SGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETIGST_AMOUNT)) AS IGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.BOOKCODE,'0'), IH.BOOKTYPE, IH.BOOKSUBTYPE, DECODE(INVOICESEQTYPE,8,'G','S') AS GOODS_SERV, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE)) AS ITEMVALUE, IH.DIV_CODE, HMST.HSN_DESC," & vbCrLf _
            & " DECODE(GST_RC_CLAIM,'Y',1,0) AS STATUS, DECODE(GST_RC_CLAIM,'A',1,0) AS APPROVAL"

        ''FROM CLAUSE...
        MakeSQLMRR = MakeSQLMRR & vbCrLf & " FROM FIN_INVOICE_HDR IH,  FIN_SUPP_CUST_MST CMST, GEN_HSN_MST HMST"
        ''WHERE CLAUSE...
        MakeSQLMRR = MakeSQLMRR & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=HMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.SAC_CODE=HMST.HSN_CODE(+)"

        MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND INVOICESEQTYPE IN (7, 8)"

        If cboShow.SelectedIndex = 1 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND GST_RC_CLAIM='Y'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND GST_RC_CLAIM='N'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLMRR = MakeSQLMRR & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        MakeSQLMRR = MakeSQLMRR & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLMRR = MakeSQLMRR & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''ORDER CLAUSE...

        If optOrderBy(0).Checked = True Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME, IH.BILLNO, IH.INVOICE_DATE"
        ElseIf optOrderBy(1).Checked = True Then
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "ORDER BY IH.BILLNO, IH.INVOICE_DATE"
        Else
            MakeSQLMRR = MakeSQLMRR & vbCrLf & "ORDER BY IH.VNO, IH.VDATE"
        End If

        Exit Function
ERR1:
        '    Resume
        MsgInformation(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim i As Integer
        Dim mCurRowNo As Integer
        Dim xMkey As String = ""
        Dim mClaimDate As String
        Dim mClaimNo As Double
        Dim mStatus As String
        Dim mVNO As String
        Dim mVDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mSuppCustCode As String
        Dim mAccountCode As String
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mBookCode As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mCapital As String
        Dim mItemCode As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mItemCGSTAmount As Double
        Dim mItemSGSTAmount As Double
        Dim mItemIGSTAmount As Double
        Dim mDivCode As Double
        Dim mHSNCode As String
        Dim mItemDesc As String
        Dim mPOS As String
        Dim mRefType As String
        Dim mShortageQty As Double
        Dim mMRRNo As Double
        Dim mGoodsServices As String

        Dim mSACCode As String
        Dim mSACDesc As String
        Dim mAmount As Double
        'Dim mDiv As Long
        Dim mApproved As String
        Dim mClaimValue As String
        Dim mLocationId As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                mClaimValue = mStatus

                .Col = ColApproved
                mApproved = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "A", "N")

                mClaimValue = IIf(mClaimValue = "N", mApproved, mClaimValue)

                If mClaimValue = "N" Then GoTo NextRow

                .Col = ColMKEY
                xMkey = Trim(.Text)


                .Col = ColRefNo
                mClaimNo = Val(.Text)

                .Col = ColBillNo
                mBillNo = Trim(.Text)

                .Col = ColBillDate
                mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCustCode
                mSuppCustCode = Trim(.Text)

                .Col = ColCGST
                mCGSTAmount = Val(.Text)

                .Col = ColSGST
                mSGSTAmount = Val(.Text)

                .Col = ColIGST
                mIGSTAmount = Val(.Text)

                .Col = ColBookCode
                mBookCode = Trim(.Text)

                .Col = ColBookType
                mBookType = Trim(.Text)

                .Col = ColBookSubType
                mBookSubType = Trim(.Text)

                .Col = ColGoodServ
                mGoodsServices = Trim(.Text)


                .Col = ColAmount
                mAmount = CDbl(VB6.Format(.Text, "0.00"))

                .Col = ColDiv
                mDivCode = Val(.Text)

                .Col = ColSACDesc
                mSACDesc = Trim(.Text)

                .Col = ColSACCode
                mSACCode = Trim(.Text)

                If (mClaimValue = "Y" Or mClaimValue = "A") And mClaimNo <= 0 Then
                    mClaimNo = CDbl(AutoGenSeqGSTAppNo(mClaimValue, "R"))
                    mClaimDate = VB6.Format(txtClaimDate.Text, "DD/MM/YYYY")

                    .Col = ColRefNo
                    .Text = Trim(CStr(mClaimNo))

                    .Col = ColRefDate
                    .Text = Trim(mClaimDate)

                    SqlStr = "UPDATE FIN_INVOICE_HDR SET" & vbCrLf & " GST_CLAIM_RC_NO=" & Val(CStr(mClaimNo)) & ", " & vbCrLf _
                        & " GST_CLAIM_RC_DATE=TO_DATE('" & VB6.Format(mClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " GST_RC_CLAIM='" & mClaimValue & "', " & vbCrLf _
                        & " TOTCGST_RC_REFUNDAMT=" & mCGSTAmount & "," & vbCrLf & " TOTSGST_RC_REFUNDAMT=" & mSGSTAmount & ", " & vbCrLf _
                        & " TOTIGST_RC_REFUNDAMT=" & mIGSTAmount & ", " & vbCrLf & " UPDATE_FROM='H', " & vbCrLf _
                        & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"

                    PubDBCn.Execute(SqlStr)


                    If UpdateGSTAppSeqMaster(PubDBCn, xMkey, mBookCode, mBookType, mBookSubType, mClaimNo, VB6.Format(mClaimDate, "DD-MMM-YYYY"), mClaimValue, "R") = False Then GoTo UpdateErr

                    mSuppCustCode = IIf(IsDBNull(RsCompany.Fields("COMPANY_ACCTCODE").Value), "", RsCompany.Fields("COMPANY_ACCTCODE").Value)
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("COMPANY_ACCTCODE").Value), "", RsCompany.Fields("COMPANY_ACCTCODE").Value)

                    If UpdateGSTTRN(PubDBCn, xMkey, CStr(ConRCSalesBookCode), mBookType, mBookSubType, mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), Trim(mBillNo), VB6.Format(mBillDate, "DD-MMM-YYYY"), "", "", mSuppCustCode, mAccountCode, "Y", mSuppCustCode, 1, "-1", 1, "NOS", mAmount, mAmount, mAmount, 0, 0, 0, 0, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivCode, mSACCode, Trim(mSACDesc), "", "N", mGoodsServices, mGoodsServices, "Y", "C", mClaimDate, "N") = False Then GoTo UpdateErr

                    ''Account Posting
                    If mStatus = "Y" Then
                        mLocationId = GetDefaultLocation(mSuppCustCode)
                        If mLocationId = "" Then
                            If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_CITY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                                mLocationId = MasterNo
                            End If
                        End If
                        mCurRowNo = 1
                        If RCPurchasePostTRNGST(PubDBCn, xMkey, mCurRowNo, CStr(ConRCSalesBookCode), mBookType, mBookSubType, mBillNo, mBillDate, mBillNo, mBillDate, mSuppCustCode, False, mClaimDate, "REVERSE CHARGE CLAIM", "", "Y", Val(CStr(mCGSTAmount)), Val(CStr(mSGSTAmount)), Val(CStr(mIGSTAmount)), mBillDate, ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, mLocationId) = False Then GoTo UpdateErr
                    End If

                End If
NextRow:
            Next
        End With
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        '    MsgBox err.Description
        '    Resume
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        '    Resume
    End Function
    Private Function AutoGenGSTNo(ByRef mBookType As String, ByRef pStartingNo As Integer, ByRef pIsServiceRefund As String) As String

        On Error GoTo AutoGenServNoErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Double
        Dim mMaxNo As Double
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = "SELECT Max(SERVNO)  FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND SERVDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SERVDATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & "AND ISSERVTAX_POST='Y'" & vbCrLf & " AND ISPLA='N'"

        SqlStr = SqlStr & vbCrLf & "AND SERVICE_REFUND='" & pIsServiceRefund & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = False Then
                mMaxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMaxNo <= 0 Then
                    mNewSeqBillNo = pStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = pStartingNo
                End If
            Else
                mNewSeqBillNo = pStartingNo
            End If
        End With
        AutoGenGSTNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenServNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub frmGSTAgtRCClaimEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With

        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        cmdSave.Enabled = IIf(lblView.Text = "V", False, True)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        cmdSave.Enabled = IIf(lblView.Text = "V", False, True)
        '     SprdMain.Col = ColStatus
        '     SprdMain.Row = SprdMain.ActiveRow
        '     SprdMain.Text = "Y"
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
        '    If lblBookType.text = "M" Then
        '        If Not IsDate(xVDate) Then Exit Sub
        '        If CDate(xVDate) >= CDate(RsCompany!Start_Date) And CDate(xVDate) <= CDate(RsCompany!END_DATE) Then
        '            Call ShowTrn(xMKey, xVDate, "", xVNo, "P", "")
        '        End If
        ''    Else
        ''        Call ShowTrn(xMkey, xVDate, "", xVNo, "J", "")
        '    End If
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

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
