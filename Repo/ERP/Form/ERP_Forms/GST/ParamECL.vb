Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamECL
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection

    Dim mAccountCode As Integer
    Private Const ColLocked As Short = 1
    Private Const Col1 As Short = 2
    Private Const col2 As Short = 3
    Private Const Col3 As Short = 4
    Private Const Col4 As Short = 5
    Private Const Col5 As Short = 6
    Private Const Col6 As Short = 7
    Private Const Col7 As Short = 8
    Private Const Col8 As Short = 9
    Private Const Col9 As Short = 10
    Private Const Col10 As Short = 11
    Private Const Col11 As Short = 12
    Private Const Col12 As Short = 13
    Private Const Col13 As Short = 14
    Private Const Col14 As Short = 15
    Private Const Col15 As Short = 16
    Private Const Col16 As Short = 17
    Private Const Col17 As Short = 18
    Private Const Col18 As Short = 19
    Private Const Col19 As Short = 20
    Private Const ColMKEY As Short = 21


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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        '    Call ReportShow("V")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        '    Call ReportShow("P")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamECL_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamECL_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamECL_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamECL_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
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
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 2.5)
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


            .Col = Col1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 12)

            .Col = col2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)

            .Col = Col3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)

            .Col = Col4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 10)

            .Col = Col5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)

            For cntCol = Col6 To Col16
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


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 12)
            .ColHidden = True

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL("N")
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL(ByRef pIsOpening As String) As String
        On Error GoTo ERR1


        ''SELECT CLAUSE...
        MakeSQL = "SELECT '', " & vbCrLf & " IH.CHALLANDATE AS DEPOSIT_DATE, " & vbCrLf & " '' AS DEPOSIT_TIME, " & vbCrLf & " '' AS REPORT_DATE, " & vbCrLf & " IH.CHALLANNO AS REF_NO, " & vbCrLf & " IH.REF_DATE AS TAX_PERIOD, " & vbCrLf & " ID.GST_TYPE AS DESCRIPTION," & vbCrLf & " 'DEBIT' AS TRANS_TYPE, " & vbCrLf & " ID.TAX_PAYABLE AS TAX_AMOUNT," & vbCrLf & " 0 AS INTEREST_AMOUNT," & vbCrLf & " 0 AS PENALTY_AMOUNT," & vbCrLf & " 0 AS FEE_AMOUNT," & vbCrLf & " 0 AS OTH_AMOUNT," & vbCrLf & " '' AS TOTAL_AMOUNT," & vbCrLf & " '' AS TAX_BALAMOUNT," & vbCrLf & " '' AS INTEREST_BALAMOUNT," & vbCrLf & " '' AS PENALTY_BALAMOUNT," & vbCrLf & " '' AS FEE_BALAMOUNT," & vbCrLf & " '' AS OTH_BALAMOUNT," & vbCrLf & " '' AS TOTAL_BALAMOUNT," & vbCrLf & " '' AS MKEY"

        '   SQL> desc FIN_GSTCHALLAN_DET
        ' Name
        ' ------------------------------
        ' COMPANY_CODE
        ' FYEAR
        ' REF_NO
        ' REF_DATE
        ' SERIAL_NO
        ' GST_TYPE
        ' TAX_PAYABLE
        ' PAID_FROM_IGST
        ' PAID_FROM_CGST
        ' PAID_FROM_SGST
        ' PAID_FROM_CESS
        ' CASH_PAID
        ' INTEREST_AMT
        ' LATE_FEE
        '
        '
        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_GSTCHALLAN_HDR IH, FIN_GSTCHALLAN_DET ID"

        ''& " AND B.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf

        ''WHERE CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.FYEAR=ID.FYEAR" & vbCrLf & " AND IH.REF_NO=ID.REF_NO"

        '    If cboShowType.ListIndex = 0 Then
        '        MakeSQL = MakeSQL & vbCrLf & " AND IH.IS_RC='N'"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf & " AND IH.IS_RC='Y'"
        '    End If

        If pIsOpening = "Y" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.REF_DATE"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        'Dim cntRow As Long
        'Dim mOpening As Double
        'Dim mClosing As Double
        'Dim mCredit As Double
        'Dim mDebit As Double
        '
        '
        'Dim mCessOpening As Double
        'Dim mCessClosing As Double
        'Dim mCessCredit As Double
        'Dim mCessDebit As Double
        '
        'Dim mSHECessOpening As Double
        'Dim mSHECessClosing As Double
        'Dim mSHECessCredit As Double
        'Dim mSHECessDebit As Double
        '
        'Dim mMiscOpening As Double
        'Dim mMiscCredit As Double
        'Dim mMiscDebit As Double
        '
        '    mOpening = GetOpBalance("B")
        '
        '    With SprdMain
        '        .MaxRows = .MaxRows + 1
        '        .Row = 1
        '        .Action = SS_ACTION_INSERT_ROW
        '        .Col = col2
        '
        '        .Row = 1
        '        .Text = "OPENING BALANCE: "
        '        .FontBold = True
        '        'FormatSprdLedg -1
        '
        '        .Col = Col10
        '        .Text = Format(mOpening, "0.00")
        '
        '        .Col = Col13
        '        .Text = Format(mCessOpening, "0.00")
        '
        '        .Col = Col16
        '        .Text = Format(mSHECessOpening, "0.00")
        '
        ''        .Col = Col9C
        ''        .Text = Format(mMiscOpening, "0.00")
        '
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '
        '            .Col = Col6
        '            mCredit = mCredit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col7
        '            mDebit = mDebit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '             'Misc
        '            .Col = Col8
        '            mMiscCredit = mMiscCredit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col9
        '            mMiscDebit = mMiscDebit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col10
        '            .Text = mOpening + mCredit + mMiscCredit - mDebit - mMiscDebit
        '
        '            ''Cess
        '            .Col = Col11
        '            mCessCredit = mCessCredit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col12
        '            mCessDebit = mCessDebit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col13
        '            .Text = mCessOpening + mCessCredit - mCessDebit
        '
        '            ''S.H.E. Cess
        '            .Col = Col14
        '            mSHECessCredit = mSHECessCredit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col15
        '            mSHECessDebit = mSHECessDebit + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))
        '
        '            .Col = Col16
        '            .Text = mSHECessOpening + mSHECessCredit - mSHECessDebit
        '
        ''            .Col = Col9C
        ''            .Text = mMiscOpening + mMiscCredit - mMiscDebit
        '
        '        Next
        '
        '        Call MainClass.AddBlankfpSprdRow(SprdMain, Col1)
        '
        '        .Col = col2
        '        .Row = .MaxRows
        '        .Text = "GRAND TOTAL :"
        '        .FontBold = True
        '
        '        .Row = .MaxRows
        '        .Row2 = .MaxRows
        '        .Col = 1
        '        .col2 = .MaxCols
        '        .BlockMode = True
        '        .BackColor = &H80FF80
        '        .BlockMode = False
        '
        '        .Row = .MaxRows
        '
        '        .Col = Col6
        '        .Text = Format(mCredit, "0.00")
        '        .FontBold = True
        '
        '
        '        .Col = Col7
        '        .Text = Format(mDebit, "0.00")
        '        .FontBold = True
        '
        '        .Col = Col11
        '        .Text = Format(mCessCredit, "0.00")
        '        .FontBold = True
        '
        '
        '        .Col = Col12
        '        .Text = Format(mCessDebit, "0.00")
        '        .FontBold = True
        '
        '        .Col = Col14
        '        .Text = Format(mSHECessCredit, "0.00")
        '        .FontBold = True
        '
        '
        '        .Col = Col15
        '        .Text = Format(mSHECessDebit, "0.00")
        '        .FontBold = True
        '
        '        .Col = Col8
        '        .Text = Format(mMiscCredit, "0.00")
        '        .FontBold = True
        '
        '
        '        .Col = Col9
        '        .Text = Format(mMiscDebit, "0.00")
        '        .FontBold = True
        '
        '    End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function ReportShow(ByRef pPrintMode As String) As Boolean
        On Error GoTo ErrPart
        'Dim mLineCount As Long
        'Dim pPageNo As Long
        'Dim cntRow As Double
        'Dim mPrintFooter As Boolean
        '
        'Dim mCenvat As Double
        'Dim mDebitAmount As Double
        '
        'Dim pFileName As String
        '
        '    mCenvat = 0
        '
        '    mLineCount = 1
        '    pFileName = mLocalPath & "\Report.Prn"
        '    ''Shell "ATTRIB +A -R " & pFileName
        '
        '    Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        '
        '    With SprdMain
        '        If .MaxRows >= 1 Then
        '
        '            Open pFileName For Output As #1
        '            For cntRow = 1 To .MaxRows - 1
        '                If mLineCount = 1 Then
        '                    pPageNo = pPageNo + 1
        '                    Call PrintHeader(mLineCount)
        '                    mPrintFooter = False
        '                    If pPageNo <> 1 Then
        '                        Call PrintPageTotal("Brought Forward : ", mLineCount, mCenvat, mDebitAmount)
        '                        mLineCount = mLineCount + 1
        '                    End If
        '                End If
        '
        '                .Row = cntRow
        '                .Col = Col11
        '                mCenvat = Format(mCenvat + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00")
        '                .Col = Col10
        '                mDebitAmount = Format(mDebitAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00")
        '
        '                Call PrintDetail(cntRow, mLineCount)
        '
        '                If mLineCount >= 65 And mPrintFooter = False Then
        '                    Print #1, Tab(0); Chr(20) & String(230, "-") & Chr(15)
        '                    mLineCount = mLineCount + 1
        '                    Call PrintPageTotal("Page Total :", mLineCount, mCenvat, mDebitAmount)
        '                    Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
        '                ElseIf cntRow = SprdMain.MaxRows - 1 Then
        '                    Do While mLineCount <= 65
        '                        Print #1, " "
        '                        mLineCount = mLineCount + 1
        '                    Loop
        '                    Print #1, Tab(0); String(230, "-")
        '                    mLineCount = mLineCount + 1
        '                    Call PrintPageTotal("Grand Total :", mLineCount, mCenvat, mDebitAmount)
        '                    Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
        '                End If
        '            Next
        '             Close #1
        '        End If
        '    End With
        '
        '    If pPrintMode = "P" Then
        '        Shell App.path & "\PrintReport.bat",vbNormalFocus
        '    Else
        '        Shell "ATTRIB +R -A " & pFileName
        '        Shell "NOTEPAD.EXE " & pFileName, vbMaximizedFocus
        '        'App.Path & "\RVIEW.EXE "
        '    End If

        ReportShow = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        ReportShow = False
        ''Resume
        FileClose(1)
    End Function

    Private Sub FillHeading()

        With SprdMain
            .Row = 0

            .Col = ColLocked
            .Text = "Locked"

            .Col = Col1
            .Text = "Date of deposit/Debit"

            .Col = col2
            .Text = "Time of deposit"

            .Col = Col3
            .Text = "Reporting Date"

            .Col = Col4
            .Text = "Reference Number"

            .Col = Col5
            .Text = "Tax Period, if applicable"

            .Col = Col6
            .Text = "Description"

            .Col = Col7
            .Text = "Type of Transaction [Debit / Credit]"

            .Col = Col8
            .Text = "Tax [Debit / Credit]"

            .Col = Col9
            .Text = "Interest [Debit / Credit]"

            .Col = Col10
            .Text = "Penalty [Debit / Credit]"

            .Col = Col11
            .Text = "Fee [Debit / Credit]"

            .Col = Col12
            .Text = "Others [Debit / Credit]"

            .Col = Col13
            .Text = "Total [Debit / Credit]"

            .Col = Col14
            .Text = "Tax [Balance]"

            .Col = Col15
            .Text = "Interest [Balance]"

            .Col = Col16
            .Text = "Penalty [Balance]"

            .Col = Col17
            .Text = "Fee [Balance]"

            .Col = Col18
            .Text = "Others [Balance]"

            .Col = Col19
            .Text = "Total [Balance]"

            .Col = ColMKEY
            .Text = CStr(ColMKEY)

        End With

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


    Private Function GetOpBalance(ByRef IsFieldType As String) As Double
        On Error GoTo ErrPart
        Dim mOpening As Double
        Dim mSql As String
        Dim RsTemp As ADODB.Recordset = Nothing

        '    mSql = "SELECT * FROM FIN_RG23IIOPAMT_MST " & vbCrLf _
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''        & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        '    MainClass.UOpenRecordSet mSql, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        If IsFieldType = "B" Then
        '            mOpening = IIf(IsNull(RsTemp!PLA), 0, RsTemp!PLA)
        '        ElseIf IsFieldType = "C" Then
        '            mOpening = IIf(IsNull(RsTemp!PLA_CESS), 0, RsTemp!PLA_CESS)
        '        ElseIf IsFieldType = "H" Then
        '            mOpening = IIf(IsNull(RsTemp!PLA_SHECESS), 0, RsTemp!PLA_SHECESS)
        '        End If
        '    Else
        '        mOpening = 0
        '    End If
        '
        '    If IsFieldType = "B" Then
        '        mSql = "SELECT SUM(B.MODVATAMOUNT) AS MODVATAMOUNT "
        '    ElseIf IsFieldType = "C" Then
        '        mSql = "SELECT SUM(B.CESSAMOUNT) AS MODVATAMOUNT "
        '    ElseIf IsFieldType = "H" Then
        '        mSql = "SELECT SUM(B.SHECMODVATAMOUNT) AS MODVATAMOUNT "
        '    End If
        '
        '    ''FROM CLAUSE...
        '    mSql = mSql & vbCrLf & " FROM FIN_PURCHASE_HDR B"
        '
        '    ''WHERE CLAUSE...''& " AND B.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
        '
        '    mSql = mSql & vbCrLf & " WHERE " & vbCrLf _
        ''            & " B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ISMODVAT='Y' AND B.ISPLA='Y'"
        '
        '    mSql = mSql & vbCrLf _
        ''            & " AND B.MODVATDATE>=TO_DATE('" & VB6.Format(RsCompany!Start_Date, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND B.MODVATDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"
        '
        '    MainClass.UOpenRecordSet mSql, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mOpening = mOpening + IIf(IsNull(RsTemp!MODVATAMOUNT), 0, RsTemp!MODVATAMOUNT)
        '    End If
        GetOpBalance = mOpening
        Exit Function
ErrPart:
        GetOpBalance = 0
    End Function
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertPrintDummy()


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Personal Ledger Account"
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        mRPTName = "PLA.Rpt"
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

        Dim mRptOpening As Double
        Dim mRptDeposited As Double
        Dim mRptDebited As Double

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")

        With SprdMain
            .Row = 1

            .Col = Col10
            mRptOpening = Val(.Text)

            .Row = .MaxRows

            .Col = Col6
            mRptDeposited = Val(.Text)

            .Col = Col7
            mRptDebited = Val(.Text)

        End With

        MainClass.AssignCRptFormulas(Report1, "Opening=""" & VB6.Format(mRptOpening, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "TotDeposited=""" & VB6.Format(mRptDeposited, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "TotDebited=""" & VB6.Format(mRptDebited, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "PLANO=""" & IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "ECCNO=""" & IIf(IsDbNull(RsCompany.Fields("ECC_NO").Value), "", RsCompany.Fields("ECC_NO").Value) & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub

    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub InsertPrintDummy()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCol1 As String
        Dim mCol2 As String
        Dim mCol6 As String
        Dim mCol7 As String
        Dim mCol10 As String
        Dim mCol11 As String
        Dim mCol12 As String
        Dim mCol13 As String
        Dim mCol14 As String
        Dim mCol15 As String
        Dim mCol16 As String
        Dim mCol8 As String
        Dim mCol9 As String
        Dim mCol9C As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = Col1
                mCol1 = Replace(.Text, "'", "''")

                .Col = col2
                mCol2 = Replace(.Text, "'", "''")


                .Col = Col6
                mCol6 = Trim(.Text)

                .Col = Col7
                mCol7 = Trim(.Text)

                .Col = Col10
                mCol10 = Trim(.Text)

                .Col = Col11
                mCol11 = Trim(.Text)

                .Col = Col12
                mCol12 = Trim(.Text)

                .Col = Col13
                mCol13 = Trim(.Text)

                .Col = Col14
                mCol14 = Trim(.Text)

                .Col = Col15
                mCol15 = Trim(.Text)

                .Col = Col16
                mCol16 = Trim(.Text)

                .Col = Col8
                mCol8 = Trim(.Text)

                .Col = Col9
                mCol9 = Trim(.Text)

                '            .Col = Col9C
                '            mCol9C = Trim(.Text)

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf & " Field1,Field2,Field6,Field7,Field8," & vbCrLf & " Field9,Field10,Field11, " & vbCrLf & " Field12,Field13,Field14,Field15,Field16,Field17 " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mCol1 & "', '" & Trim(mCol2) & "', " & vbCrLf & " '" & Trim(mCol6) & "', '" & Trim(mCol7) & "', '" & Trim(mCol10) & "'," & vbCrLf & " '" & Trim(mCol11) & "', '" & Trim(mCol12) & "','" & Trim(mCol13) & "'," & vbCrLf & " '" & Trim(mCol14) & "', '" & Trim(mCol15) & "','" & Trim(mCol16) & "', " & vbCrLf & " '" & Trim(mCol8) & "', '" & Trim(mCol9) & "','" & Trim(mCol9C) & "') "

                PubDBCn.Execute(SqlStr)
            Next

        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
End Class
