Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBalanceSheet
    Inherits System.Windows.Forms.Form

    Private FormLoaded As Boolean
    Dim mCurrentAmt As Decimal
    Dim mPreviousAmt As Decimal

    Private Const ColPicText As Short = 1
    Private Const ColPic As Short = 2
    Private Const ColDesc As Short = 3
    Private Const ColSchd As Short = 4
    Private Const ColCurrSubTotal As Short = 5
    Private Const ColCurrTotal As Short = 6
    Private Const ColPrevSubTotal As Short = 7
    Private Const ColPrevTotal As Short = 8
    Private Const ColCode As Short = 9
    Private Const ColLevel As Short = 10
    Private Const ColSNO As Short = 11
    Private Const ColCategory As Short = 12

    Private Const ConRowHeight As Short = 30
    Dim mCurrProfit_Loss As Double
    Dim mClickProcess As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer

    Private Function InsertIntoBS() As Boolean
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim SqlStrBS As String
        Dim SqlStrACMG As String
        Dim SqlStrTRN As String
        Dim mSqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        Sqlstr = "DELETE FROM TEMP_BALANCESHEET NOLOGGING " ''& vbCrLf |            & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(Sqlstr)

        Sqlstr = ""
        mSqlStr = "INSERT INTO TEMP_BALANCESHEET (" & vbCrLf & " USERID, CODE, NAME," & vbCrLf & " PARENTCODE, BSCODEDR, BSCODECR," & vbCrLf & " CATEGORY, ACCTTYPE, SCHEDULENO," & vbCrLf & " PREVIOUSFYAMT, CURRENTFYRAMT,SEQ_NO )"

        SqlStrBS = BSQry
        SqlStrACMG = ACMGROUPQry
        SqlStrTRN = TRNQry

        Sqlstr = mSqlStr & vbCrLf & SqlStrBS
        PubDBCn.Execute(Sqlstr)

        Sqlstr = ""
        Sqlstr = mSqlStr & vbCrLf & SqlStrACMG
        PubDBCn.Execute(Sqlstr)

        Sqlstr = ""
        Sqlstr = mSqlStr & vbCrLf & SqlStrTRN
        PubDBCn.Execute(Sqlstr)

        PubDBCn.CommitTrans()
        InsertIntoBS = True

        Exit Function
ErrPart:
        '' Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        InsertIntoBS = False
    End Function

    Private Function ACMGROUPQry() As String

        On Error GoTo ViewTrialErr
        Dim Sqlstr As String

        ''********SELECTION..........
        Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " ACMGROUP.GROUP_CATEGORY || ACMGROUP.GROUP_CODE, ACMGROUP.GROUP_NAME, " & vbCrLf _
            & " DECODE(ACMGROUP.GROUP_PARENTCODE,-1,'H' || ACMGROUP.GROUP_BSCODEDR,ACMGROUP.GROUP_CATEGORY || ACMGROUP.GROUP_PARENTCODE), " & vbCrLf _
            & " 'H' || ACMGROUP.GROUP_BSCODEDR, " & vbCrLf _
            & " 'H' || ACMGROUP.GROUP_BSCODECR, " & vbCrLf _
            & " ACMGROUP.GROUP_CATEGORY,BSGROUP.BSGROUP_ACCTTYPE,ACMGROUP.GROUP_SCHEDULENO, 0, 0,GROUP_SEQNO "

        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf _
            & " FROM FIN_GROUP_MST ACMGROUP,FIN_BSGROUP_MST BSGROUP "

        ''********JOINING..........
        Sqlstr = Sqlstr & vbCrLf & " WHERE " & vbCrLf _
            & " ACMGROUP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ACMGROUP.COMPANY_CODE=BSGROUP.COMPANY_CODE" & vbCrLf _
            & " AND ACMGROUP.GROUP_BSCODEDR=BSGROUP.BSGROUP_CODE AND GROUP_STATUS='O'"

        ''********WHERE CLAUSE..........
        If UCase(lblType.Text) = UCase("Balance Sheet") Then
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.BSGROUP_PRINT='Y' "
        ElseIf UCase(lblType.Text) = UCase("Fund Flow") Then
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.FUNDFLOW_PRINT='Y' "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.PLGROUP_PRINT='Y' "
        End If

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf & " ACMGROUP.GROUP_CODE "


        ACMGROUPQry = Sqlstr
        Exit Function
