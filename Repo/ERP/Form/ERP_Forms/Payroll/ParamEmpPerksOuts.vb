Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamEmpPerksOuts
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColEmpCode As Short = 2
    Private Const ColEmpName As Short = 3
    Private Const ColOpening As Short = 4
    Private Const ColDAmount As Short = 5
    Private Const ColCAmount As Short = 6
    Private Const ColBalance As Short = 7



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        TxtEmpCode.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearch.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean


        If TxtEmpCode.Text = "" Then PrintStatus = False Else PrintStatus = True


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
        mSubTitle = mSubTitle & "  (" & TxtEmpCode.Text & ")"


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

        If TxtEmpCode.Text = "" Then PrintStatus = False Else PrintStatus = True

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
        Call SearchAccounts(TxtEmpCode)
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



    Public Sub frmParamEmpPerksOuts_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim I As Integer
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        TxtEmpCode.Visible = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamEmpPerksOuts_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
    Private Sub frmParamEmpPerksOuts_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub frmParamEmpPerksOuts_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
        Call SearchAccounts(TxtEmpCode)
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
                lblEmpName.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccounts(TxtEmpCode)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblEmpName.Text = ""
        If TxtEmpCode.Text = "" Then GoTo EventExitSub

        TxtEmpCode.Text = VB6.Format(TxtEmpCode.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblEmpName.Text = MasterNo
            TxtEmpCode.Text = UCase(Trim(TxtEmpCode.Text))
        Else
            lblEmpName.Text = ""
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
            .MaxCols = ColBalance
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            '        .RowsFrozen = 1
            .Row = -1


            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpCode, 8)

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpName, 35)

            .Col = ColOpening
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColOpening, 11)

            .Col = ColDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDAmount, 11)

            .Col = ColCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCAmount, 11)

            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalance, 11)
            .ColHidden = False

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
    Private Function LedgInfo() As Boolean

        On Error GoTo LedgError

        Dim SqlStr As String = ""


        LedgInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        '********************************

        If FillOPBalance() = False Then GoTo LedgError


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

        SqlStr = " SELECT LOCKED,  EMP_CODE, EMP_NAME, " & vbCrLf & " OPENING, DEBIT, CREDIT, BALANCE " & vbCrLf & " FROM ( "


        SqlStr = SqlStr & vbCrLf & " SELECT '' AS LOCKED, " & vbCrLf & " TRN.EMP_CODE AS EMP_CODE, EMP.EMP_NAME AS EMP_NAME, " & vbCrLf & " 0 AS OPENING, SUM(DECODE(BOOKTYPE,'P',0,AMOUNT)) AS DEBIT, " & vbCrLf & " SUM(DECODE(BOOKTYPE,'P',AMOUNT,0))  CREDIT, " & vbCrLf & " 0 AS BALANCE "


        SqlStr = SqlStr & vbCrLf & " FROM PAY_PERKS_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE =EMP.EMP_CODE" & vbCrLf & " AND TRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE AND AMOUNT<>0"

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE IN ('O','S','A','F','Z','V','P')"

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND ADD_DEDUCT.PAYMENT_TYPE='M'"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EMP_CODE='" & MainClass.AllowSingleQuote(TxtEmpCode.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.EMP_CODE, EMP.EMP_NAME"

        SqlStr = SqlStr & vbCrLf & " ) ORDER BY  EMP_CODE, EMP_NAME"


        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function FillOPBalance() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOPDate As String
        Dim cntRow As Double
        Dim mEmpCode As String
        Dim mOpening As Double
        Dim mDebit As Double
        Dim mCredit As Double
        Dim mClosing As Double


        FillOPBalance = False


        mOPDate = GetOpeningPerksDate()

        With SprdLedg
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColEmpCode

                mEmpCode = Trim(.Text)
                mOpening = 0
                mDebit = 0
                mCredit = 0
                mClosing = 0

                SqlStr = " SELECT SUM(AMOUNT * DECODE(DC,'C',1,-1)) AS AMOUNT " & vbCrLf & " FROM PAY_PERKS_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND SAL_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                If mOPDate <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mOPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mOpening = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                End If

                .Row = cntRow
                .Col = ColOpening
                .Text = VB6.Format(mOpening, "0.00")

                .Col = ColDAmount
                mDebit = CDbl(VB6.Format(.Text, "0.00"))

                .Col = ColCAmount
                mCredit = CDbl(VB6.Format(.Text, "0.00"))

                mClosing = mOpening + mDebit - mCredit

                .Col = ColBalance
                .Text = VB6.Format(mClosing, "0.00")
            Next
        End With
        FormatSprdLedg(-1)
        FillOPBalance = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FillOPBalance = False
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

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                FieldsVerification = False
                Exit Function
            End If
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
