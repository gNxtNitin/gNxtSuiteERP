Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPaidLTAArrearReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ContArrearDate As Short = 3
    Private Const ContArrearMonth As Short = 4
    Private Const ContWDays As Short = 5
    Private Const ContPaidDays As Short = 6
    Private Const ContPrevLTAAmount As Short = 7
    Private Const ContActualAmount As Short = 8
    Private Const ContPaidAmount As Short = 9
    Private Const ColDesg As Short = 10
    Private Const ColBankAcct As Short = 11
    Private Const ColPaymentMode As Short = 12
    Private Const ColBankIFSC As Short = 13

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAddDeduct
            .MaxCols = ColPaymentMode
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(mRow, ConRowHeight) ''* 1.5

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)
            .TypeMaxEditLen = 5000

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 35)
            .TypeMaxEditLen = 5000

            .ColsFrozen = ColName

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesg, 18)
            .ColHidden = True

            .Col = ColBankAcct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 10)
            .ColHidden = True

            .Col = ColPaymentMode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 10)
            .ColHidden = True

            .Col = ContArrearDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ContArrearDate, 10)
            '        .ColHidden = True

            .Col = ContArrearMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ContArrearMonth, 10)
            '        .ColHidden = True

            For cntCol = ContWDays To ContPaidAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

        End With

        Call FillHeading()
        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 1, sprdAddDeduct.MaxCols)
        sprdAddDeduct.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdAddDeduct.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdAddDeduct, mRow)
        '    MainClass.SetSpreadColor sprdAddDeduct, mRow
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        With sprdAddDeduct
            .MaxCols = ColPaymentMode
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "


            .Col = ContArrearDate
            .Text = "Arrear Date"

            .Col = ContArrearMonth
            .Text = "Arrear Month"

            .Col = ContWDays
            .Text = "WDays"

            .Col = ContPaidDays
            .Text = "Paid Days"

            .Col = ContPrevLTAAmount
            .Text = "Previous LTA Amount"

            .Col = ContActualAmount
            .Text = "Actual LTA Amount"

            .Col = ContPaidAmount
            .Text = "Paid Amount"

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColBankAcct
            .Text = "Bank Account"

            .Col = ColPaymentMode
            .Text = "Payment Mode"


        End With
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkDiv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDiv.CheckStateChanged
        If chkDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()

        Dim mRptFileName As String
        Dim mBankName As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mChequeAmount As String
        Dim pNarr As String

        If optShow(0).Checked = True Then
            mTitle = "LTA ARREAR -  Register"

            mRptFileName = "LTAARREARREGDET.Rpt"
            If FillPrintDummyData(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, 0, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo ERR1

            mSubTitle = "As On : " & lblRunDate.Text
        Else
            frmPrintOTReg.optCheckList.Text = "Register"
            frmPrintOTReg.ShowDialog()
            If G_PrintLedg = False Then
                Exit Sub
            End If

            Call MainClass.ClearCRptFormulas(Report1)

            If frmPrintOTReg.optCheckList.Checked = True Then
                mTitle = "LTA ARREAR -  Register"

                mRptFileName = "LTAARREARREG.Rpt"
                If FillPrintDummyData(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 0, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo ERR1

                mSubTitle = "As On : " & lblRunDate.Text

            ElseIf frmPrintOTReg.optBank.Checked = True Then
                '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, "CHEQUE", False) = False Then GoTo ERR1

                If frmPrintOTReg.optAllBank(0).Checked = True Then
                    mBankName = ""
                Else
                    mBankName = frmPrintOTReg.txtBankName.Text
                End If
                If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, ColCode, ColName, 0, ColPaymentMode, ContPaidAmount, ColBankAcct, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1



                mRptFileName = "BankSheet.Rpt"

                '        mBankName = InputBox("Please Enter Bank Name. :", "Bank Name")

                mTitle = "BANK ANNEXURES OF " & mBankName

                mSubTitle = "LTA Arrear For the Preiod From : " & lblRunDate.Text

            ElseIf frmPrintOTReg.optCash.Checked = True Then
                '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, "CASH", False) = False Then GoTo ERR1
                If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, ColCode, ColName, 0, ColPaymentMode, ContPaidAmount, ColBankAcct, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1

                mRptFileName = "SalCashSheet.Rpt"
                mTitle = "LTA Arrear (Cash)"
            ElseIf frmPrintOTReg.optBankTxt.Checked = True Then
                '        If CreateTxtFileForBankold = False Then GoTo ERR1
                pNarr = "BY LTA ARREAR OF " & lblRunDate.Text
                If frmPrintOTReg.optAllBank(0).Checked = True Then
                    mBankName = ""
                Else
                    mBankName = frmPrintOTReg.txtBankName.Text
                End If
                If CreateTxtFileForBank(sprdAddDeduct, ColCode, ColName, ColPaymentMode, ColBankAcct, ContPaidAmount, mBankName, pNarr, sprdAddDeduct.MaxRows - 1) = False Then GoTo ERR1

                frmPrintOTReg.Close()
                Exit Sub
            End If

        End If
        'Select Record for print...

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        frmPrintOTReg.Close()
        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Function GetTotalAmount() As Double
        On Error GoTo ErrPart1
        Dim cntRow As Integer
        Dim mAmount As Double
        With sprdAddDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ContPaidAmount
                mAmount = mAmount + IIf(IsNumeric(.Text), .Text, 0)
            Next
        End With
        GetTotalAmount = mAmount
        Exit Function

ErrPart1:
        GetTotalAmount = 0
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        cmdAccountPost.Enabled = mPrintEnable
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        MainClass.ClearGrid(sprdAddDeduct)
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
        CalcSubTotal()
        FormatSprd(-1)
    End Sub
    Private Sub CalcSubTotal()

        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim arrsal() As Double
        Dim mPaidAmount As Double

        Call MainClass.AddBlankfpSprdRow(sprdAddDeduct, ColName)

        '    ReDim arrsal(sprdAddDeduct.MaxCols)

        With sprdAddDeduct

            mPaidAmount = 0
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ContPaidAmount
                mPaidAmount = mPaidAmount + Val(VB6.Format(sprdAddDeduct.Text, "0.00"))
            Next

            .Row = .MaxRows
            .Col = ColName
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "GRAND TOTAL"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            '        For cntRow = 1 To .MaxRows
            '            .Row = cntRow
            '            For cntCol = ContPaidAmount To ContPaidAmount
            '                .Col = cntCol
            '                arrsal(cntCol) = arrsal(cntCol) + Val(sprdAddDeduct.Text)
            '            Next
            '        Next
            '
            '        .Row = .MaxRows
            '        For cntCol = ContPaidAmount To ContPaidAmount
            '            .Col = cntCol
            '            sprdAddDeduct.Text = arrsal(cntCol)
            '            .FontBold = True
            '        Next



            .Row = .MaxRows
            .Col = ContPaidAmount
            sprdAddDeduct.Text = CStr(mPaidAmount)


        End With

    End Sub
    Private Sub frmPaidLTAArrearReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmPaidLTAArrearReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        If PubSourceData = ConAccess Then
            cmdAccountPost.Enabled = False
        End If

        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        OptName.Checked = True
        optShow(0).Checked = True

        lblRunDate.Text = CStr(RunDate)
        SetDate(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)


        FillHeading()
        FillDeptCombo()
        '    txtFrom.Text = Format(RsCompany!START_DATE, "dd/mm/yyyy")
        '    txtTo.Text = Format(RsCompany!END_DATE, "dd/mm/yyyy")
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        chkDiv.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmPaidLTAArrearReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAddDeduct.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim mDeptCode As String
        Dim mDivisionCode As Double

        MainClass.ClearGrid(sprdAddDeduct)

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                MsgInformation("Please select the Division Name.")
                cboDivision.Focus()
                Exit Sub
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If optShow(0).Checked Then
            SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, TO_CHAR(ID.ARREAR_DATE,'DD/MM/YYYY') AS ARREAR_DATE, " & vbCrLf & " TO_CHAR(ID.LTA_MONTH,'DD/MM/YYYY') AS LTA_MONTH, ID.WDAYS, ID.PAID_DAYS, ID.PREV_LTA_AMOUNT, " & vbCrLf & " ID.ACTUAL_AMOUNT, ID.PAID_AMOUNT," & vbCrLf & " GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",EMP.EMP_CODE,TO_DATE) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE"
        Else
            SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, TO_CHAR(ID.ARREAR_DATE,'DD/MM/YYYY') AS ARREAR_DATE, " & vbCrLf & " '', SUM(ID.WDAYS), SUM(ID.PAID_DAYS), SUM(ID.PREV_LTA_AMOUNT), " & vbCrLf & " SUM(ID.ACTUAL_AMOUNT), SUM(ID.PAID_AMOUNT)," & vbCrLf & " GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",EMP.EMP_CODE,ID.ARREAR_DATE) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE"
        End If

        SqlStr = SqlStr & vbCrLf & " FROM PAY_LTA_ARREAR_HDR IH, PAY_LTA_ARREAR_DET ID, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.FYEAR=ID.FYEAR AND IH.EMP_CODE=ID.EMP_CODE AND IH.ARREAR_DATE=ID.ARREAR_DATE"


        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.ARREAR_DATE,'YYYYMM')='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "' "


        SqlStr = SqlStr & vbCrLf & " AND EMP_STOP_SALARY='N' "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "'"
            End If
        End If

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If optShow(1).Checked Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY EMP.EMP_CODE, EMP.EMP_NAME, ID.ARREAR_DATE," & vbCrLf & " GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",EMP.EMP_CODE,ID.ARREAR_DATE), " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE')"
        End If


        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
        End If

        If optShow(0).Checked Then
            SqlStr = SqlStr & vbCrLf & " ,ID.LTA_MONTH"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAddDeduct, StrConn, "Y")
        Call PrintCommand(True)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing

        cboDept.Items.Clear()

        SqlStr = "Select DEPT_DESC from PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub txtFrom_Change()
        Call PrintCommand(False)
    End Sub
    '
    'Private Sub txtFrom_Validate(Cancel As Boolean)
    '    If Not IsDate(txtFrom.Text) Then
    '        MsgInformation "Please enter the vaild date."
    '        Cancel = True
    '        Exit Sub
    '    ElseIf FYChk(txtFrom.Text) = False Then
    '        Cancel = True
    '    End If
    '    txtFrom.Text = Format(txtFrom.Text, "dd/mm/yyyy")
    'End Sub
    '
    '
    'Private Sub txtTo_Change()
    '    Call PrintCommand(False)
    'End Sub
    '
    'Private Sub txtTo_Validate(Cancel As Boolean)
    '    If Not IsDate(txtTo.Text) Then
    '        MsgInformation "Please enter the vaild date."
    '        Cancel = True
    '        Exit Sub
    '    ElseIf FYChk(txtTo.Text) = False Then
    '        Cancel = True
    '    End If
    '    txtTo.Text = Format(txtTo.Text, "dd/mm/yyyy")
    'End Sub
    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        Call PrintCommand(False)
        'RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        Call PrintCommand(False)
        'RefreshScreen
    End Sub

    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)


        Daysinmonth = MainClass.LastDay(VB6.Format(lblYear.Text, "mm"), VB6.Format(lblYear.Text, "yyyy"))
    End Sub
End Class
