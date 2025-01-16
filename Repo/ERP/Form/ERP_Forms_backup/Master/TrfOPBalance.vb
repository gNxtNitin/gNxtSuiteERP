Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTrfOpBalance
	Inherits System.Windows.Forms.Form
	'Dim PvtDBCN As ADODB.Connection
	
	Dim mLastFYDateFrom As String
	Dim mLastFYDateTo As String
    Dim mLastFYNo As Integer

    Dim mCurrFYDateFrom As String
	Dim mCurrFYDateTo As String
	Dim mCurrFYNo As Integer

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click

        Me.Hide()
        Me.Close()
        Me.Dispose()
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
        'TxtDisplayTransfer(0).Width = VB6.TwipsToPixelsX(5085)
        'TxtDisplayTransfer(1).Width = VB6.TwipsToPixelsX(5025)
        'TxtDisplayTransfer(0).Height = VB6.TwipsToPixelsY(2835)
        'TxtDisplayTransfer(1).Height = VB6.TwipsToPixelsY(1725)
        'TxtDisplayTransfer(0).Top = VB6.TwipsToPixelsY(1710)
        'TxtDisplayTransfer(1).Top = VB6.TwipsToPixelsY(2790)
        'TxtDisplayTransfer(0).Left = 0
        'TxtDisplayTransfer(1).Left = VB6.TwipsToPixelsX(30)
        TxtDisplayTransfer(0).Visible = True
		TxtDisplayTransfer(1).Visible = True
		TxtDisplayTransfer(0).Text = ""
		TxtDisplayTransfer(1).Text = ""
	End Sub
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		On Error GoTo SearchErr
        Dim SqlStr As String
        Dim mCompanyCode As Long
        If MainClass.ValidateWithMasterTable(cboUnit.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mCompanyCode = MasterNo
        End If

        SqlStr = ""
        If MainClass.SearchGridMaster(txtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & mCompanyCode & "") = True Then
            txtName.Text = AcName
            txtName.Focus()
        End If
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

        If mLastFYNo = 2021 Then
            FieldVarification = False
            Exit Function
        End If

        If mLastFYNo + 1 <> mCurrFYNo Then
            MsgBox("Invalid FYearFrom & FYearTo ....")
            Exit Function
        End If
        If OptParticularAccount.Checked = True Then
            Dim mCompanyCode As Long
            If MainClass.ValidateWithMasterTable(cboUnit.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mCompanyCode = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = False Then
                MsgInformation("Account Name Does Not Exist In Master.")
                Exit Function
            End If
        End If


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
        Dim mCompanyCode As Long

        If FieldVarification() = False Then Exit Sub

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If MainClass.ValidateWithMasterTable(cboUnit.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mCompanyCode = MasterNo
        Else
            MsgInformation("Please select valid Company Name.")
            Exit Sub
        End If

        mLastFYDateFrom = Mid(CboFYearFrom.Text, 8, 12)
        mLastFYDateTo = Mid(CboFYearFrom.Text, 21, 28)
        mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))

        mCurrFYDateFrom = Mid(CboFYearTo.Text, 8, 12)
        mCurrFYDateTo = Mid(CboFYearTo.Text, 21, 28)
        mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))

        MakeTxtDisplayTransferVisible()
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Opening Balance From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Please Wait........")
        TopDisplayTransfer(New String("=", 37))

        If OptParticularAccount.Checked = True Then
            If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Account Name Does Not Exist In Master.")
                Exit Sub
            End If


            SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf _
                & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                & " AND FYEAR=" & mCurrFYNo & "" & vbCrLf _
                & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
                & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'" & vbCrLf _
                & " AND ACCOUNTCODE='" & mAccountCode & "'"

            PubDBCn.Execute(SqlStr)

            ''If carry To not Equal to From Name
            If MainClass.ValidateWithMasterTable(mAccountCode, "FROM_ACCOUNT", "TO_ACCOUNT", "GEN_CARRYFORWARD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                mToAccountCode = MasterNo

                SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                    & " AND FYEAR=" & mCurrFYNo & "" & vbCrLf _
                    & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
                    & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'" & vbCrLf _
                    & " AND ACCOUNTCODE='" & mToAccountCode & "'"
                PubDBCn.Execute(SqlStr)
            End If
        ElseIf OptAllAccount.Checked = True Then
            SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf _
                & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                & " AND FYEAR=" & mCurrFYNo & "" & vbCrLf _
                & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
                & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'"
            PubDBCn.Execute(SqlStr)
        Else
            SqlStr = " DELETE From FIN_POSTED_TRN TRN WHERE " & vbCrLf _
               & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
               & " AND FYEAR=" & mCurrFYNo & "" & vbCrLf _
               & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
               & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'"

            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE IN "

            SqlStr = SqlStr & vbCrLf _
                & " (SELECT DISTINCT ACCOUNTCODE " & vbCrLf _
                & " FROM FIN_POSTED_TRN" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                & " AND FYEAR IN (" & mCurrFYNo - 1 & "," & mCurrFYNo & ")" & vbCrLf _
                & " GROUP BY ACCOUNTCODE" & vbCrLf _
                & " HAVING NVL(SUM(DECODE(DC,'D',1,-1)*Amount*DECODE(FYEAR," & mCurrFYNo - 1 & " ,1,0)),0) " & vbCrLf _
                & " <>" & vbCrLf _
                & " NVL(SUM(DECODE(DC,'D',1,-1)*Amount*DECODE(FYEAR," & mCurrFYNo & ",1,0)*DECODE(BOOKTYPE,'O',1,0)),0))"

            PubDBCn.Execute(SqlStr)

        End If

        SqlStr = "SELECT ACM.SUPP_CUST_TYPE AS CATEGORY, HEADTYPE, " & vbCrLf _
            & " ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf _
            & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE" & vbCrLf _
            & " AND ACMGROUP.GROUP_TYPE<>'E' " & vbCrLf _
            & " AND ACM.COMPANY_CODE=" & mCompanyCode & ""

        If OptParticularAccount.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_CODE='" & mAccountCode & "' "
        ElseIf optMismatch.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_CODE IN "

            SqlStr = SqlStr & vbCrLf _
                & " (SELECT DISTINCT ACCOUNTCODE " & vbCrLf _
                & " FROM FIN_POSTED_TRN" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                & " AND FYEAR IN (" & mCurrFYNo - 1 & "," & mCurrFYNo & ")" & vbCrLf _
                & " GROUP BY ACCOUNTCODE" & vbCrLf _
                & " HAVING NVL(SUM(DECODE(DC,'D',1,-1)*Amount*DECODE(FYEAR," & mCurrFYNo - 1 & ",1,0)),0) " & vbCrLf _
                & " <>" & vbCrLf _
                & " NVL(SUM(DECODE(DC,'D',1,-1)*Amount*DECODE(FYEAR," & mCurrFYNo & ",1,0)*DECODE(BOOKTYPE,'O',1,0)),0))"

        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_NAME "


        If TransferBalance(SqlStr, mCompanyCode) = False Then GoTo ERR1

        '    If ChkTrading.Value = vbChecked Then
        '        If UpdatePnL = False Then GoTo Err1
        '    End If

        PubDBCn.CommitTrans()

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
        PubDBCn.RollbackTrans()
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
    Private Function TransferBalance(ByRef RsSqlStr As String, ByRef mCompanyCode As Long) As Boolean

        On Error GoTo UpdateErr
        Dim RsOPBal As ADODB.Recordset
        Dim RsOPBalSumm As ADODB.Recordset
        Dim mAccountCode As String
        Dim mAccountName As String
        Dim mFYDateTo As Date
        Dim SqlStr As String

        Dim pLastFYNo As Integer
        Dim pCurrFyNo As Integer
        Dim mBalanceAmount As Double
        Dim pCurrFYDateFrom As String

        Dim mTRNType As String
        Dim cntRow As Integer

        Dim mMKey As String
        Dim mCostCCode As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mVNo As String
        Dim mVDate As String
        Dim mAmount As Double
        Dim mDC As String
        Dim mDueDate As String
        Dim mVType As String
        Dim mBillType As String
        Dim mHeadType As String
        Dim mDivisionCode As Double
        Dim i As Integer
        Dim mLocationId As String

        pLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))
        pCurrFyNo = CInt(VB.Left(CboFYearTo.Text, 4))
        pCurrFYDateFrom = Mid(CboFYearTo.Text, 8, 12)

        TransferBalance = False

        mFYDateTo = CDate(Mid(CboFYearFrom.Text, 21, 28))

        MainClass.UOpenRecordSet(RsSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBal, ADODB.LockTypeEnum.adLockReadOnly)

        Do While Not RsOPBal.EOF = True
            If RsOPBal.EOF = False Then
                mAccountCode = IIf(IsDBNull(RsOPBal.Fields("SUPP_CUST_CODE").Value), -1, RsOPBal.Fields("SUPP_CUST_CODE").Value)
                mAccountName = IIf(IsDBNull(RsOPBal.Fields("SUPP_CUST_NAME").Value), "", RsOPBal.Fields("SUPP_CUST_NAME").Value)
                mHeadType = IIf(IsDBNull(RsOPBal.Fields("HEADTYPE").Value), "", RsOPBal.Fields("HEADTYPE").Value)
                BottomDisplayTransfer(mAccountCode & " - " & mAccountName)

                If mAccountCode <> "-1" Then
                    If GetAccountBalancingMethod(mAccountName,, mCompanyCode) = "S" Then

                        SqlStr = " Select  ACCOUNTCODE,DIV_CODE, LOCATION_ID," & vbCrLf _
                            & " MAX(DUEDATE) AS DUEDATE, " & vbCrLf _
                            & " SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf _
                            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
                            & " WHERE TRN.Company_Code=" & mCompanyCode & "" & vbCrLf _
                            & " AND TRN.FYEAR=" & mLastFYNo & " " & vbCrLf _
                            & " AND ACCOUNTCODE='" & mAccountCode & "' "

                        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'F'"

                        SqlStr = SqlStr & vbCrLf _
                            & " HAVING SUM(DECODE(DC,'D',1,-1)*Amount) <>0 " & vbCrLf _
                            & " GROUP BY " & vbCrLf & " ACCOUNTCODE,DIV_CODE,LOCATION_ID "

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBalSumm, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsOPBalSumm.EOF = False Then
                            i = 0
                            Do While RsOPBalSumm.EOF = False
                                i = i + 1
                                mBalanceAmount = IIf(IsDBNull(RsOPBalSumm.Fields("BALANCE").Value), 0, RsOPBalSumm.Fields("BALANCE").Value)
                                mDivisionCode = IIf(IsDBNull(RsOPBalSumm.Fields("DIV_CODE").Value), 1, RsOPBalSumm.Fields("DIV_CODE").Value)
                                mBillNo = "OP" & mDivisionCode
                                mBillDate = mLastFYDateTo
                                mDC = IIf(mBalanceAmount >= 0, "D", "C")
                                mTRNType = "B"
                                mCostCCode = "-1"
                                mBillType = "B"

                                mVType = "OO"
                                mMKey = mAccountCode
                                mVNo = "OP"
                                mVDate = mLastFYDateTo
                                mLocationId = IIf(IsDBNull(RsOPBalSumm.Fields("LOCATION_ID").Value), 1, RsOPBalSumm.Fields("LOCATION_ID").Value)

                                If mBalanceAmount <> 0 Then
                                    If UpdateTRFTRN(PubDBCn, mMKey, i, i, CStr(ConOpeningBookCode), mVType, VB.Left(ConOpening, 1), VB.Right(ConOpening, 1), mAccountCode, mVNo, mVDate, mBillNo, mBillDate, mBalanceAmount, mDC, mTRNType, "", "", mCostCCode, "-1", "-1", mDueDate, "", mBillType, "", "", "Opening :", "", mBillDate, pCurrFyNo, mDivisionCode, mLocationId, mCompanyCode) = False Then GoTo UpdateErr
                                End If
                                RsOPBalSumm.MoveNext()
                            Loop
                        End If
                    Else
                        If UpdateTableDetailAccount(mAccountCode, mCompanyCode) = False Then GoTo UpdateErr
                    End If
                    '                End If
                End If
            End If
            RsOPBal.MoveNext()
        Loop


        TransferBalance = True
        Exit Function
