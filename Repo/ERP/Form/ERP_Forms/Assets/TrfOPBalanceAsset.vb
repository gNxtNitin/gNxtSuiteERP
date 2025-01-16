Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTrfOPBalanceAsset
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCN As ADODB.Connection						

    Dim mLastFYDateFrom As String
    Dim mLastFYDateTo As String
    Dim mLastFYNo As Integer

    Dim mCurrFYDateFrom As String
    Dim mCurrFYDateTo As String
    Dim mCurrFYNo As Integer

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
        'Set PvtDBCN = Nothing						
    End Sub
    Sub TopDisplayTransfer(ByRef MsgStr As String)
        TxtDisplayTransfer(0).SelectedText = MsgStr & vbCrLf
        TxtDisplayTransfer(0).SelectionLength = Len(MsgStr)
    End Sub
    Sub BottomDisplayTransfer(ByRef MsgStr As String)
        TxtDisplayTransfer(1).SelectedText = MsgStr & vbCrLf
        TxtDisplayTransfer(1).SelectionLength = Len(MsgStr)
    End Sub
    Sub MakeTxtDisplayTransferVisible()
        TxtDisplayTransfer(0).Width = VB6.TwipsToPixelsX(5085)
        TxtDisplayTransfer(1).Width = VB6.TwipsToPixelsX(5025)
        TxtDisplayTransfer(0).Height = VB6.TwipsToPixelsY(2835)
        TxtDisplayTransfer(1).Height = VB6.TwipsToPixelsY(1725)
        TxtDisplayTransfer(0).Top = VB6.TwipsToPixelsY(1710)
        TxtDisplayTransfer(1).Top = VB6.TwipsToPixelsY(2790)
        TxtDisplayTransfer(0).Left = 0
        TxtDisplayTransfer(1).Left = VB6.TwipsToPixelsX(30)
        TxtDisplayTransfer(0).Visible = True
        TxtDisplayTransfer(1).Visible = True
        TxtDisplayTransfer(0).Text = ""
        TxtDisplayTransfer(1).Text = ""
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String
        SqlStr = ""
        'UPGRADE_WARNING: Untranslated statement in cmdsearch_Click. Please check source code.						
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub
    Private Function FieldVarification() As Boolean
        On Error GoTo FieldErr
        If Trim(CboFYearFrom.Text) = "" Then
            MsgBox("FYearFrom Not Selected....")
            Exit Function
        End If
        If Trim(CboFYearTo.Text) = "" Then
            MsgBox("FYearTo Not Selected....")
            Exit Function
        End If

        mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))
        mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))


        If mLastFYNo + 1 <> mCurrFYNo Then
            MsgBox("Invalid FYearFrom & FYearTo ....")
            Exit Function
        End If
        If OptParticularAccount.Checked = True Then
            'UPGRADE_WARNING: Untranslated statement in FieldVarification. Please check source code.						
        End If

        If Trim(txtDeprMode.Text) = "" Then
            MsgInformation("Mode of Depreciation id Blank.")
            Exit Function
        End If

        'UPGRADE_WARNING: Untranslated statement in FieldVarification. Please check source code.						

        FieldVarification = True
        Exit Function
