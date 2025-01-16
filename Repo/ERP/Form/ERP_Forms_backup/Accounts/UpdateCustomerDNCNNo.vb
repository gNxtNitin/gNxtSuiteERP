Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmUpdateCustomerDNCN
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColDNCNNo As Short = 1
    Private Const ColDNCNDate As Short = 2
    Private Const ColPartyName As Short = 3
    Private Const ColInvoiceAmount As Short = 4
    Private Const ColGSTAmount As Short = 5
    Private Const ColPartyRefNo As Short = 6
    Private Const ColPartyRefDate As Short = 7
    Private Const ColPartyRecdDate As Short = 8
    Private Const ColUpdated As Short = 9
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim cntCol As Integer
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColUpdated
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
            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 22)
            .ColHidden = False
            For cntCol = ColInvoiceAmount To ColGSTAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 11)
            Next
            .Col = ColPartyRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyRefNo, 12)
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
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDNCNNo, ColGSTAmount)
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
            .Col = ColPartyName
            .Text = "Party Name"
            .Col = ColInvoiceAmount
            .Text = "Invoice Amount"
            .Col = ColGSTAmount
            .Text = "GST Amount"
            .Col = ColPartyRefNo
            .Text = "Party DN/CN No"
            .Col = ColPartyRefDate
            .Text = "Party DN/CN Date"
            .Col = ColPartyRecdDate
            .Text = "DN/CN Recd Date"
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
        Dim cntRow As Integer
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
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPartyRefNo
                If Trim(.Text) <> "" Then
                    MainClass.ProtectCell(SprdMain, cntRow, cntRow, ColPartyRefNo, ColPartyRecdDate)
                End If
            Next
        End With
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim mPartyRefNo As String
        Dim mPartyRefDate As String
        Dim mDNCnDate As String
        Dim mPartyRecdDate As String
        Dim I As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColUpdated
                If .Text = "Y" Then
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
                        '                    If CDate(mDNCNDate) > CDate(mPartyRefDate) Then
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
    Private Sub frmUpdateCustomerDNCN_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Update - Customer Debit / Credit Note No"
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdSearch.Enabled = False
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmUpdateCustomerDNCN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmUpdateCustomerDNCN_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        '    OptProdCustWise(0).Value = True
        '   OptProdCustWise_Click (0)
        Call frmUpdateCustomerDNCN_Activated(eventSender, eventArgs)
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
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim mPartyCode As String
        SqlStr = "SELECT IH.VNO, TO_CHAR(IH.VDATE,'DD/MM/YYYY') AS VDATE, CMST.SUPP_CUST_NAME, IH.NETVALUE, (IH.TOTCGST_AMOUNT+IH.TOTSGST_AMOUNT+IH.TOTIGST_AMOUNT) AS GSTAMOUNT, " & vbCrLf & " IH.PARTY_DNCN_NO, TO_CHAR(IH.PARTY_DNCN_DATE,'DD/MM/YYYY') AS PARTY_DNCN_DATE, TO_CHAR(IH.PARTY_DNCN_RECDDATE,'DD/MM/YYYY') AS PARTY_DNCN_RECDDATE " & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"
        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND IH.ISFINALPOST='Y'"
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
        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.PARTY_DNCN_NO IS NULL OR IH.PARTY_DNCN_NO = '')"
        ElseIf optShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND IH.PARTY_DNCN_NO IS NOT NULL"
        End If
        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.VDATE,IH.VNO"
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
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
                .Col = ColPartyRefDate
                mPartyRefDate = Trim(.Text)
                .Col = ColPartyRecdDate
                mPartyRecdDate = Trim(.Text)
                If mPartyRefNo <> "" And mPartyRefDate <> "" Then
                    SqlStr = " UPDATE FIN_SUPP_SALE_HDR " & vbCrLf & " SET PARTY_DNCN_NO='" & MainClass.AllowSingleQuote(mPartyRefNo) & "', " & vbCrLf & " PARTY_DNCN_DATE=TO_DATE('" & VB6.Format(mPartyRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " PARTY_DNCN_RECDDATE=TO_DATE('" & VB6.Format(mPartyRecdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR= " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND VNO= '" & mDNCnNO & "' "
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
    Private Sub frmUpdateCustomerDNCN_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        mTitle = "frmUpdatePartyDNCN"
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
        Dim mVDate As String
        Dim mPartyDate As String
        Dim mPartyRecdDate As String
        If eventArgs.NewRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColDNCNNo
        If Trim(SprdMain.Text) = "" Then Exit Sub
        SprdMain.Col = ColDNCNDate
        mVDate = Trim(SprdMain.Text)
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
                '            If CDate(mPartyDate) < CDate(mVDate) Then
                '                MsgInformation "Party Date Cann't be Less Than Our Debit Note Date."
                '                Cancel = True
                '                Exit Sub
                '            End If
            Case ColPartyRecdDate
                SprdMain.Col = ColPartyRefDate
                SprdMain.Row = eventArgs.row
                mPartyDate = Trim(SprdMain.Text)
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
        Resume
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
            cmdSearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdSearch.Enabled = True
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
