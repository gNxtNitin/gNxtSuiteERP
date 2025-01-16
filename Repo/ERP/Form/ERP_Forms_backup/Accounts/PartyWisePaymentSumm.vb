Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinExplorerBar
Imports Infragistics.Win.UltraWinGrid
Imports System.Data.OleDb

Friend Class frmPartyWisePaymentSumm
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 12
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 1 + 1
    Private Const ColOpening As Short = 2 + 1
    Private Const ColBillAmountApr As Short = 3 + 1
    Private Const ColPaymentApr As Short = 4 + 1
    Private Const ColBillAmountMay As Short = 5 + 1
    Private Const ColPaymentMay As Short = 6 + 1
    Private Const ColBillAmountJun As Short = 7 + 1
    Private Const ColPaymentJun As Short = 8 + 1
    Private Const ColBillAmountJul As Short = 9 + 1
    Private Const ColPaymentJul As Short = 10 + 1
    Private Const ColBillAmountAug As Short = 11 + 1
    Private Const ColPaymentAug As Short = 12 + 1
    Private Const ColBillAmountSep As Short = 13 + 1
    Private Const ColPaymentSep As Short = 14 + 1
    Private Const ColBillAmountOct As Short = 15 + 1
    Private Const ColPaymentOct As Short = 16 + 1
    Private Const ColBillAmountNov As Short = 17 + 1
    Private Const ColPaymentNov As Short = 18 + 1
    Private Const ColBillAmountDec As Short = 19 + 1
    Private Const ColPaymentDec As Short = 20 + 1
    Private Const ColBillAmountJan As Short = 21 + 1
    Private Const ColPaymentJan As Short = 22 + 1
    Private Const ColBillAmountFeb As Short = 23 + 1
    Private Const ColPaymentFeb As Short = 24 + 1
    Private Const ColBillAmountMar As Short = 25 + 1
    Private Const ColPaymentMar As Short = 26 + 1
    Private Const ColPaymentBalance As Short = 27 + 1
    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer
    Dim mClickProcess As Boolean
    Private Sub cmdBillSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillSearch.Click
        BillSearch()
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForAgeingAnly(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        PrintFlag = False
        PrintStatus()
        MainClass.ClearGrid(SprdAgeing, RowHeight)
        If FieldsVerification() = False Then Exit Sub
        AgeingInfo()
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
        Dim RsTemp As ADODB.Recordset

        If MainClass.ChkIsdateF(txtFromDate) = False Then Exit Function

        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Exit Function
        End If
        'If optParticulars.Checked = True Then
        '    If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mAccountCode = MasterNo
        '    Else
        '        TxtAccount.Focus()
        '        MsgInformation("Please Select Account")
        '        Exit Function
        '    End If
        'End If
        'If optBill(0).Checked = True Then
        '    If optParticulars.Checked = True Then
        '        SqlStr = "SELECT DISTINCT BILLNO FROM FIN_POSTED_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'" & vbCrLf & " AND BILLNO='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'"
        '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        '        If RsTemp.EOF = True Then
        '            txtBillNo.Focus()
        '            MsgInformation("Invaild Bill No")
        '            Exit Function
        '        End If
        '    End If
        'End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub frmPartyWisePaymentSumm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmPartyWisePaymentSumm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError

        Dim SqlStr As String
        Dim Rs As ADODB.Recordset
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


        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, Rs, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If Rs.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While Rs.EOF = False
                lstCompanyName.Items.Add(Rs.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(Rs.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                Rs.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Call FillComboBox()

        PrintFlag = False
        txtFromDate.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = CStr(RunDate)
        FormatSprdAgeing()
        FillHeading()
        PrintStatus()
        Call frmPartyWisePaymentSumm_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()

        Try
            'Dim RS As ADODB.Recordset
            Dim SqlStr As String
            'Dim CntLst As Long

            Dim oledbCnn As OleDbConnection
            Dim oledbAdapter As OleDbDataAdapter
            Dim ds As New DataSet

            oledbCnn = New OleDbConnection(StrConn)

            '', SUPP_CUST_ADDR,  SUPP_CUST_CITY, SUPP_CUST_STATE

            SqlStr = "Select DISTINCT SUPP_CUST_NAME AS AccountName, SUPP_CUST_CODE AS AccountCode " & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') ORDER BY SUPP_CUST_NAME"

            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            cboAccount.DataSource = ds
            cboAccount.DataMember = ""

            Dim c As UltraGridColumn = Me.cboAccount.DisplayLayout.Bands(0).Columns.Add()
            With c
                .Key = "Selected"
                .Header.Caption = String.Empty
                .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
                .DataType = GetType(Boolean)
                .DataType = GetType(Boolean)
                .Header.VisiblePosition = 0
            End With
            cboAccount.CheckedListSettings.CheckStateMember = "Selected"
            cboAccount.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
            ' Set up the control to use a custom list delimiter 
            cboAccount.CheckedListSettings.ListSeparator = " , "
            ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
            cboAccount.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
            cboAccount.DisplayMember = "AccountName"
            cboAccount.ValueMember = "AccountCode"

            cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
            cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
            'cboAccount.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
            'cboAccount.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
            'cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"

            cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
            cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100
            'cboAccount.DisplayLayout.Bands(0).Columns(2).Width = 350
            'cboAccount.DisplayLayout.Bands(0).Columns(3).Width = 100
            'cboAccount.DisplayLayout.Bands(0).Columns(4).Width = 100

            cboAccount.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

            'cboCompany.Rows(0).Selected = True


            oledbAdapter.Dispose()
            oledbCnn.Close()

        Catch ex As Exception
        End Try
    End Sub
    Private Sub cboAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboAccount.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            ElseIf e.KeyCode = Keys.Down Then
                cboAccount.PerformAction(UltraComboAction.Dropdown)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFromDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDate.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If MainClass.ChkIsdateF(txtFromDate) = False Then
            txtFromDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        'If FYChk(CStr(CDate(txtFromDate.Text))) = False Then
        '    txtFromDate.Focus()
        '    Cancel = True
        '    GoTo EventExitSub
        'End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub AgeingInfo()

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim mSuppCustCode As String = ""
        Dim mAgeingDays As String = ""
        Dim mSql As String = ""
        Dim mSqlStr As String = ""
        Dim mBillDate As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mCompanyName As String = ""
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        'If optParticulars.Checked = True Then
        '    If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mSuppCustCode = MasterNo
        '    End If
        'End If

        mSuppCustCode = ""
        If cboAccount.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboAccount.CheckedRows
                If mSuppCustCode <> "" Then
                    mSuppCustCode += "," & "'" & r.Cells("AccountCode").Value.ToString() & "'"
                Else
                    mSuppCustCode += "'" & r.Cells("AccountCode").Value.ToString() & "'"
                End If
            Next
            'mSuppCustCodeNew = mSuppCustCodeNew + ","
        End If

        If optBill(0).Checked = True Then
            mBillDate = ""
            SqlStr = " SELECT BILLDATE FROM FIN_POSTED_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

            If mSuppCustCode <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE IN (" & mSuppCustCode & ")"
            End If

            SqlStr = SqlStr & vbCrLf _
                & " AND BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'" & vbCrLf _
                & " AND TRNTYPE IN ('N','O', DECODE(BOOKTYPE,'J','',DECODE(BOOKTYPE,'B','','B'))) AND BOOKTYPE<>'O' ORDER BY BILLDATE "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value)
            End If
        End If

        mSql = "AMOUNT*DECODE(DC,'D',-1,1)" '" Sum(AMOUNT*DECODE(DC,'D',1,-1))"

        '
        SqlStr = "SELECT ACM.SUPP_CUST_CODE AS Code, ACM.SUPP_CUST_NAME AS Name, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE='O' THEN " & mSql & " ELSE 0 END) AS OPENING, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='04' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_APR, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='04' THEN " & mSql & " ELSE 0 END) AS PAYMENT_APR, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='05' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_MAY, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='05' THEN " & mSql & " ELSE 0 END) AS PAYMENT_MAY, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='06' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_JUN, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='06' THEN " & mSql & " ELSE 0 END) AS PAYMENT_JUN, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='07' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_JUL, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='07' THEN " & mSql & " ELSE 0 END) AS PAYMENT_JUL ," & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='08' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_AUG, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='08' THEN " & mSql & " ELSE 0 END) AS PAYMENT_AUG, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='09' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_SEP, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='09' THEN " & mSql & " ELSE 0 END) AS PAYMENT_SEP, "

        SqlStr = SqlStr & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='10' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_OCT, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='10' THEN " & mSql & " ELSE 0 END) AS PAYMENT_OCT, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='11' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_NOV, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='11' THEN " & mSql & " ELSE 0 END) AS PAYMENT_NOV, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='12' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_DEC, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='12' THEN " & mSql & " ELSE 0 END) AS PAYMENT_DEC, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='01' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_JAN, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='01' THEN " & mSql & " ELSE 0 END) AS PAYMENT_JAN, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='02' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_FEB, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='02' THEN " & mSql & " ELSE 0 END) AS PAYMENT_FEB, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('E','R','P','S','U','L') AND TO_CHAR(OUTS.VDATE,'MM')='03' THEN " & mSql & " ELSE 0 END) AS TRANSACTION_MAR, " & vbCrLf _
            & "  SUM(CASE WHEN OUTS.BOOKTYPE IN ('J','B','C') AND TO_CHAR(OUTS.VDATE,'MM')='03' THEN " & mSql & " ELSE 0 END) AS PAYMENT_MAR, " & vbCrLf _
            & "  SUM(" & mSql & ")  AS PAYMENT_BAL "

        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN OUTS,FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST CC "

        SqlStr = SqlStr & vbCrLf & " WHERE OUTS.COMPANY_CODE=CC.COMPANY_CODE "

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If optBill(0).Checked = True Then
            'SqlStr = SqlStr & vbCrLf & " AND OUTS.BILLDATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND OUTS.BOOKTYPE<>'O' AND  OUTS.BOOKSUBTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND OUTS.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        End If

        SqlStr = SqlStr & vbCrLf & " AND OUTS.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND OUTS.AccountCode=ACM.SUPP_CUST_CODE "

        'If OptAll.Checked = True Then
        SqlStr = SqlStr & vbCrLf & "AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        'ElseIf optParticulars.Checked = True Then
        '    SqlStr = SqlStr & vbCrLf & " AND  OUTS.ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(UCase(mSuppCustCode))) & "'"
        'End If


        If mSuppCustCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND  OUTS.ACCOUNTCODE IN (" & mSuppCustCode & ")"
        End If

        If optBill(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND OUTS.BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'"
        End If

        If optBill(0).Checked = True Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND OUTS.VDate>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND OUTS.VDate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME " & vbCrLf _
            & " ORDER BY ACM.SUPP_CUST_NAME"

        MainClass.AssignDataInSprd8(SqlStr, SprdAgeing, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdAgeing()

        Dim CntCol As Integer
        With SprdAgeing
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .MaxCols = ColPaymentBalance
            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColCode, 8)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColName, 35)

            For CntCol = ColOpening To ColPaymentBalance
                .Col = CntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(CntCol, 13)
            Next
            .ColsFrozen = ColName
            MainClass.SetSpreadColor(SprdAgeing, -1)
            MainClass.ProtectCell(SprdAgeing, 1, .MaxRows, 1, .MaxCols)
            SprdAgeing.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FillHeading()
        With SprdAgeing
            .Row = 0
            .Col = ColCode
            .Text = "Supplier / Customer Code"

            .Col = ColName
            .Text = "Supplier / Customer Name"

            .Col = ColOpening
            .Text = "Opening Balance"
            .Col = ColBillAmountApr
            .Text = "Bill Amount - April"
            .Col = ColPaymentApr
            .Text = "Payment - April"
            .Col = ColBillAmountMay
            .Text = "Bill Amount - May"
            .Col = ColPaymentMay
            .Text = "Payment - May"
            .Col = ColBillAmountJun
            .Text = "Bill Amount - June"
            .Col = ColPaymentJun
            .Text = "Payment - June"
            .Col = ColBillAmountJul
            .Text = "Bill Amount - July"
            .Col = ColPaymentJul
            .Text = "Payment - July"
            .Col = ColBillAmountAug
            .Text = "Bill Amount - August"
            .Col = ColPaymentAug
            .Text = "Payment - August"
            .Col = ColBillAmountSep
            .Text = "Bill Amount - Sep"
            .Col = ColPaymentSep
            .Text = "Payment - Sep"
            .Col = ColBillAmountOct
            .Text = "Bill Amount - Oct"
            .Col = ColPaymentOct
            .Text = "Payment - Oct"
            .Col = ColBillAmountNov
            .Text = "Bill Amount - Nov"
            .Col = ColPaymentNov
            .Text = "Payment - Nov"
            .Col = ColBillAmountDec
            .Text = "Bill Amount - Dec"
            .Col = ColPaymentDec
            .Text = "Payment - Dec"
            .Col = ColBillAmountJan
            .Text = "Bill Amount - Jan"
            .Col = ColPaymentJan
            .Text = "Payment - Jan"
            .Col = ColBillAmountFeb
            .Text = "Bill Amount - Feb"
            .Col = ColPaymentFeb
            .Text = "Payment - Feb"
            .Col = ColBillAmountMar
            .Text = "Bill Amount - March"
            .Col = ColPaymentMar
            .Text = "Payment - March"
            .Col = ColPaymentBalance
            .Text = "Payment - Balance"
        End With
    End Sub
    Private Sub frmPartyWisePaymentSumm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdAgeing.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 150, mReFormWidth - 150, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 90, mReFormWidth - 90, mReFormWidth))
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdAgeing, -1)
    End Sub
    Private Sub optBill_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBill.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBill.GetIndex(eventSender)
            txtBillNo.Enabled = IIf(Index = 1, False, True)
            cmdBillSearch.Enabled = IIf(Index = 1, False, True)
            PrintFlag = False
            PrintStatus()
        End If
    End Sub
    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
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
        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        Call FillPrintDummy()
        '''''Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mSubTitle = "From : " & VB6.Format(txtFromDate.Text, "DD MMM, YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mRPTName = "BillOutstanding.Rpt"
        mTitle = "Bill Wise Outstanding"
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
        Dim mName As String = ""
        Dim mBill As String = ""
        Dim mDate As String = ""
        Dim mVNo As String = ""
        Dim mVDate As String = ""
        Dim mDAmount As String = ""
        Dim mCAmount As String = ""
        Dim mBal As String = ""
        Dim mDrCr As String = ""
        Dim SqlStr As String = ""
        Dim cntRow As Integer = ""
        '    On Error GoTo ERR1
        '
        '    PubDBCn.Errors.Clear
        '
        '    PubDBCn.BeginTrans
        '
        '    SqlStr = ""
        '    With SprdAgeing
        '
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '
        '            .Col = ColName
        '            If Trim(.Text) <> "" Then
        '                mName = Trim(.Text)
        '            End If
        '
        '            .Col = ColBill
        '            mBill = IIf(Trim(.Text) = "", ".", Trim(.Text))
        '
        '            .Col = ColDate
        '            mDate = .Text
        '
        '            .Col = ColVNo
        '            mVNo = Trim(.Text)
        '
        '            .Col = ColVDate
        '            mVDate = .Text
        '
        '            .Col = ColDebitAmount
        '            mDAmount = .Text
        '
        '            .Col = ColCreditAmount
        '            mCAmount = .Text
        '
        '            .Col = ColBal
        '            mBal = .Text
        '
        '            .Col = ColDrCr
        '            mDrCr = .Text
        '
        '
        '            SqlStr = "Insert into TEMP_PrintDummyData (UserID,SubRow,Field1," & vbCrLf _
        ''                & " Field2,Field3,Field4,Field5,Field6,Field7,Field8," & vbCrLf _
        ''                & " Field9) Values (" & vbCrLf _
        ''                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                & " " & cntRow & ", " & vbCrLf _
        ''                & " '" & MainClass.AllowSingleQuote(Trim(mName)) & "', " & vbCrLf _
        ''                & " '" & Trim(mBill) & "', " & vbCrLf _
        ''                & " '" & Trim(mDate) & "', " & vbCrLf _
        ''                & " '" & Trim(mVNo) & "', " & vbCrLf _
        ''                & " '" & Trim(mVDate) & "', " & vbCrLf _
        ''                & " '" & Trim(mDAmount) & "', " & vbCrLf _
        ''                & " '" & Trim(mCAmount) & "', " & vbCrLf _
        ''                & " '" & Trim(mBal) & "', " & vbCrLf _
        ''                & " '" & Trim(mDrCr) & "') "
        '
        '            PubDBCn.Execute SqlStr
        '
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
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW,Field1"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtBillNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.DoubleClick
        BillSearch()
    End Sub
    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBillNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then BillSearch()
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
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
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub BillSearch()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mSuppCustCode As String

        SqlStr = ""
        'If optParticulars.Checked = True Then
        '    If TxtAccount.Text <> "" Then
        '        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            SqlStr = SqlStr & " ACCOUNTCODE='" & MasterNo & "'"
        '        End If
        '    End If
        'End If

        mSuppCustCode = ""
        If cboAccount.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboAccount.CheckedRows
                If mSuppCustCode <> "" Then
                    mSuppCustCode += "," & "'" & r.Cells("AccountCode").Value.ToString() & "'"
                Else
                    mSuppCustCode += "'" & r.Cells("AccountCode").Value.ToString() & "'"
                End If
            Next
            SqlStr = SqlStr & " ACCOUNTCODE IN (" & mSuppCustCode & ")"
        End If

        SqlStr = IIf(SqlStr = "", "", SqlStr & " AND ") & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "  "

        If MainClass.SearchGridMaster((txtBillNo.Text), "FIN_POSTED_TRN", "BILLNO", "BILLDATE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtBillNo.Text = AcName
            End If
        End If


        'MainClass.SearchMaster(txtBillNo.Text, "FIN_POSTED_TRN", "BILLNO", SqlStr)
        'If AcName <> "" Then
        '    txtBillNo.Text = AcName
        'End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
End Class
