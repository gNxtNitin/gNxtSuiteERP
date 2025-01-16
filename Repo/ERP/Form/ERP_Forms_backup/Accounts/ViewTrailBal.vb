Option Strict Off
Option Explicit On
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Imports Microsoft.VisualBasic.Compatibility
Imports System.Data.OleDb
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Friend Class frmViewTrailBal
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean
    'Private PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 12


    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColAccountCode As Short = 3
    Private Const ColAcmName As Short = 4
    Private Const ColOpening As Short = 5
    Private Const ColCOpening As Short = 6
    Private Const ColDAmount As Short = 7
    Private Const ColCAmount As Short = 8
    Private Const ColDBAmount As Short = 9
    Private Const ColCBAmount As Short = 10
    Private Const ColCategory As Short = 11
    Private Const ColParentCode As Short = 12
    Private Const ColTopAccountName As Short = 13
    Private Const ColFlag As Short = 14
    Private Const ColCompanyName As Short = 15

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image

    Dim PrintFlag As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer
    Dim mClickProcess As Boolean
    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub CboCC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboCC.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cboCompany_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
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
        Me.Dispose()
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
            .Col = ColTopAccountName
            .Text = "Top Account Name"

            .Col = ColPicMain
            .Text = "Pic Main"
            .Col = ColPicSub
            .Text = "Pic Sub"
            .Col = ColFlag
            .Text = "Flag"

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

            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False
            .ColHidden = IIf(OptGroup(1).Checked = True, False, True)

            .Col = ColPicSub
            '.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            '.TypePictCenter = True
            '.TypePictMaintainScale = False
            '.TypePictStretch = False
            .ColHidden = True

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColTopAccountName
            .ColHidden = True

            .Col = ColAcmName
            .set_ColWidth(ColAcmName, 26)
            .ColsFrozen = ColAcmName

            .Col = ColOpening
            .set_ColWidth(ColOpening, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColCOpening
            .set_ColWidth(ColCOpening, 15)
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColDAmount
            .set_ColWidth(ColDAmount, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColCAmount
            .set_ColWidth(ColCAmount, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColDBAmount
            .set_ColWidth(ColDBAmount, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .Col = ColCBAmount
            .set_ColWidth(ColCBAmount, 15)
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
            .ColHidden = False
            .set_ColWidth(ColAccountCode, 10)

            .Col = ColParentCode
            .ColHidden = True
            .set_ColWidth(ColParentCode, 10)
            .Col = ColCompanyName
            .ColHidden = True
            .set_ColWidth(ColCompanyName, 10)

            If OptGroup(1).Checked = True Then
                MainClass.SetSpreadColor(SprdMain, -1, False)
                MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
                .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
                SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
                'SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            Else
                MainClass.SetSpreadColor(SprdMain, -1)
                MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
                .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
                SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
                SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            End If

            'Show the grid lines over the color
            '        SprdMain.BackColorStyle = BackColorStyleOverVertGridOnly
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
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColOpening
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColCOpening
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColDAmount
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColCAmount
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColDBAmount
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColCBAmount
                '.Font = VB6.FontChangeBold(.Font, mCategory)
                .FontBold = mCategory

                .Col = ColParentCode
                mParentcode = Val(.Text)
                If mCategory = True Then
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .Col2 = .MaxCols
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
        SqlStr = ""
        Call InsertPrintDummy()
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        If txtGroup.Visible = True Then     ''And chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked 
            mTitle = "Trial Balance" & " - " & txtGroup.Text
        ElseIf OptGroup(0).Checked = True Then
            mTitle = "Trial Balance" & " (Summerised) "
        ElseIf OptGroup(1).Checked = True Then
            mTitle = "Trial Balance"
        Else
            mTitle = Me.Text
        End If
        mSubTitle = "From: " & VB6.Format(txtDate(0).Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDate(1).Text, "DD MMM, YYYY")
        If OptGroup(1).Checked = True Then
            If optPrint(0).Checked = True Then
                mRPTName = "GroupTrailBal.Rpt"
            Else
                mRPTName = "GroupTrailBalAll.Rpt"
            End If
        Else
            If optPrint(0).Checked = True Then
                mRPTName = "TrailBal.Rpt"
            Else
                mRPTName = "TrailBalAll.Rpt"
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
        ''Resume
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim Printer As New Printer

        Dim mCompanyCode As Long = -1
        Dim mCompanyName As String

        Report1.SQLQuery = mSqlStr

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If lstCompanyName.GetItemChecked(0) = True Then
                mCompanyCode = -1
            Else
                For CntLst = 1 To lstCompanyName.Items.Count - 1
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        If mCompanyCode = -1 Then
                            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                            End If
                        Else
                            mCompanyCode = 0
                        End If
                    End If
                Next
            End If
            mCompanyCode = IIf(mCompanyCode = 0, -1, mCompanyCode)
            SetCrptForLedger(mCompanyCode, Report1, mMode, 1, mTitle, mSubTitle)
        Else
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        End If

        'SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt
        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            Exit For
        '        End If
        '    Next prt
        'End If
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
        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColAcmName
                mName = Replace(.Text, "'", "''")
                .Col = ColOpening
                mOpening = CStr(Val(.Text))
                .Col = ColCOpening
                mCOpening = CStr(Val(.Text))
                .Col = ColDAmount
                mDAmt = CStr(Val(.Text))
                .Col = ColCAmount
                mCAmt = CStr(Val(.Text))
                .Col = ColDBAmount
                mBalDAmt = CStr(Val(.Text))
                .Col = ColCBAmount
                mBalCAmt = CStr(Val(.Text))
                .Col = ColCategory
                mCategory = UCase(IIf(.Text = "", "G", .Text))
                .Col = ColParentCode
                mParentcode = Trim(.Text)
                'If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked Then
                '    If .RowHidden = True Then GoTo NextRow
                'Else
                If .RowHidden = True Or mName = "" Then GoTo NextRow
                'End If
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
            'Me.Text = "TRIAL BALANCE - SUMMERISED"
            If ViewTrialSumm() = False Then Exit Sub
        ElseIf OptGroup(1).Checked = True Then
            'If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '    Me.Text = "TRIAL BALANCE - " & txtGroup.Text
            '    If txtGroup.Text = "" Then
            '        MsgInformation("Please Enter the Group Name")
            '        txtGroup.Focus()
            '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '        Exit Sub
            '    End If
            'Else
            'Me.Text = "TRIAL BALANCE - GROUP WISE"
            'End If
            ViewTrialDetail()
        ElseIf OptGroup(2).Checked = True Then
            'Me.Text = "TRIAL BALANCE "
            If ViewTrial() = False Then Exit Sub
        Else
            If OptGroup(3).Checked = True Then
                'Me.Text = "TRIAL BALANCE - EXPENSES"
                mGroupType = "E"
            ElseIf OptGroup(4).Checked = True Then
                'Me.Text = "TRIAL BALANCE - GENERAL"
                mGroupType = "G"
            ElseIf OptGroup(5).Checked = True Then
                'Me.Text = "TRIAL BALANCE - DEBTORS"
                mGroupType = "D"
            Else
                'Me.Text = "TRIAL BALANCE - CREDITORS"
                mGroupType = "C"
            End If
            Call ViewTrialTypeWise(mGroupType)
        End If
        RowFormat()
        DisplayTotals()
        SprdMain.Refresh()
        FillSprdTrail()

        If OptGroup(1).Checked = True Then
            Call GroupBySpread(ColTopAccountName)
            Call SubTotal
        End If

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
    Private Sub SubTotal()
        On Error GoTo ERR1
        Dim mOpening As Double
        Dim mCOpening As Double
        Dim mDAmount As Double
        Dim mCAmount As Double
        Dim mDBAmount As Double
        Dim mCBAmount As Double
        Dim cntRow As Integer
        Dim mGroupCode As Long

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColCategory
                If Trim(.Text) = "G" Then
                    .Col = ColAccountCode
                    mGroupCode = Val(.Text) * -1

                    mOpening = 0
                    mCOpening = 0
                    mDAmount = 0
                    mCAmount = 0
                    mDBAmount = 0
                    mCBAmount = 0

                    If GroupSumQry(mGroupCode, mOpening, mCOpening, mDAmount, mCAmount, mDBAmount, mCBAmount) = True Then
                        .Col = ColOpening
                        .Text = mOpening

                        .Col = ColCOpening
                        .Text = mCOpening

                        .Col = ColDAmount
                        .Text = mDAmount

                        .Col = ColCAmount
                        .Text = mCAmount

                        .Col = ColDBAmount
                        .Text = mDBAmount

                        .Col = ColCBAmount
                        .Text = mCBAmount
                    End If


                End If
            Next
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
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
        Dim mAccountCode As String
        Dim mSuppCustCode As String
        Dim mDummayDebit As Double
        Dim mDummayCredit As Double
        Dim mDummayOPDebit As Double
        Dim mDummayOPCredit As Double
        Dim xDummyAmount As Double
        Dim xDC As String
        Dim mBookType As String
        Dim mMkey As String
        Dim mTotalAmount As Double
        'Dim mCurrDAmount As Double
        'Dim mCurrCAmount As Double
        With SprdMain
            If PubUserID = "A00001" Then
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColAcmName
                    mSuppCustCode = ""
                    mAccountCode = ""
                    If MainClass.ValidateWithMasterTable(Trim(.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                        mSuppCustCode = MasterNo
                    ElseIf MainClass.ValidateWithMasterTable(Trim(.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                    mDummayDebit = 0
                    mDummayCredit = 0
                    xDummyAmount = 0
                    mDummayOPDebit = 0
                    mDummayOPCredit = 0
                    mTotalAmount = 0
                    mDBAmount = 0
                    mCBAmount = 0
                    If GetDummyExpAmount(mSuppCustCode, mAccountCode, "", "", "", xDummyAmount, xDC, RsCompany.Fields("START_DATE").Value, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDate(0).Text)))) = True Then
                        If mSuppCustCode <> "" Then
                            If xDC = "D" Then
                                mDummayOPDebit = xDummyAmount
                            Else
                                mDummayOPCredit = xDummyAmount
                            End If
                        Else
                            If xDC = "C" Then
                                mDummayOPDebit = xDummyAmount
                            Else
                                mDummayOPCredit = xDummyAmount
                            End If
                        End If
                    End If
                    If GetDummyExpAmount(mSuppCustCode, mAccountCode, "", "", "", xDummyAmount, xDC, txtDate(0).Text, txtDate(1).Text) = True Then
                        If mSuppCustCode <> "" Then
                            If xDC = "D" Then
                                mDummayDebit = xDummyAmount
                            Else
                                mDummayCredit = xDummyAmount
                            End If
                        Else
                            If xDC = "C" Then
                                mDummayDebit = xDummyAmount
                            Else
                                mDummayCredit = xDummyAmount
                            End If
                        End If
                    End If
                    '                .Col = ColDAmount
                    '                mCurrDAmount = Val(.Text) + mDummayDebit
                    '
                    '                .Col = ColCAmount
                    '                mCurrCAmount = Val(.Text) + mDummayCredit
                    '
                    '                If mCurrDAmount < 0 Then
                    '                    mCurrCAmount = mCurrCAmount + mCurrDAmount
                    '                    mCurrDAmount = 0
                    '                End If
                    '
                    '                If mCurrCAmount < 0 Then
                    '                    mCurrDAmount = mCurrDAmount + mCurrCAmount
                    '                    mCurrCAmount = 0
                    '                End If
                    .Col = ColOpening
                    .Text = CStr(Val(.Text) + mDummayOPDebit)
                    mTotalAmount = Val(.Text)
                    .Col = ColCOpening
                    .Text = CStr(Val(.Text) + mDummayOPCredit)
                    mTotalAmount = mTotalAmount - Val(.Text)
                    .Col = ColDAmount
                    .Text = CStr(Val(.Text) + mDummayDebit) 'mCurrDAmount ''
                    mTotalAmount = mTotalAmount + Val(.Text)
                    .Col = ColCAmount
                    .Text = CStr(Val(.Text) + mDummayCredit) ''mCurrCAmount '
                    mTotalAmount = mTotalAmount - Val(.Text)
                    If mTotalAmount >= 0 Then
                        mDBAmount = mTotalAmount
                    Else
                        mCBAmount = System.Math.Abs(mTotalAmount)
                    End If
                    .Col = ColDBAmount
                    .Text = CStr(mDBAmount)
                    .Col = ColCBAmount
                    .Text = CStr(mCBAmount)
                Next
                mDummayDebit = 0
                mDummayCredit = 0
                xDummyAmount = 0
                mDummayOPDebit = 0
                mDummayOPCredit = 0
                mTotalAmount = 0
                mDBAmount = 0
                mCBAmount = 0
            End If
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
            .Col2 = .MaxCols
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
        Dim mCompanyCodeStr As String
        Dim pCompanyCodeStr As String

        SqlStr = "SELECT PicMain, PicSub, SUPP_CUST_CODE, SUPP_CUST_NAME, " & vbCrLf _
            & " DROpening, CROpening, DEBIT, CREDIT, DEBITBAL, CREDITBAL, CATEGORY, GROUPCODE, TopAccountName, Flag, COMPANY_NAME" & vbCrLf _
            & " FROM ("

        ''********SELECTION..........
        SqlStr = SqlStr & vbCrLf _
            & " SELECT '' PicMain,'' PicSub,TO_CHAR(ACM.SUPP_CUST_CODE) AS SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,  " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf _
            & " ELSE '0.00' END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf _
            & " ELSE '0.00' END AS CROpening, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END)) AS DEBIT, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END)) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS CREDITBAL, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE AS CATEGORY,TO_CHAR(ACM.GROUPCODE) AS GROUPCODE,'' TopAccountName,'0' Flag,'' AS COMPANY_NAME "

        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM,GEN_COMPANY_MST GEN, FIN_GROUP_MST GMST "
        ''********Joining..........
        If PubUserID = "A00001" Then
            SqlStr = SqlStr & vbCrLf _
                & " WHERE " & vbCrLf _
                & " GEN.COMPANY_CODE=TRN.COMPANY_CODE " & vbCrLf _
                & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
                & " AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)=ACM.SUPP_CUST_CODE "
        Else
            SqlStr = SqlStr & vbCrLf _
                & " WHERE " & vbCrLf _
                & " GEN.COMPANY_CODE=TRN.COMPANY_CODE " & vbCrLf _
                & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        End If

        SqlStr = SqlStr & vbCrLf _
                & " AND ACM.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
                & " AND ACM.GROUPCODE=GMST.GROUP_CODE "

        'SqlStr = SqlStr & vbCrLf & " AND ACM.ACCOUNT_HIDE='N"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN A1, FIN_SUPP_CUST_MST B1 WHERE A1.COMPANY_CODE=B1.COMPANY_CODE AND A1.ACCOUNTCODE=B1.SUPP_CUST_CODE AND ACCOUNT_HIDE='Y')"
        End If

        'If lstCompanyName.SelectedIndex > 0 Then
        '    mCompanyName = Trim(lstCompanyName.Text)
        '    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '        mCompanyCode = MasterNo
        '    End If
        '    SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_NAME='" & MainClass.AllowSingleQuote(mCompanyName) & "'"
        '    SqlStr = SqlStr & vbCrLf & " AND ACM.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND TRN.COMPANY_CODE=" & mCompanyCode & ""
        'End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            pCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_CODE IN " & pCompanyCodeStr & ""
        End If

        'SqlStr = SqlStr & vbCrLf _
        '        & " AND TRN.ACCOUNTCODE || TRN.COMPANY_CODE NOT IN (" & vbCrLf _
        '        & " SELECT DISTINCT OP_ACCOUNT || A.COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT  IS NOT NULL "

        'If mCompanyCodeStr <> "" Then
        '    pCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        '    SqlStr = SqlStr & vbCrLf & " AND A.COMPANY_CODE IN " & pCompanyCodeStr & ""
        'End If

        'SqlStr = SqlStr & vbCrLf & ")"

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YO'"
        End If

        If chkGroup(11).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YC'"
        End If


        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        mGroupOption = GetGroupOption()

        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        '    SqlStr = SqlStr & vbCrLf & "AND  (TRN.VNO IN ('JV00001','P00001'))"
        SqlStr = SqlStr & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY " & vbCrLf _
            & " ACM.SUPP_CUST_NAME,ACM.SUPP_CUST_TYPE,ACM.SUPP_CUST_CODE,ACM.GROUPCODE "  '',GEN.COMPANY_NAME

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & SelectOPQry("A")
        End If


        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ) ORDER BY " & vbCrLf _
            & " SUPP_CUST_NAME "

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
        Dim mGroupCode As String = ""
        Dim mDivisionCode As Double
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        ''********SELECTION..........
        SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', ACM.SUPP_CUST_CODE,  " & vbCrLf _
            & " -1*ACM.GROUPCODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf & " ELSE 0 END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf _
            & " ELSE 0 END AS CROpening, " & vbCrLf _
            & " SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END) AS DEBIT, " & vbCrLf _
            & " SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS CREDITBAL, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE,99 "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST GMST "
        ', CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT

        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND ACM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ACM.GROUPCODE=GMST.GROUP_CODE "

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        'SqlStr = SqlStr & vbCrLf _
        '        & " AND TRN.ACCOUNTCODE || TRN.COMPANY_CODE NOT IN (" & vbCrLf _
        '        & " SELECT DISTINCT OP_ACCOUNT || A.COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT  IS NOT NULL " & vbCrLf _
        '        & ")"

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YO'"
        End If

        If chkGroup(11).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YC'"
        End If

        'SqlStr = SqlStr & vbCrLf _
        '        & " AND -1 * ACM.GROUPCODE NOT IN (SELECT DISTINCT GROUP_CODE FROM FIN_GROUP_MST " & vbCrLf _
        '        & " WHERE STOCK_GROUP='Y' AND STOCK_HEAD_TYPE IN 'C')"

        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))

        'If ChkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If MainClass.ValidateWithMasterTable(txtGroup.Text, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mGroupCode = MasterNo
        '        SqlStr = SqlStr & vbCrLf & " AND ACM.GROUPCODE=" & Val(CStr(mGroupCode)) & ""
        '    End If
        'End If

        ''If txtGroup.Text.Trim <> "" Then
        'For Each r As UltraGridRow In txtGroup.CheckedRows
        '    If mGroupCode <> "" Then
        '        mGroupCode += "," & "" & r.Cells("GROUP_CODE").Value.ToString() & ""
        '    Else
        '        mGroupCode += "" & r.Cells("GROUP_CODE").Value.ToString() & ""
        '    End If
        'Next
        ''End If

        'If mGroupCode <> "" Then
        '    SqlStr = SqlStr & vbCrLf & " AND ACM.GROUPCODE IN (" & mGroupCode & ")"
        'End If

        SqlStr = SqlStr & vbCrLf _
            & " AND -1 * ACM.GROUPCODE||'' IN (SELECT DISTINCT PARENTCODE||'' FROM Temp_TrialBal WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " UNION ALL" & vbCrLf _
            & " SELECT DISTINCT ACCOUNTCODE||'' FROM Temp_TrialBal WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " )"


        '    If mDeptName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & "  And DEPT.DEPT_DESC = '" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "'"
        '    End If
        '
        '    If mCostCName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & " AND  COSTC.COST_CENTER_DESC = '" & MainClass.AllowSingleQuote(Trim(mCostCName)) & "'"
        '    End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        mGroupOption = GetGroupOption()

        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        SqlStr = SqlStr & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
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
    Private Function InsertIntoOPQry(ByRef mSqlStr As String) As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        'Dim mDeptName As String
        'Dim mCostCName As String
        Dim mOpeningStock As Double
        Dim mAccountName As String
        Dim mGroupCode As Long = -1
        Dim mAccountCode As String = ""
        Dim mCategoryCode As String
        Dim pCompanyCode As String
        Dim pCompanyName As String
        Dim SqlStr1 As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        Dim mFGOpeningStock As Double = 0
        Dim CntLst As Long

        Dim pSqlStrCat As String
        Dim RsTempCat As ADODB.Recordset = Nothing

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        End If

        pSqlStrCat = "SELECT DISTINCT OP_ACCOUNT ACCOUNT_CODE FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT  IS NOT NULL "

        If mCompanyCodeStr <> "" Then
            pSqlStrCat = pSqlStrCat & vbCrLf & " AND A.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        MainClass.UOpenRecordSet(pSqlStrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)


        mGroupCode = -1
        If RsTempCat.EOF = False Then
            Do While RsTempCat.EOF = False

                mAccountCode = IIf(IsDBNull(RsTempCat.Fields("ACCOUNT_CODE").Value), "", RsTempCat.Fields("ACCOUNT_CODE").Value)

                mAccountName = ""
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                End If

                mGroupCode = -1
                'mGroupCode = 4002102
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "GROUPCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGroupCode = MasterNo
                End If

                mOpeningStock = 0

                SqlStr = "SELECT DISTINCT A.CATEGORY_CODE, A.COMPANY_CODE " & vbCrLf _
                    & " FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT='" & mAccountCode & "'"

                If mCompanyCodeStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " And A.COMPANY_CODE IN " & mCompanyCodeStr & ""
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        pCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)
                        mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)


                        mOpeningStock = mOpeningStock + GetClosingBalance(pCompanyCode, mCategoryCode, "OP")


                        RsTemp.MoveNext()
                    Loop

                    If mOpeningStock > 0 Then
                        SqlStr1 = " VALUES ('" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                                & " '" & mAccountCode & "', -1* " & mGroupCode & ", '" & mAccountName & "', " & vbCrLf _
                                & " " & mOpeningStock & ",0,0,0," & mOpeningStock & ",0, " & vbCrLf _
                                & " 'O', 99 )"

                        SqlStr = mSqlStr & vbCrLf & SqlStr1
                        PubDBCn.Execute(SqlStr)
                    End If
                End If

                RsTempCat.MoveNext()
            Loop
        End If


        pSqlStrCat = "SELECT DISTINCT GM.COMPANY_NAME, CMST.COMPANY_CODE, SUPP_CUST_CODE, SUPP_CUST_NAME,CMST.GROUPCODE " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST CMST, FIN_GROUP_MST GMST, GEN_COMPANY_MST GM " & vbCrLf _
            & " WHERE  CMST.COMPANY_CODE=GM.COMPANY_CODE AND CMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.GROUPCODE=GMST.GROUP_CODE"

        pSqlStrCat = pSqlStrCat & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE ='YO'"

        If mCompanyCodeStr <> "" Then
            pSqlStrCat = pSqlStrCat & vbCrLf & " And CMST.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        pSqlStrCat = pSqlStrCat & vbCrLf _
            & " AND CMST.COMPANY_CODE||CMST.SUPP_CUST_CODE NOT IN ( " & vbCrLf _
            & " SELECT DISTINCT A.COMPANY_CODE||OP_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE AND OP_ACCOUNT  IS NOT NULL "

        If mCompanyCodeStr <> "" Then
            pSqlStrCat = pSqlStrCat & vbCrLf & " AND A.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        pSqlStrCat = pSqlStrCat & vbCrLf & ")"

        MainClass.UOpenRecordSet(pSqlStrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempCat.EOF = False Then
            Do While RsTempCat.EOF = False
                mAccountName = IIf(IsDBNull(RsTempCat.Fields("SUPP_CUST_NAME").Value), "", RsTempCat.Fields("SUPP_CUST_NAME").Value)
                mAccountCode = IIf(IsDBNull(RsTempCat.Fields("SUPP_CUST_CODE").Value), "", RsTempCat.Fields("SUPP_CUST_CODE").Value)
                pCompanyCode = IIf(IsDBNull(RsTempCat.Fields("COMPANY_CODE").Value), 0, RsTempCat.Fields("COMPANY_CODE").Value)
                pCompanyName = IIf(IsDBNull(RsTempCat.Fields("COMPANY_NAME").Value), "", RsTempCat.Fields("COMPANY_NAME").Value)
                mGroupCode = IIf(IsDBNull(RsTempCat.Fields("GROUPCODE").Value), -1, RsTempCat.Fields("GROUPCODE").Value)

                mOpeningStock = GetOpeningBal(pCompanyCode, VB6.Format(txtDate(1).Text, "DD-MMM-YYYY"), pCompanyName, 0, "", "N")    '' GetClosingBalance(pCompanyCode, mCategoryCode, "OP")

                If mOpeningStock > 0 Then
                    SqlStr1 = " VALUES ('" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                                & " '" & mAccountCode & "', -1* " & mGroupCode & ", '" & mAccountName & "', " & vbCrLf _
                                & " " & mOpeningStock & ",0,0,0," & mOpeningStock & ",0, " & vbCrLf _
                                & " 'O', 99 )"

                    SqlStr = mSqlStr & vbCrLf & SqlStr1
                    PubDBCn.Execute(SqlStr)
                End If
                RsTempCat.MoveNext()
            Loop
        End If

        InsertIntoOPQry = True
        Exit Function
ViewTrialErr:
        InsertIntoOPQry = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function

    Private Function SelectOPQry(ByRef pType As String) As String
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        'Dim mDeptName As String
        'Dim mCostCName As String
        Dim mOpeningStock As Double
        Dim mAccountName As String
        Dim pCompanyName As String
        Dim mGroupCode As Long = -1
        Dim mAccountCode As String = ""
        Dim mCategoryCode As String
        Dim pCompanyCode As String
        Dim SqlStr1 As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        Dim mFGOpeningStock As Double = 0
        Dim CntLst As Long
        Dim mGroupName As String
        Dim mParentGroupCode As Long
        Dim pSqlStrCat As String
        Dim RsTempCat As ADODB.Recordset = Nothing

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        End If

        pSqlStrCat = "SELECT DISTINCT OP_ACCOUNT ACCOUNT_CODE " & vbCrLf _
            & " FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE And B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT  IS NOT NULL"


        If mCompanyCodeStr <> "" Then
            pSqlStrCat = pSqlStrCat & vbCrLf & " AND A.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        MainClass.UOpenRecordSet(pSqlStrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)


        mGroupCode = -1
        If RsTempCat.EOF = False Then
            Do While RsTempCat.EOF = False

                mAccountCode = IIf(IsDBNull(RsTempCat.Fields("ACCOUNT_CODE").Value), "", RsTempCat.Fields("ACCOUNT_CODE").Value)

                mAccountName = ""
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                End If

                mGroupCode = -1
                mGroupName = ""
                mParentGroupCode = -1
                'mGroupCode = 4002102
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "GROUPCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGroupCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mGroupCode, "GROUP_CODE", "GROUP_NAME", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGroupName = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mGroupCode, "GROUP_CODE", "GROUP_NAME", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGroupName = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mGroupCode, "GROUP_CODE", "GROUP_PARENTCODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mParentGroupCode = Val(MasterNo) * -1
                End If

                mOpeningStock = 0

                SqlStr = "SELECT DISTINCT CATEGORY_CODE, A.COMPANY_CODE " & vbCrLf _
                    & " FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT='" & mAccountCode & "'"



                If mCompanyCodeStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " And A.COMPANY_CODE IN " & mCompanyCodeStr & ""
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        pCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)
                        mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)


                        mOpeningStock = mOpeningStock + GetClosingBalance(pCompanyCode, mCategoryCode, "OP")


                        RsTemp.MoveNext()
                    Loop

                    If mOpeningStock > 0 Then

                        If pType = "A" Then
                            SelectOPQry = SelectOPQry & vbCrLf _
                               & " UNION ALL" & vbCrLf _
                               & " SELECT '' PicMain,'' PicSub,TO_CHAR(" & mAccountCode & ") AS AccountCode, '" & mAccountName & "' AS GROUP_NAME,  " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DROpening, " & vbCrLf _
                               & " '0' AS CROpening, " & vbCrLf _
                               & " '0' AS DEBIT, " & vbCrLf _
                               & " '0' AS CREDIT, " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DEBITBAL, " & vbCrLf _
                               & " '0' AS CREDITBAL, " & vbCrLf _
                               & " 'O','" & mGroupCode & "','' TopAccountName,'0' Flag,'' CompanyName FROM DUAL"
                        Else
                            SelectOPQry = SelectOPQry & vbCrLf _
                               & " UNION ALL" & vbCrLf _
                               & " SELECT '' PicMain,'' PicSub,TO_CHAR(" & mGroupCode & ") AS AccountCode, '" & mGroupName & "' AS GROUP_NAME,  " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DROpening, " & vbCrLf _
                               & " '0' AS CROpening, " & vbCrLf _
                               & " '0' AS DEBIT, " & vbCrLf _
                               & " '0' AS CREDIT, " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DEBITBAL, " & vbCrLf _
                               & " '0' AS CREDITBAL, " & vbCrLf _
                               & " 'G','" & mParentGroupCode & "','' TopAccountName,'0' Flag,'' CompanyName FROM DUAL"
                        End If

                    End If
                End If

                RsTempCat.MoveNext()
            Loop
        End If

        pSqlStrCat = "SELECT DISTINCT GM.COMPANY_NAME, CMST.COMPANY_CODE, SUPP_CUST_CODE, SUPP_CUST_NAME,CMST.GROUPCODE,GROUP_NAME " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST CMST, FIN_GROUP_MST GMST, GEN_COMPANY_MST GM " & vbCrLf _
            & " WHERE  CMST.COMPANY_CODE=GM.COMPANY_CODE AND CMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.GROUPCODE=GMST.GROUP_CODE"

        pSqlStrCat = pSqlStrCat & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE ='YO'"

        If mCompanyCodeStr <> "" Then
            pSqlStrCat = pSqlStrCat & vbCrLf & " And CMST.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        pSqlStrCat = pSqlStrCat & vbCrLf _
            & " AND CMST.COMPANY_CODE||CMST.SUPP_CUST_CODE NOT IN ( " & vbCrLf _
            & " SELECT DISTINCT A.COMPANY_CODE||OP_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE AND OP_ACCOUNT  IS NOT NULL "

        If mCompanyCodeStr <> "" Then
            pSqlStrCat = pSqlStrCat & vbCrLf & " AND A.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        pSqlStrCat = pSqlStrCat & vbCrLf & ")"

        MainClass.UOpenRecordSet(pSqlStrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempCat.EOF = False Then
            Do While RsTempCat.EOF = False
                mAccountName = IIf(IsDBNull(RsTempCat.Fields("SUPP_CUST_NAME").Value), "", RsTempCat.Fields("SUPP_CUST_NAME").Value)
                mAccountCode = IIf(IsDBNull(RsTempCat.Fields("SUPP_CUST_CODE").Value), "", RsTempCat.Fields("SUPP_CUST_CODE").Value)
                pCompanyCode = IIf(IsDBNull(RsTempCat.Fields("COMPANY_CODE").Value), 0, RsTempCat.Fields("COMPANY_CODE").Value)
                pCompanyName = IIf(IsDBNull(RsTempCat.Fields("COMPANY_NAME").Value), "", RsTempCat.Fields("COMPANY_NAME").Value)
                mGroupCode = IIf(IsDBNull(RsTempCat.Fields("GROUPCODE").Value), -1, RsTempCat.Fields("GROUPCODE").Value)
                mGroupName = IIf(IsDBNull(RsTempCat.Fields("GROUP_NAME").Value), -1, RsTempCat.Fields("GROUP_NAME").Value)

                mOpeningStock = GetOpeningBal(pCompanyCode, VB6.Format(txtDate(1).Text, "DD-MMM-YYYY"), pCompanyName, 0, "", "N")    '' GetClosingBalance(pCompanyCode, mCategoryCode, "OP")

                If mOpeningStock > 0 Then

                    If pType = "A" Then
                        SelectOPQry = SelectOPQry & vbCrLf _
                               & " UNION ALL" & vbCrLf _
                               & " SELECT '' PicMain,'' PicSub,TO_CHAR(" & mAccountCode & ") AS AccountCode, '" & mAccountName & "' AS GROUP_NAME,  " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DROpening, " & vbCrLf _
                               & " '0' AS CROpening, " & vbCrLf _
                               & " '0' AS DEBIT, " & vbCrLf _
                               & " '0' AS CREDIT, " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DEBITBAL, " & vbCrLf _
                               & " '0' AS CREDITBAL, " & vbCrLf _
                               & " 'O','" & mGroupCode & "','' TopAccountName,'0' Flag,'' CompanyName FROM DUAL"
                    Else
                        SelectOPQry = SelectOPQry & vbCrLf _
                               & " UNION ALL" & vbCrLf _
                               & " SELECT '' PicMain,'' PicSub,TO_CHAR(" & mGroupCode & ") AS AccountCode, '" & mGroupName & "' AS GROUP_NAME,  " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DROpening, " & vbCrLf _
                               & " '0' AS CROpening, " & vbCrLf _
                               & " '0' AS DEBIT, " & vbCrLf _
                               & " '0' AS CREDIT, " & vbCrLf _
                               & " '" & mOpeningStock & "' AS DEBITBAL, " & vbCrLf _
                               & " '0' AS CREDITBAL, " & vbCrLf _
                               & " 'G','" & mParentGroupCode & "','' TopAccountName,'0' Flag,'' CompanyName FROM DUAL"
                    End If
                End If
                RsTempCat.MoveNext()
            Loop
        End If

        Exit Function
ViewTrialErr:

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
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        ''********SELECTION..........
        'If lstCompanyName.SelectedIndex > 0 Then
        SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', ACM.SUPP_CUST_CODE, -1*ACM.GROUPCODE, "
        'Else
        '    SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', -1, -1, "
        'End If

        SqlStr = SqlStr & vbCrLf & " ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf & " ELSE 0 END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END))" & vbCrLf & " ELSE 0 END AS CROpening, " & vbCrLf _
            & " SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END) AS DEBIT, " & vbCrLf _
            & " SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) ELSE 0 END AS CREDITBAL, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE, 99 "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST GEN," & vbCrLf _
            & " FIN_GROUP_MST ACMGROUP"
        '', CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT "
        ''********Joining..........
        If PubUserID = "A00001" Then
            SqlStr = SqlStr & vbCrLf & " WHERE GEN.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf _
                & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
                & " AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE " & vbCrLf & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE "
            '         SqlStr = SqlStr & vbCrLf & "AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)='00072'"
        Else
            SqlStr = SqlStr & vbCrLf & " WHERE GEN.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf _
                & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE " & vbCrLf _
                & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE "
        End If
        ''& vbCrLf _
        '& " AND TRN.COMPANY_CODE=COSTC.COMPANY_CODE(+) " & vbCrLf _
        '& " AND TRN.COSTCCODE=COSTC.COST_CENTER_CODE(+) " & vbCrLf _
        '& " AND TRN.COMPANY_CODE=DEPT.COMPANY_CODE(+) " & vbCrLf _
        '& " AND TRN.DEPTCODE=DEPT.DEPT_CODE(+) "
        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))
        SqlStr = SqlStr & vbCrLf & " AND ACMGROUP.GROUP_TYPE='" & mType & "'"

        'If lstCompanyName.SelectedIndex > 0 Then
        '    mCompanyName = Trim(lstCompanyName.Text)
        '    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '        mCompanyCode = MasterNo
        '    End If
        '    SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_NAME='" & MainClass.AllowSingleQuote(mCompanyName) & "'"
        '    SqlStr = SqlStr & vbCrLf & " AND ACM.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND TRN.COMPANY_CODE=" & mCompanyCode & ""
        'End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        'SqlStr = SqlStr & vbCrLf _
        '        & " AND TRN.ACCOUNTCODE || TRN.COMPANY_CODE NOT IN (" & vbCrLf _
        '        & " SELECT DISTINCT OP_ACCOUNT || A.COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT  IS NOT NULL " & vbCrLf _
        '        & ")"

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YO'"
        End If

        If chkGroup(11).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YC'"
        End If


        If chkExcludeIU.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.INTER_UNIT='N'"
        End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        '    If mDeptName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & "  AND DEPT.DEPT_DESC = '" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "'"
        '    End If
        '
        '    If mCostCName <> "ALL" Then
        '        SqlStr = SqlStr & vbCrLf & " AND  COSTC.COST_CENTER_DESC = '" & MainClass.AllowSingleQuote(Trim(mCostCName)) & "'"
        '    End If
        mGroupOption = GetGroupOption()

        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        ''    SqlStr = SqlStr & vbCrLf & "AND  (TRN.vno NOT BETWEEN 'JV00709' AND 'JV00742')"
        ''    SqlStr = SqlStr & vbCrLf & "AND  (TRN.vno BETWEEN 'JV00709' AND 'JV00742')"
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf _
        ''            & " ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_CODE,ACM.GROUPCODE, ACM.SUPP_CUST_TYPE "

        'If lstCompanyName.SelectedIndex > 0 Then
        SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_CODE,ACM.GROUPCODE, ACM.SUPP_CUST_TYPE"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME, ACM.SUPP_CUST_TYPE"
        'End If

        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_NAME "
        InsertTypeIntoTempQry = SqlStr
        Exit Function