FieldErr:
        FieldVarification = False
        MsgBox(Err.Description)
    End Function

    Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click
        On Error GoTo ERR1
        Dim mAccountCode As String
        Dim mToAccountCode As String
        Dim mPnlAccountCode As String
        Dim SqlStr As String
        Dim mTable As String
        Dim mGroupCode As String

        If FieldVarification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mLastFYDateFrom = Mid(CboFYearFrom.Text, 8, 12)
        mLastFYDateTo = Mid(CboFYearFrom.Text, 21, 28)
        mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))

        mCurrFYDateFrom = Mid(CboFYearTo.Text, 8, 12)
        mCurrFYDateTo = Mid(CboFYearTo.Text, 21, 28)
        mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))

        MakeTxtDisplayTransferVisible()
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Assets Opening Balance From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Please Wait........")
        TopDisplayTransfer(New String("=", 37))

        mTable = "AST_OP_TRN" & mCurrFYNo

        If OptParticularAccount.Checked = True Then
            'UPGRADE_WARNING: Untranslated statement in cmdStart_Click. Please check source code.						

            'UPGRADE_WARNING: Untranslated statement in cmdStart_Click. Please check source code.						

            SqlStr = " DELETE From " & mTable & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND DESP_MODE='" & txtDeprMode.Text & "'" & vbCrLf & " AND TRNCODE='" & mGroupCode & "'"
            PubDBCn.Execute(SqlStr)
        Else
            SqlStr = " DELETE From " & mTable & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND DESP_MODE='" & txtDeprMode.Text & "' "
            PubDBCn.Execute(SqlStr)

        End If

        '    If TransferBalance(SqlStr) = False Then GoTo ERR1						
        If InsertDataInTempFile(mCurrFYDateFrom, mCurrFYDateTo, mLastFYDateFrom, mLastFYDateTo, mCurrFYNo, mLastFYNo) = False Then GoTo ERR1


        SqlStr = "INSERT INTO " & mTable & " ( " & vbCrLf & " COMPANY_CODE, COMPANY_NAME, FYEAR, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, TRNCODE, " & vbCrLf & " TRNNAME, SUPP_CUST_NAME, ITEM_DESC, " & vbCrLf & " AUTO_KEY_ASSET, LOCATION, PV_DATE, " & vbCrLf & " TOTAL_COST, OP_GROSS_BLOCK, DAYS, " & vbCrLf & " PUR_YEAR, CURRENT_DESP, CUMULATIVE_DESP, " & vbCrLf & " SALE_AMOUNT, SALE_DATE, SALE_DESP, " & vbCrLf & " PHY_DATE, PHY_WHOM, GROSS_BLOCK, " & vbCrLf & " NET_BLOCK, ITEM_TYPE, ASSET_TYPE, " & vbCrLf & " PUT_DATE, DESP_MODE )"

        SqlStr = SqlStr & vbCrLf & " SELECT  " & vbCrLf & " COMPANY_CODE, COMPANY_NAME, FYEAR, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, TRNCODE, " & vbCrLf & " TRNNAME, SUPP_CUST_NAME, ITEM_DESC, " & vbCrLf & " AUTO_KEY_ASSET, LOCATION, PV_DATE, " & vbCrLf & " TOTAL_COST, OP_GROSS_BLOCK, DAYS, " & vbCrLf & " PUR_YEAR, CURRENT_DESP, CUMULATIVE_DESP, " & vbCrLf & " SALE_AMOUNT, SALE_DATE, SALE_DESP, " & vbCrLf & " PHY_DATE, PHY_WHOM, GROSS_BLOCK, " & vbCrLf & " NET_BLOCK, ITEM_TYPE, ASSET_TYPE, " & vbCrLf & " PUT_DATE, '" & txtDeprMode.Text & "' " & vbCrLf & " FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        PubDBCn.Execute(SqlStr)

        '    If ChkTrading.Value = vbChecked Then						
        '        If UpdatePnL = False Then GoTo Err1						
        '    End If						

        TxtDisplayTransfer(1).Text = ""
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Account Balances From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Account Balances Transfer Done Successfully.")
        TopDisplayTransfer(New String("=", 37))

        cmdStart.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        If Err.Number = 7 Then
            TxtDisplayTransfer(1).Text = ""
            Resume
        End If
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Account Balances From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Account Balances Transfer Failed.........")
        TopDisplayTransfer(New String("=", 37))
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        '   Resume						
    End Sub
    Private Function TransferBalance(ByRef RsSqlStr As String) As Boolean
        'On Error GoTo UpdateErr						
        'Dim RsOPBal As ADODB.Recordset						
        'Dim RsOPBalSumm As ADODB.Recordset						
        'Dim mAccountCode As String						
        'Dim mAccountName As String						
        'Dim mFYDateTo As Date						
        'Dim SqlStr As String						
        '						
        'Dim pLastFYNo As Long						
        'Dim pCurrFyNo As Long						
        'Dim mBalanceAmount As Double						
        'Dim pCurrFYDateFrom As String						
        '						
        'Dim mTRNType As String						
        'Dim cntRow As Long						
        '						
        'Dim mMkey As String						
        'Dim mCostCCode As String						
        'Dim mBillNo As String						
        'Dim mBillDate As String						
        'Dim mVNo As String						
        'Dim mVDate As String						
        'Dim mAmount As Double						
        'Dim mDC As String						
        'Dim mDueDate As String						
        'Dim mVType As String						
        'Dim mBillType As String						
        'Dim mHeadType As String						
        '						
        '    pLastFYNo = CLng(Left(CboFYearFrom.Text, 4))						
        '    pCurrFyNo = CLng(Left(CboFYearTo.Text, 4))						
        '    pCurrFYDateFrom = Mid(CboFYearTo.Text, 8, 12)						
        '						
        '    TransferBalance = False						
        '    PubDBCn.Errors.Clear						
        '    PubDBCn.BeginTrans						
        '						
        '    mFYDateTo = Mid(CboFYearFrom.Text, 21, 28)						
        '						
        '    MainClass.UOpenRecordSet RsSqlStr, PubDBCn, adOpenStatic, RsOPBal, adLockReadOnly						
        '						
        '    Do While Not RsOPBal.EOF = True						
        '        If RsOPBal.EOF = False Then						
        '            mAccountCode = IIf(IsNull(RsOPBal!SUPP_CUST_CODE), -1, RsOPBal!SUPP_CUST_CODE)						
        '            mAccountName = IIf(IsNull(RsOPBal!SUPP_CUST_NAME), "", RsOPBal!SUPP_CUST_NAME)						
        '            mHeadType = IIf(IsNull(RsOPBal!HEADTYPE), "", RsOPBal!HEADTYPE)						
        '            BottomDisplayTransfer mAccountCode & " - " & mAccountName						
        '						
        '            If mAccountCode <> "-1" Then						
        '                If mHeadType = "P" Then						
        '                    SqlStr = " Select SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf _						
        ''                                & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP" & vbCrLf _						
        ''                                & " WHERE TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _						
        ''                                & " AND TRN.FYEAR=" & mLastFYNo & " " & vbCrLf _						
        ''                                & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _						
        ''                                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf _						
        ''                                & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf _						
        ''                                & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE" & vbCrLf _						
        ''                                & " AND ACMGROUP.GROUP_TYPE='E' " & vbCrLf _						
        ''                                & " HAVING SUM(DECODE(DC,'D',1,-1)*Amount) <>0 "						
        '						
        '                        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOPBalSumm, adLockReadOnly						
        '						
        '                        If RsOPBalSumm.EOF = False Then						
        '                            mBalanceAmount = IIf(IsNull(RsOPBalSumm!BALANCE), 0, RsOPBalSumm!BALANCE)						
        '                            mBillNo = "OP"						
        '                            mBillDate = mLastFYDateTo						
        '                            mDC = IIf(mBalanceAmount >= 0, "D", "C")						
        '                            mTRNType = "B"						
        '                            mCostCCode = "-1"						
        '                            mBillType = "B"						
        '						
        '                            mVType = "OO"						
        '                            mMkey = mAccountCode						
        '                            mVNo = "OP"						
        '                            mVDate = mLastFYDateTo						
        '						
        '                            If mBalanceAmount <> 0 Then						
        '                                If UpdateTRFTRN(PubDBCn, mMkey, 1, 1, ConOpeningBookCode, mVType, Left(ConOpening, 1), Right(ConOpening, 1), mAccountCode, mVNo, mVDate, mBillNo, mBillDate, mBalanceAmount, mDC, mTRNType, "", "", mCostCCode, "-1", "-1", mDueDate, "", mBillType, "", "", "Opening :", "", mBillDate, pCurrFyNo) = False Then GoTo UpdateErr						
        '                            End If						
        '                        End If						
        '                Else						
        '                    If GetAccountBalancingMethod(mAccountName) = "S" Then						
        '						
        '                        SqlStr = " Select  ACCOUNTCODE," & vbCrLf _						
        ''                                & " MAX(DUEDATE) AS DUEDATE, " & vbCrLf _						
        ''                                & " SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf _						
        ''                                & " FROM FIN_POSTED_TRN TRN " & vbCrLf _						
        ''                                & " WHERE TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _						
        ''                                & " AND TRN.FYEAR=" & mLastFYNo & " " & vbCrLf _						
        ''                                & " AND ACCOUNTCODE='" & mAccountCode & "' " & vbCrLf _						
        ''                                & " HAVING SUM(DECODE(DC,'D',1,-1)*Amount) <>0 " & vbCrLf _						
        ''                                & " GROUP BY " & vbCrLf _						
        ''                                & " ACCOUNTCODE "						
        '						
        '                        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOPBalSumm, adLockReadOnly						
        '						
        '                        If RsOPBalSumm.EOF = False Then						
        '                            mBalanceAmount = IIf(IsNull(RsOPBalSumm!BALANCE), 0, RsOPBalSumm!BALANCE)						
        '                            mBillNo = "OP"						
        '                            mBillDate = mLastFYDateTo						
        '                            mDC = IIf(mBalanceAmount >= 0, "D", "C")						
        '                            mTRNType = "B"						
        '                            mCostCCode = "-1"						
        '                            mBillType = "B"						
        '						
        '                            mVType = "OO"						
        '                            mMkey = mAccountCode						
        '                            mVNo = "OP"						
        '                            mVDate = mLastFYDateTo						
        '						
        '                            If mBalanceAmount <> 0 Then						
        '                                If UpdateTRFTRN(PubDBCn, mMkey, 1, 1, ConOpeningBookCode, mVType, Left(ConOpening, 1), Right(ConOpening, 1), mAccountCode, mVNo, mVDate, mBillNo, mBillDate, mBalanceAmount, mDC, mTRNType, "", "", mCostCCode, "-1", "-1", mDueDate, "", mBillType, "", "", "Opening :", "", mBillDate, pCurrFyNo) = False Then GoTo UpdateErr						
        '                            End If						
        '                        End If						
        '                    Else						
        '                        If UpdateTableDetailAccount(mAccountCode) = False Then GoTo UpdateErr						
        '                    End If						
        '                End If						
        '            End If						
        '        End If						
        '        RsOPBal.MoveNext						
        '    Loop						
        '						
        '    PubDBCn.CommitTrans						
        '    TransferBalance = True						
        '    Exit Function						
        'UpdateErr:						
        ''    Resume						
        '    If err.Number = 7 Then						
        '        TxtDisplayTransfer(1).Text = ""						
        '        Resume Next						
        '    End If						
        '    BottomDisplayTransfer "AccountCode..." & mAccountCode & " Transfer Failed..."						
        '    PubDBCn.RollbackTrans						
        '    TransferBalance = False						
        '    If err.Number <> 0 Then						
        '        MsgInformation err.Description						
        '    End If						
        ''    Resume						
    End Function


    Private Function InsertDataInTempFile(ByRef mCurrFYDateFrom As String, ByRef mFYEndDate As String, ByRef mLastFYDateFrom As String, ByRef mLastFYDateTo As String, ByRef pCurrentFyear As Integer, ByRef mLastFYNo As Integer) As Boolean
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mGroupCode As String
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAccountCode As String
        Dim mAllCheck As Boolean
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mSqlStr As String
        Dim mDepreMode As String

        mDepreMode = Trim(txtDeprMode.Text)

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        mSqlStr = "INSERT INTO TEMP_AST_DESP_TRN (" & vbCrLf & " USERID, COMPANY_CODE, COMPANY_NAME, " & vbCrLf & " FYEAR, BOOKTYPE, BOOKSUBTYPE, " & vbCrLf & " TRNCODE, TRNNAME, SUPP_CUST_NAME, ITEM_DESC, " & vbCrLf & " AUTO_KEY_ASSET, LOCATION, PV_DATE, PUT_DATE," & vbCrLf & " TOTAL_COST, DAYS, PUR_YEAR, " & vbCrLf & " CURRENT_DESP, CUMULATIVE_DESP, SALE_AMOUNT, " & vbCrLf & " SALE_DATE, SALE_DESP, PHY_DATE, " & vbCrLf & " PHY_WHOM, GROSS_BLOCK, NET_BLOCK, " & vbCrLf & " ITEM_TYPE, ASSET_TYPE,OP_GROSS_BLOCK)"


        SqlStr = " SELECT  '" & MainClass.AllowSingleQuote(PubUserID) & "', TRN.COMPANY_CODE, GEN.COMPANY_NAME," & vbCrLf & " TRN.FYEAR, TRN.BOOKTYPE, '1'," & vbCrLf & " INVMST.CODE, INVMST.NAME, TRN.SUPP_CUST_NAME, TRN.ITEM_DESC, " & vbCrLf & " TRN.AUTO_KEY_ASSET, TRN.LOCATION, TRN.PV_DATE, TRN.PUT_DATE," & vbCrLf & " TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND), 0, TRN.FYEAR," & vbCrLf & " 0, 0, 0, " & vbCrLf & " '', 0, ''," & vbCrLf & " '', 0, 0, " & vbCrLf & " TRN.ITEM_TYPE, TRN.AST_TYPE,0"

        SqlStr = SqlStr & vbCrLf & " FROM AST_ASSET_TRN TRN, FIN_INVTYPE_MST INVMST, GEN_COMPANY_MST GEN"

        ''''WHERE CLAUSE...						
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND TRN.GROUP_CODE=INVMST.CODE"


        SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_CODE='" & MainClass.AllowSingleQuote(RsCompany.Fields("COMPANY_CODE")) & "'"


        If OptParticularAccount.Checked = True Then
            If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='F'") = True Then
                mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            'If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mGroupCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            'End If
            'SqlStr = SqlStr & vbCrLf & "AND GROUP_CODE='" & MainClass.AllowSingleQuote(mGroupCode) & "'"
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        '    If optOption(1).Value = True Then						
        '        SqlStr = SqlStr & vbCrLf & " AND ABS(GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') + " & vbCrLf _						
        ''                        & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') + " & vbCrLf _						
        ''                        & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') + " & vbCrLf _						
        ''                        & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') ) " & vbCrLf _						
        ''                        & " - ABS(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND))<>0"						
        '    ElseIf optOption(2).Value = True Then						
        '        SqlStr = SqlStr & vbCrLf & " AND ABS(GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') + " & vbCrLf _						
        ''                        & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') + " & vbCrLf _						
        ''                        & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') + " & vbCrLf _						
        ''                        & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & vb6.Format(lblFYStartDate.Caption, "DD-MMM-YYYY") & "') ) " & vbCrLf _						
        ''                        & " = ABS(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND))"						
        '    End If						

        SqlStr = SqlStr & vbCrLf & " AND TRN.CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND PUT_DATE < TO_DATE('" & VB6.Format(mCurrFYDateFrom, "DD-MMM-YYYY") & "')"

        '    If chkRefNo.Value = vbUnchecked And Val(txtRefNo.Text) <> 0 Then						
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.AUTO_KEY_ASSET=" & Val(txtRefNo.Text) & ""						
        '    End If						
        '						


        SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR,TRN.PUT_DATE,TRN.PV_NO"

        mSqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(mSqlStr)

        If UpdateTempTable(mDepreMode, mCurrFYDateFrom, mCurrFYDateTo, mLastFYDateFrom, mLastFYDateTo, pCurrentFyear) = False Then GoTo LedgError

        PubDBCn.CommitTrans()
        InsertDataInTempFile = True
        Exit Function
LedgError:
        '    Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertDataInTempFile = False
        PubDBCn.RollbackTrans()

    End Function
    Private Function UpdateTempTable(ByRef mDepreMode As String, ByRef mCurrFYStartDate As String, ByRef mCurrFYEndDate As String, ByRef mFYStartDate As String, ByRef mFYEndDate As String, ByRef pCurrentFyear As Integer) As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim cntRow As Integer
        'Dim mDeprecAsOn As String						
        Dim mPurchaseDate As String
        Dim mDays As Integer
        Dim mDays1 As Integer
        Dim mDays2 As Integer
        Dim mDays3 As Integer
        Dim mDays4 As Integer
        Dim mDeprRate As Double
        Dim mCompanyCode As Integer
        Dim mCompanyName As String
        Dim pPurchaseYear As Integer
        Dim pTRNType As Double
        Dim pTrnName As String
        Dim pPurchaseAmount As Double
        Dim pSaleAmount As Double
        Dim mDepAmount As Double
        Dim mCummDepAmount As Double
        Dim mDeprecAsOn As String
        'Dim mFYStartDate As String						
        'Dim mDepreMode As String						
        Dim pCurrentYearDep As Boolean
        Dim mRefNo As Double
        Dim pSaleDesp As Double
        Dim pSaleDate As String
        Dim pIsSale As Boolean
        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mGrossBlock As String
        Dim mNetBlock As String
        Dim mTillDateSaleAmount As String
        Dim pOPGrossBlock As Double
        Dim pDays As Integer
        Dim pNormalDesc As Double
        Dim pABSPurchaseAmount As Double
        Dim pABSGrossAmount As Double

        Dim cntDate As Date
        Dim mAddDays As Integer
        Dim mCalcDepAsOn As String
        Dim mActAddDays As Integer
        Dim mCummDepr As Boolean
        Dim pOPCummDesp As Double
        Dim pOPNetGrossBlock As Double
        Dim pOPNetGrossBlockABS As Double
        Dim pNetGrossBlock As Double
        Dim RsTemp As ADODB.Recordset
        Dim xSaleDateStr As String
        Dim mSqlStr As String

        UpdateTempTable = False

        SqlStr = "SELECT * FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mDeprecAsOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mCurrFYStartDate))) ''Format(txtDepreciationDate.Text, "DD/MM/YYYY")						
        cntRow = 0
        '    lblCount.Caption = cntRow						
        System.Windows.Forms.Application.DoEvents()
        Do While RsTemp.EOF = False
            pIsSale = False
            mDays1 = 0
            mDays2 = 0
            mDays3 = 0
            mDays4 = 0
            mDays = 0
            mSaleAmount1 = 0
            mSaleAmount2 = 0
            mSaleAmount3 = 0
            mSaleAmount4 = 0
            mSaleDate1 = ""
            mSaleDate2 = ""
            mSaleDate3 = ""
            mSaleDate4 = ""
            pDays = 0
            pNormalDesc = 0
            pOPCummDesp = 0
            mCummDepr = False
            pOPNetGrossBlockABS = 0
            pOPNetGrossBlock = 0
            pNetGrossBlock = 0


            pSaleAmount = 0
            pSaleDesp = 0
            pSaleDate = ""
            mCummDepAmount = 0
            mCalcDepAsOn = CStr(0)

            mCompanyName = RsTemp.Fields("Company_Name").Value
            mCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
            '        mFYStartDate = Format(lblFYStartDate.Caption, "DD/MM/YYYY")						
            mRefNo = RsTemp.Fields("AUTO_KEY_ASSET").Value
            mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), "", RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")

            pPurchaseYear = IIf(IsDBNull(RsTemp.Fields("PUR_YEAR").Value), "0", RsTemp.Fields("PUR_YEAR").Value)
            pTrnName = IIf(IsDBNull(RsTemp.Fields("TRNNAME").Value), "", RsTemp.Fields("TRNNAME").Value)
            pTRNType = IIf(IsDBNull(RsTemp.Fields("TRNCODE").Value), "", RsTemp.Fields("TRNCODE").Value)
            pPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("TOTAL_COST").Value), 0, RsTemp.Fields("TOTAL_COST").Value) ''GetPurchaseAmount(mRefNo, mCompanyCode)						
            pPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))
            pABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)

            pOPGrossBlock = GetGrossBlock(mRefNo, mCompanyCode, mFYStartDate, pPurchaseAmount)
            pOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock, "0"))
            pABSGrossAmount = System.Math.Abs(pOPGrossBlock)

            mTillDateSaleAmount = CStr(CheckSaleAmount(mRefNo, mCompanyCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mCurrFYStartDate)))))
            If pPurchaseAmount = CDbl(mTillDateSaleAmount) Then
                GoTo NextRec
            End If

            If CDate(mPurchaseDate) < CDate(mCurrFYStartDate) Then
                If CalcSaleAmount(mRefNo, mCompanyCode, mFYStartDate, mFYEndDate, mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4) = False Then GoTo LedgError
                If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, pCurrentFyear, mCurrFYStartDate, mFYEndDate) = False Then GoTo LedgError
            End If

            If pPurchaseAmount < 0 Then
                If CDbl(VB6.Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0")) > 0 Then
                    pOPNetGrossBlock = 0
                Else
                    pOPNetGrossBlock = CDbl(VB6.Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0"))
                End If
            Else
                If CDbl(VB6.Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0")) <= 0 Then
                    pOPNetGrossBlock = 0
                Else
                    pOPNetGrossBlock = CDbl(VB6.Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0"))
                End If
            End If

            mDays1 = 0
            mDays2 = 0
            mDays3 = 0
            mDays4 = 0
            mDays = 0
            mSaleAmount1 = 0
            mSaleAmount2 = 0
            mSaleAmount3 = 0
            mSaleAmount4 = 0
            mSaleDate1 = ""
            mSaleDate2 = ""
            mSaleDate3 = ""
            mSaleDate4 = ""
            pSaleAmount = 0
            pSaleDesp = 0
            pSaleDate = ""
            '        mCummDepAmount = 0						
            mCalcDepAsOn = CStr(0)
            pOPCummDesp = 0

            ''For the Year						
            '        If CalcSaleAmount(mRefNo, mCompanyCode, mFYStartDate, mFYEndDate, mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, "") = False Then GoTo LedgError						
            '        If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "", pCurrentFyear, mFYStartDate, mFYEndDate) = False Then GoTo LedgError						

            '        .Col = ColCumulativeDeprec						
            If System.Math.Abs(pOPGrossBlock) < System.Math.Abs(mCummDepAmount) Then

                mCummDepAmount = pOPGrossBlock
                If System.Math.Abs(pSaleDesp) > System.Math.Abs(pOPGrossBlock) Then
                    pSaleDesp = pOPGrossBlock
                End If
            End If
            '        .Text = Format(mCummDepAmount, "0")						


            If CDate(mCurrFYStartDate) > CDate(mPurchaseDate) Then
                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn						
                mDays = mDays - GetLeapYear(mFYStartDate, mDeprecAsOn) ' mDeprecAsOn)						
            Else
                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn						
                mDays = mDays - GetLeapYear(mPurchaseDate, mDeprecAsOn) ' mDeprecAsOn)						
            End If

            mDays = mDays - mAddDays

            If pOPGrossBlock = 0 Or pOPNetGrossBlock = 0 Then
                mDays = 0
            End If


            mActAddDays = mDays

            mDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, mCurrFYStartDate)

            '        .Col = ColDeprec1						
            mDepAmount = (pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4) * mDeprRate * 0.01 * mDays / 365
            '            mDepAmount = (pOPGrossBlock - pSaleAmount) * mDeprRate * 0.01 * mDays / 365						

            If mSaleAmount1 <> 0 Then
                If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mSaleDate1)) + 1
                    mDays = mDays - GetLeapYear(mFYStartDate, mSaleDate1)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate1)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate1)
                End If
                mDepAmount = mDepAmount + (mSaleAmount1 * mDeprRate * 0.01 * mDays / 365)
                If pOPGrossBlock - mSaleAmount1 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount2 <> 0 Then
                If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mSaleDate2)) + 1
                    mDays = mDays - GetLeapYear(mFYStartDate, mSaleDate2)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate2)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate2)
                End If
                mDepAmount = mDepAmount + (mSaleAmount2 * mDeprRate * 0.01 * mDays / 365)
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount3 <> 0 Then
                If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mSaleDate3)) + 1
                    mDays = mDays - GetLeapYear(mFYStartDate, mSaleDate3)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate3)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate3)
                End If
                mDepAmount = mDepAmount + (mSaleAmount3 * mDeprRate * 0.01 * mDays / 365)
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount4 <> 0 Then
                If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mSaleDate4)) + 1
                    mDays = mDays - GetLeapYear(mFYStartDate, mSaleDate4)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate4)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate4)
                End If
                mDepAmount = mDepAmount + (mSaleAmount4 * mDeprRate * 0.01 * mDays / 365)
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If System.Math.Abs(mDepAmount) > System.Math.Abs(pOPNetGrossBlock) Then
                mDepAmount = pOPNetGrossBlock
            End If

            mDepAmount = CDbl(VB6.Format(mDepAmount, "0"))
            '						
            '        .Col = ColDays						
            mActAddDays = CInt(VB6.Format(mActAddDays, "0"))
            '						
            '        .Col = ColSaleAmount						
            '        .Text = Format((mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4), "0")						
            '						
            '        .Col = ColSaleDate						
            xSaleDateStr = mSaleDate1 & IIf(mSaleDate2 = "", "", "," & mSaleDate2) & IIf(mSaleDate3 = "", "", "," & mSaleDate3) & IIf(mSaleDate4 = "", "", "," & mSaleDate4)
            '        .Text = mSaleDate1 & IIf(mSaleDate2 = "", "", "," & mSaleDate2) & IIf(mSaleDate3 = "", "", "," & mSaleDate3) & IIf(mSaleDate4 = "", "", "," & mSaleDate4)						
            '						
            '        .Col = ColTotalDeprecClaim						
            '        .Text = Format(pSaleDesp, "0")						
            '						
            '        .Col = ColGrossBlock						
            mGrossBlock = CStr(GetGrossBlock(mRefNo, mCompanyCode, mDeprecAsOn, pPurchaseAmount))
            mGrossBlock = VB6.Format(mGrossBlock, CStr(0))
            '            If mGrossBlock <= 100 Then						
            '                mGrossBlock = 0						
            '            End If						
            '        .Text = Format(mGrossBlock, "0")						
            '						
            '						
            '        .Col = ColNetBlock						
            If pPurchaseAmount < 0 Then
                If CDbl(VB6.Format(CDbl(mGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0")) > 0 Then
                    pNetGrossBlock = 0
                Else
                    pNetGrossBlock = CDbl(VB6.Format(CDbl(mGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0"))
                End If
            Else
                If CDbl(VB6.Format(CDbl(mGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0")) <= 0 Then
                    pNetGrossBlock = 0
                Else
                    pNetGrossBlock = CDbl(VB6.Format(CDbl(mGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0"))
                End If
            End If
            '						
            '        .Text = pNetGrossBlock						

            mSqlStr = "UPDATE TEMP_AST_DESP_TRN SET " & vbCrLf & " DAYS=" & mActAddDays & "," & vbCrLf & " CURRENT_DESP=" & mDepAmount & "," & vbCrLf & " CUMULATIVE_DESP=" & mCummDepAmount & "," & vbCrLf & " SALE_AMOUNT=" & VB6.Format(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4, "0") & "," & vbCrLf & " SALE_DATE='" & xSaleDateStr & "'," & vbCrLf & " SALE_DESP=" & pSaleDesp & "," & vbCrLf & " GROSS_BLOCK=" & mGrossBlock & "," & vbCrLf & " NET_BLOCK=" & pNetGrossBlock & "," & vbCrLf & " OP_GROSS_BLOCK=" & pOPGrossBlock & ""

            mSqlStr = mSqlStr & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & mRefNo & ""

            PubDBCn.Execute(mSqlStr)

NextRec:

            pSaleAmount = 0
            pSaleDesp = 0
            cntRow = cntRow + 1
            '        lblCount.Caption = cntRow						
            System.Windows.Forms.Application.DoEvents()
            RsTemp.MoveNext()
        Loop
        '''********************************						
        UpdateTempTable = True

        Exit Function
