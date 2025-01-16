Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTrailBalExp
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean
    'Private PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 12
    Private Const ColAcmName As Short = 1
    Private Const ColOpening As Short = 2
    Private Const ColCOpening As Short = 3
    Private Const ColDAmount As Short = 4
    Private Const ColCAmount As Short = 5
    Private Const ColDBAmount As Short = 6
    Private Const ColCBAmount As Short = 7
    Private Const ColCategory As Short = 8
    Private Const ColAccountCode As Short = 9
    Private Const ColParentCode As Short = 10
    Private Const ColCompanyName As Short = 11
    Dim PrintFlag As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer
    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub CboCC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboCC.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cboCompany_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCompany.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub CboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub ChkAllGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkAllGroup.CheckStateChanged
        PrintFlag = False
        PrintStatus()
        TxtGroup.Enabled = IIf(chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub
    Private Sub ChkHideZeroBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkHideZeroBal.CheckStateChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub ChkHideZeroTrans_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkHideZeroTrans.CheckStateChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub FillSprdTrail()
        On Error GoTo ERR1
        With SprdMain
            .Row = 0
            .Col = 0
            .Text = "S.No."
            .Col = ColAcmName
            .Text = "Account Name"
            .Col = ColOpening
            .Text = "Debit Opening Balance"
            .Col = ColCOpening
            .Text = "Credit Opening Balance"
            .Col = ColDAmount
            .Text = "Debit Amount"
            .Col = ColCAmount
            .Text = "Credit Amount"
            .Col = ColDBAmount
            .Text = "Debit Balance"
            .Col = ColCBAmount
            .Text = "Credit Balance"
            .Col = ColCategory
            .Text = "Category"
            .Col = ColAccountCode
            .Text = "Code"
            .Col = ColParentCode
            .Text = "ParentCode"
            .Col = ColCompanyName
            .Text = "Company Name"
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FormatSprdTrail(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdMain
            .MaxCols = ColCompanyName
            .set_RowHeight(0, 2.5 * RowHeight)
            .Row = -1
            .set_ColWidth(0, 4)
            .Col = ColAcmName
            .set_ColWidth(ColAcmName, 40)
            .ColsFrozen = ColAcmName

            .Col = ColOpening
            .set_ColWidth(ColOpening, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColCOpening
            .set_ColWidth(ColCOpening, 12)
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColDAmount
            .set_ColWidth(ColDAmount, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColCAmount
            .set_ColWidth(ColCAmount, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColDBAmount
            .set_ColWidth(ColDBAmount, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColCBAmount
            .set_ColWidth(ColCBAmount, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColCategory
            .ColHidden = True
            .set_ColWidth(ColCategory, 0)
            .Col = ColAccountCode
            .ColHidden = True
            .set_ColWidth(ColAccountCode, 0)
            .Col = ColParentCode
            .ColHidden = True
            .set_ColWidth(ColParentCode, 10)
            .Col = ColCompanyName
            .ColHidden = True
            .set_ColWidth(ColCompanyName, 10)
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub RowFormat()
        On Error GoTo ERR1
        Dim mCategory As Boolean
        Dim cntRow As Integer
        Dim mParentcode As Integer
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCategory
                mCategory = IIf(.Text = "G", True, False)
                .Col = ColAcmName
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColOpening
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColCOpening
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColDAmount
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColCAmount
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColDBAmount
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColCBAmount
                .Font = VB6.FontChangeBold(.Font, mCategory)
                .Col = ColParentCode
                mParentcode = Val(.Text)
                If mCategory = True Then
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .col2 = .MaxCols
                    .BlockMode = True
                    If mParentcode = -1 Then
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                    Else
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&H80000018)
                    End If
                    .BlockMode = False
                End If
            Next
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTrailBal(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTrailBal(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForTrailBal(ByRef Mode As Crystal.DestinationConstants)
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
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        If TxtGroup.Visible = True And chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = "Trial Balance (Expenses)" & " - " & TxtGroup.Text
        ElseIf OptGroup(0).Checked = True Then
            mTitle = "Trial Balance (Expenses)" & " (Summerised) "
        ElseIf OptGroup(1).Checked = True Then
            mTitle = "Trial Balance (Expenses)"
        Else
            mTitle = Me.Text
        End If
        mSubTitle = "From: " & VB6.Format(txtDate(0).Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDate(1).Text, "DD MMM, YYYY")
        If OptGroup(1).Checked = True Then
            mRPTName = "GroupTrailBal.Rpt"
        Else
            mRPTName = "TrailBal.Rpt"
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
        ''Resume
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
    Private Sub InsertPrintDummy()
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mBalance As String
        Dim mSrn As Integer
        Dim mName As String
        Dim mOpening As String
        Dim mCOpening As String
        Dim mDAmt As String
        Dim mCAmt As String
        Dim mBalDAmt As String
        Dim mBalCAmt As String
        Dim mCategory As String
        Dim mParentcode As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColAcmName
                mName = Replace(.Text, "'", "''")
                .Col = ColOpening
                mOpening = .Text
                .Col = ColCOpening
                mCOpening = .Text
                .Col = ColDAmount
                mDAmt = .Text
                .Col = ColCAmount
                mCAmt = .Text
                .Col = ColDBAmount
                mBalDAmt = .Text
                .Col = ColCBAmount
                mBalCAmt = .Text
                .Col = ColCategory
                mCategory = UCase(IIf(.Text = "", "G", .Text))
                .Col = ColParentCode
                mParentcode = Trim(.Text)
                If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If .RowHidden = True Then GoTo NextRow
                Else
                    If .RowHidden = True Or mName = "" Then GoTo NextRow
                End If
                mSrn = mSrn + 1
                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mName & "', " & vbCrLf & " '" & Trim(mOpening) & "', " & vbCrLf & " '" & Trim(mCOpening) & "', " & vbCrLf & " '" & Trim(mDAmt) & "', " & vbCrLf & " '" & Trim(mCAmt) & "', " & vbCrLf & " '" & Trim(mBalDAmt) & "', " & vbCrLf & " '" & Trim(mBalCAmt) & "', " & vbCrLf & " '" & Trim(CStr(mSrn)) & "','" & Trim(mCategory) & "','" & Trim(mParentcode) & "') "
                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mGroupType As String
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)
        If OptGroup(0).Checked = True Then
            Me.Text = "TRIAL BALANCE (Expenses) - SUMMERISED"
            If ViewTrialSumm = False Then Exit Sub
        ElseIf OptGroup(1).Checked = True Then
            If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                Me.Text = "TRIAL BALANCE (Expenses) - " & TxtGroup.Text
                If TxtGroup.Text = "" Then
                    MsgInformation("Please Enter the Group Name")
                    TxtGroup.Focus()
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If
            Else
                Me.Text = "TRIAL BALANCE (Expenses) - GROUP WISE"
            End If
            ViewTrialDetail()
        ElseIf OptGroup(2).Checked = True Then
            Me.Text = "TRIAL BALANCE (Expenses) "
            If ViewTrial = False Then Exit Sub
        Else
            If OptGroup(3).Checked = True Then
                Me.Text = "TRIAL BALANCE (Expenses) - EXPENSES"
                mGroupType = "E"
            ElseIf OptGroup(4).Checked = True Then
                Me.Text = "TRIAL BALANCE (Expenses) - GENERAL"
                mGroupType = "G"
            ElseIf OptGroup(5).Checked = True Then
                Me.Text = "TRIAL BALANCE (Expenses) - DEBTORS"
                mGroupType = "D"
            Else
                Me.Text = "TRIAL BALANCE (Expenses) - CREDITORS"
                mGroupType = "C"
            End If
            Call ViewTrialTypeWise(mGroupType)
        End If
        RowFormat()
        DisplayTotals()
        SprdMain.Refresh()
        FillSprdTrail()
        SprdMain.Focus()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        PrintFlag = True
        PrintStatus()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub DisplayTotals()
        On Error GoTo ERR1
        Dim mOpening As Double
        Dim mCOpening As Double
        Dim mDAmount As Double
        Dim mCAmount As Double
        Dim mDBAmount As Double
        Dim mCBAmount As Double
        Dim cntRow As Integer
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColOpening
                mOpening = mOpening + CDbl(IIf(IsNumeric(.Text), .Text, 0))
                .Col = ColCOpening
                mCOpening = mCOpening + CDbl(IIf(IsNumeric(.Text), .Text, 0))
                .Col = ColDAmount
                mDAmount = mDAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))
                .Col = ColCAmount
                mCAmount = mCAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))
                .Col = ColDBAmount
                mDBAmount = mDBAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))
                .Col = ColCBAmount
                mCBAmount = mCBAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))
            Next
            Call MainClass.AddBlankfpSprdRow(SprdMain, ColAccountCode)
            .Row = .MaxRows
            .Col = ColAcmName
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False
            .Row = .MaxRows
            .Col = ColOpening
            .Text = VB6.Format(mOpening, "0.00")
            .Col = ColCOpening
            .Text = VB6.Format(mCOpening, "0.00")
            .Col = ColDAmount
            .Text = VB6.Format(mDAmount, "0.00")
            .Col = ColCAmount
            .Text = VB6.Format(mCAmount, "0.00")
            .Col = ColDBAmount
            .Text = VB6.Format(mDBAmount, "0.00")
            .Col = ColCBAmount
            .Text = VB6.Format(mCBAmount, "0.00")
            '        Call CalcRowTotal(SprdMain, ColOpening, 1, ColOpening, .MaxRows - 1, .MaxRows, ColOpening)
            '        Call CalcRowTotal(SprdMain, ColCOpening, 1, ColCOpening, .MaxRows - 1, .MaxRows, ColCOpening)
            '
            '        Call CalcRowTotal(SprdMain, ColDAmount, 1, ColDAmount, .MaxRows - 1, .MaxRows, ColDAmount)
            '        Call CalcRowTotal(SprdMain, ColCAmount, 1, ColCAmount, .MaxRows - 1, .MaxRows, ColCAmount)
            '
            '        Call CalcRowTotal(SprdMain, ColDBAmount, 1, ColDBAmount, .MaxRows - 1, .MaxRows, ColDBAmount)
            '        Call CalcRowTotal(SprdMain, ColCBAmount, 1, ColCBAmount, .MaxRows - 1, .MaxRows, ColCBAmount)
            FormatSprdTrail(-1)
        End With
        PrintStatus()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Function ViewTrial() As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim mDeptName As String
        Dim mCostCName As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mDivisionCode As Double
        ''********SELECTION..........
        SqlStr = "SELECT ACM.SUPP_CUST_NAME,  " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf & " ELSE '0.00' END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf _
            & " ELSE '0.00' END AS CROpening, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END)) AS DEBIT, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END)) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS DEBITBAL, " & vbCrLf & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS CREDITBAL, " & vbCrLf & " ACM.SUPP_CUST_TYPE AS CATEGORY,TO_CHAR(ACM.SUPP_CUST_CODE),TO_CHAR(ACM.GROUPCODE),GEN.COMPANY_NAME "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM,GEN_COMPANY_MST GEN "
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " GEN.COMPANY_CODE=TRN.COMPANY_CODE AND ACM.COMPANY_CODE=TRN.COMPANY_CODE " & vbCrLf & " AND ACM.SUPP_CUST_CODE=TRN.ACCOUNTCODE "
        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))
        '& " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '
        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If cboCompany.SelectedIndex > 0 Then
            mCompanyName = Trim(cboCompany.Text)
            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mCompanyCode = MasterNo
            End If
            SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_NAME='" & MainClass.AllowSingleQuote(mCompanyName) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ACM.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND TRN.COMPANY_CODE=" & mCompanyCode & ""
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        mGroupOption = GetGroupOption
        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        SqlStr = SqlStr & vbCrLf & " AND TRN.EXPDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " ACM.SUPP_CUST_NAME,ACM.SUPP_CUST_TYPE,ACM.SUPP_CUST_CODE,ACM.GROUPCODE,GEN.COMPANY_NAME "
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_NAME "
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        ViewTrial = True
        Exit Function
