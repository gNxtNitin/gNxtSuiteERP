Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.PowerPacks
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmBankReco
    Inherits System.Windows.Forms.Form
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    '''Private PvtDBCn As ADODB.Connection

    Private Const RowHeight As Short = 12
    Private Const ColMark As Short = 1
    Private Const ColTransDate As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColVNo As Short = 4
    Private Const colAccount As Short = 5
    Private Const ColChequeNo As Short = 6
    Private Const ColDAmount As Short = 7
    Private Const ColCAmount As Short = 8
    Private Const ColAmount As Short = 9
    Private Const ColAmountDC As Short = 10

    Private Const ColClearDate As Short = 11
    Private Const ColUnitName As Short = 12
    Private Const ColMKEY As Short = 13
    Private Const ColSubRowNo As Short = 14

    Dim mClickProcess As Boolean

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBankRecon(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBankRecon(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub PrintBankRecon(ByRef mMode As Crystal.DestinationConstants)

        On Error GoTo PrintERR
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mBBBal As String
        Dim mCLBal As String
        Dim mNotClearBal As String
        Dim mNotInBookBal As String
        Dim mBSBal As String

        Call InsertIntoPrintDummy()

        SqlStr = "SELECT * FROM TEMP_PrintDummyData PrintDummyData " & " WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        mTitle = "BANK RECONCILIATION"
        mSubTitle = "From: " & VB6.Format(TxtDateFrom.Text, "DD-MM-YYYY") & " To: " & VB6.Format(TxtDateTo.Text, "DD-MM-YYYY") & " [" & cboBank.Text & "]"
        mBBBal = TxtBankBookBal.Text & "  " & lblBBDrCr.Text
        mCLBal = TxtBankStmtBal.Text & "  " & lblBSDrCr.Text

        mNotClearBal = txtNotClear.Text & "  " & lblNCDRCr.Text
        mNotInBookBal = txtNotInOurBook.Text & "  " & lblNIOBDrCr.Text
        mBSBal = txtBankBalance.Text & "  " & lblBankBDrCr.Text

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "BBBal='" & mBBBal & "'")
        MainClass.AssignCRptFormulas(Report1, "Balance='" & mCLBal & "'")

        MainClass.AssignCRptFormulas(Report1, "NotClear='" & mNotClearBal & "'")
        MainClass.AssignCRptFormulas(Report1, "NotInOurBook='" & mNotInBookBal & "'")
        MainClass.AssignCRptFormulas(Report1, "BSBal='" & mBSBal & "'")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\BankReco.rpt"
        Report1.Action = 1
        Exit Sub
PrintERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub InsertIntoPrintDummy()

        On Error GoTo InsertErr1
        Dim SqlStr As String
        Dim I As Integer
        Dim mVDate As String
        Dim mVNo As String
        Dim mCHQNo As String
        Dim mAccount As String
        Dim mDAmt As String
        Dim mCAmt As String
        Dim mClearDate As String
        Dim mAmt As String
        Dim mDC As String
        Dim mTransDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE userID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For I = 1 To .MaxRows - 2
                .Row = I

                .Col = ColTransDate
                mTransDate = VB.Left(.Text, 255)

                .Col = ColVNo
                mVNo = VB.Left(.Text, 255)

                .Col = colAccount
                mAccount = VB.Left(.Text, 255)

                .Col = ColChequeNo
                mCHQNo = VB.Left(.Text, 255)

                .Col = ColAmount
                mAmt = VB.Left(.Text, 255)

                .Col = ColAmountDC
                mDC = VB.Left(.Text, 255)

                .Col = ColDAmount
                mDAmt = VB.Left(.Text, 255)

                .Col = ColCAmount
                mCAmt = VB.Left(.Text, 255)

                .Col = ColClearDate
                mClearDate = VB.Left(.Text, 255)

                .Col = ColVDate
                mVDate = VB.Left(.Text, 255)

                SqlStr = "INSERT INTO TEMP_PrintDummyData (UserID,SubRow,Field1,Field2,Field3,Field4,Field5, " & vbCrLf _
                    & " Field6,Field7,Field8,Field9,Field10) " & vbCrLf _
                    & " Values ('" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " " & I & ",'" & MainClass.AllowSingleQuote(mTransDate) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mVNo) & "','" & MainClass.AllowSingleQuote(mAccount) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mCHQNo) & "', " & vbCrLf _
                    & " '" & mDAmt & "','" & mCAmt & "','" & MainClass.AllowSingleQuote(mClearDate) & "'," & vbCrLf _
                    & " '" & mAmt & "','" & mDC & "','" & MainClass.AllowSingleQuote(mVDate) & "')"
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
InsertErr1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        Dim SqlStr As String
        Dim mAccountCode As String

        On Error GoTo ERR1
        txtNotClear.Text = "0.00"
        TxtBankBookBal.Text = "0.00"
        TxtBankStmtBal.Text = "0.00"
        txtNotInOurBook.Text = "0.00"
        txtBankBalance.Text = "0.00"

        If FieldsVerification() = False Then Exit Sub
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)
        If MainClass.ValidateWithMasterTable(cboBank.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master", MsgBoxStyle.Information)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If optShow(1).Checked = True Then
            Call GetBankBookBal("O")
            Call GetBankBookBal("C")
            Call GetBankBookBal("B")
        End If

        FormatSprdLedg(-1)
        If ShowForMultiEntry(mAccountCode) = False Then GoTo ERR1
        SprdMain.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default

        CmdPreview.Enabled = True
        cmdPrint.Enabled = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function ShowForMultiEntry(ByRef pAccountCode As String) As Boolean

        On Error GoTo PErr
        Dim mOpening As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim mVType, mConsolidated As String
        Dim RS As New ADODB.Recordset
        Dim mGroupQry As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""

        ShowForMultiEntry = True

        SqlStr = "SELECT DISTINCT ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST ACM, FIN_POSTED_TRN TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND TRN.BOOKTYPE<>'" & ConPDCBook & "'"

        ''AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " 
        ''            & " AND TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.ClearDate<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            SqlStr = SqlStr & " AND (Trn.ClearDate IS NOT NULL OR Trn.ClearDate<>'')"
        ElseIf optShow(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND (TRN.ClearDate>TO_DATE('" & VB6.Format(txtAsOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " OR Trn.ClearDate IS NULL OR Trn.ClearDate='')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & pAccountCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS)

        SprdMain.MaxRows = 0
        Do While RS.EOF = False
            If DisplayForMulti(RS.Fields("SUPP_CUST_CODE").Value, RS.Fields("SUPP_CUST_NAME").Value) = False Then GoTo PErr
            RS.MoveNext()
        Loop
        Call FillGridBot()
        If optShow(1).Checked = True Then
            Call GetBankStmtBal()
        End If

        Exit Function
PErr:
        ShowForMultiEntry = False
        MsgBox(Err.Description)
    End Function
    Function DisplayForMulti(ByVal pAccountCode As String, ByVal pAccountName As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim SqlStr As String
        Dim mOptionalCond As String
        Dim RS As ADODB.Recordset
        Dim JJ As Integer
        Dim mOPBal As Double
        Dim mDateField As String

        SqlStr1 = MakeSQLForMultiEntry(pAccountCode, pAccountName)
        SqlStr2 = MakeSQLCond()
        SqlStr = SqlStr1 & vbCrLf & SqlStr2

        If optVDate.Checked = True Then
            mDateField = "A.VDATE"
        Else
            mDateField = "A.ClearDate"
        End If

        If optShow(0).Checked = True Then
            'SqlStr = SqlStr & vbCrLf _
            '& " AND TRN.BOOKTYPE||TRN.MKEY IN (SELECT TRN.BOOKTYPE||TRN.MKEY " & SqlStr2 & vbCrLf _
            '& " AND ACCOUNTCODE='" & pAccountCode & "')"

            SqlStr = SqlStr & vbCrLf _
               & " AND TRN.BOOKTYPE||TRN.MKEY IN (SELECT A.BOOKTYPE||A.MKEY  FROM FIN_POSTED_TRN A  WHERE " & vbCrLf _
               & " A.COMPANY_CODE = TRN.COMPANY_CODE " & vbCrLf _
               & " AND A.BOOKTYPE<>'" & ConPDCBook & "' AND (BOOKCODE='" & pAccountCode & "' OR  ACCOUNTCODE='" & pAccountCode & "')"

            ''AND A.FYEAR=" & RsCompany.Fields("FYEAR").Value & "  

            ''" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf _ Sandeep

            SqlStr = SqlStr & vbCrLf & " AND " & mDateField & ">=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            SqlStr = SqlStr & vbCrLf & " AND " & mDateField & "<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "


        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND TRN.BOOKTYPE||TRN.MKEY IN (SELECT A.BOOKTYPE||A.MKEY  FROM FIN_POSTED_TRN A  WHERE " & vbCrLf _
                & " A.COMPANY_CODE = TRN.COMPANY_CODE" & vbCrLf _
                & " AND A.BOOKTYPE<>'" & ConPDCBook & "' AND (BOOKCODE='" & pAccountCode & "' OR  ACCOUNTCODE='" & pAccountCode & "')"

            ''AND A.FYEAR=" & RsCompany.Fields("FYEAR").Value & "  
            ''SANDEEp

            If optShow(1).Checked = True Then
                SqlStr = SqlStr & " AND (A.ClearDate IS NOT NULL OR A.ClearDate<>'')"
                SqlStr = SqlStr & vbCrLf & " AND A.ClearDate>=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                SqlStr = SqlStr & vbCrLf & " AND A.ClearDate<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
            ElseIf optShow(3).Checked = True Then
                SqlStr = SqlStr & vbCrLf _
                    & " AND (A.ClearDate>TO_DATE('" & VB6.Format(txtAsOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " OR A.ClearDate IS NULL OR A.ClearDate=''))"
            Else
                SqlStr = SqlStr & " AND (A.ClearDate IS NULL OR A.ClearDate='') "
                SqlStr = SqlStr & vbCrLf & " AND " & mDateField & ">=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                SqlStr = SqlStr & vbCrLf & " AND " & mDateField & "<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
            End If


            'If optShow(1).Checked = True Then
            '    SqlStr = SqlStr & " AND (Trn.ClearDate IS NOT NULL OR Trn.ClearDate<>'')"
            'Else
            '    SqlStr = SqlStr & " AND (Trn.ClearDate IS NULL OR Trn.ClearDate='') "
            'End If
        End If
        SqlStr3 = MakeGroupBYForMultiEntry(pAccountCode)
        SqlStr = SqlStr & vbCrLf & SqlStr3
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS)

        With SprdMain
            Do While RS.EOF = False
                .MaxRows = .MaxRows + 1
                .Row = .MaxRows
                For JJ = 1 To RS.Fields.Count
                    .Col = JJ
                    .Text = IIf(IsDBNull(RS.Fields(JJ - 1).Value), "", RS.Fields(JJ - 1).Value)
                Next JJ
                RS.MoveNext()
            Loop
        End With
        DisplayForMulti = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        DisplayForMulti = False
        'Resume
    End Function

    Private Function MakeSQLForMultiEntry(ByRef pAccountCode As String, ByRef pAccountName As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mField As String

        If optShow(1).Checked = True Then
            mField = "TRN.CLEARDATE"
        Else
            mField = "TRN.VDATE"
        End If

        SqlStr = " SELECT ' ' AS MARK,"

        If optShow(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(" & mField & ",'DD/MM/YYYY') AS TRANS_DATE , "

            SqlStr = SqlStr & vbCrLf & " TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS V_DATE , TRN.VNO||TRN.BOOKSUBTYPE  AS V_NO ," & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "', ' ',ACM.SUPP_CUST_NAME) AS ACCTNAME," & vbCrLf _
                    & " CASE WHEN TRN.CHEQUENO='0' OR  CHEQUENO IS NULL THEN '' ELSE TRN.CHEQUENO END AS ChequeNo, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(SUM(DECODE(DC,'D',TRN.Amount, 0 )),'9,99,99,99,999.99'),'') AS DAmount, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(SUM(DECODE(DC,'C',TRN.Amount, 0 )),'9,99,99,99,999.99'),'') AS CAmount, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',TO_CHAR(ABS(SUM(TRN.AMOUNT*DECODE(DC,'D',1,-1))),'9,99,99,99,999.99')) AS Amount, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',DECODE(DC,'D', 'Dr','Cr')) AS AmountDC, " & vbCrLf _
                    & " Trn.ClearDate, COMP.COMPANY_SHORTNAME," & vbCrLf _
                    & " TRN.MKEY AS Mkey,"

            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(TRN.TRNDTLSUBROWNO,'999999999') AS SubRowNo "

        Else
            SqlStr = SqlStr & vbCrLf & "(CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " THEN '' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(" & mField & ",'DD/MM/YYYY'),'') END) AS TRANS_DATE , "

            SqlStr = SqlStr & vbCrLf & "(CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " THEN '' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),'') END) AS V_DATE , "


            SqlStr = SqlStr & vbCrLf & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " THEN '' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TRN.VNO||TRN.BOOKSUBTYPE,'') END)  AS V_NO ," & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " THEN ' OPENING BALANCE' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "'," & vbCrLf _
                    & " ' ',ACM.SUPP_CUST_NAME) END) AS ACCTNAME," & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " THEN '' ELSE CASE WHEN TRN.CHEQUENO='0' OR  CHEQUENO IS NULL THEN '' ELSE TRN.CHEQUENO END END) AS ChequeNo, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(SUM(DECODE(DC,'D',TRN.Amount, 0 )),'9,99,99,99,999.99'),'') AS DAmount, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(SUM(DECODE(DC,'C',TRN.Amount, 0 )),'9,99,99,99,999.99'),'') AS CAmount, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',TO_CHAR(ABS(SUM((CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 0 ELSE TRN.AMOUNT*DECODE(DC,'D',1,-1 ) END ))),'9,99,99,99,999.99')) AS Amount, " & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',DECODE(DC,'D', 'Dr','Cr')) END) AS AmountDC, " & vbCrLf _
                    & " Trn.ClearDate, COMPANY_SHORTNAME," & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE TRN.MKEY END)  AS Mkey,"

            SqlStr = SqlStr & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE TO_CHAR(TRN.TRNDTLSUBROWNO,'999999999') END) AS SubRowNo "

        End If


        MakeSQLForMultiEntry = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLForMultiEntry = ""
    End Function
    Private Function MakeSQLCond() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mVType, mOptionalCond As String
        Dim mField As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""

        If optShow(1).Checked = True Then
            mField = "TRN.CLEARDATE"
        Else
            mField = "TRN.VDATE"
        End If

        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM , GEN_COMPANY_MST COMP"
        SqlStr = SqlStr _
            & " WHERE COMP.COMPANY_CODE=TRN.COMPANY_CODE " & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " And TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "


        '& vbCrLf _
        '    & " And TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        'TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '    & " And 

        ''TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.BOOKTYPE<>'" & ConPDCBook & "' "

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND " & mField & ">=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " AND " & mField & "<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & " AND (Trn.ClearDate IS NOT NULL OR Trn.ClearDate<>'')"
        End If

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND TRN.Vdate>='" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND TRN.Vdate<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"

        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
    End Function

    Private Function MakeGroupBYForMultiEntry(ByRef pAccountCode As String) As String
        Dim mStr As String
        Dim mField As String

        If optShow(1).Checked = True Then
            mField = "TRN.CLEARDATE"
        Else
            mField = "TRN.VDATE"
        End If

        mStr = ""
        If optShow(3).Checked = True Then

            mStr = " GROUP BY " & vbCrLf _
                    & " " & mField & ",TRN.VDATE,TRN.VNO||TRN.BOOKSUBTYPE, " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',' ',ACM.SUPP_CUST_NAME) , " & vbCrLf _
                    & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',DECODE(DC,'D', 'Dr','Cr')), " & vbCrLf _
                    & " ACM.SUPP_CUST_NAME,ACM.SUPP_CUST_CODE,Trn.ClearDate, " & vbCrLf _
                    & " CASE WHEN TRN.CHEQUENO='0' OR  CHEQUENO IS NULL THEN '' ELSE TRN.CHEQUENO END, "

            mStr = mStr & "TO_CHAR(TRN.TRNDTLSUBROWNO,'999999999')," & vbCrLf _
                    & " COMP.COMPANY_SHORTNAME,TRN.MKEY  "


            'mStr = " GROUP BY " & vbCrLf _
            '    & " TO_CHAR(" & mField & ",'DD/MM/YYYY') AS V_DATE , "

            'mStr = mStr & vbCrLf & " TRN.VNO||TRN.BOOKSUBTYPE  AS V_NO ," & vbCrLf _
            '        & "  DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "', ' {AS PER DETAIL}',ACM.SUPP_CUST_NAME) AS ACCTNAME," & vbCrLf _
            '        & "  CASE WHEN TRN.CHEQUENO='0' OR  CHEQUENO IS NULL THEN '' ELSE TRN.CHEQUENO END AS ChequeNo, " & vbCrLf _
            '        & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(SUM(DECODE(DC,'D',TRN.Amount, 0 )),'9,99,99,99,999.99'),'') AS DAmount, " & vbCrLf _
            '        & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',TO_CHAR(SUM(DECODE(DC,'C',TRN.Amount, 0 )),'9,99,99,99,999.99'),'') AS CAmount, " & vbCrLf _
            '        & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',TO_CHAR(ABS(SUM(TRN.AMOUNT*DECODE(DC,'D',1,-1)))),'9,99,99,99,999.99') AS Amount, " & vbCrLf _
            '        & " DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',DECODE(DC,'D', 'Dr','Cr')) AS AmountDC, " & vbCrLf _
            '        & " Trn.ClearDate, " & vbCrLf _
            '        & " TRN.MKEY AS Mkey,"

            'mStr = mStr & vbCrLf _
            '        & " TO_CHAR(TRN.TRNDTLSUBROWNO,'999999999') AS SubRowNo "
        Else
            mStr = " GROUP BY " & vbCrLf _
                    & " " & mField & ",TRN.VDATE,TRN.VNO||TRN.BOOKSUBTYPE, " & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' OPENING BALANCE' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "',ACM.SUPP_CUST_NAME) END) , " & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE DECODE(ACM.SUPP_CUST_CODE,'" & pAccountCode & "','',DECODE(DC,'D', 'Dr','Cr')) END) , " & vbCrLf _
                    & " ACM.SUPP_CUST_NAME,ACM.SUPP_CUST_CODE,Trn.ClearDate, COMP.COMPANY_SHORTNAME," & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE CASE WHEN TRN.CHEQUENO='0' OR  CHEQUENO IS NULL THEN '' ELSE TRN.CHEQUENO END END), "

            mStr = mStr & "(CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE TO_CHAR(TRN.TRNDTLSUBROWNO,'999999999') END) ," & vbCrLf _
                    & " (CASE WHEN " & mField & "<TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE TRN.MKEY END)  "

            '    mStr = mStr & " TRN.MKEY "
        End If

        mStr = mStr & " ORDER BY " & mField & ",TRN.VNO||TRN.BOOKSUBTYPE,ACCTNAME"
        MakeGroupBYForMultiEntry = mStr
    End Function
    Private Sub FormatSprdLedg(ByRef Arow As Integer)

        With SprdMain
            .MaxCols = ColSubRowNo
            .set_RowHeight(0, RowHeight)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = Arow


            .Col = ColMark

            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 1
            .TypeEditMultiLine = False
            .set_ColWidth(ColMark, 3)
            .Row = 0
            .Text = "+/-"
            .Row = Arow

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 10)
            .Row = 0
            .Text = "VDate"
            .Row = Arow

            .Col = ColTransDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTransDate, 10)
            .Row = 0
            .Text = "Transaction Date"
            .Row = Arow

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 12)
            .Row = 0
            .Text = "V.No."

            .Col = colAccount
            .set_ColWidth(colAccount, 30)

            .Row = 0
            .Text = "Account Head"

            .Row = Arow


            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 9)
            .ColHidden = False

            .Row = 0
            .Text = "Amount"

            .Row = Arow
            .Col = ColAmountDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAmountDC, 3)
            .ColHidden = False

            .Row = 0
            .Text = "D/C"

            .Row = Arow


            .Row = Arow

            .Col = ColChequeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChequeNo, 6)
            .Row = 0
            .Text = "Chq.No"

            .Row = Arow

            .Col = ColDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDAmount, 10.5)
            .Row = 0
            .Text = "Debit Amount"
            .Row = Arow

            .Col = ColCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCAmount, 10.5)
            .Row = 0
            .Text = "Credit Amount"
            .Row = Arow


            .Col = ColClearDate
            .set_ColWidth(ColClearDate, 9)
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .Row = 0
            .Text = "Clear Date"

            .Col = ColUnitName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnitName, 8)
            .Row = 0
            .Text = "Company Name"

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColSubRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSubRowNo, 5)
            .ColHidden = True

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            'SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            'MainClass.CellColor(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        End With
    End Sub

    Private Sub FillGridBot()
        Dim I As Integer
        Dim mTotDAmount As Double
        Dim mTotCAmount As Double
        Dim mGridBalance As Double

        Dim mTotRecDAmount As Double
        Dim mTotRecCAmount As Double
        Dim mTotBalRecmount As Double
        Dim mClearDate As String

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColClearDate
                mClearDate = Trim(IIf(IsDate(.Text), .Text, ""))
                .Col = ColDAmount
                mTotDAmount = mTotDAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                If IsDate(mClearDate) Then
                    mTotRecDAmount = mTotRecDAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                End If

                .Col = ColCAmount
                mTotCAmount = mTotCAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                If IsDate(mClearDate) Then
                    mTotRecCAmount = mTotRecCAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                End If
            Next
            mGridBalance = mTotDAmount - mTotCAmount
            mTotBalRecmount = mTotRecDAmount - mTotRecCAmount

            .MaxRows = .MaxRows + 2
            .Row = .MaxRows
            .Action = SS_ACTION_INSERT_ROW
            .set_RowHeight(.MaxRows, 20)

            FormatSprdLedg(-1)

            For I = .MaxRows - 1 To .MaxRows
                .Row = I
                If I = .MaxRows Then GoTo LastRow

                .Col = colAccount
                .Font = VB6.FontChangeBold(.Font, True)
                .Text = "Total : "

                .Col = ColDAmount
                .Font = VB6.FontChangeBold(.Font, True)
                .Text = CStr(mTotDAmount)
                .Text = VB6.Format(.Text, "0.00")

                .Col = ColCAmount
                .Font = VB6.FontChangeBold(.Font, True)
                .Text = CStr(mTotCAmount)
                .Text = VB6.Format(.Text, "0.00")

            Next
LastRow:
            .Col = colAccount
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "Balance : "

            If mGridBalance > 0 Then
                .Col = ColDAmount
                .Font = VB6.FontChangeBold(.Font, True)
                .Text = CStr(mGridBalance)
                .Text = VB6.Format(.Text, "0.00")
            Else
                .Col = ColCAmount
                .Font = VB6.FontChangeBold(.Font, True)
                .Text = CStr(System.Math.Abs(mGridBalance))
                .Text = VB6.Format(.Text, "0.00")
            End If
        End With
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(TxtDateFrom) = False Then
            MsgInformation("Invalid From Date")
            GoTo ERR1
        End If
        If MainClass.ChkIsdateF(TxtDateTo) = False Then
            MsgInformation("Invalid To Date")
            GoTo ERR1
        End If

        If Not IsDate(txtAsOnDate.Text) Then
            MsgInformation("Invalid To Date")
            GoTo ERR1
        End If

        If cboBank.SelectedIndex = -1 Then
            MsgInformation("No Bank is Selected Please select it")
            GoTo ERR1
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub frmBankReco_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
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

        TxtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        TxtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtAsOnDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        optShow(1).Checked = True
        'Frame2.Enabled = False
        Call FillBankCombo()
        'Call frmBankReco_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub GetBankBookBal(ByRef pType As String)
        Dim mBalance As Double
        Dim mAccountCode As String
        Dim mDate As String
        Dim CntLst As Long

        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            'SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If pType = "O" Then
            mDate = DateAdd("d", -1, TxtDateFrom.Text)
            If MainClass.ValidateWithMasterTable((cboBank.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '        mBalance = AccountBal(PubDBCn, RsCompany, cboBank.Text, txtDateTo.Text)
                mAccountCode = MasterNo


                mBalance = GetOpeningBal(mAccountCode, mDate,,, "R", "Y", mCompanyCodeStr)  '' GetOpeningBalClearDate(mAccountCode, mDate)
                If mBalance > 0 Then
                    lblBBDrCr.Text = "Dr"
                Else
                    lblBBDrCr.Text = "Cr"
                End If
                TxtBankBookBal.Text = CStr(System.Math.Abs(mBalance))
                TxtBankBookBal.Text = VB6.Format(TxtBankBookBal.Text, "0.00")
            Else
                MsgBox("Bank Does Not Exist In Master")
                TxtBankBookBal.Text = CStr(0.0#)
            End If
        ElseIf pType = "C" Then
            mDate = TxtDateTo.Text
            If MainClass.ValidateWithMasterTable((cboBank.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '        mBalance = AccountBal(PubDBCn, RsCompany, cboBank.Text, txtDateTo.Text)
                mAccountCode = MasterNo
                mBalance = GetOpeningBal(mAccountCode, mDate,,, "R", "Y", mCompanyCodeStr)  '' GetOpeningBalClearDate(mAccountCode, mDate)
                If mBalance > 0 Then
                    lblBSDrCr.Text = "Dr"
                Else
                    lblBSDrCr.Text = "Cr"
                End If
                TxtBankStmtBal.Text = CStr(System.Math.Abs(mBalance))
                TxtBankStmtBal.Text = VB6.Format(TxtBankStmtBal.Text, "0.00")
            Else
                MsgBox("Bank Does Not Exist In Master")
                TxtBankStmtBal.Text = CStr(0.0#)
            End If
        ElseIf pType = "B" Then
            mDate = TxtDateTo.Text
            If MainClass.ValidateWithMasterTable((cboBank.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '        mBalance = AccountBal(PubDBCn, RsCompany, cboBank.Text, txtDateTo.Text)
                mAccountCode = MasterNo
                mBalance = GetOpeningBal(mAccountCode, mDate,,, "R", "Y", mCompanyCodeStr) - GetOpeningBalClearDate(mAccountCode, mDate, "", 0, "Y", mCompanyCodeStr)
                If mBalance > 0 Then
                    lblNCDRCr.Text = "Dr"
                Else
                    lblNCDRCr.Text = "Cr"
                End If
                txtNotClear.Text = CStr(System.Math.Abs(mBalance))
                txtNotClear.Text = VB6.Format(txtNotClear.Text, "0.00")
            Else
                MsgBox("Bank Does Not Exist In Master")
                txtNotClear.Text = CStr(0.0#)
            End If
        End If

    End Sub
    Private Sub GetBankStmtBal()
        Dim mBBBal As Double
        Dim mBSBal As Double
        Dim mGridBalance As Double

        Dim mNotCleared As Double
        Dim mNotBooked As Double



        txtNotInOurBook.Text = 0


        mBBBal = Val(TxtBankStmtBal.Text)
        If lblBSDrCr.Text = "Dr" Then
            mBBBal = mBBBal
        Else
            mBBBal = -1 * mBBBal
        End If

        mNotCleared = Val(txtNotClear.Text)
        If lblNCDRCr.Text = "Dr" Then
            mNotCleared = mNotCleared
        Else
            mNotCleared = -1 * mNotCleared
        End If

        mNotBooked = Val(txtNotInOurBook.Text)
        If lblNIOBDrCr.Text = "Dr" Then
            mNotBooked = mNotBooked
        Else
            mNotBooked = -1 * mNotBooked
        End If

        With SprdMain
            .Row = .MaxRows
            .Col = ColDAmount
            If Val(.Text) <> 0.0# Then
                mGridBalance = Val(.Text)
            Else
                .Col = ColCAmount
                mGridBalance = -1 * Val(.Text)
            End If
        End With

        mBSBal = mBBBal - mNotCleared + mNotBooked     ''+ mGridBalance



        If mBSBal > 0 Then
            lblBankBDrCr.Text = "Dr"
        Else
            lblBankBDrCr.Text = "Cr"
        End If
        txtBankBalance.Text = CStr(System.Math.Abs(mBSBal))
        txtBankBalance.Text = VB6.Format(txtBankBalance.Text, "0.00")

        TxtBankBookBal.ReadOnly = True
        TxtBankStmtBal.ReadOnly = True
        txtNotClear.ReadOnly = True
        txtNotInOurBook.ReadOnly = True
        txtBankBalance.ReadOnly = True

    End Sub
    Private Sub FillBankCombo()

        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Long
        Dim mCompanyName As String

        On Error GoTo ERR1
        SqlStr = "Select SUPP_CUST_NAME from FIN_SUPP_CUST_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboBank.Items.Clear()
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboBank.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                RS.MoveNext()
            Loop
            cboBank.SelectedIndex = 0
            'Call GetBankBookBal("O")
        End If

        Dim mCompanyAdd As String

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
ERR1:
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        GetClearDate()
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        Dim mMKey As String
        If eventArgs.keyCode = System.Windows.Forms.Keys.Add Or eventArgs.keyCode = 187 Then
            FillMarkUnMark(("+"))
        End If

        If eventArgs.keyCode = System.Windows.Forms.Keys.Subtract Or eventArgs.keyCode = 189 Then
            FillMarkUnMark((" "))
        End If
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            GetClearDate()
        End If
    End Sub
    Private Sub FillMarkUnMark(ByRef pMarkSign As String)
        Dim mMKey As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = colAccount
        If SprdMain.Text = " " Then
            SprdMain.Col = ColMKEY
            mMKey = SprdMain.Text
            SprdMain.Col = ColMark
            SprdMain.Text = IIf(pMarkSign = "+", "+", "-")
            SprdMain.Row = SprdMain.Row + 1
            SprdMain.Col = ColMKEY
            Do While mMKey = SprdMain.Text
                SprdMain.Col = ColMark
                SprdMain.Text = pMarkSign
                SprdMain.Row = SprdMain.Row + 1
                SprdMain.Col = ColMKEY
            Loop
        Else
            SprdMain.Col = ColMark
            SprdMain.Text = pMarkSign
        End If
        SprdMain.Focus()
    End Sub
    Sub GetClearDate()
        Dim mRow As Integer
        lblClearDate.Visible = True
        txtClearDate.Visible = True
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColClearDate
        txtClearDate.Text = IIf(SprdMain.Text = "", RunDate, SprdMain.Text)
        txtClearDate.Focus()
    End Sub


    Private Sub txtChqNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim ii As Integer
        If txtChqNo.Text = "" Then GoTo EventExitSub
        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColChequeNo
                If UCase(.Text) = UCase(txtChqNo.Text) Then Exit For
            Next
            .Col = 1
            .Col2 = .MaxCols
            .Row = ii
            .Row2 = ii
            .Action = SS_ACTION_SELECT_BLOCK
            .Focus()
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtClearDate_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtClearDate.Leave

        On Error GoTo ERR1
        Dim mMKey As String
        Dim mSubRowNo As Integer
        Dim SqlStr As String
        Dim mClearDate As String
        Dim I As Short
        For I = 1 To SprdMain.MaxRows - 2
            SprdMain.Row = I
            SprdMain.Col = ColMark
            If SprdMain.Text = "+" Then
                SprdMain.Col = ColClearDate
                SprdMain.Text = txtClearDate.Text
                mClearDate = SprdMain.Text
                SprdMain.Col = ColMKEY
                mMKey = SprdMain.Text
                SprdMain.Col = ColSubRowNo
                mSubRowNo = Val(SprdMain.Text)

                SqlStr = " UPDATE FIN_POSTED_TRN SET" & vbCrLf _
                    & " ClearDate=TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " Where Mkey='" & mMKey & "' and TRNDTLSUBROWNO=" & mSubRowNo & ""
                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                    & " UPDATE_FROM='N'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " Where Mkey='" & mMKey & "'"

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE FIN_VOUCHER_DET " & vbCrLf & " SET " & vbCrLf _
                    & " ClearDate=TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " Where Mkey='" & mMKey & "' and Subrowno=" & mSubRowNo & ""
                PubDBCn.Execute(SqlStr)

            End If
            SprdMain.Col = ColMark
            SprdMain.Text = " "
        Next I
        FormatSprdLedg(-1)
        lblClearDate.Visible = False
        txtClearDate.Visible = False
        Call cmdShow_Click(cmdShow, New System.EventArgs())
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtClearDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtClearDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtClearDate.Text = "" Then GoTo EventExitSub
        If Not MainClass.ChkIsdateF(txtClearDate) Then
            MsgInformation("Invalid Date format")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateTo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDateTo.Leave
        'Call GetBankBookBal("C")
    End Sub

    Private Sub frmBankReco_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 300, mReFormWidth))
        Frame3.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        'MainClass.SetSpreadColor(UltraGrid1, -1)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColTransDate, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColTransDate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(sender As Object, e As _DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        'If EventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        'If EventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        'If EventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPartNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPartNo, 0))


        'If EventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
        '    If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

        '        SprdMain.Row = cntSearchRow
        '        SprdMain.Row2 = cntSearchRow
        '        SprdMain.Col = 1
        '        SprdMain.Col2 = SprdMain.MaxCols
        '        SprdMain.BlockMode = True
        '        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
        '        SprdMain.BlockMode = False

        '        MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
        '        cntSearchRow = cntSearchRow + 1
        '        cntSearchCol = cntSearchCol + 1
        '    End If
        'End If

        'SprdMain.Refresh()
    End Sub
End Class