LedgError:
        '    Resume						
        UpdateTempTable = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CalcDepreciationAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef pPurchaseDate As String, ByRef pPurchaseYear As Integer, ByRef pTRNType As Double, ByRef pPurchaseAmount As Double, ByRef pModCode As String, ByRef pCurrentYearDep As Boolean, ByRef pSaleAmount As Double, ByRef pSaleDesp As Double, ByRef pSaleDate As String, ByRef mTotalDepAmount As Double, ByRef mCalcDepAsOn As String, ByRef mOPCummDesp As Double, ByRef pCurrentFyear As Integer, ByRef mFYStartDate As String, ByRef mFYEndDate As String) As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDays As Integer
        Dim mDepRate As Double
        Dim mDepAmount As Double
        'Dim mTotalDepAmount As Double						
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mAsOnDate As String
        Dim mABSPurchaseAmount As Double

        Dim pCurrentYear As Integer
        Dim pCheckCurrentYear As Integer
        Dim mDescpCalcOn As Double
        Dim mSaleValue As Double
        Dim mSaleDesp As Double
        Dim mcntType As Integer
        Dim mSaleDate As String
        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim pSaleOPDesp As Double
        Dim cntDate As Integer
        Dim mAddDays As Integer
        Dim mYearDay As Integer

        Dim mDays1 As Integer
        Dim mDays2 As Integer
        Dim mDays3 As Integer
        Dim mDays4 As Integer
        Dim mLastFYEndDate As String

        mTotalDepAmount = 0
        mOPCummDesp = 0
        SqlStr = "SELECT FYEAR, DEPR_RATE " & vbCrLf & " FROM AST_DEPRECIATION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND FYEAR=" & pCurrentFyear & "" & vbCrLf & " AND GROUP_CODE=" & pTRNType & ""


        SqlStr = SqlStr & vbCrLf & " AND MODE_CODE='" & pModCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY FYEAR"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStartDate = pPurchaseDate

        mAsOnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(mFYStartDate, "DD/MM/YYYY"))))
        mCalcDepAsOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(mFYStartDate, "DD/MM/YYYY"))))

        mABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)
        pCurrentYearDep = False
        mDescpCalcOn = mABSPurchaseAmount
        If RsTemp.EOF = False Then
            mDepRate = IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value)
            mSaleAmount1 = 0
            mSaleAmount2 = 0
            mSaleAmount3 = 0
            mSaleAmount4 = 0
            mSaleDate1 = ""
            mSaleDate2 = ""
            mSaleDate3 = ""
            mSaleDate4 = ""
            pSaleDesp = 0
            mDepAmount = 0
            If CheckSaleAmount(pRefNo, pCompanyCode, mAsOnDate) = 0 Then
                mAddDays = 0
                mAddDays = GetLeapYear(mStartDate, mAsOnDate)
                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) + 1 - mAddDays
                mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))


                If mDepAmount > mDescpCalcOn Then
                    mDepAmount = mDescpCalcOn
                End If
                mTotalDepAmount = mTotalDepAmount + mDepAmount

            Else
                If CalcSaleAmount(pRefNo, pCompanyCode, mStartDate, mAsOnDate, mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4) = False Then GoTo LedgError
                mSaleAmount1 = System.Math.Abs(mSaleAmount1)
                mSaleAmount2 = System.Math.Abs(mSaleAmount2)
                mSaleAmount3 = System.Math.Abs(mSaleAmount3)
                mSaleAmount4 = System.Math.Abs(mSaleAmount4)

                If mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 <> 0 Then
                    If mSaleDate1 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate1)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate1)) + 1 - mAddDays
                        mDays1 = mDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate1)))
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount1

                        If CDate(mSaleDate1) >= CDate(mFYStartDate) And CDate(mSaleDate1) <= CDate(mFYEndDate) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount1 * mDepRate * mDays * 0.01 / 365, "0"))
                            pSaleAmount = pSaleAmount + mSaleAmount1
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount1 * mDepRate * mDays * 0.01 / 365, "0"))
                        End If
                        mCalcDepAsOn = mSaleDate1
                    End If
                    If mSaleDate2 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate2)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate2)) + 1 - mAddDays
                        mDays2 = mDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate2)))
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount2
                        If CDate(mSaleDate2) >= CDate(mFYStartDate) And CDate(mSaleDate2) <= CDate(mFYEndDate) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount2 * mDepRate * (mDays + mDays1) * 0.01 / 365, "0"))
                            pSaleAmount = pSaleAmount + mSaleAmount2
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount2 * mDepRate * mDays * 0.01 / 365, "0"))
                        End If
                        mCalcDepAsOn = mSaleDate2
                    End If
                    If mSaleDate3 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate3)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate3)) + 1 - mAddDays
                        mDays3 = mDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * (mDays + mDays1 + mDays2) * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate3)))
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount3
                        If CDate(mSaleDate3) >= CDate(mFYStartDate) And CDate(mSaleDate3) <= CDate(mFYEndDate) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount3 * mDepRate * mDays * 0.01 / 365, "0"))
                            pSaleAmount = pSaleAmount + mSaleAmount3
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount3 * mDepRate * mDays * 0.01 / 365, "0"))
                        End If
                        mCalcDepAsOn = mSaleDate3
                    End If
                    If mSaleDate4 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate4)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate4)) + 1 - mAddDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate4)))
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount4
                        If CDate(mSaleDate4) >= CDate(mFYStartDate) And CDate(mSaleDate4) <= CDate(mFYEndDate) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount4 * mDepRate * (mDays + mDays1 + mDays2 + mDays3) * 0.01 / 365, "0"))
                            pSaleAmount = pSaleAmount + mSaleAmount4
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount4 * mDepRate * mDays * 0.01 / 365, "0"))
                        End If
                        mCalcDepAsOn = mSaleDate4
                    End If
                End If
                mAddDays = 0
                mAddDays = GetLeapYear(mStartDate, mAsOnDate)
                If CDate(pPurchaseDate) = CDate(mStartDate) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) + 1 - mAddDays
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) - mAddDays
                End If
                mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                mTotalDepAmount = mTotalDepAmount + mDepAmount - pSaleOPDesp
            End If
        End If

        '    If mDepRate = 100 Then						
        '        mAddDays = 0						
        '        mLastFYEndDate = DateAdd("d", -1, lblFYStartDate.Caption)						
        '        mAddDays = GetLeapYear(pPurchaseDate, mLastFYEndDate)						
        '        mDays = DateDiff("d", pPurchaseDate, mLastFYEndDate) + 1 - mAddDays						
        '        mOPCummDesp = Format(mABSPurchaseAmount * mDepRate * mDays * 0.01 / 365, "0")						
        '						
        '        If mOPCummDesp < 1 Then						
        '            mOPCummDesp = 0						
        '        ElseIf mOPCummDesp > mABSPurchaseAmount Then						
        '            mOPCummDesp = mABSPurchaseAmount						
        '        End If						
        '    End If						

        If pPurchaseAmount < 1 Then
            mTotalDepAmount = mTotalDepAmount * -1
            pSaleDesp = pSaleDesp * -1
        End If

        If System.Math.Abs(pSaleDesp) > System.Math.Abs(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4) Then
            pSaleDesp = mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4
        End If
        CalcDepreciationAmount = True
        Exit Function
