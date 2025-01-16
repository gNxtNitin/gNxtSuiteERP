Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Friend Class frmPrintMultiEntry
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection

    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If cboVoucher.SelectedIndex = -1 Then
            MsgBox("Please Select a Voucher ", MsgBoxStyle.Information)
            cboVoucher.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If optPrintRange(1).Checked = True Then
            If Trim(txtVNoFrom.Text) = "" Then
                MsgBox("Voucher No. Cann't be blank. ", MsgBoxStyle.Information)
                txtVNoFrom.Focus()
                FieldsVerification = False
                Exit Function
            End If

            If Trim(txtVNoTo.Text) = "" Then
                MsgBox("Voucher No. Cann't be blank. ", MsgBoxStyle.Information)
                txtVNoTo.Focus()
                FieldsVerification = False
                Exit Function
            End If

            If Trim(txtVNoFrom.Text) > Trim(txtVNoTo.Text) Then
                MsgBox(" 'Voucher No. To ' Cann't be Less Than 'Voucher No. From.' ", MsgBoxStyle.Information)
                txtVNoTo.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Select Case lblBookType.Text
            Case "M"
                Call ReportONMRR(Crystal.DestinationConstants.crptToWindow)
            Case "P"
                Call ReportonPO(Crystal.DestinationConstants.crptToWindow)
            Case "D"
                Call ReportOnDS(Crystal.DestinationConstants.crptToWindow)
        End Select
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnDS(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim pSqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Dim Response As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        pSqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = SelectQryForDS(pSqlStr)
        mTitle = "Delivery Schedule"
        mRptFileName = "DS.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False)

        Response = MsgQuestion("Do You Want to Print Detail Delivery Schedule?")

        If Response = CStr(MsgBoxResult.Yes) Then
            Call MainClass.ClearCRptFormulas(Report1)

            Call SelectQryForDailyDS(SqlStr)
            '        mTitle = "Shortage Follow-up register for the month of " & VB6.Format(txtScheduleDate, "MMMM , YYYY")
            mRptFileName = "DSDetail.rpt"

            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDailyDS(ByRef mSqlStr As String) As String

        Dim mPartyCode As String

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DAILY_SCHLD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                mPartyCode = MasterNo
                mSqlStr = mSqlStr & " AND IH.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        If optPrintRange(0).Checked = True Then
            mSqlStr = mSqlStr & " AND IH.DELV_SCHLD_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        Else
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_DELV BETWEEN " & Val(txtVNoFrom.Text) & " AND " & Val(txtVNoTo.Text) & ""
        End If

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_DATE"

        SelectQryForDailyDS = mSqlStr

    End Function

    Private Function SelectQryForDS(ByRef mSqlStr As String) As String

        Dim mPartyCode As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                mPartyCode = MasterNo
                mSqlStr = mSqlStr & " AND IH.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        If optPrintRange(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.DELV_SCHLD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.DELV_SCHLD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        Else
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_DELV BETWEEN " & Val(txtVNoFrom.Text) & " AND " & Val(txtVNoTo.Text) & ""
        End If

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY AUTO_KEY_DELV,ID.SERIAL_NO"

        SelectQryForDS = mSqlStr
    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Select Case lblBookType.Text
            Case "M"
                Call ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
            Case "P"
                Call ReportonPO(Crystal.DestinationConstants.crptToPrinter)
            Case "D"
                Call ReportOnDS(Crystal.DestinationConstants.crptToPrinter)
        End Select
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim pSqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Dim mPrintSubReport As Boolean

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        pSqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        pSqlStr = SelectQryForPO(pSqlStr)


        '
        If VB.Left(lblBookType.Text, 1) = "J" Then
            mTitle = "Job Work Order"
        ElseIf VB.Left(lblBookType.Text, 1) = "P" Then
            mTitle = "Purchase Order"
        ElseIf VB.Left(lblBookType.Text, 1) = "W" Then
            mTitle = "Work Order"
        End If

        If VB.Right(lblBookType.Text, 1) = "O" Then
            mSubTitle = "(OPEN)"
        ElseIf VB.Right(lblBookType.Text, 1) = "C" Then
            mSubTitle = "(CLOSE)"
        End If
        If RsCompany.Fields("COMPANY_CODE").Value = 5 Then
            mRptFileName = "PO_PRN_NEW.rpt"
        Else
            mRptFileName = "PO_PRN.rpt"
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 6 Then
            mRptFileName = "PO_PRN.rpt"
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 5 Then
            mRptFileName = "PO_PRN_NEW.rpt"
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("COMPANY_CODE").Value = 21 Or RsCompany.Fields("COMPANY_CODE").Value = 20 Then
            mRptFileName = "PO_PRN_UNIT16.rpt"
        Else
            mRptFileName = "PO_PRN_UNIT1.rpt"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, mPrintSubReport)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForPO(ByRef mSqlStr As String) As String

        Dim mPartyCode As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST,FIN_PAYTERM_MST PAYMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.PO_CLOSED='N'"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                mPartyCode = MasterNo
                mSqlStr = mSqlStr & " AND IH.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        If optPrintRange(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PUR_ORD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.PUR_ORD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        Else
            mSqlStr = mSqlStr & " AND IH.MKEY BETWEEN " & Val(txtVNoFrom.Text) & " AND " & Val(txtVNoTo.Text) & ""
        End If

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.MKEY,ID.SERIAL_NO"

        SelectQryForPO = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef pPrintSubReport As Boolean)
        'Dim Printer As New Printer

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        Dim mCurr As String
        Dim cntRow As Integer
        Dim mItemValue As Double
        Dim SqlStrSub As String

        If UCase(mRptFileName) = "PO_PRN_UNIT1.RPT" Then
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True)
        Else
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        End If

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        ''RsCompany.fields("COMPANY_CODE").value = 16 Or

        If lblBookType.Text = "M" Then

        Else
            If (RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("COMPANY_CODE").Value = 21) Then
                MainClass.AssignCRptFormulas(Report1, "TINNo=""" & IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & """")
                MainClass.AssignCRptFormulas(Report1, "ExciseRegnNo=""" & IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value) & """")
                MainClass.AssignCRptFormulas(Report1, "ECCNo=""" & IIf(IsDbNull(RsCompany.Fields("ECC_NO").Value), "", RsCompany.Fields("ECC_NO").Value) & """")
                MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDbNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
                MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDbNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
                MainClass.AssignCRptFormulas(Report1, "PANNo=""" & IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & """")
            End If
        End If

        If UCase(mRptFileName) = "PO_PRN_NEW.RPT" Then
            '        mAmountInword = MainClass.RupeesConversion(CDbl(mItemValue))
            '
            '        MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"
            '        MainClass.AssignCRptFormulas Report1, "NetAmount=""" & VB6.Format(mItemValue, "0.00") & """"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn

            Report1.SubreportToChange = ""
        End If

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
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdsearchVNO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchVNO.Click
        Dim Index As Short = cmdsearchVNO.GetIndex(eventSender)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mTable As String = ""
        Dim mFieldName1 As String = ""
        Dim mFieldName2 As String = ""
        Dim mCheckFY As String = ""

        If lblBookType.Text = "M" Then
            mTable = "INV_GATE_HDR"
            mFieldName1 = "AUTO_KEY_MRR"
            mFieldName2 = "SEND_AC_DATE"
            mCheckFY = "AUTO_KEY_MRR"
        ElseIf lblBookType.Text = "P" Then
            mTable = "PUR_PURCHASE_HDR"
            mFieldName1 = "MKEY"
            mFieldName2 = "PUR_ORD_DATE"
            mCheckFY = "AUTO_KEY_PO"
        ElseIf lblBookType.Text = "D" Then
            mTable = "PUR_DELV_SCHLD_HDR"
            mFieldName1 = "AUTO_KEY_DELV"
            mFieldName2 = "DELV_SCHLD_DATE"
            mCheckFY = "AUTO_KEY_DELV"
        End If

        SqlStr = SqlStr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(" & mCheckFY & ",LENGTH(" & mCheckFY & ")-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

        If MainClass.SearchGridMaster(IIf(Index = 0, txtVNoFrom.Text, txtVNoTo.Text), mTable, mFieldName1, mFieldName2, , , SqlStr) = True Then
            If Index = 0 Then
                txtVNoFrom.Text = AcName
            Else
                txtVNoTo.Text = AcName
            End If
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub


    Private Sub frmPrintMultiEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If lblBookType.Text = "M" Then
            cboVoucher.SelectedIndex = 1
            Me.Text = "Multiply MRR Printing"

            optPrintRange(2).Enabled = True
            optPrintRange(2).Visible = True

            optPrintRange(0).Text = "Send Date Range"
            FraDateRange.Text = "Send Date Range Wise"

            optPrintRange(2).Text = "MRR Date Range"
            FraDateRange.Text = "MRR Date Range Wise"

            optPrintRange(1).Text = "MRR Range"
            FraVNoRange.Text = "MRR No  Range Wise"

            FraSend.Enabled = True
            optSend(1).Checked = True

        ElseIf lblBookType.Text = "P" Then
            cboVoucher.SelectedIndex = 2
            Me.Text = "Multiply PO Printing"

            optPrintRange(2).Enabled = False
            optPrintRange(2).Visible = False

            optPrintRange(0).Text = "Date Range"
            FraDateRange.Text = "Date Range Wise"

            optPrintRange(1).Text = "PO Range"
            FraVNoRange.Text = "PO No  Range Wise"

            FraSend.Enabled = False
        ElseIf lblBookType.Text = "D" Then
            cboVoucher.SelectedIndex = 0
            Me.Text = "Multiply DS Printing"

            optPrintRange(2).Enabled = False
            optPrintRange(2).Visible = False

            optPrintRange(0).Text = "Date Range"
            FraDateRange.Text = "Date Range Wise"

            optPrintRange(1).Text = "DS Range"
            FraVNoRange.Text = "DS No  Range Wise"

            FraSend.Enabled = False
        End If
    End Sub

    Private Sub frmPrintMultiEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(4845)
        'Me.Width = VB6.TwipsToPixelsX(5685)


        cboVoucher.Items.Clear()

        cboVoucher.Items.Add("Delivery Schedule")
        cboVoucher.Items.Add("MRR")
        cboVoucher.Items.Add("Purchase Order")

        cboVoucher.SelectedIndex = 0

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub optPrintRange_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPrintRange.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPrintRange.GetIndex(eventSender)
            FraDateRange.Enabled = IIf(Index = 0 Or Index = 2, True, False)
            FraVNoRange.Enabled = IIf(Index = 1, True, False)
        End If
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Cancel = True : txtDateFrom.Focus() : GoTo EventExitSub
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then Cancel = True: txtDateFrom.SetFocus: Exit Sub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then Cancel = True : txtDateTo.Focus() : GoTo EventExitSub
        '    If FYChk(CDate(txtDateTo.Text)) = False Then Cancel = True: txtDateTo.SetFocus: Exit Sub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        '    Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub
    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        '    Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            txtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub
    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtSupplier.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Supplier in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVNoFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoFrom.DoubleClick
        Call cmdsearchVNO_Click(cmdsearchVNO.Item(0), New System.EventArgs())
    End Sub
    Private Sub txtVNoFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNoFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVNoFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVNoFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVNoFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearchVNO_Click(cmdsearchVNO.Item(0), New System.EventArgs())
    End Sub
    Private Sub txtVNoTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoTo.DoubleClick
        Call cmdsearchVNO_Click(cmdsearchVNO.Item(1), New System.EventArgs())
    End Sub
    Private Sub txtVNoTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNoTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVNoTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVNoTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVNoTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearchVNO_Click(cmdsearchVNO.Item(1), New System.EventArgs())
    End Sub
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim pSqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        pSqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = SelectQryForMRR(pSqlStr)


        mTitle = "Material Receipt Report"
        mSubTitle = ""
        mRptFileName = "MRR.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForMRR(ByVal mSqlStr As String) As String

        Dim mPartyCode As String

        ''SELECT CLAUSE...

        ''SELECT CLAUSE...				

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC, BCMST.*, PREBY.EMP_NAME"

        ''FROM CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, INV_ITEM_MST INVMST,PAY_EMPLOYEE_MST PREBY"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mSqlStr = mSqlStr & vbCrLf & ", INV_GENERAL_MST GMST"
        End If

        ''WHERE CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BCMST.LOCATION_ID" & vbCrLf _
            & " AND ID.COMPANY_CODE=PREBY.COMPANY_CODE(+)" & vbCrLf _
            & " AND ID.QC_EMP_CODE=PREBY.EMP_CODE(+)" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then

            mSqlStr = mSqlStr & vbCrLf _
               & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
               & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE AND GMST.GEN_TYPE='C'" & vbCrLf

        End If

        If optSend(2).Checked = True Then
            mSqlStr = mSqlStr & " And SEND_AC_FLAG='Y'"
        ElseIf optSend(1).Checked = True Then
            mSqlStr = mSqlStr & " AND SEND_AC_FLAG='N'"
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                mPartyCode = MasterNo
                mSqlStr = mSqlStr & " AND IH.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        If optPrintRange(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf _
                & " AND IH.SEND_AC_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.SEND_AC_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        ElseIf optPrintRange(1).Checked = True Then
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_MRR BETWEEN " & Val(txtVNoFrom.Text) & " AND " & Val(txtVNoTo.Text) & ""
        ElseIf optPrintRange(2).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf _
                & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If



        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_MRR,IH.MRR_DATE,ID.SERIAL_NO"

        SelectQryForMRR = mSqlStr
    End Function
End Class
