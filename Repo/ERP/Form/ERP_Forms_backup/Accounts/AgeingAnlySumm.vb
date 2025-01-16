Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAgeingAnalSumm
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 12
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColBal As Short = 3
    Private Const ColDrCr As Short = 4
    Private Const ColPayTerms As Short = 5
    Private Const ColBal1 As Short = 6
    Private Const ColBal2 As Short = 7
    Private Const ColBal3 As Short = 8
    Private Const ColBal4 As Short = 9
    Private Const ColBal5 As Short = 10
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer
    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
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
    Private Sub ChkAllGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllGroup.CheckStateChanged
        TxtGroup.Enabled = IIf(chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
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
        Me.Close()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtGroup.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtGroup, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & " AND (GROUPCODE=" & MasterNo & ")"
            End If
        End If

        If MainClass.SearchGridMaster((TxtAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", "SUPP_CUST_CODE", , , SqlStr) = True Then
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

        Dim mCntRow As Integer
        Dim mCode As String
        Dim mAsOnDate As String
        Dim mDate2 As String
        Dim mDate1 As String
        Dim mReceiptBalance As Double
        Dim mOpeningBal1 As Double
        Dim mOpeningBal2 As Double
        Dim mOpeningBal3 As Double
        Dim mOpeningBal4 As Double
        Dim mOpeningBal5 As Double
        Dim mReceipt1 As Double
        Dim mReceipt2 As Double
        Dim mReceipt3 As Double
        Dim mReceipt4 As Double
        Dim mBalance1 As Double
        Dim mBalance2 As Double
        Dim mBalance3 As Double
        Dim mBalance4 As Double
        Dim mBalance5 As Double
        Dim mBalance As Double
        Dim mMonthDate As String
        PrintFlag = False
        PrintStatus()
        MainClass.ClearGrid(SprdAgeing, RowHeight)
        If FieldsVerification = False Then Exit Sub
        txtDateTo.Text = MainClass.LastDay(Month(CDate(txtDateTo.Text)), Year(CDate(txtDateTo.Text))) & "/" & VB6.Format(txtDateTo.Text, "mm/YYYY")
        AgeingInfo()
        With SprdAgeing
            For mCntRow = 1 To .MaxRows
                .Row = mCntRow
                .Col = ColCode
                mCode = Trim(.Text)
                .Col = ColBal
                mBalance = Val(.Text)
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -6, CDate(VB6.Format(txtDateTo.Text))))
                mDate1 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mDate1))))
                mOpeningBal1 = GetAmount(mCode, mDate1, mDate1, "O")
                mReceipt1 = System.Math.Abs(GetAmount(mCode, mDate1, VB6.Format(txtDateTo.Text), "B"))
                mReceiptBalance = mOpeningBal1 - mReceipt1
                mBalance1 = IIf(mOpeningBal1 > mReceipt1, mOpeningBal1 - mReceipt1, 0)
                .Col = ColBal5
                .Text = VB6.Format(mBalance1, "0.00")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -6, CDate(VB6.Format(txtDateTo.Text))))
                mDate1 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mDate1))))
                mDate2 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -3, CDate(VB6.Format(txtDateTo.Text))))
                mDate2 = MainClass.LastDay(Month(CDate(mDate2)), Year(CDate(mDate2))) & "/" & VB6.Format(mDate2, "mm/YYYY")
                '            mDate1 = DateAdd("d", -90, Format(txtDateTo.Text))
                '            mDate2 = MainClass.LastDay(Month(mDate1), Year(mDate1)) & "/" & vb6.Format(mDate1, "mm/YYYY")
                mOpeningBal2 = GetAmount(mCode, mDate1, mDate2, "")
                mReceiptBalance = mOpeningBal2 + IIf(mReceiptBalance > 0, 0, mReceiptBalance)
                If mReceiptBalance > 0 Then
                    mBalance2 = mReceiptBalance '' mOpeningBal2
                Else
                    mBalance2 = 0
                End If
                .Col = ColBal4
                .Text = VB6.Format(mBalance2, "0.00")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -3, CDate(VB6.Format(txtDateTo.Text))))
                mDate1 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mDate1))))
                mDate2 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mOpeningBal3 = GetAmount(mCode, mDate1, mDate2, "")
                mReceiptBalance = mOpeningBal3 + IIf(mReceiptBalance > 0, 0, mReceiptBalance)
                If mReceiptBalance > 0 Then
                    mBalance3 = mReceiptBalance '' mOpeningBal3
                Else
                    mBalance3 = 0
                End If
                .Col = ColBal3
                .Text = VB6.Format(mBalance3, "0.00")
                '            mBalance = GetAmount(mCode, mDate1, mDate1, False)
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -2, CDate(VB6.Format(txtDateTo.Text))))
                mDate1 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mDate1))))
                mDate2 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mOpeningBal4 = GetAmount(mCode, mDate1, mDate2, "")
                mReceiptBalance = mOpeningBal4 + IIf(mReceiptBalance > 0, 0, mReceiptBalance)
                If mReceiptBalance > 0 Then
                    mBalance4 = mReceiptBalance 'mOpeningBal4
                Else
                    mBalance4 = 0
                End If
                .Col = ColBal2
                .Text = VB6.Format(mBalance4, "0.00") ''+ mBalance2 + mBalance3
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(VB6.Format(txtDateTo.Text))))
                mDate1 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mDate1 = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mDate1))))
                mDate2 = MainClass.LastDay(Month(CDate(mDate1)), Year(CDate(mDate1))) & "/" & VB6.Format(mDate1, "mm/YYYY")
                mOpeningBal5 = GetAmount(mCode, mDate1, mDate2, "")
                mReceiptBalance = mOpeningBal5 + IIf(mReceiptBalance > 0, 0, mReceiptBalance)
                If mReceiptBalance > 0 Then
                    mBalance5 = mReceiptBalance 'mOpeningBal4
                Else
                    mBalance5 = 0
                End If
                .Col = ColBal1
                .Text = VB6.Format(mBalance5, "0.00") ''+ mBalance2 + mBalance3
            Next
        End With
        '    DisplayTotal
        FormatSprdAgeing()
        FillHeading()
        SprdAgeing.Focus()
        PrintFlag = True
        PrintStatus()
        MainClass.SetFocusToCell(SprdAgeing, mActiveRow, 4)
    End Sub
    Function FieldsVerification() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsACM As ADODB.Recordset
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
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
            If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(TxtGroup, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SqlStr = SqlStr & "AND (GROUPCODE=" & MasterNo & ")"
                End If
            End If
            SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)
            If RsACM.EOF Then
                TxtAccount.Focus()
                MsgInformation("No Such Account in Account Master")
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub frmAgeingAnalSumm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        TxtGroup.Enabled = IIf(chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmAgeingAnalSumm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
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
        chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked
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
        FormatSprdAgeing()
        FillHeading()
        chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked
        chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        PrintStatus()
        Call frmAgeingAnalSumm_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub AgeingInfo()

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mAgeingDays As String
        Dim mSql As String
        Dim mSQLS As String
        Dim mSql1 As String
        Dim mBillAmtStr As String
        Dim mDivisionCode As Double
        '    mBillAmtStr = "TO_CHAR(ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'DR',1,-1)*Amount)),'99999999999.99')"
        '
        '    mSql1 = "SUM(DECODE(DC,'DR',1,-1)*Amount)"
        '
        '    mSql = "TO_CHAR(Abs(SUM(DECODE(DC,'DR',1,-1)*Amount)),'99999999999.99')"
        '    mSQLS = "DECODE(DC,'DR',1,-1)*Amount"
        SqlStr = "SELECT CMST.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS Name, "
        SqlStr = SqlStr & vbCrLf & " SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT) AS BAL, " & vbCrLf & " CASE WHEN SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT)>=0 THEN 'DR' ELSE 'CR' END,'',0,0,0,0,0"
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE = CMST.COMPANY_CODE " & vbCrLf & " AND TRN.ACCOUNTCODE = CMST.SUPP_CUST_CODE "
        SqlStr = SqlStr & vbCrLf & " AND (CMST.SUPP_CUST_TYPE='C' OR CMST.SUPP_CUST_TYPE='S')"
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtGroup.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtGroup, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & " AND (GROUPCODE=" & MasterNo & " OR GROUPCODECR=" & MasterNo & ")"
            End If
        End If
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND  CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If
        SqlStr = SqlStr & vbCrLf & "AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " GROUP BY CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME" '',DC"
        If OptShow(0).Checked = True Then
            If chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT) <> 0   "
            End If
        ElseIf OptShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT) > 0   "
        ElseIf OptShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT) < 0   "
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME"
        MainClass.AssignDataInSprd(SqlStr, AData1, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub
    Private Function GetAmount(ByRef mAccountCode As String, ByRef mDate1 As String, ByRef mDate2 As String, ByRef mBookType As String) As Double

        On Error GoTo LedgError
        Dim mDivisionCode As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim MakeSQL As String
        GetAmount = 0
        '    If mBookType = "O" Then
        MakeSQL = "SELECT MKEY, SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT) AS BAL "
        '    ElseIf mBookType = "B" Then
        '        MakeSql = "SELECT SUM(DECODE(TRN.DC,'D',0,-1) * TRN.AMOUNT) AS BAL "
        '    Else
        '        MakeSql = "SELECT  SUM(DECODE(TRN.DC,'D',1,0) * TRN.AMOUNT) AS BAL "
        '    End If
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_POSTED_TRN TRN" & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.ACCOUNTCODE = '" & mAccountCode & "' "
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        If mBookType = "B" Then
            '        MakeSql = MakeSql & vbCrLf & " AND (TRN.BookType= '" & ConBankBook & "' or TRN.BookType= '" & ConPurchaseBook & "' OR TRN.BookType= '" & ConCreditNoteBook & "')"
            MakeSQL = MakeSQL & vbCrLf & "AND VDATE>=TO_DATE('" & VB6.Format(mDate1, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND VDATE<=TO_DATE('" & VB6.Format(mDate2, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf mBookType = "O" Then
            MakeSQL = MakeSQL & vbCrLf & "AND VDATE<TO_DATE('" & VB6.Format(mDate1, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            '        MakeSql = MakeSql & vbCrLf & " AND TRN.BookType NOT IN ( '" & ConBankBook & "','" & ConPurchaseBook & "' ,'" & ConCreditNoteBook & "')"
            MakeSQL = MakeSQL & vbCrLf & "AND VDATE>=TO_DATE('" & VB6.Format(mDate1, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND VDATE<=TO_DATE('" & VB6.Format(mDate2, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MakeSQL = MakeSQL & vbCrLf & " GROUP BY MKEY"
        If mBookType = "O" Then
        ElseIf mBookType = "B" Then
            MakeSQL = MakeSQL & vbCrLf & " HAVING SUM(DECODE(TRN.DC,'D',1,-1)* TRN.AMOUNT)<0"
        Else
            MakeSQL = MakeSQL & vbCrLf & " HAVING SUM(DECODE(TRN.DC,'D',1,-1)* TRN.AMOUNT)>0"
        End If
        MainClass.UOpenRecordSet(MakeSQL, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            '        GetAmount = GetAmount + IIf(IsNull(RsTemp!BAL), 0, RsTemp!BAL)
            Do While RsTemp.EOF = False
                '            If mBookType = "B" Then
                '                If IIf(IsNull(RsTemp!BAL), 0, RsTemp!BAL) < 0 Then
                '                    GetAmount = GetAmount + IIf(IsNull(RsTemp!BAL), 0, RsTemp!BAL)
                '                End If
                '            ElseIf mBookType = "O" Then
                '                GetAmount = GetAmount + IIf(IsNull(RsTemp!BAL), 0, RsTemp!BAL)
                '            Else
                '                If IIf(IsNull(RsTemp!BAL), 0, RsTemp!BAL) > 0 Then
                '                    GetAmount = GetAmount + IIf(IsNull(RsTemp!BAL), 0, RsTemp!BAL)
                '                End If
                '            End If
                GetAmount = GetAmount + IIf(IsDbNull(RsTemp.Fields("BAL").Value), 0, RsTemp.Fields("BAL").Value)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
LedgError:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdAgeing()

        Dim cntCol As Integer
        With SprdAgeing
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .MaxCols = ColBal5
            .Col = ColCode
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColCode, 5)
            .Col = ColName
            '.CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColName, 25)
            .Col = ColDrCr
            .set_ColWidth(ColDrCr, 3)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '.CellType = SS_CELL_TYPE_STATIC_TEXT
            .Col = ColPayTerms
            .set_ColWidth(ColPayTerms, 20)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            For cntCol = ColBal To ColBal
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 15)
            Next
            For cntCol = ColBal1 To ColBal5
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 15)
            Next
            MainClass.SetSpreadColor(SprdAgeing, -1)
            MainClass.ProtectCell(SprdAgeing, 1, .MaxRows, 1, .MaxCols)
            SprdAgeing.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' OperationModeSingle
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
            .Col = ColBal
            .Text = "Bal Amount"
            .Col = ColDrCr
            .Text = "DC"
            .Col = ColPayTerms
            .Text = "Payment Terms"
            .Col = ColBal1
            .Text = "0 Days-30 Days"
            .Col = ColBal2
            .Text = "31 Days-60 Days"
            .Col = ColBal3
            .Text = "61 Days-90 Days"
            .Col = ColBal4
            .Text = "91 Days-180 Days"
            .Col = ColBal5
            .Text = "More Than 180 Days"
        End With
    End Sub
    Private Sub frmAgeingAnalSumm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdAgeing.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 165, mReFormWidth - 165, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 105, mReFormWidth - 105, mReFormWidth))
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdAgeing, -1)
    End Sub
    Private Sub frmAgeingAnalSumm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then
        '        PvtDBCn.Close
        '        Set PvtDBCn = Nothing
        '    End If
        Me.Close()
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
        SqlStr = "SELECT SUPP_CUST_NAME,PAIDDAY, PAIDDAY2, PAIDDAY3, PAIDDAY4 FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'" & vbCrLf & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        If chkAllGroup.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtGroup, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & "AND GROUPCODE=" & MasterNo & ""
            End If
        End If
        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)
        If RsACM.EOF = True Then
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
        mRPTName = "AgeAnly.Rpt"
        mTitle = "Outstanding - (Age Wise)"
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
        On Error GoTo ERR1
        Dim mName As String
        Dim mBill As String
        Dim mDate As String
        Dim mBal As String
        Dim mBillAmount As String
        Dim mDrCr As String
        Dim mPayTerms As String
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mDEL As String
        Dim mAddress As String
        Dim mMaxRow As Integer
        '    PubDBCn.Errors.Clear
        '
        '    PubDBCn.BeginTrans
        '
        '    mMaxRow = SprdAgeing.MaxRows
        '    SqlStr = ""
        '    With SprdAgeing
        '
        '        For cntRow = 1 To mMaxRow
        '            .Row = cntRow
        '
        '            .Col = ColName
        '            If OptSumDet(0).Value = True Then
        '                If .Text <> "" Then
        '                    mName = .Text
        '                End If
        '            Else
        '                mName = .Text
        '            End If
        '
        '            .Col = ColBill
        '            mBill = IIf(Trim(.Text) = "", ".", .Text)
        ''            If Trim(.Text) = "GRAND TOTAL :" Then
        ''                    mName = ""
        ''            End If
        '
        '            .Col = ColDate
        '            mDate = .Text
        '
        '            .Col = ColBillAmount
        '            mBillAmount = .Text
        '
        '            .Col = ColBal
        '            mBal = .Text
        '
        '            .Col = ColDrCr
        '            mDrCr = .Text
        '
        '            .Col = ColPayTerms
        '            mPayTerms = .Text
        '
        '            .Col = ColDel
        '            If chkHideZero.Value = vbChecked And chkAll.Value = vbChecked Then
        '                mDEL = .Text
        '            Else
        '                mDEL = ""
        '            End If
        '
        '            .Col = ColAddress
        '            mAddress = .Text
        '
        '            If mDEL <> "D" Then
        '                SqlStr = "Insert into TEMP_PRINTDUMMYDATA (UserID,SubRow,Field1," & vbCrLf _
        ''                    & " Field2,Field3,Field4,Field5,Field6,Field7,Field8) Values (" & vbCrLf _
        ''                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                    & " " & cntRow & ", " & vbCrLf _
        ''                    & " '" & MainClass.AllowSingleQuote(Trim(mName)) & "', " & vbCrLf _
        ''                    & " '" & Trim(mBill) & "', " & vbCrLf _
        ''                    & " '" & Trim(mDate) & "', " & vbCrLf _
        ''                    & " '" & Trim(mBillAmount) & "', " & vbCrLf _
        ''                    & " '" & Trim(mBal) & "', " & vbCrLf _
        ''                    & " '" & Trim(mDrCr) & "', '" & Trim(mPayTerms) & "', " & vbCrLf _
        ''                    & " '" & MainClass.AllowSingleQuote(mAddress) & "') "
        '
        '                PubDBCn.Execute SqlStr
        '            End If
        'NextRow:
        '        Next
        '    End With
        '    PubDBCn.CommitTrans
        Exit Sub
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mColTitle1 As String = ""
        Dim mColTitle2 As String = ""
        Dim mColTitle3 As String = ""
        Dim mColTitle4 As String = ""
        Dim mColTitle5 As String = ""
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        SprdAgeing.Row = 0
        SprdAgeing.Col = ColBal
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW,Field1"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
        cntRow = 1
        With SprdAgeing
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColName
                mPartyName = .Text
                .Col = ColDrCr
                mDC = .Text
                .Col = ColBal
                mBalance = mBalance + (IIf(mDC = "DR", 1, -1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                mTotBalance = mTotBalance + (IIf(mDC = "DR", 1, -1) * CDbl(IIf(IsNumeric(.Text), .Text, 0)))
                cntRow = cntRow + 1
                .Row = cntRow
                .Col = ColName
                mNextPartyName = .Text
                If mPartyName <> mNextPartyName Then
                    .Row = cntRow
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    For cntCol = ColBal To ColDrCr
                        .Col = cntCol
                        .Text = New String("_", 254)
                    Next
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    Call GridTotal("Total :", mBalance, cntRow - 1)
                    mBalance = 0
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    For cntCol = ColBal To ColDrCr
                        .Col = cntCol
                        .Text = New String("_", 254)
                    Next
                    cntRow = cntRow + 1
                End If
            Loop
            ''        .MaxRows = .MaxRows + 1
            ''        For cntCol = ColBal To ColDrCr
            ''            .Row = .MaxRows
            ''            .Col = cntCol
            ''            .Text = String(254, "_")
            ''        Next
            ''
            ''        .MaxRows = .MaxRows + 1
            ''        Call GridTotal("Total :", mBalance, .MaxRows)
            '
            '        .MaxRows = .MaxRows + 1
            '        For cntCol = ColBal To ColDrCr
            '            .Row = .MaxRows
            '            .Col = cntCol
            '            .Text = String(254, "_")
            '        Next
            If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
                .MaxRows = .MaxRows + 1
                Call GridTotal("Grand Total :", mTotBalance, .MaxRows)
                .MaxRows = .MaxRows + 1
                For cntCol = ColBal To ColDrCr
                    .Row = .MaxRows
                    .Col = cntCol
                    .Text = New String("_", 254)
                Next
            End If
        End With
        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub GridTotal(ByRef mTotalString As String, ByRef mBalance As Double, ByRef mRow As Integer)
        With SprdAgeing
            .Row = mRow
            .Col = ColName
            .Text = mTotalString
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBal
            .Text = VB6.Format(System.Math.Abs(mBalance), "0.00") ''& CStr(IIf(mBalance >= 0, "Dr", "Cr"))
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColDrCr
            .Text = CStr(IIf(mBalance >= 0, "DR", "CR"))
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Private Sub DisplaySummTotal()

        On Error GoTo DisplayErr
        Dim mDrCr As Integer
        Dim mBal As Double
        Dim mAge1 As Double
        Dim mAge2 As Double
        Dim mAge3 As Double
        Dim mAge4 As Double
        Dim mAge5 As Double
        Dim cntRow As Integer
        Dim cntCol As Integer
        With SprdAgeing
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDrCr
                mDrCr = IIf(UCase(.Text) = "DR", 1, -1)
                .Col = ColBal
                If IsNumeric(.Text) Then
                    mBal = mBal + (CDbl(.Text) * mDrCr)
                End If
                .Col = ColBal
                If .Text <> "" Then
                    If IsNumeric(Mid(.Text, 1, Len(.Text) - 2)) Then
                        mAge1 = mAge1 + (CDbl(Mid(.Text, 1, Len(.Text) - 2)) * IIf(Mid(.Text, Len(.Text) - 1, Len(.Text)) = "DR", 1, -1))
                    End If
                End If
            Next
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            For cntCol = ColBal To .MaxCols
                .Col = cntCol
                .Text = New String("_", 254)
            Next
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            .Col = ColName
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBal
            .Text = MainClass.FormatRupees(System.Math.Abs(mBal))
            .Col = ColDrCr
            .Text = IIf(mBal >= 0, "DR", "CR")
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            For cntCol = ColBal To .MaxCols
                .Col = cntCol
                .Text = New String("=", 254)
            Next
        End With
        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGroup.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtGroup.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtGroup_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGroup.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtGroup.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtGroup.Text), "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_Category='G'") = False Then
            MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
            TxtGroup.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub ViewAccountLedger()

        On Error GoTo ErrPart
        If SprdAgeing.ActiveRow <= 0 Then Exit Sub
        frmViewLedger.lblBookType.Text = "LEDG"
        SprdAgeing.Row = SprdAgeing.ActiveRow
        SprdAgeing.Col = ColName
        If LTrim(RTrim(SprdAgeing.Text)) = "" Then Exit Sub
        frmViewLedger.cboAccount.Text = LTrim(RTrim(SprdAgeing.Text))
        MainClass.ValidateWithMasterTable(SprdAgeing.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        frmViewLedger.lblAcCode.Text = MasterNo
        If LTrim(RTrim(frmViewLedger.lblAcCode.Text)) = "" Then Exit Sub
        frmViewLedger.txtDateFrom.Text = RsCompany.Fields("Start_Date").Value
        frmViewLedger.txtDateTo.Text = txtDateTo.Text
        frmViewLedger.OptSumDet(2).Checked = True
        '    frmViewLedger.cboDivision.Text = cboDivision.Text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedger.MdiParent = Me.MdiParent
        frmViewLedger.Show()
        frmViewLedger.frmViewLedger_Activated(Nothing, New System.EventArgs())
        frmViewLedger.cmdShow_Click(Nothing, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub TxtGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroup.TextChanged
        PrintFlag = False
        PrintStatus()
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
End Class
