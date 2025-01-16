Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamAcctVsParty
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 24

    Private Const ColLocked As Short = 1
    Private Const ColAccountName As Short = 2
    Private Const ColBookType As Short = 3
    Private Const ColBookSubType As Short = 4
    Private Const ColVDate As Short = 5
    Private Const ColVNo As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColAmount As Short = 8
    Private Const ColMKEY As Short = 9

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mClickProcess As Boolean
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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

    Private Sub chkAllParty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllParty.CheckStateChanged
        Call PrintStatus(False)
        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPartyName.Enabled = False
            cmdPartySearch.Enabled = False
        Else
            txtPartyName.Enabled = True
            cmdPartySearch.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'"
        If MainClass.SearchGridMaster((TxtAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtAccount.Text = AcName
            txtAccount_Validating(txtAccount, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchParty()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If MainClass.SearchGridMaster((txtPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchAccount()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE NOT IN ('S','C','1','2')"
        If MainClass.SearchGridMaster((txtPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtAccount.Text = AcName
            txtAccount_Validating(txtAccount, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPartySearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPartySearch.Click
        Call SearchParty()
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        LedgInfo()
        CalcSubTotal()
        FormatSprdLedg()
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
    End Sub
    Private Sub CalcSubTotal()

        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mPartyName As String
        Dim StartRow As Integer
        Dim EndRow As Integer
        Dim mAmount As Double


        Call MainClass.AddBlankfpSprdRow(SprdLedg, ColPartyName)
        With SprdLedg
            .Row = .MaxRows
            .Col = ColPartyName
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "GRAND TOTAL"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmount
                mAmount = mAmount + Val(.Text)
            Next

            .Row = .MaxRows
            .Col = ColAmount
            .Text = VB6.Format(mAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

        End With

    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        '    If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE='O'") = True Then
        '        mAccountCode = MasterNo
        '    Else
        '        MsgInformation "Please Select Account"
        '        Exit Function
        '    End If


        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Select Account Name.")
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE NOT IN ('S','C')") = False Then
                MsgInformation("Invalid Account Name.")
                Exit Function
            Else
                mAccountCode = MasterNo
            End If
        End If

        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPartyName.Text) = "" Then
                MsgInformation("Please Select Party Name.")
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
                MsgInformation("Invalid Party Name.")
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Public Sub frmParamAcctVsParty_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamAcctVsParty_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked
        txtPartyName.Enabled = False
        cmdPartySearch.Enabled = False

        chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        Call FillInvoiceType()

        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)

        Call frmParamAcctVsParty_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer
        Dim mCompanyName As String
        Dim mCompanyAdd As String

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' ORDER BY NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

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

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LedgInfo()

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim I As Integer


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")
        Call FormatSprdLedg()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mAccountCode As String
        Dim CntLst As Integer
        Dim mAccountCodeStr As String
        Dim mInvoiceType As String
        Dim mPartyAdd As String
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String


        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                mAccountCodeStr = "'" & Trim(mAccountCode) & "'"
            End If
        Else
            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        mAccountCodeStr = "(" & mAccountCodeStr & ")"

        If InsertIntoTemp() = False Then GoTo ERR1

        ''
        'SqlStr = SqlStr & vbCrLf _
        '        & " NVL((SELECT LISTAGG(CASE WHEN MOVE_TYPE='O' THEN 'OD' WHEN MOVE_TYPE='P' THEN 'SHORT LEAVE' ELSE 'MANUAL' END, ', ') WITHIN GROUP (ORDER BY TIME_FROM) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y'),'') AS REMARKS"


        If optPartyWise.Checked = True Then
            If OptShow(0).Checked = True Then
                SqlStr = " SELECT DISTINCT '',ACM.SUPP_CUST_NAME, TRN.BOOKTYPE, TRN.BOOKSUBTYPE, TRN.VDATE, " & vbCrLf _
                    & " TRN.VNO, "

                'SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_NAME,"

                'SqlStr = SqlStr & vbCrLf _
                '        & " (SELECT DISTINCT LISTAGG(CMST.SUPP_CUST_NAME, ', ') WITHIN GROUP (ORDER BY CMST.SUPP_CUST_NAME) FROM FIN_POSTED_TRN STRN, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                '        & " WHERE STRN.COMPANY_CODE = CMST.COMPANY_CODE" & vbCrLf _
                '        & " And STRN.ACCOUNTCODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                '        & " AND BOOKTYPE=TRN.BOOKTYPE And MKEY=TRN.MKEY" & vbCrLf _
                '        & " AND CMST.SUPP_CUST_TYPE IN ('C','S') AND CMST.INTER_UNIT='N'" & vbCrLf _
                '        & " ) AS SUPP_CUST_NAME,"


                SqlStr = SqlStr & vbCrLf _
                        & " GETLEDGERCUSTOMERHEAD(TRN.BOOKTYPE, TRN.MKEY, TRN.VNO) AS SUPP_CUST_NAME,"

                SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(DECODE(TB.VDC,'D',1,-1) * TB.VAMOUNT) AS AMOUNT," & vbCrLf _
                    & " TRN.MKEY"
            Else
                SqlStr = " SELECT '', ACM.SUPP_CUST_NAME,'', '', '', " & vbCrLf _
                    & " '', "

                ''CMST.SUPP_CUST_NAME,"

                'SqlStr = SqlStr & vbCrLf _
                '        & " NVL((SELECT LISTAGG(CMST.SUPP_CUST_NAME, ', ') WITHIN GROUP (ORDER BY MKEY) FROM FIN_POSTED_TRN STRN, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                '        & " WHERE STRN.COMPANY_CODE = CMST.COMPANY_CODE" & vbCrLf _
                '        & " And STRN.ACCOUNTCODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                '        & " AND BOOKTYPE=TRN.BOOKTYPE And MKEY=TRN.MKEY" & vbCrLf _
                '        & " AND CMST.SUPP_CUST_TYPE IN ('C','S') AND CMST.INTER_UNIT='N'" & vbCrLf _
                '        & " ),'') AS SUPP_CUST_NAME,"

                SqlStr = SqlStr & vbCrLf _
                        & " GETLEDGERCUSTOMERHEAD(TRN.BOOKTYPE, TRN.MKEY, TRN.VNO) AS SUPP_CUST_NAME,"


                SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(DISTINCT DECODE(TB.VDC,'D',1,-1) * TB.VAMOUNT)) AS AMOUNT," & vbCrLf _
                    & " ''"
            End If
        Else
            If OptShow(0).Checked = True Then
                SqlStr = " SELECT DISTINCT '',ACM.SUPP_CUST_NAME, TRN.BOOKTYPE, TRN.BOOKSUBTYPE, TRN.VDATE, " & vbCrLf _
                    & " TRN.VNO, CMST.SUPP_CUST_NAME," & vbCrLf _
                    & " TO_CHAR(DECODE(TRN.DC,'C',1,-1) * TRN.AMOUNT) AS AMOUNT," & vbCrLf _
                    & " TRN.MKEY"
            Else
                SqlStr = " SELECT '', ACM.SUPP_CUST_NAME,'', '', '', " & vbCrLf _
                    & " '', CMST.SUPP_CUST_NAME," & vbCrLf _
                    & " TO_CHAR(SUM(DECODE(TRN.DC,'C',1,-1) * TRN.AMOUNT)) AS AMOUNT," & vbCrLf _
                    & " ''"
            End If
        End If

        If optPartyWise.Checked = True Then
            SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, TEMP_VIEWBOOK TB, FIN_SUPP_CUST_MST ACM" ' ''
        Else
            SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, TEMP_VIEWBOOK TB, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST ACM" ' ''
        End If


        SqlStr = SqlStr & vbCrLf _
            & " WHERE TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.MKEY=TB.MKEY" & vbCrLf _
            & " AND TRN.BOOKTYPE=TB.BOOKTYPE" & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
            & " AND TB.VACCOUNTCODE=ACM.SUPP_CUST_CODE"

        If optPartyWise.Checked = True Then

        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND TRN.ACCOUNTCODE=CMST.SUPP_CUST_CODE"
        End If

        ''TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "
        If optPartyWise.Checked = True Then
            'SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_TYPE IN ('C','S') AND CMST.INTER_UNIT='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.BOOKTYPE NOT IN ('B')"
        End If

        'If lstCompanyName.GetItemChecked(0) = True Then
        '    mCompanyCodeStr = ""
        'Else
        '    For CntLst = 1 To lstCompanyName.Items.Count - 1
        '        If lstCompanyName.GetItemChecked(CntLst) = True Then
        '            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
        '            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '            End If
        '            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
        '        End If

        '    Next
        'End If

        'If mCompanyCodeStr <> "" Then
        '    mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        'End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE NOT IN " & mAccountCodeStr & ""

        SqlStr = SqlStr & vbCrLf & " AND TB.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        Dim mCustomerCode As String = "-1"

        If optPartyWise.Checked = True Then
            If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustomerCode = MasterNo
                End If

                SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"
            End If
        Else
            If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtPartyName.Text) & "'"
            End If
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.VDATE, TRN.VNO "
        Else
            If optPartyWise.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME,GETLEDGERCUSTOMERHEAD(TRN.BOOKTYPE, TRN.MKEY, TRN.VNO) "
            Else
                SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME "
            End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME "
        End If
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Function InsertIntoTemp() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mPartyCode As String

        Dim mAccountCode As String
        Dim CntLst As Integer
        Dim mAccountCodeStr As String
        Dim mInvoiceType As String

        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        InsertIntoTemp = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                mAccountCodeStr = "'" & Trim(mAccountCode) & "'"
            End If
        Else
            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        mAccountCodeStr = "(" & mAccountCodeStr & ")"




        SqlStr = "DELETE FROM TEMP_VIEWBOOK NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "INSERT INTO TEMP_VIEWBOOK (" & vbCrLf & " USERID, BOOKTYPE, MKEY,VAMOUNT, VDC,VACCOUNTCODE) "

        'TRN.AMOUNT, TRN.DC

        SqlStr = SqlStr & vbCrLf _
            & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', TRN.BOOKTYPE, TRN.MKEY, TRN.AMOUNT,TRN.DC,TRN.ACCOUNTCODE" & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
            & " WHERE TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


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

        mAccountCode = "-1"
        mPartyCode = "-1"

        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE IN " & mAccountCodeStr & ""


        '    If chkAllAccount.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '            mPartyCode = MasterNo
        ''            SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        '        End If
        '    End If
        '
        '   SqlStr = SqlStr & vbCrLf & " AND (TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' OR TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "')"

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        InsertIntoTemp = True

        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertIntoTemp = False
        PubDBCn.RollbackTrans()
    End Function

    Private Sub FormatSprdLedg()

        Dim cntCol As Integer

        With SprdLedg
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColLocked, 1)
            .ColHidden = True

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookType, 1)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookSubType, 1)
            .ColHidden = True

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVDate, 9)
            .ColHidden = IIf(OptShow(0).Checked = True, False, True)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVNo, 12)
            .ColHidden = IIf(OptShow(0).Checked = True, False, True)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAccountName, 35)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 35)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColAmount, 15)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True


            Call FillHeading()

            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub frmParamAcctVsParty_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdLedg.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdLedg, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamAcctVsParty_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
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
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = True Then
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



        '''''Select Record for print...

        SqlStr = ""
        If MainClass.FillPrintDummyDataFromSprd(SprdLedg, 1, SprdLedg.MaxRows, 1, SprdLedg.MaxCols, PubDBCn) = False Then GoTo ERR1
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Service Tax Register (" & TxtAccount.Text & ")"
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        mReportFileName = "ST_Reg.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        '    SqlStr = "DELETE FROM TEmp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        '    PubDBCn.Execute SqlStr

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
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
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

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub FillHeading()

        With SprdLedg
            .Row = 0
            .Col = ColLocked
            .Text = "Locked"

            .Col = ColBookType
            .Text = "Book Type"

            .Col = ColBookSubType
            .Text = "Book SubType"

            .Col = ColVDate
            .Text = "Date"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColAccountName
            .Text = "Account Name"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColAmount
            .Text = "Amount"

            .Col = ColMKEY
            .Text = "Mkey"

        End With

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

    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        SearchParty()
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchParty()
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        On Error GoTo ERR1
        If txtPartyName.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtPartyName.Text = MasterNo
        Else
            MsgInformation("No Such Party Name in Account Master")
            Cancel = True
        End If
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
End Class
