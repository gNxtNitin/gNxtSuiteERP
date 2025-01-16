Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewOutsDueDate
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
    Private Const ColDueDate As Short = 17
    Private Const ConRowHeight As Short = 15
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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
        If MainClass.SearchMaster((TxtName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr) = True Then
            TxtName.Text = AcName
        End If
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ERR1
        If Trim(TxtName.Text) = "" And optParty(0).Checked = True Then
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
        If OptSumDet(0).Checked = True Then
            ViewOuts()
        Else
            ViewOutsSummary()
        End If
        DisplayTotals()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView(ByRef Arow As Integer)
        On Error GoTo ErrPart
        With SprdView
            .MaxCols = ColDueDate
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
            If optParty(0).Checked = True Then
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
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewOutsDueDate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        Dim SqlStr As String
        Call SetMainFormCordinate(Me)
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormLoaded = False
        Call FillCombo()
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
        CboShowFor.Items.Add("Uncleared")
        CboShowFor.Items.Add("Cleared")
        CboShowFor.Items.Add("Both")
        CboShowFor.SelectedIndex = 0
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
    Private Sub frmViewOutsDueDate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub ViewOuts()
        On Error GoTo ERR1
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
        Dim mDivisionCode As Double
        mDAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mBalAmtStr = "" & mDAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " + " & mCNAmtStr & " + " & mCAmtStr & ""
        If OptCalcOn(0).Checked = True Then
            If optDays(0).Checked = True Then
                mDueDate = "GETPARTYPAYTERMSDAYS(" & RsCompany.Fields("COMPANY_CODE").Value & ",ACCOUNTCODE,BILLNO,BILLDATE,'P')"
            Else
                mDueDate = "GETPARTYPAYTERMSDAYS(" & RsCompany.Fields("COMPANY_CODE").Value & ",ACCOUNTCODE,BILLNO,BILLDATE,'M')"
            End If
        Else
            If optDays(0).Checked = True Then
                mDueDate = "DECODE(FROM_DAYS,NULL,0,FROM_DAYS)"
            Else
                mDueDate = "DECODE(TO_DAYS,NULL,0,TO_DAYS)"
            End If
        End If
        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"
        If OptSumDet(0).Checked = True Then
            SqlStr = " Select  ACM.SUPP_CUST_NAME," & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO, " & vbCrLf & " BillNo, BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mDAmtStr & ")) AS BILLAMOUNT, " & vbCrLf & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf & " TO_CHAR(ABS(" & mCAmtStr & "))  "
            If lblOutsType.Text = "P" Then
                SqlStr = SqlStr & " AS PAYMENT, "
            Else
                SqlStr = SqlStr & " AS RECEIPT, "
            End If
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, " & vbCrLf & " MIN(BillDate)  + MAX(" & mDueDate & ")  AS DUEDATE "
        Else
            SqlStr = " Select  ACM.SUPP_CUST_NAME," & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO, " & vbCrLf & " '' AS BillNo, '' AS BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mDAmtStr & ")) AS BILLAMOUNT, " & vbCrLf & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf & " TO_CHAR(ABS(" & mCAmtStr & "))  "
            If lblOutsType.Text = "P" Then
                SqlStr = SqlStr & " AS PAYMENT, "
            Else
                SqlStr = SqlStr & " AS RECEIPT, "
            End If
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, " & vbCrLf & " MIN(EXPDATE) + MAX(" & mDueDate & ") AS DUEDATE "
        End If
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_PAYTERM_MST PMST "
        SqlStr = SqlStr & vbCrLf & " WHERE TRN.Company_Code=Acm.Company_Code AND TRN.AccountCode=Acm.SUPP_CUST_Code " & vbCrLf & " AND TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        SqlStr = SqlStr & vbCrLf & " AND ACM.Company_Code=PMST.Company_Code(+) " & vbCrLf & " AND ACM.PAYMENT_CODE=PMST.PAY_TERM_CODE(+) "
        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & " AND ACM.SUPP_CUST_TYPE ='S'"
        Else
            SqlStr = SqlStr & " AND ACM.SUPP_CUST_TYPE ='C'"
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If optParty(0).Checked = True Then
            If MainClass.ValidateWithMasterTable(UCase(TxtName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If
        SqlStr = SqlStr & vbCrLf & " AND TRN.BILLDATE + CASE WHEN (BOOKTYPE='B' AND (TRNTYPE='O' OR TRNTYPE='A')) THEN 0 ELSE " & mDueDate & " END<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If optAsOn(0).Checked = True Then ''AS On By Bill...
            '        SqlStr = SqlStr & vbCrLf & " AND (TRN.BILLDATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' OR TRN.BILLDATE IS NULL OR TRN.BILLDATE='') "
            If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
                SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' OR BILLTYPE='D' OR BILLTYPE='C' OR TRNTYPE='B' THEN  TRN.BILLDATE + " & mDueDate & "  ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' OR BILLTYPE='D' OR BILLTYPE='C' OR TRNTYPE='B' THEN TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END "
            Else
                SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' THEN  TRN.BILLDATE + " & mDueDate & " ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' THEN TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END "
            End If
        Else
            If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
                SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' THEN  TRN.EXPDATE + " & mDueDate & "  ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' THEN TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END "
            Else
                SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN  TRN.BILLDATE + " & mDueDate & " ELSE TRN.EXPDATE END <= CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END "
            End If
        End If
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY BillNo, BillDate,ACM.SUPP_CUST_NAME, " & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO "
        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME, " & vbCrLf & " SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, " & vbCrLf & " SUPP_CUST_PIN, CONTACT_TELNO "
        End If
        If CboShowFor.Text = "Cleared" Then
            SqlStr = SqlStr & vbCrLf & " HAVING " & mBalAmtStr & "=0 "
        ElseIf CboShowFor.Text = "Uncleared" Then
            SqlStr = SqlStr & vbCrLf & " HAVING " & mBalAmtStr & "<>0 "
        End If
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME,BillDate,BillNo "
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ViewOutsSummary()
        On Error GoTo ERR1
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
        Dim mDivisionCode As Double
        If OptCalcOn(0).Checked = True Then
            If optDays(0).Checked = True Then
                mDueDate = "GETPARTYPAYTERMSDAYS(" & RsCompany.Fields("COMPANY_CODE").Value & ",ACCOUNTCODE,BILLNO,BILLDATE,'P')"
            Else
                mDueDate = "GETPARTYPAYTERMSDAYS(" & RsCompany.Fields("COMPANY_CODE").Value & ",ACCOUNTCODE,BILLNO,BILLDATE,'M')"
            End If
        Else
            If optDays(0).Checked = True Then
                mDueDate = "DECODE(FROM_DAYS,NULL,0,FROM_DAYS)"
            Else
                mDueDate = "DECODE(TO_DAYS,NULL,0,TO_DAYS)"
            End If
        End If
        SqlStr = " Select  TRN.PARTYNAME," & vbCrLf & " TRN.SUPP_CUST_ADDR,TRN.SUPP_CUST_CITY,TRN.SUPP_CUST_STATE, " & vbCrLf & " TRN.SUPP_CUST_PIN, TRN.SUPP_CUST_PHONE, " & vbCrLf & " '' AS BillNo, '' AS BillDate, " & vbCrLf & " TO_CHAR(SUM(TRN.BILLAMOUNT)) AS BILLAMOUNT, " & vbCrLf & " TO_CHAR(SUM(TRN.ADV)) AS ADV, " & vbCrLf & " TO_CHAR(SUM(TRN.DNOTE)) AS DNOTE, " & vbCrLf & " TO_CHAR(SUM(TRN.CNOTE)) AS CNOTE, " & vbCrLf & " TO_CHAR(SUM(TRN.TDS)) AS TDS, " & vbCrLf & " TO_CHAR(SUM(TRN.PAYMENT))  "
        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & " AS PAYMENT, "
        Else
            SqlStr = SqlStr & " AS RECEIPT, "
        End If
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(BALANCE * DECODE(DC,'DR',1,-1)))) AS BALANCE, " & vbCrLf & " CASE WHEN SUM(BALANCE * DECODE(DC,'DR',1,-1)) >=0 THEN 'DR' ELSE 'CR' END AS DC, " & vbCrLf & " MAX(EXPDATE) AS DUEDATE "
        SqlStr = SqlStr & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN, FIN_SUPP_CUST_MST CH, FIN_PAYTERM_MST PMST "
        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code=CH.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=CH.SUPP_CUST_Code " & vbCrLf & " AND CH.Company_Code=PMST.Company_Code(+) " & vbCrLf & " AND CH.PAYMENT_CODE=PMST.PAY_TERM_CODE(+) "
        If lblOutsType.Text = "P" Then
            SqlStr = SqlStr & " AND TRN.SUPP_CUST_TYPE ='S'"
        Else
            SqlStr = SqlStr & " AND TRN.SUPP_CUST_TYPE ='C'"
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If optParty(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PARTYNAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"
        End If
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.BILLDATE + DECODE(FROM_DAYS,NULL,0,FROM_DAYS)<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
        If optAsOn(0).Checked = True Then ''AS On By Bill...
            '        SqlStr = SqlStr & vbCrLf & " AND (TRN.BILLDATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' OR TRN.BILLDATE IS NULL OR TRN.BILLDATE='') "
            '        If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.BILLDATE+ " & mDueDate & " <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '        Else
            '            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' THEN  TRN.BILLDATE ELSE TRN.VDate END <= CASE WHEN BILLTYPE='B' THEN '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END "
            '        End If
        Else
            '        If CDate(txtDateTo.Text) < CDate(txtPaymentDate.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EXPDATE+ " & mDueDate & " <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '        Else
            '            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN  TRN.BILLDATE ELSE TRN.EXPDATE END <= CASE WHEN BILLTYPE='B' OR TRNTYPE='B' THEN '" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' ELSE '" & vb6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "' END "
            '        End If
        End If
        If CboShowFor.Text = "Cleared" Then
            SqlStr = SqlStr & vbCrLf & " AND BALANCE=0"
        Else
            SqlStr = SqlStr & vbCrLf & " AND BALANCE<>0"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.PARTYNAME, " & vbCrLf & " TRN.SUPP_CUST_ADDR,TRN.SUPP_CUST_CITY,TRN.SUPP_CUST_STATE, " & vbCrLf & " TRN.SUPP_CUST_PIN, TRN.SUPP_CUST_PHONE "
        If CboShowFor.Text = "Cleared" Then
            SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.BALANCE * DECODE(DC,'DR',1,-1))=0 "
        ElseIf CboShowFor.Text = "Uncleared" Then
            SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.BALANCE * DECODE(DC,'DR',1,-1))<>0 "
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.PARTYNAME"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub optParty_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optParty.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optParty.GetIndex(eventSender)
            TxtName.Enabled = IIf(Index = 0, True, False)
            cmdsearch.Enabled = IIf(Index = 0, True, False)
        End If
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub PrintCommand()
        CmdPreview.Enabled = PrintEnable
        cmdPrint.Enabled = PrintEnable
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
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
        mDate = InputBox("Please Enter Payment Due on Date :", "Payment Due on Date", VB6.Format(txtDateTo.Text, "DD/MM/YYYY"))
        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        '    If NewFlagsSetting = 1048600 Then   'Print ALL records(&H80018)
        '        Call InsertALLPrintDummy
        '    Else                            'Print Current Record (selection)
        Call InsertPrintDummy()
        '    End If
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        If chkReminderLetter.CheckState = System.Windows.Forms.CheckState.Checked Then
            mTitle = "Reminder Letter"
            MainClass.AssignCRptFormulas(Report1, "HLine1='Dear Sir,'")
            MainClass.AssignCRptFormulas(Report1, "HLine2='You are requested to send the following overdue payments immediately.'")
            MainClass.AssignCRptFormulas(Report1, "FLine1='Thanking You,'")
            MainClass.AssignCRptFormulas(Report1, "FLine2='Your sincerely,'")
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
        Else
            If lblOutsType.Text = "R" Then
                mRPTName = "OutStandingR.Rpt"
            Else
                mRPTName = "OutStanding.Rpt"
            End If
        End If
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
        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY Field1,TO_DATE(Field17,'DD/MM/YYYY'),Field7"
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