ViewTrialErr:
        ACMGROUPQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume
    End Function
    Private Function FillIntoTempTRNQry() As Boolean

        On Error GoTo ViewTrialErr
        Dim mSqlStr As String
        Dim Sqlstr As String
        Dim mCurrPnL As Double
        Dim mFinalSheet As Boolean
        Dim mClosingStock As Double
        Dim mOpeningStock As Double
        Dim RsTempAcct As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim RsTempCompany As ADODB.Recordset
        Dim mCategoryCode As String
        Dim mAccountCode As String
        Dim mAccountName As String
        Dim mGroupCode As Long
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim pCompanyCode As Long

        Dim mPrevClosingStock As Double
        Dim mPrevOpeningStock As Double
        Dim mCurrFYear As Long
        Dim SqlstrCat As String
        Dim RsTempCat As ADODB.Recordset
        ' select distinct GROUP_TYPE from fin_group_mst;


        If CDate(txtDateTo.Text) = CDate(RsCompany.Fields("END_DATE").Value) Then
            mFinalSheet = True
        Else
            mFinalSheet = False
        End If

        mCurrFYear = RsCompany.Fields("FYEAR").Value

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            'Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        Sqlstr = "DELETE FROM TEMP_TRN NOLOGGING " ''& vbCrLf |            & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(Sqlstr)

        If Me.lblType.Text = UCase("Balance Sheet") Then
            mCurrPnL = 0 ''GetCurrentProfit
        End If

        mSqlStr = "INSERT INTO TEMP_TRN (" & vbCrLf _
            & " USERID, COMPANY_CODE, CODE, NAME," & vbCrLf _
            & " GROUPCODE, CATEGORY, " & vbCrLf _
            & " PREVIOUSFYAMT, CURRENTFYRAMT)"

        ''********SELECTION..........
        Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " ACM.COMPANY_CODE,   " & vbCrLf _
            & " '' || ACM.SUPP_CUST_CODE, " & vbCrLf & " ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ACM.GROUPCODE ELSE ACM.GROUPCODECR END AS GCODE, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE, "


        'If RsCompany.Fields("FYEAR").Value <= 2023 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
        '    Sqlstr = Sqlstr & vbCrLf & " 0 OP, "
        'Else
        If UCase(lblType.Text) = UCase("Profit & Loss A/c") Then
            Sqlstr = Sqlstr & vbCrLf & " GETPROFITLOSS(ACM.COMPANY_CODE, " & RsCompany.Fields("FYEAR").Value - 1 & " , ACM.SUPP_CUST_CODE) AS OP, "
        Else
            Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " AND VDATE< TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN DECODE(ACM.HEADTYPE,'P',0,AMOUNT * DECODE(DC,'D',1,-1)) ELSE 0 END) OP, "
        End If
        'End If

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            Sqlstr = Sqlstr & vbCrLf _
                & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END) + DECODE(ACM.HEADTYPE,'P'," & mCurrPnL & ",0) AS CURRFY"
        Else
            Sqlstr = Sqlstr & vbCrLf _
                & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " AND TRN.VDate >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END) + DECODE(ACM.HEADTYPE,'P'," & mCurrPnL & ",0)  AS CURRFY"
        End If

        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, " & vbCrLf _
            & " FIN_SUPP_CUST_MST ACM "


        '    ''********Joining..........
        Sqlstr = Sqlstr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "


        Sqlstr = Sqlstr & vbCrLf _
            & " AND TRN.FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
            & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        'If lstCompanyName.GetItemChecked(0) = True Then
        '    mCompanyCodeStr = ""
        'Else
        '    For CntLst = 1 To lstCompanyName.Items.Count - 1
        '        If lstCompanyName.GetItemChecked(CntLst) = True Then
        '            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
        '            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '            End If
        '            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
        '        End If
        '    Next
        'End If

        If mCompanyCodeStr <> "" Then
            'mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        Sqlstr = Sqlstr & vbCrLf & " AND TRN.PL_FLAG='N' " ''Expenses Adjust Amount not Consider..

        If mFinalSheet = False Then
            Sqlstr = Sqlstr & vbCrLf _
                & " AND TRN.ACCOUNTCODE || TRN.COMPANY_CODE NOT IN ( " & vbCrLf _
                & " SELECT DISTINCT ACCOUNTCODE || COMPANY_CODE " & vbCrLf _
                & " FROM FIN_PROFITLOSS_TRN " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
                & " AND VDate = TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND TRN.PL_FLAG='N' )"
        End If

        '& " SELECT DISTINCT OP_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OP_ACCOUNT  IS NOT NULL " & vbCrLf _
        '& " UNION ALL" & vbCrLf _

        If Me.lblType.Text = UCase("Balance Sheet") Then   ''And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.ACCOUNTCODE || TRN.COMPANY_CODE NOT IN (" & vbCrLf _
                & " SELECT DISTINCT CL_ACCOUNT || COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT  IS NOT NULL " & vbCrLf _
                & ")"
        End If

        If UCase(lblType.Text) = UCase("Profit & Loss A/c") Then ''And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.ACCOUNTCODE NOT IN (" & vbCrLf _
                & " SELECT DISTINCT OP_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST WHERE OP_ACCOUNT  IS NOT NULL "

            If mCompanyCodeStr <> "" Then
                'mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            Sqlstr = Sqlstr & vbCrLf _
                & " UNION ALL" & vbCrLf _
                & " SELECT DISTINCT CL_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT  IS NOT NULL "

            If mCompanyCodeStr <> "" Then
                'mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            Sqlstr = Sqlstr & vbCrLf _
                & ")"
        End If

        ''********GROUP BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " GROUP BY " & vbCrLf _
            & " ACM.COMPANY_CODE,ACM.SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,ACM.GROUPCODE,ACM.GROUPCODECR, " & vbCrLf _
            & " ACM.SUPP_CUST_TYPE, ACM.HEADTYPE "

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf _
            & " ACM.SUPP_CUST_CODE "


        Sqlstr = mSqlStr & vbCrLf & Sqlstr
        PubDBCn.Execute(Sqlstr)

        '
        If Me.lblType.Text = UCase("Balance Sheet") Then
            Dim mFGClosingStock As Double = 0

            Sqlstr = "SELECT DISTINCT CL_ACCOUNT ACCOUNT_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT  IS NOT NULL "

            If mCompanyCodeStr <> "" Then
                Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If chkClosingStock.Checked = False Then
                Sqlstr = Sqlstr & vbCrLf & " AND 1=2"
            End If

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAcct, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempAcct.EOF = False Then

                Do While RsTempAcct.EOF = False

                    mClosingStock = 0
                    mPrevClosingStock = 0
                    mAccountCode = IIf(IsDBNull(RsTempAcct.Fields("ACCOUNT_CODE").Value), "", RsTempAcct.Fields("ACCOUNT_CODE").Value)
                    mGroupCode = -1



                    mGroupCode = 1001789

                    'If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "GROUPCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                    '    mGroupCode = Val(MasterNo)
                    'End If

                    If mAccountCode <> "" Then

                        Sqlstr = "SELECT DISTINCT COMPANY_CODE, CATEGORY_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT ='" & mAccountCode & "' "

                        If mCompanyCodeStr <> "" Then
                            Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
                        End If

                        If chkOpeningStock.Checked = False Then
                            Sqlstr = Sqlstr & vbCrLf & " AND 1=2"
                        End If

                        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            'mPrevClosingStock = GetLedgerStockBalance(mAccountCode, RsCompany.Fields("FYEAR").Value - 1, mCompanyCodeStr)
                            'mPrevClosingStock = mPrevClosingStock * -1

                            Do While RsTemp.EOF = False

                                pCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)
                                mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)
                                mClosingStock = mClosingStock + GetClosingBalance(pCompanyCode, mCategoryCode, "CL", mCurrFYear, mAccountCode)
                                mPrevClosingStock = mPrevClosingStock + GetClosingBalance(pCompanyCode, mCategoryCode, "CL", mCurrFYear - 1, mAccountCode)

                                'If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                                '    mAccountName = MasterNo
                                'End If
                                mAccountName = "CLOSING BALANCE"

                                RsTemp.MoveNext()
                            Loop

                            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                                mAccountName = MasterNo
                            End If

                            Sqlstr = "INSERT INTO TEMP_TRN (" & vbCrLf _
                                    & " USERID, COMPANY_CODE, CODE, NAME," & vbCrLf _
                                    & " GROUPCODE, CATEGORY, " & vbCrLf _
                                    & " PREVIOUSFYAMT, CURRENTFYRAMT) VALUES ("

                            Sqlstr = Sqlstr & vbCrLf _
                                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                                    & " " & pCompanyCode & ",   " & vbCrLf _
                                    & " '" & mAccountCode & "', " & vbCrLf _
                                    & " '" & mAccountName & "', " & vbCrLf _
                                    & " " & mGroupCode & ", " & vbCrLf _
                                    & " 'O', " & mPrevClosingStock & ", " & mClosingStock & " )"

                            PubDBCn.Execute(Sqlstr)
                        End If
                    End If



                    RsTempAcct.MoveNext()
                Loop
            End If
        End If

        If UCase(lblType.Text) = UCase("Profit & Loss A/c") Then

            ''Closing Balance 
            Dim mFGClosingStock As Double = 0

            SqlstrCat = "SELECT DISTINCT CL_ACCOUNT ACCOUNT_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT  IS NOT NULL "

            If mCompanyCodeStr <> "" Then
                SqlstrCat = SqlstrCat & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If chkClosingStock.Checked = False Then
                SqlstrCat = SqlstrCat & vbCrLf & " AND 1=2"
            End If

            MainClass.UOpenRecordSet(SqlstrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)

            mClosingStock = 0
            mGroupCode = -1
            If RsTempCat.EOF = False Then
                Do While RsTempCat.EOF = False

                    mAccountCode = IIf(IsDBNull(RsTempCat.Fields("ACCOUNT_CODE").Value), "", RsTempCat.Fields("ACCOUNT_CODE").Value)
                    mAccountName = ""

                    mGroupCode = 1000010
                    'If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "GROUPCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '    mGroupCode = Val(MasterNo)
                    'End If

                    mClosingStock = 0
                    mPrevClosingStock = 0
                    Sqlstr = "SELECT DISTINCT CATEGORY_CODE, COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT ='" & mAccountCode & "' "

                    If mCompanyCodeStr <> "" Then
                        Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
                    End If

                    If chkClosingStock.Checked = False Then
                        Sqlstr = Sqlstr & vbCrLf & " AND 1=2"
                    End If

                    MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        'mPrevClosingStock = GetLedgerStockBalance(mAccountCode, RsCompany.Fields("FYEAR").Value - 1, mCompanyCodeStr)

                        Do While RsTemp.EOF = False
                            pCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)
                            mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)

                            mClosingStock = mClosingStock + GetClosingBalance(pCompanyCode, mCategoryCode, "CL", mCurrFYear, mAccountCode)
                            mPrevClosingStock = mPrevClosingStock + GetClosingBalance(pCompanyCode, mCategoryCode, "CL", mCurrFYear - 1, mAccountCode)
                            RsTemp.MoveNext()
                        Loop

                        If GetStockFromInventory(pCompanyCode) = "N" Then
                            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                                mAccountName = MasterNo
                            End If

                        Else
                            mAccountName = "CLOSING BALANCE"
                            mAccountCode = "-11111"
                        End If


                        mClosingStock = mClosingStock * -1

                        Sqlstr = "INSERT INTO TEMP_TRN (" & vbCrLf _
                            & " USERID, COMPANY_CODE, CODE, NAME," & vbCrLf _
                            & " GROUPCODE, CATEGORY, " & vbCrLf _
                            & " PREVIOUSFYAMT, CURRENTFYRAMT) VALUES ("

                        Sqlstr = Sqlstr & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " " & pCompanyCode & ",   " & vbCrLf _
                            & " '" & mAccountCode & "', " & vbCrLf _
                            & " '" & mAccountName & "', " & vbCrLf _
                            & " " & mGroupCode & ", " & vbCrLf _
                            & " 'O', " & mPrevClosingStock * -1 & ", " & mClosingStock & " )"

                        PubDBCn.Execute(Sqlstr)

                    End If
                    RsTempCat.MoveNext()
                Loop
            End If


            ''Opening Balance 
            Dim mFGOpeningStock As Double = 0

            Sqlstr = "SELECT DISTINCT COMPANY_CODE FROM GEN_COMPANY_MST"

            If mCompanyCodeStr <> "" Then
                Sqlstr = Sqlstr & vbCrLf & " WHERE COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCompany, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempCompany.EOF = False Then
                Do While RsTempCompany.EOF = False
                    pCompanyCode = IIf(IsDBNull(RsTempCompany.Fields("COMPANY_CODE").Value), 0, RsTempCompany.Fields("COMPANY_CODE").Value)

                    SqlstrCat = "SELECT DISTINCT OP_ACCOUNT ACCOUNT_CODE FROM " & vbCrLf _
                        & " GEN_CATEGORY_MAPPING_MST WHERE OP_ACCOUNT  Is Not NULL And COMPANY_CODE=" & pCompanyCode & ""

                    If chkOpeningStock.Checked = False Then
                        SqlstrCat = SqlstrCat & vbCrLf & " And 1=2"
                    End If

                    MainClass.UOpenRecordSet(SqlstrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)

                    mClosingStock = 0
                    mPrevOpeningStock = 0
                    mGroupCode = -1
                    If RsTempCat.EOF = False Then
                        Do While RsTempCat.EOF = False

                            mAccountCode = IIf(IsDBNull(RsTempCat.Fields("ACCOUNT_CODE").Value), "", RsTempCat.Fields("ACCOUNT_CODE").Value)
                            mAccountName = ""

                            mOpeningStock = 0
                            mPrevOpeningStock = 0
                            mGroupCode = 4002102

                            mOpeningStock = 0
                            mPrevOpeningStock = 0

                            Sqlstr = "SELECT DISTINCT CATEGORY_CODE, COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST " & vbCrLf _
                                & " WHERE OP_ACCOUNT ='" & mAccountCode & "' " & vbCrLf _
                                & " AND COMPANY_CODE =" & pCompanyCode & ""

                            If chkOpeningStock.Checked = False Then
                                Sqlstr = Sqlstr & vbCrLf & " AND 1=2"
                            End If

                            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = False Then
                                'mPrevOpeningStock = GetLedgerStockBalance(mAccountCode, RsCompany.Fields("FYEAR").Value - 1, mCompanyCodeStr)
                                Do While RsTemp.EOF = False
                                    mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)

                                    mOpeningStock = mOpeningStock + GetClosingBalance(pCompanyCode, mCategoryCode, "OP", mCurrFYear, mAccountCode)
                                    mPrevOpeningStock = mPrevOpeningStock + GetClosingBalance(pCompanyCode, mCategoryCode, "OP", mCurrFYear - 1, mAccountCode)

                                    RsTemp.MoveNext()
                                Loop

                                If GetStockFromInventory(pCompanyCode) = "N" Then
                                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                                        mAccountName = MasterNo
                                    End If
                                Else
                                    If RsCompany.Fields("FYEAR").Value = 2023 Then
                                        mPrevOpeningStock = GetLedgerStockBalance(mAccountCode, mCurrFYear - 1, pCompanyCode)
                                    End If

                                    mAccountName = "OPENING BALANCE"
                                    mAccountCode = "-22222"
                                End If

                                Sqlstr = "INSERT INTO TEMP_TRN (" & vbCrLf _
                                    & " USERID, COMPANY_CODE, CODE, NAME," & vbCrLf _
                                    & " GROUPCODE, CATEGORY, " & vbCrLf _
                                    & " PREVIOUSFYAMT, CURRENTFYRAMT) VALUES ("

                                Sqlstr = Sqlstr & vbCrLf _
                                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                                    & " " & pCompanyCode & ",   " & vbCrLf _
                                    & " '" & mAccountCode & "', " & vbCrLf _
                                    & " '" & mAccountName & "', " & vbCrLf _
                                    & " " & mGroupCode & ", " & vbCrLf _
                                    & " 'O', " & mPrevOpeningStock & ", " & mOpeningStock & " )"

                                PubDBCn.Execute(Sqlstr)

                            End If


                            RsTempCat.MoveNext()
                        Loop
                    End If

                    RsTempCompany.MoveNext()
                Loop

            End If
        End If

        ''From Addition Voucher

        Sqlstr = ""
        If mFinalSheet = False Then
            ''********SELECTION..........
            Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " ACM.COMPANY_CODE,   " & vbCrLf _
                & " '' || ACM.SUPP_CUST_CODE, " & vbCrLf & " ACM.SUPP_CUST_NAME, " & vbCrLf _
                & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ACM.GROUPCODE ELSE ACM.GROUPCODECR END AS GCODE, " & vbCrLf _
                & " ACM.SUPP_CUST_TYPE, "

            If UCase(lblType.Text) = UCase("Profit & Loss A/c") Then
                Sqlstr = Sqlstr & vbCrLf & " GETPROFITLOSS(ACM.COMPANY_CODE, " & RsCompany.Fields("FYEAR").Value - 1 & " , ACM.SUPP_CUST_CODE) AS OP, "
            Else
                Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " AND VDATE< TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END) OP, "
            End If

            If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END)  AS CURRFY"
            Else
                Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " AND TRN.VDate >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END)  AS CURRFY"
            End If

            ''********TABLEs..........
            Sqlstr = Sqlstr & vbCrLf & " FROM FIN_PROFITLOSS_TRN TRN, " & vbCrLf _
                & " FIN_SUPP_CUST_MST ACM "


            '    ''********Joining..........
            Sqlstr = Sqlstr & vbCrLf _
                & " WHERE " & vbCrLf _
                & " TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "


            Sqlstr = Sqlstr & vbCrLf _
                & " AND TRN.FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
                & " AND TRN.VDate = TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

            If mCompanyCodeStr <> "" Then
                Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            Sqlstr = Sqlstr & vbCrLf & " AND TRN.PL_FLAG='N' " ''Expenses Adjust Amount not Consider..


            ''********GROUP BY CLAUSE..........
            Sqlstr = Sqlstr & vbCrLf & " GROUP BY " & vbCrLf _
                & " ACM.COMPANY_CODE,ACM.SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,ACM.GROUPCODE,ACM.GROUPCODECR, " & vbCrLf _
                & " ACM.SUPP_CUST_TYPE "

            ''********ORDER BY CLAUSE..........
            Sqlstr = Sqlstr & vbCrLf _
                & " ORDER BY " & vbCrLf _
                & " ACM.SUPP_CUST_CODE "
            Sqlstr = mSqlStr & vbCrLf & Sqlstr
            PubDBCn.Execute(Sqlstr)
        End If

        ''Profit & Loss
        Dim RsTempComp As ADODB.Recordset
        Dim xCompanyCode As Long

        If Me.lblType.Text = UCase("Balance Sheet") Then

            Sqlstr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"

            If mCompanyCodeStr <> "" Then
                Sqlstr = Sqlstr & vbCrLf & " WHERE COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempComp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempComp.EOF = False Then
                Do While RsTempComp.EOF = False
                    xCompanyCode = IIf(IsDBNull(RsTempComp.Fields("COMPANY_CODE").Value), -1, RsTempComp.Fields("COMPANY_CODE").Value)

                    mCurrPnL = GetCurrentProfit(xCompanyCode)

                    ''********SELECTION..........
                    Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " ACM.COMPANY_CODE, '' || ACM.SUPP_CUST_CODE,  " & vbCrLf _
                            & " ACM.SUPP_CUST_NAME, " & vbCrLf _
                            & " CASE WHEN " & mCurrPnL & " >=0 THEN ACM.GROUPCODE ELSE ACM.GROUPCODECR END AS GCODE, " & vbCrLf _
                            & " ACM.SUPP_CUST_TYPE, " & vbCrLf _
                            & " 0, " & vbCrLf _
                            & " " & mCurrPnL & "  AS CURRFY " & vbCrLf _
                            & " FROM FIN_SUPP_CUST_MST ACM " & vbCrLf _
                            & " WHERE " & vbCrLf _
                            & " HEADTYPE='P'"


                    Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE =" & xCompanyCode & ""

                    Sqlstr = mSqlStr & vbCrLf & Sqlstr
                    PubDBCn.Execute(Sqlstr)

                    RsTempComp.MoveNext()
                Loop
            End If


        End If

        PubDBCn.CommitTrans()

        FillIntoTempTRNQry = True
        Exit Function
ViewTrialErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume

        FillIntoTempTRNQry = False
        PubDBCn.RollbackTrans()

    End Function

    Private Function GetClosingBalance(ByRef pCompanyCode As Long, ByRef pCategoryCode As String, ByRef pType As String, ByRef pYear As Long, ByRef mAccountCode As String) As Double

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mTableName As String
        Dim mToDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pStockFromInventory As String

        pStockFromInventory = GetStockFromInventory(pCompanyCode)

        'If RsCompany.Fields("FYEAR").Value > pYear And pType = "OP" Then
        '    pStockFromInventory = "N"
        'End If

        If pStockFromInventory = "N" Then
            GetClosingBalance = 0
            GetClosingBalance = GetLedgerStockBalance(mAccountCode, pYear, pCompanyCode)
            GetClosingBalance = GetClosingBalance * IIf(pType = "OP", 1, -1)
        Else

            If RsCompany.Fields("FYEAR").Value = pYear Then
                If pType = "OP" Then
                    mToDate = VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY")
                Else
                    mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")
                End If
            Else
                If pType = "OP" Then
                    mToDate = VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY")
                    mToDate = DateAdd(Microsoft.VisualBasic.DateInterval.Year, -1, CDate(mToDate)) '' DateAdd("Y", -1, mToDate)
                Else
                    pYear = pYear + 1
                    mToDate = VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY")
                End If
            End If


            mTableName = ConInventoryTable


            SqlStr = " SELECT ITEM.ITEM_CODE,"

            ''DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,DECODE(STOCK_TYPE,'FG',1,0))) * 
            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), ITEM.ITEM_CODE,  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as CLOSING_BALANCE"


            SqlStr = SqlStr & vbCrLf _
                & " FROM " & mTableName & " INV, " & vbCrLf _
                & " INV_ITEM_MST ITEM, INV_GENERAL_MST GMST"

            ''**********WHERE CLAUSE .......*************

            SqlStr = SqlStr & vbCrLf _
                & " WHERE INV.FYEAR=" & pYear & ""  ''RsCompany.Fields("FYEAR").Value 

            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID = 'WH'"


            SqlStr = SqlStr & vbCrLf _
                & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
                & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

            SqlStr = SqlStr & vbCrLf _
                & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE "

            ''
            SqlStr = SqlStr & vbCrLf & " AND GMST.GEN_CODE = '" & pCategoryCode & "'"

            'SqlStr = SqlStr & vbCrLf & " AND GMST.PRD_TYPE = 'P'"

            SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE = " & pCompanyCode & ""


            'SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "


            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"



            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


            SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
                & "  ITEM.ITEM_CODE,  INV.COMPANY_CODE "


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            GetClosingBalance = 0
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    GetClosingBalance = GetClosingBalance + IIf(IsDBNull(RsTemp.Fields("CLOSING_BALANCE").Value), 0, RsTemp.Fields("CLOSING_BALANCE").Value)
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        Exit Function
InsertErr:
        GetClosingBalance = 0
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function TRNQry() As String

        On Error GoTo ViewTrialErr

        Dim Sqlstr As String
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            'Sqlstr = Sqlstr & vbCrLf & " AND GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        ''********SELECTION..........
        Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " '' || TRN.CODE,  " & vbCrLf _
            & " TRN.NAME, " & vbCrLf _
            & " 'G' || TRN.GROUPCODE, " & vbCrLf _
            & " 'G' || TRN.GROUPCODE, " & vbCrLf _
            & " 'G' || TRN.GROUPCODE, " & vbCrLf _
            & " TRN.CATEGORY, BSGROUP_ACCTTYPE, ACMGROUP.GROUP_SCHEDULENO, " & vbCrLf _
            & " PREVIOUSFYAMT, " & vbCrLf _
            & " CURRENTFYRAMT,-1 "


        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf & " FROM TEMP_TRN TRN, " & vbCrLf _
            & " FIN_GROUP_MST ACMGROUP,FIN_BSGROUP_MST BSGROUP "


        ''********Joining..........
        Sqlstr = Sqlstr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACMGROUP.COMPANY_CODE " & vbCrLf _
            & " AND TRN.GROUPCODE=ACMGROUP.GROUP_CODE " & vbCrLf _
            & " AND ACMGROUP.COMPANY_CODE=BSGROUP.COMPANY_CODE " & vbCrLf _
            & " AND ACMGROUP.GROUP_BSCODEDR=BSGROUP.BSGROUP_CODE "

        If mCompanyCodeStr <> "" Then
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        ''********WHERE CLAUSE..........
        If UCase(lblType.Text) = UCase("Balance Sheet") Then
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.BSGROUP_PRINT='Y' "
        ElseIf UCase(lblType.Text) = UCase("Fund Flow") Then
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.FUNDFLOW_PRINT='Y' "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP.PLGROUP_PRINT='Y' "
        End If

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf & " TRN.CODE "

        TRNQry = Sqlstr
        Exit Function
