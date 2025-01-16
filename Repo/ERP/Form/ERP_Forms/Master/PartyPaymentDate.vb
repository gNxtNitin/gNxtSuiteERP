Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPartyPaymentDate
    Inherits System.Windows.Forms.Form
    ''Dim PvtDBCn As ADODB.Connection						

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 14

    Private Const ColPartyCode As Short = 1
    Private Const ColPartyName As Short = 2
    Private Const ColPayDay1 As Short = 3
    Private Const ColPayDay2 As Short = 4
    Private Const ColPayDay3 As Short = 5
    Private Const ColPayDay4 As Short = 6
    Private Const ColCheFreq As Short = 7
    Private Const ColPayTerms As Short = 8
    Private Const ColPayTermsDesc As Short = 9
    Private Const ColMinDay As Short = 10
    Private Const ColMaxDay As Short = 11
    Private Const ColADHOCDays As Short = 12
    Private Const ColUpdated As Short = 13
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColUpdated

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColPartyCode, 8)
            .ColHidden = False

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 35)
            .ColHidden = False

            .Col = ColPayDay1
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(ColPayDay1, 6)
            .ColHidden = True

            .Col = ColPayDay2
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(ColPayDay2, 6)
            .ColHidden = True

            .Col = ColPayDay3
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(ColPayDay3, 6)
            .ColHidden = True

            .Col = ColPayDay4
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(ColPayDay4, 6)
            .ColHidden = True

            .Col = ColCheFreq
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(ColCheFreq, 8)

            .Col = ColPayTerms
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPayTerms, 8)

            .Col = ColPayTermsDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPayTermsDesc, 30)

            .Col = ColMinDay
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColMinDay, 8)

            .Col = ColMaxDay
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColMaxDay, 8)

            .Col = ColADHOCDays
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(ColADHOCDays, 8)

            .Col = ColUpdated
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColUpdated, 6)
            .ColHidden = False

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartyCode, ColPartyName)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayTermsDesc, ColPayTermsDesc)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMinDay, ColMaxDay)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColUpdated, ColUpdated)
            '        SprdMain.OperationMode = OperationModeNormal						
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
            .Col = ColPartyCode
            .Text = "Party Code"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColPayDay1
            .Text = "Pay Day 1"

            .Col = ColPayDay2
            .Text = "Pay Day 2"

            .Col = ColPayDay3
            .Text = "Pay Day 3"

            .Col = ColPayDay4
            .Text = "Pay Day 4"

            .Col = ColCheFreq
            .Text = "Cheque Frequency"

            .Col = ColPayTerms
            .Text = "Payment Term Code"

            .Col = ColMinDay
            .Text = "Min Day"

            .Col = ColMaxDay
            .Text = "Max Day"

            .Col = ColADHOCDays
            .Text = "ADHOC Payment Day"

            .Col = ColPayTermsDesc
            .Text = "Payment Term Desc"

            .set_RowHeight(0, 26)
        End With
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        txtPartyName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
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

        Clear1()
        Show1()
        Call FormatSprdMain(-1)
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = False Then GoTo ErrPart
        CmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click

        If MainClass.SearchGridMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPartyName.Text = AcName
            'DoEvents
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))
        End If

        If SprdMain.Enabled = True Then SprdMain.Focus()
    End Sub

    Private Sub frmPartyPaymentDate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPartyPaymentDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPartyPaymentDate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        ''''Set PvtDBCn = New ADODB.Connection						
        ''''PvtDBCn.Open StrConn						

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        FillCategory()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtPartyName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        MainClass.SetControlsColor(Me)

        Call FormatSprdMain(-1)
        ADDMode = False
        MODIFYMode = False

        '    OptProdCustWise(0).Value = True						
        '   OptProdCustWise_Click (0)						
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
        Dim mAcctCode As String
        Dim I As Integer
        Dim xPayTerms As String

        SqlStr = "SELECT IH.SUPP_CUST_CODE, IH.SUPP_CUST_NAME, " & vbCrLf & " IH.PAIDDAY,IH.PAIDDAY2,IH.PAIDDAY3,IH.PAIDDAY4,IH.ACTIVITY,IH.PAYMENT_CODE, IH.PAYMENT_DESC,FROM_DAYS,TO_DAYS,ADHOC_PAY_TERMS,''" & vbCrLf & " FROM FIN_SUPP_CUST_MST IH, FIN_PAYTERM_MST PH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND IH.COMPANY_CODE=PH.COMPANY_CODE (+) AND IH.PAYMENT_CODE=PH.PAY_TERM_CODE(+)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPartyName.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAcctCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAcctCode) & "'"
                End If
            End If
        End If

        If Trim(txtPaymentTerms.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.PAYMENT_CODE='" & MainClass.AllowSingleQuote(txtPaymentTerms.Text) & "'"
        End If

        If cboCategory.SelectedIndex <> 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_TYPE='" & MainClass.AllowSingleQuote(VB.Left(cboCategory.Text, 1)) & "'"
        End If

        If Val(txtPaidDays.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.ACTIVITY='" & Val(txtPaidDays.Text) & "'"
            '        SqlStr = SqlStr & vbCrLf _						
            ''                & "AND (PAIDDAY=" & Val(txtPaidDays.Text) & "" & vbCrLf _						
            ''                & " OR PAIDDAY2=" & Val(txtPaidDays.Text) & "" & vbCrLf _						
            ''                & " OR PAIDDAY3=" & Val(txtPaidDays.Text) & "" & vbCrLf _						
            ''                & " OR PAIDDAY4=" & Val(txtPaidDays.Text) & ")"						

        End If

        If optOrderBy(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "ORDER BY IH.SUPP_CUST_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "ORDER BY IH.SUPP_CUST_CODE"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '    With SprdMain						
        '        For I = 1 To .MaxRows						
        '            .Row = I						
        '            .Col = ColPayTerms						
        '            xPayTerms = Trim(.Text)						
        '            If MainClass.ValidateWithMasterTable(xPayTerms, "PAY_TERM_CODE", "FROM_DAYS", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
        '                .Row = I						
        '                .Col = ColMinDay						
        '                .Text = MasterNo						
        '            End If						
        '            If MainClass.ValidateWithMasterTable(xPayTerms, "PAY_TERM_CODE", "TO_DAYS", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
        '                .Row = I						
        '                .Col = ColMaxDay						
        '                .Text = MasterNo						
        '            End If						
        '        Next						
        '    End With						



        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume						
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String
        Dim I As Integer
        Dim mPartyCode As String
        Dim mPayDay As String
        Dim mPayDay2 As String
        Dim mPayDay3 As String
        Dim mPayDay4 As String
        Dim mChqFreq As Integer
        Dim mPaymentDesc As String
        Dim mPaymentCode As String
        Dim mADHOCDays As Integer

        Dim RsTemp As ADODB.Recordset
        Dim xCompanyCode As Long

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                With SprdMain
                    For I = 1 To .MaxRows
                        .Row = I

                        .Col = ColUpdated
                        If .Text <> "Y" Then GoTo LoopNext

                        .Col = ColPartyCode
                        mPartyCode = Trim(.Text)

                        .Col = ColPayDay1
                        mPayDay = CStr(Val(.Text))

                        .Col = ColPayDay2
                        mPayDay2 = CStr(Val(.Text))

                        .Col = ColPayDay3
                        mPayDay3 = CStr(Val(.Text))

                        .Col = ColPayDay4
                        mPayDay4 = CStr(Val(.Text))

                        .Col = ColCheFreq
                        mChqFreq = Val(.Text)

                        .Col = ColPayTerms
                        mPaymentCode = MainClass.AllowSingleQuote(.Text)

                        .Col = ColPayTermsDesc
                        mPaymentDesc = MainClass.AllowSingleQuote(.Text)

                        .Col = ColADHOCDays
                        mADHOCDays = Val(.Text)


                        SqlStr = " UPDATE FIN_SUPP_CUST_MST " & vbCrLf _
                                    & " SET PAIDDAY='" & mPayDay & "', " & vbCrLf _
                                    & " PAIDDAY2='" & mPayDay2 & "', " & vbCrLf _
                                    & " PAIDDAY3='" & mPayDay3 & "', " & vbCrLf _
                                    & " PAIDDAY4='" & mPayDay4 & "', " & vbCrLf _
                                    & " ADHOC_PAY_TERMS='" & mADHOCDays & "', " & vbCrLf _
                                    & " ACTIVITY ='" & mChqFreq & "', " & vbCrLf _
                                    & " PAYMENT_CODE='" & mPaymentCode & "', PAYMENT_DESC='" & mPaymentDesc & "', " & vbCrLf _
                                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                                    & " WHERE Company_Code= " & xCompanyCode & "" & vbCrLf _
                                    & " AND SUPP_CUST_CODE= '" & mPartyCode & "' "


                        PubDBCn.Execute(SqlStr)
LoopNext:
                    Next
                End With
                RsTemp.MoveNext()
            Loop
        End If


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
    Private Sub frmPartyPaymentDate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        ''PvtDBCn.Close						
        ''Set PvtDBCn = Nothing						
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        SprdMain.Col = ColUpdated
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Text = "Y"
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.col
            Case ColPayTerms
                If eventArgs.row = 0 Then
                    MainClass.SearchGridMaster("", "FIN_PAYTERM_MST", "PAY_TERM_CODE", "PAY_TERM_DESC",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

                    If AcName <> "" Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = eventArgs.col
                        SprdMain.Text = AcName

                        SprdMain.Col = ColPayTermsDesc
                        SprdMain.Text = AcName1
                    End If
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xPayTerms As String
        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColPayTerms
        xPayTerms = Trim(SprdMain.Text)
        If xPayTerms = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColPayTerms
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColPayTerms
                xPayTerms = Trim(SprdMain.Text)
                If xPayTerms = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xPayTerms, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColPayTermsDesc
                    SprdMain.Text = MasterNo
                    If MainClass.ValidateWithMasterTable(xPayTerms, "PAY_TERM_CODE", "FROM_DAYS", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColMinDay
                        SprdMain.Text = MasterNo
                    End If
                    If MainClass.ValidateWithMasterTable(xPayTerms, "PAY_TERM_CODE", "TO_DAYS", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColMaxDay
                        SprdMain.Text = MasterNo
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPayTerms, "Invaild Payment Terms.")
                    eventArgs.cancel = True
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtPaidDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDays.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Party Name Does Not Exist In Master")
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillCategory()
        On Error GoTo ErrPart
        cboCategory.Items.Clear()
        cboCategory.Items.Add("All")
        cboCategory.Items.Add("Customer")
        cboCategory.Items.Add("Supplier")
        cboCategory.Items.Add("Employee")
        cboCategory.Items.Add("1- Cash")
        cboCategory.Items.Add("2- Bank")
        cboCategory.Items.Add("Other")
        cboCategory.Items.Add("Fixed Assets")
        cboCategory.SelectedIndex = 0
        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
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

        Call InsertPrintDummy()


        '''''Select Record for print...						

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Party Payment List"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & IIf(Trim(txtPartyName.Text) = "", "", " - " & txtPartyName.Text)
        End If
        mSubTitle = cboCategory.Text

        mRPTName = "PartyPaymentList.Rpt"

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
    Private Sub InsertPrintDummy()


        On Error GoTo ERR1
        Dim mPartyCode As String
        Dim mPartyName As String
        Dim mPayDay1 As String
        Dim mPayDay2 As String
        Dim mPayDay3 As String
        Dim mPayDay4 As String

        Dim SqlStr As String
        Dim cntRow As Integer


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColPartyCode
                mPartyCode = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColPayDay1
                mPayDay1 = Trim(.Text)

                .Col = ColPayDay2
                mPayDay2 = Trim(.Text)

                .Col = ColPayDay3
                mPayDay3 = Trim(.Text)

                .Col = ColPayDay4
                mPayDay4 = Trim(.Text)

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf & " Field1,Field2,Field3,Field4,Field5,Field6 " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mPartyCode & "', " & vbCrLf & " '" & mPartyName & "', " & vbCrLf & " '" & mPayDay1 & "', " & vbCrLf & " '" & mPayDay2 & "', " & vbCrLf & " '" & mPayDay3 & "', " & vbCrLf & " '" & mPayDay4 & "' " & vbCrLf & " ) "

                PubDBCn.Execute(SqlStr)
            Next

        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub txtPaymentTerms_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentTerms.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPaymentTerms_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentTerms.DoubleClick
        Call cmdSearchPT_Click(cmdSearchPT, New System.EventArgs())
    End Sub
    Private Sub txtPaymentTerms_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaymentTerms.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPaymentTerms.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPaymentTerms_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPaymentTerms.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPT_Click(cmdSearchPT, New System.EventArgs())
    End Sub
    Private Sub txtPaymentTerms_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaymentTerms.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtPaymentTerms.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtPaymentTerms.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Party Name Does Not Exist In Master")
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchPT_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPT.Click

        If MainClass.SearchGridMaster(txtPaymentTerms.Text, "FIN_PAYTERM_MST", "PAY_TERM_CODE", "PAY_TERM_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPaymentTerms.Text = AcName
            'DoEvents
            txtPaymentTerms_Validating(txtPaymentTerms, New System.ComponentModel.CancelEventArgs(False))
        End If

        If SprdMain.Enabled = True Then SprdMain.Focus()
    End Sub
End Class