UpdateErr:
        '    Resume
        MsgInformation(mBillNo)
        If Err.Number = 7 Then
            TxtDisplayTransfer(1).Text = ""
            Resume Next
        End If
        BottomDisplayTransfer("AccountCode..." & mAccountCode & " Transfer Failed...")
        TransferBalance = False
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Function


    Private Function UpdateTRFTRN(ByRef pDBCn As ADODB.Connection, ByRef PKey As String, ByRef pTRNDtlSubRowNo As Integer, ByRef pSubRowNo As Integer, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pAccountCode As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pAmount As Double, ByRef pDC As String, ByRef pTrnType As String, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pDueDate As String,
                                  ByRef pIBRNo As String, ByRef pBillType As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef pRemarks As String, ByRef pExpDate As String, ByRef xCurrFYear As Integer, ByRef mDivisionCode As Double,
                                  ByRef mLocationId As String, ByRef mCompanyCode As Long) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim pDueDays As Double
        Dim mToAccountCode As String
        Dim xBillDate As String
        Dim xDueDays As String
        Dim xCompanyCode As Long


        If mCompanyCode = 0 Then
            xCompanyCode = RsCompany.Fields("Company_Code").Value
        Else
            xCompanyCode = mCompanyCode
        End If

        'OpenLocalConnection()
        ''pDueDate = DateAdd("D", pDueDays, pBillDate)
        If IsDate(pDueDate) Then
            pDueDate = IIf(CDate(pDueDate) < CDate(pBillDate), pBillDate, pDueDate)
            xDueDays = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(pBillDate), CDate(pDueDate)))
        Else
            xDueDays = CStr(0)
        End If

        If MainClass.ValidateWithMasterTable(pAccountCode, "FROM_ACCOUNT", "TO_ACCOUNT", "GEN_CARRYFORWARD_MST", pDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mToAccountCode = MasterNo
        Else
            mToAccountCode = pAccountCode
        End If

        SqlStr = "Insert Into FIN_POSTED_TRN ( " & vbCrLf _
            & " MKey, TRNDtlSubRowNo, SubRowNo, " & vbCrLf _
            & " Company_Code, FYEAR, BookCode, VType, " & vbCrLf _
            & " BookType, BookSubType, AccountCode, " & vbCrLf _
            & " Vno, VDate, BillNo, BillDate, " & vbCrLf _
            & " Amount, DC, " & vbCrLf _
            & " TrnType, ChequeNo, ChqDate, CostCCode, " & vbCrLf _
            & " DeptCode, EmpCode, DueDays, DueDate, " & vbCrLf _
            & " IBRNo, ClearDate, Locked, BILLTYPE, Narration, Remarks, " & vbCrLf _
            & " AddUser , AddDate, ModUser, ModDate,EXPDATE,DIV_CODE,LOCATION_ID ) VALUES ( " & vbCrLf _
            & " '" & PKey & "' , " & pTRNDtlSubRowNo & ", " & pSubRowNo & ", " & vbCrLf _
            & " " & xCompanyCode & ", " & xCurrFYear & ", '" & pBookCode & "', '" & pVType & "', " & vbCrLf _
            & " '" & pBookType & "','" & pBookSubType & "', '" & mToAccountCode & "', " & vbCrLf _
            & " '" & pVNo & "',  TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '" & pBillNo & "',  TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " " & Math.Abs(pAmount) & ", '" & pDC & "', '" & pTrnType & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(pChequeNo) & "', TO_DATE('" & VB6.Format(pChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(pCostCCode) & "', '" & MainClass.AllowSingleQuote(pDeptCode) & "', '" & MainClass.AllowSingleQuote(pEmpCode) & "', " & pDueDays & ", " & vbCrLf _
            & " TO_DATE('" & VB6.Format(xDueDays, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(pIBRNo) & "', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(pClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(pLocked) & "', " & vbCrLf _
            & " '" & pBillType & "', '" & MainClass.AllowSingleQuote(pNarration) & "','" & MainClass.AllowSingleQuote(pRemarks) & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & vbCrLf _
            & " TO_DATE('" & VB6.Format(pExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & ",'" & mLocationId & "')"

        pDBCn.Execute(SqlStr)

        UpdateTRFTRN = True
        Exit Function
ErrPart:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateTRFTRN = False
        ' Resume
    End Function
    Private Sub frmTrfOpBalance_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        'Set PvtDBCN = New ADODB.Connection
        'PvtDBCN.CommandTimeout = 0
        'PvtDBCN.ConnectionTimeout = 0
        'PvtDBCN.Open StrConn

        'TxtDisplayTransfer(0).Visible = False
        'TxtDisplayTransfer(1).Visible = False
        'OptAllAccount.Checked = True
        optMismatch.Checked = True
        'Me.Height = VB6.TwipsToPixelsY(5595)
        'Me.Width = VB6.TwipsToPixelsX(5220)
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
        Dim mRsFYear As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing
        CboFYearFrom.Items.Clear()
        CboFYearTo.Items.Clear()
        SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN  " & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY FYEAR"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsFYear, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsFYear.EOF = False Then
            Do While Not mRsFYear.EOF
                CboFYearFrom.Items.Add(VB6.Format(mRsFYear.Fields("FYEAR").Value, "00") & "  :  " & VB6.Format(mRsFYear.Fields("Start_Date").Value, "DD-MM-YYYY") & "_" & VB6.Format(mRsFYear.Fields("END_DATE").Value, "DD-MM-YYYY"))
                CboFYearTo.Items.Add(VB6.Format(mRsFYear.Fields("FYEAR").Value, "00") & "  :  " & VB6.Format(mRsFYear.Fields("Start_Date").Value, "DD-MM-YYYY") & "_" & VB6.Format(mRsFYear.Fields("END_DATE").Value, "DD-MM-YYYY"))
                mRsFYear.MoveNext()
            Loop
        End If

        cboUnit.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST WHERE STATUS='O' ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboUnit.Items.Add(RS.Fields("COMPANY_NAME").Value)
                RS.MoveNext()
            Loop
        End If

        cboUnit.Text = RsCompany.Fields("COMPANY_NAME").Value

    End Sub

    Private Sub OptAllAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAllAccount.CheckedChanged, optMismatch.CheckedChanged
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
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
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
        Dim mCompanyCode As Long
        If Trim(txtName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(cboUnit.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mCompanyCode = MasterNo
        End If

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & mCompanyCode & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'"

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
    Private Function UpdateTableDetailAccount(ByRef pAccountCode As String, ByRef mCompanyCode As Long) As Boolean

        On Error GoTo UpdateTableDetailAccountErr
        Dim SqlStr As String
        Dim RsBal As ADODB.Recordset
        Dim i As Integer
        Dim mAccountCode As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mTRNType As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mAmount As Double
        Dim mTotBalance As Double
        Dim mFYDateTo As Date
        Dim ProcStat As Short
        Dim mCostCCode As String
        Dim mDC As String
        Dim mDueDate As String
        Dim mBillType As String
        Dim mVType As String
        Dim mVNo As String
        Dim mVDate As String
        Dim mExpDate As String

        Dim mDAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mCAmtStr As String
        Dim mBalAmtStr As String
        Dim mDivisionCode As Double
        Dim mLocationID As String

        mDAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mCAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mBalAmtStr = "" & mDAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " + " & mCNAmtStr & " + " & mCAmtStr & ""

        SqlStr = " Select  ACCOUNTCODE, DIV_CODE,LOCATION_ID," & vbCrLf _
            & " BILLNO, BILLDATE, MAX(DUEDATE) AS DUEDATE, " & vbCrLf _
            & " " & mBalAmtStr & " AS BALANCE " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN "

        If mCompanyCode = 0 Then
            SqlStr = SqlStr & vbCrLf & " WHERE TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " WHERE TRN.Company_Code=" & mCompanyCode & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.FYEAR=" & mLastFYNo & " " & vbCrLf _
            & " AND ACCOUNTCODE='" & pAccountCode & "' "

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'F'"

        SqlStr = SqlStr & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf _
            & " GROUP BY " & vbCrLf _
            & " ACCOUNTCODE, BILLNO, BILLDATE,DIV_CODE,LOCATION_ID " & vbCrLf _
            & " ORDER BY " & vbCrLf _
            & " BILLNO, BILLDATE "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBal, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBal.EOF = False Then
            Do While Not RsBal.EOF = True
                i = i + 1
                mAccountCode = IIf(IsDBNull(RsBal.Fields("ACCOUNTCODE").Value), "-1", RsBal.Fields("ACCOUNTCODE").Value)
                mDivisionCode = IIf(IsDBNull(RsBal.Fields("DIV_CODE").Value), 1, RsBal.Fields("DIV_CODE").Value)
                mVType = "OO"
                mVNo = "OP"
                mVDate = CStr(CDate(mLastFYDateTo)) ''RsCompany.Fields("START_DATE").Value - 1


                mBookType = "O"
                mBookSubType = "O"
                mTRNType = "B"
                mBillNo = IIf(IsDBNull(RsBal.Fields("BILLNO").Value), "", RsBal.Fields("BILLNO").Value)
                '            If mCurrFYNo > 2006 Then
                '                mBillNo = "OP" & mCurrFYNo & "-" & mBillNo
                '            End If
                mBillDate = IIf(IsDBNull(RsBal.Fields("BillDate").Value), "", RsBal.Fields("BillDate").Value)

                '            If mBillNo = "ON ACCOUNT-1035" Then MsgBox "OK"
                mExpDate = GetMRRDate(mCompanyCode, mAccountCode, mBillNo, mBillDate) '' Sandeep 22-05-2012 '' mVDate ''Exp Date same Vdate for Transfer Time....  Sandeep 22-09-2011
                mExpDate = IIf(mExpDate = "", mBillDate, mExpDate)

                mAmount = IIf(IsDBNull(RsBal.Fields("BALANCE").Value), 0, RsBal.Fields("BALANCE").Value)
                mCostCCode = CStr(-1)
                mDC = IIf(mAmount >= 0, "D", "C")
                mDueDate = IIf(IsDBNull(RsBal.Fields("DUEDATE").Value), "", RsBal.Fields("DUEDATE").Value)
                mBillType = "B"

                mLocationID = IIf(IsDBNull(RsBal.Fields("LOCATION_ID").Value), "", RsBal.Fields("LOCATION_ID").Value)
                If mAmount <> 0 And mAccountCode <> "-1" Then
                    If UpdateTRFTRN(PubDBCn, mAccountCode, i, i, CStr(ConOpeningBookCode), mVType, VB.Left(ConOpening, 1), VB.Right(ConOpening, 1), mAccountCode, mVNo, mVDate, mBillNo, mBillDate, System.Math.Abs(mAmount), mDC, mTRNType, "", "", mCostCCode, "-1", "-1", mDueDate, "", mBillType, "", "", "Opening :", "", mExpDate, mCurrFYNo, mDivisionCode, mLocationID, mCompanyCode) = False Then GoTo UpdateTableDetailAccountErr
                End If

                RsBal.MoveNext()
            Loop
        End If
        UpdateTableDetailAccount = True
        Exit Function
UpdateTableDetailAccountErr:
        MsgInformation("Bill No : " & mBillNo & " Bill Date : " & mBillDate & " Amount : " & mAmount)
        UpdateTableDetailAccount = False
    End Function
    Private Function GetMRRDate(ByRef mCompanyCode As String, ByRef mAccountCode As String, ByRef mBillNo As String, ByRef mBillDate As String) As String

        On Error GoTo UpdateTableDetailAccountErr
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetMRRDate = ""

        SqlStr = " Select  MRRDATE " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & mCompanyCode & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' " & vbCrLf _
            & " AND BILLNO='" & mBillNo & "' " & vbCrLf _
            & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetMRRDate = IIf(IsDBNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value)
        Else
            SqlStr = " Select  INVOICE_DATE " & vbCrLf _
                & " FROM FIN_INVOICE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & mCompanyCode & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' " & vbCrLf _
                & " AND BILLNO='" & mBillNo & "' " & vbCrLf _
                & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetMRRDate = IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value)
            End If
        End If

        Exit Function
UpdateTableDetailAccountErr:
        GetMRRDate = ""
    End Function
End Class