ViewTrialErr:
        TRNQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume
    End Function

    Private Function BSQry() As String

        On Error GoTo ViewTrialErr
        Dim Sqlstr As String

        ''********SELECTION..........
        Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " BSGROUP_CATEGORY || BSGROUP_CODE, BSGROUP_NAME, " & vbCrLf _
            & " DECODE(BSGROUP_PARENTCODE,-1,''||BSGROUP_PARENTCODE,BSGROUP_CATEGORY || BSGROUP_PARENTCODE), " & vbCrLf _
            & " DECODE(BSGROUP_PARENTCODE,-1,''||BSGROUP_PARENTCODE,BSGROUP_CATEGORY || BSGROUP_PARENTCODE), " & vbCrLf _
            & " DECODE(BSGROUP_PARENTCODE,-1,''||BSGROUP_PARENTCODE,BSGROUP_CATEGORY || BSGROUP_PARENTCODE), " & vbCrLf _
            & " BSGROUP_CATEGORY,BSGROUP_ACCTTYPE,BSGROUP_SCHEDULENO, 0, 0,BSGROUP_SEQNO "

        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf _
            & " FROM FIN_BSGROUP_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BSGROUP_STATUS='O'"

        ''********WHERE CLAUSE..........
        If UCase(lblType.Text) = UCase("Balance Sheet") Then
            Sqlstr = Sqlstr & vbCrLf & " AND BSGROUP_PRINT='Y' "
        ElseIf UCase(lblType.Text) = UCase("Fund Flow") Then
            Sqlstr = Sqlstr & vbCrLf & " AND FUNDFLOW_PRINT='Y' "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND PLGROUP_PRINT='Y' "
        End If

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf _
            & " ORDER BY " & vbCrLf _
            & " BSGROUP_CODE "


        BSQry = Sqlstr
        Exit Function
ViewTrialErr:
        BSQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume
    End Function


    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        FraPreview.Visible = True
        FraPreview.BringToFront()
        With SprdView
            .Col = ColPic
            .ColHidden = True
            .set_ColWidth(ColDesc, 27 + 15)
            .set_ColWidth(ColSchd, 4)
            .set_ColWidth(ColCurrSubTotal, 12)
            .set_ColWidth(ColCurrTotal, 12)
            .set_ColWidth(ColPrevSubTotal, 12)
            .set_ColWidth(ColPrevTotal, 12)
        End With

        If UCase(lblType.Text) = UCase("Balance Sheet") Then
            SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Balance Sheet as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        ElseIf UCase(lblType.Text) = UCase("Fund Flow") Then
            SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Fund Flow as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        Else
            SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Profit & Loss A//c as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        End If
        Call SpreadSheetPreview(SprdView, SprdPreview, SprdCommand, VB6.PixelsToTwipsX(ClientRectangle.Width) - 200, VB6.PixelsToTwipsY(ClientRectangle.Height) - 200)

        With SprdView
            .Col = ColPic
            .ColHidden = False
            .set_ColWidth(ColDesc, 60)
        End With

    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click

        Dim Font1 As String
        Dim Font2 As String
        Dim Font3 As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ' Set printing options for spreadsheet
        With SprdView
            .Col = ColPic
            .ColHidden = True
            .set_ColWidth(ColDesc, 27 + 15)
            .set_ColWidth(ColSchd, 4)
            .set_ColWidth(ColCurrSubTotal, 12)
            .set_ColWidth(ColCurrTotal, 12)
            .set_ColWidth(ColPrevSubTotal, 12)
            .set_ColWidth(ColPrevTotal, 12)
        End With

        SprdView.PrintJobName = RsCompany.Fields("Company_Name").Value
        Font1 = "/fn""Arial""/fz""14""/fb1"
        Font2 = "/fn""Arial""/fz""10""/fb0"
        Font3 = "/fn""Arial""/fz""10""/fb1"



        If UCase(lblType.Text) = UCase("Balance Sheet") Then
            SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Balance Sheet as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        ElseIf UCase(lblType.Text) = UCase("Fund Flow") Then
            SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Fund Flow as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        Else
            SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Profit & Loss A//c as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        End If

        'SprdView.PrintFooter = "/cPrint Footer/rPage #/p/n2nd Line"


        Call SpreadPrint(SprdView)

        With SprdView
            .Col = ColPic
            .ColHidden = False
            .set_ColWidth(ColDesc, 60)
            .set_ColWidth(ColSchd, 4)
            .set_ColWidth(ColCurrSubTotal, 12)
            .set_ColWidth(ColCurrTotal, 12)
            .set_ColWidth(ColPrevSubTotal, 12)
            .set_ColWidth(ColPrevTotal, 12)
        End With


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        On Error GoTo RefreshErr
        Dim Sqlstr As String

        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        MainClass.ClearGrid(SprdView)
        FormatSprdView()
        Show1()
        PrintCommand(True)
        SprdView.Refresh()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
RefreshErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RsBalance As ADODB.Recordset
        Dim RowPos As Integer
        Dim mhaschildren As Boolean
        Dim mCurrTotal As Double
        Dim mCurrBalance As Double
        Dim mPrevTotal As Double
        Dim mPrevBalance As Double
        Dim mAcctType As Integer
        Dim mProfitLoss As Double
        Dim mPrevProfitLoss As Double

        Dim mSubTotal As Boolean

        Dim mGroupCurrTotal As Double
        Dim mGroupPrevTotal As Double

        Dim mRow As Integer
        Dim mDiff As Double

        Dim mCategory As String
        Dim mParentHead As String

        If FillIntoTempTRNQry = False Then GoTo ErrPart

        If InsertIntoBS = False Then GoTo ErrPart

        'Sqlstr = " SELECT * " & vbCrLf _
        '    & " FROM TEMP_BALANCESHEET " & vbCrLf _
        '    & " WHERE Category='H' AND PARENTCODE='-1' " & vbCrLf _
        '    & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '    & " ORDER BY SEQ_NO"

        Sqlstr = " SELECT TEMP_BALANCESHEET.*, LPAD(' ',4*(LEVEL-1)) || Name as BSGROUP_NAME, LEVEL " & vbCrLf _
                    & " FROM TEMP_BALANCESHEET " & vbCrLf _
                    & " WHERE Category='H' AND PARENTCODE='-1' " & vbCrLf _
                    & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " START WITH  PARENTCODE='-1' AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " CONNECT BY PRIOR CODE=PARENTCODE AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " ORDER SIBLINGS BY SEQ_NO, NAME"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalance, ADODB.LockTypeEnum.adLockReadOnly)

        RowPos = 1
        mSubTotal = False

        If RsBalance.EOF = False Then
            With SprdView
                RsBalance.MoveFirst()
                Do While Not RsBalance.EOF
                    mhaschildren = ChildFound((RsBalance.Fields("Code").Value), "")
                    mCategory = IIf(IsDBNull(RsBalance.Fields("Category").Value), "", RsBalance.Fields("Category").Value)
                    mParentHead = IIf(IsDBNull(RsBalance.Fields("PARENTCODE").Value), "", RsBalance.Fields("PARENTCODE").Value)

                    .Row = RowPos

                    .Col = ColPic
                    .CellType = SS_CELL_TYPE_PICTURE

                    If mhaschildren = True Then
                        .TypePictPicture = PicDown.Image
                        .Col = ColPicText
                        .Text = "D"
                    Else
                        .TypePictPicture = PicDash.Image
                        .Col = ColPicText
                        .Text = ""
                    End If

                    .Col = ColDesc
                    .Text = IIf(IsDBNull(RsBalance.Fields("BSGROUP_NAME").Value), "", RsBalance.Fields("BSGROUP_NAME").Value) 'IIf(IsDbNull(RsBalance.Fields("Name").Value), "", RsBalance.Fields("Name").Value)
                    .Font = VB6.FontChangeBold(.Font, True)
                    If mParentHead = "-1" Then
                        .FontSize = 12   '' VB6.FontChangeSize(.Font, 12)
                        .ForeColor = System.Drawing.ColorTranslator.FromOle(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue))
                    Else
                        .FontSize = 10   '' VB6.FontChangeSize(.Font, 12)
                        .ForeColor = System.Drawing.ColorTranslator.FromOle(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue))
                    End If


                    .Col = ColSchd
                    .Text = IIf(IsDBNull(RsBalance.Fields("SCHEDULENO").Value), "", RsBalance.Fields("SCHEDULENO").Value)

                    .Col = ColCode
                    .Text = CStr(IIf(IsDbNull(RsBalance.Fields("Code").Value), "", RsBalance.Fields("Code").Value))

                    .Col = ColCategory
                    .Text = IIf(IsDbNull(RsBalance.Fields("Category").Value), "", RsBalance.Fields("Category").Value)
                    mAcctType = IIf(IsDbNull(RsBalance.Fields("ACCTTYPE").Value), -1, RsBalance.Fields("ACCTTYPE").Value)

                    .Col = ColLevel
                    .Text = IIf(IsDBNull(RsBalance.Fields("LEVEL").Value), "", RsBalance.Fields("LEVEL").Value)

                    .Col = ColSNO
                    .Text = IIf(IsDBNull(RsBalance.Fields("SEQ_NO").Value), "", RsBalance.Fields("SEQ_NO").Value)

                    mCurrTotal = 0
                    mPrevTotal = 0


                    If mAcctType <> 4 Then
                        mGroupCurrTotal = 0
                        mGroupPrevTotal = 0
                    End If

                    If CalcBSAmount((RsBalance.Fields("Code").Value), mAcctType, mCurrTotal, mPrevTotal, RsBalance.Fields("CATEGORY").Value) = False Then GoTo ErrPart

                    .Col = ColCurrSubTotal
                    .Text = ""

                    .Col = ColCurrTotal
                    If mAcctType = 4 Then
                        mCurrProfit_Loss = 0 ' GetCurrentProfit()
                        mCurrTotal = mCurrTotal + mCurrProfit_Loss
                        .Text = IIf(mCurrTotal >= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)"))
                    Else
                        If mAcctType = 6 Or mAcctType = 1 Then
                            .Text = IIf(mCurrTotal <= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)"))
                        Else
                            .Text = IIf(mCurrTotal >= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)"))
                        End If
                    End If

                    .Col = ColPrevSubTotal
                    .Text = ""

                    .Col = ColPrevTotal
                    If mAcctType = 6 Or mAcctType = 1 Then
                        .Text = IIf(mPrevTotal <= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) ''Format(Abs(mPrevTotal), "##,##,##,##,###.00")
                    Else
                        .Text = IIf(mPrevTotal >= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) ''Format(Abs(mPrevTotal), "##,##,##,##,###.00")
                    End If

                    mProfitLoss = mProfitLoss + mCurrTotal
                    mPrevProfitLoss = mPrevProfitLoss + mPrevTotal

                    '                If mAcctType = 4 Then
                    '                    RowPos = RowPos + 1
                    '                    .MaxRows = RowPos
                    '
                    '                    .Col = ColCurrSubTotal
                    '                    .Text = ""
                    '
                    '                    .Col = ColCurrTotal
                    '                    .Text = Format(Abs(mProfitLoss), "##,##,##,##,###.00")
                    '
                    '                    .Col = ColPrevSubTotal
                    '                    .Text = ""
                    '
                    '                    .Col = ColPrevTotal
                    '                    .Text = Format(Abs(mProfitLoss), "##,##,##,##,###.00")
                    '
                    '                End If

                    RsBalance.MoveNext()

                    '                If RsBalance.EOF = True Then
                    '                    mSubTotal = True
                    '                Else
                    '                    If RsBalance!PARENTCODE = "-1" Then
                    '                        mSubTotal = True
                    '                    End If
                    '                End If


                    'If RsBalance.EOF = False Then
                    '    mParentHead = IIf(IsDBNull(RsBalance.Fields("PARENTCODE").Value), "", RsBalance.Fields("PARENTCODE").Value)
                    'Else
                    '    mParentHead = "-1"
                    'End If

                    'If mParentHead = "-1" Then
                    RowPos = RowPos + 1
                    .MaxRows = RowPos
                    .Row = RowPos
                    .Col = ColCurrTotal
                    .Text = New String("-", 100)
                    .Col = ColPrevTotal
                    .Text = New String("-", 100)

                    RowPos = RowPos + 1
                    .MaxRows = RowPos
                    .Row = RowPos
                    .Col = ColCurrTotal
                    mGroupCurrTotal = mGroupCurrTotal + mCurrTotal
                    .Text = VB6.Format(System.Math.Abs(mGroupCurrTotal), "##,##,##,##,###.00")
                    .Col = ColPrevTotal
                    mGroupPrevTotal = mGroupPrevTotal + mPrevTotal
                    .Text = VB6.Format(System.Math.Abs(mGroupPrevTotal), "##,##,##,##,###.00")

                    mDiff = mDiff + mGroupCurrTotal

                    RowPos = RowPos + 1
                    .MaxRows = RowPos
                    .Row = RowPos
                    .Col = ColCurrTotal
                    .Text = New String("=", 100)
                    .Col = ColPrevTotal
                    .Text = New String("=", 100)
                    'End If

                    RowPos = RowPos + 1
                    .MaxRows = RowPos
                Loop

                If UCase(lblType.Text) = UCase("Profit & Loss A/c") Then
                    .Row = .MaxRows
                    .Col = ColDesc
                    .Text = "Net Profit (Loss) Before Tax :"
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColCurrTotal
                    .Text = IIf(mProfitLoss > 0, "(" & VB6.Format(System.Math.Abs(mProfitLoss), "##,##,##,##,###.00") & ")", VB6.Format(System.Math.Abs(mProfitLoss), "##,##,##,##,###.00"))
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mProfitLoss > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))

                    .Col = ColPrevTotal
                    .Text = IIf(mPrevProfitLoss > 0, "(" & VB6.Format(System.Math.Abs(mPrevProfitLoss), "##,##,##,##,###.00") & ")", VB6.Format(System.Math.Abs(mPrevProfitLoss), "##,##,##,##,###.00"))
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mProfitLoss > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))

                End If
            End With
        End If

        Dim mCode As String

