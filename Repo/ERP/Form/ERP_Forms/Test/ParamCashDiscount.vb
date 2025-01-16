Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamCashDiscount
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 12

    Private Const ColUnitName As Short = 1
    Private Const ColName As Short = 2
    Private Const ColBill As Short = 3
    Private Const ColDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColVDate As Short = 6
    Private Const ColInvoiceAmount As Short = 7
    Private Const ColTaxableAmount As Short = 8
    Private Const ColPaymentAmount As Short = 9
    Private Const ColBal As Short = 10
    Private Const ColDays As Short = 11
    Private Const ColMaxDays As Short = 12
    Private Const ColROI As Short = 13
    Private Const ColCDAmount As Short = 14
    Private Const ColBookType As Short = 15
    Private Const ColBookSubType As Short = 16
    Private Const ColGenerateCN As Short = 17
    Private Const ColCreditNoteNo As Short = 18
    Private Const ColCustomerCode As Short = 19
    Private Const ColMKEY As Short = 20

    Dim mClickProcess As Boolean

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer

    Private Sub cmdBillSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillSearch.Click
        BillSearch()
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
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

        'MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)

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

        AgeingInfo()
        DisplayTotal()

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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCompanyCodeStr As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyName As String = ""

        If MainClass.ChkIsdateF(txtFromDate) = False Then Exit Function

        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Exit Function
        End If

        If optParticulars.Checked = True Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                TxtAccount.Focus()
                MsgInformation("Please Select Account")
                Exit Function
            End If
        End If

        If optBill(0).Checked = True Then
            If optParticulars.Checked = True Then
                SqlStr = "SELECT DISTINCT BILLNO FROM FIN_INVOICE_HDR " & vbCrLf _
                    & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & mAccountCode & "'" & vbCrLf _
                    & " AND BILLNO='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'"

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
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
                If RsTemp.EOF = True Then
                    txtBillNo.Focus()
                    MsgInformation("Invaild Bill No")
                    Exit Function
                End If
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub frmParamCashDiscount_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamCashDiscount_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        PrintFlag = False
        txtFromDate.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)

        FormatSprdAgeing()
        FillHeading()

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

        CboShow.Items.Add("Cleared")
        CboShow.Items.Add("UnCleared")
        CboShow.Items.Add("Both")
        CboShow.SelectedIndex = 0

        PrintStatus()
        Call frmParamCashDiscount_Activated(eventSender, eventArgs)
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
    Private Sub AgeingInfo()

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mSuppCustCode As String
        Dim mAgeingDays As String
        Dim mSql As String
        Dim mSqlStr As String
        Dim mBillDate As String
        Dim RsTemp As ADODB.Recordset
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        If optParticulars.Checked = True Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
            End If
        End If

        If optParticulars.Checked = True And optBill(0).Checked = True Then
            mBillDate = ""


            SqlStr = " SELECT BILLDATE FROM FIN_POSTED_TRN " & vbCrLf _
                & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(UCase(mSuppCustCode))) & "'" & vbCrLf _
                & " AND BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'" & vbCrLf _
                & " AND TRNTYPE IN ('N','O', DECODE(BOOKTYPE,'J','',DECODE(BOOKTYPE,'B','','B'))) AND BOOKTYPE<>'O' "

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
                SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY BILLDATE "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value)
            End If

        End If

        mCompanyCodeStr = ""

        mSql = " Sum(AMOUNT*DECODE(DC,'D',1,-1))"
        mSqlStr = " TO_CHAR(ABS(SUM(AMOUNT*DECODE(DC,'D',1,-1))),'999999999.99')"

        SqlStr = "SELECT COMPANY_SHORTNAME, SUPP_CUST_NAME, BillNo, " & vbCrLf _
                & " BillDate, VNO, VDate, InvoiceValue, ItemValue, " & vbCrLf _
                & " Payment, Balance, BILL_DAYS, MAX_BILL_DAYS, "

        SqlStr = SqlStr & vbCrLf _
                & " NVL(( " & vbCrLf _
                & " SELECT CD_PER FROM FIN_CDSLAB_MST A" & vbCrLf _
                & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE = T.SUPP_CUST_CODE" & vbCrLf _
                & " AND WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) FROM FIN_CDSLAB_MST WHERE COMPANY_CODE=A.COMPANY_CODE" & vbCrLf _
                & " AND SUPP_CUST_CODE = A.SUPP_CUST_CODE" & vbCrLf _
                & " AND WEF <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
                & " AND CD_DAYS = (" & vbCrLf _
                & " SELECT MIN(CD_DAYS) FROM FIN_CDSLAB_MST WHERE COMPANY_CODE=A.COMPANY_CODE" & vbCrLf _
                & " AND CD_DAYS >= MAX_BILL_DAYS)" & vbCrLf _
                & " ),0) AS ROI, "

        SqlStr = SqlStr & vbCrLf _
                & " ItemValue * .01 * NVL(( " & vbCrLf _
                & " SELECT CD_PER FROM FIN_CDSLAB_MST A" & vbCrLf _
                & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE = T.SUPP_CUST_CODE" & vbCrLf _
                & " AND WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) FROM FIN_CDSLAB_MST WHERE COMPANY_CODE=A.COMPANY_CODE" & vbCrLf _
                & " AND SUPP_CUST_CODE = A.SUPP_CUST_CODE" & vbCrLf _
                & " AND WEF <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
                & " AND CD_DAYS = (" & vbCrLf _
                & " SELECT MIN(CD_DAYS) FROM FIN_CDSLAB_MST WHERE COMPANY_CODE=A.COMPANY_CODE" & vbCrLf _
                & " AND CD_DAYS >= MAX_BILL_DAYS)" & vbCrLf _
                & " ),0) AS CD_AMOUNT, "


        SqlStr = SqlStr & vbCrLf _
                & " BOOKTYPE, BOOKSUBTYPE, GENERATECN, CN_NO, SUPP_CUST_CODE, MKEY FROM ("


        SqlStr = SqlStr & vbCrLf _
            & " Select CC.COMPANY_SHORTNAME, ACM.SUPP_CUST_NAME,OUTS.BillNo As BillNo, " & vbCrLf _
            & " OUTS.BillDate As BillDate, " & vbCrLf _
            & " DECODE(OUTS.VNo,NULL,'',OUTS.VNo) as VNo," & vbCrLf _
            & " OUTS.VDate AS VDate,"

        SqlStr = SqlStr & vbCrLf _
            & " IH.NETVALUE As InvoiceValue, IH.ITEMVALUE As ItemValue, " & vbCrLf _
            & " CASE WHEN " & mSql & "<= 0 THEN " & vbCrLf _
            & " " & mSqlStr & " ELSE '' END as Payment,"

        SqlStr = SqlStr & vbCrLf _
            & " IH.NETVALUE - NVL((SELECT SUM(AMOUNT*DECODE(DC,'D',-1,1)) FROM FIN_POSTED_TRN A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=OUTS.COMPANY_CODE AND A.FYEAR=OUTS.FYEAR AND A.BILLNO=OUTS.BILLNO AND A.BILLDATE=OUTS.BILLDATE AND BILLNO<>VNO),0) AS Balance,"

        'ColDays
        SqlStr = SqlStr & vbCrLf _
            & " OUTS.VDate - OUTS.BillDate AS BILL_DAYS," & vbCrLf

        ''ColMaxDays
        SqlStr = SqlStr & vbCrLf _
            & "NVL((SELECT MAX(VDATE)-OUTS.BillDate FROM FIN_POSTED_TRN A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=OUTS.COMPANY_CODE AND A.FYEAR=OUTS.FYEAR AND A.BILLNO=OUTS.BILLNO AND A.BILLDATE=OUTS.BILLDATE AND BILLNO<>VNO),0) AS MAX_BILL_DAYS,"

        ''ColROI
        SqlStr = SqlStr & vbCrLf & "0 AS ROI, "

        ' , BOOKTYPE, BOOKSUBTYPE, GENERATECN, CN_NO, MKEY GENERATECN, CN_NO


        ''ColCDAmount
        SqlStr = SqlStr & vbCrLf & "0 AS CD_AMOUNT, "

        SqlStr = SqlStr & vbCrLf _
            & " OUTS.BOOKTYPE, OUTS.BOOKSUBTYPE, "

        ''ColGenerateCN
        SqlStr = SqlStr & vbCrLf & " '0' GENERATECN, "

        ''ColCreditNoteNo
        SqlStr = SqlStr & vbCrLf & "'' AS CN_NO, "

        SqlStr = SqlStr & vbCrLf & "  IH.SUPP_CUST_CODE, OUTS.MKEY"

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN OUTS, FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST CC "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE OUTS.COMPANY_CODE=CC.COMPANY_CODE"        ''OUTS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf _
            & " AND OUTS.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf _
            & " AND OUTS.AccountCode=IH.SUPP_CUST_CODE " & vbCrLf _
            & " AND OUTS.BILLNO=IH.BILLNO " & vbCrLf _
            & " AND OUTS.BILLDATE=IH.INVOICE_DATE "

        SqlStr = SqlStr & vbCrLf _
            & " AND OUTS.VNO<>IH.BILLNO "

        If optParticulars.Checked = True And optBill(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND OUTS.BILLDATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf _
                & " AND OUTS.BOOKTYPE<>'O' AND  OUTS.BOOKSUBTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND OUTS.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND OUTS.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND OUTS.AccountCode=ACM.SUPP_CUST_CODE "

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

        If OptAll.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        ElseIf optParticulars.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND  OUTS.ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(UCase(mSuppCustCode))) & "'"
        End If

        If optBill(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND OUTS.BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'"
        End If

        If optParticulars.Checked = True And optBill(0).Checked = True Then

        Else
            SqlStr = SqlStr & vbCrLf & " AND OUTS.VDate>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " AND OUTS.VDate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY ACM.SUPP_CUST_NAME, IH.SUPP_CUST_CODE, CC.COMPANY_SHORTNAME,OUTS.BillNo,OUTS.BillDate,OUTS.VDate, OUTS.COMPANY_CODE ,OUTS.FYEAR ," & vbCrLf _
            & " DECODE(OUTS.VNo,NULL,'',OUTS.VNo),OUTS.MKEY, OUTS.BOOKTYPE, OUTS.BOOKSUBTYPE, IH.NETVALUE,IH.ITEMVALUE "

        'SqlStr = SqlStr & vbCrLf _
        '    & " HAVING " & mSql & " <> 0 "

        If CboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf _
            & " HAVING IH.NETVALUE - NVL((SELECT SUM(AMOUNT*DECODE(DC,'D',-1,1)) FROM FIN_POSTED_TRN A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=OUTS.COMPANY_CODE AND A.FYEAR=OUTS.FYEAR AND A.BILLNO=OUTS.BILLNO AND A.BILLDATE=OUTS.BILLDATE AND BILLNO<>VNO),0) <= 0"
        ElseIf CboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf _
            & " HAVING IH.NETVALUE - NVL((SELECT SUM(AMOUNT*DECODE(DC,'D',-1,1)) FROM FIN_POSTED_TRN A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=OUTS.COMPANY_CODE AND A.FYEAR=OUTS.FYEAR AND A.BILLNO=OUTS.BILLNO AND A.BILLDATE=OUTS.BILLDATE AND BILLNO<>VNO),0) > 0"
        End If

        SqlStr = SqlStr & vbCrLf & " ) T"

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY SUPP_CUST_NAME,BillDate,BillNo,VDATE "




        MainClass.AssignDataInSprd8(SqlStr, SprdAgeing, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdAgeing()

        With SprdAgeing
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .MaxCols = ColMKEY

            .Col = ColUnitName
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColName, 10)

            .Col = ColName
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColName, 19)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted  '' MergeCellsSettings.flexMergeRestrictColumns

            .Col = ColBill
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            .set_ColWidth(ColBill, 12)
            .ColsFrozen = ColBill


            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColDate, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted


            .Col = ColVNo
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVNo, 12)

            .Col = ColVDate
            .set_ColWidth(ColVDate, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColInvoiceAmount
            .set_ColWidth(ColInvoiceAmount, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColTaxableAmount
            .set_ColWidth(ColTaxableAmount, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColGenerateCN
            .set_ColWidth(ColGenerateCN, 6)
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Col = ColCreditNoteNo
            .set_ColWidth(ColCreditNoteNo, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColPaymentAmount
            .set_ColWidth(ColPaymentAmount, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColBal
            .set_ColWidth(ColBal, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            '.Col = ColDrCr
            '.set_ColWidth(ColDrCr, 3)
            '.TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '.ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColDays
            .set_ColWidth(ColDays, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeNone

            .Col = ColMaxDays
            .set_ColWidth(ColDays, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColROI
            .set_ColWidth(ColROI, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColCDAmount
            .set_ColWidth(ColCDAmount, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            If optParticulars.Checked = True Then
                .Col = ColName
                .ColHidden = True
            Else
                .Col = ColName
                .ColHidden = False
            End If

            .Col = ColBookType
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookType, 8)
            .ColHidden = True

            .Col = ColBookSubType
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookSubType, 8)
            .ColHidden = True

            .Col = ColCustomerCode
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColCustomerCode, 8)
            .ColHidden = True

            .Col = ColMKEY
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColBill
            .ColHidden = False
            .Col = ColDate
            .ColHidden = False
            .Col = ColVDate
            .ColHidden = False

            MainClass.SetSpreadColor(SprdAgeing, -1)
            MainClass.ProtectCell(SprdAgeing, 1, .MaxRows, 1, .MaxCols)
            SprdAgeing.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FillHeading()
        With SprdAgeing
            .Row = 0

            .Col = ColUnitName
            .Text = "Unit Name"

            .Col = ColName
            .Text = "Account Name"

            .Col = ColBill
            .Text = "Bill No."

            .Col = ColDate
            .Text = "Bill Date"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColVDate
            .Text = "VDate"

            .Col = ColInvoiceAmount
            .Text = "Invoice Value"

            .Col = ColTaxableAmount
            .Text = "Taxable Amount"

            .Col = ColPaymentAmount
            .Text = "Payment Amount"

            .Col = ColDays
            .Text = "No of Days"

            .Col = ColMaxDays
            .Text = "Reciepts Days"

            .Col = ColBal
            .Text = "Balance Amount"

            .Col = ColROI
            .Text = "CD %"

            .Col = ColCDAmount
            .Text = "Cash Discount Amount"

            .Col = ColBookType
            .Text = "Book Type"

            .Col = ColBookSubType
            .Text = "Book Sub Type"

            .Col = ColTaxableAmount
            .Text = "Item Amount"

            .Col = ColGenerateCN
            .Text = "Generate Credit Note"

            .Col = ColCreditNoteNo
            .Text = "Credit Note No"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColMKEY
            .Text = "MKey"

        End With
    End Sub

    Private Sub frmParamCashDiscount_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdAgeing.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 150, mReFormWidth - 150, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 90, mReFormWidth - 90, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdAgeing, -1)
    End Sub

    Private Sub OptAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
            PrintFlag = False
            PrintStatus()
        End If
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


    Private Sub optParticulars_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optParticulars.CheckedChanged
        If eventSender.Checked Then
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
            PrintFlag = False
            PrintStatus()
        End If
    End Sub

    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub SprdAgeing_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdAgeing.DblClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        Dim mGetFY As Integer

        '    Call ViewAccountLedger
        With SprdAgeing
            .Row = .ActiveRow

            .Col = ColVDate
            xVDate = .Text

            .Col = ColMKEY
            xMKey = .Text

            .Col = ColVNo
            xVNo = .Text

            .Col = ColBookType
            xBookType = .Text

            .Col = ColBookSubType
            xBookSubType = .Text
        End With

        mGetFY = GetCurrentFYNo(PubDBCn, xVDate)

        If mGetFY <> RsCompany.Fields("FYEAR").Value Then
            MsgInformation("Not a current Year Voucher, So cann't be Open.")
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

        Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
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
        'lblAcCode.text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        SqlStr = SqlStr & "AND (SUPP_CUST_TYPE IN ('C','S','2'))"
        SqlStr = SqlStr & "ORDER BY SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF Then
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


        Dim mName As String
        Dim mBill As String
        Dim mDate As String
        Dim mVNo As String
        Dim mVdate As String
        Dim mDAmount As String
        Dim mCAmount As String
        Dim mBal As String
        Dim mDrCr As String
        Dim SqlStr As String
        Dim cntRow As Integer

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdAgeing

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColName
                If Trim(.Text) <> "" Then
                    mName = Trim(.Text)
                End If

                .Col = ColBill
                mBill = IIf(Trim(.Text) = "", ".", Trim(.Text))

                .Col = ColDate
                mDate = .Text

                .Col = ColVNo
                mVNo = Trim(.Text)

                .Col = ColVDate
                mVdate = .Text

                .Col = ColInvoiceAmount
                mDAmount = .Text

                .Col = ColPaymentAmount
                mCAmount = .Text

                .Col = ColBal
                mBal = .Text

                '.Col = ColDrCr
                'mDrCr = .Text

                SqlStr = "Insert into TEMP_PrintDummyData (UserID,SubRow,Field1," & vbCrLf _
                    & " Field2,Field3,Field4,Field5,Field6,Field7,Field8," & vbCrLf _
                    & " Field9) Values (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " " & cntRow & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(mName)) & "', " & vbCrLf _
                    & " '" & Trim(mBill) & "', " & vbCrLf _
                    & " '" & Trim(mDate) & "', " & vbCrLf _
                    & " '" & Trim(mVNo) & "', " & vbCrLf _
                    & " '" & Trim(mVdate) & "', " & vbCrLf _
                    & " '" & Trim(mDAmount) & "', " & vbCrLf _
                    & " '" & Trim(mCAmount) & "', " & vbCrLf _
                    & " '" & Trim(mBal) & "', " & vbCrLf _
                    & " '" & Trim(mDrCr) & "') "

                PubDBCn.Execute(SqlStr)

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
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & vbCrLf _
            & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " ORDER BY SUBROW,Field1"

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
    Private Sub txtFromDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDate.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub DisplayTotal()
        On Error GoTo DisplayErr
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mNextPartyName As String
        Dim mPartyName As String
        Dim mDAmount As Double
        Dim mCAmount As Double
        Dim mItemAmount As Double

        Dim mBillNo As String
        Dim mPreviousBillNo As String

        cntRow = 1
        mPreviousBillNo = ""
        With SprdAgeing
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColName
                mPartyName = .Text

                .Col = ColBill
                mBillNo = .Text



                If mBillNo <> mPreviousBillNo Then
                    .Col = ColInvoiceAmount
                    mDAmount = mDAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                    .Col = ColTaxableAmount
                    mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                End If

                .Col = ColPaymentAmount
                mCAmount = mCAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColBill
                mPreviousBillNo = .Text

                cntRow = cntRow + 1
                .Row = cntRow
                .Col = ColName
                mNextPartyName = .Text
                If mPartyName <> mNextPartyName Then
                    .Row = cntRow
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    For cntCol = ColInvoiceAmount To ColBal
                        .Col = cntCol
                        .Text = New String("_", 254)
                    Next

                    cntRow = cntRow + 1

                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    Call GridTotal(mDAmount, mCAmount, mItemAmount, cntRow - 1)

                    mDAmount = 0
                    mCAmount = 0

                    cntRow = cntRow + 1

                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    For cntCol = ColInvoiceAmount To ColBal
                        .Col = cntCol
                        .Text = New String("_", 254)
                    Next
                    cntRow = cntRow + 1
                End If
            Loop

            .MaxRows = .MaxRows + 1
            For cntCol = ColInvoiceAmount To ColBal
                .Row = .MaxRows
                .Col = cntCol
                .Text = New String("_", 254)
            Next

            .MaxRows = .MaxRows + 1
            Call GridTotal(mDAmount, mCAmount, mItemAmount, .MaxRows)

            .MaxRows = .MaxRows + 1
            For cntCol = ColInvoiceAmount To ColBal
                .Row = .MaxRows
                .Col = cntCol
                .Text = New String("_", 254)
            Next

        End With


        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        'Resume
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
        eventArgs.Cancel = Cancel 'txtFromDate
    End Sub
    Private Sub txtFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtFromDate) = False Then
            txtFromDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtFromDate.Text))) = False Then
            txtFromDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel '
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

        SprdAgeing.Col = ColVDate
        frmViewLedger.txtDateFrom.Text = txtFromDate.Text     ' RsCompany.Fields("START_DATE").Value
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


    Private Sub BillSearch()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""


        If optParticulars.Checked = True Then
            If TxtAccount.Text <> "" Then
                If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SqlStr = SqlStr & " ACCOUNTCODE='" & MasterNo & "'"
                End If
            End If
        End If

        SqlStr = IIf(SqlStr = "", "", SqlStr & " AND ") & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='S' "

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

    Private Sub GridTotal(ByRef mDAmount As Double, ByRef mCAmount As Double, ByRef mTaxableAmount As Double, ByRef mRow As Integer)

        With SprdAgeing
            .Row = mRow
            .Col = ColBill
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColInvoiceAmount
            .Text = VB6.Format(mDAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColTaxableAmount
            .Text = VB6.Format(mTaxableAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColPaymentAmount
            .Text = VB6.Format(mCAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBal
            .Text = VB6.Format(System.Math.Abs(mDAmount - mCAmount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            '.Col = ColDrCr
            '.Text = CStr(IIf((mDAmount - mCAmount) >= 0, "Dr", "Cr"))
            '.Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Private Sub CboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboShow.SelectedIndexChanged
        'PrintEnable = False
        'PrintCommand()
    End Sub

End Class
