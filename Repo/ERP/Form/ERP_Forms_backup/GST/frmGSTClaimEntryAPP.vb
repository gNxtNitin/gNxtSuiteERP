Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTClaimEntryApp
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mSearchStartRow As Integer

    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColUnitCode As Short = 2
    Private Const ColUnitName As Short = 3
    Private Const ColRefNo As Short = 4
    Private Const ColRefDate As Short = 5
    Private Const ColMRRNo As Short = 6
    Private Const ColMRRDate As Short = 7
    Private Const ColVNo As Short = 8
    Private Const ColVDate As Short = 9
    Private Const ColBillNo As Short = 10
    Private Const ColBillDate As Short = 11
    Private Const ColCustCode As Short = 12
    Private Const ColCustGSTNo As Short = 13
    Private Const ColCustName As Short = 14
    Private Const ColInvAmount As Short = 15
    Private Const ColCGST As Short = 16
    Private Const ColSGST As Short = 17
    Private Const ColIGST As Short = 18
    Private Const ColTotalGSTAmount As Short = 19
    Private Const ColCGSTRefund As Short = 20
    Private Const ColSGSTRefund As Short = 21
    Private Const ColIGSTRefund As Short = 22
    Private Const ColBookCode As Short = 23
    Private Const ColBookType As Short = 24
    Private Const ColBookSubType As Short = 25
    Private Const ColGoodServ As Short = 26
    Private Const ColAmount As Short = 27
    Private Const ColDiv As Short = 28
    Private Const ColPaymentDate As Short = 29
    Private Const ColAccountHead As Short = 30
    Private Const ColInvoiceType As Short = 31
    Private Const ColAddUser As Short = 32
    Private Const ColAddDate As Short = 33
    Private Const ColModUser As Short = 34
    Private Const ColModDate As Short = 35

    Private Const ColAccountStatus As Short = 36
    Private Const ColStatus As Short = 37
    Private Const ColApproved As Short = 38
    Dim mClickProcess As Boolean

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

            .Col = ColUnitName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColUnitName, 12)

            .Col = ColUnitCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColUnitCode, 12)
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

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMRRNo, 9)
            .ColHidden = False

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMRRDate, 9)
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

            '.ColsFrozen = ColBillNo

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
            .ColsFrozen = ColCustName

            For cntCol = ColInvAmount To ColIGSTRefund
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

            For cntCol = ColDiv To ColDiv
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 4)
                .ColHidden = False 'True
            Next

            .Col = ColPaymentDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColPaymentDate, 9)
            .ColHidden = False

            .Col = ColAccountHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAccountHead, 20)
            .ColHidden = False

            .Col = ColInvoiceType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColInvoiceType, 20)
            .ColHidden = False

            .Col = ColAddUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAddUser, 12)
            .ColHidden = False

            .Col = ColAddDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAddDate, 12)
            .ColHidden = False

            .Col = ColModUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColModUser, 12)
            .ColHidden = False

            .Col = ColModDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColModDate, 12)
            .ColHidden = False


            .Col = ColAccountStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAccountStatus, 10)


            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .ColHidden = IIf(lblView.Text = "V", True, False)
            .set_ColWidth(ColStatus, 6)

            .Col = ColApproved
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .ColHidden = IIf(lblView.Text = "V", True, False)
            .set_ColWidth(ColApproved, 6)

            '        .Value = vbChecked



            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditMultiLine = False

            ''        .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColAccountStatus)
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

            .Col = ColUnitName
            .Text = "Unit"

            .Col = ColUnitCode
            .Text = "Unit Code"

            .Col = ColAccountHead
            .Text = "Account Head"

            .Col = ColInvoiceType
            .Text = "Invoice Type"

            .Col = ColAddUser
            .Text = "Add User"

            .Col = ColAddDate
            .Text = "Add Date"

            .Col = ColModUser
            .Text = "Mod User"

            .Col = ColModDate
            .Text = "Mod Date"

            .Col = ColRefNo
            .Text = "Claim No"

            .Col = ColRefDate
            .Text = "Claim Date"

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

            .Col = ColInvAmount
            .Text = "Inv Amount"

            .Col = ColCGST
            .Text = "CGST Amount"

            .Col = ColSGST
            .Text = "SGST Amount"

            .Col = ColIGST
            .Text = "IGST Amount"

            .Col = ColTotalGSTAmount
            .Text = "Total Claim Amount"

            .Col = ColCGSTRefund
            .Text = "CGST Claim Amount"

            .Col = ColSGSTRefund
            .Text = "SGST Claim Amount"

            .Col = ColIGSTRefund
            .Text = "IGST Claim Amount"

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

            .Col = ColGoodServ
            .Text = "SAC Description"

            .Col = ColPaymentDate
            .Text = "Payment Date"

            .Col = ColAccountStatus
            .Text = "Status in Accounts"

            .Col = ColStatus
            .Text = "Claim Eligible"

            .Col = ColApproved
            .Text = "Approved"


            .set_RowHeight(0, 20)
        End With
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr

        Clear1()

        Show1()
        Call CalcSprdTotal()
        Call FormatSprdMain(-1)

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mTotValue As Double

        Call MainClass.AddBlankfpSprdRow(SprdMain, ColMKEY)
        FormatSprdMain(-1)

        With SprdMain
            .Col = ColCustName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False


            For cntCol = ColInvAmount To ColIGSTRefund
                mTotValue = 0
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = cntCol
                    mTotValue = mTotValue + Val(.Text)
                Next
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotValue, "0.00")
            Next
            For cntCol = ColAmount To ColAmount
                mTotValue = 0
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = cntCol
                    mTotValue = mTotValue + Val(.Text)
                Next
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotValue, "0.00")
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrPart
        Dim mFromDate As String
        Dim I As Integer
        Dim mStatus As String
        Dim mBillDate As String
        Dim mVDate As String

        Dim mPartyName As String
        Dim mBillNo As String
        Dim mGSTAmount As Double
        Dim mApproved As String
        Dim mAccountStatus As String
        Dim mLockDate As String
        Dim mGSTNo As String

        Dim mGSTRefund As Double

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
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
        Else
            If CheckDateValidation(mLockDate) = False Then
                MsgInformation("GST Already Claimed till " & mLockDate & " Date. Please change the Claim date")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If





        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColCustGSTNo
                mGSTNo = Trim(.Text)

                .Col = ColAccountStatus
                mAccountStatus = VB.Left(.Text, 1)

                .Col = ColStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColApproved
                mApproved = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "A", "N")

                .Col = ColCustName
                mPartyName = Trim(.Text)

                .Col = ColVDate
                mVDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColBillNo
                mBillNo = Trim(.Text)

                .Col = ColBillDate
                mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColTotalGSTAmount
                mGSTAmount = Val(.Text)

                mGSTRefund = 0
                .Col = ColCGSTRefund
                mGSTRefund = Val(.Text)

                .Col = ColSGSTRefund
                mGSTRefund = mGSTRefund + Val(.Text)

                .Col = ColIGSTRefund
                mGSTRefund = mGSTRefund + Val(.Text)


                If mStatus = "Y" Then
                    If CDate(mBillDate) > CDate(mFromDate) Then
                        MsgInformation("Bill Date Can't be Greater than Credit date. For Party Name : " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If

                    If CDate(mVDate) > CDate(mFromDate) Then
                        MsgInformation("Voucher Date Can't be Greater than Credit date. For Party Name : " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If

                    If mGSTAmount <= 0 Then
                        MsgInformation("You Cann't be take Credit of " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If
                End If

                If mGSTRefund = 0 And mStatus = "N" And mApproved = "A" Then

                ElseIf mAccountStatus = "G" And mStatus = "N" And mApproved = "A" Then
                    If RsCompany.Fields("COMPANY_GST_RGN_NO").Value = mGSTNo Then

                    Else
                        MsgInformation("In Accounts GST Credit is Applicable, So please first Correct in Account for Party : " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If
                End If

                If mAccountStatus <> "G" And mStatus = "Y" Then
                    MsgInformation("In Accounts GST Credit is Not Applicable, So please first Correct in Account for Party : " & mPartyName & " of Bill No & Date : " & mBillNo & "-" & mBillDate)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If

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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Function CheckDateValidation(ByRef mLockDate As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing


        mSqlStr = "SELECT MAX(GST_CLAIM_DATE) AS GST_CLAIM_DATE  " & vbCrLf & " FROM FIN_GST_NEWSEQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND GST_CLAIM_DATE>TO_DATE('" & VB6.Format(txtClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        'If RsCompany.Fields("COMPANY_CODE").Value = 15 Or RsCompany.Fields("COMPANY_CODE").Value = 25 Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND BOOKCODE <> " & ConSalesBookCode & ""
        'End If

        mSqlStr = mSqlStr & vbCrLf & " HAVING MAX(GST_CLAIM_DATE) IS NOT NULL"

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

    Private Sub cmdFind_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFind.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim I As Integer
        Dim mCol As Integer


        '    mSearchStartRow = 0
        If mSearchStartRow = SprdMain.MaxRows Then mSearchStartRow = 0

        If OptSearch(0).Checked = True Then
            mCol = ColMRRNo
        ElseIf OptSearch(1).Checked = True Then
            mCol = ColBillNo
        ElseIf OptSearch(2).Checked = True Then
            mCol = ColVNo
        End If

        mSearchItem = Trim(txtSearch.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = mCol
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, mCol)
                    mSearchStartRow = I + 1
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

    Private Sub frmGSTClaimEntryApp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblView.Text = "V" Then
            Me.Text = "GST Claim Approval Register"
        Else
            Me.Text = "GST Claim Approval Entry"
        End If

        cboShow.Enabled = True ' IIf(lblView.text = "V", True, False)
        '    If PubSuperUser = "S" Or PubSuperUser = "A" Then
        '        cboShow.Enabled = True
        '    End If

        If lblView.Text = "V" Then
            txtClaimDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        End If

        txtClaimDate.Enabled = IIf(lblView.Text = "V", False, True)
        cboShow.Enabled = IIf(lblView.Text = "V", True, False)
        cmdSave.Enabled = IIf(lblView.Text = "V", False, True)

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmGSTClaimEntryApp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    If KeyAscii = vbKeyReturn Then SendKeys "{Tab}"
        '    If KeyAscii = vbKeyF1 Then txtSearch.SetFocus

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGSTClaimEntryApp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Compelete")
        cboShow.Items.Add("Claim Only")
        cboShow.Items.Add("Approval (Other Than Claim)")
        cboShow.Items.Add("Pending")
        cboShow.SelectedIndex = 4



        cboGSTType.Items.Add("ALL")
        cboGSTType.Items.Add("GST APPLICABLE")
        cboGSTType.Items.Add("INLIGIBLE")
        cboGSTType.Items.Add("NON-GST")
        cboGSTType.Items.Add("EXEMPTED")
        cboGSTType.Items.Add("REVERSE CHARGE")
        cboGSTType.Items.Add("GST NOT APPLICABLE")
        cboGSTType.SelectedIndex = 0

        Call FillInvoiceType()

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
    Private Sub frmGSTClaimEntryApp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
        txtClaimDate.Enabled = IIf(lblView.Text = "V", False, True)
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

        SqlStr = " SELECT MKEY, COMPANY_CODE, COMPANY_SHORTNAME," & vbCrLf _
            & " GST_CLAIM_NEW_NO, GST_CLAIM_NEW_DATE, " & vbCrLf _
            & " MRRNO, MRRDATE, VNO, VDATE," & vbCrLf _
            & " BILLNO, INVOICE_DATE, " & vbCrLf _
            & " SUPP_CUST_CODE, GST_RGN_NO, SUPP_CUST_NAME, " & vbCrLf _
            & " INV_AMOUNT, CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, " & vbCrLf _
            & " GST_AMOUNT, CGST_REFUNDAMT, SGST_REFUNDAMT, IGST_REFUNDAMT, " & vbCrLf _
            & " BOOKCODE,BOOKTYPE, BOOKSUBTYPE, GOODS_SERV, ITEMVALUE, DIV_CODE, PAYMENTDATE, ACCOUNTHEAD, INVOICEHEAD, ADDUSER, ADDDATE, MODUSER, MODDATE," & vbCrLf _
            & " GST_STATUS, " & vbCrLf _
            & " STATUS, APPROVED " & vbCrLf _
            & " FROM ("


        SqlStr = SqlStr & vbCrLf & MakeSQLPurchase()

        SqlStr = SqlStr & vbCrLf & " UNION ALL" & MakeSQLPurService()

        SqlStr = SqlStr & vbCrLf & " UNION ALL" & MakeSQLPurchaseSupp()

        SqlStr = SqlStr & vbCrLf & " UNION ALL" & MakeSQLLCOpen()

        SqlStr = SqlStr & vbCrLf & " UNION ALL" & MakeSQLLCDisc()

        SqlStr = SqlStr & vbCrLf & ")"

        If optOrderBy(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY SUPP_CUST_NAME, BILLNO, INVOICE_DATE"
        ElseIf optOrderBy(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY VNO, VDATE"
        ElseIf optOrderBy(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY VNO, VDATE"
        ElseIf optOrderBy(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY MRRNO"
        End If

        '    If optOrderBy(0).Value = True Then
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY 9, 6, 7"
        '    ElseIf optOrderBy(1).Value = True Then
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY 4, 5"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY 4, 5"
        '    End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function MakeSQLLCOpen() As String

        On Error GoTo ERR1
        Dim mAccountCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        MakeSQLLCOpen = " SELECT IH.MKEY, IH.COMPANY_CODE, GEN.COMPANY_SHORTNAME," & vbCrLf _
        & " IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, TO_CHAR(IH.GST_CLAIM_DATE,'DD/MM/YYYY') AS GST_CLAIM_NEW_DATE, '' AS MRRNO,'' AS MRRDATE," & vbCrLf _
        & " IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
        & " IH.REF_NO AS BILLNO, TO_CHAR(IH.REF_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf _
        & " IH.BANK_CODE AS SUPP_CUST_CODE,CMST.GST_RGN_NO, CMST.SUPP_CUST_NAME, " & vbCrLf _
        & " TO_CHAR(IH.NETVALUE) AS INV_AMOUNT, " & vbCrLf _
        & " TO_CHAR(IH.TOTCGST_AMOUNT) AS CGST_AMOUNT, " & vbCrLf _
        & " TO_CHAR(IH.TOTSGST_AMOUNT) AS SGST_AMOUNT, " & vbCrLf _
        & " TO_CHAR(IH.TOTIGST_AMOUNT) AS IGST_AMOUNT, " & vbCrLf _
        & " TO_CHAR(IH.TOTCGST_CREDITAMT + IH.TOTSGST_CREDITAMT + TOTIGST_CREDITAMT) AS GST_AMOUNT, " & vbCrLf _
        & " TO_CHAR(IH.TOTCGST_CREDITAMT) AS CGST_REFUNDAMT, " & vbCrLf _
        & " TO_CHAR(IH.TOTSGST_CREDITAMT) AS SGST_REFUNDAMT, " & vbCrLf _
        & " TO_CHAR(IH.TOTIGST_CREDITAMT) AS IGST_REFUNDAMT, " & vbCrLf _
        & " TO_CHAR(" & ConLCBookCode & ") AS BOOKCODE, SUBSTR(IH.BOOKTYPE,1,1) AS BOOKTYPE, SUBSTR(IH.BOOKTYPE,2,1) AS BOOKSUBTYPE, 'S' AS GOODS_SERV, " & vbCrLf _
        & " TO_CHAR(IH.ITEMVALUE) AS ITEMVALUE, IH.DIV_CODE,"

        If chkPaymentDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",CMST.SUPP_CUST_CODE,IH.REF_NO, IH.REF_DATE) AS PAYMENTDATE,"
        Else
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " '' AS PAYMENTDATE,"
        End If

        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " '' AS ACCOUNTHEAD, '' as INVOICEHEAD, IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE,"

        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " 'GST APPPLICABLE' AS GST_STATUS," & vbCrLf _
            & " DECODE(GST_CLAIM,'Y','1','0') AS STATUS, DECODE(GST_CLAIM,'A','1','0') AS APPROVED"


        ''FROM CLAUSE...
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " FROM FIN_LCOPEN_HDR IH,  FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GEN"
        ''WHERE CLAUSE...
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf _
            & " WHERE IH.COMPANY_CODE = GEN.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=CMST.SUPP_CUST_CODE"

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
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If cboShow.SelectedIndex = 1 Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND GST_CLAIM ='Y'"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND GST_CLAIM ='A'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND GST_CLAIM='N'"
        End If

        If cboGSTType.SelectedIndex = 0 Then
        ElseIf cboGSTType.SelectedIndex = 1 Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND 1=1"
        Else
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND 1=2"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND IH.BANK_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If OptShowDate(0).Checked = True Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " AND IH.GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " AND IH.GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        ''ORDER CLAUSE...



        Exit Function
ERR1:
        '    Resume
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLLCDisc() As String

        On Error GoTo ERR1
        Dim mAccountCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""


        MakeSQLLCDisc = " SELECT IH.MKEY, IH.COMPANY_CODE, GEN.COMPANY_SHORTNAME," & vbCrLf _
            & " IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, TO_CHAR(IH.GST_CLAIM_DATE,'DD/MM/YYYY') AS GST_CLAIM_NEW_DATE, '' AS MRRNO,'' AS MRRDATE," & vbCrLf & " IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
            & " IH.REF_NO AS BILLNO, TO_CHAR(IH.REF_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " IH.BANK_CODE AS SUPP_CUST_CODE, CMST.GST_RGN_NO,CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " TO_CHAR(IH.NETVALUE) AS INV_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.TOTCGST_AMOUNT) AS CGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.TOTSGST_AMOUNT) AS SGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.TOTIGST_AMOUNT) AS IGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.TOTCGST_CREDITAMT + IH.TOTSGST_CREDITAMT + TOTIGST_CREDITAMT) AS GST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(IH.TOTCGST_CREDITAMT) AS CGST_REFUNDAMT, " & vbCrLf _
            & " TO_CHAR(IH.TOTSGST_CREDITAMT) AS SGST_REFUNDAMT, " & vbCrLf _
            & " TO_CHAR(IH.TOTIGST_CREDITAMT) AS IGST_REFUNDAMT, " & vbCrLf _
            & " TO_CHAR(" & ConLDBookCode & ") AS BOOKCODE, SUBSTR(IH.BOOKTYPE,1,1) AS BOOKTYPE, SUBSTR(IH.BOOKTYPE,2,1) AS BOOKSUBTYPE, 'S' AS GOODS_SERV, " & vbCrLf _
            & " TO_CHAR(IH.ITEMVALUE) AS ITEMVALUE, IH.DIV_CODE,"

        If chkPaymentDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",CMST.SUPP_CUST_CODE,IH.REF_NO, IH.REF_DATE) AS PAYMENTDATE,"
        Else
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " '' AS PAYMENTDATE,"
        End If

        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " '' AS ACCOUNTHEAD, '' as INVOICEHEAD, IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE,"

        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " 'GST APPPLICABLE' AS GST_STATUS," & vbCrLf _
            & " DECODE(GST_CLAIM,'Y','1','0') AS STATUS, DECODE(GST_CLAIM,'A','1','0') AS APPROVED"


        ''FROM CLAUSE...
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " FROM FIN_LCDISC_HDR IH,  FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GEN"
        ''WHERE CLAUSE...
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=CMST.SUPP_CUST_CODE"

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
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND GST_CLAIM='Y'"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND GST_CLAIM='A'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND GST_CLAIM='N'"
        End If

        If cboGSTType.SelectedIndex = 0 Then
        ElseIf cboGSTType.SelectedIndex = 1 Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND 1=1"
        Else
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND 1=2"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND IH.BANK_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If OptShowDate(0).Checked = True Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " AND IH.GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " AND IH.GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If



        ''ORDER CLAUSE...



        Exit Function
ERR1:
        '    Resume
        MsgInformation(Err.Description)
    End Function
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

    Private Function MakeSQLPurchase() As String

        On Error GoTo ERR1

        Dim mAccountCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        MakeSQLPurchase = " SELECT IH.MKEY, IH.COMPANY_CODE, GEN.COMPANY_SHORTNAME," & vbCrLf _
            & " IH.GST_CLAIM_NEW_NO, TO_CHAR(IH.GST_CLAIM_NEW_DATE,'DD/MM/YYYY') AS GST_CLAIM_NEW_DATE, " & vbCrLf _
            & " DECODE(TO_CHAR(IH.AUTO_KEY_MRR),'-1','',TO_CHAR(IH.AUTO_KEY_MRR)) AS MRRNO, CASE WHEN TO_CHAR(IH.AUTO_KEY_MRR)='-1' OR TO_CHAR(IH.AUTO_KEY_MRR)='' THEN '' ELSE  TO_CHAR(IH.MRRDATE,'DD/MM/YYYY') END AS MRRDATE, IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE," & vbCrLf _
            & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.GST_RGN_NO, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETVALUE)) AS INV_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTCGST_AMOUNT)) AS CGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTSGST_AMOUNT)) AS SGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTIGST_AMOUNT)) AS IGST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTCGST_REFUNDAMT + IH.TOTSGST_REFUNDAMT + IH.TOTIGST_REFUNDAMT)) AS GST_AMOUNT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTCGST_REFUNDAMT)) AS CGST_REFUNDAMT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTSGST_REFUNDAMT)) AS SGST_REFUNDAMT, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTIGST_REFUNDAMT)) AS IGST_REFUNDAMT, " & vbCrLf _
            & " TO_CHAR(IH.BOOKCODE) AS BOOKCODE, IH.BOOKTYPE, IH.BOOKSUBTYPE, DECODE(PURCHASE_TYPE,'G','G','S') AS GOODS_SERV, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE)) AS ITEMVALUE, IH.DIV_CODE, "


        If chkPaymentDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
                & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",CMST.SUPP_CUST_CODE,IH.BILLNO, IH.INVOICE_DATE) AS PAYMENTDATE,"
        Else
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " '' AS PAYMENTDATE,"
        End If

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            & " AMST.SUPP_CUST_NAME AS ACCOUNTHEAD, "


        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            & " (SELECT B.SUPP_CUST_NAME FROM  FIN_PURCHASE_DET A, FIN_SUPP_CUST_MST B WHERE A.MKEY=IH.MKEY AND A.COMPANY_CODE=B.COMPANY_CODE AND A.PUR_ACCOUNT_CODE=B.SUPP_CUST_CODE  FETCH FIRST 1 ROWS ONLY)AS INVOICEHEAD, "

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            & " IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE,"

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            & " CASE WHEN ISGSTAPPLICABLE='G' THEN 'GST APPLICABLE'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='R' THEN 'REVERSE CHARGE'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='E' THEN 'EXEMPTED'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='N' THEN 'NON-GST'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='I' THEN 'INLIGIBLE'" & vbCrLf _
            & " END AS GST_STATUS, " & vbCrLf _
            & " DECODE(GST_CLAIM,'Y','1','0') AS STATUS," & vbCrLf _
            & " DECODE(GST_CLAIM,'A','1','0') AS APPROVED"



        ''FROM CLAUSE...
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM FIN_PURCHASE_HDR IH,  FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GEN, FIN_SUPP_CUST_MST AMST"
        ''WHERE CLAUSE...
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf & " AND IH.ACCOUNTCODE=AMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.CANCELLED='N' AND IH.ISFINALPOST='Y'"


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
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND PURCHASE_TYPE IN ('G','J','R')" ''S','W'

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.CANCELLED='N' "

        '    If cboShow.ListIndex = 1 Then
        '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        '    ElseIf cboShow.ListIndex = 4 Then
        '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND GST_CLAIM='N'"
        '    End If

        If cboShow.SelectedIndex = 0 Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.PURCHASESEQTYPE <> CASE WHEN GST_CLAIM IN ('Y','A') THEN 99 ELSE 2 END"
        ElseIf cboShow.SelectedIndex = 1 Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND GST_CLAIM ='Y'"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND GST_CLAIM ='A'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND GST_CLAIM='N' AND IH.PURCHASESEQTYPE<>2"
        End If

        If cboGSTType.SelectedIndex = 0 Then
        ElseIf cboGSTType.SelectedIndex = 6 Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND 1=2"
        Else
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND ISGSTAPPLICABLE= '" & Mid(cboGSTType.Text, 1, 1) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If OptShowDate(0).Checked = True Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        ''ORDER CLAUSE...



        Exit Function