Reset:
        With SprdView
            For mRow = 1 To .MaxRows
                'If .MaxRows < 100 Then
                .Row = mRow
                .Col = ColCode
                mCode = Trim(.Text)
                .Col = ColCategory
                If .Text = "H" Then
                    .Col = ColPicText
                    If .Text = "D" Then
                        mhaschildren = ChildFound(mCode, "H")
                        If mhaschildren = True Then
                            Call SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(ColPic, mRow))
                            GoTo Reset
                        End If

                    End If
                End If
                'End If
            Next
        End With
        txtDiff.Text = VB6.Format(mDiff, "##,##,##,##,###.00")
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Function CalcBSAmount(ByVal mParentcode As String, ByVal mAcctType As Integer, ByRef mCurrAmount As Double, ByRef mPrevAmount As Double, ByVal mCategory As String) As Boolean

        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim RsCalcStock As ADODB.Recordset
        Dim mAmount As Double

        If mAcctType = ConLiabilities Or mAcctType = ConPnLAcct Or mAcctType = ConIncome Then
            mAmount = -1
        Else
            mAmount = 1
        End If


        Sqlstr = " SELECT SUM(CURRENTFYRAMT) AS CurrAmount, " & vbCrLf _
            & " SUM(PREVIOUSFYAMT) AS PrevAmount " & vbCrLf _
            & " FROM TEMP_BALANCESHEET " & vbCrLf _
            & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        If mCategory = "G" Or mCategory = "H" Then
            Sqlstr = Sqlstr & vbCrLf _
                & "START WITH PARENTCODE='" & mParentcode & "'" & vbCrLf _
                & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                & " CONNECT BY PRIOR CODE=PARENTCODE " & vbCrLf _
                & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        Else
            Sqlstr = Sqlstr & vbCrLf & "AND  CODE='" & mParentcode & "'"
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalcStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCalcStock.EOF = False Then
            mCurrAmount = IIf(IsDBNull(RsCalcStock.Fields("CurrAmount").Value), 0, RsCalcStock.Fields("CurrAmount").Value)
            mPrevAmount = IIf(IsDBNull(RsCalcStock.Fields("PrevAmount").Value), 0, RsCalcStock.Fields("PrevAmount").Value)
        Else
            mCurrAmount = 0
            mPrevAmount = 0
        End If
        CalcBSAmount = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        mCurrAmount = 0
        mPrevAmount = 0
        CalcBSAmount = False
    End Function

    Private Function ChildFound(ByRef mParentcode As String, ByRef mOnlyBSGroup As String) As Boolean

        On Error GoTo ErrorPart
        Dim Sqlstr As String
        Dim RsChild As ADODB.Recordset


        Sqlstr = "SELECT Code FROM TEMP_BALANCESHEET " & vbCrLf _
            & " WHERE PARENTCODE = '" & mParentcode & "' "

        If mOnlyBSGroup <> "" Then
            Sqlstr = Sqlstr & vbCrLf & "AND CATEGORY = '" & mOnlyBSGroup & "' "
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChild, ADODB.LockTypeEnum.adLockReadOnly)

        If RsChild.EOF = True Then
            ChildFound = False
        Else
            ChildFound = True
        End If
        RsChild.Close()
        Exit Function
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ChildFound = False
    End Function

    Private Sub FillHeading()
        On Error GoTo ErrPart
        With SprdView
            .Row = 0

            .Col = ColPic
            .Text = "U/D"

            .Col = ColDesc
            .Text = "PARTICULAR"

            .Col = ColSchd
            .Text = "Sc. No."

            .Col = ColCurrSubTotal
            .Text = " "

            .Col = ColCurrTotal
            .Text = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & vbNewLine & " - " & vbNewLine & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


            .Col = ColPrevSubTotal
            .Text = " "

            .Col = ColPrevTotal
            .Text = "For The Period Ended " & DateAdd("d", -1, VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY"))

            .Col = ColSNO
            .Text = "S. No."
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        FieldsVerification = False
        If txtDateFrom.Text = "" Then
            MsgBox("Date Cannot Be Blank", MsgBoxStyle.Critical)
            txtDateFrom.Focus()
            Exit Function
        ElseIf txtDateFrom.Text <> "" Then
            If Not IsDate(txtDateFrom.Text) Then
                MsgBox("Please enter vaild Date.", MsgBoxStyle.Critical)
                txtDateFrom.Focus()
                Exit Function
            End If


            If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
                txtDateFrom.Focus()
                Exit Function
            End If

        End If
        If txtDateTo.Text = "" Then
            MsgBox("Date Cannot Be Blank", MsgBoxStyle.Critical)
            txtDateTo.Focus()
            Exit Function
        ElseIf txtDateTo.Text <> "" Then
            If Not IsDate(txtDateTo.Text) Then
                MsgBox("Please enter vaild Date.", MsgBoxStyle.Critical)
                txtDateTo.Focus()
                Exit Function
            End If

            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
                txtDateTo.Focus()
                Exit Function
            End If

        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub frmBalanceSheet_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If UCase(lblType.Text) = UCase("Balance Sheet") Then
            Me.Text = "Balance Sheet"
        ElseIf UCase(lblType.Text) = UCase("Fund Flow") Then
            Me.Text = "Fund Flow"
        ElseIf UCase(lblType.Text) = UCase("Profit & Loss A/c") Then
            Me.Text = "Profit & Loss A/c"
        End If

    End Sub

    Private Sub frmBalanceSheet_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Long



        Call SetMainFormCordinate(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormLoaded = True ''false
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        lstCompanyName.Items.Clear()
        Sqlstr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        Dim mCompanyName As String
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_NAME").Value), "", RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = RsCompany.Fields("COMPANY_NAME").Value, True, False))
                'lstCompanyName.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0



        MainClass.SetControlsColor(Me)
        FormatSprdView()
        PrintCommand(False)

        SprdView.PrintMarginTop = 0.75 * 1440
        SprdView.PrintMarginBottom = 0.75 * 1440
        SprdView.PrintMarginLeft = 0.5 * 1440
        SprdView.PrintMarginRight = 0.5 * 1440
        chkOpening.CheckState = System.Windows.Forms.CheckState.Checked
        'Init then zoom display
        zoomindex = 2 ''8   'page height

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub FormatSprdView()

        On Error GoTo ErrPart
        With SprdView
            .set_RowHeight(0, ConRowHeight)
            .Row = -1
            .MaxCols = ColCategory

            .Col = ColPicText
            .set_ColWidth(ColPicText, 4)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColPic
            .set_ColWidth(ColPic, 4)
            .CellType = SS_CELL_TYPE_PICTURE

            .Col = ColDesc
            .set_ColWidth(ColDesc, 60)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeMaxEditLen = 400
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColSchd
            .set_ColWidth(ColSchd, 4)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = ColCurrSubTotal
            .set_ColWidth(ColCurrSubTotal, 12)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCurrTotal
            .set_ColWidth(ColCurrTotal, 12)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColPrevSubTotal
            .set_ColWidth(ColPrevSubTotal, 12)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColPrevTotal
            .set_ColWidth(ColPrevTotal, 12)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCode
            .set_ColWidth(ColCode, 5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColCategory
            .set_ColWidth(ColCategory, 5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColLevel
            .set_ColWidth(ColLevel, 5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColSNO
            .set_ColWidth(ColSNO, 5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .ColHidden = False

            FillHeading()

            .UserResize = FPSpreadADO.UserResizeConstants.UserResizeColumns  ''.UserResizeNone
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1, False)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            'SprdView.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdCommand_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdCommand.ButtonClicked
        On Error GoTo ERR1
        Dim mFilename As String

        SprdCommand.Col = eventArgs.col
        SprdCommand.Row = eventArgs.row

        If SprdCommand.CellType = FPSpreadADO.CellTypeConstants.CellTypeButton Then
            Select Case eventArgs.col
                Case 2 'Next
                    ShowNextPage(SprdView, SprdPreview, SprdCommand, eventArgs.col)

                Case 4 'Previous
                    ShowPreviousPage(SprdView, SprdPreview, SprdCommand, eventArgs.col)

                Case 6 'Zoom
                    SprdPreview.ZoomState = 3

                Case 8 'Print
                    cmdPrint_Click(cmdPrint, New System.EventArgs())

                Case 10 'Export
                    'mFilename = ExportSprdToExcel(CommonDialog1)

                    'If SprdView.ExportToExcel(mFilename, "BSheet", "") = True Then
                    '    MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                    'End If

                Case 18 'Close
                    FraPreview.Visible = False
                    With SprdView
                        .Col = ColPic
                        .ColHidden = False
                        .set_ColWidth(ColDesc, 30)
                        .set_ColWidth(ColSchd, 4)
                        .set_ColWidth(ColCurrSubTotal, 12)
                        .set_ColWidth(ColCurrTotal, 12)
                        .set_ColWidth(ColPrevSubTotal, 12)
                        .set_ColWidth(ColPrevSubTotal, 12)
                    End With
            End Select
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            Exit Sub
        End If
        MsgInformation(Err.Description)
    End Sub


    Private Sub SprdCommand_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles SprdCommand.TextTipFetch
        With SprdCommand
            .Col = eventArgs.col
            .Row = eventArgs.row
            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeButton And Not .Lock Then
                eventArgs.showTip = True
                eventArgs.tipText = .TypeButtonText
            ElseIf .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit And .Text <> "" Then
                eventArgs.showTip = True
                eventArgs.tipText = .Text
            End If
        End With
    End Sub


    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        PrintCommand(False)
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintCommand(False)
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Boolean)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        On Error GoTo ERR1
        Dim mCode As String
        Dim mAcctCode As String
        Dim mCategory As String
        Dim mPicValue As String
        Dim mhaschildren As Boolean
        Dim mCurrTotal As Double
        Dim mPrevTotal As Double
        Dim mAcctType As Integer
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset
        Dim mLevel As Long

        If eventArgs.col = ColPic Then
            With SprdView
                .Row = eventArgs.row
                .Col = ColCode
                mCode = .Text
                If .Text = "" Then Exit Sub

                .Col = ColCategory
                mCategory = .Text

                .Col = ColLevel
                mLevel = Val(.Text)

                .Col = ColPicText
                If Trim(.Text) = "" Then Exit Sub
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                mPicValue = .Text

                .Col = ColPic
                .CellType = SS_CELL_TYPE_PICTURE

                mhaschildren = ChildFound(mCode, "")

                If mPicValue = "D" Then
                    If mhaschildren = True Then
                        .TypePictPicture = PicUp.Image
                        .Col = ColPicText
                        .Text = "U"

                        .Col = ColCurrSubTotal
                        .Text = ""

                        .Col = ColCurrTotal
                        .Text = ""

                        .Col = ColPrevSubTotal
                        .Text = ""

                        .Col = ColPrevTotal
                        .Text = ""
                    Else
                        .TypePictPicture = PicDash.Image
                        .Col = ColPicText
                        .Text = ""
                        Exit Sub
                    End If

                    Call StretchUpGroupCol(eventArgs.row, mCode, mPicValue, 0, mLevel)
                ElseIf mPicValue = "U" Then
                    If mhaschildren = True Then
                        .TypePictPicture = PicDown.Image
                        .Col = ColPicText
                        .Text = "D"
                    Else
                        .TypePictPicture = PicDash.Image
                        .Col = ColPicText
                        .Text = ""
                        Exit Sub
                    End If

                    .Col = ColPicText
                    If .Text <> "" Then
                        mAcctCode = Mid(mCode, 2)
                        If mCategory = "G" Then
                            Sqlstr = " SELECT BSGROUP_ACCTTYPE " & vbCrLf _
                                & " FROM FIN_BSGROUP_MST BS,FIN_GROUP_MST GR" & vbCrLf _
                                & " WHERE BS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND GR.COMPANY_CODE=BS.COMPANY_CODE " & vbCrLf _
                                & " AND GR.GROUP_BSCODEDR=BS.BSGROUP_CODE " & vbCrLf _
                                & " AND GR.GROUP_CODE=" & mAcctCode & ""
                        Else
                            Sqlstr = " SELECT BSGROUP_ACCTTYPE " & vbCrLf _
                                & " FROM FIN_BSGROUP_MST BS" & vbCrLf _
                                & " WHERE BS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND BS.BSGROUP_CODE=" & mAcctCode & ""
                        End If
                        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mAcctType = IIf(IsDBNull(RsTemp.Fields("BSGROUP_ACCTTYPE").Value), 1, RsTemp.Fields("BSGROUP_ACCTTYPE").Value)
                        End If
                    End If

                    CalcBSAmount(mCode, -1, mCurrTotal, mPrevTotal, mCategory)


                    If RsTemp.Fields("BSGROUP_ACCTTYPE").Value = "4" Then
                        mCurrTotal = mCurrTotal + mCurrProfit_Loss '' GetCurrentProfit()
                    End If

                    If .Text = "D" Then
                        If mCategory = "H" Or mCategory = "G" Then
                            .Col = ColCurrSubTotal
                            .Text = ""

                            .Col = ColCurrTotal


                            If mAcctType = 6 Or mAcctType = 1 Then
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mCurrTotal <= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) ''
                            Else
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mCurrTotal >= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) ''Format(Abs(mCurrTotal), "##,##,##,##,###.00")
                            End If

                            .Col = ColPrevSubTotal
                            .Text = ""

                            .Col = ColPrevTotal

                            If mAcctType = 6 Or mAcctType = 1 Then
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mPrevTotal <= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) '
                            Else
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mPrevTotal >= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mPrevTotal), "##,##,##,##,###.00")
                            End If
                        Else
                            .Col = ColCurrSubTotal

                            If mAcctType = 6 Or mAcctType = 1 Then
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mCurrTotal <= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)"))
                            Else
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mCurrTotal >= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) '' Format(Abs(mCurrTotal), "##,##,##,##,###.00")
                            End If

                            .Col = ColCurrTotal
                            .Text = ""

                            .Col = ColPrevSubTotal

                            If mAcctType = 6 Or mAcctType = 1 Then
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mPrevTotal <= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) '
                            Else
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                                .Text = IIf(mPrevTotal >= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mPrevTotal), "##,##,##,##,###.00")
                            End If

                            .Col = ColPrevTotal
                            .Text = ""

                        End If
                    ElseIf .Text = "U" Then
                        .Col = ColCurrSubTotal
                        .Text = ""

                        .Col = ColCurrTotal
                        .Text = ""

                        .Col = ColPrevSubTotal
                        .Text = ""

                        .Col = ColPrevTotal
                        .Text = ""

                    Else
                        .Col = ColCurrSubTotal

                        If mAcctType = 6 Or mAcctType = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrTotal <= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) '
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrTotal >= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mCurrTotal), "##,##,##,##,###.00")
                        End If

                        .Col = ColCurrTotal
                        .Text = ""

                        .Col = ColPrevSubTotal

                        If mAcctType = 6 Or mAcctType = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevTotal <= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) '
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevTotal >= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mPrevTotal), "##,##,##,##,###.00")
                        End If

                        .Col = ColPrevTotal
                        .Text = ""

                    End If
                    Call StretchDownGroupCol(eventArgs.row, mCode)
                End If
            End With
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else
            Call ViewAccountLedger()
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            Exit Sub
        End If
        MsgInformation(Err.Description)
    End Sub
    Private Sub StretchUpGroupCol(ByRef mRow As Integer, ByRef mCode As String, ByRef mPicValue As String, ByRef mAcctType As Integer, ByRef mLevel As Long)

        On Error GoTo ErrPart

        Dim RsTemp As ADODB.Recordset
        Dim RowPos As Integer
        Dim mhaschildren As Boolean
        Dim Sqlstr As String
        mhaschildren = True
        Dim mCurrSubTotal As Double
        Dim mCurrTotal As Double
        Dim mPrevSubTotal As Double
        Dim mPrevTotal As Double
        Dim mIsGroupSum As String
        Dim mGroupCurrSum As Double
        Dim mXRow As Integer
        Dim mDirectHead As Boolean
        Dim xAcctType As Integer
        '    If mCategory <> "H" And mCategory <> "G" Then
        '        Exit Sub
        '    End If
        '
        mDirectHead = False
        Sqlstr = " SELECT DISTINCT CODE, ACCTTYPE, Category, SCHEDULENO , SEQ_NO, NAME, LPAD(' ',4 * CASE WHEN CATEGORY IN ('H','G') THEN 1 ELSE 1.5 END * ((" & mLevel & " + LEVEL)-1)) || NAME AS TRN_NAME, " & mLevel & " + LEVEL - 1 AS LEVEL1" & vbCrLf _
            & " FROM TEMP_BALANCESHEET " & vbCrLf _
            & " WHERE PARENTCODE='" & mCode & "' AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " START WITH  CODE='" & mCode & "'" & vbCrLf _
            & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " CONNECT BY PRIOR CODE= PARENTCODE " & vbCrLf _
            & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        Sqlstr = Sqlstr & vbCrLf & "ORDER SIBLINGS BY SEQ_NO, NAME"

        'Sqlstr = " SELECT TEMP_BALANCESHEET.*, LPAD(' ',4*(LEVEL-1)) || Name as BSGROUP_NAME " & vbCrLf _
        '            & " FROM TEMP_BALANCESHEET " & vbCrLf _
        '            & " WHERE Category='H' AND PARENTCODE='-1' " & vbCrLf _
        '            & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " START WITH  PARENTCODE='-1' AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " CONNECT BY PRIOR CODE=PARENTCODE AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        '            & " ORDER SIBLINGS BY SEQ_NO"

        '

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        RowPos = mRow

        If RsTemp.EOF = False Then
            With SprdView
                Do While Not RsTemp.EOF
                    RowPos = RowPos + 1
                    .MaxRows = .MaxRows + 1

                    .Row = RowPos
                    .Action = SS_ACTION_INSERT_ROW

                    .Col = ColPic
                    .CellType = SS_CELL_TYPE_PICTURE
                    mhaschildren = ChildFound((RsTemp.Fields("Code").Value), "")

                    If mhaschildren = True Then
                        .TypePictPicture = PicDown.Image
                        .Col = ColPicText
                        .Text = "D"
                    Else
                        .TypePictPicture = PicDash.Image
                        .Col = ColPicText
                        .Text = ""
                    End If

                    .Col = ColDesc
                    .Text = RsTemp.Fields("TRN_NAME").Value
                    xAcctType = RsTemp.Fields("ACCTTYPE").Value


                    '.Font = VB6.FontChangeBold(.Font, True)
                    '.FontSize = 12   '' VB6.FontChangeSize(.Font, 12)
                    '.ForeColor = System.Drawing.ColorTranslator.FromOle(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue))



                    If RsTemp.Fields("Category").Value = "H" Then
                        .Font = VB6.FontChangeBold(.Font, True)
                        .FontSize = 10
                        .ForeColor = System.Drawing.ColorTranslator.FromOle(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue))
                    ElseIf RsTemp.Fields("Category").Value = "G" Then
                        .Font = VB6.FontChangeBold(.Font, True)
                        .FontSize = 10
                        .ForeColor = System.Drawing.ColorTranslator.FromOle(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
                    Else
                        .Font = VB6.FontChangeBold(.Font, False)
                        .FontSize = 10
                    End If

                    .Col = ColSchd
                    .Text = IIf(IsDBNull(RsTemp.Fields("SCHEDULENO").Value), "", RsTemp.Fields("SCHEDULENO").Value)

                    CalcBSAmount((RsTemp.Fields("Code").Value), (RsTemp.Fields("AcctType").Value), mCurrSubTotal, mPrevSubTotal, RsTemp.Fields("CATEGORY").Value)
                    If RsTemp.Fields("AcctType").Value = "4" Then
                        mCurrSubTotal = mCurrSubTotal + mCurrProfit_Loss '' GetCurrentProfit()
                    End If

                    If RsTemp.Fields("Category").Value = "H" Or RsTemp.Fields("Category").Value = "G" Then
                        mIsGroupSum = "N"
                    Else
                        mIsGroupSum = "Y"
                        '                    mCurrSubTotal = IIf(IsNull(RsTemp!CURRENTFYRAMT), 0, RsTemp!CURRENTFYRAMT)
                        '                    mPrevSubTotal = IIf(IsNull(RsTemp!PREVIOUSFYAMT), 0, RsTemp!PREVIOUSFYAMT)
                    End If


                    If mIsGroupSum = "Y" Then
                        .Col = ColCurrSubTotal

                        If RsTemp.Fields("AcctType").Value = 6 Or RsTemp.Fields("AcctType").Value = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrSubTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrSubTotal <= 0, VB6.Format(System.Math.Abs(mCurrSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrSubTotal), "(##,##,##,##,###.00)")) '   Format(Abs(mCurrSubTotal), "##,##,##,##,###.00")
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrSubTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrSubTotal >= 0, VB6.Format(System.Math.Abs(mCurrSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrSubTotal), "(##,##,##,##,###.00)")) '   Format(Abs(mCurrSubTotal), "##,##,##,##,###.00")
                        End If
                        mCurrTotal = mCurrTotal + mCurrSubTotal

                        .Col = ColCurrTotal
                        .Text = ""

                        .Col = ColPrevSubTotal

                        If RsTemp.Fields("AcctType").Value = 6 Or RsTemp.Fields("AcctType").Value = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevSubTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevSubTotal <= 0, VB6.Format(System.Math.Abs(mPrevSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevSubTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mPrevSubTotal), "##,##,##,##,###.00")
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevSubTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevSubTotal >= 0, VB6.Format(System.Math.Abs(mPrevSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevSubTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mPrevSubTotal), "##,##,##,##,###.00")
                        End If
                        mPrevTotal = mPrevTotal + mPrevSubTotal

                        .Col = ColPrevTotal
                        .Text = ""
                        mXRow = RowPos
                        mDirectHead = True
                    Else
                        .Col = ColCurrSubTotal
                        .Text = ""

                        .Col = ColCurrTotal

                        If RsTemp.Fields("AcctType").Value = 6 Or RsTemp.Fields("AcctType").Value = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrSubTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrSubTotal <= 0, VB6.Format(System.Math.Abs(mCurrSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrSubTotal), "(##,##,##,##,###.00)"))
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrSubTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrSubTotal >= 0, VB6.Format(System.Math.Abs(mCurrSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrSubTotal), "(##,##,##,##,###.00)")) 'Format(Abs(mCurrSubTotal), "##,##,##,##,###.00")
                        End If

                        .Col = ColPrevSubTotal
                        .Text = ""

                        .Col = ColPrevTotal

                        If RsTemp.Fields("AcctType").Value = 6 Or RsTemp.Fields("AcctType").Value = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevSubTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevSubTotal <= 0, VB6.Format(System.Math.Abs(mPrevSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevSubTotal), "(##,##,##,##,###.00)")) '
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevSubTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevSubTotal >= 0, VB6.Format(System.Math.Abs(mPrevSubTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevSubTotal), "(##,##,##,##,###.00)")) 'Format(Abs(mPrevSubTotal), "##,##,##,##,###.00")
                        End If
                    End If

                    .Col = ColCode
                    .Text = CStr(IIf(IsDBNull(RsTemp.Fields("Code").Value), "", RsTemp.Fields("Code").Value))

                    .Col = ColCategory
                    .Text = IIf(IsDBNull(RsTemp.Fields("Category").Value), "", RsTemp.Fields("Category").Value)

                    .Col = ColLevel
                    .Text = IIf(IsDBNull(RsTemp.Fields("LEVEL1").Value), "", RsTemp.Fields("LEVEL1").Value)

                    .Col = ColSNO
                    .Text = IIf(IsDBNull(RsTemp.Fields("SEQ_NO").Value), "", RsTemp.Fields("SEQ_NO").Value)

                    RsTemp.MoveNext()
                    If RsTemp.EOF = True And mDirectHead = True Then
                        .Row = mXRow
                        .Col = ColCurrTotal


                        If xAcctType = 6 Or xAcctType = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrTotal <= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mCurrTotal), "##,##,##,##,###.00")
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mCurrTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mCurrTotal >= 0, VB6.Format(System.Math.Abs(mCurrTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mCurrTotal), "(##,##,##,##,###.00)")) ' Format(Abs(mCurrTotal), "##,##,##,##,###.00")
                        End If

                        .Col = ColPrevTotal

                        If xAcctType = 6 Or xAcctType = 1 Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal > 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevTotal <= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) '
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(mPrevTotal <= 0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)))
                            .Text = IIf(mPrevTotal >= 0, VB6.Format(System.Math.Abs(mPrevTotal), "##,##,##,##,###.00"), VB6.Format(System.Math.Abs(mPrevTotal), "(##,##,##,##,###.00)")) 'Format(Abs(mPrevTotal), "##,##,##,##,###.00")
                        End If
                    End If
                Loop
            End With
        End If
        Exit Sub

