Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPendingMRR
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColGRDate As Short = 1
    Private Const ColGRNo As Short = 2
    Private Const ColRefType As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColBillDate As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColSendDate As Short = 8
    Private Const ColLoc As Short = 9
    Private Const ColQCEMP As Short = 10

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
        Else
            TxtAccount.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub

    Private Sub chkRefType_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRefType.CheckStateChanged
        Dim Index As Short = chkRefType.GetIndex(eventSender)
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE IN ('S','C'))")
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

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)

        ShowPendingGR()

        ''    SprdMain.MaxRows = SprdMain.MaxRows + 2
        ''    SprdMain.Row = SprdMain.MaxRows
        ''    SprdMain.Col = ColPartyName
        ''    SprdMain.Text = "TOTAL :"
        ''    SprdMain.FontBold = True

        ''Call CalcRowTotal(SprdMain, ColAmount, 1, ColAmount, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColAmount)

        Dim mcntRow As Integer
        Dim mGTotal As Double

        With SprdMain
            For mcntRow = 1 To .MaxRows
                .Row = mcntRow
                .Col = ColAmount
                mGTotal = mGTotal + Val(.Text)
            Next
        End With

        LblTotalAmt.Text = VB6.Format(mGTotal, "##,###,###,##0.00")
        FormatSprdMain()
        Call PrintStatus(True)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then ''System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
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
    Public Sub frmPendingMRR_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmPendingMRR_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

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
    Private Sub ShowPendingGR()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mRefType As String
        Dim mRefTypeStr As String
        Dim mDivisionCode As Double

        mRefType = ""
        If chkRefType(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = "'C'"
        End If
        If chkRefType(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'D'"
        End If
        If chkRefType(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'F'"
        End If
        If chkRefType(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'J'"
        End If
        If chkRefType(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'P'"
        End If
        If chkRefType(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'R'"
        End If
        If chkRefType(6).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'I'"
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'3'"
        End If

        If chkRefType(7).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'2'"
        End If

        If chkRefType(8).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'1'"
        End If

        mRefTypeStr = "AND REF_TYPE IN ( " & mRefType & ")"

        SqlStr = "SELECT GRMain.MRR_DATE, GRMain.AUTO_KEY_MRR, " & vbCrLf & " CASE WHEN REF_TYPE='C' THEN 'CASH' " & vbCrLf & " WHEN REF_TYPE='D' THEN 'DS' " & vbCrLf & " WHEN REF_TYPE='F' THEN 'FOC' " & vbCrLf & " WHEN REF_TYPE='J' THEN 'JOBWORK' " & vbCrLf & " WHEN REF_TYPE='P' THEN 'PO' " & vbCrLf & " WHEN REF_TYPE='R' THEN 'RGP' " & vbCrLf & " WHEN REF_TYPE='I' OR REF_TYPE='3' THEN 'SR' WHEN REF_TYPE='1' THEN 'J/W REJ' WHEN REF_TYPE='2' THEN 'SR-W'END AS REFTYPE, " & vbCrLf & " ACM.SUPP_CUST_NAME,GRMain.BILL_NO,GRMain.BILL_DATE, " & vbCrLf & " TO_CHAR(INVOICE_AMT,'999999999.99') AS ITEMVALUE,GRMain.SEND_AC_DATE, " & vbCrLf & " CASE WHEN GRMain.SEND_AC_FLAG='Y' THEN 'A'" & vbCrLf & " WHEN GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='Y' THEN 'S' " & vbCrLf & " WHEN GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='N' THEN 'Q' END AS LOC"

        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR GRMain,FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE GRMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND GRMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND GRMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND GRMain.MRR_FINAL_FLAG='N' And MRR_STATUS='N'"

        If optPending(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_AC_FLAG='Y'"
        ElseIf optPending(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='Y' "
        ElseIf optPending(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='N'"
        End If

        If mRefType <> "" Then
            SqlStr = SqlStr & vbCrLf & mRefTypeStr
        End If


        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND GRMain.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY GRMain.MRR_DATE,GRMain.AUTO_KEY_MRR"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColQCEMP
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColGRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColGRDate, 8)

            .Col = ColGRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColGRNo, 9)

            .Col = ColRefType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRefType, 8)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 24)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)


            .Col = ColAmount
            .set_ColWidth(ColAmount, 8)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColSendDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSendDate, 8)

            .Col = ColLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLoc, 4)

            .Col = ColQCEMP
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColQCEMP, 15)
            .ColHidden = True

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
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

            .Col = ColGRDate
            .Text = "MRR Date"

            .Col = ColGRNo
            .Text = "MRR No."

            .Col = ColRefType
            .Text = "Ref. Type."

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColAmount
            .Text = "Item Value"

            .Col = ColSendDate
            .Text = "Send Date"

            .Col = ColQCEMP
            .Text = "QC Employee"

            .Col = ColLoc
            .Text = "Location"

        End With
    End Sub
    Private Sub frmPendingMRR_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub optPending_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPending.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPending.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
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
        Dim SqlStr As String = ""
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
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Pending MRR"


        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        If optPending(1).Checked = True Then
            mTitle = mTitle & " (Accounts)"
        ElseIf optPending(2).Checked = True Then
            mTitle = mTitle & " (Store)"
        ElseIf optPending(3).Checked = True Then
            mTitle = mTitle & " (QC)"
        End If

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mReportFileName = "PendingMRR.Rpt"

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
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        Dim mRefType As String
        Dim mRefTypeStr As String

        mRefType = ""
        If chkRefType(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = "'C'"
        End If
        If chkRefType(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'D'"
        End If
        If chkRefType(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'F'"
        End If
        If chkRefType(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'J','1'"
        End If
        If chkRefType(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'P'"
        End If
        If chkRefType(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'R'"
        End If
        If chkRefType(6).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRefType = IIf(mRefType = "", "", mRefType & ",") & "'I'"
        End If

        mRefTypeStr = "AND REF_TYPE IN ( " & mRefType & ")"

        mSqlStr = "SELECT GRMain.MRR_DATE, GRMain.AUTO_KEY_MRR,REF_TYPE, ACM.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(INVOICE_AMT,'999999999.99') AS ITEMVALUE " & vbCrLf & " FROM INV_GATE_HDR GRMain,FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE GRMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND GRMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND GRMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND GRMain.MRR_FINAL_FLAG='N'"

        If optPending(1).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND GRMain.SEND_AC_FLAG='Y'"
        ElseIf optPending(2).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND GRMain.SEND_AC_FLAG='N' AND GRMain.QC_STATUS='Y' "
        ElseIf optPending(3).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND GRMain.QC_STATUS='N'"
        End If

        If mRefType <> "" Then
            mSqlStr = mSqlStr & vbCrLf & mRefTypeStr
        End If

        If txtDateFrom.Text <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND GRMain.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND GRMain.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY GRMain.MRR_DATE,GRMain.AUTO_KEY_MRR"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
