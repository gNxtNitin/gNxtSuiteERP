Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAgeingAnlyBreakup
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 12
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColBill As Short = 3
    Private Const ColDate As Short = 4
    Private Const ColBillAmount As Short = 5
    Private Const ColBal As Short = 6
    Private Const ColDue As Short = 7
    Private Const ColUnDue As Short = 8
    Private Const ColDays1 As Short = 9
    Private Const ColDays2 As Short = 10
    Private Const ColDays3 As Short = 11
    Private Const ColDays4 As Short = 12
    Private Const ColDays5 As Short = 13
    Private Const ColDays6 As Short = 14
    Private Const ColDays7 As Short = 15
    Private Const ColDays8 As Short = 16
    Private Const ColDays9 As Short = 17
    Private Const ColDays10 As Short = 18
    Private Const ColDrCr As Short = 19
    Private Const ColPayTerms As Short = 20
    Private Const ColDueDays As Short = 21
    Private Const ColLenderBank As Short = 22
    Private Const ColSalePerson As Short = 23
    Private Const ColCompanyName As Short = 24
    'Private Const ColCompanyName As Short = 23
    Private Const ColPaymentMode As Short = 25
    Private Const ColCategory As Short = 26
    Private Const ColDel As Short = 27
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer
    Dim mDays1 As Integer
    Dim mDays2 As Integer
    Dim mDays3 As Integer
    Dim mDays4 As Integer
    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean

    Dim mClickProcess As Boolean

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub chkHideZero_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHideZero.CheckStateChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then
        '        PvtDBCn.Close
        '        Set PvtDBCn = Nothing
        '    End If
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"

        If MainClass.SearchGridMaster((TxtAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                TxtAccount.Text = AcName
            End If
        End If

        'MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", SqlStr)
        'If AcName <> "" Then
        '    TxtAccount.Text = AcName
        'End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForAgeingAnly(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        PrintFlag = False
        PrintStatus()
        MainClass.ClearGrid(SprdAgeing, RowHeight)
        If FieldsVerification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTemp() = False Then Exit Sub

        If OptSumDet(0).Checked Then
            AgeingInfo(("D"))
            DisplayTotal()
        Else
            AgeingInfo(("S"))
            DisplaySummTotal()
        End If

        FormatSprdAgeing()
        FillHeading()

        SprdAgeing.DataSource = Nothing

        SprdAgeing.Focus()
        PrintFlag = True
        PrintStatus()
        MainClass.SetFocusToCell(SprdAgeing, mActiveRow, 4)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Function FieldsVerification() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsACM As ADODB.Recordset
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If MainClass.ChkIsdateF(txtPDCDate) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Exit Function
        '    End If
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '        If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '            mAccountCode = MasterNo
            '        Else
            '            TxtAccount.SetFocus
            '            MsgInformation "Please Select Account"
            '            Exit Function
            '        End If
            SqlStr = " SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'" & vbCrLf & "AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
            SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)
            If RsACM.EOF Then
                TxtAccount.Focus()
                MsgInformation("No Such Account in Account Master")
                Exit Function
            End If
        End If
        If Val(txtDays1.Text) >= Val(txtDays2.Text) Then
            MsgInformation("Day2 Must be Greater than Day1.")
            txtDays2.Focus()
            Exit Function
        End If
        If Val(txtDays2.Text) >= Val(txtDays3.Text) Then
            MsgInformation("Day3 Must be Greater than Day2.")
            txtDays3.Focus()
            Exit Function
        End If
        If Val(txtDays3.Text) >= Val(txtDays4.Text) Then
            MsgInformation("Day4 Must be Greater than Day3.")
            txtDays4.Focus()
            Exit Function
        End If
        If Val(txtDays4.Text) >= Val(txtDays5.Text) Then
            MsgInformation("Day5 Must be Greater than Day4.")
            txtDays5.Focus()
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub frmAgeingAnlyBreakup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        TxtGroup.Enabled = IIf(chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        chkAllPerson.CheckState = System.Windows.Forms.CheckState.Checked
        txtSalePerson.Enabled = False

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
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

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
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
    Private Sub SearchGroup()
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_Category='G'"

        If MainClass.SearchGridMaster((TxtGroup.Text), "FIN_GROUP_MST", "GROUP_NAME", "GROUP_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                TxtGroup.Text = AcName
            End If
        End If

        'If MainClass.SearchMaster((TxtGroup.Text), "FIN_GROUP_MST", "GROUP_Name", SqlStr) = True Then
        '    TxtGroup.Text = AcName
        '    TxtGroup.Focus()
        'End If
    End Sub
    Private Sub ChkAllGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllGroup.CheckStateChanged
        TxtGroup.Enabled = IIf(chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub frmAgeingAnlyBreakup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Long

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

        PrintFlag = False
        txtDateTo.Text = CStr(RunDate)
        txtPDCDate.Text = CStr(RunDate)
        chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked
        txtDays1.Text = CStr(0)
        txtDays2.Text = CStr(15)
        txtDays3.Text = CStr(30)
        txtDays4.Text = CStr(45)
        txtDays5.Text = CStr(60)
        txtDays6.Text = CStr(75)
        txtDays7.Text = CStr(90)
        txtDays8.Text = CStr(105)
        txtDays9.Text = CStr(120)
        txtDays10.Text = CStr(135)

        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboDivision.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.SelectedIndex = 0

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


        '    mDays1 = Val(txtDays1.Text)
        '    mDays2 = Val(txtDays2.Text)
        '

        chkAllPerson.CheckState = System.Windows.Forms.CheckState.Checked
        txtSalePerson.Enabled = False

        FormatSprdAgeing()
        FillHeading()
        chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        PrintStatus()
        Call frmAgeingAnlyBreakup_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
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
    Private Sub AgeingInfo(ByRef mType As String)

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mDays1 As Integer
        Dim mDays2 As Integer
        Dim mDays3 As Integer
        Dim mDays4 As Integer
        Dim mDays5 As Integer
        Dim mDays6 As Integer
        Dim mDays7 As Integer
        Dim mDays8 As Integer
        Dim mDays9 As Integer
        Dim mDays10 As Integer
        Dim mBillDate As String = ""
        Dim mDueDate As String = ""
        Dim mCreditField As String = ""
        mDays1 = Val(txtDays1.Text)
        mDays2 = Val(txtDays2.Text)
        mDays3 = Val(txtDays3.Text)
        mDays4 = Val(txtDays4.Text)
        mDays5 = Val(txtDays5.Text)
        mDays6 = Val(txtDays6.Text)
        mDays7 = Val(txtDays7.Text)
        mDays8 = Val(txtDays8.Text)
        mDays9 = Val(txtDays9.Text)
        mDays10 = Val(txtDays10.Text)

        mCreditField = "CREDIT_DAYS_FROM"

        If OptDueDate(0).Checked = True Then
            mBillDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- EXPDATE"
            mDueDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- EXPDATE"
        ElseIf OptDueDate(1).Checked = True Or OptDueDate(5).Checked = True Then
            mBillDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- BillDate"
            'mDueDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- BillDate"
            mDueDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- (BillDate + " & mCreditField & ")"
        ElseIf OptDueDate(2).Checked = True Then
            mBillDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- (BillDate + " & mCreditField & ")" ''GETPARTYPAYTERMSDAYS(" & RsCompany.fields("COMPANY_CODE").value & ",ACCOUNTCODE,BILLNO,BILLDATE,'P'))"
            mDueDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- (BillDate + " & mCreditField & ")" ''GETPARTYPAYTERMSDAYS(" & RsCompany.fields("COMPANY_CODE").value & ",ACCOUNTCODE,BILLNO,BILLDATE,'P'))"
        ElseIf OptDueDate(3).Checked = True Then
            mBillDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- (BillDate + " & mCreditField & ")"
            mDueDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- (BillDate + " & mCreditField & ")"
        ElseIf OptDueDate(4).Checked = True Then
            mBillDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- EXPDATE "
            mDueDate = "TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')- (EXPDATE + " & mCreditField & ")" ''GETPARTYPAYTERMSDAYS(" & RsCompany.fields("COMPANY_CODE").value & ",ACCOUNTCODE,BILLNO,BILLDATE,'P'))"
        End If

        If mType = "D" Then
            SqlStr = "SELECT ACCOUNTCODE, PARTYNAME, BillNo, BillDate, SUM(BILLAMOUNT), " & vbCrLf _
                & " SUM(BAL_AMOUNT), SUM(DUE),SUM(UNDUE), SUM(DAYS1), SUM(DAYS2), SUM(DAYS3), SUM(DAYS4), SUM(DAYS5), SUM(DAYS6)," & vbCrLf _
                & " SUM(DAYS7), SUM(DAYS8), SUM(DAYS9), SUM(DAYS10), CASE WHEN SUM(BAL_AMOUNT)>=0 THEN 'DR' ELSE 'CR' END As DRCR, " & vbCrLf _
                & " MAX(CREDIT_DESC), MAX(DUEDAYS), LENDER_BANK, RESPONSIBLE_PERSON, UNIT_NAME, PAYMENT_MODE_DESC, SUPP_CUST_NATURE"
        Else
            SqlStr = "SELECT ACCOUNTCODE, PARTYNAME, '' BillNo, '' BillDate, SUM(BILLAMOUNT), " & vbCrLf _
                & " SUM(BAL_AMOUNT),SUM(DUE), SUM(UNDUE), SUM(DAYS1), SUM(DAYS2), SUM(DAYS3), SUM(DAYS4), SUM(DAYS5), SUM(DAYS6)," & vbCrLf _
                & " SUM(DAYS7), SUM(DAYS8), SUM(DAYS9), SUM(DAYS10), CASE WHEN SUM(BAL_AMOUNT)>=0 THEN 'DR' ELSE 'CR' END As DRCR, " & vbCrLf _
                & " MAX(CREDIT_DESC), '' DUEDAYS, LENDER_BANK, RESPONSIBLE_PERSON,  "

            If OptSumDet(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " UNIT_NAME,"
            Else
                SqlStr = SqlStr & vbCrLf & " '' UNIT_NAME,"
            End If

            SqlStr = SqlStr & vbCrLf & " PAYMENT_MODE_DESC, SUPP_CUST_NATURE"

        End If

        '' SqlStr = SqlStr & vbCrLf & " Case When SUM(BALANCE*DECODE(DC,'DR',1,-1))>=0 THEN 'DR' ELSE 'CR' END AS DRCR, "

        SqlStr = SqlStr & vbCrLf & " FROM ("

        SqlStr = SqlStr & vbCrLf & "SELECT ACCOUNTCODE AS ACCOUNTCODE, PARTYNAME, "

        'If mType = "D" Then
        SqlStr = SqlStr & vbCrLf & " BillNo AS BillNo, BillDate AS BillDate ," ''
        'Else
        '    SqlStr = SqlStr & vbCrLf & " '' AS BillNo, '' AS BillDate ,"
        'End If

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(BILLAMOUNT*DECODE(DC,'DR',1,-1)))) AS BILLAMOUNT, " & vbCrLf _
            & " TO_CHAR((SUM(BALANCE*DECODE(DC,'DR',1,-1)))) AS BAL_AMOUNT,"

        If OptDueDate(4).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & " > 0 THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DUE, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & " < 0 THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS UNDUE, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">=" & mDays1 & " AND " & mBillDate & " <=" & mDays2 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS1, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays2 & " AND " & mBillDate & " <=" & mDays3 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS2, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays3 & " AND " & mBillDate & " <=" & mDays4 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS3, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays4 & " AND " & mBillDate & " <=" & mDays5 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS4, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays5 & " AND " & mBillDate & " <=" & mDays6 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS5, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays6 & " AND " & mBillDate & " <=" & mDays7 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS6, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays7 & " AND " & mBillDate & " <=" & mDays8 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS7, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays8 & " AND " & mBillDate & " <=" & mDays9 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS8, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & ">" & mDays9 & " AND " & mBillDate & " <=" & mDays10 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS9, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " >" & mDays10 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS10, "

        ElseIf OptDueDate(5).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & " > 0 THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DUE, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & " < 0 THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS UNDUE, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays2 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS1, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays3 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS2, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays4 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS3, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays5 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS4, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays6 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS5, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays7 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS6, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays8 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS7, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays9 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS8, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & ">=0 AND " & mBillDate & " <=" & mDays10 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS9, " & vbCrLf _
                & " TO_CHAR((SUM(BALANCE*DECODE(DC,'DR',1,-1)))) AS DAYS10, "

        Else
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & " > 0 THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DUE, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mDueDate & " < 0 THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS UNDUE, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">=" & mDays1 & " AND " & mBillDate & " <=" & mDays2 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS1, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays2 & " AND " & mBillDate & " <=" & mDays3 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS2, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays3 & " AND " & mBillDate & " <=" & mDays4 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS3, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays4 & " AND " & mBillDate & " <=" & mDays5 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS4, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays5 & " AND " & mBillDate & " <=" & mDays6 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS5, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays6 & " AND " & mBillDate & " <=" & mDays7 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS6, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays7 & " AND " & mBillDate & " <=" & mDays8 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS7, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays8 & " AND " & mBillDate & " <=" & mDays9 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS8, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays9 & " AND " & mBillDate & " <=" & mDays10 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS9, " & vbCrLf _
                & " TO_CHAR((SUM(CASE WHEN " & mBillDate & ">" & mDays10 & " THEN BALANCE*DECODE(DC,'DR',1,-1) ELSE 0 END))) AS DAYS10, "

        End If
        SqlStr = SqlStr & vbCrLf & " CASE WHEN SUM(BALANCE*DECODE(DC,'DR',1,-1))>=0 THEN 'DR' ELSE 'CR' END AS DRCR, "

        SqlStr = SqlStr & vbCrLf & " MAX(CREDIT_DESC) AS CREDIT_DESC,"

        If mType = "D" Then
            SqlStr = SqlStr & vbCrLf & " TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') - BillDate AS DUEDAYS,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS DUEDAYS,"
        End If

        '

        SqlStr = SqlStr & vbCrLf & " GetAccountName(COMPANY_CODE,LENDER_BANK_CODE) AS LENDER_BANK, RESPONSIBLE_PERSON,GetSaleInvoiceUnit(FYEAR,ACCOUNTCODE,BILLNO) AS UNIT_NAME, PAYMENT_MODE_DESC, SUPP_CUST_NATURE"

        SqlStr = SqlStr & vbCrLf & " FROM TEMP_FIN_PAYMENT_ADV " & vbCrLf _
            & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        If OptSuppType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        ElseIf OptSuppType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE='S'"
        ElseIf OptSuppType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE='C' "
        Else

        End If

        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtGroup.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtGroup.Text, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & " AND (GROUPCODE=" & MasterNo & " OR GROUPCODECR=" & MasterNo & ")"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND  PARTYNAME='" & MainClass.AllowSingleQuote(Trim(UCase(TxtAccount.Text))) & "'"
        End If

        If chkAllPerson.CheckState = System.Windows.Forms.CheckState.Unchecked And txtSalePerson.Text <> "" Then
            SqlStr = SqlStr & " AND RESPONSIBLE_PERSON='" & MainClass.AllowSingleQuote(txtSalePerson.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        If OptDueDate(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EXPDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & "AND BILLDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        'If mType = "S" Then
        '    SqlStr = SqlStr & vbCrLf & "AND BALANCE<>0"
        'End If

        If mType = "S" Then
            If OptShow(0).Checked = True Then
                If chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & " HAVING SUM(BALANCE*DECODE(DC,'DR',1,-1)) <> 0   "
                End If
            ElseIf OptShow(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(BALANCE*DECODE(DC,'DR',1,-1)) > 0   "
            ElseIf OptShow(2).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(BALANCE*DECODE(DC,'DR',1,-1)) < 0   "
            End If
        Else
            If chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(BALANCE*DECODE(DC,'DR',1,-1)) <> 0   "
            End If
        End If

        'If mType = "D" Then
        SqlStr = SqlStr & vbCrLf & " GROUP BY PARTYNAME,ACCOUNTCODE,GetAccountName(COMPANY_CODE,LENDER_BANK_CODE),RESPONSIBLE_PERSON, GetSaleInvoiceUnit(FYEAR,ACCOUNTCODE,BILLNO) ,PAYMENT_MODE_DESC, SUPP_CUST_NATURE,BillNo,BillDate" '',DC"           '''BillDate"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY PARTYNAME,ACCOUNTCODE,GetAccountName(COMPANY_CODE,LENDER_BANK_CODE),RESPONSIBLE_PERSON,GetSaleInvoiceUnit(FYEAR,ACCOUNTCODE,BILLNO),PAYMENT_MODE_DESC, SUPP_CUST_NATURE" '',DC"
        'End If

        If mType = "D" Then
            SqlStr = SqlStr & vbCrLf & " ) GROUP BY PARTYNAME,ACCOUNTCODE,LENDER_BANK ,RESPONSIBLE_PERSON, UNIT_NAME ,PAYMENT_MODE_DESC, SUPP_CUST_NATURE,BillNo,BillDate" '',DC"           '''BillDate"
        Else
            SqlStr = SqlStr & vbCrLf & " ) GROUP BY PARTYNAME,ACCOUNTCODE,LENDER_BANK ,RESPONSIBLE_PERSON, PAYMENT_MODE_DESC, SUPP_CUST_NATURE" '',DC"

            If OptSumDet(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " , UNIT_NAME"
            End If

        End If

        If mType = "D" Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY PARTYNAME,BillDate,BillNo "
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY PARTYNAME"
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdAgeing, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub

    Private Function InsertIntoTemp() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim mDivisionCode As Double
        Dim CntLst As Integer
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_FIN_PAYMENT_ADV NOLOGGING WHERE USER_ID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)


        mSqlStr = " INSERT INTO TEMP_FIN_PAYMENT_ADV ( " & vbCrLf _
            & " USER_ID, COMPANY_CODE, FYEAR, " & vbCrLf _
            & " BILLNO, BILLDATE, EXPDATE, " & vbCrLf _
            & " BILLAMOUNT, ADV, DNOTE, " & vbCrLf _
            & " CNOTE, TDS, PAYMENT, " & vbCrLf _
            & " BALANCE, DC, PARTYNAME, " & vbCrLf _
            & " ACCOUNTCODE, SUPP_CUST_ADDR, " & vbCrLf _
            & " SUPP_CUST_CITY, SUPP_CUST_STATE, " & vbCrLf _
            & " SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf _
            & " SUPP_CUST_TYPE, PAIDDAY, " & vbCrLf _
            & " PAIDDAY2, PAIDDAY3, PAIDDAY4,  " & vbCrLf _
            & " GROUPCODE, GROUPCODECR, CREDIT_DAYS_FROM, CREDIT_DAYS_TO,CREDIT_DESC,DIV_CODE," & vbCrLf _
            & " LENDER_BANK_CODE,RESPONSIBLE_PERSON,PAYMENT_MODE_DESC, SUPP_CUST_NATURE,COMPANY_SHORT_NAME,REGION_NAME )"

        SqlStr = " SELECT " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TRN.COMPANY_CODE,  TRN.FYEAR, " & vbCrLf _
            & " BillNo AS BILLNO,  BillDate AS BILLDATE, "


        If OptDueDate(2).Checked = True Then       ''Or OptDueDate(4).Checked = True
            SqlStr = SqlStr & vbCrLf & " NVL(EXPDATE,BILLDATE) + CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN 0 ELSE GETPARTYPAYTERMSDAYS(TRN.COMPANY_CODE,ACCOUNTCODE,BILLNO,BILLDATE,'P') END, "
        Else
            SqlStr = SqlStr & vbCrLf & " BILLDATE + CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN 0 ELSE NVL(FROM_DAYS,0) END,"
        End If


        SqlStr = SqlStr & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS BILLAMOUNT, " & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)) AS ADV, " & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS DNOTE, " & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS CNOTE, " & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS TDS, "

        If chkClearChq.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf _
                & " ABS(SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS PAYMENT, " & vbCrLf _
                & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS BALANCE,  " & vbCrLf _
                & " CASE WHEN SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount) >=0 THEn 'DR' ELSE 'CR' END AS DC, "
        Else
            SqlStr = SqlStr & vbCrLf _
                & " ABS(SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount*DECODE(CLEARDATE,NULL,DECODE(BOOKTYPE||BOOKSUBTYPE,'BP',0,1),1))) AS PAYMENT, " & vbCrLf _
                & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount*DECODE(CLEARDATE,NULL,DECODE(BOOKTYPE||BOOKSUBTYPE,'BP',0,1),1))) AS BALANCE,  " & vbCrLf _
                & " CASE WHEN SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount*DECODE(CLEARDATE,NULL,DECODE(BOOKTYPE||BOOKSUBTYPE,'BP',0,1),1)) >=0 THEN 'DR' ELSE 'CR' END AS DC, "
        End If

        If optPartyWise.Checked = True Then

            SqlStr = SqlStr & vbCrLf _
                    & " ACM.SUPP_CUST_NAME AS PARTYNAME,  TRN.ACCOUNTCODE as ACCOUNTCODE, "

        Else
            SqlStr = SqlStr & vbCrLf _
                    & " GMST.GROUP_NAME AS PARTYNAME,  GMST.GROUP_CODE as ACCOUNTCODE, "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " ACM.SUPP_CUST_ADDR,  ACM.SUPP_CUST_CITY,  ACM.SUPP_CUST_STATE, " & vbCrLf _
            & " ACM.SUPP_CUST_PIN,   ACM.SUPP_CUST_PHONE,  ACM.SUPP_CUST_TYPE, " & vbCrLf _
            & " ACM.PAIDDAY, ACM.PAIDDAY2, ACM.PAIDDAY3, ACM.PAIDDAY4, ACM.GROUPCODE,  ACM.GROUPCODECR,"

        If OptDueDate(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " GETPARTYPAYTERMSDAYS(TRN.COMPANY_CODE,ACCOUNTCODE,BILLNO,BILLDATE,'P'), 0,"
        Else
            SqlStr = SqlStr & vbCrLf & " NVL(FROM_DAYS,0),0,"
        End If

        If OptDueDate(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " GETPARTYPOPAYTERMS(TRN.COMPANY_CODE,ACCOUNTCODE,BILLNO,BILLDATE,'P'), TRN.DIV_CODE, ACM.LENDER_BANK_CODE, ACM.RESPONSIBLE_PERSON,"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " PAY_TERM_DESC, TRN.DIV_CODE, ACM.LENDER_BANK_CODE, ACM.RESPONSIBLE_PERSON," '' PAY_TERM_DESC  '  || ' DAYS'
        End If

        SqlStr = SqlStr & vbCrLf & " CASE WHEN PAYMENT_MODE='1' THEN 'CHEQUE' " & vbCrLf _
            & " WHEN PAYMENT_MODE='2' THEN 'HUNDI'" & vbCrLf _
            & " WHEN PAYMENT_MODE='3' THEN 'LC'" & vbCrLf _
            & " WHEN PAYMENT_MODE='4' THEN 'MSME'" & vbCrLf _
            & " WHEN PAYMENT_MODE='5' THEN 'PDC'" & vbCrLf _
            & " WHEN PAYMENT_MODE='6' THEN 'DISC-YES'" & vbCrLf _
            & " WHEN PAYMENT_MODE='7' THEN 'DISC-CASH'" & vbCrLf _
            & " WHEN PAYMENT_MODE='8' THEN 'DISC-TCFL'" & vbCrLf _
            & " WHEN PAYMENT_MODE='9' THEN 'UGRO'" & vbCrLf _
            & " WHEN PAYMENT_MODE='A' THEN 'ONLINE'" & vbCrLf _
            & " ELSE '' END AS PAYMENT_MODE_DESC, "

        SqlStr = SqlStr & vbCrLf & " SUPP_CUST_NATURE, '','' "
        '    PAYMENT_MODE_DESC,

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST GMST, FIN_PAYTERM_MST PMST, GEN_COMPANY_MST CC "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE TRN.COMPANY_CODE = CC.COMPANY_CODE" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " And TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " And TRN.AccountCode=ACM.SUPP_CUST_Code " & vbCrLf _
            & " And ACM.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " And ACM.GROUPCODE=GMST.GROUP_Code " & vbCrLf _
            & " And ACM.Company_Code=PMST.Company_Code(+)" & vbCrLf _
            & " And ACM.PAYMENT_CODE=PMST.PAY_TERM_CODE(+)"


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " And CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf & " And (ACM.SUPP_CUST_TYPE='C' OR ACM.SUPP_CUST_TYPE='S')"

        If OptSuppType(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.INTER_UNIT='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ACM.INTER_UNIT='N'"
        End If

        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtGroup.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtGroup.Text, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & " AND (ACM.GROUPCODE=" & MasterNo & " OR ACM.GROUPCODECR=" & MasterNo & ")"
            End If
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND  ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(Trim(UCase(TxtAccount.Text))) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE<= CASE WHEN BOOKTYPE='F' THEN TO_DATE('" & VB6.Format(txtPDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END"
        '    If chkClearChq.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BOOKTYPE='B' AND BOOKSUBTYPE='P' THEN CLEARDATE ELSE TO_DATE('15-Oct-2000') END <> CASE WHEN BOOKTYPE='B' AND BOOKSUBTYPE='P' THEN '' ELSE '01-APR-1999'  END"
        ''        SqlStr = SqlStr & vbCrLf & " AND CASE WHEN BOOKTYPE='B' AND BOOKSUBTYPE='P' THEN CLEARDATE END <> CASE WHEN BOOKTYPE='B' AND BOOKSUBTYPE='P' THEN '' END"
        '    End If
        If chkClearChq.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " HAVING ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount))<>0"
        Else
            SqlStr = SqlStr & vbCrLf & " HAVING ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount*DECODE(CLEARDATE,NULL,DECODE(BOOKTYPE||BOOKSUBTYPE,'BP',0,1),1)))<>0"
        End If
        ''
        SqlStr = SqlStr & vbCrLf & " GROUP BY BillNo, BillDate, "

        If OptDueDate(2).Checked = True Then       ''Or OptDueDate(4).Checked = True
            SqlStr = SqlStr & vbCrLf & "NVL(EXPDATE,BILLDATE),"
            'Else
            '    SqlStr = SqlStr & vbCrLf & " BILLDATE,"
        End If

        If optPartyWise.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ACM.SUPP_CUST_NAME, TRN.ACCOUNTCODE,"
        Else
            SqlStr = SqlStr & vbCrLf & " GMST.GROUP_NAME,  GMST.GROUP_CODE, "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " TRN.COMPANY_CODE, " & vbCrLf _
            & " TRN.FYEAR,ACM.SUPP_CUST_ADDR, ACM.SUPP_CUST_CITY, " & vbCrLf _
            & " ACM.SUPP_CUST_STATE, ACM.SUPP_CUST_PIN, ACM.SUPP_CUST_PHONE, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE,ACM.PAIDDAY, ACM.PAIDDAY2, ACM.PAIDDAY3, ACM.PAIDDAY4, " & vbCrLf _
            & " ACM.GROUPCODE,  ACM.GROUPCODECR, FROM_DAYS, TO_DAYS, PAY_TERM_DESC,TRN.DIV_CODE,ACM.LENDER_BANK_CODE,ACM.RESPONSIBLE_PERSON," & vbCrLf _
            & " CASE WHEN PAYMENT_MODE='1' THEN 'CHEQUE' " & vbCrLf _
            & " WHEN PAYMENT_MODE='2' THEN 'HUNDI'" & vbCrLf _
            & " WHEN PAYMENT_MODE='3' THEN 'LC'" & vbCrLf _
            & " WHEN PAYMENT_MODE='4' THEN 'MSME'" & vbCrLf _
            & " WHEN PAYMENT_MODE='5' THEN 'PDC'" & vbCrLf _
            & " WHEN PAYMENT_MODE='6' THEN 'DISC-YES'" & vbCrLf _
            & " WHEN PAYMENT_MODE='7' THEN 'DISC-CASH'" & vbCrLf _
            & " WHEN PAYMENT_MODE='8' THEN 'DISC-TCFL'" & vbCrLf _
            & " WHEN PAYMENT_MODE='9' THEN 'UGRO'" & vbCrLf _
            & " WHEN PAYMENT_MODE='A' THEN 'ONLINE'" & vbCrLf _
            & " ELSE '' END, SUPP_CUST_NATURE "

        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.COMPANY_CODE, " & vbCrLf _
            & " TRN.FYEAR,BillNo,BillDate ,"


        If optPartyWise.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ACM.SUPP_CUST_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " GMST.GROUP_NAME "
        End If

        SqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(SqlStr)
        InsertIntoTemp = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoTemp = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdAgeing()

        Dim cntCol As Integer
        With SprdAgeing
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .MaxCols = ColDel
            .Col = ColCode
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColCode, 8)
            .Col = ColName
            '.CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColName, 32)
            .Col = ColBill
            '.CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBill, 12)
            .ColsFrozen = ColBill
            .Col = ColDate
            .set_ColWidth(ColDate, 10)
            '.CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            For cntCol = ColBillAmount To ColDays10
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next
            .Col = ColDrCr
            .set_ColWidth(ColDrCr, 3)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '.CellType = SS_CELL_TYPE_STATIC_TEXT
            .Col = ColPayTerms
            .set_ColWidth(ColPayTerms, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColDueDays
            .set_ColWidth(ColDueDays, 8)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColLenderBank
            .set_ColWidth(ColLenderBank, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColSalePerson
            .set_ColWidth(ColSalePerson, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            .ColHidden = False
            'Else
            '    .ColHidden = True
            'End If

            .Col = ColCompanyName
            .set_ColWidth(ColCompanyName, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColPaymentMode
            .set_ColWidth(ColPaymentMode, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .Col = ColCategory
            .set_ColWidth(ColCategory, 15)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            If OptSumDet(0).Checked Then
                If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    .Col = ColCode
                    .ColHidden = True
                    .Col = ColName
                    .ColHidden = True
                Else
                    .Col = ColCode
                    .ColHidden = False
                    .Col = ColName
                    .ColHidden = False
                End If
                .Col = ColBill
                .ColHidden = False
                .Col = ColDate
                .ColHidden = False
            Else
                .Col = ColCode
                .ColHidden = False
                .Col = ColName
                .ColHidden = False
                .Col = ColBill
                .ColHidden = True
                .Col = ColDate
                .ColHidden = True
            End If
            .Col = ColDel
            .set_ColWidth(ColDel, 4)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True
            MainClass.SetSpreadColor(SprdAgeing, -1)
            MainClass.ProtectCell(SprdAgeing, 1, .MaxRows, 1, .MaxCols)
            SprdAgeing.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FillHeading()
        With SprdAgeing

            .Row = 0
            .Col = ColCode
            .Text = "Account Code"
            .Col = ColName
            .Text = "Account Name"
            .Col = ColBill
            .Text = "Bill No."
            .Col = ColDate
            .Text = "Date"
            .Col = ColBillAmount
            .Text = "Bill Amt"
            .Col = ColBal
            .Text = "Balance Amount"

            .Col = ColDue
            .Text = "Due Amount"
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, False, True)

            .Col = ColUnDue
            .Text = "Not Due"
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, True, False)

            .Col = ColDays1
            .Text = Val(txtDays1.Text) & " - " & Val(txtDays2.Text) & " Days"
            .Col = ColDays2
            .Text = Val(txtDays2.Text) + 1 & " - " & Val(txtDays3.Text) & " Days"
            .Col = ColDays3
            .Text = Val(txtDays3.Text) + 1 & " - " & Val(txtDays4.Text) & " Days"
            .Col = ColDays4
            .Text = Val(txtDays4.Text) + 1 & " - " & Val(txtDays5.Text) & " Days"
            .Col = ColDays5
            .Text = Val(txtDays5.Text) & " - " & Val(txtDays6.Text) & " Days"
            .Col = ColDays6
            .Text = Val(txtDays6.Text) + 1 & " - " & Val(txtDays7.Text) & " Days"
            .Col = ColDays7
            .Text = Val(txtDays7.Text) + 1 & " - " & Val(txtDays8.Text) & " Days"
            .Col = ColDays8
            .Text = Val(txtDays8.Text) + 1 & " - " & Val(txtDays9.Text) & " Days"
            .Col = ColDays9
            .Text = Val(txtDays9.Text) + 1 & " - " & Val(txtDays10.Text) & " Days"
            .Col = ColDays10
            .Text = "Above " & Val(txtDays10.Text) & " Days"
            .Col = ColDrCr
            .Text = "DC"
            .Col = ColPayTerms
            .Text = "Payment Terms"
            .Col = ColDueDays
            .Text = "Due Days"

            .Col = ColLenderBank
            .Text = "Lender Bank Name"

            .Col = ColSalePerson
            .Text = "Sale Person Name"

            .Col = ColCompanyName
            .Text = "Company Name"

            .Col = ColPaymentMode
            .Text = "Payment Mode"
            .Col = ColCategory
            .Text = "Category"
            .Col = ColDel
            .Text = "Del"

        End With
    End Sub
    Private Sub frmAgeingAnlyBreakup_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdAgeing.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 165, mReFormWidth - 165, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 105, mReFormWidth - 105, mReFormWidth))
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdAgeing, -1)
    End Sub
    Private Sub frmAgeingAnlyBreakup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then
        '        PvtDBCn.Close
        '        Set PvtDBCn = Nothing
        '    End If
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub OptSumDet_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSumDet.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSumDet.GetIndex(eventSender)
            PrintFlag = False
            PrintStatus()
            FraShow.Enabled = IIf(Index = 1, True, False)
        End If
    End Sub
    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub SprdAgeing_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdAgeing.DblClick
        Call ViewAccountLedger()
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        PrintFlag = False
        PrintStatus()
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
        Dim RsACM As ADODB.Recordset
        On Error GoTo ERR1
        If TxtAccount.Text = "" Then GoTo EventExitSub
        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'" & vbCrLf & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtGroup.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & "AND GROUPCODE=" & MasterNo & ""
            End If
        End If
        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)
        If Not RsACM.EOF = False Then
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
        Call ReportForAgeingAnly(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForAgeingAnly(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        PubDBCn.Errors.Clear()
        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        Call FillPrintDummy()
        '''''Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mSubTitle = "As On : " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        If OptSumDet(0).Checked Then
            mRPTName = "AgeAnlyBreakup.Rpt"
            mTitle = "Outstanding - (Age Wise)"
        Else
            mRPTName = "AgeAnlyBreakupSumm.Rpt"
            mTitle = "Outstanding - Summarised (Age Wise)"
        End If
        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If
        mTitle = mTitle & " - " & IIf(OptSuppType(1).Checked = True, "Supplier", IIf(OptSuppType(2).Checked = True, "Customer", "")) ''IIf(TxtGroup.Text = "", "ALL", TxtGroup.Text)
        mTitle = mTitle & " - " & IIf(TxtGroup.Text = "", "ALL", TxtGroup.Text)
        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
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
    Private Sub FillPrintDummy()

        Dim mName As String
        Dim mBill As String
        Dim mDate As String
        Dim mBal As String
        Dim mBillAmount As String
        Dim mDrCr As String
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mDEL As String
        Dim mUnDue As String
        Dim mDue As String

        Dim mDays1 As String
        Dim mDays2 As String
        Dim mDays3 As String
        Dim mDays4 As String
        Dim mDays5 As String
        Dim mDays6 As String
        Dim mDays7 As String
        Dim mDays8 As String
        Dim mDays9 As String
        Dim mDays10 As String
        On Error GoTo ERR1
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""
        With SprdAgeing
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColName
                If OptSumDet(0).Checked = True Then
                    If .Text <> "" Then
                        mName = .Text
                    End If
                Else
                    mName = .Text
                End If
                .Col = ColBill
                mBill = IIf(Trim(.Text) = "", ".", .Text)
                '            If Trim(.Text) = "GRAND TOTAL :" Then
                '                    mName = ""
                '            End If
                .Col = ColDate
                mDate = .Text
                .Col = ColBillAmount
                mBillAmount = .Text
                .Col = ColBal
                mBal = .Text

                .Col = ColDue
                mDue = .Text

                .Col = ColUnDue
                mUnDue = .Text

                .Col = ColDays1
                mDays1 = .Text
                .Col = ColDays2
                mDays2 = .Text
                .Col = ColDays3
                mDays3 = .Text
                .Col = ColDays4
                mDays4 = .Text
                .Col = ColDays5
                mDays5 = .Text
                .Col = ColDays6
                mDays6 = .Text
                .Col = ColDays7
                mDays7 = .Text
                .Col = ColDays8
                mDays8 = .Text
                .Col = ColDays9
                mDays9 = .Text
                .Col = ColDays10
                mDays10 = .Text
                .Col = ColDrCr
                mDrCr = .Text
                .Col = ColDel
                If chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked And chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mDEL = .Text
                Else
                    mDEL = ""
                End If
                If mDEL <> "D" Then
                    SqlStr = "Insert into TEMP_PRINTDUMMYDATA (UserID,SubRow,Field1," & vbCrLf _
                        & " Field2,Field3,Field4,Field5,Field6, " & vbCrLf _
                        & " Field7,Field8,Field9,Field10,Field11," & vbCrLf _
                        & " Field12,Field13,Field14,Field15,Field16,Field17, Field18) Values (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " " & cntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mName)) & "', " & vbCrLf _
                        & " '" & Trim(mBill) & "', " & vbCrLf & " '" & Trim(mDate) & "', " & vbCrLf _
                        & " '" & Trim(mBillAmount) & "', " & vbCrLf _
                        & " '" & Trim(mDays1) & "', " & vbCrLf _
                        & " '" & Trim(mDays1) & "', " & vbCrLf & " '" & Trim(mDays2) & "', " & vbCrLf _
                        & " '" & Trim(mDays3) & "', " & vbCrLf & " '" & Trim(mDays4) & "', " & vbCrLf _
                        & " '" & Trim(mDays5) & "', " & vbCrLf & " '" & Trim(mDays6) & "', " & vbCrLf _
                        & " '" & Trim(mDays7) & "', " & vbCrLf & " '" & Trim(mDays8) & "', " & vbCrLf _
                        & " '" & Trim(mDays9) & "', " & vbCrLf & " '" & Trim(mDays10) & "', " & vbCrLf _
                        & " '" & Trim(mDrCr) & "', '" & Trim(mDue) & "', '" & Trim(mBal) & "') "  ''
                    PubDBCn.Execute(SqlStr)
                End If
NextRow:
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

        Dim mColTitle1 As String
        Dim mColTitle2 As String
        Dim mColTitle3 As String
        Dim mColTitle4 As String
        Dim mString As String
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        mString = Val(txtDays1.Text) & " - " & Val(txtDays2.Text) & " Days"
        MainClass.AssignCRptFormulas(Report1, "mColTitle1=""" & mString & """")
        mString = Val(txtDays2.Text) + 1 & " - " & Val(txtDays3.Text) & " Days"
        MainClass.AssignCRptFormulas(Report1, "mColTitle2=""" & mString & """")
        mString = Val(txtDays3.Text) + 1 & " - " & Val(txtDays4.Text) & " Days"
        MainClass.AssignCRptFormulas(Report1, "mColTitle3=""" & mString & """")
        mString = Val(txtDays4.Text) + 1 & " - " & Val(txtDays5.Text) & " Days"
        MainClass.AssignCRptFormulas(Report1, "mColTitle4=""" & mString & """")
        mString = "Above " & Val(txtDays5.Text) & " Days"
        MainClass.AssignCRptFormulas(Report1, "mColTitle5=""" & mString & """")
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW,Field1"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub DisplayTotal()
        On Error GoTo DisplayErr
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mNextPartyName As String
        Dim mPartyName As String
        Dim mDC As String
        Dim mBalance As Double
        Dim mTotBalance As Double
        Dim mBillAmount As Double
        Dim mTotBillAmount As Double
        Dim mUnDueAmount As Double
        Dim mDays1Amount As Double
        Dim mDays2Amount As Double
        Dim mDays3Amount As Double
        Dim mDays4Amount As Double
        Dim mDays5Amount As Double
        Dim mDays6Amount As Double
        Dim mDays7Amount As Double
        Dim mDays8Amount As Double
        Dim mDays9Amount As Double
        Dim mDays10Amount As Double
        Dim mTotUnDueAmount As Double
        Dim mTotDays1Amount As Double
        Dim mTotDays2Amount As Double
        Dim mTotDays3Amount As Double
        Dim mTotDays4Amount As Double
        Dim mTotDays5Amount As Double
        Dim mTotDays6Amount As Double
        Dim mTotDays7Amount As Double
        Dim mTotDays8Amount As Double
        Dim mTotDays9Amount As Double
        Dim mTotDays10Amount As Double
        Dim mDueAmount As Double
        Dim mTotDueAmount As Double

        cntRow = 1
        With SprdAgeing
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColName
                mPartyName = .Text
                .Col = ColDrCr
                mDC = .Text
                .Col = ColBillAmount
                mBillAmount = mBillAmount + (IIf(mDC = "DR", 1, -1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotBillAmount = mTotBillAmount + (IIf(mDC = "DR", 1, -1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColBal
                mBalance = mBalance + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotBalance = mTotBalance + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))

                .Col = ColDue
                mDueAmount = mDueAmount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDueAmount = mTotDueAmount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))

                .Col = ColUnDue
                mUnDueAmount = mUnDueAmount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotUnDueAmount = mTotUnDueAmount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays1
                mDays1Amount = mDays1Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays1Amount = mTotDays1Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays2
                mDays2Amount = mDays2Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays2Amount = mTotDays2Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays3
                mDays3Amount = mDays3Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays3Amount = mTotDays3Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays4
                mDays4Amount = mDays4Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays4Amount = mTotDays4Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays5
                mDays5Amount = mDays5Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays5Amount = mDays5Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays6
                mDays6Amount = mDays6Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays6Amount = mTotDays6Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays7
                mDays7Amount = mDays7Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays7Amount = mTotDays7Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays8
                mDays8Amount = mDays8Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays8Amount = mTotDays8Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays9
                mDays9Amount = mDays9Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays9Amount = mTotDays9Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                .Col = ColDays10
                mDays10Amount = mDays10Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotDays10Amount = mDays10Amount + (IIf(mDC = "DR", 1, 1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                cntRow = cntRow + 1
                .Row = cntRow
                .Col = ColName
                mNextPartyName = .Text
                If mPartyName <> mNextPartyName Then
                    .Row = cntRow
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    Call GridTotal("Total :", mBillAmount, mBalance, mDueAmount, mUnDueAmount, mDays1Amount, mDays2Amount, mDays3Amount, mDays4Amount, mDays5Amount, mDays6Amount, mDays7Amount, mDays8Amount, mDays9Amount, mDays10Amount, cntRow)
                    mBillAmount = 0
                    mBalance = 0
                    mDays1Amount = 0
                    mDays2Amount = 0
                    mDays3Amount = 0
                    mDays4Amount = 0
                    mDays5Amount = 0
                    mDays6Amount = 0
                    mDays7Amount = 0
                    mDays8Amount = 0
                    mDays9Amount = 0
                    mDays10Amount = 0
                    cntRow = cntRow + 1
                End If
            Loop
            If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then

                .MaxRows = .MaxRows + 1
                Call GridTotal("Grand Total :", mTotBillAmount, mTotBalance, mDueAmount, mUnDueAmount, mTotDays1Amount, mTotDays2Amount, mTotDays3Amount, mTotDays4Amount, mTotDays5Amount, mTotDays6Amount, mTotDays7Amount, mTotDays8Amount, mTotDays9Amount, mTotDays10Amount, .MaxRows)


            End If
        End With
        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub GridTotal(ByRef mTotalString As String, ByRef mBillAmount As Double, ByRef mBalance As Double, ByRef mDueAmount As Double, ByRef mUnDueAmount As Double, ByRef mDays1Amount As Double, ByRef mDays2Amount As Double, ByRef mDays3Amount As Double, ByRef mDays4Amount As Double, ByRef mDays5Amount As Double, ByRef mDays6Amount As Double, ByRef mDays7Amount As Double, ByRef mDays8Amount As Double, ByRef mDays9Amount As Double, ByRef mDays10Amount As Double, ByRef mRow As Integer)
        With SprdAgeing
            .Row = mRow
            .Col = IIf(OptSumDet(0).Checked = True, ColBill, ColName)
            .Text = mTotalString
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBillAmount
            .Text = VB6.Format(System.Math.Abs(mBillAmount), "0.00") ''& CStr(IIf(mBillAmount >= 0, "Dr", "Cr"))
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBal
            .Text = VB6.Format(System.Math.Abs(mBalance), "0.00") ''& CStr(IIf(mBalance >= 0, "Dr", "Cr"))
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColDue
            .Text = VB6.Format(System.Math.Abs(mDueAmount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)


            .Col = ColUnDue
            .Text = VB6.Format(System.Math.Abs(mUnDueAmount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays1
            .Text = VB6.Format(System.Math.Abs(mDays1Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays2
            .Text = VB6.Format(System.Math.Abs(mDays2Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays3
            .Text = VB6.Format(System.Math.Abs(mDays3Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays4
            .Text = VB6.Format(System.Math.Abs(mDays4Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays5
            .Text = VB6.Format(System.Math.Abs(mDays5Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays6
            .Text = VB6.Format(System.Math.Abs(mDays6Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays7
            .Text = VB6.Format(System.Math.Abs(mDays7Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays8
            .Text = VB6.Format(System.Math.Abs(mDays8Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays9
            .Text = VB6.Format(System.Math.Abs(mDays9Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDays10
            .Text = VB6.Format(System.Math.Abs(mDays10Amount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDrCr
            .Text = CStr(IIf(mBalance >= 0, "DR", "CR"))
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = mRow
            .Row2 = mRow
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(IIf(mTotalString = "Total :", &H8000000F, "&HFFFFC0"))
            .BlockMode = False
        End With
    End Sub
    Private Sub DisplaySummTotal()
        On Error GoTo DisplayErr
        Dim mDrCr As Integer
        Dim mBal As Double
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mBillAmount As Double
        Dim mDueAmount As Double
        Dim mUnDueAmount As Double
        Dim mDays1Amount As Double
        Dim mDays2Amount As Double
        Dim mDays3Amount As Double
        Dim mDays4Amount As Double
        Dim mDays5Amount As Double
        Dim mDays6Amount As Double
        Dim mDays7Amount As Double
        Dim mDays8Amount As Double
        Dim mDays9Amount As Double
        Dim mDays10Amount As Double
        With SprdAgeing
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDrCr
                mDrCr = IIf(UCase(.Text) = "DR", 1, -1)
                .Col = ColBillAmount
                If IsNumeric(.Text) Then
                    mBillAmount = mBillAmount + (CDbl(.Text) * mDrCr)
                End If
                mDrCr = IIf(UCase(.Text) = "DR", 1, 1)
                .Col = ColBal
                If IsNumeric(.Text) Then
                    mBal = mBal + (CDbl(.Text) * mDrCr)
                End If

                .Col = ColDue
                If IsNumeric(.Text) Then
                    mDueAmount = mDueAmount + (CDbl(.Text) * mDrCr)
                End If

                .Col = ColUnDue
                If IsNumeric(.Text) Then
                    mUnDueAmount = mUnDueAmount + (CDbl(.Text) * mDrCr)
                End If


                .Col = ColDays1
                If IsNumeric(.Text) Then
                    mDays1Amount = mDays1Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays2
                If IsNumeric(.Text) Then
                    mDays2Amount = mDays2Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays3
                If IsNumeric(.Text) Then
                    mDays3Amount = mDays3Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays4
                If IsNumeric(.Text) Then
                    mDays4Amount = mDays4Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays5
                If IsNumeric(.Text) Then
                    mDays5Amount = mDays5Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays6
                If IsNumeric(.Text) Then
                    mDays6Amount = mDays6Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays7
                If IsNumeric(.Text) Then
                    mDays7Amount = mDays7Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays8
                If IsNumeric(.Text) Then
                    mDays8Amount = mDays8Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays9
                If IsNumeric(.Text) Then
                    mDays9Amount = mDays9Amount + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColDays10
                If IsNumeric(.Text) Then
                    mDays10Amount = mDays10Amount + (CDbl(.Text) * mDrCr)
                End If
                '            .Col = ColBal
                '            If .Text <> "" Then
                '                If IsNumeric(Mid((.Text), 1, Len(.Text) - 2)) Then
                '                    mAge1 = mAge1 + ((Mid((.Text), 1, Len(.Text) - 2)) * IIf(Mid((.Text), Len(.Text) - 1, Len(.Text)) = "DR", 1, -1))
                '                End If
                '            End If
            Next
            '        .MaxRows = .MaxRows + 1
            '        .Row = .MaxRows
            '        For cntCol = ColBal To .MaxCols
            '            .Col = cntCol
            '            .Text = String(254, "_")
            '        Next
            .MaxRows = .MaxRows + 1
            Call GridTotal("Total :", mBillAmount, mBal, mDueAmount, mUnDueAmount, mDays1Amount, mDays2Amount, mDays3Amount, mDays4Amount, mDays5Amount, mDays6Amount, mDays7Amount, mDays8Amount, mDays9Amount, mDays10Amount, .MaxRows)
            '
            '        .Col = ColName
            '        .Text = "TOTAL :"
            '        .FontBold = True
            '
            '        .Col = ColBal
            '        .Text = MainClass.FormatRupees(Abs(mBal))
            '
            '        .Col = ColDrCr
            '        .Text = IIf(mBal >= 0, "DR", "CR")
            '        .MaxRows = .MaxRows + 1
            '        .Row = .MaxRows
            '        For cntCol = ColBal To .MaxCols
            '            .Col = cntCol
            '            .Text = String(254, "=")
            '        Next
        End With
        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub
    Private Sub txtDays1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays1.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDays1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDays2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays2.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDays2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDays3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays3.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDays3_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays3.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDays4_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays4.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDays4_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays4.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDays5_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays5.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDays5_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays5.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub DeleteSprdAgingRow()
        Dim RowCnt As Integer
        Dim I As Integer
        RowCnt = 1
        I = 0
        Do While RowCnt <= SprdAgeing.MaxRows
            With SprdAgeing
                .Row = RowCnt
                .Col = ColDel
                If .Text = "D" Then
                    ''.Action = SS_ACTION_DELETE_ROW
                    .RowHidden = True
                Else
                    I = I + 1
                End If
                .Col = 0
                .Text = CStr(I)
                RowCnt = RowCnt + 1
            End With
        Loop
    End Sub
    Private Sub ViewAccountLedger()

        On Error GoTo ErrPart
        If SprdAgeing.ActiveRow <= 0 Then Exit Sub

        frmViewLedger.lblBookType.Text = "LEDG"

        SprdAgeing.Row = SprdAgeing.ActiveRow
        SprdAgeing.Col = ColName
        If LTrim(RTrim(SprdAgeing.Text)) = "" Then Exit Sub



        frmViewLedger.txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        frmViewLedger.txtDateTo.Text = txtDateTo.Text
        frmViewLedger.OptSumDet(0).Checked = True
        frmViewLedger.cboDivision.Text = cboDivision.Text
        frmViewLedger.MdiParent = Me.MdiParent

        frmViewLedger.Show()

        frmViewLedger.cboAccount.Text = SprdAgeing.Text
        MainClass.ValidateWithMasterTable(SprdAgeing.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        frmViewLedger.lblAcCode.Text = MasterNo
        If LTrim(RTrim(frmViewLedger.lblAcCode.Text)) = "" Then Exit Sub

        'frmViewLedger.cboDivision.Text = cboDivision.Text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedger.MdiParent = Me.MdiParent

        frmViewLedger.frmViewLedger_Activated(Nothing, New System.EventArgs())
        frmViewLedger.cmdShow_Click(Nothing, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub txtPDCDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPDCDate.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub ChkAllPerson_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllPerson.CheckStateChanged
        txtSalePerson.Enabled = IIf(chkAllPerson.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        PrintFlag = False
        PrintStatus()
    End Sub


    Private Sub txtSalePerson_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSalePerson.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtSalePerson_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSalePerson.DoubleClick
        SearchSalePerson()
    End Sub
    Private Sub txtSalePerson_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSalePerson.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSalePerson()
    End Sub
    Private Sub SearchSalePerson()
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If MainClass.SearchGridMaster((txtSalePerson.Text), "FIN_SALESPERSON_MST", "NAME", "CODE", , , "") = True Then
                If AcName <> "" Then
                    txtSalePerson.Text = AcName
                End If
            End If
        Else
            If MainClass.SearchGridMaster((txtSalePerson.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
                If AcName <> "" Then
                    txtSalePerson.Text = AcName
                End If
            End If
        End If

    End Sub

    Private Sub SprdAgeing_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdAgeing.KeyPressEvent
        Try
            If e.keyAscii = 18 Then
                Dim mFieldValue As String
                Dim I As Long
                Dim mDelRow As Long


                mDelRow = SprdAgeing.ActiveRow

                SprdAgeing.Row = mDelRow
                SprdAgeing.Col = IIf(OptSumDet(0).Checked = True, ColBill, ColName)
                mFieldValue = Trim(SprdAgeing.Text)

                If mFieldValue = "Total :" Or mFieldValue = "Grand Total :" Then
                    Exit Sub
                End If

                SprdAgeing.DeleteRows(mDelRow, 1)

                If SprdAgeing.MaxRows > 1 Then SprdAgeing.MaxRows = SprdAgeing.MaxRows - 1

                For I = SprdAgeing.MaxRows To 1 Step -1
                    SprdAgeing.Row = I
                    SprdAgeing.Col = IIf(OptSumDet(0).Checked = True, ColBill, ColName)
                    mFieldValue = Trim(SprdAgeing.Text)

                    If mFieldValue = "Total :" Or mFieldValue = "Grand Total :" Then
                        SprdAgeing.Action = SS_ACTION_DELETE_ROW
                        If SprdAgeing.MaxRows > 1 Then SprdAgeing.MaxRows = SprdAgeing.MaxRows - 1
                    End If
                Next

                If OptSumDet(0).Checked Then
                    DisplayTotal()
                Else
                    DisplaySummTotal()
                End If

                FormatSprdAgeing()

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
