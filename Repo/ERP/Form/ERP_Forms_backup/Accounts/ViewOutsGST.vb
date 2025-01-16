Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewOutsGST
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Dim PrintEnable As Boolean
    Dim PrintCopies As Short
    Dim NewFlagsSetting As Integer
    Dim OldFlagsSetting As Integer
    Private Const ColPartyName As Short = 1
    Private Const ColPartyAdd As Short = 2
    Private Const ColPartyCity As Short = 3
    Private Const ColPartyState As Short = 4
    Private Const ColPartyPin As Short = 5
    Private Const ColPartyPhone As Short = 6
    Private Const ColBillNo As Short = 7
    Private Const ColBillDate As Short = 8
    Private Const ColDebit As Short = 9
    Private Const ColADV As Short = 10
    Private Const ColDNOTE As Short = 11
    Private Const ColCNOTE As Short = 12
    Private Const ColTDS As Short = 13
    Private Const ColCredit As Short = 14
    Private Const ColBalance As Short = 15
    Private Const ColDC As Short = 16
    Private Const ColPaymentDate As Short = 17
    Private Const ColDueDate As Short = 18
    Private Const ColPaymentTerms As Short = 19
    Private Const ColLastTrans As Short = 20
    Private Const ConRowHeight As Short = 15
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub CboShowFor_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboShowFor.SelectedIndexChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        PrintEnable = False
        PrintCommand()
        TxtName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
    End Sub
    Private Sub ChkAllGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllGroup.CheckStateChanged
        TxtGroup.Enabled = IIf(chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
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
        'Dim RS As ADODB.Recordset
        Dim Sqlstr As String
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If lblOutsType.Text = "P" Then
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE ='S'"
        Else
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE ='C'"
        End If
        If MainClass.SearchMaster((TxtName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", Sqlstr) = True Then
            TxtName.Text = AcName
        End If
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ERR1
        If Trim(TxtName.Text) = "" And chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Account Name Cann't be Blank.")
            TxtName.Focus()
            PrintEnable = False
            PrintCommand()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdView)
        PrintEnable = True
        PrintCommand()
        Call FormatSprdView(-1)
        '    If OptSumDet(0).Value = True Then
        ViewOuts()
        '    Else
        '        ViewOutsSummary
        '    End If
        GetLastTransDate()
        DisplayTotals()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView(ByRef Arow As Integer)
        On Error GoTo ErrPart
        With SprdView
            .MaxCols = ColLastTrans ''ColPaymentTerms
            .set_RowHeight(0, ConRowHeight * 1.7)
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow
            '        .Col = ColTRNType
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditLen = 60
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .ColWidth(ColTRNType) = 10
            '
            '        .Col = ColVNo
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditLen = 60
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .ColWidth(ColVNo) = 10
            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyName, 20)
            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If
            .Col = ColPartyAdd
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyAdd, 20)
            .ColHidden = True
            .Col = ColPartyCity
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyCity, 20)
            .ColHidden = True
            .Col = ColPartyState
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyState, 20)
            .ColHidden = True
            .Col = ColPartyPin
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyPin, 20)
            .ColHidden = True
            .Col = ColPartyPhone
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyPhone, 20)
            .ColHidden = True
            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColBillNo, 8)
            .ColHidden = IIf(OptSumDet(0).Checked = True, False, True)
            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColBillDate, 8)
            .ColsFrozen = ColBillDate
            .ColHidden = IIf(OptSumDet(0).Checked = True, False, True)
            .Col = ColDebit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDebit, 10)
            .Col = ColADV
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColADV, 8)
            .Col = ColDNOTE
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDNOTE, 8)
            .Col = ColCNOTE
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCNOTE, 8)
            .Col = ColTDS
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColTDS, 8)
            .Col = ColCredit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCredit, 10)
            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalance, 10)
            .Col = ColDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColDC, 3)
            .Col = ColDueDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColDueDate, 8)
            .Col = ColPaymentTerms
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPaymentTerms, 8)
            .ColHidden = IIf(OptSumDet(0).Checked = True, False, True)
            .Col = ColLastTrans
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColLastTrans, 8)
            .ColHidden = False
            '        .Col = ColPaymentTerms
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditLen = 254
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .ColWidth(ColPaymentTerms) = 8
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewOutsGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        Dim Sqlstr As String
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        Call SetMainFormCordinate(Me)
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormLoaded = False
        Call FillCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtName.Enabled = False
        cmdsearch.Enabled = False
        chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked
        TxtGroup.Enabled = False
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
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset
        CboShowFor.Items.Add("Uncleared")
        CboShowFor.Items.Add("Cleared")
        CboShowFor.Items.Add("Both")
        CboShowFor.SelectedIndex = 0
        cboDivision.Items.Clear()
        Sqlstr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboDivision.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.SelectedIndex = 0
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
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtPaymentDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub frmViewOutsGST_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdView, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewOutsGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub ViewOuts()
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim mPartyCode As String
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDivisionCode As Double
        mDAmtStr = "SUM(DECODE(BILLTYPE,'B',1,DECODE(BILLTYPE,'N',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mBalAmtStr = "" & mDAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " + " & mCNAmtStr & " + " & mCAmtStr & ""
        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"
        If OptSumDet(0).Checked = True Then
            Sqlstr = " Select  ACM.SUPP_CUST_NAME," & vbCrLf & " '' AS SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " '' AS SUPP_CUST_PIN, '' AS CONTACT_TELNO, " & vbCrLf & " BillNo, TO_CHAR(BillDate,'DD/MM/YYYY') AS BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mDAmtStr & ")) AS BILLAMOUNT, " & vbCrLf & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf & " TO_CHAR(ABS(" & mCAmtStr & "))  "
            If lblOutsType.Text = "P" Then
                Sqlstr = Sqlstr & " AS PAYMENT, "
            Else
                Sqlstr = Sqlstr & " AS RECEIPT, "
            End If
            Sqlstr = Sqlstr & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, " & vbCrLf & "  "
            Sqlstr = Sqlstr & vbCrLf & " GETBILLPAYMENTDATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",ACM.SUPP_CUST_CODE,BillNo, BillDate) AS PAYMENTDATE,"
            Sqlstr = Sqlstr & vbCrLf & " MIN(EXPDATE) AS DUEDATE, "
        Else
            Sqlstr = " Select  ACM.SUPP_CUST_NAME," & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO, " & vbCrLf & " '' AS BillNo, '' AS BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mDAmtStr & ")) AS BILLAMOUNT, " & vbCrLf & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf & " TO_CHAR(ABS(" & mCAmtStr & "))  "
            If lblOutsType.Text = "P" Then
                Sqlstr = Sqlstr & " AS PAYMENT, "
            Else
                Sqlstr = Sqlstr & " AS RECEIPT, "
            End If
            Sqlstr = Sqlstr & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, " & vbCrLf & " '' AS PAYMENTDATE, MIN(EXPDATE) AS DUEDATE,"
        End If
        '    SqlStr = SqlStr & vbCrLf & " MIN(PMST.PAY_TERM_DESC) AS PAY_TERM"
        Sqlstr = Sqlstr & vbCrLf & " MAX(PAYMENT_DESC) AS PAYMENT_DESC, '' AS LAST_TRANS_DATE"
        Sqlstr = Sqlstr & vbCrLf & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM "
        Sqlstr = Sqlstr & vbCrLf & " WHERE TRN.Company_Code=Acm.Company_Code AND TRN.AccountCode=Acm.SUPP_CUST_Code " & vbCrLf & " AND TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" ''& vbCrLf |            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        Sqlstr = Sqlstr & " AND BOOKTYPE<>'O'"
        If lblOutsType.Text = "P" Then
            Sqlstr = Sqlstr & " AND ACM.SUPP_CUST_TYPE ='S'"
        Else
            Sqlstr = Sqlstr & " AND ACM.SUPP_CUST_TYPE ='C'"
        End If
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(UCase(TxtName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
                Sqlstr = Sqlstr & vbCrLf & " AND ACM.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtGroup.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtGroup, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                Sqlstr = Sqlstr & " AND (GROUPCODE=" & MasterNo & " OR GROUPCODECR=" & MasterNo & ")"
            End If
        End If
        Sqlstr = Sqlstr & vbCrLf & " AND TRN.BILLDATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.BILLDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlStr = SqlStr & vbCrLf & " AND (TRN.BILLDATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' OR TRN.BILLDATE IS NULL OR TRN.BILLDATE='') "
        If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
            Sqlstr = Sqlstr & vbCrLf & " AND CASE WHEN BILLTYPE='B' OR BILLTYPE='D' OR BILLTYPE='C' OR TRNTYPE='B' THEN  TRN.BILLDATE ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' OR BILLTYPE='D' OR BILLTYPE='C' OR TRNTYPE='B' THEN TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND CASE WHEN BILLTYPE='B' THEN  TRN.BILLDATE ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' THEN TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END "
        End If
        '    Else
        '        If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
        '            Sqlstr = Sqlstr & vbCrLf & " AND CASE WHEN BILLTYPE='B' THEN  TRN.EXPDATE ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' THEN '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END "
        '        Else
        '            Sqlstr = Sqlstr & vbCrLf & " AND CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN  TRN.BILLDATE ELSE TRN.EXPDATE END <= CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END "
        '        End If
        '    End If
        If OptSumDet(0).Checked = True Then
            Sqlstr = Sqlstr & vbCrLf & " GROUP BY BillNo, BillDate,ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_CODE," & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO "
        Else
            Sqlstr = Sqlstr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_CODE, " & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO "
        End If
        If CboShowFor.Text = "Cleared" Then
            Sqlstr = Sqlstr & vbCrLf & " HAVING " & mBalAmtStr & "=0 "
        ElseIf CboShowFor.Text = "Uncleared" Then
            If OptShow(0).Checked = True Then
                Sqlstr = Sqlstr & vbCrLf & " HAVING " & mBalAmtStr & "<>0 "
            ElseIf OptShow(1).Checked = True Then
                Sqlstr = Sqlstr & vbCrLf & " HAVING " & mBalAmtStr & " > 0   "
            ElseIf OptShow(2).Checked = True Then
                Sqlstr = Sqlstr & vbCrLf & " HAVING " & mBalAmtStr & " < 0   "
            End If
        End If
        If OptSumDet(0).Checked = True Then
            Sqlstr = Sqlstr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME,TO_DATE(BillDate,'DD/MM/YYYY'),BillNo "
        Else
            Sqlstr = Sqlstr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME"
        End If
        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ViewOutsSummary()
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim mPartyCode As String
        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mDivisionCode As Double
        Sqlstr = " Select  PARTYNAME," & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf & " '' AS BillNo, '' AS BillDate, " & vbCrLf & " TO_CHAR(SUM(BILLAMOUNT)) AS BILLAMOUNT, " & vbCrLf & " TO_CHAR(SUM(ADV)) AS ADV, " & vbCrLf & " TO_CHAR(SUM(DNOTE)) AS DNOTE, " & vbCrLf & " TO_CHAR(SUM(CNOTE)) AS CNOTE, " & vbCrLf & " TO_CHAR(SUM(TDS)) AS TDS, " & vbCrLf & " TO_CHAR(SUM(PAYMENT)) "
        If lblOutsType.Text = "P" Then
            Sqlstr = Sqlstr & " AS PAYMENT, "
        Else
            Sqlstr = Sqlstr & " AS RECEIPT, "
        End If
        Sqlstr = Sqlstr & vbCrLf & " TO_CHAR(ABS(SUM(BALANCE * DECODE(DC,'DR',1,-1)))) AS BALANCE, " & vbCrLf & " CASE WHEN SUM(BALANCE * DECODE(DC,'DR',1,-1)) >=0 THEN 'DR' ELSE 'CR' END AS DC, " & vbCrLf & " MAX(EXPDATE) AS DUEDATE, "
        Sqlstr = Sqlstr & vbCrLf & " '' AS PAYMENT_DESC,  '',  MAX(BILLDATE) AS LAST_TRANS1"
        Sqlstr = Sqlstr & vbCrLf & " FROM vw_FIN_PAYMENT_ADV "
        Sqlstr = Sqlstr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        If lblOutsType.Text = "P" Then
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE ='S'"
        Else
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE ='C'"
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            Sqlstr = Sqlstr & vbCrLf & " AND DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            Sqlstr = Sqlstr & vbCrLf & " AND PARTYNAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"
        End If
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtGroup.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtGroup, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                Sqlstr = Sqlstr & " AND (GROUPCODE=" & MasterNo & " OR GROUPCODECR=" & MasterNo & ")"
            End If
        End If
        '    If optAsOn(0).Value = True Then         ''AS On By Bill...
        '        SqlStr = SqlStr & vbCrLf & " AND (TRN.BILLDATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' OR TRN.BILLDATE IS NULL OR TRN.BILLDATE='') "
        '        If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
        Sqlstr = Sqlstr & vbCrLf & " AND BILLDATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND BILLDATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' THEN  TRN.BILLDATE ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' THEN '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END "
        '        End If
        '    Else
        ''        If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
        '            Sqlstr = Sqlstr & vbCrLf & " AND EXPDATE <= '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
        ''        Else
        ''            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN  TRN.BILLDATE ELSE TRN.EXPDATE END <= CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END "
        ''        End If
        '    End If
        If CboShowFor.Text = "Cleared" Then
            Sqlstr = Sqlstr & vbCrLf & " AND BALANCE=0"
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND BALANCE<>0"
            '        If OptShow(1).Value = True Then
            '            SqlStr = SqlStr & vbCrLf & " AND BALANCE > 0   "
            '        ElseIf OptShow(2).Value = True Then
            '            SqlStr = SqlStr & vbCrLf & " AND BALANCE < 0   "
            '        End If
        End If
        Sqlstr = Sqlstr & vbCrLf & " GROUP BY PARTYNAME, " & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, SUPP_CUST_PHONE "
        If CboShowFor.Text = "Cleared" Then
            Sqlstr = Sqlstr & vbCrLf & " HAVING SUM(BALANCE * DECODE(DC,'DR',1,-1))=0 "
        ElseIf CboShowFor.Text = "Uncleared" Then
            If OptShow(0).Checked = True Then
                Sqlstr = Sqlstr & vbCrLf & " HAVING SUM(BALANCE * DECODE(DC,'DR',1,-1))<>0 "
            ElseIf OptShow(1).Checked = True Then
                Sqlstr = Sqlstr & vbCrLf & " HAVING SUM(BALANCE * DECODE(DC,'DR',1,-1)) > 0   "
            ElseIf OptShow(2).Checked = True Then
                Sqlstr = Sqlstr & vbCrLf & " HAVING SUM(BALANCE * DECODE(DC,'DR',1,-1)) < 0   "
            End If
        End If
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY PARTYNAME"
        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub PrintCommand()
        CmdPreview.Enabled = PrintEnable
        cmdPrint.Enabled = PrintEnable
    End Sub
    Private Sub TxtGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroup.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub TxtGroup_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroup.DoubleClick
        SearchGroup()
    End Sub
    Private Sub TxtGroup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtGroup.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchGroup()
    End Sub
    Private Sub SearchGroup()
        Dim Sqlstr As String
        Sqlstr = ""
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_Category='G'"
        If MainClass.SearchMaster((TxtGroup.Text), "FIN_GROUP_MST", "GROUP_Name", Sqlstr) = True Then
            TxtGroup.Text = AcName
            TxtGroup.Focus()
        End If
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
        Dim Sqlstr As String
        If TxtName.Text = "" Then GoTo EventExitSub
        Sqlstr = ""
        Sqlstr = "Select SUPP_CUST_Code, SUPP_CUST_Name,SUPP_CUST_ADDR AS Address1,SUPP_CUST_STATE AS Address2, SUPP_CUST_CITY AS City, SUPP_CUST_PIN AS PINCODE " & vbCrLf & " FROM FIN_SUPP_CUST_MST ACM WHERE " & vbCrLf & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"
        If lblOutsType.Text = "P" Then
            Sqlstr = Sqlstr & vbCrLf & " AND SUPP_CUST_TYPE ='S'"
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND SUPP_CUST_TYPE ='C'"
        End If
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
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
        With SprdView
            '        Call MainClass.AddBlankfpSprdRow(SprdView, ColBillNo)
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            .Col = ColBillNo
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '&H80FF80
            .BlockMode = False
            '        Call CalcRowTotal(SprdView, ColDebit, 1, ColDebit, .MaxRows - 1, .MaxRows, ColDebit)
            '        Call CalcRowTotal(SprdView, ColCredit, 1, ColCredit, .MaxRows - 1, .MaxRows, ColCredit)
            FormatSprdView(-1)
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColDC
                mDC = VB.Left(.Text, 1)
                .Col = ColDebit
                mTotDebit = mTotDebit + (Val(.Text) * IIf(mDC = "D", 1, -1))
                .Col = ColCredit
                mTotCredit = mTotCredit + Val(.Text)
                .Col = ColBalance
                mTotBalance = mTotBalance + (Val(.Text) * IIf(mDC = "D", 1, -1))
            Next
            .Row = .MaxRows
            .Col = ColDebit
            .Text = VB6.Format(System.Math.Abs(mTotDebit), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColCredit
            .Text = VB6.Format(mTotCredit, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBalance
            .Text = VB6.Format(System.Math.Abs(mTotBalance), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDC
            .Text = IIf(mTotBalance >= 0, "DR", "CR")
            .Font = VB6.FontChangeBold(.Font, True)
            .set_RowHeight(.Row, 1.25 * ConRowHeight)
            '        .RowsFrozen = .MaxRows
        End With
        PrintCommand()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub GetLastTransDate()
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mPartyName As String
        Dim mPartyCode As String
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset
        Dim mVDate As String
        Dim mPrevPartyName As String
        With SprdView
            mPrevPartyName = ""
            mVDate = ""
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(.Text)
                If mPrevPartyName <> mPartyName Then
                    mVDate = ""
                    mPartyCode = "''"
                    If MainClass.ValidateWithMasterTable(UCase(mPartyName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPartyCode = MasterNo
                    End If
                    Sqlstr = " Select  MAX(VDATE) AS VDATE FROM FIN_POSTED_TRN" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
                    MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mVDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                    End If
                End If
                .Col = ColLastTrans
                .Text = mVDate
                mPrevPartyName = mPartyName
            Next
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub ReportForReminder(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim Sqlstr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        Dim mDate As String
        PubDBCn.Errors.Clear()
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If TxtName.Text = "" Then Exit Sub
        End If
        mDate = InputBox("Please Enter Payment Due on Date :", "Payment Due on Date", VB6.Format(txtDateTo.Text, "DD/MM/YYYY"))
        Sqlstr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(Sqlstr)
        Sqlstr = ""
        '    If NewFlagsSetting = 1048600 Then   'Print ALL records(&H80018)
        '        Call InsertALLPrintDummy
        '    Else                            'Print Current Record (selection)
        Call InsertPrintDummy()
        '    End If
        'Select Record for print...
        Sqlstr = ""
        If chkLegelNotice.CheckState = System.Windows.Forms.CheckState.Checked Then
            Sqlstr = MainClass.FetchFromTempData(Sqlstr, "SUBROW")
        Else
            Sqlstr = FetchRecordForReport(Sqlstr)
        End If
        If chkReminderLetter.CheckState = System.Windows.Forms.CheckState.Checked Then
            mTitle = "Reminder Letter"
            MainClass.AssignCRptFormulas(Report1, "HLine1='Dear Sir,'")
            MainClass.AssignCRptFormulas(Report1, "HLine2='You are requested to send the following overdue payments immediately.'")
            MainClass.AssignCRptFormulas(Report1, "FLine1='Thanking You,'")
            MainClass.AssignCRptFormulas(Report1, "FLine2='Your sincerely,'")
            MainClass.AssignCRptFormulas(Report1, "FLine3='" & "for " & RsCompany.Fields("Company_Name").Value & "'")
            MainClass.AssignCRptFormulas(Report1, "ASign='Authorised Signatory'")
            MainClass.AssignCRptFormulas(Report1, "User='" & MainClass.AllowSingleQuote(PubUserID) & "'")
        ElseIf chkLegelNotice.CheckState = System.Windows.Forms.CheckState.Checked Then
            MainClass.AssignCRptFormulas(Report1, "FLine3='" & "for " & RsCompany.Fields("Company_Name").Value & "'")
            MainClass.AssignCRptFormulas(Report1, "ASign='Authorised Signatory'")
            MainClass.AssignCRptFormulas(Report1, "User='" & MainClass.AllowSingleQuote(PubUserID) & "'")
        Else
            mTitle = "OutStanding"
            MainClass.AssignCRptFormulas(Report1, "HLine1=''")
            MainClass.AssignCRptFormulas(Report1, "HLine2=''")
            MainClass.AssignCRptFormulas(Report1, "FLine1=''")
            MainClass.AssignCRptFormulas(Report1, "FLine2=''")
            MainClass.AssignCRptFormulas(Report1, "FLine3=''")
            MainClass.AssignCRptFormulas(Report1, "ASign=''")
            MainClass.AssignCRptFormulas(Report1, "User=''")
        End If
        mSubTitle = "SUB : PAYMENT DUE ON " & mDate ''Format(txtDateTo.Text, "DD MMM, YYYY")
        If chkPrintListFormat.CheckState = System.Windows.Forms.CheckState.Checked Then
            mRPTName = "OutStandingList.Rpt"
        ElseIf chkLegelNotice.CheckState = System.Windows.Forms.CheckState.Checked Then
            mRPTName = "OutStandingLegel.Rpt"
        Else
            If lblOutsType.Text = "R" Then
                mRPTName = "OutStandingR.Rpt"
            Else
                mRPTName = "OutStanding.Rpt"
            End If
        End If
        Call ShowReport(Sqlstr, mRPTName, Mode, mTitle, mSubTitle)
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
        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY Field1,TO_DATE(Field17,'DD/MM/YYYY'),Field7"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim mInterest As String
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub InsertPrintDummy()
        On Error GoTo ERR1
        Dim Sqlstr As String
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
        Dim mPaymentTerms As String
        Dim mLastTrans As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        Sqlstr = ""
        With SprdView
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColPartyAdd
                mPartyAdd = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColPartyCity
                mPartyCity = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColPartyState
                mPartyState = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColPartyPin
                mPartyPin = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColPartyPhone
                mPartyPhone = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColBillDate
                mBillDate = Trim(.Text)
                .Col = ColDebit
                mDebit = Trim(.Text)
                .Col = ColADV
                mADV = Trim(.Text)
                .Col = ColDNOTE
                mDNOTE = Trim(.Text)
                .Col = ColCNOTE
                mCNOTE = Trim(.Text)
                .Col = ColTDS
                mTDS = Trim(.Text)
                .Col = ColCredit
                mCredit = Trim(.Text)
                .Col = ColBalance
                mBalance = Trim(.Text)
                .Col = ColDC
                mDC = Trim(.Text)
                .Col = ColDueDate
                mDueDate = Trim(.Text)
                .Col = ColPaymentTerms
                mPaymentTerms = Trim(.Text)
                .Col = ColLastTrans
                mLastTrans = Trim(.Text)
                If Val(mCredit) <> 0 Then
                    mRemarks = "Partial Paid"
                Else
                    mRemarks = ""
                End If
                If chkLegelNotice.CheckState = System.Windows.Forms.CheckState.Checked And UCase(mDC) = "CR" Then GoTo NextRec
                Sqlstr = "Insert into TEMP_PrintDummyData ( " & vbCrLf & " UserID,SubRow,Field1,Field2,Field3,Field4, " & vbCrLf & " Field5,Field6,Field7,Field8, " & vbCrLf & " Field9,Field10,Field11,Field12, " & vbCrLf & " Field13,Field14,Field15,Field16,Field17,Field18,Field19 , Field20" & vbCrLf & " ) Values ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mPartyName & "', " & vbCrLf & " '" & mPartyAdd & "', " & vbCrLf & " '" & mPartyCity & "', " & vbCrLf & " '" & mPartyState & "', " & vbCrLf & " '" & mPartyPin & "', " & vbCrLf & " '" & mPartyPhone & "', " & vbCrLf & " '" & mBillNo & "', " & vbCrLf & " '" & mDebit & "', " & vbCrLf & " '" & mADV & "', " & vbCrLf & " '" & mDNOTE & "', " & vbCrLf & " '" & mCNOTE & "', " & vbCrLf & " '" & mTDS & "', " & vbCrLf & " '" & mCredit & "', " & vbCrLf & " '" & mBalance & "', " & vbCrLf & " '" & mDC & "', " & vbCrLf & " '" & mDueDate & "','" & mBillDate & "','" & mRemarks & "','" & mPaymentTerms & "', '" & mLastTrans & "')"
                PubDBCn.Execute(Sqlstr)
NextRec:
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(TxtDateTo.Text)) = False Then
        '        TxtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
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
End Class
