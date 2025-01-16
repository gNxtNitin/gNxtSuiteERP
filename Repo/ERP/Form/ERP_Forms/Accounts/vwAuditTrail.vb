Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmVoucherAuditTrail
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection

    Dim mAccountCode As String
    Private Const ColTransType As Short = 1
    Private Const ColTransUser As Short = 2
    Private Const ColBookType As Short = 3
    Private Const ColBookSubType As Short = 4
    Private Const ColTransDate As Short = 5
    Private Const ColVDate As Short = 6
    Private Const ColVNo As Short = 7
    Private Const ColParticulars As Short = 8
    Private Const ColDAmount As Short = 9
    Private Const ColCAmount As Short = 10
    Private Const ColNarration As Short = 11
    Private Const ColBillDetail As Short = 12
    Private Const ColChequeNo As Short = 13

    Private Const ColDept As Short = 14
    Private Const ColEmp As Short = 15
    Private Const ColCostC As Short = 16
    Private Const ColMKEY As Short = 17
    Private Const ColSubRowNo As Short = 18
    Private Const ColADDUser As Short = 19
    Private Const ColADDDate As Short = 20
    Private Const ColMODUser As Short = 21
    Private Const ColMODDate As Short = 22
    Private Const ColBranch As Short = 23


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
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
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroup.CheckStateChanged
        Dim Index As Short = chkGroup.GetIndex(eventSender)
        Call PrintStatus(False)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdOptional_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOptional.Click
        FraOthers.Visible = Not FraOthers.Visible
        FraOthers.BringToFront()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call ReportForLedger(Crystal.DestinationConstants.crptToWindow)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mReportFileName As String

        If MainClass.FillPrintDummyDataFromSprd(SprdLedg, 1, SprdLedg.MaxRows, 1, SprdLedg.MaxCols, PubDBCn) = False Then GoTo ERR1
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Voucher Check List"
        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & " - " & Trim(TxtAccount.Text)
        End If

        If optDate(0).Checked = True Then
            mSubTitle = " Voucher Date : "
        Else
            mSubTitle = " Transaction Date : "
        End If

        mSubTitle = mSubTitle & "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        If cboShow.SelectedIndex > 0 Then
            mSubTitle = mSubTitle & "(" & cboShow.Text & ")"
        End If

        mReportFileName = "VoucherCheckList.Rpt"
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


        Dim mVdate As String
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

        On Error GoTo ERR1

        pDBCn.Errors.Clear()

        pDBCn.BeginTrans()

        mPartyName = TxtAccount.Text
        '    mOpening = MainClass.FormatRupees(CDbl(lblDOpBal) - CDbl(lblCOpBal))
        '
        '    mClosing = CDbl(lblDClBal) - CDbl(lblCClBal)
        '    mClosing = MainClass.FormatRupees(Abs(mClosing)) & IIf(mClosing >= 0, "Dr", "Cr")
        '
        '    mRunningBal = mOpening
        SqlStr = ""
        With SprdLedg
            '        SqlStr = "Insert into PrintDummyData (UserID,SubRow,Field1,Field8,Field5,Field6,Field9) Values (" & vbCrLf _
            ''                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf _
            ''                & " '" & MainClass.AllowSingleQuote(Trim(mPartyName)) & "', " & vbCrLf _
            ''                & " 'Opening Balance :', " & vbCrLf _
            ''                & " CASE WHEN " & mOpening & " >= 0 THEN '" & Trim(mOpening) & "' ELSE '0' END," & vbCrLf _
            ''                & " CASE WHEN " & mOpening & " < 0 THEN '" & Abs(Trim(mOpening)) & "' ELSE '0' END,'')"
            '
            '        pDBCn.Execute SqlStr

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColVDate
                mVdate = .Text

                .Col = ColVNo
                mVNo = .Text

                '            .Col = ColAcctName
                '            If frmPrintLedg.chkPrintOption(3).Value = vbChecked Then
                '                mAcctName = Left(.Text, 250)
                '            Else
                '                mAcctName = ""
                '            End If

                .Col = ColNarration
                If frmPrintLedg.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mNarration = VB.Left(.Text, IIf(mDOSPRINTING = True, 250, 250))
                Else
                    mNarration = ""
                End If

                .Col = ColChequeNo
                If frmPrintLedg.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mChequeNo = IIf(Trim(.Text) = "", "", "Chq. No. & Date : ") & .Text
                Else
                    mChequeNo = ""
                End If

                .Col = ColDAmount
                If IsNumeric(.Text) Then
                    mDAmt = CDbl(.Text)
                Else
                    mDAmt = 0
                End If
                .Col = ColCAmount
                If IsNumeric(.Text) Then
                    mCAmt = CDbl(.Text)
                Else
                    mCAmt = 0
                End If
                mRunningBal = mRunningBal + (mDAmt - mCAmt)
                mRunningBalTot = MainClass.FormatRupees(System.Math.Abs(mRunningBal)) & IIf(mRunningBal >= 0, "Dr", "Cr")

                .Col = ColCostC
                mCostC = Trim(.Text)

                .Col = ColBillDetail
                If frmPrintLedg.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mBillDetail = VB.Left(.Text, IIf(mDOSPRINTING = True, 250, 250))
                Else
                    mBillDetail = ""
                End If

                SqlStr = "Insert into Temp_Ledger ( " & vbCrLf & " UserID,SubRow,PARTYNAME,VDATE, " & vbCrLf & " VNO,NARRATION,DAmount,CAmount, " & vbCrLf & " CHQNO,ACCOUNTNAME,COSTCNAME,BILLDETAIL " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mPartyName)) & "', " & vbCrLf & " '" & Trim(mVdate) & "', " & vbCrLf & " '" & Trim(mVNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mNarration)) & "', " & vbCrLf & " " & Val(CStr(mDAmt)) & ", " & vbCrLf & " " & Val(CStr(mCAmt)) & ",'" & mChequeNo & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mCostC)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mBillDetail)) & "')"

                pDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        pDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume
        pDBCn.RollbackTrans()
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

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ERR1

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts(TxtAccount)
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        If LedgInfo() = False Then GoTo ErrPart
        '    SprdLedg.SetFocus
        Call PrintStatus(True)
        '    FraOthers.Visible = False
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Public Sub frmVoucherAuditTrail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim I As Integer
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        TxtAccount.Visible = True
        FraAccount.Text = "Accounts"


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmVoucherAuditTrail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        FraOthers.Visible = False


        ' Move the rows or columns with the scroll box
        SprdLedg.ScrollBarTrack = FPSpreadADO.ScrollBarTrackConstants.ScrollBarTrackBoth
        ' Show the scroll tips
        SprdLedg.ShowScrollTips = FPSpreadADO.ShowScrollTipsConstants.ShowScrollTipsBoth


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()

        On Error GoTo ErrPart
        'MainClass.FillCombo CboCC, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""  ', "BranchCode=" & RsCompany.fields("CBRANCHCODE & ""
        MainClass.FillCombo(CboCC, "FIN_CCENTER_HDR", "CC_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(CboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboEmp, "PAY_EMPLOYEE_MST", "EMP_NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboExpHead, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        CboDept.SelectedIndex = 0
        cboEmp.SelectedIndex = 0
        CboCC.SelectedIndex = 0
        cboExpHead.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("ADD ONLY")
        cboShow.Items.Add("MODIFY ONLY")
        cboShow.SelectedIndex = 0
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmVoucherAuditTrail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdLedg.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdLedg, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmVoucherAuditTrail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        FormActive = False
        'MainClass.AssignDataInSprd("", AData1, "", "N")
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
    End Sub
    Private Sub SprdLedg_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdLedg.DblClick
        'Dim xVDate As String
        'Dim xMKey As String
        'Dim xVNo As String
        'Dim xBookType As String
        'Dim xBookSubType As String
        'Dim pIndex As Integer
        'Dim xVTYPE As String

        'If SprdLedg.ActiveRow < 0 Then Exit Sub


        'SprdLedg.Row = SprdLedg.ActiveRow

        'SprdLedg.Col = ColVDate
        'xVDate = Me.SprdLedg.Text

        'SprdLedg.Col = ColMKEY
        'xMKey = Me.SprdLedg.Text

        'If xMKey = "-1" Then
        '    Exit Sub
        'End If
        'SprdLedg.Col = ColVNo
        'xVNo = Me.SprdLedg.Text

        'SprdLedg.Col = ColBookType
        'xBookType = Me.SprdLedg.Text

        'SprdLedg.Col = ColBookSubType
        'xBookSubType = Me.SprdLedg.Text

        'If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Then
        '    xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
        '    xVNo = VB.Right(xVNo, 5)
        'ElseIf xBookType = "R" Or xBookType = "E" Then
        '    If RsCompany.Fields("FYEAR").Value >= 2020 Then
        '        xVTYPE = Mid(xVNo, 1, Len(xVNo) - 8)
        '        xVNo = VB.Right(xVNo, 8)
        '    Else
        '        xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
        '        xVNo = VB.Right(xVNo, 5)
        '    End If
        'End If

        'Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)

    End Sub
    Private Sub ViewAccountLedger(ByRef xIndex As Integer, ByRef pDateFrom As String, ByRef pDateTo As String)
        Dim ss As New frmViewLedger
        Dim mFromDate As String
        Dim mToDate As String

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConLedger Then
            ss.MdiParent = Me.MdiParent
            ss.lblBookType.Text = "LEDG"
            ss.cboAccount.Text = TxtAccount.Text
            ss.lblAcCode.Text = lblAcCode.Text

            SprdLedg.Row = SprdLedg.ActiveRow
            SprdLedg.Col = ColVDate

            If GetMonthStartEndDate((SprdLedg.Text), mFromDate, mToDate) = True Then
                ss.txtDateFrom.Text = VB6.Format(mFromDate, "dd/mm/yyyy")
                ss.txtDateTo.Text = VB6.Format(mToDate, "dd/mm/yyyy")
            Else
                ss.txtDateFrom.Text = VB6.Format(pDateFrom, "dd/mm/yyyy")
                ss.txtDateTo.Text = VB6.Format(pDateTo, "dd/mm/yyyy")
            End If


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
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            SprdLedg_DblClick(SprdLedg, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        Call SearchAccounts(TxtAccount)
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
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    If UCase(mTextBox.Name) <> UCase("TxtAgtAccount") Then
        '        Select Case lblBookType.text
        '            Case ConLedger
        '                SqlStr = SqlStr
        '            Case ConCashBook
        '                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
        '            Case ConBankBook, ConPDCBook
        '                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
        '            Case Else
        '                SqlStr = " AND 1=2"
        '        End Select
        '    End If

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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccounts(TxtAccount)
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '    Select Case lblBookType.text
        '        Case ConLedger
        '            SqlStr = ""
        '        Case ConCashBook
        '            SqlStr = "SUPP_CUST_TYPE = '1'"
        '        Case ConBankBook, ConPDCBook
        '            SqlStr = "SUPP_CUST_TYPE = '2'"
        '        Case Else
        '            SqlStr = "1=2"
        '    End Select

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdLedg(ByRef Arow As Integer)

        With SprdLedg
            .MaxCols = ColBranch
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            '        .RowsFrozen = 0
            .Row = -1


            .Col = ColTransType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTransType, 8)
            .ColHidden = False

            .Col = ColTransUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTransUser, 8)
            .ColHidden = False

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 15)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 15)
            .ColHidden = False


            .Col = ColTransDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTransDate, 14)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 9)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 10)
            .ColHidden = False
            .ColsFrozen = ColVNo

            .Col = ColParticulars
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColParticulars, 25)
            .ColHidden = False

            .Col = ColDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDAmount, 12)

            .Col = ColCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCAmount, 12)


            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 25)
            .ColHidden = False


            .Col = ColBillDetail
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDetail, 15)
            .ColHidden = False

            .Col = ColChequeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChequeNo, 8)
            .ColHidden = False

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 8)
            .ColHidden = True

            .Col = ColEmp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmp, 8)
            .ColHidden = True

            .Col = ColCostC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCostC, 8)
            .ColHidden = True

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColADDUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColADDUser, 7)
            .ColHidden = True

            .Col = ColADDDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColADDDate, 8)
            .ColHidden = True

            .Col = ColMODUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMODUser, 7)
            .ColHidden = True

            .Col = ColMODDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMODDate, 8)
            .ColHidden = True

            .Col = ColSubRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSubRowNo, 5)
            .ColHidden = True

            .Col = ColBranch
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBranch, 15)
            .ColHidden = True ''IIf(Left(cboConsolidated.Text, 1) = "D", True, False)

            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, ColBranch)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            'Show the grid lines over the color
            '        SprdLedg.BackColorStyle = BackColorStyleOverVertGridOnly

            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function LedgInfo() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mAccountCode2 As String

        LedgInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr1 = MakeSQL()
        SqlStr2 = MakeSQLCond()

        SqlStr = SqlStr1 & vbCrLf & SqlStr2

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY " & vbCrLf _
            & " TRN.ADDDATE ," & vbCrLf _
            & " TRN.VDATE , TRN.VDATE," & vbCrLf _
            & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf _
            & " TRN.VNO ,  " & vbCrLf _
            & " ACM.SUPP_CUST_NAME ," & vbCrLf _
            & " TRN.NARRATION, TRN.REMARKS ," & vbCrLf _
            & " TRN.MKEY," & vbCrLf _
            & " CHEQUENO || ' ' || CHQDATE, BOOKCODE,TRN.ADDUSER,TRN.ADDDATE,TRN.MODUSER,TRN.MODDATE, TRANS_TYPE, CASE WHEN TRANS_TYPE='A' THEN TRN.ADDDATE ELSE TRN.MODDATE END,TRN.TRANS_DATE " & vbCrLf _
            & " ORDER BY " & vbCrLf _
            & " TRN.VDATE , " & vbCrLf _
            & " TRN.VNO, TRANS_TYPE,TRN.TRANS_DATE,MAX(SUBROWNO) "



        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        '''********************************

        FormatSprdLedg(-1)
        LedgInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        LedgInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = " SELECT DECODE(TRANS_TYPE,'A','ADD',DECODE(TRANS_TYPE,'M','MODIFY','DELETE')) AS TRANS_TYPE, "


        SqlStr = SqlStr & " CASE WHEN TRANS_TYPE='A' THEN TRN.ADDUSER ELSE TRN.MODUSER END TRANS_USER,  "


        SqlStr = SqlStr & vbCrLf _
            & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE," ''''& vbCrLf |            & " TO_CHAR(TRN.ADDDATE,'DD/MM/YYYY'),  "



        SqlStr = SqlStr & " TO_CHAR(TRN.TRANS_DATE,'DD/MM/YYYY HH24:MI') AS TRANS_DATE,  "
        '

        SqlStr = SqlStr & vbCrLf & " TRN.VDATE, " & vbCrLf _
            & " TRN.VNO, " & vbCrLf _
            & " ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf _
            & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, " & vbCrLf _
            & " TRN.NARRATION, " & vbCrLf _
            & " TRN.REMARKS, " & vbCrLf _
            & " CHEQUENO || ' ' || CHQDATE AS CHEQUENO, " & vbCrLf _
            & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE, " & vbCrLf _
            & " TRN.MKEY ," & vbCrLf _
            & " '',TRN.ADDUSER,TRN.ADDDATE,TRN.MODUSER,TRN.MODDATE,'',''"

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function MakeSQLCond() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpHeadCode As String
        Dim mConsolidated As String
        Dim mGroupOption As String

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

        SqlStr = " FROM FIN_POSTED_HIS_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  "

        SqlStr = SqlStr & vbCrLf _
            & " TRN.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        If Trim(txtVNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.VNO='" & Trim(txtVNo.Text) & "'"
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

        mGroupOption = GetGroupOption()
        '    If mIsOpening = True Then
        '        mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        '    End If
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        End If

        If lblBookType.Text = ConLedger Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " "
        End If

        If optDate(0).Checked = True Then
            SqlStr = SqlStr & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & " AND TRN.ADDDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & " AND TRN.MODDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        Else
            If cboShow.SelectedIndex = 0 Then
                SqlStr = SqlStr & " AND ((TRN.ADDDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " OR (TRN.MODDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
            ElseIf cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & " AND TRN.ADDDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & " AND TRN.MODDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
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
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConSaleBook & "'"
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

        If chkGroup(9).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConSaleDebitBook & "'"
        Else
            mAllCheck = False
        End If

        If chkGroup(10).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BOOKTYPE = '" & ConSaleCreditBook & "'"
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
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If lblBookType.text = ConLedger Then
        '        If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        '    End If
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Select  Account Name.")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please valid Account Name.")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If lblBookType.text = ConLedger Then
        '        If FYChk(CDate(txtDateTo.Text)) = False Then
        '            txtDateTo.SetFocus
        '            Cancel = True
        '            Exit Sub
        '        End If
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdLedg_ButtonClicked(sender As Object, e As _DSpreadEvents_ButtonClickedEvent) Handles SprdLedg.ButtonClicked
        Dim mMkey As String
        Dim mBookType As String = ""
        SprdLedg.Row = e.row
        SprdLedg.Col = ColMKEY
        mMkey = SprdLedg.Text

        SprdLedg.Col = ColBookType
        mBookType = SprdLedg.Text

    End Sub
End Class