ViewTrialErr:
        ViewTrial = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function InsertIntoTempQry() As String
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim mDeptName As String
        Dim mCostCName As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mGroupCode As Double
        Dim mDivisionCode As Double
        ''********SELECTION..........
        SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', ACM.SUPP_CUST_CODE,  " & vbCrLf & " -1*ACM.GROUPCODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf & " ELSE 0 END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf & " ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf & " ELSE 0 END AS CROpening, " & vbCrLf _
            & " SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END) AS DEBIT, " & vbCrLf _
            & " SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS CREDITBAL, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM"
        ', CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT
        ''********Joining..........

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))
        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.EXPDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtGroup.Text, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mGroupCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND ACM.GROUPCODE=" & Val(CStr(mGroupCode)) & ""
            End If
        End If
        '    If mDeptName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & "  AND DEPT.DEPT_DESC = '" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "'"
        '    End If
        '
        '    If mCostCName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & " AND  COSTC.COST_CENTER_DESC = '" & MainClass.AllowSingleQuote(Trim(mCostCName)) & "'"
        '    End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        mGroupOption = GetGroupOption
        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_CODE,ACM.GROUPCODE, ACM.SUPP_CUST_TYPE "
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_NAME "
        InsertIntoTempQry = SqlStr
        Exit Function
