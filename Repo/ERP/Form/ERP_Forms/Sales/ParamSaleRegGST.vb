Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSaleRegGST
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Dim cntSearchRow As Integer

    Private Const ColLocked As Short = 1
    Private Const ColChallanDate As Short = 2
    Private Const ColChallanNo As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5

    Private Const ColPartyCode As Short = 6
    Private Const ColPartyName As Short = 7

    Private Const ColOurSONo As Short = 8
    Private Const ColOurSODate As Short = 9

    Private Const ColPONo As Short = 10
    Private Const ColPODate As Short = 11

    Private Const ColPartyGSTN As Short = 12
    Private Const ColDesc As Short = 13
    Private Const ColDescUOM As Short = 14
    Private Const ColBillQty As Short = 15
    Private Const ColBillAmount As Short = 16
    Private Const ColTaxableAmount As Short = 17
    Private Const ColItemAmount As Short = 18
    Private Const ColCGSTAmount As Short = 19
    Private Const ColSGSTAmount As Short = 20
    Private Const ColIGSTAmount As Short = 21
    'Private Const ColeWayBillNo = 17
    'Private Const ColeWayBillDate = 18


    Dim ColMKEY As Integer
    Dim ColCancelled As Integer

    Dim mClickProcess As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT1.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT1.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDuty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDuty.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDuty_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDuty.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDutyForGone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDutyForGone.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExport_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExport.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboMRP_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMRP.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkShowTariff_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowTariff.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSale(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSale(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForSale(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Sales Register"
        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SALESREG.RPT"

        SqlStr = MakeSQL("P")

        'If MainClass.FillPrintDummyDataFromSprd(SprdMain, 0, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr
        'SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        FillHeading()
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleRegGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sale Register (All Taxes)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleRegGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)
        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboRejection.Items.Clear()
        cboCancelled.Items.Clear()
        cboCT3.Items.Clear()
        cboShow.Items.Clear()
        cboLocation.Items.Clear()
        cboMRP.Items.Clear()
        cboDutyForGone.Items.Clear()
        cboCT1.Items.Clear()
        cboDuty.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboCT3.Items.Add("BOTH")
        cboCT3.Items.Add("YES")
        cboCT3.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboRejection.Items.Add("BOTH")
        cboRejection.Items.Add("YES")
        cboRejection.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboExport.Items.Add("BOTH")
        cboExport.Items.Add("YES")
        cboExport.Items.Add("NO")

        cboCT1.Items.Add("BOTH")
        cboCT1.Items.Add("YES")
        cboCT1.Items.Add("NO")

        cboDuty.Items.Add("BOTH")
        cboDuty.Items.Add("YES")
        cboDuty.Items.Add("NO")

        cboShow.Items.Add("All")
        cboShow.Items.Add("Only Excise")
        cboShow.Items.Add("Only Service Tax")
        cboShow.Items.Add("Only Cess")
        cboShow.Items.Add("Only W/o Excise")
        cboShow.Items.Add("Only W/o Service Tax")
        cboShow.Items.Add("Only W/o Cess")
        cboShow.Items.Add("Only GST")
        cboShow.Items.Add("Only W/o GST")

        cboDutyForGone.Items.Add("All")
        cboDutyForGone.Items.Add("Only Duty Foregone")
        cboDutyForGone.Items.Add("Only W/o Duty Foregone")


        cboMRP.Items.Add("BOTH")
        cboMRP.Items.Add("YES")
        cboMRP.Items.Add("NO")

        cboMRP.SelectedIndex = 0
        cboAgtD3.SelectedIndex = 0
        cboCT3.SelectedIndex = 0
        cboCT1.SelectedIndex = 0
        cboDuty.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboRejection.SelectedIndex = 0
        cboCancelled.SelectedIndex = 2
        cboExport.SelectedIndex = 0
        cboShow.SelectedIndex = 0
        cboDutyForGone.SelectedIndex = 0

        cboInvoiceType.Items.Clear()
        cboInvoiceType.Items.Add("All")
        cboInvoiceType.Items.Add("1. Tax Invoice")
        cboInvoiceType.Items.Add("2. Jobwork Invoice")
        cboInvoiceType.Items.Add("3. Delivery Challan")
        cboInvoiceType.Items.Add("4. Service / Rental")
        cboInvoiceType.Items.Add("5. Delivery Challan Supp. Invoice (Internal Memo)")
        cboInvoiceType.Items.Add("6. Tax Invoice Export")
        '    cboInvoiceType.AddItem "7. Reverse Charge - Goods"
        '    cboInvoiceType.AddItem "8. Reverse Charge - Services"
        cboInvoiceType.Items.Add("9. Supplementary Invoice")
        cboInvoiceType.Items.Add("0. Bill of Supply")
        cboInvoiceType.SelectedIndex = 0

        Call FillInvoiceType()
        FillHeading()
        '    MainClass.FillCombo cboInvoiceType, "FIN_INVTYPE_MST", "NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'"

        optType(2).Checked = True

        '    cboInvoiceType.ListIndex = 0
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim SqlStr As String = ""
        Dim mRecordCount As Integer
        Dim mName As String

        MainClass.ClearGrid(SprdMain)
        MainClass.ClearGrid(SprdHeading)

        With SprdMain
            .MaxCols = ColIGSTAmount
            mRecordCount = 0


            SqlStr = " SELECT NAME,CODE FROM FIN_INTERFACE_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND TYPE IN ('B','S')" & vbCrLf & " AND STATUS='O'"

            If PubGSTApplicable = True Then
                SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
            End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY PRINTSEQUENCE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            mRecordCount = 0
            If RsTemp.EOF = False Then
                mRecordCount = 1
                Do While Not RsTemp.EOF

                    SprdHeading.Row = mRecordCount
                    SprdHeading.Col = 1
                    SprdHeading.Text = RsTemp.Fields("Code").Value
                    SprdHeading.Col = 2
                    SprdHeading.Text = RsTemp.Fields("Name").Value
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mRecordCount = mRecordCount + 1
                        SprdHeading.MaxRows = SprdHeading.MaxRows + 1
                    End If

                Loop
            End If

            .Row = 0
            ColMKEY = .MaxCols + mRecordCount + 4
            ColCancelled = ColMKEY + 1
            .MaxCols = ColCancelled

            If mRecordCount > 0 Then
                For cntCol = 1 To mRecordCount
                    SprdHeading.Row = cntCol
                    SprdHeading.Col = 2
                    mName = Trim(SprdHeading.Text)
                    .Col = ColIGSTAmount + cntCol
                    .Text = mName
                Next
            End If

            .Col = ColMKEY - 3
            .Text = "Vechile No"

            .Col = ColMKEY - 2
            .Text = "e-Way Bill No"

            .Col = ColMKEY - 1
            .Text = "e-Way Bill Date"

            .Col = ColMKEY
            .Text = "Mkey"

            .Col = ColCancelled
            .Text = "Cancelled"

            FormatSprdMain(-1)
        End With
    End Sub

    Private Function FillHeadingQry() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String = ""
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean

        SqlStr = " SELECT DISTINCT IMST.NAME ,IMST.PRINTSEQUENCE " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_EXP EXP, FIN_SUPP_CUST_MST CMST, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.MKEY=EXP.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"


        lblAcCode.Text = "-1"
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If

            SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        mShowAll = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                SqlStr = SqlStr & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
            End If
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            '        SqlStr = SqlStr & vbCrLf & "AND IH.AGTD3='" & vb.Left(cboAgtD3.Text, 1) & "'"
            If cboAgtD3.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If

        If cboCT3.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboCT1.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.AGTCT1='" & VB.Left(cboCT1.Text, 1) & "'"
        End If

        If cboDuty.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.ISDUTY_FORGONE='" & VB.Left(cboDuty.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboMRP.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TAX_ON_MRP='Y'"
        ElseIf cboMRP.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TAX_ON_MRP='N'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If chkShowTariff.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TARIFFHEADING IS NULL"
        Else
            If Trim(txtTariffHeading.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
            End If
        End If

        If cboExport.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboLocation.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.DESP_LOCATION='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If

        '    If optForGone.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & "AND IH.DESP_LOCATION='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        '    End If

        '    If cboDutyForGone.ListIndex = 1 Then
        '        SqlStr = SqlStr & vbCrLf & "AND EXP.DUTYFORGONE='Y'"
        '    ElseIf cboDutyForGone.ListIndex = 2 Then
        '        SqlStr = SqlStr & vbCrLf & "AND EXP.DUTYFORGONE='N'"
        '    End If

        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TOTEDAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TOTSERVICEAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TOTEDUAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TOTEDAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 5 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TOTSERVICEAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 6 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.TOTEDUAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 7 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT)>0"
        ElseIf cboShow.SelectedIndex = 8 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT)=0"
        End If

        SqlStr = SqlStr & vbCrLf & "AND EXP.AMOUNT<>0"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IMST.PRINTSEQUENCE"

        FillHeadingQry = SqlStr
        Exit Function
ErrPart:
        FillHeadingQry = ""
    End Function
    Private Sub frmParamSaleRegGST_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleRegGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
        '    lstInvoiceType.ToolTipText = lstInvoiceType.Text
    End Sub

    Private Sub lstInvoiceType_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lstInvoiceType.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        ToolTip1.SetToolTip(lstInvoiceType, lstInvoiceType.Text)
    End Sub


    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStr1 As String
        Dim mStr2 As String
        Dim mStr As String
        'Dim cntSearchRow As Long
        'Dim mSearchKey As String
        '
        '    cntSearchRow = 1
        '    If eventArgs.row = 0 And eventArgs.col = ColBillNo Then
        '        mSearchKey = ""
        '        mSearchKey = InputBox("Enter Bill No :", "Search", mSearchKey)
        '        MainClass.SearchIntoGrid SprdMain, ColBillNo, mSearchKey, cntSearchRow
        '        cntSearchRow = cntSearchRow + 1
        '        SprdMain.SetFocus
        '    End If

        If eventArgs.row = 0 Then Exit Sub

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColMKEY
        mMKey = SprdMain.Text

        SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR ='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND MKEY='" & mMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mStr1 = IIf(IsDbNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            mStr2 = IIf(IsDbNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
            mStr1 = IIf(mStr1 = "", "", "Vehicle No : " & mStr1)
            mStr2 = IIf(mStr2 = "", "", "Carriers : " & mStr2)
            mStr = mStr1 & IIf(mStr2 = "", "", IIf(mStr1 = "", "", ",") & mStr2)

            ToolTip1.SetToolTip(SprdMain, mStr)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent


        Dim mSearchKey As String
        Dim mCol As Integer

        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            cntSearchRow = 1
            mSearchKey = ""
            mSearchKey = InputBox("Enter Search String :", "Search", mSearchKey)
            If mSearchKey <> "" Then
                MainClass.SearchIntoGrid(SprdMain, mCol, mSearchKey, cntSearchRow)
                cntSearchRow = cntSearchRow + 1
            End If
            SprdMain.Focus()
        End If
    End Sub

    Private Sub SprdMain_RightClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles SprdMain.RightClick
        'Dim SqlStr As String=""
        'Dim mMkey As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mStr1 As String
        'Dim mStr2 As String
        'Dim mStr As String
        '
        '    SprdMain.Row = Row
        '    SprdMain.Col = ColMKEY
        '    mMkey = SprdMain.Text
        '
        '    SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf _
        ''            & " FROM FIN_INVOICE_HDR " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND FYEAR =" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''            & " AND MKEY='" & mMkey & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mStr1 = IIf(IsNull(RsTemp!VEHICLENO), "", RsTemp!VEHICLENO)
        '        mStr2 = IIf(IsNull(RsTemp!CARRIERS), "", RsTemp!CARRIERS)
        '        mStr1 = IIf(mStr1 = "", "", "Vehicle No : " & mStr1)
        '        mStr2 = IIf(mStr2 = "", "", "Carriers : " & mStr2)
        '        mStr = mStr1 & IIf(mStr2 = "", "", "," & mStr2)
        '
        '        SprdMain.ToolTipText = mStr
        '    End If
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub txtTariffHeading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariffHeading.TextChanged
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
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColCancelled
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColChallanDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChallanDate, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColChallanNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChallanNo, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)


            '        If OptSumDet(0).Value = True Then
            '            .ColHidden = False
            '        Else
            '            .ColHidden = True
            '        End If

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyCode, 6)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            '        If OptSumDet(0).Value = True Then
            '            .ColHidden = False
            '            .ColsFrozen = ColAcctName
            '        Else
            '            .ColHidden = True
            '        End If

            .Col = ColOurSODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOurSODate, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColOurSONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOurSONo, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPODate, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPONo, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColPartyGSTN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyGSTN, 15)

            .ColsFrozen = ColBillNo


            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesc, 15)

            .Col = ColDescUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDescUOM, 6)

            For cntCol = ColBillQty To ColMKEY - 4
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                '            .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next



            .Col = ColMKEY - 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY - 3, 12)
            .ColHidden = False

            .Col = ColMKEY - 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY - 2, 12)
            .ColHidden = False


            .Col = ColMKEY - 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY - 1, 12)
            .ColHidden = False


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColCancelled
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCancelled, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            '        SprdMain.OperationMode = OperationModeNormal
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim mFieldTitle As String
        Dim mMKey As String
        Dim mValue As Double
        'Dim mTotValue As Double
        Dim mCancelled As String

        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mGetFieldName As String
        Dim mGetFieldValue As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL("")
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        FormatSprdMain(-1)
        ''    With SprdMain
        ''        For cntRow = 1 To .MaxRows
        ''
        ''            .Row = cntRow
        ''            .Col = ColMKEY
        ''            mMKey = Trim(.Text)
        ''
        ''            .Col = ColCancelled
        ''            mCancelled = Trim(.Text)
        ''
        ''            If Left(mCancelled, 1) = "N" Then
        ''
        ''                pSqlStr = "SELECT EXP.AMOUNT, IMST.NAME " & vbCrLf _
        '                        & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf _
        '                        & " WHERE EXP.MKEY='" & mMKey & "'" & vbCrLf _
        '                        & " AND IMST.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        '                        & " AND EXP.EXPCODE=IMST.CODE"
        ''
        ''                If PubGSTApplicable = True Then
        ''                    pSqlStr = pSqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        ''                Else
        ''                    pSqlStr = pSqlStr & vbCrLf & " AND GST_ENABLED='N'"
        ''                End If
        ''
        ''                MainClass.UOpenRecordSet pSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        ''
        ''                Do While RsTemp.EOF = False
        ''                    mGetFieldName = IIf(IsNull(RsTemp!Name), "", RsTemp!Name)
        ''                    mGetFieldValue = IIf(IsNull(RsTemp!Amount), 0, RsTemp!Amount)
        ''
        ''                    For cntCol = ColIGSTAmount + 1 To ColMKEY - 3
        ''                        .Row = 0
        ''                        .Col = cntCol
        ''                        mFieldTitle = Trim(.Text)
        ''
        ''                        If UCase(Trim(mFieldTitle)) = UCase(Trim(mGetFieldName)) Then
        ''                            .Row = cntRow
        ''                            .Col = cntCol
        ''                            .Text = Format(mGetFieldValue, "0.00")
        ''                            Exit For
        ''                        End If
        ''                    Next
        ''                    RsTemp.MoveNext
        ''                Loop
        ''            End If
        ''        Next
        ''    End With

        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            For cntCol = ColSaleAmount + 1 To ColMKEY - 1
        '                .Row = 0
        '                .Col = cntCol
        '                mFieldTitle = Trim(.Text)
        '
        '                .Row = cntRow
        '                .Col = ColMKEY
        '                mMkey = Trim(.Text)
        '
        '                .Col = ColCancelled
        '                mCancelled = Trim(.Text)
        '
        '                mValue = GetExpenseAmount(mFieldTitle, mMkey, mCancelled)
        '                .Row = cntRow
        '                .Col = cntCol
        '                .Text = Format(mValue, "0.00")
        '
        '            Next
        '
        '        Next
        '    End With
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetExpenseAmount(ByRef pFieldTitle As String, ByRef pMKey As String, ByRef pCancelled As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetExpenseAmount = 0

        If pCancelled = "Y" Then
            Exit Function
        End If


        SqlStr = "SELECT EXP.AMOUNT " & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE EXP.MKEY='" & pMKey & "'" & vbCrLf & " AND IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE" & vbCrLf & " AND IMST.NAME='" & MainClass.AllowSingleQuote(pFieldTitle) & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        If cboDutyForGone.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND EXP.DUTYFORGONE='Y'"
        ElseIf cboDutyForGone.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND EXP.DUTYFORGONE='N'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetExpenseAmount = IIf(IsDbNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
        End If
        Exit Function
LedgError:
        GetExpenseAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL(pType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String = ""
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean
        ''SELECT CLAUSE...
        Dim cntCol As Integer
        Dim mStr As String
        Dim mDivision As Double
        Dim mHeadRow As Integer
        Dim mHeadCode As Double
        Dim mFieldName As String
        Dim mFieldValue As String
        'Dim mFieldNameStr As String
        mHeadRow = 1


        mFieldName = ""
        mFieldValue = ""
        mStr = ""
        mHeadRow = 1
        If pType = "" Then
            For cntCol = ColIGSTAmount + 1 To ColMKEY - 4
                SprdHeading.Row = mHeadRow
                SprdHeading.Col = 1
                mHeadCode = Val(SprdHeading.Text)
                mFieldName = "FIELD" & mHeadRow
                mStr = mStr & IIf(mStr = "", "", ",")
                If lblBookType.Text = "D" Then
                    mStr = mStr & vbCrLf _
                        & "( SELECT DECODE(IH.CANCELLED,'Y',0,AMOUNT) AS " & "FIELD" & mHeadRow & " " & vbCrLf _
                        & " FROM FIN_INVOICE_EXP EXP" & vbCrLf _
                        & " WHERE EXP.MKEY=IH.MKEY AND EXPCODE=" & mHeadCode & "" & vbCrLf _
                        & " ) AS " & "FIELD" & mHeadRow & ""
                Else
                    mStr = mStr & vbCrLf _
                        & "( SELECT SUM(DECODE(IH.CANCELLED,'Y',0,AMOUNT)) AS " & "FIELD" & mHeadRow & " " & vbCrLf _
                        & " FROM FIN_INVOICE_EXP EXP" & vbCrLf _
                        & " WHERE EXP.MKEY=IH.MKEY AND EXPCODE=" & mHeadCode & "" & vbCrLf _
                        & " ) AS " & "FIELD" & mHeadRow & ""
                End If
                mHeadRow = mHeadRow + 1
            Next
        End If


        MakeSQL = " SELECT "

        If lblBookType.Text = "D" Then
            MakeSQL = MakeSQL & vbCrLf & " '', TO_CHAR(IH.DCDATE,'DD/MM/YYYY') AS DCDATE, IH.AUTO_KEY_DESP, " & vbCrLf _
                & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE,   "
        Else
            MakeSQL = MakeSQL & vbCrLf & " '', '', '', " & vbCrLf & " '', '',  "
        End If

        MakeSQL = MakeSQL & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, "

        If lblBookType.Text = "D" Then
            MakeSQL = MakeSQL & vbCrLf & " OUR_AUTO_KEY_SO,OUR_SO_DATE, CUST_PO_NO, CUST_PO_DATE, "
        Else
            MakeSQL = MakeSQL & vbCrLf & " '', '', '','', "
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " CMST.GST_RGN_NO, IH.ITEMDESC, '' AS UOM, "

        If lblBookType.Text = "D" Then
            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTQTY)), TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETVALUE)), " & vbCrLf _
                & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTTAXABLEAMOUNT)), TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE)), " & vbCrLf _
                & " TO_CHAR(CASE WHEN CANCELLED = 'Y' OR IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IH.NETCGST_AMOUNT END), " & vbCrLf _
                & " TO_CHAR(CASE WHEN CANCELLED = 'Y' OR IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IH.NETSGST_AMOUNT END)," & vbCrLf _
                & " TO_CHAR(CASE WHEN CANCELLED = 'Y' OR IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IH.NETIGST_AMOUNT END), " & mStr
        Else
            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTQTY))), TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.NETVALUE))), " & vbCrLf _
                & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTTAXABLEAMOUNT))),  TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE))), " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN CANCELLED = 'Y' OR IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IH.NETCGST_AMOUNT END)), " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN CANCELLED = 'Y' OR IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IH.NETSGST_AMOUNT END)), " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN CANCELLED = 'Y' OR IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IH.NETIGST_AMOUNT END)), " & mStr
        End If

        If lblBookType.Text = "D" Then
            MakeSQL = MakeSQL & vbCrLf & " , VEHICLENO ,E_BILLWAYNO, TO_CHAR(E_BILLWAYDATE,'DD-MM-YYYY HH24:MI') AS E_BILLWAYDATE,IH.MKEY,IH.CANCELLED"
        Else
            MakeSQL = MakeSQL & vbCrLf & " ,'','','','',''"
        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST" '', FIN_TARRIF_MST ID"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVMST.CODE " ''AND IH.COMPANY_CODE=ID.COMPANY_CODE(+) AND IH.TARIFFHEADING=ID.TARRIF_CODE(+)"


        lblAcCode.Text = "-1"
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If

            MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If optType(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If

        mShowAll = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        mDivision = -1
        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
            End If
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            '        MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTD3='" & vb.Left(cboAgtD3.Text, 1) & "'"
            If cboAgtD3.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If



        ''

        If cboCT3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboCT1.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT1='" & VB.Left(cboCT1.Text, 1) & "'"
        End If

        If cboDuty.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISDUTY_FORGONE='" & VB.Left(cboDuty.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboMRP.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TAX_ON_MRP='Y'"
        ElseIf cboMRP.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TAX_ON_MRP='N'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If chkShowTariff.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING IS NULL"
        Else
            If Trim(txtTariffHeading.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
            End If
        End If

        If cboExport.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboLocation.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DESP_LOCATION='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If

        If cboInvoiceType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.INVOICESEQTYPE='" & VB.Left(cboInvoiceType.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND IH.INVOICESEQTYPE NOT IN (7,8)"

        '    If cboDutyForGone.ListIndex = 1 Then
        '        SqlStr = SqlStr & vbCrLf & "AND EXP.DUTYFORGONE='Y'"
        '    ElseIf cboDutyForGone.ListIndex = 2 Then
        '        SqlStr = SqlStr & vbCrLf & "AND EXP.DUTYFORGONE='N'"
        '    End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTSERVICEAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDUAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 5 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTSERVICEAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 6 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDUAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 7 Then
            MakeSQL = MakeSQL & vbCrLf & "AND (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT)>0"
        ElseIf cboShow.SelectedIndex = 8 Then
            MakeSQL = MakeSQL & vbCrLf & "AND (IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT)=0"
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD/MM/YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD/MM/YYYY')"


        ''ORDER CLAUSE...
        If lblBookType.Text = "S" Then
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY " & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME,CMST.GST_RGN_NO "

            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME"

        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.BILLNO, IH.INVOICE_DATE"
        End If



        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
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
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mTotValue As Double

        Call MainClass.AddBlankfpSprdRow(SprdMain, ColPartyName)
        With SprdMain
            .Col = ColPartyName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False


            For cntCol = ColBillAmount To ColMKEY - 4
                mTotValue = 0
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = cntCol
                    mTotValue = mTotValue + Val(.Text)
                Next
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotValue, "0.00")
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""
        'Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMkey = Me.SprdMain.Text

        SprdMain.Col = ColBillNo
        xVNo = Me.SprdMain.Text

        'Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "")

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookCode As String
        Dim MyVnoPrefix As String
        Dim mBillSeq As Long
        Dim mAutoBillNo As Double

        SqlStr = " SELECT BILLNOPREFIX, BILLNOSEQ, BOOKCODE, INVOICESEQTYPE FROM FIN_INVOICE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BILLNO='" & xVNo & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mBookCode = IIf(IsDBNull(RsTemp.Fields("BOOKCODE").Value), "-2", RsTemp.Fields("BOOKCODE").Value)
            MyVnoPrefix = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "S", RsTemp.Fields("BILLNOPREFIX").Value)
            mBillSeq = IIf(IsDBNull(RsTemp.Fields("INVOICESEQTYPE").Value), "", RsTemp.Fields("INVOICESEQTYPE").Value)
            mAutoBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), 0, RsTemp.Fields("BILLNOSEQ").Value)
        Else
            Exit Sub
        End If
        
        FrmInvoiceGST.MdiParent = Me.MdiParent
        FrmInvoiceGST.LblBookCode.Text = mBookCode
        FrmInvoiceGST.lblInvoiceSeq.Text = mBillSeq
        FrmInvoiceGST.Show()
        FrmInvoiceGST.FrmInvoiceGST_Activated(Nothing, New System.EventArgs())
        FrmInvoiceGST.txtBillNoPrefix.Text = MyVnoPrefix
        FrmInvoiceGST.txtBillNo.Text = mAutoBillNo
        FrmInvoiceGST.txtBillNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))


    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub txtTariffHeading_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariffHeading.DoubleClick
        SearchTariff()
    End Sub

    Private Sub txtTariffHeading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariffHeading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTariffHeading_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariffHeading.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub

    Private Sub txtTariffHeading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariffHeading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtTariffHeading.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTariffHeading.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTariffHeading.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariffHeading.Text = AcName
            '        txtTariff_Validate False
            If txtTariffHeading.Enabled = True Then txtTariffHeading.Focus()
        End If


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' AND IDENTIFICATION NOT IN ('G','S') ORDER BY NAME"
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


        SqlStr = "SELECT DISTINCT DESP_LOCATION FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " ORDER BY DESP_LOCATION"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboLocation.Items.Clear()
        cboLocation.Items.Add("All")

        Do While RS.EOF = False
            cboLocation.Items.Add(IIf(IsDbNull(RS.Fields("DESP_LOCATION").Value), "", RS.Fields("DESP_LOCATION").Value))
            RS.MoveNext()
        Loop

        cboLocation.SelectedIndex = 0
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
End Class