ERR1:
        '    Resume
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLPurService() As String

        On Error GoTo ERR1

        Dim mAccountCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        MakeSQLPurService = " SELECT IH.MKEY, IH.COMPANY_CODE, GEN.COMPANY_SHORTNAME," & vbCrLf _
            & " IH.GST_CLAIM_NEW_NO, TO_CHAR(IH.GST_CLAIM_NEW_DATE,'DD/MM/YYYY') AS GST_CLAIM_NEW_DATE, " & vbCrLf _
            & " DECODE(TO_CHAR(IH.AUTO_KEY_MRR),'-1','',TO_CHAR(IH.AUTO_KEY_MRR)) AS MRRNO, CASE WHEN TO_CHAR(IH.AUTO_KEY_MRR)='-1' OR TO_CHAR(IH.AUTO_KEY_MRR)='' THEN '' ELSE  TO_CHAR(IH.MRRDATE,'DD/MM/YYYY') END AS MRRDATE, IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE," & vbCrLf & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.GST_RGN_NO, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(DECODE(IH.ITEMVALUE,0,0,IH.NETVALUE*ID.ITEM_AMT/IH.ITEMVALUE))) AS INV_AMOUNT, " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,ID.CGST_AMOUNT))) AS CGST_AMOUNT, " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,ID.SGST_AMOUNT))) AS SGST_AMOUNT, " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,ID.IGST_AMOUNT))) AS IGST_AMOUNT, "

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " TO_CHAR(SUM(CASE WHEN ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' AND CANCELLED='N' THEN ID.GSTABLE_AMT * (CGST_PER+SGST_PER+IGST_PER) * .01" & vbCrLf & " WHEN ISGSTAPPLICABLE IN ('R','E','N','I') OR ID.GST_RCAPP='Y' OR ID.GST_EXEMPTED='Y' OR ID.GST_CREDITAPP='N' OR CANCELLED='Y'  THEN 0" & vbCrLf & " END)) AS GST_AMOUNT, "

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " TO_CHAR(SUM(CASE WHEN ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' AND CANCELLED='N' THEN ID.GSTABLE_AMT * CGST_PER * .01" & vbCrLf & " WHEN ISGSTAPPLICABLE IN ('R','E','N','I') OR ID.GST_RCAPP='Y' OR ID.GST_EXEMPTED='Y' OR ID.GST_CREDITAPP='N' OR CANCELLED='Y'  THEN 0" & vbCrLf & " END)) AS CGST_REFUNDAMT, "

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " TO_CHAR(SUM(CASE WHEN ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' AND CANCELLED='N' THEN ID.GSTABLE_AMT * SGST_PER * .01" & vbCrLf & " WHEN ISGSTAPPLICABLE IN ('R','E','N','I') OR ID.GST_RCAPP='Y' OR ID.GST_EXEMPTED='Y' OR ID.GST_CREDITAPP='N' OR CANCELLED='Y'  THEN 0" & vbCrLf & " END)) AS SGST_REFUNDAMT, "

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " TO_CHAR(SUM(CASE WHEN ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' AND CANCELLED='N' THEN ID.GSTABLE_AMT * IGST_PER * .01" & vbCrLf & " WHEN ISGSTAPPLICABLE IN ('R','E','N','I') OR ID.GST_RCAPP='Y' OR ID.GST_EXEMPTED='Y' OR ID.GST_CREDITAPP='N' OR CANCELLED='Y'  THEN 0" & vbCrLf & " END)) AS IGST_REFUNDAMT, "


        MakeSQLPurService = MakeSQLPurService & vbCrLf & " TO_CHAR(IH.BOOKCODE) AS BOOKCODE, IH.BOOKTYPE, IH.BOOKSUBTYPE, DECODE(PURCHASE_TYPE,'G','G','S') AS GOODS_SERV, " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,ID.GSTABLE_AMT))) AS ITEMVALUE, IH.DIV_CODE, "

        ''GSTABLE_AMT
        If chkPaymentDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",CMST.SUPP_CUST_CODE,IH.BILLNO, IH.INVOICE_DATE) AS PAYMENTDATE,"
        Else
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " '' AS PAYMENTDATE,"
        End If

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " MAX(AMST.SUPP_CUST_NAME) AS ACCOUNTHEAD, MAX(AMST.SUPP_CUST_NAME) AS INVOICEHEAD, IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE,"

        MakeSQLPurService = MakeSQLPurService & vbCrLf _
            & " CASE WHEN ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' THEN 'GST APPLICABLE'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y' THEN 'REVERSE CHARGE'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='E' OR ID.GST_EXEMPTED='Y' THEN 'EXEMPTED'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='N' THEN 'NON-GST'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='I' THEN 'INLIGIBLE'" & vbCrLf _
            & " WHEN ID.GST_CREDITAPP='N' THEN 'GST NOT APPLICABLE'" & vbCrLf _
            & " END AS GST_STATUS, " & vbCrLf _
            & " DECODE(GST_CLAIM,'Y','1','0') AS STATUS," & vbCrLf _
            & " DECODE(GST_CLAIM,'A','1','0') AS APPROVED"



        ''FROM CLAUSE...
        MakeSQLPurService = MakeSQLPurService & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID,  FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GEN, FIN_SUPP_CUST_MST AMST"
        ''WHERE CLAUSE...
        MakeSQLPurService = MakeSQLPurService & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GEN.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.PUR_ACCOUNT_CODE=AMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.CANCELLED='N' AND IH.ISFINALPOST='Y'"

        '            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _

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
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND PURCHASE_TYPE IN ('S','W')"

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " AND IH.CANCELLED='N' "

        '    If cboShow.ListIndex = 1 Then
        '        MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        '    ElseIf cboShow.ListIndex = 4 Then
        '        MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND GST_CLAIM='N'"
        '    End If

        If cboShow.SelectedIndex = 0 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND IH.PURCHASESEQTYPE <> CASE WHEN GST_CLAIM IN ('Y','A') THEN 99 ELSE 2 END"
        ElseIf cboShow.SelectedIndex = 1 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND GST_CLAIM ='Y'"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND GST_CLAIM ='A'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND GST_CLAIM='N' AND IH.PURCHASESEQTYPE<>2"
        End If

        If cboGSTType.SelectedIndex = 0 Then

        ElseIf cboGSTType.SelectedIndex = 1 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N'"
        ElseIf cboGSTType.SelectedIndex = 2 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND ISGSTAPPLICABLE='I'"
        ElseIf cboGSTType.SelectedIndex = 3 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND ISGSTAPPLICABLE='N'"
        ElseIf cboGSTType.SelectedIndex = 4 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND (ISGSTAPPLICABLE='E' OR ID.GST_EXEMPTED='Y')"
        ElseIf cboGSTType.SelectedIndex = 5 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND (ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y')"
        ElseIf cboGSTType.SelectedIndex = 6 Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND ID.GST_CREDITAPP='N'"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLPurService = MakeSQLPurService & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If OptShowDate(0).Checked = True Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        ''GROUP BY CLAUSE... AMST.SUPP_CUST_NAME,

        MakeSQLPurService = MakeSQLPurService & vbCrLf _
            & " GROUP BY " & vbCrLf & " IH.MKEY, IH.COMPANY_CODE, GEN.COMPANY_SHORTNAME," & vbCrLf _
            & " IH.GST_CLAIM_NEW_NO, TO_CHAR(IH.GST_CLAIM_NEW_DATE,'DD/MM/YYYY'), " & vbCrLf & " DECODE(TO_CHAR(IH.AUTO_KEY_MRR),'-1','',TO_CHAR(IH.AUTO_KEY_MRR)) , CASE WHEN TO_CHAR(IH.AUTO_KEY_MRR)='-1' OR TO_CHAR(IH.AUTO_KEY_MRR)='' THEN '' ELSE  TO_CHAR(IH.MRRDATE,'DD/MM/YYYY') END, IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY')," & vbCrLf & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY'), " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.GST_RGN_NO, CMST.SUPP_CUST_NAME, "

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " TO_CHAR(IH.BOOKCODE), IH.BOOKTYPE, IH.BOOKSUBTYPE, DECODE(PURCHASE_TYPE,'G','G','S'), " & vbCrLf _
            & " IH.DIV_CODE, IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE,"

        ''GSTABLE_AMT
        If chkPaymentDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurService = MakeSQLPurService & vbCrLf & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",CMST.SUPP_CUST_CODE,IH.BILLNO, IH.INVOICE_DATE),"
        End If

        MakeSQLPurService = MakeSQLPurService & vbCrLf & " CASE WHEN ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' THEN 'GST APPLICABLE'" & vbCrLf & " WHEN ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y' THEN 'REVERSE CHARGE'" & vbCrLf & " WHEN ISGSTAPPLICABLE='E' OR ID.GST_EXEMPTED='Y' THEN 'EXEMPTED'" & vbCrLf & " WHEN ISGSTAPPLICABLE='N' THEN 'NON-GST'" & vbCrLf & " WHEN ISGSTAPPLICABLE='I' THEN 'INLIGIBLE'" & vbCrLf & " WHEN ID.GST_CREDITAPP='N' THEN 'GST NOT APPLICABLE'" & vbCrLf & " END, " & vbCrLf & " DECODE(GST_CLAIM,'Y','1','0')," & vbCrLf & " DECODE(GST_CLAIM,'A','1','0')"

        Exit Function
ERR1:
        '    Resume
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLPurchaseSupp() As String

        On Error GoTo ERR1

        Dim mAccountCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        MakeSQLPurchaseSupp = " SELECT IH.MKEY, IH.COMPANY_CODE, GEN.COMPANY_SHORTNAME," & vbCrLf _
            & " IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, TO_CHAR(IH.GST_CLAIM_DATE,'DD/MM/YYYY') AS GST_CLAIM_NEW_DATE, " & vbCrLf _
            & " '' AS MRRNO, '' AS MRRDATE, IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE," & vbCrLf _
            & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.GST_RGN_NO, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETVALUE)) AS INV_AMOUNT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTCGST_AMOUNT)) AS CGST_AMOUNT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTSGST_AMOUNT)) AS SGST_AMOUNT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTIGST_AMOUNT)) AS IGST_AMOUNT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTCGST_REFUNDAMT + IH.TOTSGST_REFUNDAMT + IH.TOTIGST_REFUNDAMT)) AS GST_AMOUNT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTCGST_REFUNDAMT)) AS CGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTSGST_REFUNDAMT)) AS SGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTIGST_REFUNDAMT)) AS IGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(" & ConPurchaseSuppBookCode & ") AS BOOKCODE, IH.BOOKTYPE, IH.BOOKSUBTYPE, 'G' AS GOODS_SERV, " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE)) AS ITEMVALUE, IH.DIV_CODE, "

        If chkPaymentDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",CMST.SUPP_CUST_CODE,IH.BILLNO, IH.INVOICE_DATE) AS PAYMENTDATE,"
        Else
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " '' AS PAYMENTDATE,"
        End If

        MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " AMST.SUPP_CUST_NAME AS ACCOUNTHEAD, AMST.SUPP_CUST_NAME AS INVOICEHEAD, IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE,"

        MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf _
            & " CASE WHEN ISGSTAPPLICABLE='G' THEN 'GST APPLICABLE'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='R' THEN 'REVERSE CHARGE'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='E' THEN 'EXEMPTED'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='N' THEN 'NON-GST'" & vbCrLf _
            & " WHEN ISGSTAPPLICABLE='I' THEN 'INLIGIBLE'" & vbCrLf _
            & " END AS GST_STATUS, " & vbCrLf & " DECODE(GST_CLAIM,'Y','1','0') AS STATUS," & vbCrLf & " DECODE(GST_CLAIM,'A','1','0') AS APPROVED"

        ''FROM CLAUSE...
        MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR IH,  FIN_SUPP_CUST_MST CMST,GEN_COMPANY_MST GEN, FIN_SUPP_CUST_MST AMST"
        ''WHERE CLAUSE...
        MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.ACCOUNTCODE=AMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.CANCELLED='N' AND IH.ISFINALPOST='Y'"


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
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        '    MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND PURCHASE_TYPE IN ('G','S','J','R','W')"

        '    If cboShow.ListIndex = 1 Then
        '        MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        '    ElseIf cboShow.ListIndex = 4 Then
        '        MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND GST_CLAIM='N'"
        '    End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND GST_CLAIM IN ('Y','A')"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND GST_CLAIM ='Y'"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND GST_CLAIM ='A'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND GST_CLAIM='N'"
        End If

        If cboGSTType.SelectedIndex = 0 Then
        ElseIf cboGSTType.SelectedIndex = 6 Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND 1=2"
        Else
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND ISGSTAPPLICABLE= '" & Mid(cboGSTType.Text, 1, 1) & "'"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If

            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If OptShowDate(0).Checked = True Then
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " AND IH.GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MakeSQLPurchaseSupp = MakeSQLPurchaseSupp & vbCrLf & " AND IH.GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        ''ORDER CLAUSE...



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

        Dim I As Integer
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
        Dim mDataUpdate As Boolean
        Dim mApproved As String
        Dim mClaimValue As String
        Dim mUnitCode As Long = 0
        Dim mSameUnit As Boolean = True
        'Dim mMRRNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColUnitCode
                mUnitCode = Val(.Text)
                mSameUnit = IIf(RsCompany.Fields("COMPANY_CODE").Value = mUnitCode, True, False)

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

                .Col = ColMRRNo
                mMRRNo = Val(Trim(.Text))

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

                If (mClaimValue = "Y" Or mClaimValue = "A") And mClaimNo <= 0 And mSameUnit = True Then
                    mClaimNo = CDbl(AutoGenSeqGSTAppNo(mClaimValue, "I"))
                    mClaimDate = VB6.Format(txtClaimDate.Text, "DD/MM/YYYY")

                    .Col = ColRefNo
                    .Text = Trim(CStr(mClaimNo))

                    .Col = ColRefDate
                    .Text = Trim(mClaimDate)

                    If CDbl(mBookCode) = ConPurchaseBookCode Then
                        SqlStr = "UPDATE FIN_PURCHASE_HDR SET" & vbCrLf _
                            & " GST_CLAIM_NEW_NO=" & Val(CStr(mClaimNo)) & ", " & vbCrLf _
                            & " GST_CLAIM_NEW_DATE=TO_DATE('" & VB6.Format(mClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " GST_CLAIM='" & mClaimValue & "', " & vbCrLf & " UPDATE_FROM='H', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"
                    ElseIf CDbl(mBookCode) = ConPurchaseSuppBookCode Then
                        SqlStr = "UPDATE FIN_SUPP_PURCHASE_HDR SET" & vbCrLf _
                            & " GST_CLAIM_NO=" & Val(CStr(mClaimNo)) & ", " & vbCrLf & " GST_CLAIM_DATE=TO_DATE('" & VB6.Format(mClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " GST_CLAIM='" & mClaimValue & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"
                    ElseIf CDbl(mBookCode) = ConLCBookCode Then
                        SqlStr = "UPDATE FIN_LCOPEN_HDR SET" & vbCrLf & " GST_CLAIM_NO=" & Val(CStr(mClaimNo)) & ", " & vbCrLf & " GST_CLAIM_DATE=TO_DATE('" & VB6.Format(mClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " GST_CLAIM='" & mClaimValue & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"
                    ElseIf CDbl(mBookCode) = ConLDBookCode Then
                        SqlStr = "UPDATE FIN_LCDISC_HDR SET" & vbCrLf & " GST_CLAIM_NO=" & Val(CStr(mClaimNo)) & ", " & vbCrLf & " GST_CLAIM_DATE=TO_DATE('" & VB6.Format(mClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " GST_CLAIM='" & mClaimValue & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"
                    End If
                    PubDBCn.Execute(SqlStr)

                    If mMRRNo > 0 And mClaimValue = "Y" Then
                        SqlStr = " UPDATE INV_GATE_HDR SET " & vbCrLf & " GST_STATUS='Y'," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(CStr(mMRRNo)) & " " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

                        PubDBCn.Execute(SqlStr)
                    End If

                    If UpdateGSTAppSeqMaster(PubDBCn, xMkey, mBookCode, mBookType, mBookSubType, mClaimNo, VB6.Format(mClaimDate, "DD-MMM-YYYY"), mClaimValue, "I") = False Then GoTo UpdateErr


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
        Dim SqlStr As String = ""
        Dim mMAxNo As Double

        SqlStr = ""

        SqlStr = "SELECT Max(SERVNO)  FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND SERVDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SERVDATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & "AND ISSERVTAX_POST='Y'" & vbCrLf & " AND ISPLA='N'"

        SqlStr = SqlStr & vbCrLf & "AND SERVICE_REFUND='" & pIsServiceRefund & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
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
    Private Sub frmGSTClaimEntryApp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
                    .Col = ColApproved
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With

        End If
    End Sub
    Private Sub optClaimAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optClaimAll.CheckedChanged
        If FormActive = False Then Exit Sub
        If eventSender.Checked Then
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColStatus
                    .Value = System.Windows.Forms.CheckState.Checked
                Next
            End With

        End If
    End Sub
    Private Sub optClaimNone_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optClaimNone.CheckedChanged
        If FormActive = False Then Exit Sub
        If eventSender.Checked Then
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColStatus
                    .Value = System.Windows.Forms.CheckState.Unchecked
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
