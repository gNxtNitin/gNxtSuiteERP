Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMRRDayBook
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColRefType As Short = 1
    Private Const ColTOTMRR As Short = 2
    Private Const ColPostInAcct As Short = 3
    Private Const ColPendingInStore As Short = 4
    Private Const ColPendingInQC As Short = 5
    Private Const ColPendingInAcct As Short = 6

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
        Else
            TxtAccount.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='S')")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPendingGR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        Dim SqlStr As String
        Dim mCol2 As Double
        Dim mCol3 As Double
        Dim mCol4 As Double
        Dim mCol5 As Double
        Dim mCol6 As Double

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)

        SqlStr = MakeSQL

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Dim mCntRow As Integer
        Dim mGTotal As Double

        With SprdMain
            For mCntRow = 1 To .MaxRows
                .Row = mCntRow
                .Col = ColTOTMRR
                mCol2 = mCol2 + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPostInAcct
                mCol3 = mCol3 + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPendingInStore
                mCol4 = mCol4 + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPendingInQC
                mCol5 = mCol5 + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPendingInAcct
                mCol6 = mCol6 + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next
        End With
        Call MainClass.AddBlankfpSprdRow(SprdMain, ColRefType)

        With SprdMain
            .Row = .MaxRows
            .Col = ColRefType
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "TOTAL :"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows
            .Col = ColTOTMRR
            .Text = CStr(mCol2)
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColPostInAcct
            .Text = CStr(mCol3)
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColPendingInStore
            .Text = CStr(mCol4)
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColPendingInQC
            .Text = CStr(mCol5)
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColPendingInAcct
            .Text = CStr(mCol6)
            .Font = VB6.FontChangeBold(.Font, True)


        End With

        FormatSprdMain()
        Call PrintStatus(True)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If CDbl(Trim(TxtAccount.Text)) = -1 Then
                MsgInformation("Please Select Account")
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmMRRDayBook_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmMRRDayBook_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("Start_Date").Value
        txtDateTo.Text = CStr(RunDate)
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "SELECT " & vbCrLf & " CASE WHEN REF_TYPE='C' THEN 'CASH' " & vbCrLf & " WHEN REF_TYPE='D' THEN 'DS' " & vbCrLf & " WHEN REF_TYPE='F' THEN 'FOC' " & vbCrLf & " WHEN REF_TYPE='J' THEN 'JOBWORK' " & vbCrLf & " WHEN REF_TYPE='P' THEN 'PO' " & vbCrLf & " WHEN REF_TYPE='R' THEN 'RGP' " & vbCrLf & " WHEN REF_TYPE='I' THEN 'SALE RETURN' WHEN REF_TYPE='1' THEN 'JOB WORK RETURN' WHEN REF_TYPE='2' THEN 'SALE RETURN (WARRANTY)' END AS REFTYPE, " & vbCrLf & " TO_CHAR(COUNT(GRMain.AUTO_KEY_MRR)) AS TOTMRR, " & vbCrLf & " TO_CHAR(SUM(DECODE(MRR_FINAL_FLAG,'Y',1,0))) AS POSTED, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='Y' AND MRR_FINAL_FLAG='N' THEN 1 ELSE 0 END)) AS PENDS, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='N' AND MRR_FINAL_FLAG='N' THEN 1 ELSE 0 END)) AS PENDQ, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN GRMain.SEND_AC_FLAG='Y' AND MRR_FINAL_FLAG='N' THEN 1 ELSE 0 END)) AS PENDA " & vbCrLf & " FROM INV_GATE_HDR GRMain,FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE GRMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND GRMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND GRMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " And MRR_STATUS='N'"

        SqlStr = SqlStr & " GROUP BY " & vbCrLf & " CASE WHEN REF_TYPE='C' THEN 'CASH' " & vbCrLf & " WHEN REF_TYPE='D' THEN 'DS' " & vbCrLf & " WHEN REF_TYPE='F' THEN 'FOC' " & vbCrLf & " WHEN REF_TYPE='J' THEN 'JOBWORK' " & vbCrLf & " WHEN REF_TYPE='P' THEN 'PO' " & vbCrLf & " WHEN REF_TYPE='R' THEN 'RGP' " & vbCrLf & " WHEN REF_TYPE='I' THEN 'SALE RETURN' WHEN REF_TYPE='1' THEN 'JOB WORK RETURN' WHEN REF_TYPE='2' THEN 'SALE RETURN (WARRANTY)' END "

        '        SqlStr = SqlStr & vbCrLf _
        ''                & " ORDER BY REF_TYPE"

        MakeSQL = SqlStr

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain()

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColPendingInAcct
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColRefType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefType, 22)

            For cntCol = ColTOTMRR To ColPendingInAcct
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 0
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColRefType
            .Text = "Ref Type"

            .Col = ColTOTMRR
            .Text = "Total MRR Prepared"

            .Col = ColPostInAcct
            .Text = "No. of MRR Posted"

            .Col = ColPendingInStore
            .Text = "Pending MRR In Store"

            .Col = ColPendingInQC
            .Text = "Pending MRR In Q.C."

            .Col = ColPendingInAcct
            .Text = "Pending MRR In Account"
        End With
    End Sub
    Private Sub frmMRRDayBook_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
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
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
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
        Call ReportForPendingGR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForPendingGR(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If TxtAccount.Text = "" Then Exit Sub
        End If

        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        Call InsertPrintDummy()

        '''''Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "MRR - Status"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & IIf(Trim(TxtAccount.Text) = "", "", " (" & TxtAccount.Text & ")")
        End If

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mReportFileName = "MRRDayBook.Rpt"

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

    Private Sub InsertPrintDummy()


        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mRefType As String
        Dim mCol1 As String
        Dim mCol2 As String
        Dim mCol3 As String
        Dim mCol4 As String
        Dim mCol5 As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColRefType
                mRefType = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColTOTMRR
                mCol1 = .Text

                .Col = ColPostInAcct
                mCol2 = .Text

                .Col = ColPendingInStore
                mCol3 = .Text

                .Col = ColPendingInQC
                mCol4 = .Text

                .Col = ColPendingInAcct
                mCol5 = .Text


                SqlStr = "Insert into TEMP_PrintDummyData ( " & vbCrLf & " UserID,SubRow,Field1,Field2,Field3,Field4, " & vbCrLf & " Field5,Field6 " & vbCrLf & " ) Values ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mRefType & "', " & vbCrLf & " '" & mCol1 & "', " & vbCrLf & " '" & mCol2 & "', " & vbCrLf & " '" & mCol3 & "', " & vbCrLf & " '" & mCol4 & "', " & vbCrLf & " '" & mCol5 & "')"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub

    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData" & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SubRow"

        FetchRecordForReport = mSqlStr

    End Function


    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
End Class