ViewTrialErr:
        InsertIntoTempQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function InsertTypeIntoTempQry(ByRef mType As String) As String
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim mDeptName As String
        Dim mCostCName As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        ''********SELECTION..........
        SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', ACM.SUPP_CUST_CODE,  " & vbCrLf & " -1*ACM.GROUPCODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf & " ELSE 0 END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf _
            & " ELSE 0 END AS CROpening, " & vbCrLf _
            & " SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END) AS DEBIT, " & vbCrLf _
            & " SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS CREDITBAL, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, " & vbCrLf & " FIN_GROUP_MST ACMGROUP"
        '', CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT "
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " ACM.COMPANY_CODE=TRN.COMPANY_CODE(+) " & vbCrLf & " AND ACM.SUPP_CUST_CODE=TRN.ACCOUNTCODE(+) " & vbCrLf & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE(+) " & vbCrLf & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE(+) "
        ''& vbCrLf _
        '& " AND TRN.COMPANY_CODE=COSTC.COMPANY_CODE(+) " & vbCrLf _
        '& " AND TRN.COSTCCODE=COSTC.COST_CENTER_CODE(+) " & vbCrLf _
        '& " AND TRN.COMPANY_CODE=DEPT.COMPANY_CODE(+) " & vbCrLf _
        '& " AND TRN.DEPTCODE=DEPT.DEPT_CODE(+) "
        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))
        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.EXPDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND ACMGROUP.GROUP_TYPE='" & mType & "'"
        '    If mDeptName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & "  AND DEPT.DEPT_DESC = '" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "'"
        '    End If
        '
        '    If mCostCName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & " AND  COSTC.COST_CENTER_DESC = '" & MainClass.AllowSingleQuote(Trim(mCostCName)) & "'"
        '    End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        mGroupOption = GetGroupOption
        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_CODE,ACM.GROUPCODE, ACM.SUPP_CUST_TYPE "
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_NAME "
        InsertTypeIntoTempQry = SqlStr
        Exit Function