LedgError:
        '    Resume						
        CalcDepreciationAmount = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function







    Private Function CalcSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mStartDate As String, ByRef mEndDate As String, ByRef mSaleAmount1 As Double, ByRef mSaleAmount2 As Double, ByRef mSaleAmount3 As Double, ByRef mSaleAmount4 As Double, ByRef mSaleDate1 As String, ByRef mSaleDate2 As String, ByRef mSaleDate3 As String, ByRef mSaleDate4 As String) As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE <'" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = IIf(IsNull(RsTemp!ORIGINAL_COST), 0, RsTemp!ORIGINAL_COST)						
            mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value), CStr(0)))
            mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE").Value), 0, RsTemp.Fields("SALE_BILL_DATE").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"


        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE1 <'" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST1), 0, RsTemp!ORIGINAL_COST1)						
            mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value), CStr(0)))
            mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE1").Value), 0, RsTemp.Fields("SALE_BILL_DATE1").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE2 <'" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST2), 0, RsTemp!ORIGINAL_COST2)						
            mSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value), CStr(0)))
            mSaleDate3 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE2").Value), 0, RsTemp.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE3 <'" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)						
            mSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), CStr(0)))
            mSaleDate4 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), 0, RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
        End If
        CalcSaleAmount = True
        Exit Function
