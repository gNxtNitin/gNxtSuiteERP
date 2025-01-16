Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmUpdatePartyDNCN
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColDNCNNo As Short = 1
    Private Const ColDNCNDate As Short = 2
    Private Const ColReason As Short = 3
    Private Const ColBookType As Short = 4
    Private Const ColPartyCode As Short = 5
    Private Const ColPartyName As Short = 6
    Private Const ColPartyGSTNo As Short = 7
    Private Const ColPartyBillNo As Short = 8
    Private Const ColPartyDate As Short = 9
    Private Const ColTaxableAmount As Short = 10
    Private Const ColInvoiceAmount As Short = 11
    Private Const ColCGSTAmount As Short = 12
    Private Const ColSGSTAmount As Short = 13
    Private Const ColIGSTAmount As Short = 14
    Private Const ColGSTAmount As Short = 15
    Private Const ColDNCN_AGTNO As Short = 16
    Private Const ColDNCN_AGTDate As Short = 17
    Private Const ColRef_DNCN_No As Short = 18
    Private Const ColRef_DNCN_Date As Short = 19
    Private Const ColPartyRefNo As Short = 20
    Private Const ColPartyRefDate As Short = 21
    Private Const ColPartyRecdDate As Short = 22
    Private Const ColUpdated As Short = 23
    Private Const ColCompanyCode As Short = 24
    Private Const ColCompanyName As Short = 25
    Private Const ColAccountHead As Short = 26
    Private Const ColAddUser As Short = 27
    Private Const ColAddDate As Short = 28
    Private Const ColModUser As Short = 29
    Private Const ColModDate As Short = 30
    Private Const ColMKEY As Short = 31
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mClickProcess As Boolean

    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim cntCol As Integer
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColMKEY
            .Row = 0
            SetColHeadings()
            .Row = Arow
            .Col = ColDNCNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColDNCNNo, 9)
            .ColHidden = False
            .Col = ColDNCNDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditMultiLine = True
            .set_ColWidth(ColDNCNDate, 9)
            .ColHidden = False
            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColReason, 15)
            .ColHidden = False
            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 10)
            .ColHidden = True
            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColPartyCode, 5)
            .ColHidden = True
            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 22)
            .ColHidden = False
            .Col = ColPartyGSTNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyGSTNo, 12)
            .ColHidden = False
            .Col = ColPartyBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyBillNo, 12)
            .ColHidden = False
            .Col = ColPartyDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyDate, 12)
            .ColHidden = False
            For cntCol = ColTaxableAmount To ColGSTAmount
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
            .Col = ColDNCN_AGTNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColDNCN_AGTNO, 10)
            .Col = ColDNCN_AGTDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .Col = ColRef_DNCN_No
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRef_DNCN_No, 10)
            .Col = ColRef_DNCN_Date
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColMKEY, 10)
            .ColHidden = True
            .Col = ColPartyRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyRefNo, 10)
            .Col = ColPartyRefDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditMultiLine = True
            .set_ColWidth(ColPartyRefDate, 9)
            .Col = ColPartyRecdDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColPartyRecdDate, 9)
            .Col = ColUpdated
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColUpdated, 5)
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

            .Col = ColAccountHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAccountHead, 15)

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


            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDNCNNo, ColMKEY)
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartyRefNo, ColPartyRecdDate)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0
            .Col = ColDNCNNo
            .Text = "Debit / Credit No"
            .Col = ColDNCNDate
            .Text = "Debit / Credit Date"
            .Col = ColReason
            .Text = "Reason"
            .Col = ColBookType
            .Text = "Book Type"
            .Col = ColPartyCode
            .Text = "Party Code"
            .Col = ColPartyName
            .Text = "Party Name"
            .Col = ColPartyGSTNo
            .Text = "Party GST No"
            .Col = ColPartyBillNo
            .Text = "Party Bill No"
            .Col = ColPartyDate
            .Text = "Party Bill date"
            .Col = ColTaxableAmount
            .Text = "Taxable Amount"
            .Col = ColInvoiceAmount
            .Text = "Invoice Amount"
            .Col = ColCGSTAmount
            .Text = "CGST Amount"
            .Col = ColSGSTAmount
            .Text = "SGST Amount"
            .Col = ColIGSTAmount
            .Text = "IGST Amount"
            .Col = ColGSTAmount
            .Text = "GST Amount"
            .Col = ColDNCN_AGTNO
            .Text = "Debit / Credit Note Agt No"
            .Col = ColDNCN_AGTDate
            .Text = "Debit / Credit Note Agt Date"
            .Col = ColRef_DNCN_No
            .Text = "Ref Debit/Credit No"
            .Col = ColRef_DNCN_Date
            .Text = "Ref Debit/Credit Date"
            .Col = ColMKEY
            .Text = "MKEY"
            .Col = ColUpdated
            .Text = "Updated"
            .Col = ColPartyRefNo
            .Text = "Party DN/CN No"
            .Col = ColPartyRefDate
            .Text = "Party DN/CN Date"
            .Col = ColPartyRecdDate
            .Text = "DN/CN Recd Date"

            .Col = ColCompanyCode
            .Text = "Company Code"

            .Col = ColCompanyName
            .Text = "Company Name"

            .Col = ColAccountHead
            .Text = "Account Head"

            .Col = ColAddUser
            .Text = "Add User"

            .Col = ColAddDate
            .Text = "Add Date"

            .Col = ColModUser
            .Text = "Mod User"

            .Col = ColModDate
            .Text = "Mod Date"


            .set_RowHeight(0, 20)
        End With
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Call SaleReport("V")
        ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Call SaleReport("V")
        ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr
        Dim CntRow As Integer
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Sub
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Sub
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                Exit Sub
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                Exit Sub
            End If
        End If
        '    If Trim(TxtAccount.Text) = "" Then
        '        MsgBox "Party Name is empty.", vbInformation
        '        Exit Sub
        '    Else
        '        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
        '            MsgBox "Party Does Not Exist In Master.", vbInformation
        '            Exit Sub
        '        End If
        '    End If
        Clear1()
        Show1()
        Call FormatSprdMain(-1)
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColPartyRefNo
                If Trim(.Text) <> "" Then
                    MainClass.ProtectCell(SprdMain, CntRow, CntRow, ColPartyRefNo, ColPartyRecdDate)
                End If
            Next
        End With
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim mPartyRefNo As String
        Dim mPartyRefDate As String
        Dim mDNCnDate As String
        Dim mPartyRecdDate As String
        Dim xCompanyCode As Long
        Dim I As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColCompanyCode
                xCompanyCode = Val(.Text)

                .Col = ColUpdated
                If .Text = "Y" And RsCompany.Fields("COMPANY_CODE").Value = xCompanyCode Then
                    .Col = ColDNCNDate
                    mDNCnDate = Trim(.Text)
                    .Col = ColPartyRefNo
                    mPartyRefNo = Trim(.Text)
                    .Col = ColPartyRefDate
                    mPartyRefDate = Trim(.Text)
                    .Col = ColPartyRecdDate
                    mPartyRecdDate = Trim(.Text)
                    If mPartyRefNo <> "" Then
                        If mPartyRefDate = "" Then
                            MsgInformation("Please Enter The Party Ref date.")
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If
                        If IsDate(mPartyRefDate) = False Then
                            MsgInformation("Please Enter The Valid Party Ref date.")
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If
                        '                    If CDate(mDNCnDate) > CDate(mPartyRefDate) Then
                        '                        MsgInformation "Party Ref Date Cann't be Greater than Our Ref Date."
                        '                        Screen.MousePointer = 0
                        '                        Exit Sub
                        '                    End If
                        If IsDate(mPartyRecdDate) = False Then
                            MsgInformation("Please Enter The Valid receiving date.")
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If
                        If CDate(mPartyRefDate) > CDate(mPartyRecdDate) Then
                            MsgInformation("Party receving Date Cann't be Greater than Our Ref Date.")
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If
                        If ValidateBookLocking(PubDBCn, ConDebitNoteBookCode, mPartyRecdDate) = True Then
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End With
        If Update1 = False Then GoTo ErrPart
        CmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmUpdatePartyDNCN_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Update - Party Debit / Credit Note No"
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmUpdatePartyDNCN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmUpdatePartyDNCN_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Complete")
        cboShow.Items.Add("Pending")
        cboShow.Items.Add("Pending (Same GST)")
        cboShow.Items.Add("Closed")
        cboShow.SelectedIndex = 1
        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Refund")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non-GST")
        cboGSTStatus.Items.Add("Ineligible")
        cboGSTStatus.Items.Add("Composit")
        cboGSTStatus.Items.Add("Without GST")
        cboGSTStatus.Items.Add("All")
        cboGSTStatus.SelectedIndex = 0
        cboType.Items.Clear()
        cboType.Items.Add("ALL")
        cboType.Items.Add("Shortage")
        cboType.Items.Add("Rate Diff")
        cboType.Items.Add("Rejection")
        cboType.Items.Add("Others")
        cboType.SelectedIndex = 0

        Call FillInvoiceType()
        '    OptProdCustWise(0).Value = True
        '   OptProdCustWise_Click (0)
        Call frmUpdatePartyDNCN_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()
        On Error GoTo ClearErr
        CmdSave.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
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
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim mPartyCode As String
        Dim mCompanyGSTNo As String
        Dim CntRow As Integer
        Dim mDNVNo As String
        Dim mDNVDate As String
        Dim mKey As String
        Dim mPartyBillNo As String
        Dim mPartyDate As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookType As String
        'Dim mPartyCode As String

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        SqlStr = "SELECT IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE, "
        SqlStr = SqlStr & vbCrLf & " CASE WHEN DNCNTYPE='P' THEN 'PO RATE DIFF.'  " & vbCrLf & " WHEN DNCNTYPE='A' THEN 'AMEND PO RATE DIFF.'  " & vbCrLf & " WHEN DNCNTYPE='S' THEN 'SHORTAGE'  " & vbCrLf & " WHEN DNCNTYPE='R' THEN 'REJECTION'  " & vbCrLf & " WHEN DNCNTYPE='D' THEN 'DISCOUNT'  " & vbCrLf & " WHEN DNCNTYPE='V' THEN 'VOLUME DISCOUNT'  " & vbCrLf & " WHEN DNCNTYPE='O' THEN 'OTHERS' END AS REASON, "
        SqlStr = SqlStr & vbCrLf & " IH.BookType , CMST.SUPP_CUST_CODE, " & vbCrLf _
            & " CMST.SUPP_CUST_NAME, CMST.GST_RGN_NO, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " IH.ITEMVALUE, IH.NETVALUE, " & vbCrLf _
            & " IH.NETCGST_AMOUNT, IH.NETSGST_AMOUNT, IH.NETIGST_AMOUNT, " & vbCrLf _
            & " (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT) AS GSTAMOUNT, " & vbCrLf _
            & " IH.PURVNO , IH.PURVDATE, '', '', " & vbCrLf _
            & " REPLACE(PARTY_DNCN_NO,CHR(10),'') AS PARTY_DNCN_NO, TO_CHAR(IH.PARTY_DNCN_DATE,'DD/MM/YYYY') AS PARTY_DNCN_DATE, " & vbCrLf _
            & " TO_CHAR(IH.PARTY_DNCN_RECDDATE,'DD/MM/YYYY') AS PARTY_DNCN_RECDDATE,'',IH.COMPANY_CODE,GEN.COMPANY_SHORTNAME,AMST.SUPP_CUST_NAME,  IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE, IH.MKEY"


        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GEN, FIN_SUPP_CUST_MST AMST" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And DECODE(IH.BOOKTYPE,'E',IH.DEBITACCOUNTCODE,IH.CREDITACCOUNTCODE)=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " And IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf _
            & " And DECODE(IH.BOOKTYPE,'E',IH.CREDITACCOUNTCODE,IH.DEBITACCOUNTCODE)=AMST.SUPP_CUST_CODE"

        '    Sqlstr = Sqlstr & vbCrLf & " AND IH.CANCELLED='N' AND IH.APPROVED='Y' AND DNCNTYPE<> CASE WHEN IH.FYEAR>=2018 THEN 'R' ELSE '-1' END"  ''Rejection should Not fetch.

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND IH.APPROVED='Y' "

        'SqlStr = SqlStr & vbCrLf & " AND DNCNTYPE<> CASE WHEN IH.VDATE>='01-MAY-2018' THEN 'R' ELSE '-1' END" ''Rejection should Not fetch.

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
            SqlStr = SqlStr & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SqlStr = SqlStr & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
                End If
            End If
        End If
        If Trim(txtDnCnNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & MainClass.AllowSingleQuote(txtDnCnNo.Text) & "'"
        End If
        If cboGSTStatus.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & "AND ISGSTREFUND='G' AND (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT) <>0 "
        ElseIf cboGSTStatus.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND ISGSTREFUND='R'"
        ElseIf cboGSTStatus.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND ISGSTREFUND='E'"
        ElseIf cboGSTStatus.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & "AND ISGSTREFUND='N'"
        ElseIf cboGSTStatus.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & "AND ISGSTREFUND='I'"
        ElseIf cboGSTStatus.SelectedIndex = 5 Then
            SqlStr = SqlStr & vbCrLf & "AND ISGSTREFUND='C'"
        ElseIf cboGSTStatus.SelectedIndex = 6 Then
            SqlStr = SqlStr & vbCrLf & "AND (ISGSTREFUND='W' OR (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT)=0 )"
        End If
        If cboType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND DNCNTYPE='S'"
        ElseIf cboType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND DNCNTYPE IN ('P','A')"
        ElseIf cboType.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & "AND DNCNTYPE='R'"
        ElseIf cboType.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & "AND DNCNTYPE IN ('V','D','O')"
        End If
        If cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND NVL(CMST.GST_RGN_NO,'')<>'" & mCompanyGSTNo & "'"
            SqlStr = SqlStr & vbCrLf & "AND (IH.PARTY_DNCN_NO IS NULL OR IH.PARTY_DNCN_NO = '')"
            SqlStr = SqlStr & vbCrLf & "AND ISDNCN_ISSUE='N'"
        ElseIf cboShow.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & "AND NVL(CMST.GST_RGN_NO,'')='" & mCompanyGSTNo & "'"
            SqlStr = SqlStr & vbCrLf & "AND (IH.PARTY_DNCN_NO IS NULL OR IH.PARTY_DNCN_NO = '')"
            SqlStr = SqlStr & vbCrLf & "AND ISDNCN_ISSUE='N'"
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.PARTY_DNCN_NO IS NOT NULL"
            SqlStr = SqlStr & vbCrLf & "AND ISDNCN_ISSUE='Y'"
        ElseIf cboShow.SelectedIndex = 4 Then  ''Closed
            SqlStr = SqlStr & vbCrLf & "AND ISDNCN_ISSUE='Y'"
        End If
        If chkDebitNote.CheckState = System.Windows.Forms.CheckState.Checked And chkCreditNote.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKCODE IN (" & ConDebitNoteBookCode & "," & ConCreditNoteBookCode & ")"
        ElseIf chkDebitNote.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKCODE=" & ConDebitNoteBookCode & ""
        ElseIf chkCreditNote.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKCODE=" & ConCreditNoteBookCode & ""
        End If
        If optDate(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.PARTY_DNCN_RECDDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        If optDate(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "ORDER BY IH.VDATE,IH.VNO"
        Else
            SqlStr = SqlStr & vbCrLf & "ORDER BY IH.PARTY_DNCN_RECDDATE,IH.VDATE,IH.VNO"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        If chkShowRefDN.CheckState = System.Windows.Forms.CheckState.Checked Then
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColMKEY
                    mKey = Trim(.Text)
                    .Col = ColPartyCode
                    mPartyCode = Trim(.Text)
                    .Col = ColPartyBillNo
                    mPartyBillNo = Trim(.Text)
                    .Col = ColPartyDate
                    mPartyDate = Trim(.Text)
                    .Col = ColBookType
                    mBookType = Trim(.Text)
                    mDNVNo = ""
                    mDNVDate = ""
                    mSqlStr = " SELECT VNO, VDATE " & vbCrLf & " FROM FIN_DNCN_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DECODE(BOOKTYPE,'E',DEBITACCOUNTCODE,CREDITACCOUNTCODE)='" & MainClass.AllowSingleQuote(mPartyCode) & "'" & vbCrLf & " AND BILLNO='" & mPartyBillNo & "'" & vbCrLf & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mPartyDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND CANCELLED='N' AND APPROVED='Y'" & vbCrLf & " AND NETCGST_AMOUNT+NETSGST_AMOUNT+NETIGST_AMOUNT>0"
                    If mBookType = "E" Then
                        mSqlStr = mSqlStr & vbCrLf & " AND BOOKTYPE ='R'"
                    Else
                        mSqlStr = mSqlStr & vbCrLf & " AND BOOKTYPE ='E'"
                    End If
                    mSqlStr = mSqlStr & vbCrLf & " AND MKEY <> '" & mKey & "'"
                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        Do While RsTemp.EOF = False
                            mDNVNo = IIf(mDNVNo = "", "", mDNVNo) & IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                            mDNVDate = IIf(mDNVDate = "", "", mDNVDate) & VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                            RsTemp.MoveNext()
                            If RsTemp.EOF = False Then
                                mDNVNo = mDNVNo & ","
                                mDNVDate = mDNVDate & ","
                            End If
                        Loop
                    End If
                    .Col = ColRef_DNCN_No
                    .Text = mDNVNo
                    .Col = ColRef_DNCN_Date
                    .Text = mDNVDate
                Next
            End With
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateErr
        Dim SqlStr As String
        Dim I As Integer
        Dim mDNCnNO As String
        Dim mPartyRefNo As String
        Dim mPartyRefDate As String
        Dim mPartyRecdDate As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColUpdated

                If .Text <> "Y" Then GoTo LoopNext

                .Col = ColDNCNNo
                mDNCnNO = Trim(.Text)

                .Col = ColPartyRefNo
                mPartyRefNo = Trim(.Text)

                mPartyRefNo = Replace(mPartyRefNo, vbCrLf, "")

                .Col = ColPartyRefDate
                mPartyRefDate = Trim(.Text)

                .Col = ColPartyRecdDate
                mPartyRecdDate = Trim(.Text)

                If mPartyRefNo <> "" And mPartyRefDate <> "" Then
                    SqlStr = " UPDATE FIN_DNCN_HDR " & vbCrLf _
                        & " SET PARTY_DNCN_NO='" & MainClass.AllowSingleQuote(mPartyRefNo) & "', " & vbCrLf _
                        & " PARTY_DNCN_DATE=TO_DATE('" & VB6.Format(mPartyRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " PARTY_DNCN_RECDDATE=TO_DATE('" & VB6.Format(mPartyRecdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), ISDNCN_ISSUE='Y'," & vbCrLf _
                        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND FYEAR= " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                        & " AND VNO= '" & mDNCnNO & "' "
                    PubDBCn.Execute(SqlStr)
                End If
LoopNext:
            Next
        End With
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        ''Resume
    End Function
    Private Sub frmUpdatePartyDNCN_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 240, mReFormWidth - 240, mReFormWidth))
        Frame2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmUpdatePartyDNCN_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        CmdSave.Enabled = True
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        SprdMain.Col = ColUpdated
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Text = "Y"
    End Sub
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        PubDBCn.Errors.Clear()
        'If TxtName.Text = "" Then Exit Sub
        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, ColDNCNNo, ColGSTAmount, PubDBCn) = False Then GoTo ERR1
        'Select Record for print...
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        mTitle = "Update Party DN & CN Report"
        mRPTName = "UpdatePartyDNCN.Rpt"
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
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ERR1
        Dim mVNo As String
        Dim mVdate As String
        Dim mPartyDate As String
        Dim mPartyRecdDate As String
        If eventArgs.NewRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColDNCNNo
        If Trim(SprdMain.Text) = "" Then Exit Sub
        SprdMain.Col = ColDNCNDate
        mVdate = Trim(SprdMain.Text)
        Select Case eventArgs.col
            Case ColPartyRefDate
                SprdMain.Col = ColPartyRefDate
                SprdMain.Row = eventArgs.row
                mPartyDate = Trim(SprdMain.Text)
                If Trim(mPartyDate) = "" Then Exit Sub
                If Not IsDate(mPartyDate) Then
                    MsgInformation("Invalid Date.")
                    eventArgs.cancel = True
                    Exit Sub
                End If
                'If CDate(mPartyDate) < CDate(mVdate) Then
                '    MsgInformation("Party Date Cann't be Less Than Our Debit Note Date.")
                '    eventArgs.cancel = True
                '    Exit Sub
                'End If
            Case ColPartyRecdDate
                SprdMain.Col = ColPartyRefDate
                SprdMain.Row = eventArgs.row
                mPartyDate = Trim(SprdMain.Text)
                If Trim(mPartyDate) = "" Then Exit Sub

                SprdMain.Col = ColPartyRecdDate
                SprdMain.Row = eventArgs.row
                mPartyRecdDate = Trim(SprdMain.Text)
                If Trim(mPartyRecdDate) = "" Then Exit Sub
                If Not IsDate(mPartyRecdDate) Then
                    MsgInformation("Invalid Date.")
                    eventArgs.cancel = True
                    Exit Sub
                End If
                If CDate(mPartyDate) > CDate(mPartyRecdDate) Then
                    MsgInformation("Party Date Cann't be Less Than Our receiving Date.")
                    eventArgs.cancel = True
                    Exit Sub
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        If KeyAscii = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Account Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String
        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((TxtAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtAccount.Text = AcName
            txtAccount_Validating(txtAccount, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
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
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub txtDnCnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDnCnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDnCnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