ViewTrialErr:
        InsertTypeIntoTempQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function InsertGroupIntoTempQry() As String
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        ''********SELECTION..........
        SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " -1*GROUP_CODE, DECODE(GROUP_PARENTCODE,-1,1,-1)*GROUP_PARENTCODE, GROUP_NAME, " & vbCrLf & " 0,0,0,0,0,0, " & vbCrLf & " GROUP_CATEGORY "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_GROUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''********ORDER BY CLAUSE..........
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND GROUP_NAME='" & MainClass.AllowSingleQuote(TxtGroup.Text) & "'"
        End If
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " GROUP_NAME "
        InsertGroupIntoTempQry = SqlStr
        Exit Function
ViewTrialErr:
        InsertGroupIntoTempQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function ViewTrialSumm() As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim mDeptName As String
        Dim mCostCName As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        ''********SELECTION..........
        SqlStr = "SELECT ACMGROUP.GROUP_NAME,  " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf & " ELSE '0.00' END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN EXPDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf & " ELSE '0.00' END AS CROpening, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END)) AS DEBIT, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN EXPDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END)) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS CREDITBAL, " & vbCrLf _
            & " ACMGROUP.GROUP_CATEGORY,TO_CHAR(ACMGROUP.GROUP_CODE),TO_CHAR(GROUP_PARENTCODE) "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP " '',CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT "
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " ACMGROUP.COMPANY_CODE=ACM.COMPANY_CODE(+) " & vbCrLf & " AND ACMGROUP.GROUP_CODE=ACM.GROUPCODE(+) " & vbCrLf & " AND ACM.COMPANY_CODE=TRN.COMPANY_CODE(+) " & vbCrLf & " AND ACM.SUPP_CUST_CODE=TRN.ACCOUNTCODE(+) "
        '& vbCrLf _
        '& " AND TRN.COSTCCODE=COSTC.COST_CENTER_CODE(+) " & vbCrLf _
        '& " AND TRN.DEPTCODE=DEPT.DEPT_CODE(+) "
        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))
        '    mConsolidated = IIf(cboConsolidated.ListIndex = -1, "D", Left(cboConsolidated.Text, 1))
        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    If mConsolidated = "R" Then
        '        SqlStr = SqlStr & vbCrLf & " And TRN.REGIONCODE=" & RsCompany.Fields("REGIONCODE").Value & ""
        '    ElseIf mConsolidated = "B" Then
        '        SqlStr = SqlStr & vbCrLf & " And TRN.CBranchCode=" & RsCompany.Fields("CBranchCode").Value & ""
        '    ElseIf mConsolidated = "D" Then
        '        SqlStr = SqlStr & vbCrLf & " And TRN.BranchCode=" & RsCompany.Fields("BranchCode").Value & ""
        '    End If
        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.EXPDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        '    If mDeptName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & "  AND DEPT.DEPT_DESC = '" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "'"
        '    End If
        '
        '    If mCostCName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & " AND  COSTC.COST_CENTER_DESC = '" & MainClass.AllowSingleQuote(Trim(mCostCName)) & "'"
        '    End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        mGroupOption = GetGroupOption
        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " ACMGROUP.GROUP_NAME,ACMGROUP.GROUP_CATEGORY,ACMGROUP.GROUP_CODE,GROUP_PARENTCODE "
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACMGROUP.GROUP_NAME "
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        ViewTrialSumm = True
        Exit Function