ViewTrialErr:
        InsertTypeIntoTempQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function InsertGroupIntoTempQry(ByRef pGroupCode As Long) As String
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        'Dim mGroupCode As String


        SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " -1*GROUP_CODE, DECODE(GROUP_PARENTCODE,-1,1,-1)*GROUP_PARENTCODE, GROUP_NAME, " & vbCrLf _
                & " 0,0,0,0,0,0, " & vbCrLf _
                & " GROUP_CATEGORY, GROUP_SEQNO "

        SqlStr = SqlStr & vbCrLf _
                & " FROM FIN_GROUP_MST " & vbCrLf _
                & " WHERE GROUP_CATEGORY='G'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " START WITH  GROUP_PARENTCODE= " & pGroupCode & " " & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " CONNECT BY PRIOR GROUP_CODE || COMPANY_CODE =GROUP_PARENTCODE || COMPANY_CODE" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""




        '''********SELECTION..........
        'SqlStr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        '    & " -1*GROUP_CODE, DECODE(GROUP_PARENTCODE,-1,1,-1)*GROUP_PARENTCODE, GROUP_NAME, " & vbCrLf _
        '    & " 0,0,0,0,0,0, " & vbCrLf _
        '    & " GROUP_CATEGORY "

        '''********TABLEs..........
        'SqlStr = SqlStr & vbCrLf _
        '    & " FROM FIN_GROUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        ''''********ORDER BY CLAUSE..........
        ''If ChkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        ''    SqlStr = SqlStr & vbCrLf _
        ''        & "AND GROUP_NAME='" & MainClass.AllowSingleQuote(txtGroup.Text) & "' "
        ''End If

        'For Each r As UltraGridRow In txtGroup.CheckedRows
        '    If mGroupCode <> "" Then
        '        mGroupCode += "," & "" & r.Cells("GROUP_CODE").Value.ToString() & ""
        '    Else
        '        mGroupCode += "" & r.Cells("GROUP_CODE").Value.ToString() & ""
        '    End If
        'Next


        'If mGroupCode <> "" Then
        '    SqlStr = SqlStr & vbCrLf & " AND GROUP_CODE IN (" & mGroupCode & ")"
        'End If

        '''********ORDER BY CLAUSE..........
        'SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf _
        '    & " GROUP_SCHEDULENO,GROUP_NAME "
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

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String


        SqlStr = "SELECT PicMain, PicSub, AccountCode, GROUP_NAME, " & vbCrLf _
            & " DROpening, CROpening, DEBIT, CREDIT, DEBITBAL, CREDITBAL, GROUP_CATEGORY, GROUP_PARENTCODE, TopAccountName, Flag, CompanyName" & vbCrLf _
            & " FROM ("

        ''********SELECTION..........
        SqlStr = SqlStr & vbCrLf _
            & "SELECT '' PicMain,'' PicSub,TO_CHAR(ACMGROUP.GROUP_CODE) AS AccountCode,ACMGROUP.GROUP_NAME AS GROUP_NAME,  " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf _
            & " ELSE '0.00' END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf & " ELSE '0.00' END AS CROpening, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END)) AS DEBIT, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END)) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS CREDITBAL, " & vbCrLf _
            & " ACMGROUP.GROUP_CATEGORY,TO_CHAR(GROUP_PARENTCODE) AS GROUP_PARENTCODE,'' TopAccountName,'0' Flag,'' CompanyName"
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP " '',CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT "
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf
        'SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN A1, FIN_SUPP_CUST_MST B1 WHERE A1.COMPANY_CODE=B1.COMPANY_CODE AND A1.ACCOUNTCODE=B1.SUPP_CUST_CODE AND ACCOUNT_HIDE='Y')"
        End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE " & vbCrLf _
            & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE "

        'SqlStr = SqlStr & vbCrLf _
        '        & " AND TRN.ACCOUNTCODE || TRN.COMPANY_CODE  NOT IN (" & vbCrLf _
        '        & " SELECT DISTINCT OP_ACCOUNT || A.COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST A, FIN_PRINT_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND B.INV_TAKEN_FROM_STOCK='Y' AND OP_ACCOUNT  IS NOT NULL" & vbCrLf _
        '        & ")"

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YO'"
        End If

        If chkGroup(11).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_GROUP||STOCK_HEAD_TYPE <>'YC'"
        End If

        ''********Conditions..........
        mDeptName = MainClass.AllowSingleQuote(UCase(Trim(CboDept.Text)))
        mCostCName = MainClass.AllowSingleQuote(UCase(Trim(CboCC.Text)))
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        mGroupOption = GetGroupOption()

        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & ") "
        End If
        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If
        ''********GROUP BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY " & vbCrLf _
            & " ACMGROUP.GROUP_NAME,ACMGROUP.GROUP_CATEGORY,ACMGROUP.GROUP_CODE,GROUP_PARENTCODE "

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & SelectOPQry("G")
        End If


        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ) ORDER BY " & vbCrLf _
            & " GROUP_NAME "

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        ViewTrialSumm = True
        Exit Function