LedgError:
        CalcSaleAmount = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CheckSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mStartDate As String) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE <='" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
            '        mSaleAmount1 = IIf(IsNull(RsTemp!ORIGINAL_COST), 0, RsTemp!ORIGINAL_COST)						
            '        mSaleDate1 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE), 0, RsTemp!SALE_BILL_DATE), "DD/MM/YYYY")						
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE1 <='" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value)
            '        mSaleAmount2 = IIf(IsNull(RsTemp!ORIGINAL_COST1), 0, RsTemp!ORIGINAL_COST1)						
            '        mSaleDate2 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE1), 0, RsTemp!SALE_BILL_DATE1), "DD/MM/YYYY")						
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE2 <='" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value)
            '        mSaleAmount3 = IIf(IsNull(RsTemp!ORIGINAL_COST2), 0, RsTemp!ORIGINAL_COST2)						
            '        mSaleDate3 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE2), 0, RsTemp!SALE_BILL_DATE2), "DD/MM/YYYY")						
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE3 <='" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value)
            '        mSaleAmount4 = IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)						
            '        mSaleDate4 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE3), 0, RsTemp!SALE_BILL_DATE3), "DD/MM/YYYY")						
        End If

        Exit Function
LedgError:
        CheckSaleAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetDepreciationRate(ByRef pCompanyCode As Integer, ByRef pTRNType As Double, ByRef pModCode As String, ByRef pDepsDate As String) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAsOnDate As String
        Dim pCurrentYear As Integer

        GetDepreciationRate = 0
        If CDate(pDepsDate) < CDate("01/04/2003") Then
            If Month(CDate(pDepsDate)) = 1 Or Month(CDate(pDepsDate)) = 2 Or Month(CDate(pDepsDate)) = 3 Then
                pCurrentYear = Year(CDate(pDepsDate)) - 1
            Else
                pCurrentYear = Year(CDate(pDepsDate))
            End If
        Else
            pCurrentYear = GetCurrentFYNo(PubDBCn, VB6.Format(pDepsDate))
        End If
        SqlStr = "SELECT FYEAR, DEPR_RATE " & vbCrLf & " FROM AST_DEPRECIATION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND FYEAR=" & pCurrentYear & "" & vbCrLf & " AND GROUP_CODE=" & pTRNType & ""

        '						
        SqlStr = SqlStr & vbCrLf & " AND MODE_CODE='" & pModCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY FYEAR"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDepreciationRate = IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value)
        End If


        Exit Function
