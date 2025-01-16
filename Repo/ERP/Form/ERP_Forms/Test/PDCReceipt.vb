Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPDCReceipt
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 22

    Private Const ColMKEY As Short = 1
    Private Const ColCustomerCode As Short = 2
    Private Const ColCustomerName As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5
    Private Const ColCustomerPONo As Short = 6
    Private Const ColCustomerPODate As Short = 7
    Private Const ColPaymentTerms As Short = 8
    Private Const ColNetValue As Short = 9
    Private Const ColPDCAmount As Short = 10
    Private Const ColPDCNo As Short = 11
    Private Const ColPDCDate As Short = 12
    Private Const ColCompanyCode As Short = 13
    Private Const ColPostStatus As Short = 14

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        TxtAccount.Enabled = IIf(ChkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKey As Double

        Dim mSupplier As String
        Dim mUpdateCount As Integer
        Dim mCompanyCode As Long
        Dim mPDCDate As String
        Dim mPDCNo As String
        Dim mPDCAmount As Double
        Dim mCanPostPO As Boolean

        'mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        'If mAuthorisation = "N" Then
        '    MsgBox("You have no Right to Post PO. ", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                mCanPostPO = False
                .Row = cntRow

                .Col = ColMKEY
                mMKey = CDbl(Trim(.Text))

                .Col = ColPDCAmount
                mPDCAmount = Val(.Text)

                .Col = ColPDCNo
                mPDCNo = Trim(.Text)

                .Col = ColPDCDate
                mPDCDate = Trim(.Text)

                .Col = ColCompanyCode
                mCompanyCode = Val(.Text)

                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    SqlStr = "UPDATE FIN_INVOICE_HDR SET PDC_RECEIPT='Y', " & vbCrLf _
                        & " PDC_NO='" & mPDCNo & "', " & vbCrLf _
                        & " PDC_DATE=TO_DATE('" & VB6.Format(mPDCDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " PDC_AMOUNT=" & Val(mPDCAmount) & "," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE MKEY='" & mMKey & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & ""

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1

                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " PDC Update.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = False
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Select Account")
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                FieldsVerification = False
                Exit Function
            End If
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows

                .Row = cntRow

                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    .Col = ColPDCAmount
                    If Val(.Text) <= 0 Then
                        MsgInformation("Please Enter PDC Amount")
                        FieldsVerification = False
                        Exit Function
                    End If

                    .Col = ColPDCNo
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Enter PDC No")
                        FieldsVerification = False
                        Exit Function
                    End If

                    .Col = ColPDCDate
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Enter PDC Date")
                        FieldsVerification = False
                        Exit Function
                    End If

                    If Not IsDate(.Text) Then
                        MsgInformation("Please Enter Valid PDC Date")
                        FieldsVerification = False
                        Exit Function
                    End If


                End If
            Next
        End With

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmPDCReceipt_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmPDCReceipt_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)


        TxtAccount.Enabled = False
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSuppCustCode As String



        SqlStr = " SELECT IH.MKEY, IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
                & " IH.BILLNO, IH.INVOICE_DATE, IH.CUST_PO_NO, IH.CUST_PO_NO, " & vbCrLf _
                & " PAYMENT_DESC AS PAYMENT_TERMS," & vbCrLf _
                & " IH.NETVALUE, " & vbCrLf _
                & " IH.PDC_AMOUNT, " & vbCrLf _
                & " IH.PDC_NO," & vbCrLf _
                & " IH.PDC_DATE," & vbCrLf _
                & " IH.COMPANY_CODE, ''" & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code "


        SqlStr = SqlStr & vbCrLf & " AND PDC_RECEIPT = 'N' AND ACM.INTER_UNIT='N'"

        SqlStr = SqlStr & vbCrLf _
                & " AND IH.OUR_AUTO_KEY_SO IN (" & vbCrLf _
                & " SELECT AUTO_KEY_SO FROM DSP_SALEORDER_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE= IH.COMPANY_CODE" & vbCrLf _
                & " AND SUPP_CUST_CODE= IH.SUPP_CUST_CODE " & vbCrLf _
                & " AND PAYMENT_TYPE='PDC') "

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
                & " ORDER BY  IH.COMPANY_CODE, ACM.SUPP_CUST_NAME, BILLNO, INVOICE_DATE"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColPostStatus
            .set_RowHeight(0, RowHeight)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 11)
            .ColHidden = True

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerCode, 8)


            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 25)


            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillNo, 12)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillDate, 10)
            .ColsFrozen = ColBillDate

            .Col = ColCustomerPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerPONo, 10)

            .Col = ColCustomerPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerPODate, 10)

            .Col = ColPaymentTerms
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPaymentTerms, 10)

            .Col = ColNetValue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColNetValue, 8)


            .Col = ColPDCNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPDCNo, 9)

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColCompanyCode, 8)
            .ColHidden = True

            .Col = ColPDCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            '.TypeEditLen = RsSODetail.Fields("FREIGHT_COST").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColPDCDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 9)




            .Col = ColPostStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColPostStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColNetValue)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCompanyCode, ColCompanyCode)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColCustomerPONo
            .Text = "Customer PO No"

            .Col = ColCustomerPODate
            .Text = "Customer PO Date"

            'Private Const  As Short = 12
            'Private Const  As Short = 13
            'Private Const ColPostStatus As Short = 14


            .Col = ColPaymentTerms
            .Text = "Payment Terms"

            .Col = ColNetValue
            .Text = "Net Value"

            .Col = ColPDCAmount
            .Text = "PDC Amount"

            .Col = ColPDCNo
            .Text = "PDC No"

            .Col = ColPDCDate
            .Text = "PDC Date"

            .Col = ColCompanyCode
            .Text = "Company Code"

            .Col = ColPostStatus
            .Text = "PDC Receipt Status"
        End With
    End Sub
    Private Sub frmPDCReceipt_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColPostStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE IN ('S','C'))")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
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
        Dim SqlStr As String = ""
        On Error GoTo ERR1
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmPDCReceipt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColBillNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPostStatus)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColBillNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPostStatus)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub frmPDCReceipt_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 240, mReFormWidth - 240, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)

        CurrFormWidth = mReFormWidth


        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