ViewTrialErr:
        ViewTrialSumm = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function GroupSumQry(ByRef mGroupCode As Long, ByRef mOpening As Double, ByRef mCOpening As Double, ByRef mDAmount As Double, ByRef mCAmount As Double, ByRef mDBAmount As Double, ByRef mCBAmount As Double) As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String



        ''********SELECTION..........
        SqlStr = "SELECT " & vbCrLf _
            & " SUM(OPDAMOUNT) AS DROpening, " & vbCrLf _
            & " SUM(OPCAMOUNT) AS CROpening, " & vbCrLf _
            & " SUM(DAmount) AS DEBIT, " & vbCrLf _
            & " SUM(CAmount) AS CREDIT, " & vbCrLf _
            & " SUM(CDAMOUNT) AS DEBITBAL, " & vbCrLf _
            & " SUM(CCAMOUNT) AS CREDITBAL "

        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf _
            & " FROM TEMP_TRIALBAL " '',CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT "
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE "
        'SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"


        SqlStr = SqlStr & vbCrLf _
            & " AND PARENTCODE IN (SELECT DISTINCT -1 * GROUP_CODE " & vbCrLf _
            & " FROM FIN_GROUP_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " START WITH  GROUP_CODE='" & mGroupCode & "'" & vbCrLf _
            & " CONNECT BY GROUP_PARENTCODE =PRIOR GROUP_CODE)"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mOpening = mOpening + IIf(IsDBNull(RsTemp.Fields("DROpening").Value), 0, RsTemp.Fields("DROpening").Value)
                mCOpening = mCOpening + IIf(IsDBNull(RsTemp.Fields("CROpening").Value), 0, RsTemp.Fields("CROpening").Value)
                mDAmount = mDAmount + IIf(IsDBNull(RsTemp.Fields("DEBIT").Value), 0, RsTemp.Fields("DEBIT").Value)
                mCAmount = mCAmount + IIf(IsDBNull(RsTemp.Fields("CREDIT").Value), 0, RsTemp.Fields("CREDIT").Value)
                mDBAmount = mDBAmount + IIf(IsDBNull(RsTemp.Fields("DEBITBAL").Value), 0, RsTemp.Fields("DEBITBAL").Value)
                mCBAmount = mCBAmount + IIf(IsDBNull(RsTemp.Fields("CREDITBAL").Value), 0, RsTemp.Fields("CREDITBAL").Value)
                RsTemp.MoveNext()
            Loop
        End If

        GroupSumQry = True


        Exit Function
        ''********SELECTION..........
        SqlStr = "SELECT " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)>=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf _
            & " ELSE '0.00' END AS DROpening, " & vbCrLf _
            & " CASE WHEN SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)<=0 THEN " & vbCrLf _
            & " TO_CHAR(ABS(SUM(CASE WHEN VDate<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,-1) END)))" & vbCrLf & " ELSE '0.00' END AS CROpening, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',1,0) END)) AS DEBIT, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN VDate>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN AMOUNT * DECODE(DC,'D',0,1) END)) AS CREDIT, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS DEBITBAL, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))<0 THEN TO_CHAR(ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1)))) ELSE '0.00' END AS CREDITBAL "

        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP " '',CST_CENTER_MST COSTC,PAY_DEPT_MST DEPT "
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE "
        'SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN A1, FIN_SUPP_CUST_MST B1 WHERE A1.COMPANY_CODE=B1.COMPANY_CODE AND A1.ACCOUNTCODE=B1.SUPP_CUST_CODE AND ACCOUNT_HIDE='Y')"
        End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE " & vbCrLf _
            & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE "
        ''********Conditions..........

        If chkPnLFlag.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PL_FLAG='N'"
        End If

        ''SqlStr = SqlStr & vbCrLf & " AND ACMGROUP.GROUP_CODE='" & mGroupCode & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND ACMGROUP.GROUP_CODE IN (SELECT DISTINCT GROUP_CODE " & vbCrLf _
            & " FROM FIN_GROUP_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " START WITH  GROUP_CODE='" & mGroupCode & "'" & vbCrLf _
            & " CONNECT BY GROUP_PARENTCODE =PRIOR GROUP_CODE)"

        SqlStr = SqlStr & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.ACCOUNTCODE"

        'SqlStr = SqlStr & vbCrLf _
        '    & " START WITH  GROUP_CODE='" & mGroupCode & "'" & vbCrLf _
        '    & " CONNECT BY GROUP_PARENTCODE =PRIOR GROUP_CODE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mOpening = mOpening + IIf(IsDBNull(RsTemp.Fields("DROpening").Value), 0, RsTemp.Fields("DROpening").Value)
                mCOpening = mCOpening + IIf(IsDBNull(RsTemp.Fields("CROpening").Value), 0, RsTemp.Fields("CROpening").Value)
                mDAmount = mDAmount + IIf(IsDBNull(RsTemp.Fields("DEBIT").Value), 0, RsTemp.Fields("DEBIT").Value)
                mCAmount = mCAmount + IIf(IsDBNull(RsTemp.Fields("CREDIT").Value), 0, RsTemp.Fields("CREDIT").Value)
                mDBAmount = mDBAmount + IIf(IsDBNull(RsTemp.Fields("DEBITBAL").Value), 0, RsTemp.Fields("DEBITBAL").Value)
                mCBAmount = mCBAmount + IIf(IsDBNull(RsTemp.Fields("CREDITBAL").Value), 0, RsTemp.Fields("CREDITBAL").Value)
                RsTemp.MoveNext()
            Loop


        End If

        GroupSumQry = True
        Exit Function
ViewTrialErr:
        GroupSumQry = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function ViewTrialDetail() As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mSqlStr As String
        Dim mGroupCode As Long
        Dim mGroupCodeStr As String
        Dim pAllUnSelect As Boolean

        'If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked Then
        mGroupCode = -1
        'Else
        '    If MainClass.ValidateWithMasterTable((txtGroup.Text), "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mGroupCode = -1 * MasterNo
        '    Else
        '        mGroupCode = -1
        '    End If
        'End If

        pAllUnSelect = True
        For Each r As UltraGridRow In txtGroup.CheckedRows
            pAllUnSelect = False
            Exit For
        Next

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_TrialBal NOLOGGING " & vbCrLf _
            & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        mSqlStr = "INSERT INTO TEMP_TRIALBAL (" & vbCrLf _
            & " USERID, ACCOUNTCODE, PARENTCODE, ACCOUNTNAME, " & vbCrLf _
            & " OPDAMOUNT , OPCAMOUNT, DAmount, CAmount, " & vbCrLf _
            & " CDAMOUNT, CCAMOUNT, CATEGORY, SEQ_NO) "

        SqlStr1 = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " -1*GROUP_CODE, DECODE(GROUP_PARENTCODE,-1,1,-1)*GROUP_PARENTCODE, GROUP_NAME, " & vbCrLf _
                & " 0,0,0,0,0,0, " & vbCrLf _
                & " GROUP_CATEGORY, GROUP_SEQNO "

        SqlStr1 = SqlStr1 & vbCrLf _
                & " FROM FIN_GROUP_MST " & vbCrLf _
                & " WHERE GROUP_CATEGORY='G'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND GROUP_PARENTCODE=-1"

        If pAllUnSelect = True Then
            For Each r As UltraGridRow In txtGroup.Rows
                mGroupCode = r.Cells("GROUP_CODE").Value.ToString()
                mGroupCodeStr = IIf(mGroupCodeStr = "", mGroupCode, mGroupCodeStr & "," & mGroupCode)
            Next
        Else
            For Each r As UltraGridRow In txtGroup.CheckedRows
                mGroupCode = r.Cells("GROUP_CODE").Value.ToString()
                mGroupCodeStr = IIf(mGroupCodeStr = "", mGroupCode, mGroupCodeStr & "," & mGroupCode)
            Next
        End If


        If mGroupCodeStr <> "" Then
            SqlStr1 = SqlStr1 & vbCrLf & " AND GROUP_CODE IN (" & mGroupCodeStr & ")"
        End If

        SqlStr = mSqlStr & vbCrLf & SqlStr1
        PubDBCn.Execute(SqlStr)

        If pAllUnSelect = True Then
            For Each r As UltraGridRow In txtGroup.Rows
                mGroupCode = r.Cells("GROUP_CODE").Value.ToString()

                SqlStr1 = InsertGroupIntoTempQry(mGroupCode)
                SqlStr = mSqlStr & vbCrLf & SqlStr1
                PubDBCn.Execute(SqlStr)
            Next
        Else
            For Each r As UltraGridRow In txtGroup.CheckedRows
                mGroupCode = r.Cells("GROUP_CODE").Value.ToString()

                SqlStr1 = InsertGroupIntoTempQry(mGroupCode)
                SqlStr = mSqlStr & vbCrLf & SqlStr1
                PubDBCn.Execute(SqlStr)
            Next
        End If


        SqlStr = ""
        SqlStr1 = InsertIntoTempQry()
        SqlStr = mSqlStr & vbCrLf & SqlStr1
        PubDBCn.Execute(SqlStr)

        ''UpDate Opening Query
        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Checked Then
            If InsertIntoOPQry(mSqlStr) = False Then GoTo ViewTrialErr
        End If


        PubDBCn.CommitTrans()

        SqlStr = ""

        ''********SELECTION..........
        SqlStr = SqlStr & vbCrLf _
                    & " SELECT '','', A.ACCOUNTCODE, LPAD(' ',4*(LEVEL-1)*DECODE(CATEGORY,'G',1,1.5)) ||  A.ACCOUNTNAME as ACCOUNTNAME1,  "

        SqlStr = SqlStr & vbCrLf _
                    & " ABS(NVL(OPDAMOUNT,0))," & vbCrLf _
                    & " ABS(NVL(OPCAMOUNT,0))," & vbCrLf _
                    & " ABS(NVL(DAmount,0))," & vbCrLf _
                    & " ABS(NVL(CAmount,0))," & vbCrLf _
                    & " ABS(NVL(CDAMOUNT,0))," & vbCrLf _
                    & " ABS(NVL(CCAMOUNT,0))," & vbCrLf _
                    & " A.CATEGORY,A.PARENTCODE,CONNECT_BY_ROOT ACCOUNTNAME TOPACCOUNTNAME,'0', '' "

        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM TEMP_TRIALBAL A"

        ''********WHERE CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
                    & " A.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " START WITH  PARENTCODE= -1  AND  A.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " CONNECT BY PRIOR A.ACCOUNTCODE= A.PARENTCODE AND A.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"


        SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS BY SEQ_NO,A.ACCOUNTNAME"

        'Dim mCount As Long

        'If pAllUnSelect = True Then
        '    For Each r As UltraGridRow In txtGroup.Rows
        '        mCount += 1
        '        mGroupCode = r.Cells("GROUP_CODE").Value.ToString()

        '        If mCount > 1 Then
        '            SqlStr = SqlStr & vbCrLf & " UNION ALL"
        '        End If

        '        ''********SELECTION..........
        '        SqlStr = SqlStr & vbCrLf _
        '            & " SELECT '','', A.ACCOUNTCODE, LPAD(' ',2*(LEVEL-1)) ||  A.ACCOUNTNAME as ACCOUNTNAME1,  "

        '        SqlStr = SqlStr & vbCrLf _
        '            & " ABS(NVL(OPDAMOUNT,0))," & vbCrLf _
        '            & " ABS(NVL(OPCAMOUNT,0))," & vbCrLf _
        '            & " ABS(NVL(DAmount,0))," & vbCrLf _
        '            & " ABS(NVL(CAmount,0))," & vbCrLf _
        '            & " ABS(NVL(CDAMOUNT,0))," & vbCrLf _
        '            & " ABS(NVL(CCAMOUNT,0))," & vbCrLf _
        '            & " A.CATEGORY,A.PARENTCODE,CONNECT_BY_ROOT ACCOUNTNAME TOPACCOUNTNAME,'0', '' "

        '        ''********TABLEs..........
        '        SqlStr = SqlStr & vbCrLf & " FROM TEMP_TRIALBAL A"

        '        ''********WHERE CLAUSE..........
        '        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
        '            & " A.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " START WITH  PARENTCODE=" & -1 * mGroupCode & " AND  A.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " CONNECT BY PRIOR A.ACCOUNTCODE= A.PARENTCODE AND A.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        '    Next
        'Else
        '    For Each r As UltraGridRow In txtGroup.CheckedRows
        '        mCount += 1
        '        mGroupCode = r.Cells("GROUP_CODE").Value.ToString()

        '        If mCount > 1 Then
        '            SqlStr = SqlStr & vbCrLf & " UNION ALL"
        '        End If

        '        ''********SELECTION..........
        '        SqlStr = SqlStr & vbCrLf _
        '            & " SELECT '','', A.ACCOUNTCODE, LPAD(' ',2*(LEVEL-1)) ||  A.ACCOUNTNAME as ACCOUNTNAME1,  "

        '        SqlStr = SqlStr & vbCrLf _
        '            & " ABS(NVL(OPDAMOUNT,0))," & vbCrLf _
        '            & " ABS(NVL(OPCAMOUNT,0))," & vbCrLf _
        '            & " ABS(NVL(DAmount,0))," & vbCrLf _
        '            & " ABS(NVL(CAmount,0))," & vbCrLf _
        '            & " ABS(NVL(CDAMOUNT,0))," & vbCrLf _
        '            & " ABS(NVL(CCAMOUNT,0))," & vbCrLf _
        '            & " A.CATEGORY,A.PARENTCODE,CONNECT_BY_ROOT ACCOUNTNAME TOPACCOUNTNAME,'0', '' "

        '        ''********TABLEs..........
        '        SqlStr = SqlStr & vbCrLf & " FROM TEMP_TRIALBAL A"

        '        ''********WHERE CLAUSE..........
        '        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
        '            & " A.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " START WITH  PARENTCODE=" & -1 * mGroupCode & " AND  A.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " CONNECT BY PRIOR A.ACCOUNTCODE= A.PARENTCODE AND A.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        '    Next
        'End If





        ''PARENTCODE= -1 AND

        ''********GROUP BY CLAUSE..........
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf _
        ''            & " ACCOUNTNAME,CATEGORY,ACCOUNTCODE"

        ''********ORDER BY CLAUSE..........
        'SqlStr = SqlStr & vbCrLf _
        '    & " ORDER SIBLINGS BY " & vbCrLf _
        '    & " ACCOUNTNAME" ''1

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

        'mSqlStr = "INSERT INTO TEMP_TRIALBAL (" & vbCrLf _
        '    & " USERID, ACCOUNTCODE, PARENTCODE, ACCOUNTNAME, " & vbCrLf _
        '    & " OPDAMOUNT , OPCAMOUNT, DAmount, CAmount, " & vbCrLf _
        '    & " CDAMOUNT, CCAMOUNT, CATEGORY, SEQ_NO) "


        SqlStr = ""
        mSqlStr = "INSERT INTO TEMP_TRIALBAL (" & vbCrLf & " USERID, ACCOUNTCODE, PARENTCODE, ACCOUNTNAME, " & vbCrLf _
            & " OPDAMOUNT , OPCAMOUNT, DAmount, CAmount, " & vbCrLf & " CDAMOUNT, CCAMOUNT, CATEGORY,SEQ_NO) "
        SqlStr = ""
        SqlStr1 = InsertTypeIntoTempQry(mType)
        SqlStr = mSqlStr & vbCrLf & SqlStr1
        PubDBCn.Execute(SqlStr)

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Checked Then
            If InsertIntoOPQry(mSqlStr) = False Then GoTo ViewTrialErr
        End If


        PubDBCn.CommitTrans()
        SqlStr = ""
        ''********SELECTION..........
        SqlStr = "SELECT '','',TO_CHAR(ACCOUNTCODE),ACCOUNTNAME as ACCOUNTNAME,  " & vbCrLf _
            & " TO_CHAR(ABS(NVL(OPDAMOUNT,0)))," & vbCrLf _
            & " TO_CHAR(ABS(NVL(OPCAMOUNT,0)))," & vbCrLf _
            & " TO_CHAR(ABS(NVL(DAmount,0)))," & vbCrLf _
            & " TO_CHAR(ABS(NVL(CAmount,0)))," & vbCrLf _
            & " TO_CHAR(ABS(NVL(CDAMOUNT,0)))," & vbCrLf _
            & " TO_CHAR(ABS(NVL(CCAMOUNT,0)))," & vbCrLf _
            & " CATEGORY,TO_CHAR(PARENTCODE),'','0','' "

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
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConBankBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConCashBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConSaleBook & "' OR TRN.BOOKTYPE = '" & ConSaleDebitBook & "' OR  TRN.BookType = '" & ConSaleCreditBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConPurchaseBook & "' OR TRN.BOOKTYPE = '" & ConGRBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConDebitNoteBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConCreditNoteBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(6).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConJournalBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(7).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConContraBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConPDCBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(9).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConOpeningBook & "'"
        Else
            mAllCheck = False
        End If
        If mAllCheck = True Then
            GetGroupOption = ""
        End If
        Exit Function
ErrPart:
        GetGroupOption = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub frmViewTrailBal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim BookType As String
        Dim SqlStr As String
        If FormLoaded = True Then Exit Sub
        OptGroup(2).Checked = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtDate(0).Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDate(1).Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Me.Text = "Trial Balance"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked
        FormLoaded = True
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub frmViewTrailBal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        optPrint(0).Checked = True
        MainClass.SetControlsColor(Me)
        FormatSprdTrail(-1)
        FillSprdTrail()
        FillComboBox()

        'initialize pictures
        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        FormLoaded = False
        Call frmViewTrailBal_Activated(eventSender, eventArgs)
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
        Dim CntLst As Long


        Dim RsTemp As ADODB.Recordset = Nothing
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        MainClass.FillCombo(CboCC, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL")
        MainClass.FillCombo(CboCC, "FIN_CCENTER_HDR", "CC_DESC", "ALL")
        MainClass.FillCombo(CboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL")
        CboCC.SelectedIndex = 0
        CboDept.SelectedIndex = 0
        OptGroup(2).Checked = True


        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        Dim mCompanyName As String
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_NAME").Value), "", RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = RsCompany.Fields("COMPANY_NAME").Value, True, False))
                'lstCompanyName.SetItemChecked(CntLst, False)
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



        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = " Select GROUP_NAME , GROUP_CODE  FROM FIN_GROUP_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_PARENTCODE=-1 ORDER BY GROUP_NAME"


        'SqlStr = " Select GROUP_NAME , GROUP_CODE" & vbCrLf _
        '    & " FROM FIN_GROUP_MST " & vbCrLf _
        '    & " WHERE GROUP_CATEGORY='G'" & vbCrLf _
        '    & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " START WITH  GROUP_PARENTCODE= -1 " & vbCrLf _
        '    & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " CONNECT BY PRIOR GROUP_CODE || COMPANY_CODE =GROUP_PARENTCODE || COMPANY_CODE" & vbCrLf _
        '    & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        txtGroup.DataSource = ds
        txtGroup.DataMember = ""
        Dim c As UltraGridColumn = Me.txtGroup.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        txtGroup.CheckedListSettings.CheckStateMember = "Selected"
        txtGroup.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        txtGroup.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        txtGroup.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        txtGroup.DisplayMember = "GROUP_NAME"
        txtGroup.ValueMember = "GROUP_CODE"

        txtGroup.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Group Name"
        txtGroup.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Group Code"

        txtGroup.DisplayLayout.Bands(0).Columns(0).Width = 450
        txtGroup.DisplayLayout.Bands(0).Columns(1).Hidden = True


        txtGroup.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList  'DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub frmViewTrailBal_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdMain.Width = VB6.TwipsToPixelsX(mReFormWidth - 100)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdMain, -1, IIf(OptGroup(1).Checked = True, False, True))
    End Sub
    Private Sub frmViewTrailBal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
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
                txtGroup.Visible = True
                'chkAllGroup.Visible = True
            Else
                txtGroup.Visible = False
                'chkAllGroup.Visible = False
            End If
            If Index = 3 Or Index = 4 Or Index = 5 Or Index = 6 Then
                chkExcludeIU.Enabled = True
            Else
                chkExcludeIU.Enabled = False
                chkExcludeIU.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
            'chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked
            PrintFlag = False
            PrintStatus()
        End If
    End Sub
    Private Sub TxtGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroup.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub TxtGroup_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroup.DoubleClick
        'SearchGroup()
    End Sub
    Private Sub TxtGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGroup.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtGroup.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtGroup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGroup.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        'If KeyCode = System.Windows.Forms.Keys.F1 Then SearchGroup()
    End Sub
    Private Sub TxtGroup_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGroup.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'If Trim(txtGroup.Text) = "" Then GoTo EventExitSub
        'If MainClass.ValidateWithMasterTable((txtGroup.Text), "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST ", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '    MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
        '    txtGroup.Focus()
        '    Cancel = True
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SetSubTotalColor(ByRef Row1 As Integer, ByRef Row2 As Integer, ByRef Col1 As Integer, ByRef col2 As Integer)
        With SprdMain
            .Row = Row1
            .Row2 = Row2
            .Col = Col1
            .Col2 = col2
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
        If MainClass.SearchMaster((txtGroup.Text), "FIN_GROUP_MST", "GROUP_NAME", SqlStr) = True Then
            txtGroup.Text = AcName
            txtGroup.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'If Trim(RsCompany.Fields("Company_Name").Value) = Trim(lstCompanyName.Text) Then
        Call ViewAccountLedger()
        'Else
        '    MsgInformation("Other Unit Account Ledger You Cann't See.")
        'End If
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then Call ViewAccountLedger()
    End Sub
    Private Sub ViewAccountLedger()
        On Error GoTo ErrPart
        If SprdMain.ActiveRow <= 0 Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedger.MdiParent = Me.MdiParent
        frmViewLedger.lblBookType.Text = "LEDG"
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColCategory
        If SprdMain.Text = "G" Or SprdMain.Text = "H" Then
            MsgInformation("Ledger no allowed for Group Or Head")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        SprdMain.Col = ColAcmName
        'frmViewLedger.cboAccount.Text = LTrim(RTrim(SprdMain.Text))
        'MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        'frmViewLedger.lblAcCode.Text = MasterNo


        frmViewLedger.txtDateFrom.Text = txtDate(0).Text
        frmViewLedger.txtDateTo.Text = txtDate(1).Text
        frmViewLedger.OptSumDet(2).Checked = True

        frmViewLedger.MdiParent = Me.MdiParent

        frmViewLedger.Show()

        frmViewLedger.cboAccount.Text = LTrim(RTrim(SprdMain.Text))
        MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        frmViewLedger.lblAcCode.Text = MasterNo
        frmViewLedger.cboDivision.Text = cboDivision.Text
        frmViewLedger.CboCC.Text = CboCC.Text
        frmViewLedger.CboDept.Text = CboDept.Text
        ''frmViewLedger.cboConsolidated.ListIndex = 3     ''DIVISION...
        frmViewLedger.frmViewLedger_Activated(Nothing, New System.EventArgs())
        frmViewLedger.cmdShow_Click(Nothing, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows	

        'Show Summary/Detail info.	
        'If clicked on a "+" or "-" grouping	

        If eventArgs.col = ColPicMain Then
            SprdMain.Col = ColPicMain
            SprdMain.Row = eventArgs.row
            If SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                'Show or hide the specified rows	
                ShowHideRows(ColPicMain, eventArgs.row)
            End If
        End If
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows	
        Dim i As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        SprdMain.Col = ColFlag

        If SprdMain.Text = "0" Then
            collapsetype = 0 'collape/hide rows : minus picture	
            SprdMain.Col = ColPicMain
            SprdMain.TypePictPicture = pluspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "1"
        Else
            collapsetype = 1 'uncollapse / show rows: plus picture	
            SprdMain.Col = ColPicMain
            SprdMain.TypePictPicture = minuspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "0"
        End If

        SprdMain.ReDraw = False
        For i = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next i
        SprdMain.ReDraw = True

    End Sub
    Private Sub BoldHeader(Col As Long)
        'Reset the header bolds and make the sort col bold

        'Change font for visual cue to what column sorting on
        'Reset all header fonts
        SprdMain.Row = 0
        SprdMain.Col = -1
        SprdMain.FontBold = False

        'Bold the specified column
        SprdMain.Row = 0
        SprdMain.Col = Col
        SprdMain.FontBold = True

    End Sub
    Private Sub SortData(Col As Long)
        'Sort the data on the specified column

        SprdMain.Sort(1, 1, SprdMain.MaxCols, SprdMain.DataRowCnt, FPSpreadADO.SortByConstants.SortByRow, Col, SS_SORT_ORDER_ASCENDING)

    End Sub



    Private Sub GroupBySpread(ByRef Col As Long)
        'Group the data by the specified column
        Dim i As Integer
        Dim currentrow As Long
        Dim lastid As String
        Dim prevtext As Object = Nothing
        Dim lastheaderrow As Long
        Dim ret As Boolean
        Dim Currentid As String

        'Turn off the redraw
        SprdMain.ReDraw = False

        'Reset the header bolds and make the sort col bold
        BoldHeader(Col)

        'Sort the data on the specified column
        'SortData(Col)

        'Reset the max columns to allow for the inserted "gouping" picture columns
        'SprdMain.MaxCols = SprdMain.MaxCols + 2
        'Insert 2 columns at beginning
        For i = 1 To 2
            'SprdMain.InsertCols(i, 1)

            'Change col width
            'SprdMain.colwidth(i) = 2
            SprdMain.set_ColWidth(i, 2)
        Next i

        'Change background color of the first inserted column
        SprdMain.Col = 1
        SprdMain.Row = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ' &H8000000F    'Gray

        'Init variables	
        lastheaderrow = 0
        currentrow = 1
        lastid = ""

        While currentrow <= SprdMain.DataRowCnt

            SprdMain.Row = currentrow
            SprdMain.Col = ColTopAccountName
            Currentid = UCase(Trim(SprdMain.Text))
            If Currentid <> lastid Then
                If lastheaderrow <> 0 Then
                    SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
                    '                mRecordCount = SprdMain.GetRowItemData(lastheaderrow)	
                End If

                lastid = UCase(Trim(SprdMain.Text))

                lastheaderrow = currentrow

                'Insert a new header row	
                InsertHeaderRow(currentrow, 0)
            Else
                MakePictureCellType((SprdMain.Row), ColPicSub)
                SprdMain.Col = ColPicSub
                SprdMain.TypePictPicture = minuspict
                SprdMain.SetCellBorder(ColPicSub, SprdMain.Row, ColPicSub, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
            End If
            SprdMain.Row = SprdMain.Row + 1
            currentrow = currentrow + 1

        End While

        'Display last read data	
        SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        SprdMain.MaxRows = SprdMain.DataRowCnt
        SprdMain.SetActiveCell(1, 1)

        'Paint Spread	
        SprdMain.ReDraw = True

        'Update displays	
        System.Windows.Forms.Application.DoEvents()

        ''Init variables
        'lastheaderrow = 0
        'currentrow = 1
        'lastid = " "


        ''Loop through all rows
        'While currentrow <= SprdMain.DataRowCnt

        '    SprdMain.Row = currentrow
        '    SprdMain.Col = Col   'adjust for 2 inserted cols
        '    'Compare Ids to see if new
        '    If UCase(Trim(SprdMain.Text)) <> lastid Then
        '        'New ID
        '        'Set the number of rows "associated" with the previous group
        '        If lastheaderrow <> 0 Then
        '            'Set the item data with the number of rows for this grouping
        '            SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
        '            prevtext = Nothing
        '            ret = SprdMain.GetText(ColPicSub, lastheaderrow, prevtext)
        '            'Set the header row text
        '            'SprdMain.SetText(ColAccountCode, lastheaderrow, prevtext & "     " & SprdMain.GetRowItemData(lastheaderrow) & " item(s)")
        '        End If

        '        'Init new variables
        '        SprdMain.Col = Col
        '        lastid = UCase(Trim(SprdMain.Text))
        '        lastheaderrow = currentrow

        '        'Insert a new header row
        '        InsertHeaderRow(currentrow, SprdMain.Text)

        '        'Update counters
        '        SprdMain.Row = SprdMain.Row + 1
        '        currentrow = currentrow + 1
        '        'Label4.Caption = currentrow
        '    End If

        '    'Add the picture for expanding/collapsing
        '    MakePictureCellType(SprdMain.Row, ColPicMain)
        '    SprdMain.Col = ColPicMain
        '    SprdMain.TypePictPicture = minuspict

        '    'Add left border

        '    SprdMain.SetCellBorder(ColAccountCode, SprdMain.Row, ColAccountCode, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

        '    currentrow = currentrow + 1

        'End While

        ''Display last read data
        'SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        'prevtext = Nothing
        'ret = SprdMain.GetText(ColPicSub, lastheaderrow, prevtext)
        ''SprdMain.SetText(ColPicSub, lastheaderrow, prevtext & "     " & SprdMain.GetRowItemData(lastheaderrow) & " item(s)")

        ''Set the max rows = number or records
        'SprdMain.MaxRows = SprdMain.DataRowCnt

        ''Make the first cell active
        'SprdMain.SetActiveCell(1, 1)


        ''Paint Spread
        'SprdMain.ReDraw = True

        ''Update displays
        ''pb1.Value = 0
        ''Label4.Caption = "0"
        ''DoEvents

        ''Screen.MousePointer = 0
    End Sub
    Private Sub MakePictureCellType(Row As Long, Col As Integer)
        'Define specified cell as type PICTURE

        SprdMain.Col = Col
        SprdMain.Row = Row

        SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture ' CellTypePicture
        SprdMain.TypePictCenter = True
        SprdMain.TypePictMaintainScale = False
        SprdMain.TypePictStretch = False

    End Sub


    Private Sub InsertHeaderRow(rownum As Long, coltext As String)
        'Insert a header row at the specifed location

        'SprdMain.InsertRows(rownum, 1)
        'SprdMain.MaxRows = SprdMain.MaxRows + 1

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ' &H8000000F   'Gray
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) ' &HC00000     'Blue
        SprdMain.FontBold = True

        MakePictureCellType(rownum, 1)

        SprdMain.Col = 1
        SprdMain.TypePictPicture = minuspict
        SprdMain.Col = ColPicSub
        SprdMain.Text = coltext

        'Add picture state values
        SprdMain.Col = ColFlag
        SprdMain.Text = "0"

        'Add Border

        SprdMain.SetCellBorder(1, rownum, SprdMain.MaxCols, rownum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

    End Sub
    Private Function GetClosingBalance(ByRef pCompanyCode As Long, ByRef pCategoryCode As String, ByRef pType As String) As Double

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mTableName As String
        Dim mToDate As String
        Dim RsTemp As ADODB.Recordset = Nothing




        If pType = "OP" Then
            mToDate = VB6.Format(txtDate(0).Text, "DD-MMM-YYYY")
        Else
            mToDate = VB6.Format(txtDate(1).Text, "DD-MMM-YYYY")
        End If

        mTableName = ConInventoryTable


        SqlStr = " SELECT ITEM.ITEM_CODE,"

        ''DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,DECODE(STOCK_TYPE,'FG',1,0))) * 
        SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), ITEM.ITEM_CODE,  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as CLOSING_BALANCE"


        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTableName & " INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, INV_GENERAL_MST GMST"

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID = 'WH'"


        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
            & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE "

        ''
        SqlStr = SqlStr & vbCrLf & " AND GMST.GEN_CODE = '" & pCategoryCode & "'"

        'SqlStr = SqlStr & vbCrLf & " AND GMST.PRD_TYPE = 'P'"

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE = " & pCompanyCode & ""


        'SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "


        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"



        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
            & "  ITEM.ITEM_CODE,  INV.COMPANY_CODE "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        GetClosingBalance = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetClosingBalance = GetClosingBalance + IIf(IsDBNull(RsTemp.Fields("CLOSING_BALANCE").Value), 0, RsTemp.Fields("CLOSING_BALANCE").Value)
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
InsertErr:
        GetClosingBalance = 0
        MsgBox(Err.Description)
        ''Resume
    End Function
End Class
