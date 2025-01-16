Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmPrintMultiVouch
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

    Private Sub cboVoucher_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVoucher.SelectedIndexChanged
        Select Case cboVoucher.Text
            'Case "Cash Receipt"
            '    lblBookType.Text = ConCashReceipt
            'Case "Cash Payment"
            '    lblBookType.Text = ConCashPayment
            'Case "Bank Receipt"
            '    lblBookType.Text = ConBankReceipt
            'Case "Bank Payment"
            '    lblBookType.Text = ConBankPayment
            ''Case "Contra Entry"
            ''    lblBookType.Text = ConContra
            'Case "PDC Receipt"
            '    lblBookType.Text = ConPDCReceipt
            'Case "PDC Payment"
            '    lblBookType.Text = ConPDCPayment
            Case "Journal"
                lblBookType.Text = ConJournal
            Case "1. Purchase Entry (Goods Order)"
                lblBookType.Text = ConPurchase
            Case "2. Purchase Entry (Sale Return) Agt Debit Note"
                lblBookType.Text = ConPurchase
            Case "3. Purchase Entry (Ship)"
                lblBookType.Text = ConPurchase
            Case "4. Purchase Entry (Jobwork Order)"
                lblBookType.Text = ConPurchase
            Case "5. Purchase Entry (Repair)"
                lblBookType.Text = ConPurchase
            Case "6. Purchase Entry (Work Order)"
                lblBookType.Text = ConPurchase
            Case "7. Purchase Entry (Service)"
                lblBookType.Text = ConPurchase
            Case "8. Purchase Entry (Sale Return) Agt Invoice"
                lblBookType.Text = ConPurchase
            Case "9. Purchase Entry (Under Challan)"
                lblBookType.Text = ConPurchase
            'Case "General Purchase"
            '    lblBookType.Text = ConPurchaseGen
            Case "Debit Note"
                lblBookType.Text = ConDebitNote
            Case "Credit Note"
                lblBookType.Text = ConCreditNote
                'Case "Sale"
                '    lblBookType.Text = ConSale
                'Case "Customer Debit Note"
                '    lblBookType.Text = ConSaleDebit
                'Case "Sale Return"
                '    lblBookType.Text = ConPurchase
        End Select




    End Sub


    Private Sub cmdeMail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeMail.Click

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConDebitNote Or lblBookType.Text = ConCreditNote Then
            Call ReportOnDrCr(False, "", "M")
        ElseIf lblBookType.Text = ConPurchase Then
            Call ReportOnSaleReturn("M")
        Else
            Call ReportOnTrnVoucher("M")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConDebitNote Or lblBookType.Text = ConCreditNote Then
            Call ReportOnDrCr(False, "", "V")
        ElseIf lblBookType.Text = ConSaleDebit Then
            Call ReportOnCustomerDebit("V")
        ElseIf lblBookType.Text = ConPurchase Then
            Call ReportOnSaleReturn("V")
        Else
            Call ReportOnTrnVoucher("V")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnCustomerDebit(ByRef pMode As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSqlStr As String
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset
        Dim mKey As String

        Dim mRptFileName As String
        Dim mVNo As String
        Dim mPrintOption As String
        Dim cntRow As Integer
        Dim mOriginialInvNo As String
        Dim mCheckOriginialInvNo As String
        Dim Mode As Crystal.DestinationConstants
        Dim mCustCode As String
        Dim pNetValue As Double

        '    If chkCancelled.Value = vbChecked Then					
        '        MsgInformation "Cancelled Invoice Cann't be Print."					
        '        Exit Sub					
        '    End If					

        If pMode = "M" Then
            Mode = Crystal.DestinationConstants.crptToWindow
        ElseIf pMode = "P" Then
            Mode = Crystal.DestinationConstants.crptToPrinter
        Else
            Mode = Crystal.DestinationConstants.crptToWindow
        End If


        Sqlstr = " SELECT DISTINCT MKEY,REASON,SUPP_CUST_CODE,NETVALUE " & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(ConSaleDebit, 1) & "' AND CANCELLED='N'"

        If optPrintRange(0).Checked = True Then
            Sqlstr = Sqlstr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mKey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)
                mCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                pNetValue = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mTitle = "Credit Note"
                mRptFileName = "Cust_Sale.rpt"
                mSubTitle = IIf(IsDBNull(RsTemp.Fields("reason").Value), 1, RsTemp.Fields("reason").Value)
                If mSubTitle = "1" Then
                    mSubTitle = "Rate Diff"
                ElseIf mSubTitle = "2" Then
                    mSubTitle = "Shortage"
                Else
                    mSubTitle = "Others"
                End If

                mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "

                ''''FROM CLAUSE...					
                mSqlStr = mSqlStr & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, FIN_SUPP_CUST_MST CMST " ' & vbCrLf |					

                ''''WHERE CLAUSE...					
                mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & mKey & "'" & vbCrLf & " AND IH.BOOKTYPE='" & VB.Left(ConSaleDebit, 1) & "'"

                ''''ORDER CLAUSE...					

                mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"

                Report1.Reset()
                MainClass.ClearCRptFormulas(Report1)



                Call ShowCustomerDebitReport(mSqlStr, mKey, mCustCode, pNetValue, Mode, mTitle, mSubTitle, mRptFileName, True)

                RsTemp.MoveNext()
            Loop
        End If
        Exit Sub

ERR1:

        MsgInformation(Err.Description)
    End Sub
    Private Sub ReportOnSaleReturn(ByRef pMode As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSqlStr As String
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset
        Dim mKey As String

        Dim mRptFileName As String
        Dim mVNo As String
        Dim mPrintOption As String
        Dim cntRow As Integer
        Dim mOriginialInvNo As String
        Dim mCheckOriginialInvNo As String
        Dim Mode As Crystal.DestinationConstants
        Dim mCustCode As String
        Dim pNetValue As Double
        Dim mRejection As String
        Dim mBookSubType As String
        Dim mLocation As String


        If pMode = "M" Then
            Mode = Crystal.DestinationConstants.crptToWindow
        ElseIf pMode = "P" Then
            Mode = Crystal.DestinationConstants.crptToPrinter
        Else
            Mode = Crystal.DestinationConstants.crptToWindow
        End If


        mSqlStr = " SELECT " & vbCrLf _
            & " IH.*, ID.*, CMST.SUPP_CUST_NAME "

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST , FIN_SUPP_CUST_BUSINESS_MST BMST, GEN_COMPANY_MST GMST" ' & vbCrLf |

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                        & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
                        & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID" & vbCrLf _
                        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                        & " AND IH.ISFINALPOST='Y'"

        mSqlStr = mSqlStr & vbCrLf & " AND PURCHASESEQTYPE='" & Mid(cboVoucher.Text, 1, 1) & "'"

        If optPartyName(1).Checked = True Then
            mCustCode = ""
            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustCode = MasterNo
            End If

            mSqlStr = mSqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"

        End If

        If optPrintRange(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf _
                & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            mSqlStr = mSqlStr & vbCrLf _
                & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf _
                & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.MKEY,ID.SUBROWNO"


        mRptFileName = "PurchaseGST.rpt"

        If Mid(cboVoucher.Text, 1, 1) = 2 Then
            mTitle = "Credit Note (Sale Rejection)"
            mSubTitle = "Sale Return" ''IIf(IsNull(RsTemp!REASON), 1, RsTemp!REASON)			
        Else
            mTitle = "Purchase Voucher"
            mSubTitle = "" ''IIf(IsNull(RsTemp!REASON), 1, RsTemp!REASON)				
        End If


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)



        Call ShowSaleReturnReport(mSqlStr, mKey, mCustCode, pNetValue, Mode, mTitle, mSubTitle, mRptFileName, True)

        Exit Sub

        ''-----------------------------------------------------------------------------------
        Sqlstr = " SELECT DISTINCT REJECTION,MKEY,SUPP_CUST_CODE,NETVALUE, BOOKSUBTYPE, BILL_TO_LOC_ID " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(ConPurchase, 1) & "' AND CANCELLED='N'"   ''AND REJECTION='Y' 


        Sqlstr = Sqlstr & vbCrLf & " AND PURCHASESEQTYPE='" & Mid(cboVoucher.Text, 1, 1) & "'"

        If optPartyName(1).Checked = True Then
            mCustCode = ""
            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustCode = MasterNo
            End If

            Sqlstr = Sqlstr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"

        End If

        If optPrintRange(0).Checked = True Then
            Sqlstr = Sqlstr & vbCrLf _
                & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            Sqlstr = Sqlstr & vbCrLf _
                & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf _
                & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mKey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)
                mCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                pNetValue = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mRejection = IIf(IsDBNull(RsTemp.Fields("REJECTION").Value), "N", RsTemp.Fields("REJECTION").Value)
                mBookSubType = IIf(IsDBNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
                mLocation = IIf(IsDBNull(RsTemp.Fields("BILL_TO_LOC_ID").Value), "", RsTemp.Fields("BILL_TO_LOC_ID").Value)

                mRptFileName = "PurchaseGST.rpt"

                If mRejection = "Y" Then
                    mTitle = "Credit Note (Sale Rejection)"
                    mSubTitle = "Sale Return" ''IIf(IsNull(RsTemp!REASON), 1, RsTemp!REASON)			
                Else
                    mTitle = "Purchase Voucher"
                    mSubTitle = "" ''IIf(IsNull(RsTemp!REASON), 1, RsTemp!REASON)				
                End If

                '            If mSubTitle = "1" Then					
                '                mSubTitle = "Rate Diff"					
                '            ElseIf mSubTitle = "2" Then					
                '                mSubTitle = "Shortage"					
                '            Else					
                '                mSubTitle = "Others"					
                '            End If					

                mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "
                ''FROM CLAUSE...
                mSqlStr = mSqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST , FIN_SUPP_CUST_BUSINESS_MST BMST, GEN_COMPANY_MST GMST" ' & vbCrLf |
                ''WHERE CLAUSE...
                mSqlStr = mSqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                        & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
                        & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID" & vbCrLf _
                        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                        & " AND IH.MKEY='" & mKey & "'" & vbCrLf _
                        & " AND IH.BOOKTYPE='" & VB.Left(ConPurchase, 1) & "'" & vbCrLf _
                        & " AND IH.BOOKSUBTYPE='" & mBookSubType & "'" & vbCrLf _
                        & " AND IH.ISFINALPOST='Y' AND IH.BILL_TO_LOC_ID='" & mLocation & "'"
                ''ORDER CLAUSE...
                mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"


                Report1.Reset()
                MainClass.ClearCRptFormulas(Report1)



                Call ShowSaleReturnReport(mSqlStr, mKey, mCustCode, pNetValue, Mode, mTitle, mSubTitle, mRptFileName, True)

                RsTemp.MoveNext()
            Loop
        End If
        Exit Sub

ERR1:

        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowSaleReturnReport(ByRef mSqlStr As String, ByRef mKey As String, ByRef mCustCode As String, ByRef pNetValue As Double, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mStateName As String
        Dim mStateCode As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False


        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(mCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        If mRptFileName = "PO_View.rpt" Then
        Else
            MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        End If

        '    If pIsPO = "Y" Then					
        ''        Report1.SubreportToChange = Report1.GetNthSubreportName(0)					
        ''        Report1.Connect = STRRptConn					
        ''        Report1.SQLQuery = SqlStrSub					
        ''					
        ''        Report1.SubreportToChange = ""					
        '					
        '    Else					
        mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(pNetValue)) = 0, 0, pNetValue)))

        '        If chkCancelled.Value = vbChecked Then					
        '            MainClass.AssignCRptFormulas Report1, "AmountInWord=""Rs. Zero"""					
        '            MainClass.AssignCRptFormulas Report1, "NetAmount=""0.00"""					
        '        Else	

        'MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
        'MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & pNetValue & """")

        '        End If					
        ''            & " --AND FIN_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(mKey) & "'" & vbCrLf _
        Dim mCondSqlStr As String

        mCondSqlStr = " SELECT MKEY FROM FIN_PURCHASE_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                    & " AND ISFINALPOST='Y'" & vbCrLf _
                    & " AND PURCHASESEQTYPE='" & Mid(cboVoucher.Text, 1, 1) & "'"

        If optPartyName(1).Checked = True Then
            mCondSqlStr = mCondSqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"
        End If

        If optPrintRange(0).Checked = True Then
            mCondSqlStr = mCondSqlStr & vbCrLf _
                & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            mCondSqlStr = mCondSqlStr & vbCrLf _
                & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf _
                & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If


        SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR, FIN_PURCHASE_EXP, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_PURCHASE_HDR.MKEY=FIN_PURCHASE_EXP.MKEY" & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE=FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " AND FIN_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " And FIN_PURCHASE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If CDate(txtVDate.Text) >= CDate(PubGSTApplicableDate) Then 'Change on 29/010/2017 before If CDate(txtVDate.Text) < CDate(PubGSTApplicableDate) Then					
        SqlStrSub = SqlStrSub & vbCrLf _
            & " And GST_ENABLED='Y'"
        '        Else					
        '            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"					
        '        End If					

        '' SqlStrSub = SqlStrSub & vbCrLf & " AND MKEY IN ( " & mCondSqlStr & ")"

        If optPartyName(1).Checked = True Then
            SqlStrSub = SqlStrSub & vbCrLf & " AND FIN_PURCHASE_HDR.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"
        End If

        If optPrintRange(0).Checked = True Then
            SqlStrSub = SqlStrSub & vbCrLf _
                & " AND FIN_PURCHASE_HDR.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND FIN_PURCHASE_HDR.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStrSub = SqlStrSub & vbCrLf _
                & " AND FIN_PURCHASE_HDR.VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf _
                & " AND FIN_PURCHASE_HDR.VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"


        Report1.SubreportToChange = Report1.GetNthSubreportName(0)



        'Report1.set_ParameterFields(0, "IH.COMPANY_CODE")
        'Report1.set_ParameterFields(1, "IH.MKEY")

        '        {FIN_PURCHASE_HDR.COMPANY_CODE} = {?Pm-IH.COMPANY_CODE}
        'And
        '{FIN_PURCHASE_HDR.MKEY} = {?Pm-IH.MKEY}


        Report1.Connect = STRRptConn
        'Report1.SQLQuery = SqlStrSub

        '        Report1.SubreportToChange = ""					
        '            & " --AND TRN.MKEY='" & mKey & "'" & vbCrLf _

        SqlStrSub = " SELECT TRN.MKEY, ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf _
            & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " TRN.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE ='" & VB.Left(lblBookType.Text, 1) & "'"

        SqlStrSub = SqlStrSub & vbCrLf & " AND TRN.MKEY IN ( " & mCondSqlStr & ")"

        SqlStrSub = SqlStrSub & vbCrLf & " GROUP BY TRN.MKEY, ACM.SUPP_CUST_NAME"


        Report1.SubreportToChange = Report1.GetNthSubreportName(1)
        Report1.Connect = STRRptConn
        'Report1.SQLQuery = SqlStrSub
        'Report1.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(LblMKey.Text) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "' AND {BP.USER_ID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"

        Report1.SubreportToChange = ""



        '    End If					

        '					
        '    MainClass.AssignCRptFormulas Report1, "mStateCode=""" & mStateCode & """"					
        '					
        '    MainClass.AssignCRptFormulas Report1, "CompanyGSTIN=""" & IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO) & """"					
        '    MainClass.AssignCRptFormulas Report1, "COMPANYCINNo=""" & IIf(IsNull(RsCompany!CIN_NO), "", RsCompany!CIN_NO) & """"					
        '					
        '    If IsSubReport = True Then					
        '        mAmountInword = MainClass.RupeesConversion(CDbl(pNetValue))					
        '					
        '        MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"					
        '        MainClass.AssignCRptFormulas Report1, "NetAmount=""" & pNetValue & """"					
        '					
        '        SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf _					
        ''                    & " FROM FIN_PURCHASE_EXP, FIN_INTERFACE_MST " & vbCrLf _					
        ''                    & " WHERE FIN_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _					
        ''                    & " AND FIN_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(mKey) & "'" & vbCrLf _					
        ''                    & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "  AND GST_ENABLED='Y' " & vbCrLf _					
        ''                    & " ORDER BY SUBROWNO"					
        '					
        '        Report1.SubreportToChange = Report1.GetNthSubreportName(0)					
        '        Report1.Connect = STRRptConn					
        '        Report1.SQLQuery = SqlStrSub					
        '					
        '        Report1.SubreportToChange = ""					
        '    End If					
        '					
        SqlStrSub = ""

        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()
        'Report1.Dispose()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowCustomerDebitReport(ByRef mSqlStr As String, ByRef mKey As String, ByRef mCustCode As String, ByRef pNetValue As Double, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mStateName As String
        Dim mStateCode As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False


        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(mCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")

        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        If IsSubReport = True Then
            mAmountInword = MainClass.RupeesConversion(CDbl(pNetValue))

            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
            MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & pNetValue & """")

            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_SUPP_SALE_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_SUPP_SALE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_SUPP_SALE_EXP.MKEY='" & MainClass.AllowSingleQuote(mKey) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND GST_ENABLED='Y' " & vbCrLf & " ORDER BY SUBROWNO"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""
        End If

        SqlStrSub = ""

        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForAdvise(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef pAccountCode As String, ByRef mBookCode As String, ByRef pNarration As String, ByRef pBankName As String, ByRef pNetAmount As Double, ByRef pCurrBal As String, ByRef pDrCrNo As String, ByRef pAmountInWord As String, ByRef pCancelled As String) As String


        On Error GoTo ErrPart

        If InsertTempTable(mVNo, mVDate, mBookType, pAccountCode) = False Then GoTo ErrPart

        mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',TRN.SUBROWNO, " & vbCrLf _
            & " TRN.VNO,TO_CHAR(TRN.VDATE,'DD/MM/YYYY')," & vbCrLf & " TEMP_FIN_PAYMENT.BILLNO,  " & vbCrLf & " TO_CHAR(TEMP_FIN_PAYMENT.BILLDATE,'DD/MM/YYYY'), TO_CHAR(TEMP_FIN_PAYMENT.BILLAMOUNT,'999999999.99'), To_CHAR(TEMP_FIN_PAYMENT.ADV,'999999999.99'), " & vbCrLf & " TO_CHAR(TEMP_FIN_PAYMENT.DNOTE,'999999999.99'), TO_CHAR(TEMP_FIN_PAYMENT.CNOTE,'999999999.99'), TO_CHAR(TEMP_FIN_PAYMENT.TDS,'999999999.99'), TO_CHAR(TEMP_FIN_PAYMENT.PAYMENT,'999999999.99'), " & vbCrLf & " TO_CHAR(TEMP_FIN_PAYMENT.BALANCE,'999999999.99'), TEMP_FIN_PAYMENT.DC, ACM.SUPP_CUST_NAME, TEMP_FIN_PAYMENT.ACCOUNTCODE, " & vbCrLf & " SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, " & vbCrLf _
            & " SUPP_CUST_PIN,  SUPP_CUST_PHONE,TRN.CHEQUENO,TO_CHAR(TRN.CHQDATE,'DD/MM/YYYY'),TO_CHAR(TRN.AMOUNT * DECODE(TRN.DC,'D',1,-1),'999999999.99'), " & vbCrLf & " '" & pCancelled & "','','" & MainClass.AllowSingleQuote(pNarration) & "','" & MainClass.AllowSingleQuote(pBankName) & "'," & vbCrLf & " TO_CHAR('" & Val(CStr(pNetAmount)) & "','999999999.99')," & vbCrLf & " '" & pCurrBal & "','" & MainClass.AllowSingleQuote(pDrCrNo) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(pAmountInWord) & "',TEMP_FIN_PAYMENT.DCNOTE,TEMP_FIN_PAYMENT.DUEDATE,TEMP_FIN_PAYMENT.VNO, TEMP_FIN_PAYMENT.VDATE"

        mSqlStr = mSqlStr & vbCrLf & " FROM TEMP_FIN_PAYMENT , FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE TEMP_FIN_PAYMENT.UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=TEMP_FIN_PAYMENT.COMPANY_CODE(+) " & vbCrLf & " AND TRN.FYEAR=TEMP_FIN_PAYMENT.FYEAR(+) " & vbCrLf & " AND TRN.BillNo=TEMP_FIN_PAYMENT.BillNo(+) " & vbCrLf & " AND TRN.ACCOUNTCODE=TEMP_FIN_PAYMENT.ACCOUNTCODE(+) " & vbCrLf & " AND TRN.BillDate=TEMP_FIN_PAYMENT.BillDate(+) " & vbCrLf & " AND TEMP_FIN_PAYMENT.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TEMP_FIN_PAYMENT.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.BookType='" & Mid(mBookType, 1, 1) & "' " & vbCrLf _
            & " AND TRN.BookSubType='" & Mid(mBookType, 2, 1) & "'" & vbCrLf & " AND TRN.BOOKCODE='" & mBookCode & "'" & vbCrLf & " AND TRN.AccountCode<>'" & mBookCode & "' " & vbCrLf & " AND TRN.VNO='" & mVNo & "'"

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY TEMP_FIN_PAYMENT.BILLDATE,TEMP_FIN_PAYMENT.BILLNO"
        SelectQryForAdvise = mSqlStr
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        SelectQryForAdvise = ""
    End Function
    Private Function InsertTempTable(ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mAccountCode As String) As Boolean

        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim mSqlStr As String
        Dim mDrCrNo As String
        Dim mDueDate As String
        Dim mPurVNo As String
        Dim mPurVDate As String

        '    PubDBCn.Errors.Clear					
        '    PubDBCn.BeginTrans					

        Sqlstr = "DELETE FROM TEMP_FIN_PAYMENT WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(Sqlstr)

        mDrCrNo = Mid(GETDRCRNo(mVNo, mVDate, mBookType, mAccountCode, False), 1, 1000)


        If frmPrintVoucher.OptReceiptWithDue.Checked = True Then
            mDueDate = "GETBILLDUEDATE(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
        Else
            mDueDate = "''"
        End If

        If frmPrintVoucher.optHundiAdvise.Checked = True Then
            mPurVNo = "GETVOUCHERNO(TRN.COMPANY_CODE, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
            mPurVDate = "GETVOUCHERDATE(TRN.COMPANY_CODE, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
        Else
            mPurVNo = "''"
            mPurVDate = "''"
        End If

        mSqlStr = "Select '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " TRN.COMPANY_CODE,  TRN.FYEAR, TRN.ACCOUNTCODE, " & vbCrLf & " BillNo,  BillDate, " & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)) ," & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)) AS BAL," & vbCrLf & " CASE WHEN SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount) >=0 THEn 'DR' ELSE 'CR' END," & vbCrLf & " '" & mDrCrNo & "'," & mDueDate & ", " & mPurVNo & ", " & mPurVDate & " FROM FIN_POSTED_TRN TRN " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & "  AND AccountCode='" & mAccountCode & "'" & vbCrLf & " GROUP BY BillNo, BillDate,COMPANY_CODE,FYEAR,ACCOUNTCODE ORDER BY COMPANY_CODE,FYEAR , BillNo, BillDate"

        Sqlstr = "INSERT INTO TEMP_FIN_PAYMENT (" & vbCrLf & " USERID, COMPANY_CODE, FYEAR, ACCOUNTCODE," & vbCrLf & " BillNo, BillDate, BILLAMOUNT," & vbCrLf & " ADV, DNOTE, CNOTE, TDS, " & vbCrLf & " PAYMENT,BALANCE, DC,DCNOTE,DUEDATE,VNO, VDATE) " & vbCrLf & mSqlStr

        PubDBCn.Execute(Sqlstr)

        ''21/05/2018  remove from query					
        ''AND VNO='" & mVNo & "' AND VDATE='" & vb6.Format(mVDate, "DD-MMM-YYYY") & "' AND BOOKTYPE='" & Left(mBookType, 1) & "' AND BOOKSUBTYPE='" & Right(mBookType, 1) & "'					

        '    PubDBCn.CommitTrans					
        InsertTempTable = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertTempTable = False
        '    PubDBCn.RollbackTrans					
    End Function
    Private Sub ReportOnTrnVoucher(ByRef pMode As String)

        On Error GoTo ERR1
        Dim RsPrint As ADODB.Recordset
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mBranchCode As Integer
        Dim mCategoryCode As Integer
        Dim mVNo As String
        Dim pMKey As String
        Dim mDNNoStr As String
        Dim mVDate As Date
        Dim mTotalAmount As Double
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mBookCode As String
        Dim Sqlstr As String
        Dim mMultiLine As Boolean
        Dim mRptFileName As String
        Dim cntRow As Integer
        Dim mNarration As String
        Dim mAccountName As String
        Dim pLastPaymentAmount As Double
        Dim mParticularAmount As Double
        Dim mLCPayment As Boolean
        Dim mNarrDetail As String
        Dim mChequeNo As String
        Dim mNarrAcct As String
        Dim mBankName As String
        Dim mBookName As String
        Dim mAccountCode As String
        Dim xAccountCode As String
        Dim mPartyOpBal As String
        Dim mAmountInword As String
        Dim pOpBal As Double
        Dim mDrCrNo As String
        Dim mCancelled As String
        Dim Mode As Crystal.DestinationConstants

        If FieldsVerification() = False Then Exit Sub

        If pMode = "M" Then
            Mode = Crystal.DestinationConstants.crptToWindow
        ElseIf pMode = "P" Then
            Mode = Crystal.DestinationConstants.crptToPrinter
        Else
            Mode = Crystal.DestinationConstants.crptToWindow
        End If

        frmPrintVoucher.OptItemRecevied.Enabled = False
        frmPrintVoucher.OptReceipt.Enabled = True
        frmPrintVoucher.OptVoucher.Checked = True

        frmPrintVoucher.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintVoucher.optDNVoucher.Checked = True Then
            If lblBookType.Text = ConJournal Then
                Call SelectQryForVoucher(Sqlstr, mVNo, mVDate, mBookType, mBookCode, mNarration, mBankName, mTotalAmount, mPartyOpBal, "", mAmountInword, mCancelled)
                mTitle = "Debit Note"
                mRptFileName = "Voucher_DN.rpt"

                Call ShowReport(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName)
                Exit Sub
            End If
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        Sqlstr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(Sqlstr)

        Sqlstr = ""

        Report1.Reset()


        Sqlstr = " SELECT TRNMain.MKEY,TRNMain.ROWNO,TRNMain.VNO,TRNMain.VDATE, " & vbCrLf _
            & " TRNMain.BOOKTYPE,TRNMain.BOOKSUBTYPE,TRNMain.BOOKCODE, " & vbCrLf _
            & " TRNMain.CANCELLED, TRNMain.NARRATION," & vbCrLf _
            & " TRNDetail.PRROWNO,TRNDetail.SUBROWNO, TRNDetail.ACCOUNTCODE, " & vbCrLf _
            & " TRNDetail.PARTICULARS,TRNDetail.AMOUNT,TRNDetail.DC, TRNDetail.CHEQUENO, " & vbCrLf _
            & " TRNDetail.CHQDATE,TRNDetail.IBRNO,TRNDetail.CLEARDATE, " & vbCrLf _
            & " ACM.SUPP_CUST_NAME AS ACMNAME " & vbCrLf _
            & " FROM FIN_VOUCHER_HDR TRNMain, FIN_VOUCHER_DET TRNDetail, " & vbCrLf _
            & " FIN_SUPP_CUST_MST ACM " & vbCrLf _
            & " WHERE TRNMain.MKey = TRNDetail.MKey " & vbCrLf _
            & " AND TRNMain.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND TRNDETAIL.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND TRNMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TRNMain.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND TRNMain.BookType='" & VB.Left(lblBookType.Text, 1) & "' " & vbCrLf _
            & " AND TRNMain.BookSubType='" & VB.Right(lblBookType.Text, 1) & "' "



        If optPartyName(1).Checked = True And Trim(txtPartyName.Text) <> "" Then

            If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xAccountCode = MasterNo

                Sqlstr = Sqlstr & vbCrLf & " AND TRNMain.MKEY IN ( " & vbCrLf & " SELECT MKEY FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "' " & vbCrLf & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "' "

                If optPrintRange(0).Checked = True Then
                    Sqlstr = Sqlstr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                Else
                    Sqlstr = Sqlstr & vbCrLf & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
                End If


                Sqlstr = Sqlstr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAccountCode) & "')"
            End If
        End If

        If optPrintRange(0).Checked = True Then
            Sqlstr = Sqlstr & vbCrLf & " AND TRNMain.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TRNMain.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND TRNMain.VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf & " AND TRNMain.VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If

        '    If OptOrderBy(0).Value = True Then					
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY TRNMain.VNO,TRNDetail.SubRowNo "
        '    Else					
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME ,TRNMain.VNO,TRNDetail.SubRowNo "					
        '    End If					

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPrint, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPrint.EOF = False Then


            Do While Not RsPrint.EOF
                Sqlstr = ""
                pMKey = RsPrint.Fields("MKEY").Value
                mVNo = RsPrint.Fields("VNO").Value
                mVDate = RsPrint.Fields("VDATE").Value
                mBookType = lblBookType.Text
                mBookCode = CStr(Val(RsPrint.Fields("BOOKCODE").Value))

                If lblBookType.Text = ConDebitNote Or lblBookType.Text = ConCreditNote Then

                Else
                    If lblBookType.Text <> ConJournal And lblBookType.Text <> ConContra Then
                        If MainClass.ValidateWithMasterTable(mBookCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mBookName = MasterNo
                        Else
                            mBookName = ""
                        End If
                    End If
                End If
                mSubTitle = ""
                mMultiLine = False
                cntRow = 0
                mTotalAmount = 0

                ''Check multiple entry...					

                mAccountName = IIf(IsDBNull(RsPrint.Fields("ACMNAME").Value), "", RsPrint.Fields("ACMNAME").Value)
                mNarrDetail = MainClass.AllowVBNewLine(IIf(IsDBNull(RsPrint.Fields("NARRATION").Value), "", RsPrint.Fields("NARRATION").Value))
                mChequeNo = IIf(IsDBNull(RsPrint.Fields("CHEQUENO").Value), "", RsPrint.Fields("CHEQUENO").Value)
                mChequeNo = mChequeNo & IIf(IsDBNull(RsPrint.Fields("CHQDATE").Value), "", " Dt. " & RsPrint.Fields("CHQDATE").Value)
                Do While mVNo = RsPrint.Fields("VNO").Value
                    mTotalAmount = mTotalAmount + (IIf(IsDBNull(RsPrint.Fields("Amount").Value), 0, RsPrint.Fields("Amount").Value) * IIf(RsPrint.Fields("DC").Value = "D", 1, -1))
                    RsPrint.MoveNext()
                    If RsPrint.EOF Then Exit Do
                Loop


                mTotalAmount = System.Math.Abs(mTotalAmount)

                If Not RsPrint.BOF Then
                    RsPrint.MovePrevious()
                End If

                Call MainClass.ClearCRptFormulas(Report1)
                Select Case lblBookType.Text
                    Case ConCashReceipt
                        mTitle = "Cash Receipt"
                        'If frmPrintVoucher.OptReceipt Then					
                        mNarrAcct = mAccountName
                        mNarration = "Received with thanks a sum of Rs. " & mTotalAmount
                        mNarration = mNarration & " ( " & MainClass.RupeesConversion(mTotalAmount)
                        mNarration = mNarration & " ) Cash on account of " & mNarrAcct & ""
'End If					
                    Case ConCashPayment
                        mTitle = "Cash Payment"
                        mNarrAcct = mAccountName
                        mNarration = "Received with thanks a sum of Rs. " & mTotalAmount
                        mNarration = mNarration & " ( " & MainClass.RupeesConversion(mTotalAmount)
                        mNarration = mNarration & " ) Cash on account of " & mNarrAcct & ""
                    Case ConBankReceipt
                        mTitle = "Bank Receipt"
                        If frmPrintVoucher.OptReceipt.Checked Then
                            mNarrAcct = mAccountName
                            mNarration = "Received with thanks a sum of Rs. " & mTotalAmount
                            mNarration = mNarration & " ( " & MainClass.RupeesConversion(mTotalAmount)
                            mNarration = mNarration & " ) By Cheque No. " & mChequeNo & " On account of " & mNarrDetail & " "
                            mNarration = mNarration & " as Per detail given below :"
                        End If
                        mBankName = "Bank : " & mBookName
                    Case ConBankPayment
                        mTitle = "Bank Payment"
                        If frmPrintVoucher.OptReceipt.Checked Then
                            mNarrAcct = mAccountName
                            mSubTitle = "We have pleasure in enclosing herewith our cheque against "
                            mSubTitle = mSubTitle & " your invoice details given below :"
                        End If
                        '                     mNarration = "Narration : " & Trim(IIf(IsNull(RsPrint!PARTICULARS), "", RsPrint!PARTICULARS))					
                        mBankName = "Bank : " & mBookName
                    Case ConPDCReceipt
                        mTitle = "Post Dated Receipt"
                        If frmPrintVoucher.OptReceipt.Checked Then
                            mNarrAcct = mAccountName
                            mNarration = "Received with thanks a sum of Rs. " & mTotalAmount
                            mNarration = mNarration & " ( " & MainClass.RupeesConversion(mTotalAmount)
                            mNarration = mNarration & " ) By Cheque No. " & mChequeNo & " On account of " & mNarrDetail & " "
                            mNarration = mNarration & " as Per detail given below :"
                        End If
                        mBankName = "Bank : " & mBookName
                    Case ConPDCPayment
                        mTitle = "Post Dated Payment"
                        ''                     mNarration = "Narration : " & Trim(txtNarration.Text)					
                        mBankName = "Bank : " & mBookName
                    Case ConJournal
                        mTitle = "Journal"
                        mNarration = "Narration : " & mNarrDetail
                    Case ConContra
                        mTitle = "Contra"
                        mNarration = "Narration : " & mNarrDetail
                End Select

                mCancelled = IIf(RsPrint.Fields("Cancelled").Value = "Y", " (CANCELLED )", "")

                mNarration = VB.Left(mNarration, 254)

                mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mTotalAmount)) = 0, 0, mTotalAmount)))

                If frmPrintVoucher.OptItemRecevied.Checked = True Then
                    Sqlstr = SelectQryForItem(Sqlstr, mVNo, mVDate, mBookType, mBookCode)
                ElseIf frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    If IsDate(mVDate) Then
                        '                    MainClass.ValidateWithMasterTable mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & ""					
                        '                    mAccountCode = MasterNo					
                        pOpBal = GetOpeningBal(IIf(IsDBNull(RsPrint.Fields("ACCOUNTCODE").Value), "-1", RsPrint.Fields("ACCOUNTCODE").Value), VB6.Format(mVDate, "DD/MM/YYYY"))
                    End If
                    mPartyOpBal = VB6.Format(System.Math.Abs(pOpBal), "0.00") & IIf(pOpBal >= 0, "Dr", "Cr")
                    mDrCrNo = GETDRCRNo(mVNo, mVDate, mBookCode, IIf(IsDBNull(RsPrint.Fields("ACCOUNTCODE").Value), "-1", RsPrint.Fields("ACCOUNTCODE").Value), True)

                    mNarration = "Narration : " & Trim(IIf(IsDBNull(RsPrint.Fields("PARTICULARS").Value), "", RsPrint.Fields("PARTICULARS").Value))
                    Sqlstr = SelectQryForAdvise(Sqlstr, mVNo, mVDate, mBookType, IIf(IsDBNull(RsPrint.Fields("ACCOUNTCODE").Value), "-1", RsPrint.Fields("ACCOUNTCODE").Value), mBookCode, mNarration, mBankName, mTotalAmount, mPartyOpBal, mDrCrNo, mAmountInword, mCancelled)
                ElseIf frmPrintVoucher.OptVoucher.Checked = True Then
                    'Sqlstr = SelectQryForVoucherNew(Sqlstr, mVNo, mVDate, mBookType, mBookCode, mNarration, mBankName, mTotalAmount, mPartyOpBal, "", mAmountInword, mCancelled)
                    If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then
                        Sqlstr = SelectQryForVoucherBank(Sqlstr, pMKey, mVNo, mVDate, mBookType, mBookCode, mNarration, mBankName, mTotalAmount, mPartyOpBal, "", mAmountInword, mCancelled)
                        mRptFileName = "BankVoucher.rpt"
                    Else
                        Sqlstr = SelectQryForVoucherNew(Sqlstr, pMKey, mVNo, mVDate, mBookType, mBookCode, mNarration, mBankName, mTotalAmount, mPartyOpBal, "", mAmountInword, mCancelled)
                        mRptFileName = "TrnVoucher.rpt"
                    End If
                    If pMode = "M" Then
                        '        Call ShowReportForMail("", SqlStr, "", Mode, mTitle, mSubTitle, mRptFileName)					
                    Else
                        '' Call ShowReport(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName)
                        If ShowReportVoucher(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName, mNarration, mBankName, mAccountName, mPartyOpBal, pLastPaymentAmount, mChequeNo, mParticularAmount, mLCPayment) = False Then GoTo ERR1
                    End If
                    GoTo NextRecd
                ElseIf frmPrintVoucher.OptDnCn.Checked = True Then
                    mDrCrNo = GETDRCRNo(mVNo, mVDate, mBookCode, IIf(IsDBNull(RsPrint.Fields("ACCOUNTCODE").Value), "-1", RsPrint.Fields("ACCOUNTCODE").Value), False)
                    If Trim(mDrCrNo) <> "" Then
                        mDNNoStr = IIf(mDNNoStr = "", "", mDNNoStr & ", ") & IIf(mDrCrNo = "", "", mDrCrNo)
                    End If
                    GoTo NextRecd
                ElseIf frmPrintVoucher.optDNVoucher.Checked = True Then
                    If lblBookType.Text = ConJournal Then
                        Call SelectQryForVoucher(Sqlstr, mVNo, mVDate, mBookType, mBookCode, mNarration, mBankName, mTotalAmount, mPartyOpBal, "", mAmountInword, mCancelled)
                        mTitle = "Debit Note"
                        mRptFileName = "Voucher_DN.rpt"

                        Call ShowReport(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName)
                        Exit Sub
                    End If
                End If


                Sqlstr = "INSERT INTO TEMP_PRINTDUMMYDATA " & vbCrLf _
                    & " (UserID,SubRow,Field1,Field2, " & vbCrLf _
                    & " Field3,Field4,Field5,Field6,Field7," & vbCrLf _
                    & " Field8,Field9,Field10,Field11,Field12," & vbCrLf _
                    & " Field13,Field14,Field15,Field16,Field17," & vbCrLf _
                    & " Field18,Field19,Field20,Field21,Field22," & vbCrLf _
                    & " Field23,Field24,Field25,Field26,Field27," & vbCrLf _
                    & " Field28,Field29,Field30,Field31,Field32,Field33,Field34)" & vbCrLf & Sqlstr

                PubDBCn.Execute(Sqlstr)


NextRecd:
                RsPrint.MoveNext()
            Loop
            PubDBCn.CommitTrans()
        Else
            If optPrintRange(0).Checked = True Then
                MsgBox("Nothing to Print In Given Date Range", MsgBoxStyle.Information)
            Else
                MsgBox("Nothing to Print In Given Voucher Range", MsgBoxStyle.Information)
            End If
            PubDBCn.CommitTrans()
            Exit Sub
        End If

        If frmPrintVoucher.OptVoucher.Checked = True Then
            frmPrintVoucher.Close()

            Report1.Reset()
            Exit Sub
        End If

        Sqlstr = ""


        If frmPrintVoucher.OptItemRecevied.Checked = True Then
            mRptFileName = "ItemReceviedALL.rpt"
            mTitle = "Details of Items Received"
            Sqlstr = FetchRecordForReport(Sqlstr)
        ElseIf frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
            If frmPrintVoucher.OptReceipt.Checked = True Then
                mRptFileName = "ReceiptAdviseALL.rpt"
                mTitle = mTitle & " Advice"
            Else
                mRptFileName = "HundiAdviseAll.rpt"
                mTitle = "Hundi Advice"
            End If
            Sqlstr = MainClass.FetchFromTempData(Sqlstr, "FIELD1, FIELD3")
        ElseIf frmPrintVoucher.OptVoucher.Checked = True Then
            If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then
                mRptFileName = "BankVoucherALL.rpt"
            Else
                mRptFileName = "TrnVoucherall.rpt"
            End If
            Sqlstr = FetchRecordForReport(Sqlstr)
        ElseIf frmPrintVoucher.OptDnCn.Checked = True Then
            '        mVNoStr = "(" & mVNoStr & ")"					
            '        mDrCrNo = GETDRCRNo(mVNoStr, mVDate, mBookCode, IIf(IsNull(RsPrint!ACCOUNTCODE), "-1", RsPrint!ACCOUNTCODE), False)					
            'If Trim(mDNNoStr) <> "" Then
            '    Call ReportOnDrCr(True, mDNNoStr, pMode)
            'End If
            'mDNNoStr = ""
            'frmPrintVoucher.Close()
            'Exit Sub
        End If

        '''''Select Record for print...					
        If pMode = "M" Then
            '        Call ShowReportForMail("", SqlStr, "", Mode, mTitle, mSubTitle, mRptFileName)					
        Else
            Call ShowReport(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName)
        End If

        frmPrintVoucher.Close()

        Report1.Reset()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume					
        PubDBCn.RollbackTrans()

    End Sub
    Private Function SelectQryForVoucherBank(ByRef mSqlStr As String, ByRef pMKey As String, ByRef mVNo As String, ByRef mVDate As Date,
                                         ByRef mBookType As String, ByRef mBookCode As String, ByRef pNarration As String, ByRef pBankName As String, ByRef pNetAmount As Double, ByRef pCurrBal As String, ByRef pDrCrNo As String, ByRef pAmountInWord As String, ByRef pCancelled As String) As String


        mSqlStr = " SELECT TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
            & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
            & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
            & " TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf _
            & " A.SUPP_CUST_Name,B.SUPP_CUST_NAME, IDIV.DIV_ALIAS " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " TRN.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
            & " TRN.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
            & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
            & " TRN.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
            & " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
            & " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
            & " VNO='" & mVNo & "' AND " & vbCrLf _
            & " MKEY='" & pMKey & "' AND " & vbCrLf _
            & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
            & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
            mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        End If

        If frmPrintVoucher.optDNVoucher.Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.DC DESC"
        Else
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo, TRN.ACCOUNTCODE"
        End If

        SelectQryForVoucherBank = mSqlStr
    End Function
    Private Function ShowReportVoucher(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef mNarration As String, ByRef mBankName As String, ByRef mAccountName As String, ByRef pPartyOpBal As String, ByRef pLastPaymentAmount As String, ByRef mChqNo As String, ByRef mParticularAmount As Double, Optional ByRef mLCPayment As Boolean = False) As Boolean
        'Dim Printer As New Printer										
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        Dim mVoucherAmount As Double
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mReference As String
        Dim mSubject As String
        Dim mTextBody1 As String
        Dim mTextBody2 As String
        Dim mTextBody3 As String
        Dim mBankAccountNo As String
        Dim mPartyBankName As String = ""
        Dim mPartyBankBranch As String = ""
        Dim mBankBranch As String = ""
        Dim mFavourof As String = ""
        Dim mPartyBankAcctNo As String = ""
        Dim mOurBankName As String = ""
        Call MainClass.ClearCRptFormulas(Report1)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        'If frmPrintVoucher.optRTGSLetter.Checked = True Then
        '    mRefNo = "Ref. " & IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value) & "/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY") & "/"
        '    MainClass.AssignCRptFormulas(Report1, "RefNo=""" & mRefNo & """")
        '    MainClass.AssignCRptFormulas(Report1, "RefDate=""" & VB6.Format(TxtVDate.Text, "MMMM DD, YYYY") & """")
        '    mBankAccountNo = ""
        '    If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "CUST_BANK_ACCT_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mBankAccountNo = MasterNo
        '    End If
        '    If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "CUST_BANK_BANK", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mOurBankName = MasterNo
        '    End If
        '    If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "BANK_BRANCH_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mBankBranch = MasterNo
        '    End If
        '    mVoucherAmount = CDbl(VB6.Format(mParticularAmount, "0.00"))
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))
        '    mReference = "Our C.C. A/c No. " & mBankAccountNo
        '    If MainClass.ValidateWithMasterTable(Trim(mAccountName), "SUPP_CUST_NAME", "CUST_BANK_ACCT_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mPartyBankAcctNo = MasterNo
        '    End If
        '    If MainClass.ValidateWithMasterTable(Trim(mAccountName), "SUPP_CUST_NAME", "CUST_BANK_BANK", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mPartyBankName = MasterNo
        '    End If
        '    If MainClass.ValidateWithMasterTable(Trim(mAccountName), "SUPP_CUST_NAME", "BANK_BRANCH_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mPartyBankBranch = MasterNo
        '    End If
        '    mSubject = "Request for transfer of Rs. " & VB6.Format(mVoucherAmount, "0.00") & "/- under RTGS / NEFT to " & mPartyBankName & " " & mPartyBankBranch
        '    mFavourof = "In favour of M/s " & RsCompany.Fields("Company_Name").Value & " C.C. A/c No. " & mPartyBankAcctNo
        '    mTextBody1 = "It is hereby requested to transfer a sum of Rs. " & VB6.Format(mVoucherAmount, "0.00") & "/- ( Rs." & mAmountInword & ") to the following account with " & mPartyBankName & " " & mPartyBankBranch & " under RTGS / NEFT Scheme : -"
        '    mTextBody2 = "We are enclosing herewith Cheque No. " & mChqNo & " of " & mOurBankName & " " & mBankBranch & "."
        '    mTextBody3 = "You are requested to please debit our C.C. Account No. " & mBankAccountNo & " & oblige."
        '    MainClass.AssignCRptFormulas(Report1, "BankName=""" & mOurBankName & """")
        '    MainClass.AssignCRptFormulas(Report1, "BranchName=""" & mBankBranch & """")
        '    MainClass.AssignCRptFormulas(Report1, "Favourof=""" & mFavourof & """")
        '    MainClass.AssignCRptFormulas(Report1, "Reference=""" & mReference & """")
        '    MainClass.AssignCRptFormulas(Report1, "Subject=""" & mSubject & """")
        '    MainClass.AssignCRptFormulas(Report1, "TextBody1=""" & mTextBody1 & """")
        '    MainClass.AssignCRptFormulas(Report1, "TextBody2=""" & mTextBody2 & """")
        '    MainClass.AssignCRptFormulas(Report1, "TextBody3=""" & mTextBody3 & """")
        'Else
        MainClass.AssignCRptFormulas(Report1, "Narration=""" & mNarration & """")
        MainClass.AssignCRptFormulas(Report1, "BankName=""" & mBankName & """")
        mVoucherAmount = 0
        'If frmPrintVoucher.optDNVoucher.Checked = True Then
        '    mVoucherAmount = CDbl(VB6.Format(LblDrAmt.Text, "0.00"))
        'Else
        '    mVoucherAmount = GetVoucherNetAmount()
        'End If
        If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                '        mDrCrNo = GETDRCRNo										
                MainClass.AssignCRptFormulas(Report1, "AmountPaid=""" & VB6.Format(mVoucherAmount, "0.00") & """")
                MainClass.AssignCRptFormulas(Report1, "CurrBal=""" & pPartyOpBal & " & " & pLastPaymentAmount & """")
                '        MainClass.AssignCRptFormulas Report1, "DrCrNo=""" & mDrCrNo & """"										
            ElseIf frmPrintVoucher.OptVoucher.Checked = True And (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Or mLCPayment = True) Then
                MainClass.AssignCRptFormulas(Report1, "CurrBal=""" & pPartyOpBal & " & " & pLastPaymentAmount & """")
            End If
            If lblBookType.Text = ConCashPayment Then
                mReceivedBy = "Receiver's Signature"
            Else
                mReceivedBy = " "
            End If
            MainClass.AssignCRptFormulas(Report1, "ReceivedBy=""" & mReceivedBy & """")
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))
            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
        'End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
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
        ShowReportVoucher = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        ShowReportVoucher = False
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " ORDER BY Field1,SUBROW"

        FetchRecordForReport = mSqlStr

    End Function

    Private Function SelectQryForItem(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String

        On Error GoTo ErrPart
        Dim pSqlStr As String

        '    PubDBCn.Errors.Clear					
        '					
        '    PubDBCn.BeginTrans					

        pSqlStr = "DELETE FROM TEMP_FIN_ITEMRECD WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(pSqlStr)

        pSqlStr = "INSERT INTO TEMP_FIN_ITEMRECD " & vbCrLf & " (UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME) "

        pSqlStr = pSqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " PURCHMAIN.BILLNO, " & vbCrLf & " PURCHMAIN.INVOICE_DATE, " & vbCrLf & " TRN.DUEDATE, " & vbCrLf & " PURCHDETAIL.ITEM_QTY, " & vbCrLf & " PURCHDETAIL.ITEM_RATE, " & vbCrLf & " PURCHDETAIL.ITEM_DESC, " & vbCrLf & " ACM.SUPP_CUST_NAME "

        pSqlStr = pSqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN ,FIN_SUPP_CUST_MST ACM, " & vbCrLf & " FIN_PURCHASE_HDR PURCHMAIN, FIN_PURCHASE_DET PURCHDETAIL"

        ''& " TRN.FYEAR=" & RsCompany!FYEAR & " AND " & vbCrLf _					
        '					
        pSqlStr = pSqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " TRN.COMPANY_CODE=PURCHMAIN.COMPANY_CODE AND " & vbCrLf & " TRN.FYEAR=PURCHMAIN.FYEAR AND " & vbCrLf & " TRN.AccountCode=PURCHMAIN.SUPP_CUST_CODE AND " & vbCrLf & " TRN.AccountCode =ACM.SUPP_CUST_CODE AND " & vbCrLf & " TRN.BILLNO=PURCHMAIN.BILLNO AND " & vbCrLf & " PURCHMAIN.MKey=PURCHDETAIL.MKey AND"


        pSqlStr = pSqlStr & vbCrLf & " TRN.VNO='" & mVNo & "' AND " & vbCrLf _
            & " TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " TRN.BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf & " TRN.BookSubType='" & Mid(mBookType, 2, 1) & "' AND " & vbCrLf & " TRN.BOOKCODE='" & mBookCode & "' AND " & vbCrLf & " TRN.AccountCode<>'" & mBookCode & "' "

        pSqlStr = pSqlStr & vbCrLf & " AND TRN.BILLNO||TRN.BILLDATE IN ( " & vbCrLf & " SELECT BILLNO||BILLDATE FROM FIN_POSTED_TRN" & vbCrLf & " WHERE FIN_POSTED_TRN.AccountCode=Acm.SUPP_CUST_Code " & vbCrLf & " AND FIN_POSTED_TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FIN_POSTED_TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND FIN_POSTED_TRN.BILLNO=PURCHMAIN.BILLNO " & vbCrLf & " AND FIN_POSTED_TRN.BILLDATE=PURCHMAIN.INVOICE_DATE " & vbCrLf & " GROUP BY FIN_POSTED_TRN.BILLNO||FIN_POSTED_TRN.BILLDATE " & vbCrLf & " HAVING " & vbCrLf & " SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)=0 )"

        PubDBCn.Execute(pSqlStr)
        '        PubDBCn.CommitTrans					

        mSqlStr = "SELECT " & vbCrLf & " UserId, 1, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME,'','','','','','','','','','','','','','','','','','','','','','','' " & vbCrLf & " FROM TEMP_FIN_ITEMRECD" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY ITEM_DESC"

        SelectQryForItem = mSqlStr

        Exit Function
ErrPart:
        '    PubDBCn.RollbackTrans					
        MsgInformation(Err.Description)
    End Function

    Private Function SelectQryForDrCrVoucher(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As String, ByRef mBookType As String, ByRef mBookCode As Integer) As String

        mSqlStr = " SELECT TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf & " TRN.ACCOUNTCODE2,TRN.ACCOUNTCODE,TRN.NARRATION, " & vbCrLf & " TRN.DAMOUNT,TRN.CAMOUNT,TRN.CHEQUENO,TRN.CHQDATE,TRN.COSTCCODE, " & vbCrLf & " TRN.DEPTCODE,TRN.EMPCODE,TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf & " A.Name,B.NAME,EMP.Name,DEPT.NAME,COSTC.NAME " & vbCrLf & " FROM TRN,ACM A,ACM B,EMP,Dept,CostC " & vbCrLf & " WHERE TRN.COMPANYCODE=" & RsCompany.Fields("CompanyCode").Value & " AND " & vbCrLf & " TRN.FYNo=" & RsCompany.Fields("FYNO").Value & " AND " & vbCrLf & " TRN.BRANCHCODE=" & RsCompany.Fields("BranchCode").Value & " AND " & vbCrLf & " A.Code=" & mBookCode & " AND " & vbCrLf & " TRN.AccountCode=B.Code(+) AND " & vbCrLf & " TRN.CostCCode=CostC.Code(+) AND " & vbCrLf & " TRN.DeptCode=Dept.Code(+) AND " & vbCrLf & " TRN.EmpCode=Emp.Code(+) AND " & vbCrLf & " VNO='" & mVNo & "' AND " & vbCrLf _
            & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf & " BookSubType='" & Mid(mBookType, 2, 1) & "' " & vbCrLf & " AND AccountCode<>" & mBookCode & ""


        mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo"

        SelectQryForDrCrVoucher = mSqlStr
    End Function
    Private Function SelectQryForVoucherNew(ByRef mSqlStr As String, ByRef pMKey As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String, ByRef pNarration As String, ByRef pBankName As String, ByRef pNetAmount As Double, ByRef pCurrBal As String, ByRef pDrCrNo As String, ByRef pAmountInWord As String, ByRef pCancelled As String) As String
        Dim mCustCode As String


        mSqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND IH.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
            & " ID.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
            & " IH.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
            & " ID.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
            & " IH.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
            & " IH.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
            & " IH.VNO='" & mVNo & "' AND " & vbCrLf _
            & " IH.MKEY='" & pMKey & "' AND " & vbCrLf _
            & " IH.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " IH.BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
            & " IH.BookSubType='" & Mid(mBookType, 2, 1) & "' "

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.BookCode='" & mBookCode & "' AND " & vbCrLf & " ID.AccountCode<>'" & mBookCode & "' "
        End If

        If frmPrintVoucher.optDNVoucher.Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY ID.DC DESC"
        Else
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY ID.SubRowNo"
        End If


        'mSqlStr = " SELECT TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
        '    & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
        '    & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
        '    & " TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf _
        '    & " A.SUPP_CUST_Name,B.SUPP_CUST_NAME, IDIV.DIV_ALIAS " & vbCrLf _
        '    & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
        '    & " WHERE " & vbCrLf _
        '    & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
        '    & " TRN.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
        '    & " TRN.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
        '    & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
        '    & " TRN.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
        '    & " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
        '    & " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
        '    & " VNO='" & mVNo & "' AND " & vbCrLf _
        '    & " MKEY='" & pMKey & "' AND " & vbCrLf _
        '    & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
        '    & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
        '    & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If

        'If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        'End If

        'If frmPrintVoucher.optDNVoucher.Checked = True Then
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.DC DESC"
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo, TRN.ACCOUNTCODE"
        'End If


        'mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',TRN.SUBROWNO, " & vbCrLf _
        '        & " TRN.VNO,TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
        '        & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
        '        & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
        '        & " A.SUPP_CUST_Name,B.SUPP_CUST_NAME ," & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(pNarration) & "','" & MainClass.AllowSingleQuote(pBankName) & "'," & vbCrLf _
        '        & " '" & Val(pNetAmount) & "'," & vbCrLf _
        '        & " '" & Val(pCurrBal) & "','" & MainClass.AllowSingleQuote(pDrCrNo) & "'," & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(pAmountInWord) & "','','','',''," & vbCrLf _
        '        & " '" & pCancelled & "','','','','','','','','','','' " & vbCrLf _
        '        & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
        '        & " WHERE " & vbCrLf _
        '        & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
        '        & " TRN.COMPANY_CODE=A.COMPANY_CODE(+) AND " & vbCrLf _
        '        & " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
        '        & " TRN.AccountCode=A.SUPP_CUST_CODE(+) AND " & vbCrLf _
        '        & " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
        '        & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE(+) AND " & vbCrLf _
        '        & " TRN.DIV_CODE=IDIV.DIV_CODE(+) AND" & vbCrLf _
        '        & " VNO='" & mVNo & "' AND " & vbCrLf _
        '        & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
        '        & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
        '        & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        ''mSqlStr = " SELECT TRN.MKEY, TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
        ''    & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
        ''    & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
        ''    & " TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf _
        ''    & " A.SUPP_CUST_Name,B.SUPP_CUST_NAME, IDIV.DIV_ALIAS " & vbCrLf _
        ''    & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
        ''    & " WHERE " & vbCrLf _
        ''    & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
        ''    & " TRN.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
        ''    & " TRN.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
        ''    & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
        ''    & " TRN.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
        ''    & " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
        ''    & " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
        ''    & " BookType='" & Mid(ConJournal, 1, 1) & "' AND " & vbCrLf _
        ''    & " BookSubType='" & Mid(ConJournal, 2, 1) & "' "

        ''If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        ''Else
        'mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''End If

        ''If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
        ''    mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        ''End If


        ''mSqlStr = mSqlStr & vbCrLf _
        ''        & " AND VDate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        ''        & " AND VDate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        'If optPrintRange(0).Checked = True Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '        & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        'ElseIf optPrintRange(1).Checked = True Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
        'End If


        'If optPartyName(1).Checked = True Then
        '    mCustCode = ""
        '    If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mCustCode = MasterNo
        '    End If

        '    mSqlStr = mSqlStr & vbCrLf _
        '            & " AND TRN.MKEY IN (SELECT DISTINCT MKEY " & vbCrLf _
        '            & " FROM FIN_POSTED_TRN" & vbCrLf _
        '            & " WHERE " & vbCrLf _
        '            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
        '            & " BookType='" & Mid(ConJournal, 1, 1) & "' AND " & vbCrLf _
        '            & " BookSubType='" & Mid(ConJournal, 2, 1) & "' AND ACCOUNTCODE ='" & MainClass.AllowSingleQuote(mCustCode) & "')"

        '    'mSqlStr = mSqlStr & vbCrLf & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"

        'End If

        'If frmPrintVoucher.optDNVoucher.Checked = True Then
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY  TRN.MKEY,TRN.DC DESC"
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY  TRN.MKEY,TRN.SubRowNo"
        'End If

        SelectQryForVoucherNew = mSqlStr

        'mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',TRN.SUBROWNO, " & vbCrLf & " TRN.VNO,TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
        '& " A.SUPP_CUST_Name,B.SUPP_CUST_NAME ," & vbCrLf _
        '& " '" & MainClass.AllowSingleQuote(pNarration) & "','" & MainClass.AllowSingleQuote(pBankName) & "'," & vbCrLf _
        '& " '" & Val(CStr(pNetAmount)) & "'," & vbCrLf & " '" & Val(pCurrBal) & "','" & MainClass.AllowSingleQuote(pDrCrNo) & "'," & vbCrLf _
        '& " '" & MainClass.AllowSingleQuote(pAmountInWord) & "','','','',''," & vbCrLf _
        '& " '" & pCancelled & "','','','','','','','','','','' " & vbCrLf _
        '& " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
        '& " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
        '& " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " TRN.COMPANY_CODE=A.COMPANY_CODE(+) AND " & vbCrLf _
        '& " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf & " TRN.AccountCode=A.SUPP_CUST_CODE(+) AND " & vbCrLf _
        '& " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE(+) AND " & vbCrLf _
        '& " TRN.DIV_CODE=IDIV.DIV_CODE(+) AND" & vbCrLf & " VNO='" & mVNo & "' AND " & vbCrLf _

        '    & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        'If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        'End If

        'If frmPrintVoucher.optDNVoucher.Checked = True Then
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.DC DESC"
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo"
        'End If

        SelectQryForVoucherNew = mSqlStr
    End Function

    Private Function SelectQryForVoucher(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String, ByRef pNarration As String, ByRef pBankName As String, ByRef pNetAmount As Double, ByRef pCurrBal As String, ByRef pDrCrNo As String, ByRef pAmountInWord As String, ByRef pCancelled As String) As String
        Dim mCustCode As String


        mSqlStr = " SELECT TRN.MKEY, TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
            & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
            & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
            & " TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf _
            & " A.SUPP_CUST_Name,B.SUPP_CUST_NAME, IDIV.DIV_ALIAS " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " TRN.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
            & " TRN.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
            & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
            & " TRN.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
            & " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
            & " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
            & " BookType='" & Mid(ConJournal, 1, 1) & "' AND " & vbCrLf _
            & " BookSubType='" & Mid(ConJournal, 2, 1) & "' "

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If

        'If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        'End If


        'mSqlStr = mSqlStr & vbCrLf _
        '        & " AND VDate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '        & " AND VDate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        If optPrintRange(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf optPrintRange(1).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf & " AND VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If


        If optPartyName(1).Checked = True Then
            mCustCode = ""
            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustCode = MasterNo
            End If

            mSqlStr = mSqlStr & vbCrLf _
                    & " AND TRN.MKEY IN (SELECT DISTINCT MKEY " & vbCrLf _
                    & " FROM FIN_POSTED_TRN" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
                    & " BookType='" & Mid(ConJournal, 1, 1) & "' AND " & vbCrLf _
                    & " BookSubType='" & Mid(ConJournal, 2, 1) & "' AND ACCOUNTCODE ='" & MainClass.AllowSingleQuote(mCustCode) & "')"

            'mSqlStr = mSqlStr & vbCrLf & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"

        End If

        If frmPrintVoucher.optDNVoucher.Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY  TRN.MKEY,TRN.DC DESC"
        Else
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY  TRN.MKEY,TRN.SubRowNo"
        End If

        SelectQryForVoucher = mSqlStr

        'mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',TRN.SUBROWNO, " & vbCrLf & " TRN.VNO,TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
        '& " A.SUPP_CUST_Name,B.SUPP_CUST_NAME ," & vbCrLf _
        '& " '" & MainClass.AllowSingleQuote(pNarration) & "','" & MainClass.AllowSingleQuote(pBankName) & "'," & vbCrLf _
        '& " '" & Val(CStr(pNetAmount)) & "'," & vbCrLf & " '" & Val(pCurrBal) & "','" & MainClass.AllowSingleQuote(pDrCrNo) & "'," & vbCrLf _
        '& " '" & MainClass.AllowSingleQuote(pAmountInWord) & "','','','',''," & vbCrLf _
        '& " '" & pCancelled & "','','','','','','','','','','' " & vbCrLf _
        '& " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
        '& " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
        '& " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " TRN.COMPANY_CODE=A.COMPANY_CODE(+) AND " & vbCrLf _
        '& " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf & " TRN.AccountCode=A.SUPP_CUST_CODE(+) AND " & vbCrLf _
        '& " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE(+) AND " & vbCrLf _
        '& " TRN.DIV_CODE=IDIV.DIV_CODE(+) AND" & vbCrLf & " VNO='" & mVNo & "' AND " & vbCrLf _

        '    & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        'If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        'End If

        'If frmPrintVoucher.optDNVoucher.Checked = True Then
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.DC DESC"
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo"
        'End If

        SelectQryForVoucher = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, Optional ByRef IsSubReport As Boolean = False)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        Dim SqlStrSub As String
        Dim mSelectionFormula As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        If lblBookType.Text = ConCashPayment Then
            mReceivedBy = "Receiver's Signature"
        Else
            mReceivedBy = " "
        End If
        MainClass.AssignCRptFormulas(Report1, "ReceivedBy=""" & mReceivedBy & """")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & Trim(mRptFileName)
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        If IsSubReport = True Then
            MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
            SqlStrSub = ""

            '        SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf _					
            ''                    & " FROM  FIN_DNCN_EXP, FIN_INTERFACE_MST " & vbCrLf _					
            ''                    & " WHERE " & vbCrLf _					
            ''                    & " FIN_DNCN_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _					
            ''                    & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _					
            ''                    & " ORDER BY SUBROWNO"					
            '					
            '              ''& " AND FIN_DNCN_EXP.MKEY='1'" & vbCrLf _					
            '					
            '' FIN_DNCN_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Caption) & "'					

            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_DNCN_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_DNCN_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND" & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IDENTIFICATION NOT IN ('CGS','SGS','IGS') AND GST_ENABLED='Y'"

            SqlStrSub = SqlStrSub & vbCrLf & " AND 1=2"

            SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""
        End If

        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReportForMail(ByRef mCondSqlStr As String, ByRef mSqlStr As String, ByRef mOrderSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, Optional ByRef IsSubReport As Boolean = False)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        Dim SqlStrSub As String
        Dim mSelectionFormula As String

        Dim crapp As New CRAXDRT.Application
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim objRpt As CRAXDRT.Report
        Dim fPath As String
        Dim mDNNo As String
        Dim Sqlstr As String
        Dim mSupplierCode As String
        Dim mSupplierName As String
        Dim empMailId As String
        Dim mNotOKCount As String
        'Dim mOKCount As String					

        mRptFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRptFileName

        Sqlstr = "SELECT DISTINCT CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_MAILID " & vbCrLf & mCondSqlStr

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mSupplierCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mSupplierName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                empMailId = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_MAILID").Value), "", RsTemp.Fields("SUPP_CUST_MAILID").Value)

                objRpt = crapp.OpenReport(mRptFileName)

                mSqlStr = mSqlStr & vbCrLf & " AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf & mOrderSqlStr

                Call Connect_Report_To_Database(objRpt, RS, mSqlStr)
                With objRpt
                    Call ClearCRpt8Formulas(objRpt)
                    .DiscardSavedData()
                    .Database.SetDataSource(RS)
                    SetCrpteMail(objRpt, 1, mTitle, mSubTitle)

                    If lblBookType.Text = ConCashPayment Then
                        mReceivedBy = "Receiver's Signature"
                    Else
                        mReceivedBy = " "
                    End If

                    AssignCRpt8Formulas(objRpt, "ReceivedBy", "'" & mReceivedBy & "'")
                    If IsSubReport = True Then
                        AssignCRpt8Formulas(objRpt, "COMPANYCINNo", "'" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & "'")
                        AssignCRpt8Formulas(objRpt, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")
                    End If

                    .VerifyOnEveryPrint = True
                End With

                fPath = mLocalPath & "\eDnCn" & mSupplierCode & ".pdf"

                With objRpt
                    .ExportOptions.FormatType = CRAXDDRT.CRExportFormatType.crEFTPortableDocFormat
                    .ExportOptions.DestinationType = CRAXDDRT.CRExportDestinationType.crEDTDiskFile
                    .ExportOptions.DiskFileName = fPath
                    '    .ExportOptions.PDFExportAllPages = True					
                    .Export(False)
                End With

                '   Set objRpt = crapp.CanClose					
                objRpt = Nothing

                If empMailId = "" Or fPath = "" Then
                    mNotOKCount = IIf(mNotOKCount = "", mSupplierName, mNotOKCount & "," & vbCrLf & mSupplierName)
                Else
                    If SendeMail(fPath, empMailId) = False Then GoTo ErrPart
                    '                mOKCount = mOKCount + 1					
                End If
                RsTemp.MoveNext()
            Loop
        End If

        If mNotOKCount <> "" Then
            MsgInformation("Mail is Not send to - " & mNotOKCount & ", because of Mail ID not defined.")
        End If

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function SendeMail(ByRef mAttachmentFile As String, ByRef mTo As String) As Boolean
        On Error GoTo ErrPart

        Dim mCC As String
        Dim mFrom As String
        Dim mSubject As String


        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String
        'Dim mFrom As String					
        'Dim mCC As String					
        'Dim mTo As String					

        SendeMail = False

        ' *****************************************************************************					
        ' This is where all of the Components Properties are set / Methods called					
        ' *****************************************************************************					

        strServerPop3 = GetEMailID("POP_ID")
        strServerSmtp = GetEMailID("SMTP_ID")
        strAccount = GetEMailID("MAIL_ACCOUNT")
        strPassword = GetEMailID("PASSWORD")
        mFrom = GetEMailID("ACCT_MAIL_TO")
        '    mTo = "bbj@hemaengineering.com"					
        mCC = GetEMailID("ACCT_MAIL_TO")

        mSubject = "Auto Generated Mail."


        mBodyText = "<html><body><br />" & "<b></b>" & mSubject & "<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"

        If strServerPop3 = "" And strServerSmtp = "" And strAccount = "" And strPassword = "" Then
            MsgBox("Please Check Email Configuration", MsgBoxStyle.Information)
            '                SendMail = False					
            Exit Function
        End If
        If Trim(mTo) <> "" Then
            If SendMailProcessNew(mFrom, mTo, mCC, "", strAccount, strPassword, mAttachmentFile, "", "", mSubject, "", mBodyText, "") = False Then GoTo ErrPart
        End If

        SendeMail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        SendeMail = False
        '    Resume					
    End Function

    Private Function GETDRCRNo(ByRef mVNo As String, ByRef mVDate As Date, ByRef xBookCode As String, ByRef lAccountCode As String, ByRef pAsRemarks As Boolean) As String

        On Error GoTo ErrPart
        Dim pDrCrNo As String
        Dim RS As ADODB.Recordset
        Dim Sqlstr As String
        'Dim mVNo As String					
        'Dim xBookCode As String					
        'Dim lAccountCode As String					
        '        mVNo = txtVType.Text & txtVNo1.Text & txtVNo.Text					
        '        If Me.lblBookType.Caption <> ConJournal And Me.lblBookType.Caption <> ConContra Then					
        '            MainClass.ValidateWithMasterTable Me.txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""					
        '            xBookCode = MasterNo					
        '        End If					
        '					
        '        SprdMain.Row = 1					
        '        SprdMain.Col = ColAccountName					
        '        MainClass.ValidateWithMasterTable SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""					
        '        lAccountCode = MasterNo					

        Sqlstr = " SELECT DISTINCT TRN.VNO " & vbCrLf & " FROM FIN_POSTED_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " (TRN.BookType='" & ConDebitNoteBook & "' OR TRN.BookType='" & ConCreditNoteBook & "') AND " & vbCrLf & " AccountCode='" & lAccountCode & "' AND TRN.BILLNO IN (SELECT BILLNO FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND  "

        Sqlstr = Sqlstr & vbCrLf & " VNO='" & mVNo & "'"


        Sqlstr = Sqlstr & vbCrLf & " AND VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " BookType='" & Mid(lblBookType.Text, 1, 1) & "' AND " & vbCrLf & " BookSubType='" & Mid(lblBookType.Text, 2, 1) & "' AND " & vbCrLf & " BOOKCODE='" & xBookCode & "' AND " & vbCrLf & " AccountCode<>'" & xBookCode & "') "
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                If pAsRemarks = True Then
                    pDrCrNo = IIf(pDrCrNo = "", "", pDrCrNo & ", ") & IIf(IsDBNull(RS.Fields("VNO").Value), "", RS.Fields("VNO").Value)
                Else
                    pDrCrNo = IIf(pDrCrNo = "", "", pDrCrNo & ", ") & IIf(IsDBNull(RS.Fields("VNO").Value), "", "'" & RS.Fields("VNO").Value & "'")
                End If
                RS.MoveNext()
            Loop
        End If

        GETDRCRNo = pDrCrNo
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GETDRCRNo = ""
    End Function


    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConDebitNote Or lblBookType.Text = ConCreditNote Then
            Call ReportOnDrCr(False, "", "P")
        ElseIf lblBookType.Text = ConSaleDebit Then
            Call ReportOnCustomerDebit("P")
        ElseIf lblBookType.Text = ConPurchase Then
            Call ReportOnSaleReturn("P")
        Else
            Call ReportOnTrnVoucher("P")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnDrCr(ByRef pFromBankPayment As Boolean, ByRef pDnCnNo As String, ByRef pMode As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim Sqlstr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Dim Mode As Crystal.DestinationConstants
        Dim mCondSqlStr As String
        Dim mOrderSqlStr As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        If pMode = "M" Then
            Mode = Crystal.DestinationConstants.crptToWindow
        ElseIf pMode = "P" Then
            Mode = Crystal.DestinationConstants.crptToPrinter
        Else
            Mode = Crystal.DestinationConstants.crptToWindow
        End If

        Sqlstr = ""
        '    mVNo = txtVNoPrefix.Text & Trim(txtVType.Text) & txtVNo.Text & txtVNoSuffix					

        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)


        If pFromBankPayment = True Then
            Call SelectQryForDNCNVoucherFromBP(pDnCnNo, Sqlstr)

            mTitle = "Debit Note"
            If chkRejectionDnCN.CheckState = System.Windows.Forms.CheckState.Checked Then
                mRptFileName = "DrNote_GST_Rej.rpt"
            Else
                mRptFileName = "DrNote_GST.rpt"
            End If
        Else

            Call SelectQryForDNCNVoucher(mCondSqlStr, Sqlstr, mOrderSqlStr)

            If lblBookType.Text = ConDebitNote Then
                mTitle = "Debit Note"
                If chkRejectionDnCN.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRptFileName = "DrNote_GST_Rej.rpt"
                Else
                    mRptFileName = "DrNote_GST.rpt"
                End If
            Else
                mTitle = "Credit Note"
                If chkRejectionDnCN.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRptFileName = "CrNote_GST_Rej.rpt"
                Else
                    mRptFileName = "CrNote_GST.rpt"
                End If
            End If
        End If

        If pMode = "M" Then
            Call ShowReportForMail(mCondSqlStr, Sqlstr, mOrderSqlStr, Mode, mTitle, mSubTitle, mRptFileName, True)
        Else
            Call ShowReport(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName, True)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDNCNVoucher(ByRef mCondSqlStr As String, ByRef mSqlStr As String, ByRef mOrderSqlStr As String) As String
        Dim mSuppCode As String

        ''''SELECT CLAUSE...					

        mCondSqlStr = ""

        mSqlStr = " SELECT " & vbCrLf & " IH.VNOPREFIX, IH.VNOSEQ, IH.VNOSUFFIX," & vbCrLf & " IH.VNO, IH.VDATE, IH.PURVNO, IH.PURVDATE," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.AUTO_KEY_MRR, IH.MRRDATE," & vbCrLf & " IH.DEBITACCOUNTCODE, IH.CREDITACCOUNTCODE," & vbCrLf & " IH.REMARKS, IH.REASON, IH.NARRATION, IH.DNCNTYPE,"

        mSqlStr = mSqlStr & " ID.SUBROWNO, ID.ITEM_CODE, " & vbCrLf & " ID.ITEM_DESC, " & vbCrLf & " ID.ITEM_QTY, ID.ITEM_UOM, " & vbCrLf & " ID.ITEM_RATE, ID.ITEM_AMT, "

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''''FROM CLAUSE...					
        mCondSqlStr = " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST"

        ''''WHERE CLAUSE...					
        mCondSqlStr = mCondSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND IH.BOOKSUBTYPE='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf & " AND IH.APPROVED='Y'" & vbCrLf & " AND IH.MKEY=ID.MKEY(+)" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE"

        mCondSqlStr = mCondSqlStr & vbCrLf _
            & " AND CMST.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID"

        If chkOnlyPendingDNCN.CheckState = System.Windows.Forms.CheckState.Checked Then
            mCondSqlStr = mCondSqlStr & vbCrLf & " AND (PARTY_DNCN_NO='' OR PARTY_DNCN_NO IS NULL)"
        End If

        If chkRejectionDnCN.CheckState = System.Windows.Forms.CheckState.Checked Then
            mCondSqlStr = mCondSqlStr & vbCrLf & "AND DNCNTYPE='R'"
        Else
            mCondSqlStr = mCondSqlStr & vbCrLf & "AND DNCNTYPE<>'R'"
        End If

        If optPrintRange(0).Checked = True Then
            mCondSqlStr = mCondSqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            mCondSqlStr = mCondSqlStr & vbCrLf & " AND IH.VNO>='" & Trim(txtVNoFrom.Text) & "' " & vbCrLf & " AND IH.VNO<='" & Trim(txtVNoTo.Text) & "' "
        End If

        If lblBookType.Text = ConDebitNote Then
            mCondSqlStr = mCondSqlStr & vbCrLf & " AND IH.DEBITACCOUNTCODE=CMST.SUPP_CUST_CODE"
        Else
            mCondSqlStr = mCondSqlStr & vbCrLf & " AND IH.CREDITACCOUNTCODE=CMST.SUPP_CUST_CODE"
        End If

        If optPartyName(1).Checked = True Then
            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                mCondSqlStr = mCondSqlStr & vbCrLf & " AND CMST.SUPP_CUST_CODE='" & mSuppCode & "'"
            End If
        End If

        mSqlStr = mSqlStr & vbCrLf & mCondSqlStr
        ''''ORDER CLAUSE...					

        If optOrderBy(0).Checked = True Then
            mOrderSqlStr = " ORDER BY IH.VNO,ID.SUBROWNO"
        Else
            mOrderSqlStr = " ORDER BY CMST.SUPP_CUST_NAME,IH.VNO,ID.SUBROWNO"
        End If


        SelectQryForDNCNVoucher = mSqlStr
    End Function
    Private Function SelectQryForDNCNVoucherFromBP(ByRef pDnCnNo As String, ByRef mSqlStr As String) As String


        If Trim(pDnCnNo) = "" Then
            SelectQryForDNCNVoucherFromBP = ""
            Exit Function
        End If

        pDnCnNo = "(" & pDnCnNo & ")"

        ''''SELECT CLAUSE...					

        mSqlStr = " SELECT " & vbCrLf & " IH.VNOPREFIX, IH.VNOSEQ, IH.VNOSUFFIX," & vbCrLf & " IH.VNO, IH.VDATE, IH.PURVNO, IH.PURVDATE," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.AUTO_KEY_MRR, IH.MRRDATE," & vbCrLf & " IH.DEBITACCOUNTCODE, IH.CREDITACCOUNTCODE," & vbCrLf & " IH.REMARKS, IH.REASON, IH.NARRATION, IH.DNCNTYPE,"

        mSqlStr = mSqlStr & " ID.SUBROWNO, ID.ITEM_CODE, " & vbCrLf & " ID.ITEM_DESC, " & vbCrLf & " ID.ITEM_QTY, ID.ITEM_UOM, " & vbCrLf & " ID.ITEM_RATE, ID.ITEM_AMT, "

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''''FROM CLAUSE...					
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST"

        ''''WHERE CLAUSE...					
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.BOOKTYPE='" & VB.Left(ConDebitNote, 1) & "'" & vbCrLf & " AND IH.BOOKSUBTYPE='" & VB.Right(ConDebitNote, 1) & "'" & vbCrLf & " AND IH.APPROVED='Y'" & vbCrLf & " AND IH.MKEY=ID.MKEY(+)" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.VNO IN " & pDnCnNo

        '    If lblBookType.Caption = ConDebitNote Then					
        mSqlStr = mSqlStr & vbCrLf & " AND IH.DEBITACCOUNTCODE=CMST.SUPP_CUST_CODE"
        '    Else					
        '        mSqlStr = mSqlStr & vbCrLf & " AND IH.CREDITACCOUNTCODE=CMST.SUPP_CUST_CODE"					
        '    End If					

        If chkRejectionDnCN.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSqlStr = mSqlStr & vbCrLf & "AND DNCNTYPE='R'"
        Else
            mSqlStr = mSqlStr & vbCrLf & "AND DNCNTYPE<>'R'"
        End If

        ''''ORDER CLAUSE...					


        If optOrderBy(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.VNO,ID.SUBROWNO"
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,IH.VNO,ID.SUBROWNO"
        End If

        SelectQryForDNCNVoucherFromBP = mSqlStr
    End Function

    Private Sub cmdSearchParty_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchParty.Click
        On Error GoTo ErrPart
        Dim Sqlstr As String

        Sqlstr = Sqlstr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMaster((txtPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , Sqlstr) = True Then
            txtPartyName.Text = AcName
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub cmdsearchVNO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchVNO.Click
        Dim Index As Short = cmdsearchVNO.GetIndex(eventSender)
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim mTable As String

        Sqlstr = Sqlstr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "' " & vbCrLf & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "' "

        If lblBookType.Text = ConDebitNote Or lblBookType.Text = ConCreditNote Then
            mTable = "FIN_DNCN_HDR"
        Else
            mTable = "FIN_VOUCHER_HDR"
        End If

        ''If MainClass.SearchMaster(IIf(Index = 0, txtVNoFrom.Text, txtVNoTo.Text), mTable, "VNO", SqlStr) = True Then					
        If MainClass.SearchGridMaster(IIf(Index = 0, txtVNoFrom.Text, txtVNoTo.Text), mTable, "VNO", "VDATE",  ,  , Sqlstr) = True Then
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


    Private Sub frmPrintMultiVouch_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					

        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(5010)
        Me.Width = VB6.TwipsToPixelsX(6420)


        cboVoucher.Items.Clear()

        'cboVoucher.Items.Add("Cash Receipt")
        'cboVoucher.Items.Add("Cash Payment")
        'cboVoucher.Items.Add("Bank Receipt")
        'cboVoucher.Items.Add("Bank Payment")
        ''cboVoucher.AddItem "Contra Entry"					
        'cboVoucher.Items.Add("PDC Receipt")
        'cboVoucher.Items.Add("PDC Payment")
        cboVoucher.Items.Add("Journal")
        cboVoucher.Items.Add("1. Purchase Entry (Goods Order)")
        cboVoucher.Items.Add("2. Purchase Entry (Sale Return) Agt Debit Note")
        cboVoucher.Items.Add("3. Purchase Entry (Ship)")
        cboVoucher.Items.Add("4. Purchase Entry (Jobwork Order)")
        cboVoucher.Items.Add("5. Purchase Entry (Repair)")
        cboVoucher.Items.Add("6. Purchase Entry (Work Order)")
        cboVoucher.Items.Add("7. Purchase Entry (Service)")
        cboVoucher.Items.Add("8. Purchase Entry (Sale Return) Agt Invoice")
        cboVoucher.Items.Add("9. Purchase Entry (Under Challan)")

        cboVoucher.Items.Add("Debit Note")
        cboVoucher.Items.Add("Credit Note")
        ''cboVoucher.AddItem "Sale"					
        'cboVoucher.Items.Add("Customer Debit Note")
        'cboVoucher.Items.Add("Sale Return")



        cboVoucher.SelectedIndex = 0

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        optPartyName(0).Checked = True
        cmdSearchParty.Enabled = False
        txtPartyName.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub optPartyName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPartyName.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPartyName.GetIndex(eventSender)
            txtPartyName.Enabled = IIf(Index = 0, False, True)
            cmdSearchParty.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub

    Private Sub optPrintRange_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPrintRange.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPrintRange.GetIndex(eventSender)
            FraDateRange.Enabled = IIf(Index = 0, True, False)
            FraVNoRange.Enabled = IIf(Index = 1, True, False)
        End If
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Cancel = True : txtDateFrom.Focus() : GoTo EventExitSub
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then Cancel = True : txtDateFrom.Focus() : GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then Cancel = True : txtDateTo.Focus() : GoTo EventExitSub
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then Cancel = True : txtDateTo.Focus() : GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        Call cmdSearchParty_Click(cmdSearchParty, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchParty_Click(cmdSearchParty, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
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
End Class
