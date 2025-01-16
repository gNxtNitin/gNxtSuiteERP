Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb

Friend Class frmViewLedger
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColBillDate As Short = 6
    Private Const ColExpenseHead As Short = 7
    Private Const ColNarration As Short = 8
    Private Const ColDAmount As Short = 9
    Private Const ColCAmount As Short = 10
    Private Const ColBalance As Short = 11
    Private Const ColBalDC As Short = 12
    Private Const ColBillDetail As Short = 13
    Private Const ColChequeNo As Short = 14

    Private Const ColUnitName As Short = 15
    Private Const ColAdjustedAmount As Short = 16
    Private Const ColUnAdjustedAmount As Short = 17

    Private Const ColVendorCode As Short = 18
    Private Const ColDept As Short = 19
    Private Const ColEmp As Short = 20
    Private Const ColCostC As Short = 21
    Private Const ColExpDate As Short = 22


    Private Const ColClearDate As Short = 23
    Private Const ColAddUser As Short = 24
    Private Const ColAddDate As Short = 25
    Private Const ColModUser As Short = 26
    Private Const ColModDate As Short = 27

    Private Const ColMKEY As Short = 28
    Private Const ColSubRowNo As Short = 29
    Private Const ColBranch As Short = 30


    Private Const mPageWidth As Short = 232
    Private Const TabRefDate As Short = 0
    Private Const TabRefNo As Short = 15
    Private Const TabName As Short = 30
    Private Const TabDAmount As Short = 166
    Private Const TabCAmount As Short = 188
    Private Const TabBalance As Short = 210
    Dim ConShowActive As Boolean = False
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean
    Private Sub GetFormCaption(ByRef mBookName As String)
        Select Case mBookName
            Case ConCashBook
                Me.Text = "Cash Book"
            Case ConBankBook
                Me.Text = "Bank Book"
            Case ConPDCBook
                Me.Text = "PDC Book"
            Case ConJournalBook
                Me.Text = "Journal Book"
            Case ConContraBook
                Me.Text = "Contra Book"
            Case ConDebitNoteBook
                Me.Text = "Debit Note Book"
            Case ConCreditNoteBook
                Me.Text = "Credit Note Book"
            Case ConLedger
                If lblShow.Text = "D" Then
                    Me.Text = "Ledger (Debtors/Creditors)"
                ElseIf lblShow.Text = "O" Then
                    Me.Text = "Ledger (Other Than Debtors/Creditors)"
                Else
                    'Me.Text = "Ledger"
                End If
        End Select
    End Sub
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub CboCC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboCC.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub CboCC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboCC.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub chkShowExp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowExp.CheckStateChanged
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mVDate As String
        Dim mExpDate As String
        '        If OptSumDet(0).Checked = True Then
        '            If chkShowExp.CheckState = System.Windows.Forms.CheckState.Checked Then
        '                With SprdLedg
        '                    For cntRow = 2 To .MaxRows - 1
        '                        .Row = cntRow
        '                        .Col = ColVDate
        '                        mVDate = Trim(.Text)
        '                        .Col = ColExpDate
        '                        mExpDate = Trim(.Text)
        '                        If Trim(mVDate) = "" Or Trim(mExpDate) = "" Then GoTo NextRow
        '                        If CDate(mVDate) = CDate(mExpDate) Then
        '                            .Row = cntRow
        '                            .RowHidden = True
        '                        End If
        'NextRow:
        '                    Next
        '                End With
        '            Else
        '                With SprdLedg
        '                    For cntRow = 2 To .MaxRows - 1
        '                        .Row = cntRow
        '                        .RowHidden = False
        '                    Next
        '                End With
        '            End If
        '        End If
        Exit Sub
ErrPart:
    End Sub
    Private Sub txtCondAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCondAmount.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCondAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCondAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub CboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboEmp_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExpHead_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExpHead.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExpHead_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExpHead.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub chkAllAccount_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllAccount.CheckStateChanged
        Call PrintStatus(False)
        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAgtAccount.Enabled = False
            cmdAgtsearch.Enabled = False
        Else
            TxtAgtAccount.Enabled = True
            cmdAgtsearch.Enabled = True
        End If
    End Sub
    Private Sub chkGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroup.CheckStateChanged
        Dim Index As Short = chkGroup.GetIndex(eventSender)
        Call PrintStatus(False)
    End Sub
    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        FraAmountCond.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        PrintStatus(False)
    End Sub
    Private Sub ChkWithRunBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkWithRunBal.CheckStateChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cmdAgtsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAgtsearch.Click
        Call SearchAccounts(TxtAgtAccount)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdOptional_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOptional.Click
        FraOthers.Visible = Not FraOthers.Visible
        FraOthers.BringToFront()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean
        '    Dim x As Boolean
        '    x = SprdLedg.ExportToHTML("C:\FILE.HTML", False, "C:\LOGFILE.TXT")
        '    If x = True Then
        '        MsgBox ("Export complete.")
        '    Else
        '        MsgBox ("Export did not succeed.")
        '    End If
        '   Exit Sub
        If cboAccount.Text = "" Then PrintStatus = False Else PrintStatus = True
        frmPrintLedg.OptSelected.Enabled = PrintStatus
        If OptSumDet(1).Checked = True Or OptSumDet(2).Checked = True Then
            frmPrintLedg.fraPrintOption.Enabled = False
        Else
            frmPrintLedg.fraPrintOption.Enabled = True
        End If
        frmPrintLedg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToWindow, PubDBCn)
        frmPrintLedg.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        frmPrintLedg.Close()
    End Sub
    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants, ByRef pDBCn As ADODB.Connection)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String
        pDBCn.Errors.Clear()
        SqlStr = "DELETE FROM Temp_Ledger NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = ""
        If frmPrintLedg.OptSelected.Checked Then
            Call InsertSelectedAcct(pDBCn)
        ElseIf frmPrintLedg.OptAll.Checked Then
            Call InsertAllLederAcct(pDBCn, True, "ALL")
        ElseIf frmPrintLedg.OptGroup.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "ALL")
        ElseIf frmPrintLedg.OptExpenses.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "E")
        ElseIf frmPrintLedg.OptGeneral.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "G")
        ElseIf frmPrintLedg.OptDebtors.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "D")
        ElseIf frmPrintLedg.OptCreditors.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "C")
        ElseIf frmPrintLedg.optSalesPerson.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "S")
        End If
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Account Ledger"
        If OptSumDet(1).Checked = True Then
            mTitle = mTitle & " - Daily"
        ElseIf OptSumDet(2).Checked = True Then
            mTitle = mTitle & " - Monthly"
        End If
        If frmPrintLedg.OptGroup.Checked Then
            mTitle = mTitle & "  (" & frmPrintLedg.txtLedgerGroup.Text & ")"
        End If
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "  (" & TxtAgtAccount.Text & ")"
        End If
        If OptSumDet(0).Checked = True Then
            If frmPrintLedg.chkWideFormat.CheckState = System.Windows.Forms.CheckState.Checked Then
                mReportFileName = "Ledger.Rpt"
            Else
                mReportFileName = "Ledger_80.Rpt"
            End If
        Else
            mReportFileName = "LedgerSummary.Rpt"
        End If
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
    Private Sub InsertSelectedAcct(ByRef pDBCn As ADODB.Connection)
        Dim mVDate As String
        Dim mVNo As String
        Dim mAcctName As String
        Dim mNarration As String
        Dim mBillDetail As String
        Dim mChequeNo As String
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mOpening As Double
        Dim mClosing As String
        Dim mPartyName As String
        Dim mRunningBal As Double
        Dim mRunningBalTot As String
        Dim mCostC As String
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mBillDate As String
        Dim mPartyAddress As String
        Dim mVoucherType As String
        Dim mBookType As String
        Dim mBookSubType As String

        On Error GoTo ERR1
        pDBCn.Errors.Clear()
        pDBCn.BeginTrans()
        mPartyName = cboAccount.Text
        Dim mRow As UltraGridRow
        Dim lngRow As Long, lngCount As Long


        '" & MainClass.AllowSingleQuote(Trim(mPartyAddress)) & "'

        mPartyAddress = ""
        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ' - ' || SUPP_CUST_PIN || ', ' || SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyAddress = MasterNo
        End If
        SqlStr = ""

        For lngRow = 0 To UltraGrid1.Rows.Count - 1 '' UltraDataSource1.Rows.Count - 1





            cntRow = lngRow
            mRow = UltraGrid1.Rows(lngRow)

            mVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))
            mVNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1))
            mBillDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1))

            mAcctName = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColExpenseHead - 1))

            If frmPrintLedg.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                mNarration = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColNarration - 1))      '' VB.Left(.Text, IIf(mDOSPRINTING = True, 250, 250))
                mNarration = Mid(mNarration, 1, 250)
            Else
                mNarration = ""
            End If

            If frmPrintLedg.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                mChequeNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1))      ''
                mChequeNo = IIf(Trim(mChequeNo) = "", "", "Chq. No. & Date : ") & mChequeNo
            Else
                mChequeNo = ""
            End If

            If IsNumeric(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))) Then
                mDAmt = CDbl(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1)))
            Else
                mDAmt = 0
            End If

            If IsNumeric(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))) Then
                mCAmt = CDbl(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1)))
            Else
                mCAmt = 0
            End If

            mRunningBal = mRunningBal + (mDAmt - mCAmt)
            mRunningBalTot = MainClass.FormatRupees(System.Math.Abs(mRunningBal)) & IIf(mRunningBal >= 0, "Dr", "Cr")
            mCostC = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCostC - 1))


            If frmPrintLedg.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
                mBillDetail = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDetail - 1))
                mBillDetail = Mid(mBillDetail, 1, 250)
            Else
                mBillDetail = ""
            End If

            mBookType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1))
            mBookSubType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookSubType - 1))

            If mBookType = "B" Then
                mVoucherType = "BANK " & IIf(mBookSubType = "P", "PAYMENT", "RECEIPT")
            ElseIf mBookType = "C" Then
                mVoucherType = "CASH " & IIf(mBookSubType = "P", "PAYMENT", "RECEIPT")
            ElseIf mBookType = "J" Then
                mVoucherType = "JOURNAL"
            ElseIf mBookType = "S" Then
                mVoucherType = "SALE"
            ElseIf mBookType = "P" Then
                mVoucherType = "PURCHASE"
            ElseIf mBookType = "L" Then
                mVoucherType = "CREDIT NOTE"
            ElseIf mBookType = "U" Then
                mVoucherType = "SUPPLIMENTRY"
            ElseIf mBookType = "E" Then
                mVoucherType = "DEBIT NOTE"
            ElseIf mBookType = "R" Then
                mVoucherType = "CREDIT NOTE"
            ElseIf mBookType = "M" Then
                mVoucherType = "DEBIT NOTE"
            Else
                mVoucherType = ""
            End If




            '  

            SqlStr = "Insert into Temp_Ledger ( " & vbCrLf _
                & " UserID,SubRow,PARTYNAME,VDATE, " & vbCrLf _
                & " VNO,NARRATION,DAmount,CAmount, " & vbCrLf _
                & " CHQNO,ACCOUNTNAME,COSTCNAME,BILLDETAIL,PARTY_ADD, VOUCHER_TYPE " & vbCrLf _
                & " ) Values (" & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " " & cntRow & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mPartyName)) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & Trim(mVNo) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mNarration)) & "', " & vbCrLf _
                & " " & Val(CStr(mDAmt)) & ", " & vbCrLf _
                & " " & Val(CStr(mCAmt)) & ",'" & mChequeNo & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mCostC)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mBillDetail)) & "','" & MainClass.AllowSingleQuote(Trim(mPartyAddress)) & "','" & mVoucherType & "')"

            pDBCn.Execute(SqlStr)