ErrPart:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub StretchDownGroupCol(ByRef mRow As Integer, ByRef mCode As String)

        On Error GoTo MakeErr
        Dim RsTemp As ADODB.Recordset
        Dim RowPos As Integer
        Dim Sqlstr As String

        Sqlstr = " SELECT * " & vbCrLf _
            & " FROM TEMP_BALANCESHEET WHERE " & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " START WITH  PARENTCODE='" & mCode & "'" & vbCrLf _
            & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " CONNECT BY PRIOR CODE= PARENTCODE " & vbCrLf _
            & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        RowPos = mRow + 1
        If RsTemp.EOF = False Then
Rec1:
            RsTemp.MoveFirst()
            Do While Not RsTemp.EOF
                SprdView.Row = RowPos
                SprdView.Col = ColDesc
                If UCase(LTrim(RsTemp.Fields("Name").Value)) = UCase(LTrim(SprdView.Text)) Then
                    SprdView.Action = SS_ACTION_DELETE_ROW
                    SprdView.MaxRows = SprdView.MaxRows - 1
                    GoTo Rec1
                End If
                If UCase(LTrim(SprdView.Text)) = "" Then Exit Sub
                RsTemp.MoveNext()
            Loop
        End If
        Exit Sub

MakeErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function GetCurrentProfit(ByRef xCompanyCode As Long) As Double

        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RsOPBalSumm As ADODB.Recordset
        Dim mFinalSheet As Boolean

        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        Dim mNetClosingAmount As Double
        Dim mNetOpeningAmount As Double

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            'Sqlstr = Sqlstr & vbCrLf & " AND GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        ' select distinct GROUP_TYPE from fin_group_mst;


        If CDate(txtDateTo.Text) = CDate(RsCompany.Fields("END_DATE").Value) Then
            mFinalSheet = True
        Else
            mFinalSheet = False
        End If


        Sqlstr = " Select SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP" & vbCrLf _
            & " WHERE TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf _
            & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf _
            & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE" & vbCrLf _
            & " AND ACMGROUP.GROUP_TYPE='E' AND TRN.PL_FLAG='N' AND TRN.COMPANY_CODE=" & xCompanyCode & ""

        'If mCompanyCodeStr <> "" Then
        '    'mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        '    Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        'End If

        Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE || TRN.ACCOUNTCODE NOT IN (" & vbCrLf _
                & " SELECT DISTINCT COMPANY_CODE || OP_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST WHERE OP_ACCOUNT  IS NOT NULL AND COMPANY_CODE=" & xCompanyCode & ""

        'If mCompanyCodeStr <> "" Then
        '    Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        'End If

        Sqlstr = Sqlstr & vbCrLf _
                & " UNION ALL" & vbCrLf _
                & " SELECT DISTINCT COMPANY_CODE || CL_ACCOUNT FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT  IS NOT NULL AND COMPANY_CODE=" & xCompanyCode & ""

        'If mCompanyCodeStr <> "" Then
        '    Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        'End If

        Sqlstr = Sqlstr & vbCrLf _
                & ")"

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBalSumm, ADODB.LockTypeEnum.adLockReadOnly)

        If RsOPBalSumm.EOF = False Then
            GetCurrentProfit = IIf(IsDBNull(RsOPBalSumm.Fields("BALANCE").Value), 0, RsOPBalSumm.Fields("BALANCE").Value)
        End If

        ''-----------------------------------

        Dim SqlstrCat As String = ""
        Dim mClosingStock As Double = 0
        Dim RsTempCat As ADODB.Recordset = Nothing
        Dim mAccountCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pCompanyCode As Long
        Dim mCategoryCode As String
        Dim mCurrFYear As String
        Dim mOpeningStock As Double

        mCurrFYear = RsCompany.Fields("FYEAR").Value



        ''Closing Balance 
        Dim mFGClosingStock As Double = 0

        SqlstrCat = "SELECT DISTINCT CL_ACCOUNT ACCOUNT_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT  IS NOT NULL AND COMPANY_CODE=" & xCompanyCode & ""

        'If mCompanyCodeStr <> "" Then
        '    SqlstrCat = SqlstrCat & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        'End If

        If chkClosingStock.Checked = False Then
            SqlstrCat = SqlstrCat & vbCrLf & " AND 1=2"
        End If

        MainClass.UOpenRecordSet(SqlstrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)

        mClosingStock = 0

        If RsTempCat.EOF = False Then
            Do While RsTempCat.EOF = False

                mAccountCode = IIf(IsDBNull(RsTempCat.Fields("ACCOUNT_CODE").Value), "", RsTempCat.Fields("ACCOUNT_CODE").Value)

                mClosingStock = 0

                Sqlstr = "SELECT DISTINCT CATEGORY_CODE, COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE CL_ACCOUNT ='" & mAccountCode & "' AND COMPANY_CODE=" & xCompanyCode & ""

                'If mCompanyCodeStr <> "" Then
                '    Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
                'End If

                If chkClosingStock.Checked = False Then
                    Sqlstr = Sqlstr & vbCrLf & " AND 1=2"
                End If

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    'mPrevClosingStock = GetLedgerStockBalance(mAccountCode, RsCompany.Fields("FYEAR").Value - 1, mCompanyCodeStr)

                    Do While RsTemp.EOF = False
                        pCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)
                        mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)

                        mClosingStock = mClosingStock + GetClosingBalance(pCompanyCode, mCategoryCode, "CL", mCurrFYear, mAccountCode)

                        RsTemp.MoveNext()
                    Loop


                    mClosingStock = mClosingStock * -1
                    mNetClosingAmount = mNetClosingAmount + mClosingStock
                    GetCurrentProfit = GetCurrentProfit + mClosingStock


                End If
                RsTempCat.MoveNext()
            Loop
        End If

        ''Opening Balance 
        Dim mFGOpeningStock As Double = 0

        SqlstrCat = "SELECT DISTINCT OP_ACCOUNT ACCOUNT_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE OP_ACCOUNT  IS NOT NULL AND COMPANY_CODE=" & xCompanyCode & ""

        'If mCompanyCodeStr <> "" Then
        '    SqlstrCat = SqlstrCat & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        'End If

        If chkOpeningStock.Checked = False Then
            SqlstrCat = SqlstrCat & vbCrLf & " AND 1=2"
        End If

        MainClass.UOpenRecordSet(SqlstrCat, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCat, ADODB.LockTypeEnum.adLockReadOnly)

        mClosingStock = 0

        If RsTempCat.EOF = False Then
            Do While RsTempCat.EOF = False

                mAccountCode = IIf(IsDBNull(RsTempCat.Fields("ACCOUNT_CODE").Value), "", RsTempCat.Fields("ACCOUNT_CODE").Value)

                mOpeningStock = 0

                Sqlstr = "SELECT DISTINCT CATEGORY_CODE, COMPANY_CODE FROM GEN_CATEGORY_MAPPING_MST WHERE OP_ACCOUNT ='" & mAccountCode & "' AND COMPANY_CODE=" & xCompanyCode & " "

                'If mCompanyCodeStr <> "" Then
                '    Sqlstr = Sqlstr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
                'End If

                If chkOpeningStock.Checked = False Then
                    Sqlstr = Sqlstr & vbCrLf & " AND 1=2"
                End If

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    'mPrevOpeningStock = GetLedgerStockBalance(mAccountCode, RsCompany.Fields("FYEAR").Value - 1, mCompanyCodeStr)
                    Do While RsTemp.EOF = False
                        pCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)
                        mCategoryCode = IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)

                        mOpeningStock = mOpeningStock + GetClosingBalance(pCompanyCode, mCategoryCode, "OP", mCurrFYear, mAccountCode)

                        RsTemp.MoveNext()
                    Loop

                    mNetOpeningAmount = mNetOpeningAmount + mOpeningStock
                    GetCurrentProfit = GetCurrentProfit + mOpeningStock

                End If

                RsTempCat.MoveNext()
            Loop
        End If

        '''--------------------------------------
        ''From Addition Voucher

        Sqlstr = ""
        If mFinalSheet = False Then
            ''********SELECTION..........
            Sqlstr = "SELECT SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf _
                & " FROM FIN_PROFITLOSS_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP" & vbCrLf _
                & " WHERE TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf _
                & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf _
                & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE" & vbCrLf _
                & " AND ACMGROUP.GROUP_TYPE='E' AND TRN.PL_FLAG='N' AND TRN.COMPANY_CODE=" & xCompanyCode & ""

            'If mCompanyCodeStr <> "" Then
            '    'mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            '    Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
            'End If

            If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            Else
                Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBalSumm, ADODB.LockTypeEnum.adLockReadOnly)

            If RsOPBalSumm.EOF = False Then
                GetCurrentProfit = GetCurrentProfit + IIf(IsDBNull(RsOPBalSumm.Fields("BALANCE").Value), 0, RsOPBalSumm.Fields("BALANCE").Value)
            End If
        End If


        Exit Function
ErrPart:
        GetCurrentProfit = 0
    End Function
    Private Sub ViewAccountLedger()
        On Error GoTo ErrPart
        Dim mAccountCode As String
        Dim mAccountName As String

        If SprdView.ActiveRow <= 0 Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedger.MdiParent = Me.MdiParent
        frmViewLedger.lblBookType.Text = "LEDG"
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = ColCategory
        If SprdView.Text = "G" Or SprdView.Text = "H" Then
            'MsgInformation("Ledger no allowed for Group Or Head")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        SprdView.Col = ColDesc
        mAccountName = LTrim(RTrim(SprdView.Text))
        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'frmViewLedger.lblAcCode.Text = MasterNo
        frmViewLedger.txtDateFrom.Text = txtDateFrom.Text
        frmViewLedger.txtDateTo.Text = txtDateTo.Text
        frmViewLedger.OptSumDet(2).Checked = True
        'frmViewLedger.cboDivision.Text = cboDivision.Text
        frmViewLedger.MdiParent = Me.MdiParent
        frmViewLedger.Show()

        frmViewLedger.cboAccount.Text = mAccountName ''LTrim(RTrim(SprdView.Text))
        'MainClass.ValidateWithMasterTable(SprdView.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        frmViewLedger.lblAcCode.Text = mAccountCode

        'frmViewLedger.CboCC.Text = CboCC.Text
        'frmViewLedger.CboDept.Text = CboDept.Text
        ''frmViewLedger.cboConsolidated.ListIndex = 3     ''DIVISION...
        frmViewLedger.frmViewLedger_Activated(Nothing, New System.EventArgs())
        frmViewLedger.cmdShow_Click(Nothing, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmBalanceSheet_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdView.Width = VB6.TwipsToPixelsX(mReFormWidth - 150)
        fraGrid.Width = VB6.TwipsToPixelsX(mReFormWidth - 100)
        CurrFormWidth = mReFormWidth
    End Sub

    Private Sub chkExpand_Click(sender As Object, e As EventArgs) Handles chkExpand.Click
        Try
            Dim mCode As String
            Dim mhaschildren As String

Reset:
            With SprdView
                For mRow = 1 To .MaxRows
                    'If .MaxRows < 100 Then
                    .Row = mRow
                    .Col = ColCode
                    mCode = Trim(.Text)
                    .Col = ColCategory
                    If .Text = "H" Or .Text = "G" Then
                        .Col = ColPicText
                        If .Text = "D" Then
                            mhaschildren = ChildFound(mCode, "")
                            If mhaschildren = True Then
                                Call SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(ColPic, mRow))
                                GoTo Reset
                            End If

                        End If
                    End If
                    'End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetLedgerStockBalance(ByRef pAccountCode As String, ByRef pFyear As Long, mCompanyCodeStr As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsOP As ADODB.Recordset = Nothing
        Dim pCompanyCode As Integer
        Dim mConditionTrue As Boolean
        Dim mIsGroupLimit As String = "N"
        Dim mGroupCode As Long

        GetLedgerStockBalance = 0
        mConditionTrue = False


        SqlStr = " SELECT " & vbCrLf _
            & " SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT)AS OPENING "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
            & " WHERE TRN.FYEAR=" & pFyear & ""


        If mCompanyCodeStr <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'"



        'If pVDate <> "" Then
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<=TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOP.EOF = False Then
            GetLedgerStockBalance = Val(IIf(IsDBNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
