Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewOutsSummary
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Dim PrintEnable As Boolean
    Dim PrintCopies As Short
    Dim NewFlagsSetting As Integer
    Dim OldFlagsSetting As Integer

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColSRNo As Short = 3
    Private Const ColPartyCode As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColPaymentTerms As Short = 6
    Private Const ColOpeningBal As Short = 7
    Private Const ColBillNo As Short = 8
    Private Const ColBillDate As Short = 9
    Private Const ColDue1 As Short = 10
    Private Const ColDue2 As Short = 11
    Private Const ColDue3 As Short = 12
    Private Const ColDue4 As Short = 13
    Private Const ColTotalDue As Short = 14
    Private Const ColClosingBalance As Short = 15
    Private Const ColPaidinMonth As Short = 16
    Private Const ColPaymentMode As Short = 17
    Private Const ColFlag As Short = 18

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Private Const ConRowHeight As Short = 15
    Dim mClickProcess As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cboChqsInMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboChqsInMonth.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub

    Private Sub cboChqsInMonth_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboChqsInMonth.SelectedIndexChanged
        PrintEnable = False
        PrintCommand()
    End Sub


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        PrintEnable = False
        PrintCommand()
    End Sub

    Private Sub cboPaymentMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub

    Private Sub cboPaymentMode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.SelectedIndexChanged
        PrintEnable = False
        PrintCommand()
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForReminder(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForReminder(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Dim RS As ADODB.Recordset
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE ='S'"
        Else
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE ='C'"
        End If

        If MainClass.SearchGridMaster((TxtName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                TxtName.Text = AcName
            End If
        End If


        'If MainClass.SearchMaster((TxtName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr) = True Then
        '    TxtName.Text = AcName
        'End If

    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ERR1
        Dim mLastDays As Integer

        SetDate(CDate(lblYear.Text))

        mLastDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        If Trim(TxtName.Text) = "" And optParty(0).Checked = True Then
            MsgInformation("Account Name Cann't be Blank.")
            TxtName.Focus()
            PrintEnable = False
            PrintCommand()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If


        If Val(txtDays1.Text) < 1 Or Val(txtDays1.Text) > mLastDays Then
            MsgInformation("Invalid Month Day.")
            txtDays1.Focus()
            Exit Sub
        End If

        If Val(txtDays2.Text) < 1 Or Val(txtDays2.Text) > mLastDays Then
            MsgInformation("Invalid Month Day.")
            txtDays2.Focus()
            Exit Sub
        End If

        If Val(txtDays3.Text) < 1 Or Val(txtDays3.Text) > mLastDays Then
            MsgInformation("Invalid Month Day.")
            txtDays3.Focus()
            Exit Sub
        End If

        If Val(txtDays4.Text) < 1 Or Val(txtDays4.Text) > mLastDays Then
            MsgInformation("Invalid Month Day.")
            txtDays4.Focus()
            Exit Sub
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdView)
        PrintEnable = True
        PrintCommand()
        Call FormatSprdView(-1)

        'If OptShow(0).Checked = True Then
        ViewOutsSummary()
            'Else
            '    ViewOutsAdhocSummary()
            'End If
            DisplayTotals()
        Me.Cursor = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView(ByRef Arow As Integer)

        On Error GoTo ErrPart
        Dim cntCol As Integer

        With SprdView
            .MaxCols = ColFlag

            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False
            .set_ColWidth(ColPicMain, 2)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColPicSub
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False
            .set_ColWidth(ColPicSub, 2)
            .ColHidden = True

            .Col = ColSRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColSRNo, 4)

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyCode, 8)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 80
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyName, 25)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColBillNo, 8)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColBillDate, 8)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If



            '        If optParty(0).Value = True Then
            '            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If
            '
            .Col = ColPaymentTerms
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditLen = 60
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMin = CInt("0")
            .TypeIntegerMax = CInt("99999999")
            .set_ColWidth(ColPaymentTerms, 5)
            '        .ColHidden = True

            For cntCol = ColOpeningBal To ColOpeningBal
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next

            For cntCol = ColDue1 To ColClosingBalance
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next

            .Col = ColPaymentMode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPaymentMode, 10)
            .TypeEditMultiLine = True
            .ColHidden = False

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColPaidinMonth
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMin = CInt("0")
            .TypeIntegerMax = CInt("99999999")
            .set_ColWidth(ColPaidinMonth, 10)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With

        Call FillHeading()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewOutsSummary_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
        Dim SqlStr As String

        Call SetMainFormCordinate(Me)
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormLoaded = False

        Call FillCombo()

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        txtDays1.Text = "3"
        txtDays2.Text = "15"
        txtDays3.Text = "19"
        txtDays4.Text = "30"

        lblRunDate.Text = CStr(RunDate)
        lblYear.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)     '' VB6.Format(lblYear.Text, "MMMM YYYY")

        SetDate(CDate(lblYear.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        MainClass.SetControlsColor(Me)
        PrintEnable = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub FillCombo()

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim mPaymentMode As String
        Dim mPaymentModeStr As String
        Dim CntLst As Long

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        cboChqsInMonth.Items.Clear()
        cboChqsInMonth.Items.Add("All")
        cboChqsInMonth.Items.Add("0")
        cboChqsInMonth.Items.Add("1")
        cboChqsInMonth.Items.Add("2")
        cboChqsInMonth.Items.Add("3")
        cboChqsInMonth.Items.Add("4")
        cboChqsInMonth.SelectedIndex = 0


        '     mPaymentMode = IIf(IsNull(RsTemp!PAYMENT_MODE), "", RsTemp!PAYMENT_MODE)
        '            If mPaymentMode = "1" Then
        '                .Text = "CHEQUE"
        '            ElseIf mPaymentMode = "2" Then
        '                .Text = "HUNDI"
        '            Else
        '                .Text = "LC"
        '            End If
        '
        '
        '    Sqlstr = "SELECT DISTINCT PAYMENT_MODE FROM FIN_SUPP_CUST_MST " & vbCrLf _
        ''        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''        & " ORDER BY PAYMENT_MODE"
        '    MainClass.UOpenRecordSet Sqlstr, PubDBCn, adOpenStatic, RS, adLockReadOnly
        '
        '    cboPaymentMode.AddItem "ALL"
        '
        '    If RS.EOF = False Then
        '        Do While RS.EOF = False
        '            mPaymentMode = IIf(IsNull(RS!PAYMENT_MODE), "", RS!PAYMENT_MODE)
        '
        '            If mPaymentMode = "1" Then
        '                mPaymentModeStr = "1. CHEQUE"
        '            ElseIf mPaymentMode = "2" Then
        '                mPaymentModeStr = "2. HUNDI"
        '            Else
        '                mPaymentModeStr = "3. LC"
        '            End If
        '
        '            cboPaymentMode.AddItem mPaymentModeStr
        '            RS.MoveNext
        '        Loop
        '    End If

        cboPaymentMode.Items.Add("ALL")
        cboPaymentMode.Items.Add("1. CHEQUE")
        cboPaymentMode.Items.Add("2. HUNDI")
        cboPaymentMode.Items.Add("3. LC")
        cboPaymentMode.Items.Add("4. MSME")
        cboPaymentMode.Items.Add("5. PDC")
        cboPaymentMode.Items.Add("6. DISC-YES")
        cboPaymentMode.Items.Add("7. DISC-CASH")
        cboPaymentMode.Items.Add("8. DISC-TCFL")
        cboPaymentMode.Items.Add("9. UGRO")
        cboPaymentMode.Items.Add("10. BLANK")
        cboPaymentMode.SelectedIndex = 0


        '
        '    CboShowFor.AddItem "Uncleared"
        '    CboShowFor.AddItem "Cleared"
        '    CboShowFor.AddItem "Both"
        '    CboShowFor.ListIndex = 0

        '    MainClass.FillCombo CboCostC, "CST_CENTER_MST", "NAME", "ALL"
        '    MainClass.FillCombo CboDept, "PAY_DESC_MST", "NAME", "ALL"
        '    MainClass.FillCombo cboEmp, "PAY_EMPLOYEE_MST", "NAME", "ALL"

        '    CboCostC.ListIndex = -1
        '    CboDept.ListIndex = -1
        '    cboEmp.ListIndex = -1


        '    cboConsolidated.Clear
        '    cboConsolidated.AddItem "COMPANY"
        '    cboConsolidated.AddItem "REGION"
        '    cboConsolidated.AddItem "BRANCH"
        '    cboConsolidated.AddItem "DIVISION"
        '    cboConsolidated.Enabled = True

        '    cboConsolidated.ListIndex = 3

        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")
        txtPaymentDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
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
    Private Sub frmViewOutsSummary_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        '    Frame1.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmViewOutsSummary_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub


    Private Sub ViewOutsSummary()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mPartyCode As String
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDueDate As String


        Dim mOPDate As String
        Dim mDueDate1 As String
        Dim mDueDate2 As String
        Dim mDueDate3 As String
        Dim mDueDate4 As String
        Dim mLastDate As String


        'Dim mPartyCode As String
        Dim mPaymentTerm As Integer
        Dim mChqFequency As Integer

        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double

        Dim mDivisionCode As Double
        Dim cntRow As Integer
        Dim cntSNo As Integer
        Dim mPaymentMode As String

        Dim CntLst As Integer
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""


        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate1 = Val(txtDays1.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate2 = Val(txtDays2.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate3 = Val(txtDays3.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate4 = Val(txtDays4.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        mDueDate = "DECODE(FROM_DAYS,NULL,0,FROM_DAYS)"


        SqlStr = " Select  '','', '', CH.SUPP_CUST_Code, TRN.PARTYNAME," & vbCrLf & " " & mDueDate & " As Due_Days, CH.PAYMENT_MODE,"

        '    SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(BALANCE * DECODE(DC,'DR',1,-1)))) AS BALANCE, "

        If optAsOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TRN.BILLDATE < TO_DATE('" & VB6.Format(mOPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS OPBal,"
            SqlStr = SqlStr & vbCrLf & " 0, 0, 0, 0, 0, "
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TRN.BILLDATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS CLBal,"
            SqlStr = SqlStr & vbCrLf & " TO_NUMBER(ACTIVITY) AS ACTIVITY"
        Else
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN NVL(TRN.EXPDATE,TRN.BILLDATE) < TO_DATE('" & VB6.Format(mOPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS OPBal,"
            SqlStr = SqlStr & vbCrLf & " 0, 0, 0, 0, 0, "
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN NVL(TRN.EXPDATE,TRN.BILLDATE) <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS CLBal,"
            SqlStr = SqlStr & vbCrLf & " TO_NUMBER(ACTIVITY) AS ACTIVITY,'0'"
        End If

        SqlStr = SqlStr & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN, FIN_SUPP_CUST_MST CH, FIN_PAYTERM_MST PMST, GEN_COMPANY_MST CC "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If



        SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code=CH.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=CH.SUPP_CUST_Code " & vbCrLf _
            & " AND CH.Company_Code=PMST.Company_Code " & vbCrLf _
            & " AND CH.PAYMENT_CODE=PMST.PAY_TERM_CODE "

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If cboChqsInMonth.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND TO_NUMBER(ACTIVITY)=" & Val(cboChqsInMonth.Text) & ""
        End If

        If cboPaymentMode.SelectedIndex > 0 Then
            If cboPaymentMode.SelectedIndex = 9 Then
                SqlStr = SqlStr & vbCrLf & " AND (CH.PAYMENT_MODE IS NULL OR CH.PAYMENT_MODE='')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND CH.PAYMENT_MODE='" & VB.Left(Trim(cboPaymentMode.Text), 1) & "'"
            End If
        End If

        If Val(txtPaymentDays.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND " & mDueDate & "=" & Val(txtPaymentDays.Text) & ""
        End If

        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & " AND TRN.SUPP_CUST_TYPE ='S'"
        Else
            SqlStr = SqlStr & " AND TRN.SUPP_CUST_TYPE ='C'"
        End If

        If optParty(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PARTYNAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"
        End If

        If optAsOn(0).Checked = True Then ''AS On By Bill...
            SqlStr = SqlStr & vbCrLf & " AND TRN.BILLDATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND NVL(TRN.EXPDATE,TRN.BILLDATE) <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND BALANCE<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY CH.SUPP_CUST_Code, TRN.PARTYNAME, " & mDueDate & ", CH.PAYMENT_MODE, TO_NUMBER(ACTIVITY)"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.BALANCE * DECODE(DC,'DR',1,-1))<0 "


        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.PARTYNAME"


        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        cntSNo = 1
        With SprdView
            Do While RsTemp.EOF = False

                .Row = cntRow

                .Col = ColSRNo
                .Text = VB6.Format(cntSNo)

                .Col = ColPartyCode
                .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mPartyCode = Trim(.Text)

                .Col = ColPartyName
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("PARTYNAME").Value), "", RsTemp.Fields("PARTYNAME").Value))

                .Col = ColPaymentTerms
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Due_Days").Value), "", RsTemp.Fields("Due_Days").Value), "0")
                mPaymentTerm = Val(.Text)

                .Col = ColOpeningBal
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OPBal").Value), "", RsTemp.Fields("OPBal").Value), "0.00")

                .Col = ColClosingBalance
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CLBal").Value), "", RsTemp.Fields("CLBal").Value), "0.00")

                .Col = ColPaidinMonth
                .Text = 4   ''VB6.Format(IIf(IsDbNull(RsTemp.Fields("ACTIVITY").Value), "", RsTemp.Fields("ACTIVITY").Value), "0")
                mChqFequency = Val(.Text)

                .Col = ColPaymentMode
                mPaymentMode = IIf(IsDbNull(RsTemp.Fields("PAYMENT_MODE").Value), "", RsTemp.Fields("PAYMENT_MODE").Value)
                If mPaymentMode = "1" Then
                    .Text = "CHEQUE"
                ElseIf mPaymentMode = "2" Then
                    .Text = "HUNDI"
                ElseIf mPaymentMode = "3" Then
                    .Text = "LC"
                ElseIf mPaymentMode = "4" Then
                    .Text = "MSME"
                ElseIf mPaymentMode = "5" Then
                    .Text = "PDC"
                ElseIf mPaymentMode = "6" Then
                    .Text = "DISC-YES"
                ElseIf mPaymentMode = "7" Then
                    .Text = "DISC-CASH"
                ElseIf mPaymentMode = "8" Then
                    .Text = "DISC-TCFL"
                ElseIf mPaymentMode = "9" Then
                    .Text = "UGRO"
                Else
                    .Text = ""
                End If

                mIstTermAmount = 0
                mIIstTermAmount = 0
                mIIIstTermAmount = 0
                mIVstTermAmount = 0
                Call FillDataInSprd(mPartyCode, mPaymentTerm, mChqFequency, mIstTermAmount, mIIstTermAmount, mIIIstTermAmount, mIVstTermAmount)

                .Row = cntRow
                .Col = ColDue1
                .Text = VB6.Format(mIstTermAmount, "0.00")

                .Col = ColDue2
                .Text = VB6.Format(mIIstTermAmount, "0.00")

                .Col = ColDue3
                .Text = VB6.Format(mIIIstTermAmount, "0.00")

                .Col = ColDue4
                .Text = VB6.Format(mIVstTermAmount, "0.00")

                .Col = ColTotalDue
                .Text = VB6.Format(mIstTermAmount + mIIstTermAmount + mIIIstTermAmount + mIVstTermAmount, "0.00")

                If OptSumDet(0).Checked = True Then
                    Call FillDetailDataInSprd(mPartyCode, IIf(IsDbNull(RsTemp.Fields("PARTYNAME").Value), "", RsTemp.Fields("PARTYNAME").Value), mPaymentTerm, mChqFequency, cntRow, cntSNo)
                End If

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                    cntSNo = cntSNo + 1
                    Call FormatSprdView(cntRow)
                End If
            Loop
        End With
        If OptSumDet(0).Checked = True Then
            GroupBySpread(ColPicMain)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub ViewOutsAdhocSummary()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mPartyCode As String
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDueDate As String


        Dim mOPDate As String
        Dim mDueDate1 As String
        Dim mDueDate2 As String
        Dim mDueDate3 As String
        Dim mDueDate4 As String
        Dim mLastDate As String


        'Dim mPartyCode As String
        Dim mPaymentTerm As Integer
        Dim mChqFequency As Integer

        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double

        Dim mDivisionCode As Double
        Dim cntRow As Integer
        Dim cntSNo As Integer
        Dim mPaymentMode As String

        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate1 = Val(txtDays1.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate2 = Val(txtDays2.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate3 = Val(txtDays3.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDueDate4 = Val(txtDays4.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        mDueDate = "DECODE(ADHOC_PAY_TERMS,0,DECODE(TO_DAYS,NULL,0,TO_DAYS),ADHOC_PAY_TERMS)"

        SqlStr = " Select  '','', '', CH.SUPP_CUST_Code, TRN.PARTYNAME," & vbCrLf & " " & mDueDate & " As Due_Days, CH.PAYMENT_MODE, "

        '    SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(BALANCE * DECODE(DC,'DR',1,-1)))) AS BALANCE, "

        If optAsOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TRN.BILLDATE < TO_DATE('" & VB6.Format(mOPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS OPBal,"
            SqlStr = SqlStr & vbCrLf & " 0, 0, 0, 0, 0, "
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TRN.BILLDATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS CLBal,"
            SqlStr = SqlStr & vbCrLf & " TO_NUMBER(ACTIVITY) AS ACTIVITY"
        Else
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TRN.EXPDATE < TO_DATE('" & VB6.Format(mOPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS OPBal,"
            SqlStr = SqlStr & vbCrLf & " 0, 0, 0, 0, 0, "
            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TRN.EXPDATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN BALANCE * DECODE(DC,'DR',-1,1) ELSE 0 END)AS CLBal,"
            SqlStr = SqlStr & vbCrLf & " TO_NUMBER(ACTIVITY) AS ACTIVITY,'0'"
        End If

        SqlStr = SqlStr & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN, FIN_SUPP_CUST_MST CH, FIN_PAYTERM_MST PMST "

        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code=CH.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=CH.SUPP_CUST_Code " & vbCrLf & " AND CH.Company_Code=PMST.Company_Code(+) " & vbCrLf & " AND CH.PAYMENT_CODE=PMST.PAY_TERM_CODE(+) "

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If cboChqsInMonth.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND TO_NUMBER(ACTIVITY)=" & Val(cboChqsInMonth.Text) & ""
        End If

        If cboPaymentMode.SelectedIndex > 0 Then
            If cboPaymentMode.SelectedIndex = 9 Then
                SqlStr = SqlStr & vbCrLf & " AND (CH.PAYMENT_MODE IS NULL OR CH.PAYMENT_MODE='')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND CH.PAYMENT_MODE='" & VB.Left(Trim(cboPaymentMode.Text), 1) & "'"
            End If
        End If

        If Val(txtPaymentDays.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND " & mDueDate & "=" & Val(txtPaymentDays.Text) & ""
        End If

        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & " AND TRN.SUPP_CUST_TYPE ='S'"
        Else
            SqlStr = SqlStr & " AND TRN.SUPP_CUST_TYPE ='C'"
        End If

        If optParty(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PARTYNAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"
        End If

        If optAsOn(0).Checked = True Then ''AS On By Bill...
            SqlStr = SqlStr & vbCrLf & " AND TRN.BILLDATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.EXPDATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND BALANCE<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY CH.SUPP_CUST_Code, TRN.PARTYNAME, " & mDueDate & ",CH.PAYMENT_MODE, TO_NUMBER(ACTIVITY)"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.BALANCE * DECODE(DC,'DR',1,-1))<0 "


        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.PARTYNAME"


        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        cntSNo = 1
        With SprdView
            Do While RsTemp.EOF = False

                .Row = cntRow

                .Col = ColSRNo
                .Text = VB6.Format(cntSNo)

                .Col = ColPartyCode
                .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mPartyCode = Trim(.Text)

                .Col = ColPartyName
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("PARTYNAME").Value), "", RsTemp.Fields("PARTYNAME").Value))

                .Col = ColPaymentTerms
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Due_Days").Value), "", RsTemp.Fields("Due_Days").Value), "0")
                mPaymentTerm = Val(.Text)

                .Col = ColOpeningBal
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OPBal").Value), "", RsTemp.Fields("OPBal").Value), "0.00")

                .Col = ColClosingBalance
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CLBal").Value), "", RsTemp.Fields("CLBal").Value), "0.00")

                .Col = ColPaidinMonth
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ACTIVITY").Value), "", RsTemp.Fields("ACTIVITY").Value), "0")
                mChqFequency = Val(.Text)

                .Col = ColPaymentMode
                mPaymentMode = IIf(IsDbNull(RsTemp.Fields("PAYMENT_MODE").Value), "", RsTemp.Fields("PAYMENT_MODE").Value)
                If mPaymentMode = "1" Then
                    .Text = "CHEQUE"
                ElseIf mPaymentMode = "2" Then
                    .Text = "HUNDI"
                ElseIf mPaymentMode = "3" Then
                    .Text = "LC"
                ElseIf mPaymentMode = "4" Then
                    .Text = "MSME"
                ElseIf mPaymentMode = "5" Then
                    .Text = "PDC"
                ElseIf mPaymentMode = "6" Then
                    .Text = "DISC-YES"
                ElseIf mPaymentMode = "7" Then
                    .Text = "DISC-CASH"
                ElseIf mPaymentMode = "8" Then
                    .Text = "DISC-TCFL"
                ElseIf mPaymentMode = "9" Then
                    .Text = "UGRO"
                ElseIf mPaymentMode = "A" Then
                    .Text = "ONLINE"
                Else
                    .Text = ""
                End If

                mIstTermAmount = 0
                mIIstTermAmount = 0
                mIIIstTermAmount = 0
                mIVstTermAmount = 0
                Call FillDataInSprd(mPartyCode, mPaymentTerm, mChqFequency, mIstTermAmount, mIIstTermAmount, mIIIstTermAmount, mIVstTermAmount)

                .Row = cntRow
                .Col = ColDue1
                .Text = VB6.Format(mIstTermAmount, "0.00")

                .Col = ColDue2
                .Text = VB6.Format(mIIstTermAmount, "0.00")

                .Col = ColDue3
                .Text = VB6.Format(mIIIstTermAmount, "0.00")

                .Col = ColDue4
                .Text = VB6.Format(mIVstTermAmount, "0.00")

                .Col = ColTotalDue
                .Text = VB6.Format(mIstTermAmount + mIIstTermAmount + mIIIstTermAmount + mIVstTermAmount, "0.00")

                If OptSumDet(0).Checked = True Then
                    Call FillDetailDataInSprd(mPartyCode, IIf(IsDbNull(RsTemp.Fields("PARTYNAME").Value), "", RsTemp.Fields("PARTYNAME").Value), mPaymentTerm, mChqFequency, cntRow, cntSNo)
                End If

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                    cntSNo = cntSNo + 1
                    Call FormatSprdView(cntRow)
                End If
            Loop
        End With
        If OptSumDet(0).Checked = True Then
            GroupBySpread(ColPicMain)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub GroupBySpread(ByRef Col As Integer)
        'Group the data by the specified column
        Dim I As Short
        Dim currentrow As Integer
        Dim lastid As String
        Dim prevtext As Object
        Dim lastheaderrow As Integer
        Dim ret As Boolean
        Dim Currentid As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SprdView.ReDraw = False
        BoldHeader(Col)

        '    SprdView.MaxCols = SprdView.MaxCols + 2
        'Insert 2 columns at beginning
        For I = 1 To 2
            '        SprdView.InsertCols i, 1

            'Change col width
            SprdView.set_ColWidth(I, 2)
        Next I

        SprdView.Col = ColPicMain
        SprdView.Row = -1
        SprdView.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray

        'Init variables
        lastheaderrow = 0
        currentrow = 1
        lastid = "  "

        While currentrow <= SprdView.DataRowCnt

            SprdView.Row = currentrow
            SprdView.Col = ColSRNo
            Currentid = UCase(Trim(SprdView.Text))
            If InStr(1, Currentid, ".") > 0 Then
                Currentid = VB.Left(Currentid, InStr(1, Currentid, ".") - 1)
            End If
            If Currentid <> lastid Then
                '            Call SprdView_Click(1, currentrow)

                If lastheaderrow <> 0 Then
                    SprdView.SetRowItemData(lastheaderrow, (SprdView.Row - lastheaderrow - 1))
                    '                mRecordCount = SprdView.GetRowItemData(lastheaderrow)
                End If

                lastid = UCase(Trim(SprdView.Text))
                If InStr(1, lastid, ".") > 0 Then
                    lastid = VB.Left(lastid, InStr(1, lastid, ".") - 1)
                End If

                lastheaderrow = currentrow

                'Insert a new header row
                InsertHeaderRow(currentrow, 0)
            Else
                MakePictureCellType((SprdView.Row), ColPicSub)
                SprdView.Col = ColPicSub
                SprdView.TypePictPicture = minuspict
                SprdView.SetCellBorder(ColPicSub, SprdView.Row, ColPicSub, SprdView.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
            End If
            SprdView.Row = SprdView.Row + 1
            currentrow = currentrow + 1

        End While

        'Display last read data
        SprdView.SetRowItemData(lastheaderrow, (SprdView.Row - lastheaderrow))
        SprdView.MaxRows = SprdView.DataRowCnt
        SprdView.SetActiveCell(1, 1)

        'Paint Spread
        SprdView.ReDraw = True

        'Update displays
        System.Windows.Forms.Application.DoEvents()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub MakePictureCellType(ByRef Row As Integer, ByRef Col As Short)
        'Define specified cell as type PICTURE

        Exit Sub
        SprdView.Col = Col
        SprdView.Row = Row

        SprdView.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
        SprdView.TypePictCenter = True
        SprdView.TypePictMaintainScale = False
        SprdView.TypePictStretch = False

    End Sub
    Private Sub BoldHeader(ByRef Col As Integer)
        'Reset the header bolds and make the sort col bold

        'Change font for visual cue to what column sorting on
        'Reset all header fonts
        With SprdView
            .Row = 0
            .Col = -1
            .Font = VB6.FontChangeBold(.Font, False)

            'Bold the specified column
            .Row = 0
            .Col = Col
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Private Sub InsertHeaderRow(ByRef RowNum As Integer, ByRef pRecordCount As Integer)
        'Insert a header row at the specifed location

        '    SprdView.InsertRows rownum, 1

        SprdView.Col = -1
        SprdView.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray
        SprdView.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
        SprdView.Font = VB6.FontChangeBold(SprdView.Font, True)

        MakePictureCellType(RowNum, ColPicMain)

        SprdView.Col = ColPicMain
        SprdView.TypePictPicture = minuspict
        SprdView.Col = ColPicSub
        SprdView.Text = ""

        'Add picture state values
        SprdView.Col = ColFlag
        SprdView.Text = "0"

        'Add Border

        SprdView.SetCellBorder(ColPicMain, RowNum, SprdView.MaxCols, RowNum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

    End Sub

    Private Function FillDataInSprd(ByRef mPartyCode As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef mIstTermAmount As Double, ByRef mIIstTermAmount As Double, ByRef mIIIstTermAmount As Double, ByRef mIVstTermAmount As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        'Dim mDueDate1 As String
        'Dim mDueDate2 As String
        'Dim mDueDate3 As String
        'Dim mDueDate4 As String
        'Dim mLastDate As String
        '
        '
        'Dim mPartyCode As String
        'Dim mPaymentTerm As Double
        'Dim mChqFequency As Double
        '
        'Dim CntRow As Long
        Dim mMonth As Integer
        'Dim mIstTermAmount As Double
        'Dim mIIstTermAmount As Double
        'Dim mIIIstTermAmount As Double
        'Dim mIVstTermAmount As Double
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer

        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mBalAmtABSStr As String
        Dim mTrnTypeStr As String

        SqlStr = " SELECT "

        'If OptShow(0).Checked = True Then
        SqlStr = SqlStr & vbCrLf & SqlCond(mPartyCode, "", mPaymentTerm, mChqFequency, 0, 0)
        'Else
        '    SqlStr = SqlStr & vbCrLf & SqlCondADHOC(mPartyCode, "", mPaymentTerm, mChqFequency, 0, 0)
        'End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mIstTermAmount = mIstTermAmount + IIf(IsDbNull(RsTemp.Fields("Day1").Value), 0, RsTemp.Fields("Day1").Value)
                mIIstTermAmount = mIIstTermAmount + IIf(IsDbNull(RsTemp.Fields("Day2").Value), 0, RsTemp.Fields("Day2").Value)
                mIIIstTermAmount = mIIIstTermAmount + IIf(IsDbNull(RsTemp.Fields("Day3").Value), 0, RsTemp.Fields("Day3").Value)
                mIVstTermAmount = mIVstTermAmount + IIf(IsDbNull(RsTemp.Fields("Day4").Value), 0, RsTemp.Fields("Day4").Value)
                RsTemp.MoveNext()
            Loop
        End If

        mIIstTermAmount = IIf(mIIstTermAmount = 0, 0, mIIstTermAmount - mIstTermAmount)
        mIIIstTermAmount = IIf(mIIIstTermAmount = 0, 0, mIIIstTermAmount - mIstTermAmount - mIIstTermAmount)
        mIVstTermAmount = IIf(mIVstTermAmount = 0, 0, mIVstTermAmount - mIstTermAmount - mIIstTermAmount - mIIIstTermAmount)

        FillDataInSprd = True

        Exit Function
ERR1:
        '    Resume
        FillDataInSprd = False
        MsgInformation(Err.Description)
    End Function
    Private Function FillDataInSprdOld(ByRef mPartyCode As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef mIstTermAmount As Double, ByRef mIIstTermAmount As Double, ByRef mIIIstTermAmount As Double, ByRef mIVstTermAmount As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        'Dim mDueDate1 As String
        'Dim mDueDate2 As String
        'Dim mDueDate3 As String
        'Dim mDueDate4 As String
        'Dim mLastDate As String
        '
        '
        'Dim mPartyCode As String
        'Dim mPaymentTerm As Double
        'Dim mChqFequency As Double
        '
        'Dim CntRow As Long
        Dim mMonth As Integer
        'Dim mIstTermAmount As Double
        'Dim mIIstTermAmount As Double
        'Dim mIIIstTermAmount As Double
        'Dim mIVstTermAmount As Double
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer

        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String


        mDAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mCAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mBalAmtStr = "" & mDAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " + " & mCNAmtStr & " + " & mCAmtStr & ""


        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        '    mLastDate = MainClass.LastDay(Month(lblRunDate.text), Year(lblRunDate.text)) & "/" & vb6.Format(lblRunDate.text, "MM/YYYY")
        '    mDueDate1 = Val(txtDays1.Text) & "/" & vb6.Format(lblRunDate.text, "MM/YYYY")
        '    mDueDate2 = Val(txtDays2.Text) & "/" & vb6.Format(lblRunDate.text, "MM/YYYY")
        '    mDueDate3 = Val(txtDays3.Text) & "/" & vb6.Format(lblRunDate.text, "MM/YYYY")
        '    mDueDate4 = Val(txtDays4.Text) & "/" & vb6.Format(lblRunDate.text, "MM/YYYY")
        '
        '    mDueDate = "DECODE(TO_DAYS,NULL,0,TO_DAYS)"
        '    mChqFequency = 2
        '    mPaymentTerm = 75
        mMonth = Int(mPaymentTerm / 30) - 1
        mMonth = IIf(mMonth < 0, 0, mMonth)
        mChqFequency = IIf(mChqFequency = 0, 1, mChqFequency)
        mDate = CStr((Int(mPaymentTerm / (mChqFequency * 2))) Mod 30)

        mPayIstWeek = False
        mPayIIndWeek = False
        mPayIIIrdWeek = False
        mPayIVthWeek = False



        If mChqFequency = 1 Then
            If CDbl(mDate) > Val(txtDays1.Text) Then
                If CDbl(mDate) > Val(txtDays2.Text) Then
                    If CDbl(mDate) > Val(txtDays3.Text) Then
                        mPayIVthWeek = True
                    Else
                        mPayIIIrdWeek = True
                    End If
                Else
                    mPayIIndWeek = True
                End If
            Else
                mPayIstWeek = True
            End If

            mAddDays1 = 0
            mAddDays2 = 0
            mAddDays3 = 0
            mAddDays4 = 0
        ElseIf mChqFequency = 2 Then

            If CDbl(mDate) > Val(txtDays1.Text) Then
                If CDbl(mDate) > Val(txtDays2.Text) Then
                    If CDbl(mDate) > Val(txtDays3.Text) Then
                        mPayIVthWeek = True
                    Else
                        mPayIIIrdWeek = True
                    End If
                Else
                    mPayIIndWeek = True
                End If
            Else
                mPayIstWeek = True
            End If

            If mPayIstWeek = True Then
                mAddDays1 = 15
                mAddDays3 = 0
                mPayIIIrdWeek = True
            ElseIf mPayIIndWeek = True Then
                mAddDays2 = 15
                mAddDays4 = 0
                mPayIVthWeek = True
            ElseIf mPayIIIrdWeek = True Then
                mAddDays3 = 0
                mAddDays1 = 15
                mPayIstWeek = True
            ElseIf mPayIVthWeek = True Then
                mAddDays4 = 0
                mAddDays2 = 15
                mPayIIndWeek = True
            End If


        ElseIf mChqFequency = 3 Then
            mPayIstWeek = False
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays1 = 30
            mAddDays2 = 20
            mAddDays3 = 10
            mAddDays4 = 0
        ElseIf mChqFequency = 4 Then
            mPayIstWeek = True
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays1 = 21
            mAddDays2 = 14
            mAddDays3 = 7
            mAddDays4 = 0
        End If

        SqlStr = " SELECT "
        If optAsOn(0).Checked = True Then
            If mPayIstWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays1 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day1,"
            End If

            If mPayIIndWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays2 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day2,"
            End If

            If mPayIIIrdWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays3 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day3,"
            End If

            If mPayIVthWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays4 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day4"
            End If
        Else
            If mPayIstWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays1 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day1,"
            End If

            If mPayIIndWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays2 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day2,"
            End If

            If mPayIIIrdWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays3 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day3,"
            End If

            If mPayIVthWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays4 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN AMOUNT* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day4"
            End If
        End If
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN" ''vw_FIN_PAYMENT_ADV

        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"

        '    SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.AMOUNT * DECODE(DC,'DR',1,-1))<0 "

        SqlStr = SqlStr & vbCrLf & " HAVING " & mBalAmtStr & "<0 "

        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlStr = SqlStr & vbCrLf & " AND CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & "),'YYYYMM') ELSE TO_CHAR(BILLDATE,'DD-MMM-YYYY') END < CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN '" & vb6.Format(mOPDate, "YYYYMM") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & "),'YYYYMM')  ELSE TO_CHAR(EXPDATE,'DD-MMM-YYYY') END < CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN '" & vb6.Format(mOPDate, "YYYYMM") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END"
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day1").Value), 0, RsTemp.Fields("Day1").Value)
            mIIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day2").Value), 0, RsTemp.Fields("Day2").Value)
            mIIstTermAmount = IIf(mIIstTermAmount = 0, 0, mIIstTermAmount - mIstTermAmount)

            mIIIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day3").Value), 0, RsTemp.Fields("Day3").Value)
            mIIIstTermAmount = IIf(mIIIstTermAmount = 0, 0, mIIIstTermAmount - mIstTermAmount - mIIstTermAmount)

            mIVstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day4").Value), 0, RsTemp.Fields("Day4").Value)
            mIVstTermAmount = IIf(mIVstTermAmount = 0, 0, mIVstTermAmount - mIstTermAmount - mIIstTermAmount - mIIIstTermAmount)
        End If

        FillDataInSprdOld = True

        Exit Function
ERR1:
        '    Resume
        FillDataInSprdOld = False
        MsgInformation(Err.Description)
    End Function
    Private Function FillDetailDataInSprd(ByRef mPartyCode As String, ByRef mPartyName As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef cntRow As Integer, ByRef mSRNo As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        Dim mMonth As Integer
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer
        Dim mRow As Integer
        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDateCond1 As String
        Dim mDateCond2 As String
        Dim mDateCond3 As String
        Dim mDateCond4 As String

        If optAsOn(0).Checked = True Then
            SqlStr = " SELECT BILLNO, BILLDATE,"
        Else
            SqlStr = " SELECT BILLNO, EXPDATE AS BILLDATE,"
        End If

        'If OptShow(0).Checked = True Then
        SqlStr = SqlStr & vbCrLf & SqlCond(mPartyCode, mPartyName, mPaymentTerm, mChqFequency, cntRow, mSRNo)
        'Else
        '    SqlStr = SqlStr & vbCrLf & SqlCondADHOC(mPartyCode, mPartyName, mPaymentTerm, mChqFequency, cntRow, mSRNo)
        'End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            cntRow = cntRow + 1
            SprdView.MaxRows = cntRow
            Call FormatSprdView(cntRow)
            mRow = 1
            Do While RsTemp.EOF = False
                mIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day1").Value), 0, RsTemp.Fields("Day1").Value)
                mIIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day2").Value), 0, RsTemp.Fields("Day2").Value)
                mIIstTermAmount = IIf(mIIstTermAmount = 0, 0, mIIstTermAmount - mIstTermAmount)

                mIIIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day3").Value), 0, RsTemp.Fields("Day3").Value)
                mIIIstTermAmount = IIf(mIIIstTermAmount = 0, 0, mIIIstTermAmount - mIstTermAmount - mIIstTermAmount)

                mIVstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day4").Value), 0, RsTemp.Fields("Day4").Value)
                mIVstTermAmount = IIf(mIVstTermAmount = 0, 0, mIVstTermAmount - mIstTermAmount - mIIstTermAmount - mIIIstTermAmount)

                With SprdView
                    .Row = cntRow
                    .Col = ColSRNo
                    .Text = mSRNo & "." & mRow

                    .Col = ColPartyCode
                    .Text = mPartyCode

                    .Col = ColPartyName
                    .Text = mPartyName

                    .Col = ColBillNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                    .Col = ColBillDate
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")

                    .Col = ColDue1
                    .Text = VB6.Format(mIstTermAmount, "0.00")

                    .Col = ColDue2
                    .Text = VB6.Format(mIIstTermAmount, "0.00")

                    .Col = ColDue3
                    .Text = VB6.Format(mIIIstTermAmount, "0.00")

                    .Col = ColDue4
                    .Text = VB6.Format(mIVstTermAmount, "0.00")

                    .Col = ColTotalDue
                    .Text = VB6.Format(mIstTermAmount + mIIstTermAmount + mIIIstTermAmount + mIVstTermAmount, "0.00")
                End With
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                    SprdView.MaxRows = cntRow
                End If
                Call FormatSprdView(cntRow)
                mRow = mRow + 1
                mIstTermAmount = 0
                mIIstTermAmount = 0
                mIIIstTermAmount = 0
                mIVstTermAmount = 0
            Loop
        End If

        FillDetailDataInSprd = True

        Exit Function
ERR1:
        '    Resume
        FillDetailDataInSprd = False
        MsgInformation(Err.Description)
    End Function

    Private Function SqlCond(ByRef mPartyCode As String, ByRef mPartyName As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef cntRow As Integer, ByRef mSRNo As Integer) As String

        On Error GoTo ERR1
        'Dim SqlCond As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        Dim mMonth As Integer
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer
        Dim mRow As Integer
        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDateCond1 As String
        Dim mDateCond2 As String
        Dim mDateCond3 As String
        Dim mDateCond4 As String
        Dim mDateCond As String
        Dim mAddDays As Integer
        Dim mAddFloor As Integer
        Dim mLastDate As String

        Dim mDivisionCode As Double

        mLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mBalAmtStr = "BALANCE"

        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mMonth = mPaymentTerm
        'mAddFloor = 0
        'If mChqFequency >= 3 Then
        '    mMonth = Int(mPaymentTerm / 30)
        'Else
        '    If mPaymentTerm Mod 30 = 0 Then
        '        mMonth = Int(mPaymentTerm / 30) - 1
        '        mMonth = IIf(mMonth < 0, 0, mMonth)
        '        mChqFequency = IIf(mChqFequency = 0, 1, mChqFequency)
        '        mDate = CStr(Int((mPaymentTerm - (mMonth * 30)) / (mChqFequency))) ''Mod 30
        '    Else
        '        mMonth = Int(mPaymentTerm / 30)

        '        mMonth = IIf(mMonth < 0, 0, mMonth)
        '        mChqFequency = IIf(mChqFequency = 0, 1, mChqFequency)
        '        mDate = CStr(Int((mPaymentTerm - (mMonth * 30)) / (mChqFequency)))
        '    End If
        'End If


        mPayIstWeek = True
        mPayIIndWeek = True
        mPayIIIrdWeek = True
        mPayIVthWeek = True



        'If mChqFequency = 1 Then
        '    If CDbl(mDate) - 3 > Val(txtDays1.Text) Then
        '        If CDbl(mDate) - 3 > Val(txtDays2.Text) Then
        '            If CDbl(mDate) - 3 > Val(txtDays3.Text) Then
        '                mPayIVthWeek = True
        '            Else
        '                mPayIIIrdWeek = True
        '            End If
        '        Else
        '            mPayIIndWeek = True
        '        End If
        '    Else
        '        mPayIstWeek = True
        '    End If

        '    mAddDays1 = 0
        '    mAddDays2 = 0
        '    mAddDays3 = 0
        '    mAddDays4 = 0
        '    mAddDays = 0
        'ElseIf mChqFequency = 2 Then

        '    If CDbl(mDate) - 3 > Val(txtDays1.Text) Then
        '        If CDbl(mDate) - 3 > Val(txtDays2.Text) Then
        '            If CDbl(mDate) - 3 > Val(txtDays3.Text) Then
        '                mPayIVthWeek = True
        '            Else
        '                mPayIIIrdWeek = True
        '            End If
        '        Else
        '            mPayIIndWeek = True
        '        End If
        '    Else
        '        mPayIstWeek = True
        '    End If

        '    If mPayIstWeek = True Then
        '        mAddDays1 = 0 - mAddFloor
        '        mAddDays3 = -15 - mAddFloor
        '        mAddDays = -15 - mAddFloor
        '        mPayIIIrdWeek = True
        '    ElseIf mPayIIndWeek = True Then
        '        mAddDays2 = 0 - mAddFloor
        '        mAddDays4 = -15 - mAddFloor
        '        mAddDays = -15 - mAddFloor
        '        mPayIVthWeek = True
        '    ElseIf mPayIIIrdWeek = True Then
        '        mAddDays3 = 0 - mAddFloor '0
        '        mAddDays1 = 15 - mAddFloor ''15
        '        mPayIstWeek = True
        '        mAddDays = 0 - mAddFloor
        '    ElseIf mPayIVthWeek = True Then
        '        mAddDays4 = 0 - mAddFloor
        '        mAddDays2 = 15 - mAddFloor
        '        mPayIIndWeek = True
        '        mAddDays = 0 - mAddFloor
        '    End If


        'ElseIf mChqFequency = 3 Then

        '    mPayIstWeek = False
        '    mPayIIndWeek = True
        '    mPayIIIrdWeek = True
        '    mPayIVthWeek = True
        '    mAddDays2 = -15
        '    mAddDays3 = -25
        '    mAddDays4 = -35
        '    mAddDays = -35

        'ElseIf mChqFequency = 4 Then
        '    mPayIstWeek = True
        '    mPayIIndWeek = True
        '    mPayIIIrdWeek = True
        '    mPayIVthWeek = True
        '    mAddDays1 = -7
        '    mAddDays2 = -14
        '    mAddDays3 = -21
        '    mAddDays4 = -30
        '    mAddDays = -30
        'End If

        mAddDays1 = Val(txtDays1.Text) * -1
        mAddDays2 = Val(txtDays2.Text) * -1
        mAddDays3 = Val(txtDays3.Text) * -1
        mAddDays4 = Val(txtDays4.Text) * -1
        mAddDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) * -1

        If optAsOn(0).Checked = True Then
            mDateCond1 = "TO_CHAR(TRN.BILLDATE  + " & mMonth & " + " & mAddDays1 & " ,'YYYYMMDD')"
            mDateCond2 = "TO_CHAR(TRN.BILLDATE  + " & mMonth & " + " & mAddDays2 & " ,'YYYYMMDD')"
            mDateCond3 = "TO_CHAR(TRN.BILLDATE  + " & mMonth & " + " & mAddDays3 & " ,'YYYYMMDD')"
            mDateCond4 = "TO_CHAR(TRN.BILLDATE  + " & mMonth & " + " & mAddDays4 & " ,'YYYYMMDD')"
            mDateCond = " TO_CHAR(TRN.BILLDATE  + " & mMonth & "+ " & mAddDays & " ,'YYYYMMDD')"
            '        mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"
        Else
            mDateCond1 = "TO_CHAR(TRN.EXPDATE  + " & mMonth & " + " & mAddDays1 & " ,'YYYYMMDD')"
            mDateCond2 = "TO_CHAR(TRN.EXPDATE  + " & mMonth & " + " & mAddDays2 & " ,'YYYYMMDD')"
            mDateCond3 = "TO_CHAR(TRN.EXPDATE  + " & mMonth & " + " & mAddDays3 & " ,'YYYYMMDD')"
            mDateCond4 = "TO_CHAR(TRN.EXPDATE  + " & mMonth & " + " & mAddDays4 & " ,'YYYYMMDD')"

            '        mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"
            mDateCond = "TO_CHAR(TRN.EXPDATE  + " & mMonth & "+ " & mAddDays & " ,'YYYYMMDD')"
        End If
        SqlCond = ""


        If mPayIstWeek = True Then
            SqlCond = SqlCond & " SUM(CASE WHEN " & mDateCond1 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
        Else
            SqlCond = SqlCond & " 0 AS Day1,"
        End If

        If mPayIIndWeek = True Then
            SqlCond = SqlCond & vbCrLf & " SUM(CASE WHEN " & mDateCond2 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
        Else
            SqlCond = SqlCond & vbCrLf & " 0 AS Day2,"
        End If

        If mPayIIIrdWeek = True Then
            SqlCond = SqlCond & vbCrLf & " SUM(CASE WHEN " & mDateCond3 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
        Else
            SqlCond = SqlCond & vbCrLf & " 0 AS Day3,"
        End If

        If mPayIVthWeek = True Then
            SqlCond = SqlCond & vbCrLf & " SUM(CASE WHEN " & mDateCond4 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
        Else
            SqlCond = SqlCond & vbCrLf & " 0 AS Day4"
        End If



        SqlCond = SqlCond & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN" ''FIN_POSTED_TRN

        SqlCond = SqlCond & vbCrLf _
            & " WHERE TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlCond = SqlCond & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If


        SqlCond = SqlCond & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"

        '    SqlCond = SqlCond & vbCrLf & " AND " & mDateCond & " < CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN '" & vb6.Format(mOPDate, "YYYYMMDD") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "YYYYMMDD") & "' END"
        SqlCond = SqlCond & vbCrLf & " AND " & mDateCond & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "'" ''

        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlCond = SqlCond & vbCrLf & " AND TRN.BILLDATE <= '" & vb6.Format(mLastDate, "DD-MMM-YYYY") & "'"
        '    Else
        '        SqlCond = SqlCond & vbCrLf & " AND TRN.EXPDATE <= '" & vb6.Format(mLastDate, "DD-MMM-YYYY") & "'"
        '    End If

        If optAsOn(0).Checked = True Then
            SqlCond = SqlCond & vbCrLf & " GROUP BY BILLNO, BILLDATE"
            SqlCond = SqlCond & vbCrLf & " HAVING SUM(" & mBalAmtStr & ")<>0 "
            SqlCond = SqlCond & vbCrLf & " ORDER BY BILLDATE,BILLNO"
        Else
            SqlCond = SqlCond & vbCrLf & " GROUP BY BILLNO, EXPDATE, NVL(EXPDATE,BILLDATE)"
            SqlCond = SqlCond & vbCrLf & " HAVING SUM(" & mBalAmtStr & ")<>0 "
            SqlCond = SqlCond & vbCrLf & " ORDER BY EXPDATE, NVL(EXPDATE,BILLDATE)"
        End If
        Exit Function
ERR1:
        '    Resume
        SqlCond = ""
        MsgInformation(Err.Description)
    End Function
    Private Function SqlCondOLD(ByRef mPartyCode As String, ByRef mPartyName As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef cntRow As Integer, ByRef mSRNo As Integer) As String

        On Error GoTo ERR1
        'Dim SqlCondOLD As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        Dim mMonth As Integer
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer
        Dim mRow As Integer
        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDateCond1 As String
        Dim mDateCond2 As String
        Dim mDateCond3 As String
        Dim mDateCond4 As String
        Dim mDateCond As String
        Dim mAddDays As Integer
        Dim mAddFloor As Integer
        Dim mLastDate As String

        Dim mDivisionCode As Double

        mLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mBalAmtStr = "BALANCE"

        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mAddFloor = 0
        If mChqFequency >= 3 Then
            mMonth = Int(mPaymentTerm / 30)
        Else
            If mPaymentTerm Mod 30 = 0 Then
                mMonth = Int(mPaymentTerm / 30) - 1
                mMonth = IIf(mMonth < 0, 0, mMonth)
                mChqFequency = IIf(mChqFequency = 0, 1, mChqFequency)
                mDate = CStr(Int((mPaymentTerm - (mMonth * 30)) / (mChqFequency))) ''Mod 30
            Else
                mMonth = Int(mPaymentTerm / 30)

                mMonth = IIf(mMonth < 0, 0, mMonth)
                mChqFequency = IIf(mChqFequency = 0, 1, mChqFequency)
                mDate = CStr(Int((mPaymentTerm - (mMonth * 30)) / (mChqFequency)))
            End If
        End If


        mPayIstWeek = False
        mPayIIndWeek = False
        mPayIIIrdWeek = False
        mPayIVthWeek = False



        If mChqFequency = 1 Then
            If CDbl(mDate) - 3 > Val(txtDays1.Text) Then
                If CDbl(mDate) - 3 > Val(txtDays2.Text) Then
                    If CDbl(mDate) - 3 > Val(txtDays3.Text) Then
                        mPayIVthWeek = True
                    Else
                        mPayIIIrdWeek = True
                    End If
                Else
                    mPayIIndWeek = True
                End If
            Else
                mPayIstWeek = True
            End If

            mAddDays1 = 0
            mAddDays2 = 0
            mAddDays3 = 0
            mAddDays4 = 0
            mAddDays = 0
        ElseIf mChqFequency = 2 Then

            If CDbl(mDate) - 3 > Val(txtDays1.Text) Then
                If CDbl(mDate) - 3 > Val(txtDays2.Text) Then
                    If CDbl(mDate) - 3 > Val(txtDays3.Text) Then
                        mPayIVthWeek = True
                    Else
                        mPayIIIrdWeek = True
                    End If
                Else
                    mPayIIndWeek = True
                End If
            Else
                mPayIstWeek = True
            End If

            If mPayIstWeek = True Then
                mAddDays1 = 0 - mAddFloor
                mAddDays3 = -15 - mAddFloor
                mAddDays = -15 - mAddFloor
                mPayIIIrdWeek = True
            ElseIf mPayIIndWeek = True Then
                mAddDays2 = 0 - mAddFloor
                mAddDays4 = -15 - mAddFloor
                mAddDays = -15 - mAddFloor
                mPayIVthWeek = True
            ElseIf mPayIIIrdWeek = True Then
                mAddDays3 = 0 - mAddFloor '0
                mAddDays1 = 15 - mAddFloor ''15
                mPayIstWeek = True
                mAddDays = 0 - mAddFloor
            ElseIf mPayIVthWeek = True Then
                mAddDays4 = 0 - mAddFloor
                mAddDays2 = 15 - mAddFloor
                mPayIIndWeek = True
                mAddDays = 0 - mAddFloor
            End If


        ElseIf mChqFequency = 3 Then

            mPayIstWeek = False
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays2 = -15
            mAddDays3 = -25
            mAddDays4 = -35
            mAddDays = -35

        ElseIf mChqFequency = 4 Then
            mPayIstWeek = True
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays1 = -7
            mAddDays2 = -14
            mAddDays3 = -21
            mAddDays4 = -30
            mAddDays = -30
        End If

        If optAsOn(0).Checked = True Then
            mDateCond1 = "TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") + " & mAddDays1 & " ,'YYYYMMDD')"
            mDateCond2 = "TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") + " & mAddDays2 & " ,'YYYYMMDD')"
            mDateCond3 = "TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") + " & mAddDays3 & " ,'YYYYMMDD')"
            mDateCond4 = "TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") + " & mAddDays4 & " ,'YYYYMMDD')"
            mDateCond = "TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ")+ " & mAddDays & " ,'YYYYMMDD')"
            '        mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"
        Else
            mDateCond1 = "TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") + " & mAddDays1 & " ,'YYYYMMDD')"
            mDateCond2 = "TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") + " & mAddDays2 & " ,'YYYYMMDD')"
            mDateCond3 = "TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") + " & mAddDays3 & " ,'YYYYMMDD')"
            mDateCond4 = "TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") + " & mAddDays4 & " ,'YYYYMMDD')"

            '        mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"
            mDateCond = "TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ")+ " & mAddDays & " ,'YYYYMMDD')"
        End If
        SqlCondOLD = ""


        If mPayIstWeek = True Then
            SqlCondOLD = SqlCondOLD & " SUM(CASE WHEN " & mDateCond1 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
        Else
            SqlCondOLD = SqlCondOLD & " 0 AS Day1,"
        End If

        If mPayIIndWeek = True Then
            SqlCondOLD = SqlCondOLD & vbCrLf & " SUM(CASE WHEN " & mDateCond2 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
        Else
            SqlCondOLD = SqlCondOLD & vbCrLf & " 0 AS Day2,"
        End If

        If mPayIIIrdWeek = True Then
            SqlCondOLD = SqlCondOLD & vbCrLf & " SUM(CASE WHEN " & mDateCond3 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
        Else
            SqlCondOLD = SqlCondOLD & vbCrLf & " 0 AS Day3,"
        End If

        If mPayIVthWeek = True Then
            SqlCondOLD = SqlCondOLD & vbCrLf & " SUM(CASE WHEN " & mDateCond4 & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
        Else
            SqlCondOLD = SqlCondOLD & vbCrLf & " 0 AS Day4"
        End If



        SqlCondOLD = SqlCondOLD & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN" ''FIN_POSTED_TRN

        SqlCondOLD = SqlCondOLD & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlCondOLD = SqlCondOLD & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If


        SqlCondOLD = SqlCondOLD & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"

        '    SqlCondOLD = SqlCondOLD & vbCrLf & " AND " & mDateCond & " < CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN '" & vb6.Format(mOPDate, "YYYYMMDD") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "YYYYMMDD") & "' END"
        SqlCondOLD = SqlCondOLD & vbCrLf & " AND " & mDateCond & " < '" & VB6.Format(mOPDate, "YYYYMMDD") & "'" ''

        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlCondOLD = SqlCondOLD & vbCrLf & " AND TRN.BILLDATE <= '" & vb6.Format(mLastDate, "DD-MMM-YYYY") & "'"
        '    Else
        '        SqlCondOLD = SqlCondOLD & vbCrLf & " AND TRN.EXPDATE <= '" & vb6.Format(mLastDate, "DD-MMM-YYYY") & "'"
        '    End If

        If optAsOn(0).Checked = True Then
            SqlCondOLD = SqlCondOLD & vbCrLf & " GROUP BY BILLNO, BILLDATE"
            SqlCondOLD = SqlCondOLD & vbCrLf & " HAVING SUM(" & mBalAmtStr & ")<>0 "
            SqlCondOLD = SqlCondOLD & vbCrLf & " ORDER BY BILLDATE,BILLNO"
        Else
            SqlCondOLD = SqlCondOLD & vbCrLf & " GROUP BY BILLNO, EXPDATE"
            SqlCondOLD = SqlCondOLD & vbCrLf & " HAVING SUM(" & mBalAmtStr & ")<>0 "
            SqlCondOLD = SqlCondOLD & vbCrLf & " ORDER BY EXPDATE,BILLNO"
        End If
        Exit Function