LedgError:
        GetDepreciationRate = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Private Function GetGrossBlock(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mEndDate As String, ByRef xPurchaseAmount As Double) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPurchaseAmount As Double

        GetGrossBlock = 0

        '    SqlStr = " SELECT  TOTAL_COST+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND) AS PURCHASE_COST " & vbCrLf _						
        ''            & " FROM AST_ASSET_TRN " & vbCrLf _						
        ''            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _						
        ''            & " AND AUTO_KEY_ASSET=" & pRefNo & ""						
        '						
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly						
        '						
        '    If RsTemp.EOF = False Then						
        '        GetGrossBlock = IIf(IsNull(RsTemp!PURCHASE_COST), 0, RsTemp!PURCHASE_COST)						
        '        mPurchaseAmount = IIf(IsNull(RsTemp!PURCHASE_COST), 0, RsTemp!PURCHASE_COST)						
        '    End If						

        GetGrossBlock = xPurchaseAmount
        mPurchaseAmount = xPurchaseAmount

        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE <='" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE1 <='" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE2 <='" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE3 <='" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value)
        End If

        '    If mPurchaseAmount > 0 Then						
        '        If GetGrossBlock < 0 Then						
        '            GetGrossBlock = 0						
        '        End If						
        '    End If						
        Exit Function
