Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTDSChallan
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection				
    Dim mAccountCode As String
    Private Const RowHeight As Short = 16

    Private Const ColRefNo As Short = 1
    Private Const ColFROMDATE As Short = 2
    Private Const ColTODATE As Short = 3
    Private Const ColBankName As Short = 4
    Private Const ColChallanNo As Short = 5
    Private Const ColChallanDate As Short = 6
    Private Const ColDeductAmt As Short = 7

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'"
        If MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtAccount.Text = AcName
            txtAccount_Validating(TxtAccount, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdShow.Click
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        LedgInfo()
        FormatSprdLedg()
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function				
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.Focus				
        '    If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function				
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateTo.Focus				


        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            mAccountCode = MasterNo
        Else
            MsgInformation("Please Select Account")
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmViewTDSChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmViewTDSChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub LedgInfo()
        On Error GoTo LedgError
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTable As String

        If optShow(0).Checked = True Then
            mTable = "TDS_CHALLAN"
        Else
            mTable = "TDS_CHALLAN_CORR"
        End If

        SqlStr = " Select TO_CHAR(REFNO,'00000') ,TO_CHAR(FROMDATE,'DD/MM/YYYY') AS FROMDATE, " & vbCrLf & " TO_CHAR(TODATE,'DD/MM/YYYY') AS TODATE," & vbCrLf & " BANKNAME, CHALLANNO, " & vbCrLf & " TO_CHAR(CHALLANDATE,'DD/MM/YYYY') AS ChallanDate, " & vbCrLf & " TO_CHAR(AMOUNT) As Amount" & vbCrLf & " FROM " & mTable & " TDSChallan, FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE TDSChallan.AccountCode = ACM.SUPP_CUST_CODE " & vbCrLf & " AND TDSChallan.Company_Code= ACM.Company_Code  " & vbCrLf & " AND TDSChallan.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TDSChallan.AccountCode = '" & mAccountCode & "'"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND TDSChallan.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TDSChallan.COMPANY_CODE,TDSChallan.REFNO"

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Sub FormatSprdLedg()
        With SprdLedg
            .MaxCols = ColDeductAmt
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRefNo, 8)

            .Col = ColFROMDATE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColFROMDATE, 8)

            .Col = ColTODATE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColTODATE, 8)

            .Col = ColChallanNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColChallanNo, 10)

            .Col = ColChallanDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColChallanDate, 8)

            .Col = ColBankName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankName, 19)


            .Col = ColDeductAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDeductAmt, 8)

            Call FillHeading()

            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdLedg, -1)

            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle				
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub frmViewTDSChallan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
    End Sub
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        On Error GoTo ERR1
        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub


        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertSelectedAcct()

        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "TDS Challan Ledger"
        mSubTitle = MainClass.AllowSingleQuote(TxtAccount.Text)

        mReportFileName = "TDSChallanLedger.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

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
    Private Sub InsertSelectedAcct()

        Dim mRefNo As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mBankName As String
        Dim mChallanNo As String
        Dim mChallanDate As String
        Dim mDeductAmt As String

        Dim SqlStr As String
        Dim cntRow As Integer

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()


        SqlStr = ""
        With SprdLedg

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColRefNo
                mRefNo = Trim(.Text)

                .Col = ColFROMDATE
                mFromDate = Trim(.Text)

                .Col = ColTODATE
                mToDate = Trim(.Text)

                .Col = ColBankName
                mBankName = Trim(.Text)

                .Col = ColChallanNo
                mChallanNo = Trim(.Text)

                .Col = ColChallanDate
                mChallanDate = Trim(.Text)

                .Col = ColDeductAmt
                mDeductAmt = Trim(.Text)

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3,Field4,Field5,Field6,Field7) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow + 1 & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRefNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mFromDate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mToDate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mBankName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mChallanNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mChallanDate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDeductAmt) & "')"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume				
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
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
        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData" & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY Field1,SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub FillHeading()


        With SprdLedg
            .Row = 0
            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColFROMDATE
            .Text = "From Date"

            .Col = ColTODATE
            .Text = "To Date"

            .Col = ColDeductAmt
            .Text = "Amount"

            .Col = ColBankName
            .Text = "Bank Name"

            .Col = ColChallanNo
            .Text = "Challan No"

            .Col = ColChallanDate
            .Text = "Challan Date"

        End With

    End Sub
End Class