ERR1:
        '    Resume
        SqlCondOLD = ""
        MsgInformation(Err.Description)
    End Function
    Private Function SqlCondADHOC(ByRef mPartyCode As String, ByRef mPartyName As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef cntRow As Integer, ByRef mSRNo As Integer) As String

        On Error GoTo ERR1
        'Dim SqlCondADHOC As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        Dim mMonth As Integer
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer
        Dim mRow As Integer
        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDateCond1 As String
        Dim mDateCond2 As String
        Dim mDateCond3 As String
        Dim mDateCond4 As String
        Dim mDateCond As String
        Dim mAddDays As Integer
        Dim mAddFloor As Integer
        Dim mLastDate As String

        Dim mDate1 As String
        Dim mDate2 As String
        Dim mDate3 As String
        Dim mDate4 As String

        Dim mDivisionCode As Double

        mLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mBalAmtStr = "BALANCE"

        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        If mChqFequency >= 4 Then
            mPayIstWeek = True
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays = 4
            mAddDays1 = 4 ' Val(txtDays1.Text)
            mAddDays2 = 4 ' Val(txtDays2.Text)
            mAddDays3 = 4 ' Val(txtDays3.Text)
            mAddDays4 = 4 'Val(txtDays4.Text)
            mDate1 = VB6.Format(Val(txtDays1.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate2 = VB6.Format(Val(txtDays2.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate3 = VB6.Format(Val(txtDays3.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate4 = VB6.Format(Val(txtDays4.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

        ElseIf mChqFequency = 3 Then
            mPayIstWeek = False
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays = 5
            mAddDays1 = 5 ' 0
            mAddDays2 = 5 ' Val(txtDays2.Text)
            mAddDays3 = 5 ' Val(txtDays3.Text)
            mAddDays4 = 5 ' Val(txtDays4.Text)

            mDate1 = ""
            mDate2 = VB6.Format(Val(txtDays2.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate3 = VB6.Format(Val(txtDays3.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate4 = VB6.Format(Val(txtDays4.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

        ElseIf mChqFequency = 2 Then
            mPayIstWeek = False
            mPayIIndWeek = True
            mPayIIIrdWeek = False
            mPayIVthWeek = True
            mAddDays = 7
            mAddDays1 = 7 ' 0
            mAddDays2 = 7 ' Val(txtDays2.Text)
            mAddDays3 = 7 ' 0
            mAddDays4 = 7 ' Val(txtDays4.Text)

            mDate1 = ""
            mDate2 = VB6.Format(Val(txtDays2.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate3 = ""
            mDate4 = VB6.Format(Val(txtDays4.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

        Else
            mPayIstWeek = True
            mPayIIndWeek = False
            mPayIIIrdWeek = False
            mPayIVthWeek = False
            mAddDays = 15
            mAddDays1 = 15 'Val(txtDays1.Text)
            mAddDays2 = 15 ' 0
            mAddDays3 = 15 ' 0
            mAddDays4 = 15 ' 0
            mDate1 = VB6.Format(Val(txtDays1.Text) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
            mDate2 = ""
            mDate3 = ""
            mDate4 = ""
        End If


        If optAsOn(0).Checked = True Then
            mDateCond1 = "TO_CHAR(TRN.BILLDATE  + " & mPaymentTerm & " - " & mAddDays1 & " ,'YYYYMMDD')"
            mDateCond2 = "TO_CHAR(TRN.BILLDATE  + " & mPaymentTerm & " - " & mAddDays2 & " ,'YYYYMMDD')"
            mDateCond3 = "TO_CHAR(TRN.BILLDATE  + " & mPaymentTerm & " - " & mAddDays3 & " ,'YYYYMMDD')"
            mDateCond4 = "TO_CHAR(TRN.BILLDATE  + " & mPaymentTerm & " - " & mAddDays4 & " ,'YYYYMMDD')"
            mDateCond = "TO_CHAR(TRN.BILLDATE  + " & mPaymentTerm & " - " & mAddDays & " ,'YYYYMMDD')"
            '        mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"
        Else
            mDateCond1 = "TO_CHAR(TRN.EXPDATE  + " & mPaymentTerm & " - " & mAddDays1 & " ,'YYYYMMDD')"
            mDateCond2 = "TO_CHAR(TRN.EXPDATE  + " & mPaymentTerm & " - " & mAddDays2 & " ,'YYYYMMDD')"
            mDateCond3 = "TO_CHAR(TRN.EXPDATE  + " & mPaymentTerm & " - " & mAddDays3 & " ,'YYYYMMDD')"
            mDateCond4 = "TO_CHAR(TRN.EXPDATE  + " & mPaymentTerm & " - " & mAddDays4 & " ,'YYYYMMDD')"

            '        mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"
            mDateCond = "TO_CHAR(TRN.EXPDATE  + " & mPaymentTerm & " - " & mAddDays & " ,'YYYYMMDD')"
        End If
        SqlCondADHOC = ""


        If mPayIstWeek = True Then
            SqlCondADHOC = SqlCondADHOC & " SUM(CASE WHEN " & mDateCond1 & " < '" & VB6.Format(mDate1, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
        Else
            SqlCondADHOC = SqlCondADHOC & " 0 AS Day1,"
        End If

        If mPayIIndWeek = True Then
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " SUM(CASE WHEN " & mDateCond2 & " < '" & VB6.Format(mDate2, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
        Else
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " 0 AS Day2,"
        End If

        If mPayIIIrdWeek = True Then
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " SUM(CASE WHEN " & mDateCond3 & " < '" & VB6.Format(mDate3, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
        Else
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " 0 AS Day3,"
        End If

        If mPayIVthWeek = True Then
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " SUM(CASE WHEN " & mDateCond4 & " < '" & VB6.Format(mDate4, "YYYYMMDD") & "' THEN " & mBalAmtStr & "* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
        Else
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " 0 AS Day4"
        End If



        SqlCondADHOC = SqlCondADHOC & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN" ''FIN_POSTED_TRN

        SqlCondADHOC = SqlCondADHOC & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If


        SqlCondADHOC = SqlCondADHOC & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"

        '    SqlCondADHOC = SqlCondADHOC & vbCrLf & " AND " & mDateCond & " < CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN '" & vb6.Format(mOPDate, "YYYYMMDD") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "YYYYMMDD") & "' END"
        SqlCondADHOC = SqlCondADHOC & vbCrLf & " AND " & mDateCond & " < '" & VB6.Format(mLastDate, "YYYYMMDD") & "'" ''mOPDate

        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlCondADHOC = SqlCondADHOC & vbCrLf & " AND TRN.BILLDATE <= '" & vb6.Format(mLastDate, "DD-MMM-YYYY") & "'"
        '    Else
        '        SqlCondADHOC = SqlCondADHOC & vbCrLf & " AND TRN.EXPDATE <= '" & vb6.Format(mLastDate, "DD-MMM-YYYY") & "'"
        '    End If

        If optAsOn(0).Checked = True Then
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " GROUP BY BILLNO, BILLDATE"
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " HAVING SUM(" & mBalAmtStr & ")<>0 "
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " ORDER BY BILLDATE,BILLNO"
        Else
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " GROUP BY BILLNO, EXPDATE"
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " HAVING SUM(" & mBalAmtStr & ")<>0 "
            SqlCondADHOC = SqlCondADHOC & vbCrLf & " ORDER BY EXPDATE,BILLNO"
        End If
        Exit Function
ERR1:
        '    Resume
        SqlCondADHOC = ""
        MsgInformation(Err.Description)
    End Function
    Private Function FillDetailDataInSprdOld(ByRef mPartyCode As String, ByRef mPartyName As String, ByRef mPaymentTerm As Integer, ByRef mChqFequency As Integer, ByRef cntRow As Integer, ByRef mSRNo As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOPDate As String
        Dim mMonth As Integer
        Dim mDate As String
        Dim mPayIstWeek As Boolean
        Dim mPayIIndWeek As Boolean
        Dim mPayIIIrdWeek As Boolean
        Dim mPayIVthWeek As Boolean
        Dim mAddDays1 As Integer
        Dim mAddDays2 As Integer
        Dim mAddDays3 As Integer
        Dim mAddDays4 As Integer
        Dim mRow As Integer
        Dim mIstTermAmount As Double
        Dim mIIstTermAmount As Double
        Dim mIIIstTermAmount As Double
        Dim mIVstTermAmount As Double

        mOPDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mMonth = Int(mPaymentTerm / 30) - 1
        mMonth = IIf(mMonth < 0, 0, mMonth)
        mChqFequency = IIf(mChqFequency = 0, 1, mChqFequency)
        mDate = CStr((Int(mPaymentTerm / (mChqFequency * 2))) Mod 30)

        mPayIstWeek = False
        mPayIIndWeek = False
        mPayIIIrdWeek = False
        mPayIVthWeek = False



        If mChqFequency = 1 Then
            If CDbl(mDate) > Val(txtDays1.Text) Then
                If CDbl(mDate) > Val(txtDays2.Text) Then
                    If CDbl(mDate) > Val(txtDays3.Text) Then
                        mPayIVthWeek = True
                    Else
                        mPayIIIrdWeek = True
                    End If
                Else
                    mPayIIndWeek = True
                End If
            Else
                mPayIstWeek = True
            End If

            mAddDays1 = 0
            mAddDays2 = 0
            mAddDays3 = 0
            mAddDays4 = 0
        ElseIf mChqFequency = 2 Then

            If CDbl(mDate) > Val(txtDays1.Text) Then
                If CDbl(mDate) > Val(txtDays2.Text) Then
                    If CDbl(mDate) > Val(txtDays3.Text) Then
                        mPayIVthWeek = True
                    Else
                        mPayIIIrdWeek = True
                    End If
                Else
                    mPayIIndWeek = True
                End If
            Else
                mPayIstWeek = True
            End If

            If mPayIstWeek = True Then
                mAddDays1 = 15
                mAddDays3 = 0
                mPayIIIrdWeek = True
            ElseIf mPayIIndWeek = True Then
                mAddDays2 = 15
                mAddDays4 = 0
                mPayIVthWeek = True
            ElseIf mPayIIIrdWeek = True Then
                mAddDays3 = 0
                mAddDays1 = 15
                mPayIstWeek = True
            ElseIf mPayIVthWeek = True Then
                mAddDays4 = 0
                mAddDays2 = 15
                mPayIIndWeek = True
            End If


        ElseIf mChqFequency = 3 Then
            mPayIstWeek = False
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays1 = 30
            mAddDays2 = 20
            mAddDays3 = 10
            mAddDays4 = 0
        ElseIf mChqFequency = 4 Then
            mPayIstWeek = True
            mPayIIndWeek = True
            mPayIIIrdWeek = True
            mPayIVthWeek = True
            mAddDays1 = 21
            mAddDays2 = 14
            mAddDays3 = 7
            mAddDays4 = 0
        End If


        If optAsOn(0).Checked = True Then
            SqlStr = " SELECT BILLNO, BILLDATE,"
            If mPayIstWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays1 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day1,"
            End If

            If mPayIIndWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays2 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day2,"
            End If

            If mPayIIIrdWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays3 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day3,"
            End If

            If mPayIVthWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE + " & mAddDays4 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day4"
            End If
        Else
            SqlStr = " SELECT BILLNO, EXPDATE AS BILLDATE,"
            If mPayIstWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays1 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day1,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day1,"
            End If

            If mPayIIndWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays2 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day2,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day2,"
            End If

            If mPayIIIrdWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays3 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day3,"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day3,"
            End If

            If mPayIVthWeek = True Then
                SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE + " & mAddDays4 & ", + " & mMonth & "),'YYYYMM') < '" & VB6.Format(mOPDate, "YYYYMM") & "' THEN BALANCE* DECODE(DC,'DR',-1,1) ELSE 0 END) AS Day4"
            Else
                SqlStr = SqlStr & vbCrLf & " 0 AS Day4"
            End If
        End If
        SqlStr = SqlStr & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN"

        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"

        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlStr = SqlStr & vbCrLf & " AND CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & "),'YYYYMM') ELSE BILLDATE END < CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN '" & vb6.Format(mOPDate, "YYYYMM") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN TO_CHAR(ADD_MONTHS(TRN.EXPDATE , + " & mMonth & "),'YYYYMM')  ELSE EXPDATE END < CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN '" & vb6.Format(mOPDate, "YYYYMM") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END"
        '    End If


        If optAsOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY BILLNO, BILLDATE"
        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY BILLNO, EXPDATE"
        End If


        '     mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
        ''                    & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
        ''                    & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
        ''                    & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
        ''                    & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"
        '
        '
        SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.BALANCE * DECODE(DC,'DR',1,-1))<0 "

        If optAsOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY BILLDATE,BILLNO"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY EXPDATE,BILLNO"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            cntRow = cntRow + 1
            SprdView.MaxRows = cntRow
            Call FormatSprdView(cntRow)
            mRow = 1
            Do While RsTemp.EOF = False
                mIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day1").Value), 0, RsTemp.Fields("Day1").Value)
                mIIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day2").Value), 0, RsTemp.Fields("Day2").Value)
                mIIstTermAmount = IIf(mIIstTermAmount = 0, 0, mIIstTermAmount - mIstTermAmount)

                mIIIstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day3").Value), 0, RsTemp.Fields("Day3").Value)
                mIIIstTermAmount = IIf(mIIIstTermAmount = 0, 0, mIIIstTermAmount - mIstTermAmount - mIIstTermAmount)

                mIVstTermAmount = IIf(IsDbNull(RsTemp.Fields("Day4").Value), 0, RsTemp.Fields("Day4").Value)
                mIVstTermAmount = IIf(mIVstTermAmount = 0, 0, mIVstTermAmount - mIstTermAmount - mIIstTermAmount - mIIIstTermAmount)

                With SprdView
                    .Row = cntRow
                    .Col = ColSRNo
                    .Text = mSRNo & "." & mRow

                    .Col = ColPartyCode
                    .Text = mPartyCode

                    .Col = ColPartyName
                    .Text = mPartyName

                    .Col = ColBillNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                    .Col = ColBillDate
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")

                    .Col = ColDue1
                    .Text = VB6.Format(mIstTermAmount, "0.00")

                    .Col = ColDue2
                    .Text = VB6.Format(mIIstTermAmount, "0.00")

                    .Col = ColDue3
                    .Text = VB6.Format(mIIIstTermAmount, "0.00")

                    .Col = ColDue4
                    .Text = VB6.Format(mIVstTermAmount, "0.00")

                    .Col = ColTotalDue
                    .Text = VB6.Format(mIstTermAmount + mIIstTermAmount + mIIIstTermAmount + mIVstTermAmount, "0.00")
                End With
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                    SprdView.MaxRows = cntRow
                End If
                Call FormatSprdView(cntRow)
                mRow = mRow + 1
                mIstTermAmount = 0
                mIIstTermAmount = 0
                mIIIstTermAmount = 0
                mIVstTermAmount = 0
            Loop
        End If

        FillDetailDataInSprdOld = True

        Exit Function
ERR1:
        '    Resume
        FillDetailDataInSprdOld = False
        MsgInformation(Err.Description)
    End Function
    Private Sub FillHeading()
        With SprdView
            .Row = 0

            .Col = ColPicMain
            .Text = "A"

            .Col = ColPicSub
            .Text = "B"

            .Col = ColSRNo
            .Text = "S. No."

            .Col = ColPartyCode
            .Text = "Party Code"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColPaymentTerms
            .Text = "Payment Terms"

            .Col = ColBillNo
            .Text = "Bill no"

            .Col = ColBillDate
            If optAsOn(0).Checked = True Then
                .Text = "Bill Date"
            Else
                .Text = "MRR Date"
            End If
            .Col = ColOpeningBal
            .Text = "Ledger Opening Balance"

            .Col = ColDue1
            .Text = VB6.Format(txtDays1.Text, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

            .Col = ColDue2
            .Text = VB6.Format(txtDays2.Text, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

            .Col = ColDue3
            .Text = VB6.Format(txtDays3.Text, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

            .Col = ColDue4
            .Text = VB6.Format(txtDays4.Text, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

            .Col = ColTotalDue
            .Text = "Total Due (This Month)"

            .Col = ColClosingBalance
            .Text = "Ledger Closing Balance (This Month)"

            .Col = ColPaidinMonth
            .Text = "Cheque Frequency in a Month"

            .Col = ColPaymentMode
            .Text = "Payment of Mode"

        End With
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)


        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub


    Private Sub optParty_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optParty.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optParty.GetIndex(eventSender)
            TxtName.Enabled = IIf(Index = 0, True, False)
            cmdsearch.Enabled = IIf(Index = 0, True, False)
        End If
    End Sub

    Private Sub PrintCommand()
        CmdPreview.Enabled = PrintEnable
        cmdPrint.Enabled = PrintEnable
    End Sub

    Private Sub SprdView_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdView.ClickEvent
        'Sort on specified column or show/collapse rows

        'Show Summary/Detail info.
        'If clicked on a "+" or "-" grouping

        If eventArgs.col = ColPicMain Then
            SprdView.Col = ColPicMain
            SprdView.Row = eventArgs.row
            If SprdView.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                'Show or hide the specified rows
                ShowHideRows(ColPicMain, eventArgs.row)
            End If
        End If
    End Sub

    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows
        Dim I As Short
        Dim collapsetype As Short

        SprdView.Row = Row
        SprdView.Col = ColFlag

        If SprdView.Text = "0" Then
            collapsetype = 0 'collape/hide rows : minus picture
            SprdView.Col = 1
            SprdView.TypePictPicture = pluspict
            SprdView.Col = ColFlag
            SprdView.Text = "1"
        Else
            collapsetype = 1 'uncollapse / show rows: plus picture
            SprdView.Col = 1
            SprdView.TypePictPicture = minuspict
            SprdView.Col = ColFlag
            SprdView.Text = "0"
        End If

        SprdView.ReDraw = False
        For I = 1 To SprdView.GetRowItemData(Row)
            SprdView.Row = SprdView.Row + 1
            If collapsetype = 0 Then
                SprdView.RowHidden = True
            Else
                SprdView.RowHidden = False
            End If
        Next I
        SprdView.ReDraw = True

    End Sub

    Private Sub txtDays1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDays1_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDays1.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mLastDays As Integer
        mLastDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        If Val(txtDays1.Text) < 1 Or Val(txtDays1.Text) > mLastDays Then
            MsgInformation("Invalid Date.")
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDays2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDays2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDays2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mLastDays As Integer
        mLastDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        If Val(txtDays2.Text) < 1 Or Val(txtDays2.Text) > mLastDays Then
            MsgInformation("Invalid Date.")
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDays3_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays3.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDays3_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDays3.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mLastDays As Integer
        mLastDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        If Val(txtDays3.Text) < 1 Or Val(txtDays3.Text) > mLastDays Then
            MsgInformation("Invalid Date.")
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDays4_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays4.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDays4_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDays4.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mLastDays As Integer
        mLastDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        If Val(txtDays4.Text) < 1 Or Val(txtDays4.Text) > mLastDays Then
            MsgInformation("Invalid Date.")
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub

    Private Sub txtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset
        Dim SqlStr As String

        If TxtName.Text = "" Then GoTo EventExitSub
        SqlStr = ""
        SqlStr = "Select SUPP_CUST_Code, SUPP_CUST_Name,SUPP_CUST_ADDR AS Address1,SUPP_CUST_STATE AS Address2, SUPP_CUST_CITY AS City, SUPP_CUST_PIN AS PINCODE " & vbCrLf & " FROM FIN_SUPP_CUST_MST ACM WHERE " & vbCrLf & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"


        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE ='S'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE ='C'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            lblAddress.Text = IIf(IsDbNull(RS.Fields("Address1").Value), "", RS.Fields("Address1").Value & ", " & RS.Fields("Address2").Value & ", " & RS.Fields("City").Value & " - " & RS.Fields("Pincode").Value & "")
        Else
            MsgBox("Invalid Name", MsgBoxStyle.Information)
            Cancel = True
        End If
        RS.Close()
        RS = Nothing
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub DisplayTotals()
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mTotDebit As Double
        Dim mTotCredit As Double
        Dim mTotBalance As Double
        Dim mDC As String

        '    With SprdView
        '
        ''        Call MainClass.AddBlankfpSprdRow(SprdView, ColBillNo)
        '        .MaxRows = .MaxRows + 1
        '        .Row = .MaxRows
        '
        '        .Col = ColPartyName
        '        .Text = "TOTAL :"
        '        .FontBold = True
        '
        '        .Row = .MaxRows
        '        .Row2 = .MaxRows
        '        .Col = 1
        '        .col2 = .MaxCols
        '        .BlockMode = True
        '        .BackColor = &H8000000F             '''&H80FF80
        '        .BlockMode = False
        '
        ''        Call CalcRowTotal(SprdView, ColOpeningBal, 1, ColOpeningBal, .MaxRows - 1, .MaxRows, ColOpeningBal)
        ''        Call CalcRowTotal(SprdView, ColCredit, 1, ColCredit, .MaxRows - 1, .MaxRows, ColCredit)
        '
        '        FormatSprdView -1
        '
        '        For cntRow = 1 To .MaxRows - 1
        '            .Row = cntRow
        '
        '            .Col = ColDC
        '            mDC = Left(.Text, 1)
        '
        '            .Col = ColOpeningBal
        '            mTotDebit = mTotDebit + (Val(.Text) * IIf(mDC = "D", 1, -1))
        '
        '            .Col = ColCredit
        '            mTotCredit = mTotCredit + Val(.Text)
        '
        '            .Col = ColTotalDue
        '            mTotBalance = mTotBalance + (Val(.Text) * IIf(mDC = "D", 1, -1))
        '
        '        Next
        '        .Row = .MaxRows
        '
        '        .Col = ColOpeningBal
        '        .Text = Format(Abs(mTotDebit), "0.00")
        '        .FontBold = True
        '
        '        .Col = ColCredit
        '        .Text = Format(mTotCredit, "0.00")
        '        .FontBold = True
        '
        '        .Col = ColTotalDue
        '        .Text = Format(Abs(mTotBalance), "0.00")
        '        .FontBold = True
        '
        '        .Col = ColDC
        '        .Text = IIf(mTotBalance >= 0, "DR", "CR")
        '        .FontBold = True
        '
        '        .RowHeight(.Row) = 1.25 * ConRowHeight
        ''        .RowsFrozen = .MaxRows
        '    End With
        '
        PrintCommand()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub ReportForReminder(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        Dim mDate As String

        PubDBCn.Errors.Clear()

        If optParty(0).Checked = True Then
            If TxtName.Text = "" Then Exit Sub
        End If

        '    mDate = InputBox("Please Enter Payment Due on Date :", "Payment Due on Date", Format(txtDateTo.Text, "DD/MM/YYYY"))

        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdView, 0, SprdView.MaxRows, 1, SprdView.MaxCols, PubDBCn) = False Then GoTo ERR1

        '''''Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "Payment Summary for the Month : " & VB6.Format(lblRunDate.Text, "MMMM, YYYY")

        mRPTName = "OutStandingSummary.rpt"


        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        'Resume
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SubRow"

        FetchRecordForReport = mSqlStr

    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mInterest As String
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub InsertPrintDummy()


        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mPartyName As String
        Dim mPartyAdd As String
        Dim mPartyCity As String
        Dim mPartyState As String
        Dim mPartyPin As String
        Dim mPartyPhone As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mRemarks As String
        Dim mDebit As String
        Dim mADV As String
        Dim mDNOTE As String
        Dim mCNOTE As String
        Dim mTDS As String
        Dim mCredit As String
        Dim mBalance As String
        Dim mDC As String
        Dim mDueDate As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdView
            For cntRow = 0 To .MaxRows ''- 1
                .Row = cntRow

                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColPaymentTerms
                mPartyCity = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColOpeningBal
                mDebit = Trim(.Text)

                .Col = ColDue1
                mADV = Trim(.Text)

                .Col = ColDue2
                mDNOTE = Trim(.Text)

                .Col = ColDue3
                mCNOTE = Trim(.Text)

                .Col = ColDue4
                mTDS = Trim(.Text)

                .Col = ColTotalDue
                mBalance = Trim(.Text)

                '            .Col = ColDC
                '            mDC = Trim(.Text)

                If Val(mCredit) <> 0 Then
                    mRemarks = "Partial Paid"
                Else
                    mRemarks = ""
                End If
                SqlStr = "Insert into TEMP_PrintDummyData ( " & vbCrLf & " UserID,SubRow,Field1,Field2,Field3,Field4, " & vbCrLf & " Field5,Field6,Field7,Field8, " & vbCrLf & " Field9,Field10,Field11,Field12, " & vbCrLf & " Field13,Field14,Field15,Field16,Field17,Field18 " & vbCrLf & " ) Values ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mPartyName & "', " & vbCrLf & " '" & mPartyAdd & "', " & vbCrLf & " '" & mPartyCity & "', " & vbCrLf & " '" & mPartyState & "', " & vbCrLf & " '" & mPartyPin & "', " & vbCrLf & " '" & mPartyPhone & "', " & vbCrLf & " '" & mBillNo & "', " & vbCrLf & " '" & mDebit & "', " & vbCrLf & " '" & mADV & "', " & vbCrLf & " '" & mDNOTE & "', " & vbCrLf & " '" & mCNOTE & "', " & vbCrLf & " '" & mTDS & "', " & vbCrLf & " '" & mCredit & "', " & vbCrLf & " '" & mBalance & "', " & vbCrLf & " '" & mDC & "', " & vbCrLf & " '" & mDueDate & "','" & mBillDate & "','" & mRemarks & "')"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtPaymentDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentDate.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub


    Private Sub txtPaymentDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaymentDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtPaymentDate) = False Then
            txtPaymentDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPaymentDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentDays.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub

    Private Sub txtPaymentDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaymentDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    'Private Sub UpDYear_DownClick()
    '    lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
    '    SetDate(CDate(lblRunDate.Text))
    '    '    Call PrintCommand(False)
    '    'RefreshScreen
    'End Sub
    'Private Sub UpDYear_UpClick()
    '    lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
    '    SetDate(CDate(lblRunDate.Text))
    '    '    Call PrintCommand(False)
    '    'RefreshScreen
    'End Sub
End Class