NextRow:
        Next

        pDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume
        pDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub InsertAllLederAcctOld(ByRef pDBCn As ADODB.Connection, ByRef pAllAccount As Boolean, ByRef pGroupType As String)
        On Error GoTo LedgError
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim SqlStr As String
        Dim InsertSqlStr As String
        Dim mGroupOption As String
        Dim RS As ADODB.Recordset
        Dim mCntCount As Integer
        Dim mAccountName As String
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim RsOP As ADODB.Recordset
        Dim mPartyAdd As String

        pDBCn.Errors.Clear()
        pDBCn.BeginTrans()
        SqlStr1 = ""
        SqlStr2 = ""
        SqlStr3 = ""
        SqlStr = ""
        InsertSqlStr = ""
        lblPrintCount.Visible = True
        lblPrintCount.Text = ""

        If optAccount.Checked = True Then
            If pAllAccount = True Then
                SqlStr = "SELECT SUPP_CUST_CODE,SUPP_CUST_NAME, SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ' - ' || SUPP_CUST_PIN || ', ' || SUPP_CUST_STATE PARTY_ADD FROM FIN_SUPP_CUST_MST " & vbCrLf _
                & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                & " And FIN_SUPP_CUST_MST.SUPP_CUST_TYPE NOT IN ('1','2') ORDER BY SUPP_CUST_NAME"
            Else
                SqlStr = "SELECT SUPP_CUST_CODE,SUPP_CUST_NAME, SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ' - ' || SUPP_CUST_PIN || ', ' || SUPP_CUST_STATE PARTY_ADD FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST" & vbCrLf _
                & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_GROUP_MST.COMPANY_CODE" & vbCrLf _
                & " AND FIN_SUPP_CUST_MST.GROUPCODE=FIN_GROUP_MST.GROUP_CODE" & vbCrLf _
                & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                & " And FIN_SUPP_CUST_MST.SUPP_CUST_TYPE NOT IN ('1','2')"

                If pGroupType <> "ALL" Then
                    If pGroupType = "S" Then
                        SqlStr = SqlStr & vbCrLf & " AND FIN_SUPP_CUST_MST.RESPONSIBLE_PERSON='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtSalesPerson.Text)) & "'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GROUP_TYPE='" & pGroupType & "'"
                    End If
                Else
                    SqlStr = SqlStr & vbCrLf & " AND FIN_GROUP_MST.GROUP_NAME='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtLedgerGroup.Text)) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY SUPP_CUST_NAME"
            End If
        Else
            If pAllAccount = True Then
                SqlStr = "SELECT GROUP_CODE AS SUPP_CUST_CODE,GROUP_NAME AS SUPP_CUST_NAME, '' PARTY_ADD FROM FIN_GROUP_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                    & " ORDER BY GROUP_NAME"
            Else
                SqlStr = "SELECT GROUP_CODE SUPP_CUST_CODE, GROUP_NAME SUPP_CUST_NAME, '' PARTY_ADD FROM FIN_GROUP_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & ""

                If pGroupType <> "ALL" Then
                    If pGroupType = "S" Then
                        SqlStr = SqlStr & vbCrLf & " AND 1=2"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GROUP_TYPE='" & pGroupType & "'"
                    End If
                Else
                    SqlStr = SqlStr & vbCrLf & " AND GROUP_NAME='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtLedgerGroup.Text)) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY GROUP_NAME"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While Not RS.EOF
                mAccountCode = IIf(IsDBNull(RS.Fields("SUPP_CUST_CODE").Value), "-1", RS.Fields("SUPP_CUST_CODE").Value)
                mAccountName = IIf(IsDBNull(RS.Fields("SUPP_CUST_NAME").Value), "-1", RS.Fields("SUPP_CUST_NAME").Value)
                mPartyAdd = IIf(IsDBNull(RS.Fields("PARTY_ADD").Value), "", RS.Fields("PARTY_ADD").Value)

                mCntCount = mCntCount + 1
                lblPrintCount.Text = mCntCount & " - " & Trim(Replace(mAccountName, "&", "&&"))
                System.Windows.Forms.Application.DoEvents()
                'Get Opening Balance.........
                SqlStr = MakeOPSQL(mAccountCode)
                '            SqlStr2 = MakeSQLCondInsert(True)
                '            SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf _
                ''                    & " AND ACCOUNTCODE=" & mAccountCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
                If RsOP.EOF = False Then
                    mOpening = IIf(IsDBNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
                    If mOpening >= 0 Then
                        mOpDr = mOpening
                        mOpCr = 0
                    Else
                        mOpDr = 0
                        mOpCr = System.Math.Abs(mOpening)
                    End If
                End If

                'If mBookType = "B" Then
                '    mVoucherType = "BANK " & IIf(mBookSubType = "P", "PAYMENT", "RECEIPT")
                'ElseIf mBookType = "C" Then
                '    mVoucherType = "CASH " & IIf(mBookSubType = "P", "PAYMENT", "RECEIPT")
                'ElseIf mBookType = "J" Then
                '    mVoucherType = "JOURNAL"
                'ElseIf mBookType = "S" Then
                '    mVoucherType = "SALE"
                'ElseIf mBookType = "P" Then
                '    mVoucherType = "PURCHASE"
                'ElseIf mBookType = "L" Then
                '    mVoucherType = "CREDIT NOTE"
                'ElseIf mBookType = "U" Then
                '    mVoucherType = "SUPPLIMENTRY"
                'ElseIf mBookType = "E" Then
                '    mVoucherType = "DEBIT NOTE"
                'ElseIf mBookType = "R" Then
                '    mVoucherType = "CREDIT NOTE"
                'Else
                '    mVoucherType = ""
                'End If


                If mOpening <> 0 Then
                    InsertSqlStr = " Insert into Temp_Ledger (UserID,SubRow,PARTYNAME, " & vbCrLf _
                        & " NARRATION,DAmount,CAmount,PARTY_ADD) VALUES ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mAccountName) & "','OPENING :' , " & vbCrLf _
                        & " " & Val(CStr(mOpDr)) & ", " & vbCrLf & " " & Val(CStr(mOpCr)) & ",'" & MainClass.AllowSingleQuote(mPartyAdd) & "') "

                    pDBCn.Execute(InsertSqlStr)
                End If
                'Get Detail.........
                SqlStr1 = MakeSQLForInsert(mAccountName, mPartyAdd)
                SqlStr2 = MakeSQLCondInsert(False)
                SqlStr = SqlStr1 & vbCrLf & SqlStr2

                If optAccount.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE IN (SELECT DISTINCT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=TRN.COMPANY_CODE AND GROUPCODE='" & mAccountCode & "')"
                End If

                'SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                ''            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' " & vbCrLf _
                '                        & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT TRN.BOOKTYPE || TRN.MKEY " & SqlStr2 & vbCrLf _
                '                        & " AND ACCOUNTCODE='" & mAccountCode & "')"
                ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
                '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION, " & vbCrLf _

                '  || ' ' || CHQDATE

                If OptSumDet(0).Checked = True Then
                    'SqlStr = SqlStr & vbCrLf & " GROUP BY CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'YYYYMMDD') ELSE TO_CHAR(Vdate,'YYYYMMDD') END," & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.VDATE,'DD/MM/YYYY') END," & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.VDATE,'DD/MM/YYYY') END, " & vbCrLf _
                    '    & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END,  " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.VDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) END," & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE TRN.REMARKS END, " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END," & vbCrLf _
                    '    & " CHEQUENO" & vbCrLf _
                    '    & " ORDER BY " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.VDATE,'DD/MM/YYYY') END, " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END"

                    SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(Vdate,'YYYYMMDD') ," & vbCrLf _
                       & " TRN.VDATE," & vbCrLf _
                       & " TRN.VDATE, " & vbCrLf _
                       & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf _
                       & " TRN.VNO ,  " & vbCrLf _
                       & " DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) ," & vbCrLf _
                       & " TRN.REMARKS , " & vbCrLf _
                       & " TRN.MKEY," & vbCrLf _
                       & " CHEQUENO" & vbCrLf _
                       & " ORDER BY " & vbCrLf _
                       & " TRN.VDATE, " & vbCrLf _
                       & " TRN.VNO "

                ElseIf OptSumDet(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf _
                        & " GROUP BY TRN.VDATE" & vbCrLf _
                        & " ORDER BY TRN.VDATE"
                ElseIf OptSumDet(2).Checked = True Then
                    SqlStr = SqlStr & vbCrLf _
                        & " GROUP BY TO_CHAR(Vdate,'MON-YYYY'),TO_CHAR(Vdate,'YYYYMM')" & vbCrLf _
                        & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
                End If
                InsertSqlStr = " Insert into Temp_Ledger ( " & vbCrLf _
                    & " UserID,SubRow,PARTYNAME,VDATE, " & vbCrLf _
                    & " VNO,NARRATION,DAmount,CAmount,CHQNO, " & vbCrLf _
                    & " ACCOUNTNAME,COSTCNAME,BILLDETAIL,PARTY_ADD) " & vbCrLf _
                    & SqlStr
                pDBCn.Execute(InsertSqlStr)
                RS.MoveNext()
            Loop
        End If
        pDBCn.CommitTrans()
        lblPrintCount.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        '    Resume
        MsgInformation(Err.Description)
        pDBCn.RollbackTrans()
        lblPrintCount.Visible = False
    End Sub
    Private Sub InsertAllLederAcct(ByRef pDBCn As ADODB.Connection, ByRef pAllAccount As Boolean, ByRef pGroupType As String)
        On Error GoTo LedgError
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim SqlStr As String
        Dim InsertSqlStr As String
        Dim mGroupOption As String
        Dim RS As ADODB.Recordset
        Dim mCntCount As Integer
        Dim mAccountName As String
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim RsOP As ADODB.Recordset
        Dim mPartyAdd As String
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        pDBCn.Errors.Clear()
        pDBCn.BeginTrans()
        SqlStr1 = ""
        SqlStr2 = ""
        SqlStr3 = ""
        SqlStr = ""
        InsertSqlStr = ""
        lblPrintCount.Visible = True
        lblPrintCount.Text = ""

        If optAccount.Checked = True Then
            If pAllAccount = True Then
                SqlStr = "SELECT DISTINCT SUPP_CUST_CODE,SUPP_CUST_NAME, SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ' - ' || SUPP_CUST_PIN || ', ' || SUPP_CUST_STATE PARTY_ADD FROM FIN_SUPP_CUST_MST "

                SqlStr = SqlStr & vbCrLf _
                & " WHERE FIN_SUPP_CUST_MST.SUPP_CUST_TYPE NOT IN ('1','2')"

                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    For CntLst = 1 To lstCompanyName.Items.Count - 1
                        If lstCompanyName.GetItemChecked(CntLst) = True Then
                            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                            End If
                            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                        End If

                    Next
                End If

                If mCompanyCodeStr <> "" Then
                    mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
                End If

                SqlStr = SqlStr & vbCrLf _
                & "  ORDER BY SUPP_CUST_NAME"
            Else
                SqlStr = "SELECT DISTINCT SUPP_CUST_CODE,SUPP_CUST_NAME, SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ' - ' || SUPP_CUST_PIN || ', ' || SUPP_CUST_STATE PARTY_ADD FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST" & vbCrLf _
                & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_GROUP_MST.COMPANY_CODE" & vbCrLf _
                & " AND FIN_SUPP_CUST_MST.GROUPCODE=FIN_GROUP_MST.GROUP_CODE" & vbCrLf _
                & " And FIN_SUPP_CUST_MST.SUPP_CUST_TYPE NOT IN ('1','2')"

                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    For CntLst = 1 To lstCompanyName.Items.Count - 1
                        If lstCompanyName.GetItemChecked(CntLst) = True Then
                            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                            End If
                            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                        End If

                    Next
                End If

                If mCompanyCodeStr <> "" Then
                    mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                    SqlStr = SqlStr & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE IN " & mCompanyCodeStr & ""
                End If

                If pGroupType <> "ALL" Then
                    If pGroupType = "S" Then
                        SqlStr = SqlStr & vbCrLf & " AND FIN_SUPP_CUST_MST.RESPONSIBLE_PERSON='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtSalesPerson.Text)) & "'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GROUP_TYPE='" & pGroupType & "'"
                    End If
                Else
                    SqlStr = SqlStr & vbCrLf & " AND FIN_GROUP_MST.GROUP_NAME='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtLedgerGroup.Text)) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY SUPP_CUST_NAME"
            End If
        Else
            If pAllAccount = True Then
                SqlStr = "SELECT GROUP_CODE AS SUPP_CUST_CODE,GROUP_NAME AS SUPP_CUST_NAME, '' PARTY_ADD FROM FIN_GROUP_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                    & " ORDER BY GROUP_NAME"
            Else
                SqlStr = "SELECT GROUP_CODE SUPP_CUST_CODE, GROUP_NAME SUPP_CUST_NAME, '' PARTY_ADD FROM FIN_GROUP_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & ""

                If pGroupType <> "ALL" Then
                    If pGroupType = "S" Then
                        SqlStr = SqlStr & vbCrLf & " AND 1=2"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GROUP_TYPE='" & pGroupType & "'"
                    End If
                Else
                    SqlStr = SqlStr & vbCrLf & " AND GROUP_NAME='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtLedgerGroup.Text)) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY GROUP_NAME"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While Not RS.EOF
                mAccountCode = IIf(IsDBNull(RS.Fields("SUPP_CUST_CODE").Value), "-1", RS.Fields("SUPP_CUST_CODE").Value)
                mAccountName = IIf(IsDBNull(RS.Fields("SUPP_CUST_NAME").Value), "-1", RS.Fields("SUPP_CUST_NAME").Value)
                mPartyAdd = IIf(IsDBNull(RS.Fields("PARTY_ADD").Value), "", RS.Fields("PARTY_ADD").Value)

                mCntCount = mCntCount + 1
                lblPrintCount.Text = mCntCount & " - " & Trim(Replace(mAccountName, "&", "&&"))
                System.Windows.Forms.Application.DoEvents()
                'Get Opening Balance.........
                SqlStr = MakeOPSQL(mAccountCode)
                '            SqlStr2 = MakeSQLCondInsert(True)
                '            SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf _
                ''                    & " AND ACCOUNTCODE=" & mAccountCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
                If RsOP.EOF = False Then
                    mOpening = IIf(IsDBNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
                    If mOpening >= 0 Then
                        mOpDr = mOpening
                        mOpCr = 0
                    Else
                        mOpDr = 0
                        mOpCr = System.Math.Abs(mOpening)
                    End If
                End If


                'If mOpening <> 0 Then
                InsertSqlStr = " Insert into Temp_Ledger (UserID,SubRow,PARTYNAME, " & vbCrLf _
                    & " NARRATION,DAmount,CAmount,PARTY_ADD) VALUES ( " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mAccountName) & "','OPENING :' , " & vbCrLf _
                    & " " & Val(CStr(mOpDr)) & ", " & vbCrLf & " " & Val(CStr(mOpCr)) & ",'" & MainClass.AllowSingleQuote(mPartyAdd) & "') "

                pDBCn.Execute(InsertSqlStr)
                'End If
                'Get Detail.........
                SqlStr1 = MakeSQLForInsert(mAccountName, mPartyAdd)
                SqlStr2 = MakeSQLCondInsert(False)
                SqlStr = SqlStr1 & vbCrLf & SqlStr2

                If optAccount.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE IN (SELECT DISTINCT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=TRN.COMPANY_CODE AND GROUPCODE='" & mAccountCode & "')"
                End If

                'SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                ''            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' " & vbCrLf _
                '                        & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT TRN.BOOKTYPE || TRN.MKEY " & SqlStr2 & vbCrLf _
                '                        & " AND ACCOUNTCODE='" & mAccountCode & "')"
                ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
                '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION, " & vbCrLf _

                '  || ' ' || CHQDATE

                If OptSumDet(0).Checked = True Then
                    'SqlStr = SqlStr & vbCrLf & " GROUP BY CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'YYYYMMDD') ELSE TO_CHAR(Vdate,'YYYYMMDD') END," & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.VDATE,'DD/MM/YYYY') END," & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.VDATE,'DD/MM/YYYY') END, " & vbCrLf _
                    '    & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END,  " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.VDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) END," & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE TRN.REMARKS END, " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END," & vbCrLf _
                    '    & " CHEQUENO" & vbCrLf _
                    '    & " ORDER BY " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.VDATE,'DD/MM/YYYY') END, " & vbCrLf _
                    '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END"

                    SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(Vdate,'YYYYMMDD') ," & vbCrLf _
                       & " TRN.VDATE," & vbCrLf _
                       & " TRN.VDATE, " & vbCrLf _
                       & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf _
                       & " TRN.VNO ,  TRN.COMPANY_CODE," & vbCrLf _
                       & " DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) ," & vbCrLf _
                       & " TRN.REMARKS , " & vbCrLf _
                       & " TRN.MKEY," & vbCrLf _
                       & " CHEQUENO" & vbCrLf _
                       & " ORDER BY " & vbCrLf _
                       & " TRN.VDATE, " & vbCrLf _
                       & " TRN.VNO "

                ElseIf OptSumDet(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf _
                        & " GROUP BY TRN.VDATE, TRN.COMPANY_CODE" & vbCrLf _
                        & " ORDER BY TRN.VDATE"
                ElseIf OptSumDet(2).Checked = True Then
                    SqlStr = SqlStr & vbCrLf _
                        & " GROUP BY TO_CHAR(Vdate,'MON-YYYY'),TO_CHAR(Vdate,'YYYYMM'), TRN.COMPANY_CODE" & vbCrLf _
                        & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
                End If
                InsertSqlStr = " Insert into Temp_Ledger ( " & vbCrLf _
                    & " UserID,SubRow,PARTYNAME,VDATE, " & vbCrLf _
                    & " VNO,NARRATION,DAmount,CAmount,CHQNO, " & vbCrLf _
                    & " ACCOUNTNAME,COSTCNAME,BILLDETAIL,PARTY_ADD,VOUCHER_TYPE) " & vbCrLf _
                    & SqlStr
                pDBCn.Execute(InsertSqlStr)

                ''MIN(GETLEDGEREXPHEAD (TRN.COMPANY_CODE,  TRN.FYEAR, TRN.BOOKCODE, TRN.MKEY, TRN.VNO, TRN.ACCOUNTCODE)) AS EXPENSES_HEAD
                RS.MoveNext()
            Loop
        End If
        pDBCn.CommitTrans()
        lblPrintCount.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        '    Resume
        MsgInformation(Err.Description)
        pDBCn.RollbackTrans()
        lblPrintCount.Visible = False
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim Printer As New Printer
        Report1.SQLQuery = mSqlStr
        Dim mCompanyCode As Long = -1
        Dim mCompanyName As String

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



        ' Report1.CopiesToPrinter = PrintCopies
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
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_Ledger " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY PARTYNAME, SUBROW "
        FetchRecordForReport = mSqlStr
    End Function


    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean
        If cboAccount.Text = "" Then PrintStatus = False Else PrintStatus = True
        frmPrintLedg.OptSelected.Enabled = PrintStatus
        If OptSumDet(1).Checked = True Or OptSumDet(2).Checked = True Then
            frmPrintLedg.fraPrintOption.Enabled = False
        Else
            frmPrintLedg.fraPrintOption.Enabled = True
        End If
        frmPrintLedg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter, PubDBCn)
        frmPrintLedg.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        frmPrintLedg.Close()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart

        ConShowActive = True
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdLedg, RowHeight)

        txtCreditLimit.Text = ""
        txtPaymentTerms.Text = ""
        txtSecurityDeposit.Text = ""
        txtSecurityAmount.Text = ""
        txtSecurityChqNo.Text = ""
        txtBankName.Text = ""
        txtSaleRep.Text = ""

        CreateGridHeader()
        If LedgInfo("") = False Then GoTo ErrPart

        Me.UltraGrid1.DisplayLayout.Override.FixedRowIndicator = FixedRowIndicator.None
        Me.UltraGrid1.Rows(0).Fixed = True

        'Me.UltraGrid1.Rows(UltraGrid1.Rows.Count - 1).Fixed = True

        UltraGrid1.Focus()
        Call PrintStatus(True)
        '    FraOthers.Visible = False
        'MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
        ConShowActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub DisplayTotals(ByRef pOpeningDr As Double, ByRef pOpeningCr As Double, ByRef mAdjustedAmount As Double, ByRef mUnAdjustedAmount As Double)
        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mDebit As Double
        Dim mCredit As Double
        Dim mBalance As Double
        Dim mDC As String
        Dim mRow As UltraGridRow
        Dim lngRow As Long, lngCount As Long

        Dim row As UltraGridRow = Me.UltraGrid1.DisplayLayout.Bands(0).AddNew()
        UltraGrid1.Rows.Move(row, 0)
        'Dim row1 As UltraGridRow = Me.UltraGrid1.DisplayLayout.Bands(0).AddNew()


        mRow = UltraGrid1.Rows(0)
        mRow.Cells(ColNarration - 1).Value = "OPENING : "

        '.Font = VB6.FontChangeBold(.Font, True)
        'FormatSprdLedg -1

        '.Col = ColDAmount
        mRow.Cells(ColDAmount - 1).Value = VB6.Format(pOpeningDr, "0.00")
        '.Font = VB6.FontChangeBold(.Font, True)

        '.Col = ColCAmount
        mRow.Cells(ColCAmount - 1).Value = VB6.Format(pOpeningCr, "0.00")
        '    .Font = VB6.FontChangeBold(.Font, True)

        '.Col = ColBalance
        mRow.Cells(ColBalance - 1).Value = "0.00"
        '    .Font = VB6.FontChangeBold(.Font, True)

        '.Col = ColBalDC
        mRow.Cells(ColBalDC - 1).Value = "Dr"
        '    .Font = VB6.FontChangeBold(.Font, True)

        '.Col = ColAdjustedAmount
        mRow.Cells(ColAdjustedAmount - 1).Value = VB6.Format(mAdjustedAmount, "0.00")
        '    .Font = VB6.FontChangeBold(.Font, True)

        '.Col = ColUnAdjustedAmount
        mRow.Cells(ColUnAdjustedAmount - 1).Value = VB6.Format(mUnAdjustedAmount, "0.00")
        '    .Font = VB6.FontChangeBold(.Font, True)

        'mRow = UltraGrid1.Rows(UltraGrid1.Rows.Count - 1)
        'mRow.Cells(ColNarration - 1).Value = "TOTAL :"

        'mDebit = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))))

        'mCredit = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))))
        'mBalance = mDebit - mCredit
        'mRow.Cells(ColBalance - 1).Value = Str(System.Math.Abs(mBalance))
        'mRow.Cells(ColBalDC - 1).Value = IIf(mBalance >= 0, "DR", "CR")


        Call FillRunBalCol()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Public Sub frmViewLedger_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim I As Integer
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call GetFormCaption((lblBookType.Text))
        cboAccount.Visible = True
        If lblBookType.Text = ConLedger Then
            FraAccount.Text = "Accounts"
            chkGroup(8).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(8).Enabled = False
        Else
            FraAccount.Text = "PCD Book"
            chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked
            chkGroup(8).Enabled = False
            For I = 0 To 7
                chkGroup(I).CheckState = System.Windows.Forms.CheckState.Unchecked
                chkGroup(I).Enabled = False
            Next
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If LedgInfo("S") = False Then GoTo ERR1

        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewLedger_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        Call FillComboBox()
        If LedgInfo("S") = False Then GoTo BSLError
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        OptSumDet(0).Checked = True
        FraOthers.Visible = False



        '' Move the rows or columns with the scroll box
        'SprdLedg.ScrollBarTrack = FPSpreadADO.ScrollBarTrackConstants.ScrollBarTrackBoth
        '' Show the scroll tips
        'SprdLedg.ShowScrollTips = FPSpreadADO.ShowScrollTipsConstants.ShowScrollTipsBoth

        Call frmViewLedger_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset
        Dim SqlStr As String
        Dim CntLst As Long
        Dim mCompanyName As String

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        'MainClass.FillCombo CboCC, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""  ', "BranchCode=" & RsCompany.fields("CBRANCHCODE & ""
        MainClass.FillCombo(CboCC, "FIN_CCENTER_HDR", "CC_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(CboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboEmp, "PAY_EMPLOYEE_MST", "EMP_NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboExpHead, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboDivision, "INV_DIVISION_MST", "DIV_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        CboDept.SelectedIndex = 0
        cboEmp.SelectedIndex = 0
        CboCC.SelectedIndex = 0
        cboExpHead.SelectedIndex = 0
        cboDivision.SelectedIndex = 0
        txtCondAmount.Text = CStr(0)
        cboCond.Items.Clear()
        cboCond.Items.Add("=")
        cboCond.Items.Add("<=")
        cboCond.Items.Add(">=")
        cboCond.Items.Add("<")
        cboCond.Items.Add(">")
        cboCond.Items.Add("<>")
        cboCond.SelectedIndex = 0

        Dim mCompanyAdd As String
        '    cboConsolidated.Clear
        '    cboConsolidated.AddItem "COMPANY"
        '    cboConsolidated.AddItem "REGION"
        '    cboConsolidated.AddItem "BRANCH"
        '    cboConsolidated.AddItem "DIVISION"
        '    cboConsolidated.Enabled = True
        '    cboConsolidated.Enabled = True
        '    cboConsolidated.ListIndex = 3

        mCompanyAdd = IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value)
        mCompanyAdd = mCompanyAdd & ", " & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME || ', ' ||  COMPANY_ADDR AS COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_NAME").Value), "", RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = mCompanyAdd, True, False))      '' RsCompany.Fields("COMPANY_NAME").Value
                'lstCompanyName.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Call FillAccountComboBox()

        'If optAccount.Checked = True Then
        '    SqlStr = "Select DISTINCT SUPP_CUST_NAME, SUPP_CUST_CODE, SUPP_CUST_ADDR,  SUPP_CUST_CITY, SUPP_CUST_STATE " & vbCrLf _
        '            & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SUPP_CUST_NAME"

        'Else
        '    SqlStr = "Select DISTINCT GROUP_NAME, GROUP_CODE " & vbCrLf _
        '           & " FROM FIN_GROUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY GROUP_NAME"
        'End If
        'oledbCnn.Open()
        'oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        'oledbAdapter.Fill(ds)

        '' Set the data source and data member to bind the grid.
        'cboAccount.DataSource = ds
        'cboAccount.DataMember = ""
        ''cmbCompany.ValueMember = "COMPANY_CODE"
        ''cmbCompany.DisplayMember = "Company Name"

        'cboAccount.Appearance.FontData.SizeInPoints = 8.5

        'If optAccount.Checked = True Then
        '    cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        '    cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        '    cboAccount.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        '    cboAccount.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        '    cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"
        '    ''cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"

        '    cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
        '    cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100
        '    cboAccount.DisplayLayout.Bands(0).Columns(2).Width = 350
        '    cboAccount.DisplayLayout.Bands(0).Columns(3).Width = 100
        '    cboAccount.DisplayLayout.Bands(0).Columns(4).Width = 100
        'Else
        '    cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        '    cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"


        '    cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
        '    cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100

        'End If

        'cboAccount.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        'cboAccount.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        ''cboCompany.Rows(0).Selected = True


        'oledbAdapter.Dispose()
        'oledbCnn.Close()

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

    Private Sub frmViewLedger_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 300, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        'MainClass.SetSpreadColor(UltraGrid1, -1)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewLedger_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        'MainClass.AssignDataInSprd8("", SprdLedg, "", "N")
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub OptSumDet_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSumDet.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSumDet.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub
    'Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent)
    '    SprdLedg.Row = -1
    '    SprdLedg.Col = eventArgs.col
    '    SprdLedg.DAutoCellTypes = True
    '    SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdLedg.TypeEditLen = 1000
    'End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        Dim xCompanyCode As Long

        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub

        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)


        If OptSumDet(0).Checked = True Then


            xCompanyCode = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1)))  ''Val(Me.SprdLedg.Text)
            xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))  ''       Me.SprdLedg.Text
            xMKey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))  ''     Me.SprdLedg.Text


            xVNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1))  '' Me.SprdLedg.Text
            xBookType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1))  '' Me.SprdLedg.Text
            xBookSubType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookSubType - 1))  ''= Me.SprdLedg.Text


            If xMKey <> "-1" And xCompanyCode = 0 Then
                If MainClass.ValidateWithMasterTable(xMKey, "MKEY", "COMPANY_CODE", "FIN_POSTED_TRN", PubDBCn, MasterNo, , " VNO='" & xVNo & "' AND BOOKTYPE='" & xBookType & "'") = True Then
                    xCompanyCode = Val(MasterNo)
                End If
            End If

            If xMKey = "-1" Or xCompanyCode = 0 Then
                Exit Sub
            End If


            If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Then
                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                xVNo = VB.Right(xVNo, 5)
            ElseIf xBookType = "R" Or xBookType = "E" Then
                If RsCompany.Fields("FYEAR").Value >= 2020 Then
                    xVTYPE = Mid(xVNo, 1, Len(xVNo) - 8)
                    xVNo = VB.Right(xVNo, 8)
                Else
                    xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                    xVNo = VB.Right(xVNo, 5)
                End If
            End If
            If RsCompany.Fields("COMPANY_CODE").Value = xCompanyCode Then
                Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
            Else
                'Call ShowTrnOtherUnit(xCompanyCode, xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
                '' to be check again 
                MsgInformation("Voucher Is Not related to this Unit.")
                Exit Sub
            End If

        Else
            If OptSumDet(2).Checked = True Then
                pIndex = 2
            Else
                pIndex = 1
                xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))  ''   
            End If
            Call ViewAccountLedger(pIndex, xVDate, xVDate)
        End If
    End Sub
    Private Sub ViewAccountLedger(ByRef xIndex As Integer, ByRef pDateFrom As String, ByRef pDateTo As String)
        Dim ss As New frmViewLedger
        Dim mFromDate As String
        Dim mToDate As String
        On Error GoTo ErrPart
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn
        Dim xVDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConLedger Then
            ss.MdiParent = Me.MdiParent
            ss.lblBookType.Text = "LEDG"
            ss.cboAccount.Text = cboAccount.Text
            ss.lblAcCode.Text = lblAcCode.Text

            mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
            xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))  ''

            If GetMonthStartEndDate(xVDate, mFromDate, mToDate) = True Then
                ss.txtDateFrom.Text = VB6.Format(mFromDate, "dd/mm/yyyy")
                ss.txtDateTo.Text = VB6.Format(mToDate, "dd/mm/yyyy")
            Else
                ss.txtDateFrom.Text = VB6.Format(pDateFrom, "dd/mm/yyyy")
                ss.txtDateTo.Text = VB6.Format(pDateTo, "dd/mm/yyyy")
            End If
            ss.cboDivision.Text = cboDivision.Text
            ss.OptSumDet(xIndex - 1).Checked = True
            ''ss.cboConsolidated.Text = cboConsolidated.Text
            '        ss.cboConsolidated.ListIndex = 3     ''DIVISION...
            ss.Show()
            ss.frmViewLedger_Activated(Nothing, New System.EventArgs())
            ss.cmdShow_Click(Nothing, New System.EventArgs())
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    'Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent)
    '    If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
    '        SprdLedg_DblClick(SprdLedg, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
    '    End If
    '    If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    'End Sub
    Private Sub cboAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAccount.TextChanged
        Call PrintStatus(False)
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
    Private Sub SearchAccounts(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If UCase(mTextBox.Name) <> UCase("TxtAgtAccount") Then
            Select Case lblBookType.Text
                Case ConLedger
                    '                SqlStr = SqlStr
                    '                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                    If lblShow.Text = "D" Then
                        SqlStr = SqlStr & " AND SUPP_CUST_TYPE IN ('S','C')"
                    ElseIf lblShow.Text = "O" Then
                        SqlStr = SqlStr & " AND SUPP_CUST_TYPE NOT IN ('1','2','S','C')"
                    End If
                Case ConCashBook
                    SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
                Case ConBankBook, ConPDCBook
                    SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
                Case Else
                    SqlStr = " AND 1=2"
            End Select
        End If
        '    MainClass.SearchMaster mTextBox.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr
        If MainClass.SearchGridMaster((mTextBox.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                mTextBox.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAgtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAgtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAgtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAgtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAgtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccounts(TxtAgtAccount)
    End Sub
    '    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        On Error GoTo ERR1
    '        Dim SqlStr As String
    '        lblAcCode.Text = ""
    '        If TxtAccount.Text = "" Then GoTo EventExitSub
    '        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And "
    '        Select Case lblBookType.Text
    '            Case ConLedger
    '                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
    '                If lblShow.Text = "D" Then
    '                    SqlStr = SqlStr & " AND SUPP_CUST_TYPE IN ('S','C')"
    '                ElseIf lblShow.Text = "O" Then
    '                    SqlStr = SqlStr & " AND SUPP_CUST_TYPE NOT IN ('1','2','S','C')"
    '                End If
    '            Case ConCashBook
    '                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE = '1'"
    '            Case ConBankBook, ConPDCBook
    '                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE = '2'"
    '            Case Else
    '                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND 1=2"
    '        End Select
    '        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
    '            lblAcCode.Text = MasterNo
    '            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
    '        Else
    '            lblAcCode.Text = ""
    '            MsgInformation("No Such Account in Account Master")
    '            Cancel = True
    '        End If
    '        GoTo EventExitSub
    'ERR1:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    'EventExitSub:
    '        eventArgs.Cancel = Cancel
    '    End Sub
    'Private Sub FormatSprdLedg(ByRef Arow As Integer)
    '    With SprdLedg
    '        .MaxCols = ColBranch
    '        .set_RowHeight(0, RowHeight * 1.25)
    '        .set_ColWidth(0, 4.5)
    '        .set_RowHeight(-1, RowHeight)
    '        .RowsFrozen = 1
    '        .Row = -1

    '        .Col = ColLocked
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColLocked, 15)
    '        .ColHidden = True

    '        .Col = ColBookType
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBookType, 15)
    '        .ColHidden = True

    '        .Col = ColBookSubType
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBookSubType, 15)
    '        .ColHidden = True

    '        .Col = ColVDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColVDate, 9)

    '        .Col = ColExpDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColExpDate, 9)

    '        .Col = ColVNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColVNo, 10)
    '        If OptSumDet(0).Checked = True Then
    '            .ColHidden = False
    '        Else
    '            .ColHidden = True
    '        End If
    '        '        .Col = ColAcctName
    '        '        .CellType = SS_CELL_TYPE_EDIT
    '        '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        '        .TypeEditLen = 255
    '        '        .TypeEditMultiLine = True
    '        '        .ColWidth(ColAcctName) = 15
    '        '        If OptSumDet(0).Value = True Then
    '        '            .ColHidden = False
    '        '            .ColsFrozen = ColAcctName
    '        '        Else
    '        '            .ColHidden = True
    '        '        End If
    '        '
    '        .Col = ColDAmount
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 2
    '        .TypeFloatMin = CDbl("0")
    '        .TypeFloatMax = CDbl("9999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .set_ColWidth(ColDAmount, 12)

    '        .Col = ColCAmount
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 2
    '        .TypeFloatMin = CDbl("0")
    '        .TypeFloatMax = CDbl("9999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .set_ColWidth(ColCAmount, 12)

    '        .Col = ColBalance
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 2
    '        .TypeFloatMin = CDbl("0")
    '        .TypeFloatMax = CDbl("9999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .set_ColWidth(ColBalance, 12)
    '        .ColHidden = False

    '        .Col = ColBalDC
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBalDC, 5)
    '        .ColHidden = False
    '        '        If ChkWithRunBal.Value = vbUnchecked Then
    '        '            .Col = ColBalance
    '        '            .ColHidden = True
    '        '            .Col = ColBalDC
    '        '            .ColHidden = True
    '        '        End If

    '        .Col = ColNarration
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColNarration, 25)
    '        If OptSumDet(0).Checked = True Then
    '            .ColHidden = False
    '            .ColsFrozen = ColNarration
    '        Else
    '            .ColHidden = True
    '            .ColsFrozen = ColVDate
    '        End If

    '        .Col = ColBillDetail
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBillDetail, 15)
    '        .ColHidden = True
    '        If OptSumDet(0).Checked = True Then
    '            .ColHidden = False
    '        Else
    '            .ColHidden = True
    '        End If

    '        .Col = ColChequeNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColChequeNo, 8)
    '        If OptSumDet(0).Checked = True Then
    '            .ColHidden = False
    '        Else
    '            .ColHidden = True
    '        End If


    '        .Col = ColUnitName
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColUnitName, 8)
    '        If OptSumDet(0).Checked = True Then
    '            .ColHidden = False
    '        Else
    '            .ColHidden = True
    '        End If

    '        .Col = ColAdjustedAmount
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 2
    '        .TypeFloatMin = CDbl("0")
    '        .TypeFloatMax = CDbl("9999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .ColHidden = False
    '        .set_ColWidth(ColAdjustedAmount, 12)

    '        .Col = ColUnAdjustedAmount
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 2
    '        .TypeFloatMin = CDbl("0")
    '        .TypeFloatMax = CDbl("9999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .ColHidden = False
    '        .set_ColWidth(ColUnAdjustedAmount, 12)

    '        .Col = ColVendorCode
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColVendorCode, 8)

    '        .Col = ColDept
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColDept, 8)
    '        .ColHidden = True

    '        .Col = ColEmp
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColEmp, 8)
    '        .ColHidden = True

    '        .Col = ColCostC
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColCostC, 8)
    '        .ColHidden = True

    '        .Col = ColMKEY
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColMKEY, 8)
    '        .ColHidden = True

    '        .Col = ColSubRowNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColSubRowNo, 5)
    '        .ColHidden = True

    '        .Col = ColBranch
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBranch, 15)

    '        .ColHidden = True ''IIf(Left(cboConsolidated.Text, 1) = "D", True, False)

    '        MainClass.SetSpreadColor(SprdLedg, -1)
    '        MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
    '        SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
    '        SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))

    '        SprdLedg.DAutoCellTypes = True
    '        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '        SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
    '    End With
    'End Sub
    Private Sub FillRunBalCol()
        On Error GoTo ERR1
        Dim ii As Long
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
        Dim mMkey As String
        Dim xFromDate As String
        Dim xToDate As String
        Dim mDate As String

        Dim mAdjAmount As Double = 0
        Dim mTotAdjAmount As Double = 0
        Dim mUnAdjAmount As Double = 0
        Dim mTotUnAdjAmount As Double = 0
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        Dim lngRow As Long, lngCount As Long



        mSuppCustCode = ""
        mAccountCode = ""
        If MainClass.ValidateWithMasterTable((cboAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            mSuppCustCode = MasterNo
        ElseIf MainClass.ValidateWithMasterTable((cboAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        End If

        If PubUserID = "A00001" Then
            For lngRow = 0 To UltraGrid1.Rows.Count - 1   '' UltraDataSource1.Rows.Count - 1

                UltraGrid1.Rows(lngRow).Tag = lngRow

                mRow = UltraGrid1.Rows(lngRow)
                mBookType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1))
                mMkey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))

                mDummayDebit = 0
                mDummayCredit = 0
                xDummyAmount = 0
                If mBookType = "P" Or mBookType = "S" Or OptSumDet(2).Checked = True Then
                    If mMkey = "" Then
                        mDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))
                        mDate = CStr(MonthValue(mDate, True))
                        If Val(mDate) > 0 Then
                            xFromDate = VB6.Format("01/" & mDate & "/" & RsCompany.Fields("FYEAR").Value + IIf(CDbl(mDate) >= 1 And CDbl(mDate) <= 3, 1, 0), "DD/MM/YYYY")
                            xToDate = VB6.Format(MainClass.LastDay(Month(CDate(xFromDate)), Year(CDate(xFromDate))) & "/" & mDate & "/" & RsCompany.Fields("FYEAR").Value + IIf(CDbl(mDate) >= 1 And CDbl(mDate) <= 3, 1, 0), "DD/MM/YYYY")
                        End If
                    End If
                    If GetDummyExpAmount(mSuppCustCode, mAccountCode, "", mMkey, mBookType, xDummyAmount, xDC, xFromDate, xToDate) = True Then
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
                End If
                If mMkey = "" Then
                    mRow.Cells(ColDAmount - 1).Value = CStr(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))) + mDummayDebit)
                    mRow.Cells(ColCAmount - 1).Value = CStr(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))) + mDummayCredit)
                Else
                    mRow.Cells(ColDAmount - 1).Value = CStr(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))) + mDummayDebit)
                    mRow.Cells(ColCAmount - 1).Value = CStr(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))) + mDummayCredit)
                End If

                If IsNumeric(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1)))) Then
                    mDAmount = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))))
                Else
                    mDAmount = 0
                End If

                mTotDAmount = mTotDAmount + mDAmount
                If IsNumeric(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1)))) Then
                    mCAmount = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))))
                Else
                    mCAmount = 0
                End If

                mTotCAmount = mTotCAmount + mCAmount
                mBalance = mBalance + mDAmount - mCAmount
                mRow.Cells(ColBalance - 1).Value = MainClass.FormatRupees(System.Math.Abs(mBalance))
                mRow.Cells(ColBalDC - 1).Value = IIf(mBalance > 0, "Dr", "Cr")
            Next
        Else
            For lngRow = 0 To UltraGrid1.Rows.Count - 1   '' UltraDataSource1.Rows.Count - 1

                UltraGrid1.Rows(lngRow).Tag = lngRow
                mRow = UltraGrid1.Rows(lngRow)

                If IsNumeric((Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))))) Then
                    mDAmount = CDbl((Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1)))))
                Else
                    mDAmount = 0
                End If
                mTotDAmount = mTotDAmount + mDAmount

                If IsNumeric((Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))))) Then
                    mCAmount = CDbl((Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1)))))
                Else
                    mCAmount = 0
                End If
                mTotCAmount = mTotCAmount + mCAmount

                mBalance = mBalance + mDAmount - mCAmount

                mRow.Cells(ColBalance - 1).Value = MainClass.FormatRupees(System.Math.Abs(mBalance))
                mRow.Cells(ColBalDC - 1).Value = IIf(mBalance > 0, "Dr", "Cr")

                If IsNumeric(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdjustedAmount - 1)))) Then
                    mAdjAmount = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdjustedAmount - 1))))
                Else
                    mAdjAmount = 0
                End If
                mTotAdjAmount = mTotAdjAmount + mAdjAmount

                If IsNumeric(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnAdjustedAmount - 1)))) Then
                    mUnAdjAmount = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnAdjustedAmount - 1))))
                Else
                    mUnAdjAmount = 0
                End If
                mTotUnAdjAmount = mTotUnAdjAmount + mUnAdjAmount
            Next
        End If

        'mBalance = 0
        'For lngRow = UltraGrid1.Rows.Count - 1 To UltraGrid1.Rows.Count - 1   '' UltraDataSource1.Rows.Count - 1
        '    mRow = UltraGrid1.Rows(lngRow)

        '    mRow.Cells(ColDAmount - 1).Value = MainClass.FormatRupees(System.Math.Abs(mTotDAmount))

        '    If IsNumeric(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1)))) Then
        '        mDAmount = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1))))
        '    Else
        '        mDAmount = 0
        '    End If

        '    mRow.Cells(ColCAmount - 1).Value = MainClass.FormatRupees(System.Math.Abs(mTotCAmount))

        '    If IsNumeric(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1)))) Then
        '        mCAmount = CDbl(Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1))))
        '    Else
        '        mCAmount = 0
        '    End If

        '    mBalance = mBalance + mDAmount - mCAmount
        '    mRow.Cells(ColBalance - 1).Value = MainClass.FormatRupees(System.Math.Abs(mBalance))
        '    mRow.Cells(ColBalDC - 1).Value = IIf(mBalance > 0, "Dr", "Cr")

        '    mRow.Cells(ColAdjustedAmount - 1).Value = MainClass.FormatRupees(mTotAdjAmount)
        '    mRow.Cells(ColUnAdjustedAmount - 1).Value = MainClass.FormatRupees(mTotUnAdjAmount)
        'Next


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Function LedgInfo(pType As String) As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mAdjustedAmount As Double = 0
        Dim mUnAdjustedAmount As Double = 0
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
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

        SqlStr1 = MakeSQL()
        SqlStr2 = MakeSQLCond(False)
        SqlStr = SqlStr1 & vbCrLf & SqlStr2

        If pType = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If

        'SqlStr = SqlStr & vbCrLf & " AND BILLNO='157266'"

        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.AMOUNT " & cboCond.Text & Val(txtCondAmount.Text) & "" ''* DECODE(TRN.DC,'D',1,-1)
        End If
        If lblBookType.Text = ConJournalBook Then
            '        SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConJournalBookCode & "' "
        ElseIf lblBookType.Text = ConContraBook Then
            '        SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConContraBookCode & "' "
        Else
            '        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
            If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable((TxtAgtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode2 = MasterNo
                Else
                    mAccountCode2 = "-1"
                End If

                ''" & RsCompany.Fields("COMPANY_CODE").Value & " Sandeep 20072022

                SqlStr = SqlStr & vbCrLf _
                    & " AND TRN.BOOKTYPE ||TRN.MKEY IN ( " & vbCrLf _
                    & " SELECT BOOKTYPE || MKEY " & vbCrLf _
                    & " FROM FIN_POSTED_TRN " & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf _
                    & " AND ACCOUNTCODE='" & mAccountCode2 & "'" & vbCrLf _
                    & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " ) "
            End If
            ''ok
            ''         SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' " & vbCrLf _
            '                & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT TRN.BOOKTYPE || TRN.MKEY " & SqlStr2 & vbCrLf _
            '                & " AND ACCOUNTCODE='" & mAccountCode & "')"
        End If
        '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.VDATE,'DD/MM/YYYY') END V_DATE, " & vbCrLf _
        ''                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MMMYYYY') ELSE TRN.VNO END AS V_NO, " & vbCrLf _
        '
        ''& " ELSE " & vbCrLf _
        '& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)  || CHR(13) ELSE '' END || DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END ," & vbCrLf _'
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY  "

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            Else
                SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE, GCMT.COMPANY_SHORTNAME,"
            End If

            SqlStr = SqlStr & vbCrLf & " CASE WHEN BOOKCODE=" & ConSalesBookCode & " THEN GETVENDORCODE(TRN.COMPANY_CODE,TRN.MKEY,TRN.VNO) ELSE '' END," & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.VDATE) ELSE TRN.VDATE END," & vbCrLf _
                & " --CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.VDATE) ELSE TRN.VDATE END, " & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END,  " & vbCrLf _
                & " --CASE WHEN BOOKCODE=" & ConSalesBookCode & " THEN  TO_CHAR(TRN.BILLDATE,'DD/MM/YYYY') ELSE '' END," & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.VDATE,'MONYYYY')  ELSE DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END ,"

            SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.EXPDATE,'DD/MM/YYYY') END," & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.EXPDATE) ELSE TRN.EXPDATE END, " & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END,  " ''& vbCrLf |                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.EXPDATE,'MONYYYY')  ELSE DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END ," & vbCrLf |                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE TRN.REMARKS END,"

            SqlStr = SqlStr & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE REPLACE(TRN.REMARKS, chr(13)||chr(10),' ') END," & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END ," & vbCrLf _
                & " TRN.BOOKTYPE, TRN.BOOKSUBTYPE, CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY'),MONTHWISE_LDGR, BOOKCODE,  TRN.ADDUSER, TRN.ADDDATE" ''TRN.CLEARDATE,, TRN.MODUSER,TRN.MODDATE "

            If OptOrderBy(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.VDATE) ELSE TRN.VDATE END, CASE WHEN ((BOOKSUBTYPE ='A' OR BOOKSUBTYPE ='R') AND (BOOKTYPE='" & ConContraBook & "' OR BOOKTYPE='" & ConCashBook & "' OR BOOKTYPE='" & ConBankBook & "')) THEN 1 ELSE 2 END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END"
            Else
                SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.EXPDATE) ELSE TRN.EXPDATE END, CASE WHEN ((BOOKSUBTYPE ='A' OR BOOKSUBTYPE ='R') AND (BOOKTYPE='" & ConContraBook & "' OR BOOKTYPE='" & ConCashBook & "' OR BOOKTYPE='" & ConBankBook & "')) THEN 1 ELSE 2 END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END"
            End If
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.VDATE" & vbCrLf & " ORDER BY TRN.VDATE"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(TRN.VDATE,'MON-YYYY'),TO_CHAR(Vdate,'YYYYMM')" & vbCrLf & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
        End If

        'MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")
        Call FillUltraGrid(SqlStr)

        If pType = "S" Then
            LedgInfo = True
            Exit Function
        End If

        'Me.UltraGrid1.Rows(0).Fixed = True
        '********************************
        'Get Opening Balance.........
        SqlStr = MakeOPSQL(mAccountCode)
        '    SqlStr2 = MakeSQLCond(True)
        '    SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf _
        ''            & " AND ACCOUNTCODE=" & mAccountCode & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOP.EOF = False Then
            mOpening = IIf(IsDBNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)

            mAdjustedAmount = IIf(IsDBNull(RsOP.Fields("ADJUST_AMOUNT").Value), 0, RsOP.Fields("ADJUST_AMOUNT").Value)
            mUnAdjustedAmount = IIf(IsDBNull(RsOP.Fields("UNADJUST_AMOUNT").Value), 0, RsOP.Fields("UNADJUST_AMOUNT").Value)



            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                mSuppCustCode = MasterNo
            ElseIf MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDummyAccountCode = MasterNo
            End If
            If GetDummyExpAmount(mSuppCustCode, mDummyAccountCode, "", "", "", xDummyAmount, "", RsCompany.Fields("START_DATE").Value, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text)))) = True Then
                If mSuppCustCode <> "" Then
                    mDummayOPDebit = xDummyAmount
                Else
                    mDummayOPDebit = xDummyAmount
                End If
            End If
            If mOpening >= 0 Then
                mOpDr = mOpening + mDummayOPDebit
                mOpCr = 0
            Else
                mOpDr = 0
                mOpCr = System.Math.Abs(mOpening) + mDummayOPDebit
            End If
        End If
        DisplayTotals(mOpDr, mOpCr, mAdjustedAmount, mUnAdjustedAmount)
        LedgInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        LedgInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        UltraDataSource1.Rows.Clear()

        ClearFilterFromUltraGrid(UltraGrid1)
        ClearGroupFromUltraGrid(UltraGrid1)

        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()

            'UltraGridColumnChooser1.SourceGrid = UltraGrid1
            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
        '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION AS NARRATION, " & vbCrLf _
        ''" & mainclass.LastDay(month(trn.VDATE),Year(TRN.VDATE)) & "' || '/' ||
        ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE) || CHR(13) ELSE '' END || DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END AS NARRATION, " & vbCrLf _'
        If OptSumDet(0).Checked = True Then

            SqlStr = " SELECT "

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                SqlStr = SqlStr & vbCrLf & " '' AS LOCKED,"
            Else
                SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE AS LOCKED,"
            End If

            SqlStr = SqlStr & vbCrLf _
                & " TRN.BOOKTYPE , " & vbCrLf _
                & " TRN.BOOKSUBTYPE,  " & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.VDATE) ELSE TRN.VDATE END V_DATE, " & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END AS V_NO, " & vbCrLf _
                & " MAX(CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  '' ELSE TO_CHAR(TRN.BILLDATE,'DD/MM/YYYY') END) AS BILLDATE, "

            SqlStr = SqlStr & vbCrLf _
                & " MIN(GETLEDGEREXPHEAD (TRN.COMPANY_CODE,  TRN.FYEAR, TRN.BOOKCODE, TRN.MKEY, TRN.VNO, TRN.ACCOUNTCODE)) AS EXPENSES_HEAD," & vbCrLf

            SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.VDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END AS NARRATION, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf _
                & " '','', " & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE REPLACE(TRN.REMARKS, chr(13)||chr(10),' ') END, " & vbCrLf & " CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY') AS CHEQUENO, " & vbCrLf _
                & "  "

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                SqlStr = SqlStr & vbCrLf & " '' AS COMPANY_SHORTNAME,"
            Else
                SqlStr = SqlStr & vbCrLf & " GCMT.COMPANY_SHORTNAME,"
            End If
            ''GETBILLBALANCEAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE)

            'SqlStr = SqlStr & vbCrLf _
            '    & " SUM(CASE WHEN BILLTYPE='B' THEN TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)-GETBILLBALANCEAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE) ELSE CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN 0 ELSE TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) END END)  AS ADJUST_AMOUNT, " & vbCrLf _
            '    & " SUM(CASE WHEN BILLTYPE='B' THEN GETBILLBALANCEAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE) ELSE CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) ELSE 0 END END)  AS UNADJUST_AMOUNT, "

            'SqlStr = SqlStr & vbCrLf _
            '    & " SUM(CASE WHEN BILLTYPE='D' OR BILLTYPE='T' THEN 0 ELSE GETADJUSTEDAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE)*DECODE(BILLTYPE,'B',-1,1) END) AS ADJUST_AMOUNT, " & vbCrLf _
            '    & " SUM(CASE WHEN BILLTYPE='D' OR BILLTYPE='T' THEN 0 ELSE GETUNADJUSTEDAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE)*DECODE(BILLTYPE,'B',-1,1) END)  AS UNADJUST_AMOUNT, "

            If chkAdjustDetail.CheckState = System.Windows.Forms.CheckState.Checked Then

                SqlStr = SqlStr & vbCrLf _
                    & " ABS(SUM(CASE WHEN BILLTYPE='D' OR BILLTYPE='T' THEN TRN.AMOUNT ELSE GETADJUSTEDAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE, TRN.BOOKTYPE, TRN.BOOKSUBTYPE, TRN.VNO) END)) AS ADJUST_AMOUNT, " & vbCrLf _
                    & " SUM(CASE WHEN BILLTYPE='D' OR BILLTYPE='T' THEN 0 ELSE GETUNADJUSTEDAMOUNT(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE, TRN.VDATE, TRN.BOOKTYPE, TRN.BOOKSUBTYPE, TRN.VNO) END)  AS UNADJUST_AMOUNT, "

            Else
                SqlStr = SqlStr & vbCrLf _
                    & " 0 AS ADJUST_AMOUNT, " & vbCrLf _
                    & " 0 AS UNADJUST_AMOUNT, "

            End If

            'SqlStr = SqlStr & vbCrLf _
            '    & " ABS(SUM(ADJUSTED_AMOUNT)) AS ADJUST_AMOUNT, " & vbCrLf _
            '    & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))-ABS(SUM(ADJUSTED_AMOUNT))  AS UNADJUST_AMOUNT, "


            'SqlStr = SqlStr & vbCrLf _
            '    & " SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN 0 ELSE TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) END)  AS ADJUST_AMOUNT, " & vbCrLf _
            '    & " SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) ELSE 0 END)  AS UNADJUST_AMOUNT, "

            SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN BOOKCODE=" & ConSalesBookCode & " THEN GETVENDORCODE(TRN.COMPANY_CODE,TRN.MKEY,TRN.VNO) ELSE '' END AS VENDOR_CODE, " & vbCrLf _
                & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE, " & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.EXPDATE) ELSE TRN.EXPDATE END EXP_DATE, " & vbCrLf _
                & " MAX(TRN.CLEARDATE), TRN.ADDUSER, TRN.ADDDATE, MAX(TRN.MODUSER),MAX(TRN.MODDATE)," & vbCrLf _
                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END ,"

            SqlStr = SqlStr & vbCrLf _
                & " '','' "

        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = " SELECT '', '','',TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),'','', '', ''," & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf _
                & " '','', '','', " & vbCrLf _
                & " '' AS COMPANY_SHORTNAME,"

            If chkAdjustDetail.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf _
                    & " ABS(SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN 0 ELSE TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) END))  AS ADJUST_AMOUNT," & vbCrLf _
                    & " ABS(SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) ELSE 0 END))  AS UNADJUST_AMOUNT,"
            Else
                SqlStr = SqlStr & vbCrLf _
                    & " 0  AS ADJUST_AMOUNT," & vbCrLf _
                    & " 0 AS UNADJUST_AMOUNT,"

            End If

            SqlStr = SqlStr & vbCrLf _
                & " '' AS VENDOR_CODE, '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE,'','','','','','','','','','' "

        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = " SELECT '', '','',TO_CHAR(TRN.VDATE,'MON-YYYY'),'','', '',''," & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf _
                & " '','','', " & vbCrLf _
                & " '' AS COMPANY_SHORTNAME, "

            If chkAdjustDetail.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf _
                    & " ABS(SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN 0 ELSE TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) END)) AS ADJUST_AMOUNT," & vbCrLf _
                    & " ABS(SUM(Case When BILLTYPE='P' AND TRNTYPE='O' THEN TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) ELSE 0 END)) AS UNADJUST_AMOUNT,"
            Else
                SqlStr = SqlStr & vbCrLf _
                   & " 0  AS ADJUST_AMOUNT," & vbCrLf _
                   & " 0 AS UNADJUST_AMOUNT,"
            End If
            SqlStr = SqlStr & vbCrLf _
                & " '' AS VENDOR_CODE,'' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE,'','','','','','','','','' "
        End If
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function MakeSQLForInsert(ByRef pAccountName As String, ByRef pPartyAdd As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
        '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION, " & vbCrLf _
        '
        SqlStr = " Select '" & MainClass.AllowSingleQuote(PubUserID) & "', "

        '

        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr _
                & " TO_CHAR(Vdate,'YYYYMMDD') ," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pAccountName) & "'," & vbCrLf _
                & " TRN.VDATE, " & vbCrLf _
                & " TRN.VNO , "

            If frmPrintLedg.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION)  AS NARRATION,"
            Else
                SqlStr = SqlStr & vbCrLf & " '' AS NARRATION,"
            End If


            SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END , " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END , "

            If frmPrintLedg.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " CHEQUENO AS CHEQUENO, "
            Else
                SqlStr = SqlStr & vbCrLf & " '' AS CHEQUENO, "
            End If


            SqlStr = SqlStr & vbCrLf _
                & " MIN(GETLEDGEREXPHEAD (TRN.COMPANY_CODE,  TRN.FYEAR, TRN.BOOKCODE, TRN.MKEY, TRN.VNO, TRN.ACCOUNTCODE)) AS ACCOUNTNAME,'', " & vbCrLf _
                & " TRN.REMARKS ,'" & MainClass.AllowSingleQuote(pPartyAdd) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN TRN.BOOKTYPE='B' AND TRN.BOOKSUBTYPE='P' THEN 'BANK PAYMENT'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='B' AND TRN.BOOKSUBTYPE='R' THEN 'BANK RECEIPT'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='C' AND TRN.BOOKSUBTYPE='P' THEN 'CASH PAYMENT'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='C' AND TRN.BOOKSUBTYPE='R' THEN 'CASH RECEIPT'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='J' THEN 'JOURNAL'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='S' THEN 'SALE'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='P' THEN 'PURCHASE'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='L' THEN 'CREDIT NOTE'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='U' THEN 'SUPPLIMENTRY'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='E' THEN 'DEBIT NOTE'" & vbCrLf _
                & " WHEN TRN.BOOKTYPE='R' THEN 'CREDIT NOTE'" & vbCrLf _
                & " ELSE 'OTHERS' END"



            'SqlStr = SqlStr _
            '   & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'YYYYMMDD') ELSE TO_CHAR(Vdate,'YYYYMMDD') END," & vbCrLf _
            '   & " '" & MainClass.AllowSingleQuote(pAccountName) & "'," & vbCrLf _
            '   & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.VDATE,'DD/MM/YYYY') END, " & vbCrLf _
            '   & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MONYYYY') ELSE TRN.VNO END, " & vbCrLf _
            '   & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.VDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) END AS NARRATION," & vbCrLf _
            '   & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END , " & vbCrLf _
            '   & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END , " & vbCrLf _
            '   & " CHEQUENO AS CHEQUENO, " & vbCrLf & " '','', " & vbCrLf _
            '   & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE TRN.REMARKS END,'" & MainClass.AllowSingleQuote(pPartyAdd) & "'"

            ''& " DEPT.DEPT_CODE,EMP.EMP_CODE,COSTC.COST_CENTER_CODE,TRN.MKEY,'','' "   || ' ' || CHQDATE 
        ElseIf OptSumDet(1).Checked = True Then
            '
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(Vdate,'YYYYMMDD'),'', TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),'','', " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf _
                & " '','', '','','',''"
        ElseIf OptSumDet(2).Checked = True Then
            '
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(Vdate,'YYYYMM'),'',TO_CHAR(Vdate,'MON-YYYY'),'','', " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf _
                & " '','','','','',''"
        End If
        MakeSQLForInsert = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLForInsert = ""
    End Function
    Private Function MakeOPSQL(ByRef pAccountCode As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""

        SqlStr = " SELECT " & vbCrLf _
            & " SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT)  AS OPENING, "

        'SqlStr = SqlStr & vbCrLf _
        '    & " SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN 0 ELSE TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) END)  AS ADJUST_AMOUNT,SUM(CASE WHEN BILLTYPE='P' AND TRNTYPE='O' THEN TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1) ELSE 0 END)  AS UNADJUST_AMOUNT " & vbCrLf

        SqlStr = SqlStr & vbCrLf _
            & " 0  AS ADJUST_AMOUNT, 0 AS UNADJUST_AMOUNT " & vbCrLf

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
            & " WHERE  "

        If PubUserID = "A00001" Then
            SqlStr = SqlStr & vbCrLf & " GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)='" & mAccountCode & "'"
        Else
            'SqlStr = SqlStr & vbCrLf & " TRN.ACCOUNTCODE='" & mAccountCode & "'"

            If optAccount.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " TRN.ACCOUNTCODE='" & mAccountCode & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " TRN.ACCOUNTCODE IN (SELECT DISTINCT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=TRN.COMPANY_CODE AND GROUPCODE='" & mAccountCode & "')"
            End If

        End If

        'If cboCompany.SelectedIndex > 0 Then
        '    mCompanyName = Trim(cboCompany.Text)
        '    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '        mCompanyCode = MasterNo
        '    End If
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & mCompanyCode & ""
        'End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        mGroupOption = GetGroupOption()
        mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ( " & mGroupOption & " ) "
        End If
        SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        End If
        '    If CboCC.Text = "ALL" Then
        '        mCostCName = ""
        '    Else
        '        mCostCName = MainClass.AllowSingleQuote(CboCC.Text)
        '    End If
        '
        '    If CboDept.Text = "ALL" Then
        '        mDeptName = ""
        '    Else
        '        mDeptName = MainClass.AllowSingleQuote(CboDept.Text)
        '    End If
        '
        '    If cboEmp.Text = "ALL" Then
        '        mEmp = ""
        '    Else
        '        mEmp = MainClass.AllowSingleQuote(cboEmp.Text)
        '    End If
        MakeOPSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeOPSQL = ""
    End Function
    Private Function MakeSQLCond(ByRef mIsOpening As Boolean) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpHeadCode As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String

        If CboCC.Text = "ALL" Then
            mCostCCode = ""
        Else
            If MainClass.ValidateWithMasterTable((CboCC.Text), "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCostCCode = MasterNo
            End If
        End If
        If cboExpHead.Text = "ALL" Then
            mExpHeadCode = ""
        Else
            If MainClass.ValidateWithMasterTable((cboExpHead.Text), "COST_CENTER_DESC", "COST_CENTER_CODE", "CST_CENTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExpHeadCode = MasterNo
            End If
        End If
        If CboDept.Text = "ALL" Then
            mDeptCode = ""
        Else
            If MainClass.ValidateWithMasterTable((CboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
            End If
        End If
        If cboEmp.Text = "ALL" Then
            mEmpCode = ""
        Else
            If MainClass.ValidateWithMasterTable((cboEmp.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCode = MasterNo
            End If
        End If
        '    mConsolidated = IIf(cboConsolidated.ListIndex = -1, "D", Left(cboConsolidated.Text, 1))
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST GCMT " & vbCrLf _
            & " WHERE TRN.Company_Code = ACM.Company_Code AND TRN.Company_Code = GCMT.Company_Code"

        If PubUserID = "A00001" Then
            SqlStr = SqlStr & vbCrLf & " AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)=ACM.SUPP_CUST_CODE "
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If

            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ACM.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If lblBookType.Text = ConLedger Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        End If
        ''

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN A1, FIN_SUPP_CUST_MST B1 WHERE A1.COMPANY_CODE=B1.COMPANY_CODE AND A1.ACCOUNTCODE=B1.SUPP_CUST_CODE AND ACCOUNT_HIDE='Y')"
            'SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN WHERE SUPP_CUST_CODE = '11848')"
        End If

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE||BILLTYPE<>'PP'"

        If lblBookType.Text = ConJournalBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConJournalBookCode & "' "
        ElseIf lblBookType.Text = ConContraBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConContraBookCode & "' "
        Else
            If PubUserID = "A00001" Then
                SqlStr = SqlStr & vbCrLf & " AND GETDUMMYACCOUNTCODE(TRN.COMPANY_CODE,TRN.FYEAR, TRN.ACCOUNTCODE, TRN.MKEY)='" & mAccountCode & "'"
            Else
                If optAccount.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE IN (SELECT DISTINCT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=TRN.COMPANY_CODE AND GROUPCODE='" & mAccountCode & "')"
                End If
            End If
            End If
        If mCostCCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.COSTCCODE='" & mCostCCode & "'"
        End If
        If mDeptCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.DEPTCODE='" & mDeptCode & "'"
        End If
        If mEmpCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EMPCODE='" & mEmpCode & "'"
        End If
        If mExpHeadCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EXP_CODE='" & mExpHeadCode & "'"
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        End If
        mGroupOption = GetGroupOption()

        If mIsOpening = True Then
            mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        End If
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        End If
        If mIsOpening = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
    End Function
    Private Function MakeSQLCondInsert(ByRef mIsOpening As Boolean) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String


        '& " PAY_EMPLOYEE_MST EMP,PAY_DEPT_MST DEPT,CST_CENTER_MST COSTC " & vbCrLf _
        '
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM" & vbCrLf _
            & " WHERE TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        '" & vbCrLf _
        '    & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        '    & " And


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
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
            & " AND TRN.Company_Code =ACM.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        '& vbCrLf |            & " AND EMP.Company_Code(+) = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf |            & " AND TRN.EMPCODE=EMP.EMP_CODE(+) " & vbCrLf |            & " AND COSTC.Company_Code(+) = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf |            & " AND TRN.COSTCCODE=COSTC.COST_CENTER_CODE(+) " & vbCrLf |            & " AND DEPT.Company_Code(+) = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf |            & " AND TRN.DEPTCODE=DEPT.DEPT_CODE(+) "
        '    If CboCC.Text <> "ALL" Then SqlStr = SqlStr & vbCrLf & " AND COSTCALIAS='" & MainClass.AllowSingleQuote(CboCC.Text) & "'"
        '    If CboDept.Text <> "ALL" Then SqlStr = SqlStr & vbCrLf & " AND DEPTALIAS='" & MainClass.AllowSingleQuote(CboDept.Text) & "'"
        '    If cboEmp.Text <> "ALL" Then SqlStr = SqlStr & vbCrLf & " AND EMPALIAS='" & MainClass.AllowSingleQuote(cboEmp.Text) & "'"
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        End If
        mGroupOption = GetGroupOption()

        If mIsOpening = True Then
            mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        End If
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        End If
        If mIsOpening = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '        SqlStr = SqlStr & vbCrLf _
            ''                & " AND TRN.Vdate>='" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' " & vbCrLf _
            ''                & " AND TRN.Vdate<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' "
        End If
        MakeSQLCondInsert = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCondInsert = ""
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
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConSaleBook & "' OR  TRN.BookType = '" & ConSaleDebitBook & "' OR  TRN.BookType = '" & ConSaleCreditBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPurchaseBook & "' OR TRN.BookType = '" & ConGRBook & "' OR TRN.BookType = '" & ConPurchaseSuppBook & "'"
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
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mAccountCode2 As String
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If lblBookType.Text = ConLedger Then
            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        End If

        If optGroup.Checked = True Then
            If MainClass.ValidateWithMasterTable((cboAccount.Text), "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            End If
        Else
            If MainClass.ValidateWithMasterTable((cboAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                Select Case lblBookType.Text
                    Case ConLedger
                        MsgInformation("Please Select Account")
                        Exit Function
                    Case ConCashBook, ConBankBook
                        MsgInformation("Please Select Book")
                        Exit Function
                    Case ConPurchaseBook
                        mAccountCode = CStr(ConPurchaseBookCode)
                    Case ConPurchaseGenBook
                        mAccountCode = CStr(ConPurchaseGenBookCode)
                    Case ConSaleBook
                    '                If cboAccount.ListIndex = 0 Then
                    '                    mAccountCode = ConSalesBookCode
                    '                Else
                    '                    mAccountCode = ConExciseSalesBookCode
                    '                End If
                    Case ConJournalBook
                        mAccountCode = CStr(ConJournalBookCode)
                    Case ConDebitNoteBook
                        mAccountCode = CStr(ConDebitNoteBookCode)
                    Case ConCreditNoteBook
                        mAccountCode = CStr(ConCreditNoteBookCode)
                    Case ConGRBook
                        mAccountCode = CStr(ConGRBookCode)
                End Select
            End If
        End If

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAgtAccount.Text) = "" Then
                MsgInformation("Please Select Agt. Account Name.")
                TxtAgtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAgtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please valid Agt. Account Name.")
                TxtAgtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        If CheckLimitedAccountRights(mAccountCode) = False Then
            MsgInformation("You have not Enough Rights to Show Such Account Ledger.")
            TxtAgtAccount.Focus()
            FieldsVerification = False
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Function CheckLimitedAccountRights(ByRef mAccountCode As String) As Boolean
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        CheckLimitedAccountRights = True
        SqlStr = " SELECT USER_ID FROM ATH_LEDGER_RIGHTS " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            CheckLimitedAccountRights = True
            Exit Function
        End If

        SqlStr = " SELECT USER_ID FROM ATH_LEDGER_RIGHTS " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckLimitedAccountRights = True
            Exit Function
        Else
            CheckLimitedAccountRights = False
            Exit Function
        End If
        Exit Function
ERR1:
        CheckLimitedAccountRights = False
    End Function
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If lblBookType.Text = ConLedger Then
            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
                txtDateTo.Focus()
                Cancel = True
                GoTo EventExitSub
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header

            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True
            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "")

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Caption = "Company Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1).Header.Caption = "Book Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookSubType - 1).Header.Caption = "Book Sub Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Header.Caption = "V Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Header.Caption = "VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Header.Caption = "Bill / Ref Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColExpenseHead - 1).Header.Caption = "Expenses Head"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNarration - 1).Header.Caption = "Narration"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1).Header.Caption = "Debit Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1).Header.Caption = "Credit Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalance - 1).Header.Caption = "Balance"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalDC - 1).Header.Caption = "Bal DC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDetail - 1).Header.Caption = "Bill Details"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Header.Caption = "Cheque No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnitName - 1).Header.Caption = "Unit Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdjustedAmount - 1).Header.Caption = "Adjustment Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnAdjustedAmount - 1).Header.Caption = "Un Adjustment Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVendorCode - 1).Header.Caption = "Vendor Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDept - 1).Header.Caption = "Department"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColEmp - 1).Header.Caption = "Employee"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCostC - 1).Header.Caption = "CostC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColExpDate - 1).Header.Caption = "MRR / Expense Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubRowNo - 1).Header.Caption = "Sub Row No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBranch - 1).Header.Caption = "Branch"
            ''

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColClearDate - 1).Header.Caption = "Clear Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Header.Caption = "Add User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Header.Caption = "Add Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Header.Caption = "Modify User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Header.Caption = "Modify Date"



            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
                Me.UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellMultiLine = DefaultableBoolean.True
                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.FixedHeaderIndicator = FixedHeaderIndicator.Button ''FixedHeaderIndicator.None
                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Fixed = False     ''True
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).SortIndicator = SortIndicator.Ascending
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalance - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdjustedAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnAdjustedAmount - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalance - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdjustedAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnAdjustedAmount - 1).CellAppearance.TextHAlign = HAlign.Right

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookSubType - 1).Hidden = True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Hidden = IIf(OptSumDet(0).Checked = True, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Hidden = IIf(OptSumDet(0).Checked = True, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNarration - 1).Hidden = IIf(OptSumDet(0).Checked = True, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDetail - 1).Hidden = IIf(OptSumDet(0).Checked = True, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Hidden = IIf(OptSumDet(0).Checked = True, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnitName - 1).Hidden = IIf(OptSumDet(0).Checked = True, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColEmp - 1).Hidden = True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCostC - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDept - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubRowNo - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBranch - 1).Hidden = True


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookSubType - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNarration - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDAmount - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCAmount - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalance - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalDC - 1).Width = 40
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDetail - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnitName - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColExpenseHead - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColClearDate - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Width = 90


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdjustedAmount - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnAdjustedAmount - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVendorCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDept - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColEmp - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCostC - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColExpDate - 1).Width = 80



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubRowNo - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBranch - 1).Width = 90


            ''CHANGE BY SANDEEP

            Me.UltraGrid1.DisplayLayout.Override.DefaultRowHeight = 30
            Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle   ''HeaderClickAction.Select
            Me.UltraGrid1.DisplayLayout.Override.SelectTypeCol = SelectType.None
            Me.UltraGrid1.DisplayLayout.Override.SelectedAppearancesEnabled = DefaultableBoolean.False
            'Me.UltraGrid1.DisplayLayout.Override.SelectTypeCell = SelectType.ExtendedAutoDrag
            'Me.UltraGrid1.DisplayLayout.Override.SelectTypeRow = SelectType.ExtendedAutoDrag
            Me.UltraGrid1.DisplayLayout.Override.RowSizingAutoMaxLines = True

            Me.UltraGrid1.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.Copy

            Me.UltraGrid1.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect
            Me.UltraGrid1.DisplayLayout.Override.FixedRowStyle = FixedRowStyle.Top
            Me.UltraGrid1.DisplayLayout.Override.FilterRowAppearance.BackColor = Color.LightYellow
            Me.UltraGrid1.DisplayLayout.Override.CellAppearance.BackColor = Color.White
            Me.UltraGrid1.DisplayLayout.Override.CellAppearance.ForeColor = Color.Navy
            ''CHANGE BY SANDEEP

            'Me.UltraGrid1.DisplayLayout.Override.FixedRowStyle = FixedRowStyle.Bottom

            'Me.UltraGrid1.DisplayLayout.Override.GroupByColumnsHidden = Infragistics.Win.DefaultableBoolean.True

            'Me.UltraGrid1.DisplayLayout.UseFixedHeaders = True
            'Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Header.Fixed = True

            'Me.UltraGrid1.DisplayLayout.Override.FixedRowIndicator = FixedRowIndicator.Button
            'Me.UltraGrid1.Rows(0).AllowFixing = Infragistics.Win.DefaultableBoolean.False

            'Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Header.Fixed = True
            'Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Header.Fixed = True
            ''Me.UltraGrid1.DisplayLayout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow
            ''Me.UltraGrid1.DisplayLayout.Override.FixedCellAppearance.BackColor = Color.LightYellow
            'Me.UltraGrid1.DisplayLayout.Override.FixedCellSeparatorColor = Color.DarkBlue


            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default


            '' Format the Running Total column as currency.
            'UltraGrid1.Columns(ColBalance - 1).DefaultCellStyle.Format = "c"
            '' Set the ValueType of the Running Total column to Decimal.
            'Me.UltraGrid1.Columns(ColBalance - 1).ValueType = GetType(System.Decimal)



        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub UltraGrid1_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout



        ''
        ' Turn on all of the Cut, Copy, and Paste functionality. 
        e.Layout.Override.AllowMultiCellOperations = AllowMultiCellOperation.Copy

        ' In order to cut or copy, the user needs to select cells or rows. 
        ' So set CellClickAction so that clicking on a cell selects that cell
        ' instead of going into edit mode.
        e.Layout.Override.CellClickAction = CellClickAction.CellSelect


        ''Allowing Summaries in the UltraGrid 
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True
        '' Setting the Sum Summary for the desired column

        e.Layout.Bands(0).Summaries.Add("ColDAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColDAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColCAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColCAmount - 1))


        ''Set the display format to be just the number 
        e.Layout.Bands(0).Summaries("ColDAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColCAmount").DisplayFormat = "{0:###0.00}"

        ''Hide the SummaryFooterCaption row 
        e.Layout.Bands(0).Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False

        'e.Layout.Bands(0).SummaryFooterCaption = "TOTAL :"
        Me.UltraGrid1.DisplayLayout.Bands(0).SummaryFooterCaption = "TOTAL :"

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSteelBlue

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black

        e.Layout.Override.BorderStyleSummaryValue = Infragistics.Win.UIElementBorderStyle.None

        '     / Here, I want to add grand total

        e.Layout.Bands(0).Summaries("ColDAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColCAmount").Appearance.TextHAlign = HAlign.Right

        'Disable grid default highlight

        'UltraGrid1.DisplayLayout.Override.ResetActiveRowAppearance()
        'UltraGrid1.DisplayLayout.Override.ResetActiveCellAppearance()
        e.Layout.Override.ActiveAppearancesEnabled = DefaultableBoolean.True

        e.Layout.Override.CellAppearance.BackColor = Color.White
        e.Layout.Override.CellAppearance.ForeColor = Color.Navy
        e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy

    End Sub

    Private Sub UltraGrid1_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGrid1.AfterRowActivate
        Try
            UltraGrid1.DisplayLayout.Override.ActiveRowAppearance.BackColor = UltraGrid1.ActiveRow.Appearance.BackColor
            UltraGrid1.DisplayLayout.Override.ActiveRowAppearance.ForeColor = UltraGrid1.ActiveRow.Appearance.ForeColor

        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UltraGrid1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraGrid1.KeyDown
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.Enter Then Call UltraGrid1_DoubleClick(sender, e)

        'If KeyCode = System.Windows.Forms.Keys.ControlKey And KeyCode = System.Windows.Forms.Keys.R Then
        '    Dim mMaxRow As Integer
        '    Dim mCheckFieldValue As String
        '    Dim ultRow As UltraGridRow ''UltraDataRow
        '    Dim I As Long
        '    Dim mCurrRow As Integer
        '    Dim ultRemoveRow As UltraGridRow ''UltraDataRow
        '    Dim Response As String

        '    'Me.UltraDataSource1 = Nothing
        '    mMaxRow = UltraGrid1.Rows.Count - 1
        '    ultRow = UltraGrid1.Rows(mMaxRow) '' Me.UltraDataSource1.Rows(mMaxRow)

        '    mCurrRow = UltraGrid1.ActiveRow.Tag
        '    ultRemoveRow = UltraGrid1.Rows(mCurrRow)

        '    mCheckFieldValue = ultRemoveRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1))

        '    If mCheckFieldValue <> "" Then
        '        UltraGrid1.Rows(mCurrRow).Delete()
        '        'mMaxRow = UltraGrid1.Rows.Count - 1
        '        'For I = 0 To mMaxRow
        '        '    UltraGrid1.Rows(I).Tag = I
        '        'Next
        '        Call FillRunBalCol()
        '    End If

        'End If

    End Sub

    Private Sub cboAccount_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles cboAccount.InitializeLayout
        Try
            e.Layout.Override.AllowRowFiltering = DefaultableBoolean.True
            e.Layout.Override.FilterUIType = FilterUIType.FilterRow
            'e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle
            'e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboAccount_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles cboAccount.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, cboAccount.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub UltraGrid1_AfterSortChange(sender As Object, e As BandEventArgs) Handles UltraGrid1.AfterSortChange
        Try
            If ConShowActive = True Then Exit Sub
            If FormActive = False Then Exit Sub
            Call FillRunBalCol()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UltraGrid1_AfterRowFilterChanged(sender As Object, e As AfterRowFilterChangedEventArgs) Handles UltraGrid1.AfterRowFilterChanged
        Try
            If ConShowActive = True Then Exit Sub
            If FormActive = False Then Exit Sub
            Call FillRunBalCol()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdOutstanding_Click(sender As Object, e As EventArgs) Handles cmdOutstanding.Click




        frmViewOuts.MdiParent = Me.MdiParent
        frmViewOuts.Show()
        frmViewOuts.TxtName.Text = cboAccount.Text
        frmViewOuts.chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        frmViewOuts.lblOutsType.Text = ""
        frmViewOuts.TxtName.Enabled = True
        frmViewOuts.cmdsearch.Enabled = True
        frmViewOuts.frmViewOuts_Activated(Nothing, New System.EventArgs())
    End Sub

    Private Sub cmdMasterDetail_Click(sender As Object, e As EventArgs) Handles cmdMasterDetail.Click
        Try
            fraMasterDetail.Visible = Not fraMasterDetail.Visible

            txtCreditLimit.Text = ""
            txtPaymentTerms.Text = ""
            txtSecurityDeposit.Text = ""
            txtSecurityAmount.Text = ""

            txtSecurityChqNo.Text = ""
            txtBankName.Text = ""
            txtSaleRep.Text = ""

            If fraMasterDetail.Visible = True Then
                Dim mSqlStr As String = ""
                Dim RsTemp As ADODB.Recordset = Nothing
                Dim mPaymentTermCode As String = ""

                mSqlStr = "Select CREDIT_LIMIT, PAYMENT_CODE, DECODE(IS_SECURITY_DEPOSIT,'Y','YES','NO') AS IS_SECURITY_DEPOSIT, " & vbCrLf _
                        & " SECURITY_AMOUNT, SECURITY_CHEQUE_NO, RESPONSIBLE_PERSON, CUST_BANK_BANK, BANK_BRANCH_NAME, BANK_IFSC_CODE" & vbCrLf _
                        & " From FIN_SUPP_CUST_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And LTRIM(RTRIM(SUPP_CUST_NAME))='" & MainClass.AllowSingleQuote(cboAccount.Text) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    txtCreditLimit.Text = IIf(IsDBNull(RsTemp.Fields("CREDIT_LIMIT").Value), "", RsTemp.Fields("CREDIT_LIMIT").Value)
                    mPaymentTermCode = IIf(IsDBNull(RsTemp.Fields("PAYMENT_CODE").Value), "", RsTemp.Fields("PAYMENT_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mPaymentTermCode, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtPaymentTerms.Text = MasterNo
                    End If
                    txtSecurityDeposit.Text = IIf(IsDBNull(RsTemp.Fields("IS_SECURITY_DEPOSIT").Value), "", RsTemp.Fields("IS_SECURITY_DEPOSIT").Value)
                    txtSecurityAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SECURITY_AMOUNT").Value), 0, RsTemp.Fields("SECURITY_AMOUNT").Value), "0.00")

                    txtSecurityChqNo.Text = IIf(IsDBNull(RsTemp.Fields("SECURITY_CHEQUE_NO").Value), "", RsTemp.Fields("SECURITY_CHEQUE_NO").Value)

                    ', , BANK_BRANCH_NAME, BANK_IFSC_CODE

                    txtBankName.Text = IIf(IsDBNull(RsTemp.Fields("CUST_BANK_BANK").Value), "", RsTemp.Fields("CUST_BANK_BANK").Value)
                    txtSaleRep.Text = IIf(IsDBNull(RsTemp.Fields("RESPONSIBLE_PERSON").Value), "", RsTemp.Fields("RESPONSIBLE_PERSON").Value)


                End If
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub optAccount_CheckedChanged(sender As Object, e As EventArgs) Handles optAccount.CheckedChanged
        If FormActive = False Then Exit Sub
        Call FillAccountComboBox()
    End Sub

    Private Sub optGroup_CheckedChanged(sender As Object, e As EventArgs) Handles optGroup.CheckedChanged
        If FormActive = False Then Exit Sub

        Call FillAccountComboBox()
    End Sub
    Private Sub FillAccountComboBox()
        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset
        Dim SqlStr As String
        Dim CntLst As Long
        Dim mCompanyName As String

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        cboAccount.Text = ""

        oledbCnn = New OleDbConnection(StrConn)

        If optAccount.Checked = True Then
            SqlStr = "Select DISTINCT SUPP_CUST_NAME, SUPP_CUST_CODE, SUPP_CUST_ADDR,  SUPP_CUST_CITY, SUPP_CUST_STATE " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SUPP_CUST_NAME"

        Else
            SqlStr = "Select DISTINCT GROUP_NAME, GROUP_CODE " & vbCrLf _
                   & " FROM FIN_GROUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY GROUP_NAME"
        End If
        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboAccount.DataSource = ds
        cboAccount.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        cboAccount.Appearance.FontData.SizeInPoints = 8.5

        If optAccount.Checked = True Then
            cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
            cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
            cboAccount.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
            cboAccount.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
            cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"
            ''cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"

            cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
            cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100
            cboAccount.DisplayLayout.Bands(0).Columns(2).Width = 350
            cboAccount.DisplayLayout.Bands(0).Columns(3).Width = 100
            cboAccount.DisplayLayout.Bands(0).Columns(4).Width = 100
        Else
            cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
            cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"


            cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
            cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100

        End If

        cboAccount.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        cboAccount.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        'cboCompany.Rows(0).Selected = True


        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

End Class
