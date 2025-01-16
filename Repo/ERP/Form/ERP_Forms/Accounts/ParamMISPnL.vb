Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMISPnL
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColGroupType As Short = 3
    Private Const ColGroupSeqNo As Short = 4
    Private Const ColCode As Short = 5
    Private Const ColDescription As Short = 6
    Private Const ColCompanyName1 As Short = 7

    Dim ColFlag As Short

    Dim mcntRow As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain()

        If Trim(txtDateFrom.Text) = "" Then
            MsgInformation("Please Enter Date.")
            txtDateFrom.Focus()
            Exit Sub
        End If

        If Trim(txtDateTo.Text) = "" Then
            MsgInformation("Please Enter Date.")
            txtDateTo.Focus()
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mGroupCode As String
        Dim i As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String
        Dim mLevel As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Trim(txtDateFrom.Text) = "" Or Trim(txtDateTo.Text) = "" Then Exit Sub
        If IsDate(txtDateFrom.Text) = False Then
            MsgBox("Invalid Date")
            Exit Sub
        End If

        If IsDate(txtDateTo.Text) = False Then
            MsgBox("Invalid Date")
            Exit Sub
        End If

        SqlStr = "SELECT * FROM FIN_GROUP_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(GROUP_HEAD,1,1) IN ('E','I')"

        If cboGroupType.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf _
            & " AND GROUP_HEAD ='" & Trim(VB.Left(cboGroupType.Text, 3)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY SUBSTR(GROUP_HEAD,1,1) DESC, MIS_SEQNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 0
        i = 0

        If Not RsShow.EOF Then

            Do While Not RsShow.EOF
                mcntRow = mcntRow + 1
                i = i + 1
                SprdMain.Row = mcntRow

                mSrn = Str(i)

                mGroupCode = Trim(IIf(IsDBNull(RsShow.Fields("GROUP_CODE").Value), "", RsShow.Fields("GROUP_CODE").Value))
                mLevel = 1
                Call FillGridCol(RsShow, mLevel)

                SprdMain.MaxRows = SprdMain.MaxRows + 1
                RsShow.MoveNext()
            Loop
        End If



        Call FormatSprdMain()
        GroupBySpread(ColPicMain)

        Dim cntRow As Long
        Dim cntCol As Long
        Dim mValue As String
        Dim cntSummaryRow As Long
        Dim mTotalStartRow As Long
        Dim mTotalEndRow As Long

        Dim mExpType As String
        Dim mPreviousExpType As String = ""

        SprdSummary.MaxCols = SprdMain.MaxCols

        cntSummaryRow = 0
        mTotalStartRow = 1
        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColPicSub
            If Trim(SprdMain.Text) = "H" Then
                SprdMain.Row = cntRow
                SprdMain.Col = ColGroupType
                mExpType = Mid(SprdMain.Text, 1, 1)



                If mPreviousExpType <> mExpType And mPreviousExpType <> "" Then
                    cntSummaryRow = cntSummaryRow + 1
                    SprdSummary.MaxRows = cntSummaryRow

                    mTotalEndRow = cntSummaryRow - 1
                    For cntCol = ColCompanyName1 To SprdMain.MaxCols
                        Call CalcRowTotal(SprdSummary, cntCol, mTotalStartRow, cntCol, mTotalEndRow, cntSummaryRow, cntCol)
                    Next

                    SprdSummary.Row = cntSummaryRow
                    SprdSummary.Col = ColDescription
                    SprdSummary.Text = IIf(mExpType = "I", "INCOME ", "EXPENSES ") & "Total"

                    mTotalStartRow = cntSummaryRow + 1
                End If

                cntSummaryRow = cntSummaryRow + 1
                SprdSummary.MaxRows = cntSummaryRow
                For cntCol = ColGroupType To SprdMain.MaxCols
                    SprdMain.Row = cntRow
                    SprdMain.Col = cntCol
                    mValue = Trim(SprdMain.Text)

                    SprdSummary.Row = cntSummaryRow
                    SprdSummary.Col = cntCol
                    SprdSummary.Text = mValue


                Next
                mPreviousExpType = mExpType
            End If
        Next


        cntSummaryRow = cntSummaryRow + 1
        SprdSummary.MaxRows = cntSummaryRow

        mTotalEndRow = cntSummaryRow - 1
        For cntCol = ColCompanyName1 To SprdMain.MaxCols
            Call CalcRowTotal(SprdSummary, cntCol, mTotalStartRow, cntCol, mTotalEndRow, cntSummaryRow, cntCol)
        Next

        SprdSummary.Row = cntSummaryRow
        SprdSummary.Col = ColDescription
        SprdSummary.Text = IIf(mExpType = "I", "INCOME ", "EXPENSES ") & "Total"




        'mTotalEndRow = mcntRow
        'For j = ColCompanyName1 To ColFlag - 1
        '    If mGroupStock = "Y" Then
        '        Call CalcRowTotal(SprdMain, j, mTotalStartRow, j, mTotalEndRow, mTotalStartRow - 1, j, "Y")
        '    Else
        '        Call CalcRowTotal(SprdMain, j, mTotalStartRow, j, mTotalEndRow, mTotalStartRow - 1, j)
        '    End If
        'Next

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsShow.Cancel()
        RsShow.Close()
        RsShow = Nothing
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pLevel As Integer)
        On Error GoTo FillGERR
        Dim mGroupCode As String
        Dim mGroupType As String
        Dim mGroupSeqNo As String
        Dim mGroupStock As String

        With SprdMain

            .Col = ColGroupType
            mGroupType = IIf(IsDBNull(pRs.Fields("GROUP_HEAD").Value), "", pRs.Fields("GROUP_HEAD").Value)
            .Text = IIf(IsDBNull(pRs.Fields("GROUP_HEAD").Value), "", pRs.Fields("GROUP_HEAD").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColGroupSeqNo
            mGroupSeqNo = IIf(IsDBNull(pRs.Fields("MIS_SEQNO").Value), "", pRs.Fields("MIS_SEQNO").Value)
            .Text = IIf(IsDBNull(pRs.Fields("MIS_SEQNO").Value), "", pRs.Fields("MIS_SEQNO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColCode
            mGroupCode = IIf(IsDBNull(pRs.Fields("GROUP_CODE").Value), "", pRs.Fields("GROUP_CODE").Value)
            .Text = IIf(IsDBNull(pRs.Fields("GROUP_CODE").Value), "", pRs.Fields("GROUP_CODE").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDescription
            .Text = IIf(IsDBNull(pRs.Fields("GROUP_NAME").Value), "", pRs.Fields("GROUP_NAME").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColFlag
            .Text = "0"
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            mGroupStock = IIf(IsDBNull(pRs.Fields("STOCK_GROUP").Value), "N", pRs.Fields("STOCK_GROUP").Value)

        End With
NextRow:
        Call FillAccountRecord(mGroupCode, mGroupType, mGroupSeqNo, mGroupStock)
        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Sub FillAccountRecord(ByRef mGroupCode As String, ByRef mGroupType As String, ByRef mGroupSeqNo As String, ByRef mGroupStock As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mAccountCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer
        Dim mStatus As String
        Dim mCompanyName As String
        Dim mCompanyCode As Double
        Dim mLedgerAmount As Double

        Dim mTotalStartRow As Long
        Dim mTotalEndRow As Long
        Dim mProvisionCol As Boolean

        mTotalStartRow = mcntRow + 1

        If mGroupStock = "Y" Then
            With SprdMain
                mcntRow = mcntRow + 1
                .MaxRows = .MaxRows + 1
                .Row = mcntRow

                .Col = ColGroupType
                .Text = mGroupType
                .Font = VB6.FontChangeBold(.Font, False)

                .Col = ColGroupSeqNo
                .Text = mGroupSeqNo
                .Font = VB6.FontChangeBold(.Font, False)

                .Col = ColCode
                .Text = ""  ''IIf(IsDBNull(RsShow.Fields("SUPP_CUST_CODE").Value), "", RsShow.Fields("SUPP_CUST_CODE").Value)
                .Font = VB6.FontChangeBold(.Font, False)

                .Col = ColDescription
                .Text = "OPENING STOCK"
                .Font = VB6.FontChangeBold(.Font, False)

                For j = ColCompanyName1 To ColFlag - 1
                    .Row = 0
                    .Col = j
                    mCompanyName = Trim(.Text)
                    mCompanyCode = -1
                    mLedgerAmount = 0
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)

                        mLedgerAmount = GetStockAmount(mCompanyCode, mGroupCode, mGroupType, "O")
                    End If
                    .Row = mcntRow
                    .Col = j
                    .Text = VB.Format(mLedgerAmount, "0.00")
                Next
            End With
        End If
        SqlStr = " SELECT SUPP_CUST_CODE, SUPP_CUST_NAME "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND GROUPCODE='" & MainClass.AllowSingleQuote(mGroupCode) & "' "

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUPP_CUST_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            If Not RsShow.EOF Then
                Do While Not RsShow.EOF

                    mcntRow = mcntRow + 1
                    .MaxRows = .MaxRows + 1
                    .Row = mcntRow

                    .Col = ColGroupType
                    .Text = mGroupType
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColGroupSeqNo
                    .Text = mGroupSeqNo
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColCode
                    mAccountCode = IIf(IsDBNull(RsShow.Fields("SUPP_CUST_CODE").Value), "", RsShow.Fields("SUPP_CUST_CODE").Value)
                    .Text = IIf(IsDBNull(RsShow.Fields("SUPP_CUST_CODE").Value), "", RsShow.Fields("SUPP_CUST_CODE").Value)
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColDescription
                    .Text = IIf(IsDBNull(RsShow.Fields("SUPP_CUST_NAME").Value), "", RsShow.Fields("SUPP_CUST_NAME").Value)
                    .Font = VB6.FontChangeBold(.Font, False)

                    For j = ColCompanyName1 To ColFlag - 1
                        .Row = 0
                        .Col = j
                        mCompanyName = Trim(.Text)
                        mCompanyCode = -1
                        mLedgerAmount = 0
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)

                            mLedgerAmount = GetLedgerAmount(mCompanyCode, mAccountCode, mGroupType)
                            mProvisionCol = False
                        ElseIf mProvisionCol = False Then
                            mLedgerAmount = GetProvisionalLedgerAmount(mCompanyName, mAccountCode, mGroupType)
                            mProvisionCol = True
                        End If
                        .Row = mcntRow
                        .Col = j
                        .Text = VB.Format(mLedgerAmount, "0.00")
                    Next

                    RsShow.MoveNext()

                    If RsShow.EOF = True Then

                        If mGroupStock = "Y" Then
                            With SprdMain
                                mcntRow = mcntRow + 1
                                .MaxRows = .MaxRows + 1
                                .Row = mcntRow

                                .Col = ColGroupType
                                .Text = mGroupType
                                .Font = VB6.FontChangeBold(.Font, False)

                                .Col = ColGroupSeqNo
                                .Text = mGroupSeqNo
                                .Font = VB6.FontChangeBold(.Font, False)

                                .Col = ColCode
                                .Text = ""  ''IIf(IsDBNull(RsShow.Fields("SUPP_CUST_CODE").Value), "", RsShow.Fields("SUPP_CUST_CODE").Value)
                                .Font = VB6.FontChangeBold(.Font, False)

                                .Col = ColDescription
                                .Text = "CLOSING STOCK"
                                .Font = VB6.FontChangeBold(.Font, False)

                                For j = ColCompanyName1 To ColFlag - 1
                                    .Row = 0
                                    .Col = j
                                    mCompanyName = Trim(.Text)
                                    mCompanyCode = -1
                                    mLedgerAmount = 0
                                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)

                                        mLedgerAmount = GetStockAmount(mCompanyCode, mGroupCode, mGroupType, "C")
                                    End If
                                    .Row = mcntRow
                                    .Col = j
                                    .Text = VB.Format(mLedgerAmount, "0.00")
                                Next
                            End With
                        End If

                        mTotalEndRow = mcntRow
                        For j = ColCompanyName1 To ColFlag - 1
                            If mGroupStock = "Y" Then
                                Call CalcRowTotal(SprdMain, j, mTotalStartRow, j, mTotalEndRow, mTotalStartRow - 1, j, "Y")
                            Else
                                Call CalcRowTotal(SprdMain, j, mTotalStartRow, j, mTotalEndRow, mTotalStartRow - 1, j)
                            End If
                        Next
                    End If
                Loop
            End If
        End With

        Dim mColFrom As Long
        Dim mTotalShow As Boolean
        mTotalEndRow = mcntRow
        With SprdMain
            mColFrom = ColCompanyName1
            mTotalShow = False
            For j = ColCompanyName1 To ColFlag - 1
                .Row = 0
                .Col = j
                mCompanyName = Trim(.Text)
                mLedgerAmount = 0
                If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_SHORTNAME", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = False Then
                    If mTotalShow = False Then
                        mTotalShow = True
                        For i = 1 To SprdMain.MaxRows
                            Call CalcRowTotal(SprdMain, mColFrom, i, j, i, i, j + 1)
                            .Row = i
                            .Col = j + 1
                            '.SetCellBorder(j, i, j, i, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
                            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
                            .Font = VB6.FontChangeBold(.Font, True)
                        Next
                        mColFrom = j + 1 + 1  '(ConLockProvision & CalcRowTotal() Col)
                    End If
                Else
                        mTotalShow = False
                End If
            Next
        End With

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume	
    End Sub
    Private Function GetProvisionalLedgerAmount(ByRef mCompanyGroupName As String, ByRef mAccountCode As String, ByRef mGroupType As String) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mGroupName As String

        'Dim SearchWithinThis As String = mCompanyGroupName
        'Dim SearchForThis As String = "PROVISIONS : "
        'Dim FirstCharacter As Integer = SearchWithinThis.IndexOf(SearchForThis)

        ''COMPANY_GROUPING
        ''mCompanyGroupName  "PROVISIONS : "
        mGroupName = Mid(mCompanyGroupName, Len("PROVISIONS : ") + 1)
        If Mid(mGroupType, 1, 1) = "I" Then
            SqlStr = " SELECT SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) AMOUNT "
        Else
            SqlStr = " SELECT SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) AMOUNT "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_PROVISION_TRN TRN, GEN_COMPANY_MST GMST" & vbCrLf _
            & " WHERE TRN.Company_Code = GMST.Company_Code " & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.ACCOUNTCODE='" & mAccountCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND COMPANY_GROUPING='" & mGroupName & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If Not RsTemp.EOF Then
            GetProvisionalLedgerAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
        End If


        Exit Function
LedgError:
        GetProvisionalLedgerAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetLedgerAmount(ByRef mCompanyCode As Double, ByRef mAccountCode As String, ByRef mGroupType As String) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If Mid(mGroupType, 1, 1) = "I" Then
            SqlStr = " SELECT SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) AMOUNT "
        Else
            SqlStr = " SELECT SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) AMOUNT "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN" & vbCrLf _
            & " WHERE TRN.Company_Code = " & mCompanyCode & "" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.ACCOUNTCODE='" & mAccountCode & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If Not RsTemp.EOF Then
            GetLedgerAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
        End If


        Exit Function
LedgError:
        GetLedgerAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetStockAmount(ByRef mCompanyCode As Double, ByRef mGroupCode As String, ByRef mGroupType As String, ByRef mShowType As String) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetStockAmount = 0

        SqlStr = " SELECT "

        If mShowType = "O" Then
            SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.PARENT_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE,SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS STOCKVALUE"
        Else
            SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.PARENT_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'CS',0,1)  * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) AS STOCKVALUE"
        End If

        'If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " , NVL(ACCMST.SUPP_CUST_NAME,'') AS LEDGER_HEAD" & vbCrLf _
        '        & " , GETITEMLEDGERAMOUNT(INV.COMPANY_CODE, INV.FYEAR, ITEM.ITEM_CODE, NVL(GMST.ACCT_CONSUM_CODE,''), TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS LEDGER_AMT"
        'End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_STOCK_REC_TRN INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, INV_GENERAL_MST GMST, FIN_SUPP_CUST_MST ACM"     '', FIN_GROUP_CODE GRMST"
        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.COMPANY_CODE=" & mCompanyCode & " AND ACM.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        'SqlStr = SqlStr & vbCrLf & " And INV.STOCK_ID IN ('WH','PH','SH')"

        SqlStr = SqlStr & vbCrLf & " AND (INV.STOCK_ID = CASE WHEN GMST.PRD_TYPE='P' Then 'PH' ELSE '-1' END"
        SqlStr = SqlStr & vbCrLf & " OR INV.STOCK_ID = CASE WHEN GMST.PRD_TYPE='P' Then '-1' ELSE 'SH' END"
        SqlStr = SqlStr & vbCrLf & " OR INV.STOCK_ID = 'WH')"

        SqlStr = SqlStr & vbCrLf & " And INV.STOCK_TYPE In ('ST','RJ','QC') "

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
            & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf _
                & " AND GMST.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
                & " AND GMST.ACCT_CONSUM_CODE=ACM.SUPP_CUST_CODE AND ACM.GROUPCODE='" & mGroupCode & "'"

        'SqlStr = SqlStr & vbCrLf _
        '        & " AND ACM.COMPANY_CODE=GRMST.COMPANY_CODE" & vbCrLf _
        '        & " AND ACM.GROUPCODE=GRMST.GROUP_CODE"





        ''AND DEPT_CODE_TO='STR'
        'If cboShow.SelectedIndex = 0 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        'ElseIf cboShow.SelectedIndex = 1 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        'End If


        'If cboExportItem.SelectedIndex >= 1 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        'End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
            & " INV.COMPANY_CODE, ITEM.ITEM_CODE, NVL(ITEM.PARENT_CODE,ITEM.ITEM_CODE) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        GetStockAmount = 0
        If Not RsTemp.EOF Then
            Do While RsTemp.EOF = False
                GetStockAmount = GetStockAmount + IIf(IsDBNull(RsTemp.Fields("STOCKVALUE").Value), 0, RsTemp.Fields("STOCKVALUE").Value)
                RsTemp.MoveNext()
            Loop

        End If


        Exit Function
LedgError:
        GetStockAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub GroupBySpread(ByRef Col As Integer)
        'Group the data by the specified column	
        Dim i As Short
        Dim currentrow As Integer
        Dim lastid As String
        Dim prevtext As Object
        Dim lastheaderrow As Integer
        Dim ret As Boolean
        Dim Currentid As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SprdMain.ReDraw = False
        BoldHeader(Col)

        '    SprdMain.MaxCols = SprdMain.MaxCols + 2	
        'Insert 2 columns at beginning	
        For i = 1 To 2
            '        SprdMain.InsertCols i, 1	

            'Change col width	
            SprdMain.set_ColWidth(i, 2)
        Next i

        SprdMain.Col = ColPicMain
        SprdMain.Row = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	

        'Init variables	
        lastheaderrow = 0
        currentrow = 1
        lastid = "  "

        While currentrow <= SprdMain.DataRowCnt

            SprdMain.Row = currentrow
            SprdMain.Col = ColGroupType     ''ColCode
            Currentid = UCase(Trim(SprdMain.Text))
            'If InStr(1, Currentid, ".") > 0 Then
            '    Currentid = VB.Left(Currentid, InStr(1, Currentid, ".") - 1)
            'End If
            If Currentid <> lastid Then
                If lastheaderrow <> 0 Then
                    SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
                End If

                lastid = UCase(Trim(SprdMain.Text))
                'If InStr(1, lastid, ".") > 0 Then
                '    lastid = VB.Left(lastid, InStr(1, lastid, ".") - 1)
                'End If

                lastheaderrow = currentrow

                'Insert a new header row	
                InsertHeaderRow(currentrow, 0)
            Else
                MakePictureCellType((SprdMain.Row), ColPicSub)
                SprdMain.Col = ColPicSub
                SprdMain.TypePictPicture = minuspict
                SprdMain.SetCellBorder(ColPicSub, SprdMain.Row, ColPicSub, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
            End If
            SprdMain.Row = SprdMain.Row + 1
            currentrow = currentrow + 1

        End While

        'Display last read data	
        SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        SprdMain.MaxRows = SprdMain.DataRowCnt
        SprdMain.SetActiveCell(1, 1)

        'Paint Spread	
        SprdMain.ReDraw = True

        'Update displays	
        System.Windows.Forms.Application.DoEvents()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub MakePictureCellType(ByRef Row As Integer, ByRef Col As Short)
        'Define specified cell as type PICTURE	

        Exit Sub
        SprdMain.Col = Col
        SprdMain.Row = Row

        SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
        SprdMain.TypePictCenter = True
        SprdMain.TypePictMaintainScale = False
        SprdMain.TypePictStretch = False

    End Sub

    Private Sub InsertHeaderRow(ByRef RowNum As Integer, ByRef pRecordCount As Integer)
        'Insert a header row at the specifed location	

        '    SprdMain.InsertRows rownum, 1	

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

        MakePictureCellType(RowNum, ColPicMain)

        SprdMain.Col = ColPicMain
        SprdMain.TypePictPicture = minuspict

        SprdMain.Col = ColPicSub
        SprdMain.CellType = SS_CELL_TYPE_EDIT
        SprdMain.Text = "H"

        'Add picture state values	
        'SprdMain.Col = ColFlag
        'SprdMain.Text = "0"

        'Add Border	

        SprdMain.SetCellBorder(ColPicMain, RowNum, SprdMain.MaxCols, RowNum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

    End Sub
    Private Sub BoldHeader(ByRef Col As Integer)
        'Reset the header bolds and make the sort col bold	

        'Change font for visual cue to what column sorting on	
        'Reset all header fonts	
        With SprdMain
            .Row = 0
            .Col = -1
            .Font = VB6.FontChangeBold(.Font, False)

            'Bold the specified column	
            .Row = 0
            .Col = Col
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Public Sub frmParamMISPnL_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim mCol As Integer
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCompanyGroup As String
        Dim mCurrCompanyGroup As String
        If FormActive = True Then Exit Sub

        cboGroupType.Items.Clear()
        '    cboGroupType.AddItem ""	
        cboGroupType.Items.Add("ALL")

        cboGroupType.Items.Add("I1 : INCOME SALE")
        cboGroupType.Items.Add("I2 : INCOME OTHER SALE")
        cboGroupType.Items.Add("I3 : INCOME SALE SCRAP")
        cboGroupType.Items.Add("I4 : INCOME SALE JOBWORK")
        cboGroupType.Items.Add("I5 : INCOME SALE INTER-UNIT")
        cboGroupType.Items.Add("I6 : INCOME OTHER INCOME")
        cboGroupType.Items.Add("I7 : SALE RETURN")
        cboGroupType.Items.Add("I8 : SALE SUPPLIMENTRY")
        cboGroupType.Items.Add("E1 : MATERIAL COST")
        cboGroupType.Items.Add("E2 : POWER & FUEL")
        cboGroupType.Items.Add("E3 : FREIGHT OUTWARD")
        cboGroupType.Items.Add("E4 : FINANCE COST")
        cboGroupType.Items.Add("E5 : OTHER EXPENSES")
        cboGroupType.Items.Add("E6 : MANPOWER - PRODUCTION STAFF")
        cboGroupType.Items.Add("E7 : MANPOWER - GENERAL STAFF")
        cboGroupType.Items.Add("E8 : MANUFACTURING EXPENSES")
        cboGroupType.Items.Add("E9 : ADMINISTRATIVE EXPENSES")
        cboGroupType.Items.Add("E10 : DEPRECIATION")
        cboGroupType.Items.Add("E11 : PACKING EXPENSES")
        cboGroupType.Items.Add("E12 : JOBWORK SUB CONTACTING EXPENSES")
        cboGroupType.Items.Add("E13 : CORPORATE EXPENSES")
        cboGroupType.Items.Add("E14 : FREIGHT INWARD")
        cboGroupType.Items.Add("E15 : DUTY DRAW BACK")
        cboGroupType.Items.Add("E16 : FOREIGN EXCHANGE RATE FLUCTION")

        cboGroupType.Items.Add("E17 : INCREASE/(DECREASE) IN WIP & FG STOCKS")
        cboGroupType.Items.Add("E18 : BOP CONSUMPTION")
        cboGroupType.Items.Add("E19 : RAW MATERIAL WIP")
        cboGroupType.Items.Add("E20 : CONSUMPTION OF STORES & SPARES")
        cboGroupType.Items.Add("E21 : CUTTING TOOLS")
        cboGroupType.Items.Add("E22 : TOOLS & DIES")
        cboGroupType.Items.Add("E23 : GAS (LPG) & FURNACE OIL")
        cboGroupType.Items.Add("E24 : DIESEL")
        cboGroupType.Items.Add("E25 : PERSONNEL EXPENSES")
        cboGroupType.Items.Add("E26 : WORKMEN & STAFF WELFARE EXPENSES")
        cboGroupType.Items.Add("E27 : CONTRIBUTION TO PROVIDENT FUND & OTHER")
        cboGroupType.Items.Add("E28 : INTEREST")
        cboGroupType.Items.Add("E29 : INTEREST ON TERM LOAN")
        cboGroupType.Items.Add("E30 : INTEREST ON WORKING CAPITAL")
        cboGroupType.Items.Add("E31 : INTEREST ON UNSECURED LOANS")
        cboGroupType.Items.Add("E32 : BANK CHARGES")
        cboGroupType.Items.Add("E33 : SELLING EXPENSES")
        cboGroupType.Items.Add("E34 : BUSINESS PROMOTION")

        mSqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_GROUPING FROM GEN_COMPANY_MST WHERE STATUS='O' ORDER BY COMPANY_GROUPING,COMPANY_SHORTNAME"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            .Row = 0
            mCol = ColCompanyName1
            If Not RsTemp.EOF Then
                Do While Not RsTemp.EOF
                    .Col = mCol
                    .Text = IIf(IsDBNull(RsTemp.Fields("COMPANY_SHORTNAME").Value), "", RsTemp.Fields("COMPANY_SHORTNAME").Value)
                    mCompanyGroup = IIf(IsDBNull(RsTemp.Fields("COMPANY_GROUPING").Value), "", RsTemp.Fields("COMPANY_GROUPING").Value)
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mCurrCompanyGroup = IIf(IsDBNull(RsTemp.Fields("COMPANY_GROUPING").Value), "", RsTemp.Fields("COMPANY_GROUPING").Value)
                        If mCompanyGroup <> mCurrCompanyGroup Then
                            mCol = mCol + 1
                            .MaxCols = .MaxCols + 1
                            .Col = mCol
                            .Text = "PROVISIONS : " & mCompanyGroup

                            mCol = mCol + 1
                            .MaxCols = .MaxCols + 1
                            .Col = mCol
                            .Text = "TOTAL (" & mCompanyGroup & ")"
                        End If
                    Else
                        mCol = mCol + 1
                        .MaxCols = .MaxCols + 1
                        .Col = mCol
                        .Text = "PROVISIONS : " & mCompanyGroup

                        mCol = mCol + 1
                        .MaxCols = .MaxCols + 1
                        .Col = mCol
                        .Text = "TOTAL (" & mCompanyGroup & ")"
                    End If

                    mCol = mCol + 1
                    .MaxCols = .MaxCols + 1
                    If RsTemp.EOF = True Then
                        ColFlag = mCol
                    End If
                Loop
            End If
        End With

        Dim cntRow As Long
        Dim cntCol As Long
        Dim mValue As String

        SprdSummary.MaxCols = SprdMain.MaxCols

        For cntRow = 0 To 0
            For cntCol = 1 To SprdMain.MaxCols
                SprdMain.Row = cntRow
                SprdMain.Col = cntCol
                mValue = Trim(SprdMain.Text)

                SprdSummary.Row = cntRow
                SprdSummary.Col = cntCol
                SprdSummary.Text = mValue

            Next
        Next
        FormatSprdMain()
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamMISPnL_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7440)
        'Me.Width = VB6.TwipsToPixelsX(11625)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        mIsGrouped = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(0, RowHeight * 2)

            .Row = -1
            .set_RowHeight(-1, RowHeight)

            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColPicSub
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColCode, 8)

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColDescription, 25)
            .TypeEditMultiLine = True
            .ColsFrozen = ColDescription

            For cntCol = ColCompanyName1 To ColFlag - 1
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 14)
                .TypeEditMultiLine = True
            Next

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColFlag, 5)
            .ColHidden = True

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With

        With SprdSummary
            .set_RowHeight(0, RowHeight * 2)

            .Row = -1
            .set_RowHeight(-1, RowHeight)

            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColPicSub
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColCode, 8)

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColDescription, 25)
            .TypeEditMultiLine = True
            .ColsFrozen = ColDescription

            For cntCol = ColCompanyName1 To ColFlag - 1
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 14)
                .TypeEditMultiLine = True
            Next

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColFlag, 5)
            .ColHidden = True

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdSummary, -1)
            MainClass.ProtectCell(SprdSummary, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdSummary.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With
    End Sub

    Private Sub frmParamMISPnL_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SSTab.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1	
        '    MainClass.SetSpreadColor SprdOption, -1	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMISPnL_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


                For cntCol = 1 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'" & ","
                    End If

                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ")"
                mValueSQL = mValueSQL & ")"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume	
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub PrintBOM(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCustDealer As String
        Dim SqlStr As String = ""

        Report1.Reset()
        SqlStr = ""
        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        MainClass.ClearCRptFormulas(Report1)

        Call InsertIntoPrintdummyData()

        '*************** Fetching Record For Report ***************************	
        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " ORDER BY SUBROW"


        mTitle = "MISReport Report"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MISPNL.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim ii As Integer
        Dim mHeadStr As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, "mnuBOM")
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub



    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows	

        'Show Summary/Detail info.	
        'If clicked on a "+" or "-" grouping	

        If eventArgs.col = ColPicMain Then
            SprdMain.Col = ColPicMain
            SprdMain.Row = eventArgs.row
            If SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                'Show or hide the specified rows	
                ShowHideRows(ColPicMain, eventArgs.row)
            End If
        End If
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows	
        Dim i As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        SprdMain.Col = ColFlag

        If SprdMain.Text = "0" Then
            collapsetype = 0 'collape/hide rows : minus picture	
            SprdMain.Col = 1
            SprdMain.TypePictPicture = pluspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "1"
        Else
            collapsetype = 1 'uncollapse / show rows: plus picture	
            SprdMain.Col = 1
            SprdMain.TypePictPicture = minuspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "0"
        End If

        SprdMain.Redraw = False
        For i = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next i
        SprdMain.Redraw = True

    End Sub
End Class