LedgError:
        GetGrossBlock = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function




    Private Function GetLeapYear(ByRef pStartDate As String, ByRef pEndDate As String) As Integer
        On Error GoTo LedgError

        Dim cntDate As Date
        Dim mAddDays As Integer

        GetLeapYear = 0
        pStartDate = CStr(CDate(pStartDate))
        pEndDate = CStr(CDate(pEndDate))
        cntDate = CDate(pStartDate)
        Do While cntDate <= CDate(pEndDate)
            'UPGRADE_WARNING: Untranslated statement in GetLeapYear. Please check source code.						
            cntDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, cntDate)
        Loop
        Exit Function
LedgError:
        '    Resume						
        GetLeapYear = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetPurchaseAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPurchaseAmount As Double

        GetPurchaseAmount = 0

        SqlStr = " SELECT  TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT + DN_CR_AMOUNT -(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND) AS PURCHASE_COST " & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
        End If

        Exit Function
LedgError:
        GetPurchaseAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub frmTrfOPBalanceAsset_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        'Set PvtDBCN = New ADODB.Connection						
        'PvtDBCN.CommandTimeout = 0						
        'PvtDBCN.ConnectionTimeout = 0						
        'PvtDBCN.Open StrConn						

        'TxtDisplayTransfer(0).Visible = False						
        'TxtDisplayTransfer(1).Visible = False						
        OptAllAccount.Checked = True
        Me.Height = VB6.TwipsToPixelsY(5595)
        Me.Width = VB6.TwipsToPixelsX(5220)
        Me.Left = 0
        Me.Top = 0
        Call FillFYear()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume						
    End Sub
    Private Sub FillFYear()
        Dim SqlStr As String
        Dim mRsFYear As ADODB.Recordset
        CboFYearFrom.Items.Clear()
        CboFYearTo.Items.Clear()
        SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN  " & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY FYEAR"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsFYear, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsFYear.EOF = False Then
            Do While Not mRsFYear.EOF
                CboFYearFrom.Items.Add(VB6.Format(mRsFYear.Fields("FYEAR").Value, "00") & "  :  " & VB6.Format(mRsFYear.Fields("START_DATE").Value, "DD-MM-YYYY") & "_" & VB6.Format(mRsFYear.Fields("END_DATE").Value, "DD-MM-YYYY"))
                CboFYearTo.Items.Add(VB6.Format(mRsFYear.Fields("FYEAR").Value, "00") & "  :  " & VB6.Format(mRsFYear.Fields("START_DATE").Value, "DD-MM-YYYY") & "_" & VB6.Format(mRsFYear.Fields("END_DATE").Value, "DD-MM-YYYY"))
                mRsFYear.MoveNext()
            Loop
        End If
    End Sub

    Private Sub OptAllAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAllAccount.CheckedChanged
        If eventSender.Checked Then
            txtName.Enabled = False
            cmdSearch.Enabled = False
            cmdStart.Enabled = True
        End If
    End Sub
    Private Sub OptParticularAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParticularAccount.CheckedChanged
        If eventSender.Checked Then
            txtName.Enabled = True
            cmdSearch.Enabled = True
            cmdStart.Enabled = True
        End If
    End Sub


    Private Sub txtDeprMode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeprMode.DoubleClick
        SearchDepr()
    End Sub
    Private Sub txtDeprMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeprMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeprMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDeprMode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeprMode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchDepr()
    End Sub
    Private Sub txtDeprMode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeprMode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtDeprMode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'UPGRADE_WARNING: Untranslated statement in txtDeprMode_Validate. Please check source code.						
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearchDepr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchDepr.Click
        SearchDepr()
    End Sub
    Private Sub SearchDepr()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        ''MainClass.SearchMaster txtDeprMode, "AST_DEPRECIATION_MODE_MST", "NAME", SqlStr						
        MainClass.SearchGridMaster(txtDeprMode.Text, "AST_DEPRECIATION_MODE_MST", "MODE_CODE", "MODE_DESC", "MODE_TYPE",  , SqlStr)
        If AcName <> "" Then
            txtDeprMode.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo SearchErr
        Dim RsItem As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        'UPGRADE_WARNING: Untranslated statement in TxtName_Validate. Please check source code.						
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)
        If RsItem.EOF = True Then
            MsgBox("Account Name Not Exist In Master", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
SearchErr:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