ViewTrialErr:
        ViewTrialSumm = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function ViewTrialDetail() As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mSqlStr As String
        Dim mGroupCode As Integer
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked Then
            mGroupCode = -1
        Else
            If MainClass.ValidateWithMasterTable((TxtGroup.Text), "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mGroupCode = -1 * MasterNo
            Else
                mGroupCode = -1
            End If
        End If
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM Temp_TrialBal NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        mSqlStr = "INSERT INTO TEMP_TRIALBAL (" & vbCrLf & " USERID, ACCOUNTCODE, PARENTCODE, ACCOUNTNAME, " & vbCrLf & " OPDAMOUNT , OPCAMOUNT, DAmount, CAmount, " & vbCrLf & " CDAMOUNT, CCAMOUNT, CATEGORY) "
        SqlStr1 = InsertGroupIntoTempQry
        SqlStr = mSqlStr & vbCrLf & SqlStr1
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        SqlStr1 = InsertIntoTempQry
        SqlStr = mSqlStr & vbCrLf & SqlStr1
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        SqlStr = ""
        ''********SELECTION..........
        SqlStr = "SELECT TO_CHAR(LPAD(' ',2*(LEVEL-1))) ||  ACCOUNTNAME as ACCOUNTNAME1,  "
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(NVL(OPDAMOUNT,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(OPCAMOUNT,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(DAmount,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(CAmount,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(CDAMOUNT,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(CCAMOUNT,0)))," & vbCrLf & " CATEGORY,TO_CHAR(ACCOUNTCODE),TO_CHAR(PARENTCODE) "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM TEMP_TRIALBAL "
        ''********WHERE CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " START WITH  PARENTCODE=" & mGroupCode & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR ACCOUNTCODE= PARENTCODE AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        ''********GROUP BY CLAUSE..........
        ''    SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf _
        '            & " ACCOUNTNAME,CATEGORY,ACCOUNTCODE"
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS BY " & vbCrLf & " ACCOUNTNAME "
        ''
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        ViewTrialDetail = True
        Exit Function
ViewTrialErr:
        PubDBCn.RollbackTrans() ''
        ViewTrialDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function ViewTrialTypeWise(ByRef mType As String) As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mSqlStr As String
        Dim mGroupCode As Integer
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM Temp_TrialBal NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        mSqlStr = "INSERT INTO TEMP_TRIALBAL (" & vbCrLf & " USERID, ACCOUNTCODE, PARENTCODE, ACCOUNTNAME, " & vbCrLf & " OPDAMOUNT , OPCAMOUNT, DAmount, CAmount, " & vbCrLf & " CDAMOUNT, CCAMOUNT, CATEGORY) "
        SqlStr = ""
        SqlStr1 = InsertTypeIntoTempQry(mType)
        SqlStr = mSqlStr & vbCrLf & SqlStr1
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        SqlStr = ""
        ''********SELECTION..........
        SqlStr = "SELECT ACCOUNTNAME as ACCOUNTNAME,  " & vbCrLf & " TO_CHAR(ABS(NVL(OPDAMOUNT,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(OPCAMOUNT,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(DAmount,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(CAmount,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(CDAMOUNT,0)))," & vbCrLf & " TO_CHAR(ABS(NVL(CCAMOUNT,0)))," & vbCrLf & " CATEGORY,TO_CHAR(ACCOUNTCODE),TO_CHAR(PARENTCODE) "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM TEMP_TRIALBAL "
        ''********WHERE CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        ''********GROUP BY CLAUSE..........
        ''    SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf _
        '            & " ACCOUNTNAME,CATEGORY,ACCOUNTCODE"
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACCOUNTNAME "
        ''
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        ViewTrialTypeWise = True
        Exit Function
ViewTrialErr:
        PubDBCn.RollbackTrans() ''
        ViewTrialTypeWise = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function GetGroupOption() As String
        On Error GoTo ErrPart
        Dim mAllCheck As Boolean
        GetGroupOption = ""
        mAllCheck = True
        If chkGroup(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConBankBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConCashBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConSaleBook & "'  OR TRN.BOOKTYPE = '" & ConSaleDebitBook & "' OR  TRN.BookType = '" & ConSaleCreditBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPurchaseBook & "' OR TRN.BookType = '" & ConGRBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConDebitNoteBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConCreditNoteBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(6).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConJournalBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(7).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConContraBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPDCBook & "'"
        Else
            mAllCheck = False
        End If
        If mAllCheck = True Then
            GetGroupOption = ""
        End If
        Exit Function
ErrPart:
        GetGroupOption = ""
        MsgBox(Err.Description)
    End Function
    Private Sub frmViewTrailBalExp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim BookType As String
        Dim SqlStr As String
        If FormLoaded = True Then Exit Sub
        OptGroup(2).Checked = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtDate(0).Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDate(1).Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Me.Text = "Trial Balance (Expense Wise)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked
        FormLoaded = True
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub frmViewTrailBalExp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)
        FormatSprdTrail(-1)
        FillSprdTrail()
        FillComboBox()
        FormLoaded = False
        Call frmViewTrailBalExp_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset
        Dim SqlStr As String
        '    MainClass.FillCombo CboCC, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL" ', "BranchCode=" & RsCompany.Fields("CBRANCHCODE & ""
        MainClass.FillCombo(CboCC, "FIN_CCENTER_HDR", "CC_DESC", "ALL")
        MainClass.FillCombo(CboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL")
        CboCC.SelectedIndex = 0
        CboDept.SelectedIndex = 0
        OptGroup(2).Checked = True
        cboCompany.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf & " ORDER BY COMPANY_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboCompany.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCompany.Items.Add(RS.Fields("Company_Name").Value)
                RS.MoveNext()
            Loop
        End If
        cboCompany.Text = RsCompany.Fields("Company_Name").Value
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
        '    cboConsolidated.Clear
        '    cboConsolidated.AddItem "COMPANY"
        '    cboConsolidated.AddItem "REGION"
        '    cboConsolidated.AddItem "BRANCH"
        '    cboConsolidated.AddItem "DIVISION"
        '    cboConsolidated.Enabled = True
        '
        '    If RsCompany.Fields("Type").Value = "R" Then
        '        cboConsolidated.ListIndex = 3
        '    ElseIf RsCompany.Fields("Type").Value = "B" Then
        '        cboConsolidated.ListIndex = 3
        '    ElseIf RsCompany.Fields("Type").Value = "D" Then
        '        ''cboConsolidated.Enabled = False
        '        cboConsolidated.ListIndex = 3
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewTrailBalExp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdMain.Width = VB6.TwipsToPixelsX(mReFormWidth - 100)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdMain, -1)
    End Sub
    Private Sub frmViewTrailBalExp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Close()
    End Sub
    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged
        Dim Index As Short = txtDate.GetIndex(eventSender)
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim Index As Short = txtDate.GetIndex(eventSender)
        If txtDate(Index).Text = "" Then GoTo EventExitSub
        If MainClass.ChkIsdateF(txtDate(Index)) = False Then Cancel = True : txtDate(Index).Focus() : GoTo EventExitSub
        If FYChk(CStr(CDate(txtDate(Index).Text))) = False Then Cancel = True : txtDate(Index).Focus() : GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub OptGroup_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptGroup.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptGroup.GetIndex(eventSender)
            If Index = 1 Then
                TxtGroup.Visible = True
                chkAllGroup.Visible = True
            Else
                TxtGroup.Visible = False
                chkAllGroup.Visible = False
            End If
            chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked
            PrintFlag = False
            PrintStatus()
        End If
    End Sub
    Private Sub TxtGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroup.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub TxtGroup_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroup.DoubleClick
        SearchGroup()
    End Sub
    Private Sub TxtGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGroup.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtGroup.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtGroup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtGroup.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchGroup()
    End Sub
    Private Sub TxtGroup_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGroup.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtGroup.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtGroup.Text), "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST ", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
            TxtGroup.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SetSubTotalColor(ByRef Row1 As Integer, ByRef Row2 As Integer, ByRef Col1 As Integer, ByRef col2 As Integer)
        With SprdMain
            .Row = Row1
            .Row2 = Row2
            .Col = Col1
            .col2 = col2
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0C0)
            .BlockMode = False
        End With
    End Sub
    Private Sub SearchGroup()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANy_CODE").Value & " AND GROUP_Category='G'"
        If MainClass.SearchMaster((TxtGroup.Text), "FIN_GROUP_MST", "GROUP_NAME", SqlStr) = True Then
            TxtGroup.Text = AcName
            TxtGroup.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Call ViewAccountLedger()
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then Call ViewAccountLedger()
    End Sub
    Private Sub ViewAccountLedger()
        On Error GoTo ErrPart
        If SprdMain.ActiveRow <= 0 Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedgerExp.MdiParent = Me.MdiParent
        frmViewLedgerExp.lblBookType.Text = "LEDG"
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColCategory
        If SprdMain.Text = "G" Or SprdMain.Text = "H" Then
            MsgInformation("Ledger no allowed for Group Or Head")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        SprdMain.Col = ColAcmName
        frmViewLedgerExp.TxtAccount.Text = LTrim(RTrim(SprdMain.Text))
        MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        frmViewLedgerExp.lblAcCode.Text = MasterNo
        frmViewLedgerExp.txtDateFrom.Text = txtDate(0).Text
        frmViewLedgerExp.txtDateTo.Text = txtDate(1).Text
        frmViewLedgerExp.OptSumDet(2).Checked = True
        frmViewLedgerExp.cboDivision.Text = cboDivision.Text
        frmViewLedgerExp.MdiParent = Me.MdiParent
        frmViewLedgerExp.Show()
        frmViewLedgerExp.CboCC.Text = CboCC.Text
        frmViewLedgerExp.CboDept.Text = CboDept.Text
        ''frmViewLedgerExp.cboConsolidated.ListIndex = 3     ''DIVISION...
        frmViewLedgerExp.frmViewLedgerExp_Activated(Nothing, New System.EventArgs())
        frmViewLedgerExp.cmdShow_Click(Nothing, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
End Class
