Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamEmpPerksLedger
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDATE As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColNarration As Short = 6
    Private Const ColDAmount As Short = 7
    Private Const ColCAmount As Short = 8
    Private Const ColBalance As Short = 9
    Private Const ColBalDC As Short = 10



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean


        If txtEmpCode.Text = "" Then PrintStatus = False Else PrintStatus = True


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call ReportForLedger(Crystal.DestinationConstants.crptToWindow, PubDBCn)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants, ByRef pDBCn As ADODB.Connection)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        pDBCn.Errors.Clear()

        SqlStr = "DELETE FROM Temp_Ledger NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = ""


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Employee Perks Ledger"

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mSubTitle = mSubTitle & "  (" & txtEmpCode.Text & ")"


        mReportFileName = "Ledger.Rpt"
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
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_Ledger " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY PARTYNAME, SUBROW "

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean

        If txtEmpCode.Text = "" Then PrintStatus = False Else PrintStatus = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter, PubDBCn)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts(txtEmpCode)
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        If LedgInfo = False Then GoTo ErrPart
        SprdLedg.Focus()
        Call PrintStatus(True)
        '    FraOthers.Visible = False
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub DisplayTotals(ByRef pOpeningDr As Double, ByRef pOpeningCr As Double)

        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mDebit As Double
        Dim mCredit As Double
        Dim mBalance As Double
        Dim mDC As String


        With SprdLedg
            .MaxRows = .MaxRows + 1
            .Row = 1
            .Action = SS_ACTION_INSERT_ROW
            .Col = ColNarration

            .Row = 1
            .Text = "OPENING : "
            .Font = VB6.FontChangeBold(.Font, True)
            'FormatSprdLedg -1

            .Col = ColDAmount
            .Text = VB6.Format(pOpeningDr, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColCAmount
            .Text = VB6.Format(pOpeningCr, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBalance
            .Text = "0.00"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBalDC
            .Text = "Dr"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = 1
            .Row2 = 1
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            Call MainClass.AddBlankfpSprdRow(SprdLedg, ColVDATE)
            'FormatSprdLedg -1

            '        .MaxRows = .MaxRows + 1
            '        .Row = .MaxRows
            '        .Action = SS_ACTION_INSERT_ROW
            .Col = ColNarration
            .Row = .MaxRows
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            'FormatSprdLedg -1

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False


            '        Call CalcRowTotal(SprdLedg, ColDAmount, 1, ColDAmount, .MaxRows - 1, .MaxRows, ColDAmount)
            '        Call CalcRowTotal(SprdLedg, ColCAmount, 1, ColCAmount, .MaxRows - 1, .MaxRows, ColCAmount)
            '

            .Row = .MaxRows

            .Col = ColDAmount
            mDebit = Val(.Text)

            .Col = ColCAmount
            mCredit = Val(.Text)

            mBalance = mDebit - mCredit
            .Col = ColBalance
            .Text = Str(System.Math.Abs(mBalance))
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBalDC
            .Text = IIf(mBalance >= 0, "DR", "CR")
            .Font = VB6.FontChangeBold(.Font, True)
            FormatSprdLedg(-1)


        End With

        Call FillRunBalCol()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub



    Public Sub frmParamEmpPerksLedger_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim I As Integer
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        txtEmpCode.Visible = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamEmpPerksLedger_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        ' Move the rows or columns with the scroll box
        SprdLedg.ScrollBarTrack = FPSpreadADO.ScrollBarTrackConstants.ScrollBarTrackBoth
        ' Show the scroll tips
        SprdLedg.ShowScrollTips = FPSpreadADO.ShowScrollTipsConstants.ShowScrollTipsBoth


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamEmpPerksLedger_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdLedg.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdLedg, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamEmpPerksLedger_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        FormActive = False
        MainClass.AssignDataInSprd8("", SprdLedg, "", "N")
        Me.hide()
    End Sub

    Private Sub OptSumDet_Click(ByRef Index As Short)
        Call PrintStatus(False)
    End Sub


    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
    End Sub

    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtEmpCode.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtEmpCode.DoubleClick
        Call SearchAccounts(txtEmpCode)
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((mTextBox.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                mTextBox.Text = AcName1
                lblEmpname.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccounts(txtEmpCode)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblEmpname.Text = ""
        If txtEmpCode.Text = "" Then GoTo EventExitSub

        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblEmpname.Text = MasterNo
            txtEmpCode.Text = UCase(Trim(txtEmpCode.Text))
        Else
            lblEmpname.Text = ""
            MsgInformation("No Such Employee in Employee Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdLedg(ByRef Arow As Integer)

        With SprdLedg
            .MaxCols = ColBalDC
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .RowsFrozen = 1
            .Row = -1


            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 15)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 15)
            .ColHidden = True

            .Col = ColVDATE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDATE, 9)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColHidden = True
            .set_ColWidth(ColVNo, 10)

            .Col = ColDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDAmount, 12)

            .Col = ColCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCAmount, 12)

            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalance, 12)
            .ColHidden = False

            .Col = ColBalDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBalDC, 5)
            .ColHidden = False
            '        If ChkWithRunBal.Value = vbUnchecked Then
            '            .Col = ColBalance
            '            .ColHidden = True
            '            .Col = ColBalDC
            '            .ColHidden = True
            '        End If

            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            'Show the grid lines over the color
            '        SprdLedg.BackColorStyle = BackColorStyleOverVertGridOnly

            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FillRunBalCol()

        On Error GoTo ERR1
        Dim ii As Integer
        Dim mBalance As Double
        Dim mDAmount As Double
        Dim mCAmount As Double
        Dim mTotDAmount As Double
        Dim mTotCAmount As Double

        Dim mAccountCode As String
        Dim mSuppCustCode As String
        Dim mDummayDebit As Double
        Dim mDummayCredit As Double
        Dim xDummyAmount As Double
        Dim xDC As String
        Dim mBookType As String
        Dim mMKey As String
        Dim xFromDate As String
        Dim xToDate As String
        Dim mDate As String

        '    mSuppCustCode = ""
        '    mAccountCode = ""
        '
        '    If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
        '        mSuppCustCode = MasterNo
        '    ElseIf MainClass.ValidateWithMasterTable(txtEmpCode.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mAccountCode = MasterNo
        '    End If


        With SprdLedg
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColDAmount
                If IsNumeric(.Text) Then
                    mDAmount = CDbl(.Text)
                Else
                    mDAmount = 0
                End If
                mTotDAmount = mTotDAmount + mDAmount

                .Col = ColCAmount
                If IsNumeric(.Text) Then
                    mCAmount = CDbl(.Text)
                Else
                    mCAmount = 0
                End If
                mTotCAmount = mTotCAmount + mCAmount

                mBalance = mBalance + mDAmount - mCAmount

                .Col = ColBalance
                .Text = MainClass.FormatRupees(System.Math.Abs(mBalance))

                .Col = ColBalDC
                .Text = IIf(mBalance > 0, "Dr", "Cr")
            Next

            mBalance = 0
            For ii = .MaxRows To .MaxRows
                .set_RowHeight(ii, RowHeight * 1.25)
                .Row = ii
                .Col = ColDAmount
                .Text = MainClass.FormatRupees(System.Math.Abs(mTotDAmount))
                .Font = VB6.FontChangeBold(.Font, True)

                If IsNumeric(.Text) Then
                    mDAmount = CDbl(.Text)
                Else
                    mDAmount = 0
                End If

                .Col = ColCAmount
                .Text = MainClass.FormatRupees(System.Math.Abs(mTotCAmount))
                .Font = VB6.FontChangeBold(.Font, True)

                If IsNumeric(.Text) Then
                    mCAmount = CDbl(.Text)
                Else
                    mCAmount = 0
                End If

                mBalance = mBalance + mDAmount - mCAmount

                .Col = ColBalance
                .Text = MainClass.FormatRupees(System.Math.Abs(mBalance))

                .Col = ColBalDC
                .Text = IIf(mBalance > 0, "Dr", "Cr")
            Next

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Function LedgInfo() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mAccountCode2 As String
        Dim mSuppCustCode As String
        Dim mDummyAccountCode As String
        Dim mDummayOPDebit As Double
        Dim mDummayOPCredit As Double
        Dim xDummyAmount As Double


        LedgInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        '    SqlStr2 = MakeSQLCond(False)

        '    SqlStr = SqlStr1 & vbCrLf & SqlStr2


        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        '********************************
        'Get Opening Balance.........

        If GetOPBalance(mOpening) = False Then GoTo LedgError

        If mOpening >= 0 Then
            mOpDr = mOpening
            mOpCr = 0
        Else
            mOpDr = 0
            mOpCr = System.Math.Abs(mOpening)
        End If

        DisplayTotals(mOpDr, mOpCr)

        LedgInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        LedgInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT LOCKED,  BOOKTYPE, BOOKSUBTYPE, " & vbCrLf & " V_DATE, V_NO, NARRATION, " & vbCrLf & " DEBIT, CREDIT, BALANCE, DC " & vbCrLf & " FROM ( "

        If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " SELECT '' AS LOCKED, " & vbCrLf & " TRN.BOOKTYPE As BOOKTYPE, 'E' AS BOOKSUBTYPE, " & vbCrLf & " SAL_DATE AS V_DATE, " & vbCrLf & " ADD_DEDUCTCODE AS V_NO, " & vbCrLf & " ADD_DEDUCT.NAME AS NARRATION," & vbCrLf & " AMOUNT AS DEBIT, " & vbCrLf & " 0  CREDIT, " & vbCrLf & " 0 AS BALANCE, " & vbCrLf & " 0 AS DC "
        Else
            SqlStr = SqlStr & vbCrLf & " SELECT '' AS LOCKED, " & vbCrLf & " TRN.BOOKTYPE As BOOKTYPE, 'E' AS BOOKSUBTYPE, " & vbCrLf & " SAL_DATE AS V_DATE, " & vbCrLf & " '' AS V_NO, " & vbCrLf & " CASE WHEN BOOKTYPE='O' THEN 'OPENING' " & vbCrLf & " WHEN BOOKTYPE='S' THEN 'SALARY'" & vbCrLf & " WHEN BOOKTYPE='A' THEN 'ARREAR'" & vbCrLf & " WHEN BOOKTYPE='F' THEN 'F & F'" & vbCrLf & " WHEN BOOKTYPE='Z' THEN 'SALARY'" & vbCrLf & " WHEN BOOKTYPE='V' THEN 'VARIABLE'" & vbCrLf & " ELSE 'OTHERS' END " & vbCrLf & " || ' - FOR THE MONTH ' || TO_CHAR(SAL_DATE,'MON-YYYY') AS NARRATION," & vbCrLf & " SUM(AMOUNT) AS DEBIT, " & vbCrLf & " 0  CREDIT, " & vbCrLf & " 0 AS BALANCE, " & vbCrLf & " 0 AS DC "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM PAY_PERKS_TRN TRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE AND AMOUNT<>0"

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE IN ('O','S','A','F','Z','V')"

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND ADD_DEDUCT.PAYMENT_TYPE='M'"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.BOOKTYPE, SAL_DATE, " & vbCrLf & " CASE WHEN BOOKTYPE='O' THEN 'OPENING' " & vbCrLf & " WHEN BOOKTYPE='S' THEN 'SALARY'" & vbCrLf & " WHEN BOOKTYPE='A' THEN 'ARREAR'" & vbCrLf & " WHEN BOOKTYPE='F' THEN 'F & F'" & vbCrLf & " WHEN BOOKTYPE='Z' THEN 'SALARY'" & vbCrLf & " WHEN BOOKTYPE='V' THEN 'VARIABLE'" & vbCrLf & " ELSE 'OTHERS' END "
        End If

        SqlStr = SqlStr & " UNION ALL "

        If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " SELECT '' AS LOCKED, " & vbCrLf & " TRN.BOOKTYPE As BOOKTYPE, 'P' AS BOOKSUBTYPE, " & vbCrLf & " SAL_DATE AS V_DATE, " & vbCrLf & " ADD_DEDUCTCODE AS V_NO, " & vbCrLf & " ADD_DEDUCT.NAME AS NARRATION," & vbCrLf & " 0 AS DEBIT, " & vbCrLf & " AMOUNT  CREDIT, " & vbCrLf & " 0 AS BALANCE, " & vbCrLf & " 0 AS DC "
        Else
            SqlStr = SqlStr & vbCrLf & " SELECT '' AS LOCKED, " & vbCrLf & " TRN.BOOKTYPE As BOOKTYPE, 'P' AS BOOKSUBTYPE, " & vbCrLf & " SAL_DATE AS V_DATE, " & vbCrLf & " '' AS V_NO, " & vbCrLf & " 'PAID - FOR THE MONTH ' || TO_CHAR(SAL_DATE,'MON-YYYY') AS NARRATION," & vbCrLf & " 0 AS DEBIT, " & vbCrLf & " SUM(AMOUNT)  CREDIT, " & vbCrLf & " 0 AS BALANCE, " & vbCrLf & " 0 AS DC "

        End If
        SqlStr = SqlStr & vbCrLf & " FROM PAY_PERKS_TRN TRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE AND AMOUNT<>0"

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE IN ('P')"

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND ADD_DEDUCT.PAYMENT_TYPE='M'"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.BOOKTYPE, SAL_DATE"
        End If

        '    If optShow(0).Value = True Then
        SqlStr = SqlStr & vbCrLf & " ) ORDER BY  V_DATE,BOOKSUBTYPE, V_NO"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " ) ORDER BY  TO_CHAR(V_DATE,'YYYYMM'),BOOKSUBTYPE, V_NO"
        '    End If

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function GetOPBalance(ByRef mOpening As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOPDate As String


        ''& " AND ADD_DEDUCTCODE ='" & MainClass.AllowSingleQuote(pAddDedCode) & "'" & vbCrLf _
        '& " AND ADD_DEDUCTCODE ='" & MainClass.AllowSingleQuote(pAddDedCode) & "'" & vbCrLf _
        '
        GetOPBalance = False
        mOpening = 0

        mOPDate = GetOpeningPerksDate()


        SqlStr = " SELECT SUM(AMOUNT * DECODE(DC,'C',1,-1)) AS AMOUNT " & vbCrLf & " FROM PAY_PERKS_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & MainClass.AllowSingleQuote((TxtEmpCode.Text)) & "'" & vbCrLf & " AND SAL_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If mOPDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mOPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOpening = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        '    SqlStr = " SELECT SUM(Amount) AS Amount " & vbCrLf _
        ''            & " FROM PAY_PERKS_TRN WHERE " & vbCrLf _
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(TxtEmpCode.Text) & "'" & vbCrLf _
        ''            & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(pSalDate, "YYYYMM") & "' " & vbCrLf _
        ''            & " AND BOOKTYPE='" & lblBookType.Caption & "'" & vbCrLf _
        ''            & " AND PAID_WEEK<>'" & Trim(cboPaidWeek.Text) & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '    If RsTemp.EOF = False Then
        '        mOpening = mOpening - IIf(IsNull(RsTemp!Amount), 0, RsTemp!Amount)
        '    End If


        GetOPBalance = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        GetOPBalance = False
    End Function

    Private Function MakeSQLCond(ByRef mIsOpening As Boolean) As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        '    SqlStr = " FROM FIN_POSTED_TRN TRN , PAY_EMPLOYEE_MST ACM " & vbCrLf _
        ''            & " WHERE  "
        '
        '    If lblBookType.Caption = ConLedger Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        ''                & " AND ACM.Company_Code = " & RsCompany.Fields("Company_Code").Value & " "
        '
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " AND ACM.Company_Code = " & RsCompany.Fields("Company_Code").Value & " "
        '    End If
        '
        '    If PubUserID = "A00001" Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf _
        ''                & " AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)=ACM.SUPP_CUST_CODE "
        '    Else
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf _
        ''                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        '    End If
        '
        '    If lblBookType.Caption = ConJournalBook Then
        '        SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConJournalBookCode & "' "
        '    ElseIf lblBookType.Caption = ConContraBook Then
        '        SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConContraBookCode & "' "
        '    Else
        '        If PubUserID = "A00001" Then
        '            SqlStr = SqlStr & vbCrLf & " AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)='" & mAccountCode & "'"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
        '        End If
        '    End If
        '
        '    If mCostCCode <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.COSTCCODE='" & mCostCCode & "'"
        '    End If
        '
        '    If mDeptCode <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.DEPTCODE='" & mDeptCode & "'"
        '    End If
        '
        '    If mEmpCode <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.EMPCODE='" & mEmpCode & "'"
        '    End If
        '
        '    If mExpHeadCode <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.EXP_CODE='" & mExpHeadCode & "'"
        '    End If
        '
        '    If cboDivision.ListIndex > 0 Then
        '        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mDivisionCode = Trim(MasterNo)
        '        End If
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        '    End If
        '
        '    mGroupOption = GetGroupOption
        '    If mIsOpening = True Then
        '        mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        '    End If
        '    If mGroupOption <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        '    End If
        '
        '    If mIsOpening = True Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.Vdate<'" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
        '    Else
        '        SqlStr = SqlStr & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''                & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
        '
        '    End If

        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1

        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            FieldsVerification = False
            Exit Function
        End If


        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
