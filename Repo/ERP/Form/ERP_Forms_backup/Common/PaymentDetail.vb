Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports System.ComponentModel

Friend Class frmPaymentDetail
    Inherits System.Windows.Forms.Form
    Private XRIGHT As String
    Private ADDMode As Boolean
    Private MODIFYMode As Boolean
    Private FormLoaded As Boolean
    Private DataLoading As Boolean
    'Private PvtDBCn As ADODB.Connection

    Private Const ColPayType As Short = 1
    Private Const ColBillCheck As Short = 2
    Private Const ColBillNo As Short = 3
    Private Const ColBillDate As Short = 4
    Private Const ColLocationId As Short = 5
    Private Const ColBillAmount As Short = 6
    Private Const ColBillAmountDC As Short = 7
    Private Const ColBalance As Short = 8
    Private Const ColBalanceDC As Short = 9
    Private Const ColTDSAmount As Short = 10
    Private Const ColInterestAmount As Short = 11
    Private Const ColAmount As Short = 12
    Private Const ColDC As Short = 13
    Private Const ColOldAmount As Short = 14
    Private Const ColOldDC As Short = 15
    Private Const ColOldBillNo As Short = 16
    Private Const ColRefNo As Short = 17
    Private Const ColTaxableAmount As Short = 18
    Private Const ColDivCode As Short = 19
    Private Const ColBillCompany As Short = 20
    Private Const ColPONo As Short = 21
    Private Const ColDueDate As Short = 22

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim FileDBCn As ADODB.Connection
    Private Const ConRowHeight As Short = 14
    Private Sub FillHeading()
        With SprdMain
            .Row = 0

            .Col = ColPayType
            .Text = "Payment Type (B/N/O/A/D/C/T)"

            .Col = ColBillCheck
            .Text = "B"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColLocationId
            .Text = "Location ID"

            .Col = ColBillAmount
            .Text = "Bill Amount"

            .Col = ColBillAmountDC
            .Text = " "

            .Col = ColBalance
            .Text = "Balance Amount"

            .Col = ColBalanceDC
            .Text = " "

            .Col = ColTDSAmount
            .Text = "TDS Amount"

            .Col = ColInterestAmount
            .Text = "Interest Amount"

            .Col = ColAmount
            .Text = "Amount"

            .Col = ColDC
            .Text = "Dr/Cr"

            .Col = ColOldAmount
            .Text = " "

            .Col = ColOldDC
            .Text = " "

            .Col = ColOldBillNo
            .Text = " "

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColTaxableAmount
            .Text = "Taxable Amount"

            .Col = ColPONo
            .Text = "PO No"

            .Col = ColDivCode
            .Text = "Div"

            .Col = ColDueDate
            .Text = "Due Date"

            .Col = ColBillCompany
            .Text = "Bill For Which Company"

        End With
    End Sub

    Private Function GetPayType(ByRef pPayType As Object) As String
        Select Case UCase(pPayType)
            Case "B"
                GetPayType = "BILL"
            Case "N"
                GetPayType = "NEW REF"
            Case "D"
                GetPayType = "D/N"
            Case "C"
                GetPayType = "C/N"
            Case "T"
                GetPayType = "TDS"
            Case "O"
                GetPayType = "ON ACCOUNT"
            Case "A"
                GetPayType = "ADVANCE"
            Case Else
                GetPayType = "ON ACCOUNT"
        End Select
    End Function

    Private Sub PayTypeAction(ByRef pPayType As String, ByRef pRow As Integer)

        On Error GoTo ErrPart

        With SprdMain
            .Row = pRow
            Select Case UCase(pPayType)
                Case "B", "N"
                    .Col = ColPayType
                    .Text = IIf(pPayType = "B", "BILL", "NEW REF")

                    'MainClass.SetFocusToCell(SprdMain, pRow, ColBillNo)

                Case "D", "C"
                    .Col = ColPayType
                    .Text = IIf(pPayType = "D", "D/N", "C/N")

                    .Col = ColDC
                    .Text = IIf(pPayType = "D", "Cr", "Dr")

                    .Col = ColDivCode
                    .Text = Str(CDbl(lblDivisionCode.Text))

                    'MainClass.SetFocusToCell(SprdMain, pRow, ColBillNo)

                Case "T"
                    .Col = ColPayType
                    .Text = "TDS"

                    .Col = ColDivCode
                    .Text = Str(CDbl(lblDivisionCode.Text))

                    'MainClass.SetFocusToCell(SprdMain, pRow, ColBillNo)

                Case "A", "O"
                    .Col = ColPayType
                    .Text = IIf(pPayType = "O", "ON ACCOUNT", "ADVANCE")

                    .Col = ColBillNo
                    .Text = IIf(Trim(.Text) = "", "", .Text)

                    .Col = ColBillDate
                    .Text = VB6.Format(IIf(Trim(.Text) = "", lblVDate.Text, .Text), "dd/mm/yyyy")

                    .Col = ColBillAmount
                    .Text = ""

                    .Col = ColTDSAmount
                    .Text = ""

                    .Col = ColInterestAmount
                    .Text = ""

                    .Col = ColBalance
                    .Text = ""

                    .Col = ColDivCode
                    .Text = Str(CDbl(lblDivisionCode.Text))

                    .Col = ColAmount
                    If Val(.Text) = 0 Then
                        .Text = CStr(Val(lblDiffAmt.Text))
                    End If

                    'If UCase(pPayType) = "O" Then
                    '    MainClass.SetFocusToCell(SprdMain, pRow, ColBillDate)
                    'Else
                    '    MainClass.SetFocusToCell(SprdMain, pRow, ColAmount)
                    'End If
            End Select
        End With

        ProtectUnProtectCell(UCase(pPayType), pRow)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function ValidatePOAmount(ByRef pPONO As String, ByRef pBillDate As String, ByRef pAccountCode As String, ByRef pPOAmount As Double, ByRef pPrevBillAmount As Double, ByRef pCurrBillAmount As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        pPrevBillAmount = 0
        pPOAmount = 0
        pCurrBillAmount = 0

        SqlStr = " SELECT SUM(ID.GROSS_AMT) AS AMOUNT " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_PO=" & Val(pPONO) & " " & vbCrLf _
            & " AND PO_STATUS='Y' AND AMEND_WEF_DATE = (" & vbCrLf _
            & " SELECT MAX(AMEND_WEF_DATE) AS AMOUNT " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _
            & " AND AUTO_KEY_PO=" & Val(pPONO) & " " & vbCrLf _
            & " AND PO_STATUS='Y' AND AMEND_WEF_DATE <=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pPOAmount = RsTemp.Fields("Amount").Value
        End If

        SqlStr = "SELECT SUM(TRN.AMOUNT) AS AMOUNT " & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_BILLDETAILS_TRN TRN" & vbCrLf _
            & " WHERE IH.MKEY=TRN.MKEY AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _
            & " AND TRN.PONO=" & Val(pPONO) & " "

        If lblMkey.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY <> '" & lblMkey.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pPrevBillAmount = RsTemp.Fields("Amount").Value
        End If


        SqlStr = "SELECT SUM(TRN.AMOUNT) AS AMOUNT From FIN_TEMPBILL_TRN " & vbCrLf _
            & " Where UserID='" & PubUserID & "' AND TEMPMKEY=" & Val(lblTempProcessKey.Text) & "" & vbCrLf _
            & " AND AccountCode = '" & MainClass.AllowSingleQuote(pAccountCode) & "' " & vbCrLf _
            & " AND TRNDTLSUBROWNO=" & Val(lblTrnRowNo.Text) & "" & vbCrLf _
            & " AND BookType='" & UCase(Trim(lblBookType.Text)) & "'" & vbCrLf _
            & " AND TRN.PONO=" & Val(pPONO) & " "

        If RsTemp.EOF = False Then
            pCurrBillAmount = RsTemp.Fields("Amount").Value
        End If

        ValidatePOAmount = True
        Exit Function
ErrPart:
        ValidatePOAmount = False
    End Function

    Private Sub cmdAppendDetail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAppendDetail.Click

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mCheckBillNo As String
        Dim mDrCr As String = ""
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mSearchCompanyCode As Double

        DataLoading = True
        If Trim(txtDefaultCompanyName.Text) = "" Then
            mSearchCompanyCode = -1
        Else
            If MainClass.ValidateWithMasterTable(Trim(txtDefaultCompanyName.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mSearchCompanyCode = MasterNo
            Else
                mSearchCompanyCode = -1
            End If
        End If

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
                    & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
                    & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
                    & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select COMPANY_CODE, CASE WHEN BillNo='ON ACCOUNT' THEN 'O' WHEN BillNo='ADVANCE' THEN 'A' ELSE  'B' END AS TRNTYPE,BILLNO, BillDate, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BILLDC , " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BALDC, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS Amount, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf _
            & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo,Min(EXPDATE) AS DUEDATE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf & " WHERE " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & lblAccountCode.Text & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
            If mSearchCompanyCode > 0 Then
                SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & mSearchCompanyCode & ""
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If optAsPerBill.Checked = True Then
            If IsDate(txtBillSearchFrom.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND BILLDATE >=TO_DATE('" & VB6.Format(txtBillSearchFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If IsDate(txtDate.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND BILLDATE <=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        Else
            If IsDate(txtBillSearchFrom.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND EXPDATE >=TO_DATE('" & VB6.Format(txtBillSearchFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If IsDate(txtDate.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND EXPDATE <=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If


        SqlStr = SqlStr & vbCrLf & " GROUP BY COMPANY_CODE,BillNo,BillDate" & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BillNo "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempPRDetail.EOF = True Then Exit Sub

        With SprdMain
            Do While RsTempPRDetail.EOF = False
                .Row = .MaxRows
                mCheckBillNo = IIf(RsTempPRDetail.Fields("TRNTYPE").Value = "O" Or RsTempPRDetail.Fields("TRNTYPE").Value = "A", "", IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value))


                If BillNoNotIsGrid(mCheckBillNo, IIf(IsDBNull(RsTempPRDetail.Fields("BillDate").Value), "", RsTempPRDetail.Fields("BillDate").Value)) = False Then GoTo NextRecd

                If RsTempPRDetail.Fields("TRNTYPE").Value = "O" Or RsTempPRDetail.Fields("TRNTYPE").Value = "A" Then
                    mDivCode = IIf(Val(lblDivisionCode.Text) = 0, 1, Val(lblDivisionCode.Text))
                Else
                    mDivCode = GetDivisionCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text))
                End If

                .Col = ColPayType
                mPayType = RsTempPRDetail.Fields("TRNTYPE").Value

                mCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTempPRDetail.Fields("COMPANY_CODE").Value)  '' GetCompanyCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text))


                .Col = ColBillCompany
                .Text = GetCompanyName(mCompanyCode, "S")

                mLocCode = GetLocationCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text), mCompanyCode, mPayType)


                .Col = ColBillNo  ''
                .Text = IIf(mPayType = "O" Or mPayType = "A", "", IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value))

                If .Text = mPayType Then .Text = ""

                .Col = ColBillDate
                .Text = RsTempPRDetail.Fields("BillDate").Value

                .Col = ColLocationId
                .Text = mLocCode

                .Col = ColBillAmount
                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BillAMT").Value), 0, RsTempPRDetail.Fields("BillAMT").Value)))


                .Col = ColBillAmountDC
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)

                mDrCr = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)

                .Col = ColBalance
                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value)))

                .Col = ColBalanceDC
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)

                .Col = ColTDSAmount
                .Text = ""

                .Col = ColInterestAmount
                .Text = ""

                .Col = ColAmount
                .Text = CStr(Val(RsTempPRDetail.Fields("Amount").Value))

                .Col = ColDC
                .Text = RsTempPRDetail.Fields("PAYDC").Value

                .Col = ColOldBillNo
                .Text = (IIf(IsDBNull(RsTempPRDetail.Fields("OldBillNo").Value), "", RsTempPRDetail.Fields("OldBillNo").Value))

                .Col = ColOldAmount
                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("OldAmount").Value), 0, RsTempPRDetail.Fields("OldAmount").Value)))

                .Col = ColOldDC
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("OldDC").Value), "D", RsTempPRDetail.Fields("OldDC").Value)

                .Col = ColRefNo
                .Text = ""

                .Col = ColTaxableAmount
                .Text = "0.00"

                '            .Col = ColPONo
                '            .Text = IIf(IsNull(RsTempPRDetail!PONO), "", RsTempPRDetail!PONO)

                .Col = ColDueDate
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)

                .Col = ColDivCode
                .Text = Str(mDivCode)

                .Row = .MaxRows
                .Col = ColPayType
                .Text = GetPayType(mPayType)

                .Col = ColPayType
                If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then

                Else
                    MainClass.ProtectCell(SprdMain, .Row, .Row, ColDC, ColDC)
                End If
                .Col = ColPayType
                ProtectUnProtectCell(VB.Left(.Text, 1), .Row)

                .Col = ColBillAmountDC
                If UCase(mDrCr) = UCase(lblDC.Text) Then
                    .Row = .Row
                    .Row2 = .Row
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                    .BlockMode = False
                End If


                .MaxRows = .MaxRows + 1
NextRecd:
                RsTempPRDetail.MoveNext()
            Loop
            '        ProtectUnProtectCell Left(mPayType, 1), -1
            CalcTots()
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDivCode, ColDivCode)
            '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC
            If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        End With
        DataLoading = False
    End Sub
    Private Function GetLocationCode(ByRef pBillNo As String, ByRef pBillDate As String, ByRef pAccountCode As String, ByRef pCompanyCode As Long, ByRef mPayType As String) As String

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " Select DISTINCT LOCATION_ID  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & pCompanyCode & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & pAccountCode & "'" ''& vbCrLf |            & " AND BILLDATE ='" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "'"

        'If mPayType = "O" Then
        '    SqlStr = SqlStr & vbCrLf & " AND VNO='" & pBillNo & "'"
        'Else
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & pBillNo & "'"
        'End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            GetLocationCode = GetDefaultLocation(pAccountCode)
        Else
            GetLocationCode = RsTemp.Fields("LOCATION_ID").Value
        End If
        Exit Function
ErrPart:
        GetLocationCode = GetDefaultLocation(pAccountCode)
    End Function
    Private Function GetCompanyCode(ByRef pBillNo As String, ByRef pBillDate As String, ByRef pAccountCode As String) As Long

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetCompanyCode = 0   '' RsCompany.Fields("COMPANY_CODE").Value  ''Sandeep Bill Validate also

        SqlStr = " Select DISTINCT COMPANY_CODE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & pAccountCode & "'" & vbCrLf & " AND BillNo='" & pBillNo & "'" & vbCrLf _
            & " AND BILLDATE =TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
        End If
        Exit Function
ErrPart:
    End Function
    Private Function GetDivisionCode(ByRef pBillNo As String, ByRef pBillDate As String, ByRef pAccountCode As String) As Double

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " Select DISTINCT DIV_CODE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & pAccountCode & "'" & vbCrLf & " AND BillNo='" & pBillNo & "'" ''& vbCrLf |            & " AND BILLDATE ='" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            GetDivisionCode = IIf(Val(lblDivisionCode.Text) = 0, 1, Val(lblDivisionCode.Text))
        Else
            GetDivisionCode = RsTemp.Fields("DIV_CODE").Value
        End If
        Exit Function
ErrPart:
        GetDivisionCode = Val(lblDivisionCode.Text)
    End Function
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        ConPaymentDetail = False
        Me.Hide()
        Me.Close()
        FormLoaded = False
        frmAtrn.Refresh()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        On Error GoTo ErrPart
        Dim pBillNo As String = ""
        Dim pBillDate As String = ""
        Dim pVNo As String = ""
        Dim mErrorMsg As String = ""

        If CheckSupplierDuplicateBill(pBillNo, pBillDate, pVNo, mErrorMsg) = True Then
            MsgInformation(mErrorMsg) '"Supplier Bill No : " & pBillNo & " Dated : " & pBillDate & " Already Entered Against V No. " & pVNo & "."
            Exit Sub
        End If

        If ValidCompanyBill(mErrorMsg) = False Then
            MsgInformation(mErrorMsg) '"Supplier Bill No : " & pBillNo & " Dated : " & pBillDate & " Already Entered Against V No. " & pVNo & "."
            Exit Sub
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            If Val(LblNetAmt.Text) = Val(lblVoucherAmount.Text) Then
                If Mid(lblNetDC.Text, 1, 1) <> Mid(lblVoucherDC.Text, 1, 1) Then
                    MsgInformation("Net Amount :" & Val(LblNetAmt.Text) & lblNetDC.Text & " is not Match with Voucher Amount :" & Val(lblVoucherAmount.Text) & lblVoucherDC.Text & ".") '"Supplier Bill No : " & pBillNo & " Dated : " & pBillDate & " Already Entered Against V No. " & pVNo & "."
                    Exit Sub
                End If
            Else
                MsgInformation("Net Amount :" & Val(LblNetAmt.Text) & lblNetDC.Text & " is not Match with Voucher Amount :" & Val(lblVoucherAmount.Text) & lblVoucherDC.Text & ".") '"Supplier Bill No : " & pBillNo & " Dated : " & pBillDate & " Already Entered Against V No. " & pVNo & "."
                Exit Sub
            End If
        Else

        End If

        CheckForEqualAmount()
        Exit Sub
ErrPart:
    End Sub
    Private Function CheckSupplierDuplicateBill(ByRef pBillNo As String, ByRef pBillDate As String, ByRef pVNo As String, ByRef mErrorMsg As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim pPayType As String

        CheckSupplierDuplicateBill = False
        pBillNo = ""
        pBillDate = ""
        pVNo = ""

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColPayType


                pPayType = VB.Left(Trim(UCase(.Text)), 1)

                .Col = ColBillCheck
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColBillNo
                    pBillNo = Trim(UCase(.Text))

                    .Col = ColBillDate
                    pBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                    If pPayType = "A" Then
                        '                    If CDate(lblVDate.text) >= CDate(PubGSTApplicableDate) Then
                        '                        mErrorMsg = "Cann't be Select Advance payment here."
                        '                        CheckSupplierDuplicateBill = True
                        '                        Exit Function
                        '                    End If
                    End If

                    .Col = ColLocationId
                    If Trim(.Text) = "" Then
                        mErrorMsg = "Location ID Cann't be Blank."
                        CheckSupplierDuplicateBill = True
                        Exit Function
                    End If

                    If pPayType = "N" Then
                        If Not IsDate(pBillDate) Then
                            pVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                            pVNo = pVNo & " Dt. " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                            mErrorMsg = "Bill Date is Blank for Bill No : " & pBillNo & " Dated : " & pBillDate & "."
                            CheckSupplierDuplicateBill = True
                            Exit Function
                        End If

                        ''Sandeep 14-03-2023
                        'If CDate(lblVDate.Text) >= CDate(PubGSTApplicableDate) Then
                        '    mErrorMsg = "Cann't be Select New Bill Here."
                        '    CheckSupplierDuplicateBill = True
                        '    Exit Function
                        'End If

                        'If CDate(pBillDate) >= CDate(PubGSTApplicableDate) Then
                        '    mErrorMsg = "Cann't be Select New Bill Here."
                        '    CheckSupplierDuplicateBill = True
                        '    Exit Function
                        'End If

                        SqlStr = " SELECT VNO, VDATE FROM FIN_POSTED_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND" & vbCrLf & " ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(UCase(lblAccountCode.Text))) & "'" & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(Trim(pBillNo)) & "'" & vbCrLf & " AND BILLDATE=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRNTYPE IN ('N', DECODE(BOOKTYPE,'J','',DECODE(BOOKTYPE,'B','','B')))" & vbCrLf & " AND BOOKTYPE<>'O'"

                        If Trim(lblMkey.Text) <> "" Then
                            SqlStr = SqlStr & " AND MKEY<>'" & lblMkey.Text & "'"
                        End If

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            pVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                            pVNo = pVNo & " Dt. " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                            mErrorMsg = "Supplier Bill No : " & pBillNo & " Dated : " & pBillDate & " Already Entered Against V No. " & pVNo & "."
                            CheckSupplierDuplicateBill = True
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With

        Exit Function
ErrPart:
        CheckSupplierDuplicateBill = False
    End Function
    Private Function ValidCompanyBill(ByRef mErrorMsg As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim pPayType As String
        Dim mCompanyCode As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim mLocation As String
        Dim mCheckBillCompany As String
        Dim mBillCompany As String

        ValidCompanyBill = False


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColPayType
                pPayType = VB.Left(Trim(UCase(.Text)), 1)

                .Col = ColBillCheck
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColLocationId
                    If Trim(.Text) = "" Then
                        mErrorMsg = "Location ID Cann't be Blank."
                        ValidCompanyBill = False
                        Exit Function
                    End If

                    .Col = ColBillCompany
                    If Trim(.Text) = "" Then
                        mErrorMsg = "Party Bill Company Name Cann't be Blank."
                        ValidCompanyBill = False
                        Exit Function
                    End If
                    mCheckBillCompany = Trim(.Text)

                    .Col = ColBillNo
                    pBillNo = Trim(UCase(.Text))

                    .Col = ColBillDate
                    pBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                    If pPayType = "B" Then
                        mCompanyCode = GetCompanyCode(pBillNo, pBillDate, lblAccountCode.Text)
                        If mCheckBillCompany <> GetCompanyName(mCompanyCode, "S") Then
                            mErrorMsg = "Party Bill No. " & pBillNo & " is not match with Selected Company Name."
                            ValidCompanyBill = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With
        ValidCompanyBill = True
        Exit Function
ErrPart:
        ValidCompanyBill = False
    End Function
    Private Sub CheckForEqualAmount()
        On Error GoTo ERR1
        CalcTots()
        If MainClass.ValidDataInGrid(SprdMain, ColDC, "S", "Amount Dr/Cr Column is must") = False Then Exit Sub


        If Val(LblNetAmt.Text) = Val(lblAmount.Text) And VB.Left(lblNetDC.Text, 1) = VB.Left(lblDC.Text, 1) Then
            ConPaymentDetail = True
            ConLCDiscPaymentDetail = True
            UpdateTempPRDetail()
            Me.Hide()
            '        Unload Me
            FormLoaded = False
            frmAtrn.Refresh()
        ElseIf Val(LblNetAmt.Text) = Val(lblAmount.Text) Then
            ConPaymentDetail = True
            ConLCDiscPaymentDetail = True
            UpdateTempPRDetail()
            Me.Hide()
            '        Unload Me
            FormLoaded = False
            frmAtrn.Refresh()
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function UpdateTempPRDetail() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim cntRow As Short
        Dim mTRNType As String
        Dim mBillNo As String
        Dim mAmount As Double
        Dim mDC As String
        Dim mBillAmount As Double
        Dim mBillDC As String
        Dim mBillDate As String
        Dim mOldAmount As Double
        Dim mOldDC As String
        Dim mOldBillNo As String
        Dim mDueDate As String
        Dim mRemarks As String = ""
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mTaxableAmount As Double
        Dim mPONo As String
        Dim mDivCode As Double
        Dim mRefNo As String
        Dim xRefNo As String
        Dim mLocCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As Long

        Dim mInterestAmount As Double
        Dim mTDSAmount As Double

        UpdateTempPRDetail = False
        SqlStr = "DELETE FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & Val(lblTempProcessKey.Text) & "" & vbCrLf & " AND AccountCode='" & MainClass.AllowSingleQuote((lblAccountCode.Text)) & "'" & vbCrLf & " AND TRNDTLSUBROWNO=" & Val(lblTrnRowNo.Text) & "" & vbCrLf & " AND BookType='" & UCase(Trim(lblBookType.Text)) & "'  "

        PubDBCn.Execute(SqlStr)

        mFormRecdCode = -1

        mFormDueCode = -1

        mIsRegdNo = "N"


        mSTType = "0"


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColBillCheck

                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColPayType
                    mTRNType = .Text

                    .Col = ColBillNo
                    mBillNo = IIf(Trim(.Text) = "", mTRNType, Trim(.Text))

                    .Col = ColRefNo
                    xRefNo = Trim(.Text)

                    .Col = ColBillDate
                    mBillDate = IIf(mBillNo = "ON ACCOUNT" Or mBillNo = "ADVANCE", "", Trim(.Text))

                    If (VB.Left(mTRNType, 1) = "O" Or VB.Left(mTRNType, 1) = "A") And xRefNo <> "" Then
                        mBillNo = mBillNo & "-" & xRefNo
                    End If

                    mRemarks = mRemarks & IIf(mRemarks = "", "", ", ") & mBillNo '& IIf(Trim(mBillDate) = "", "", " Dt. " & mBillDate)
                End If
            Next
            mRemarks = IIf(mRemarks = "", "", " agt Bill No(s) ") & mRemarks
            mRemarks = VB.Left(mRemarks, 254)

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColBillCheck

                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    .Col = ColPayType
                    mTRNType = .Text

                    .Col = ColLocationId
                    mLocCode = Trim(.Text)

                    .Col = ColBillNo
                    mBillNo = IIf(Trim(.Text) = "", mTRNType, Trim(.Text))

                    .Col = ColBillDate
                    mBillDate = Trim(.Text)

                    .Col = ColBillAmount
                    mBillAmount = Val(.Text)

                    .Col = ColTDSAmount
                    mTDSAmount = Val(.Text)

                    .Col = ColInterestAmount
                    mInterestAmount = Val(.Text)


                    .Col = ColBillAmountDC
                    mBillDC = VB.Left(Trim(.Text), 1)

                    .Col = ColAmount
                    mAmount = Val(.Text)

                    .Col = ColDC
                    mDC = VB.Left(.Text, 1)

                    .Col = ColOldAmount
                    mOldAmount = Val(.Text)

                    .Col = ColOldBillNo
                    mOldBillNo = .Text

                    .Col = ColOldDC
                    mOldDC = .Text

                    .Col = ColRefNo
                    mRefNo = Trim(.Text)

                    .Col = ColTaxableAmount
                    mTaxableAmount = Val(.Text)

                    .Col = ColPONo
                    mPONo = Trim(.Text)

                    .Col = ColDivCode
                    mDivCode = Val(.Text)

                    .Col = ColDueDate
                    mDueDate = VB6.Format(IIf(Trim(.Text) = "", lblVDate.Text, .Text), "dd/mm/yyyy")

                    .Col = ColBillCompany
                    mCompanyName = Trim(.Text)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = MasterNo
                    Else
                        mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
                    End If

                    mTRNType = VB.Left(mTRNType, 1)

                    If mAmount = 0 Then GoTo NextRow


                    SqlStr = "INSERT INTO FIN_TEMPBILL_TRN  ( " & vbCrLf _
                        & " USERID, TRNDTLSUBROWNO ,SUBROWNO , BOOKTYPE, " & vbCrLf _
                        & " ACCOUNTCODE, TRNTYPE, BILLNO, BILLDATE, " & vbCrLf _
                        & " BILLAMOUNT, BILLDC, Amount, DC, " & vbCrLf _
                        & " OldBillNo,OldAmount,OldDC," & vbCrLf _
                        & " DUEDATE,REMARKS,BillCheck, " & vbCrLf _
                        & " STTYPE, STFORMCODE, STFORMNAME, " & vbCrLf _
                        & " STFORMNO, STFORMDATE, STDUEFORMCODE, " & vbCrLf _
                        & " STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE, " & vbCrLf _
                        & " ISREGDNO, TAXABLE_AMOUNT,PONO,DIV_CODE, REF_NO, TEMPMKEY,BILL_TO_LOC_ID,BILL_COMPANY_CODE,TDS_AMOUNT, INTEREST_AMOUNT " & vbCrLf _
                        & " ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " " & Val(lblTrnRowNo.Text) & "," & vbCrLf _
                        & " " & cntRow & ", '" & UCase(lblBookType.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(UCase(mTRNType)) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mBillAmount & ", '" & mBillDC & "',  " & " " & mAmount & ", '" & mDC & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mOldBillNo) & "', " & vbCrLf _
                        & " " & mOldAmount & ", '" & mOldDC & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mRemarks) & "','Y', " & vbCrLf _
                        & " '" & mSTType & "', " & mFormRecdCode & ", ''," & vbCrLf _
                        & " '', '', " & mFormDueCode & "," & vbCrLf _
                        & " '', '', ''," & vbCrLf _
                        & " '" & mIsRegdNo & "'," & mTaxableAmount & ", '" & mPONo & "'," & mDivCode & ",'" & MainClass.AllowSingleQuote(mRefNo) & "'," & Val(lblTempProcessKey.Text) & ",'" & mLocCode & "','" & mCompanyCode & "'," & Val(mTDSAmount) & "," & Val(mInterestAmount) & ") "

                    PubDBCn.Execute(SqlStr)
                End If
NextRow:
            Next
        End With
        UpdateTempPRDetail = True
        Exit Function
ERR1:
        UpdateTempPRDetail = False
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Sub cmdPopulateNew_Click()

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        'Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""


        MainClass.ClearGrid(SprdMain)

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""

        '    mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
        ''                & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
        ''                & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
        ''                & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
        ''                & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " SELECT "

        '' ColPayType
        SqlStr = SqlStr & vbCrLf & " CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN BillNo ELSE  'BILL' END AS TRNTYPE, "

        ''ColBillCheck
        SqlStr = SqlStr & vbCrLf & " '0' AS BILLCHECK, "

        ''ColBillNo
        SqlStr = SqlStr & vbCrLf & " CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN '' ELSE BILLNO END AS BILLNO,  "

        ''ColBillDate
        SqlStr = SqlStr & vbCrLf & " BillDate, BILL_TO_LOC_ID, "

        'ColBillAmount
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN 0 ELSE ABS(" & mBillAmtStr & ") END) AS BillAMT, "


        'ColBillAmountDC
        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BILLDC, "

        'ColBalance
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN 0 ELSE ABS(" & mBalAmtStr & ") END) AS BALANCE, "

        'ColBalanceDC
        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BALDC, "

        'ColTDSAmount, InterestAmount
        SqlStr = SqlStr & vbCrLf & " '0' AS TDSAMT, '0' AS IntAMT,"

        'ColAmount
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS Amount, "

        'ColDC
        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'CR' ELSE 'DR' END AS PAYDC, "

        'ColOldAmount
        SqlStr = SqlStr & vbCrLf & " 0 AS OldAmount, "

        'ColOldDC
        SqlStr = SqlStr & vbCrLf & " 'D' AS OldDC, "

        'ColOldBillNo
        SqlStr = SqlStr & vbCrLf & " '' AS OldBillNo, "

        'ColRefNo

        SqlStr = SqlStr & vbCrLf & " '' AS RefNo, "
        'ColTaxableAmount
        SqlStr = SqlStr & vbCrLf & " '0.00' AS TaxableAmount, "

        'ColDueDate
        SqlStr = SqlStr & vbCrLf & " Min(EXPDATE) AS DUEDATE "

        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN  " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND BILLDATE <='" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""

        SqlStr = SqlStr & vbCrLf & " GROUP BY BILLNO, BillDate, BILL_TO_LOC_ID" & vbCrLf & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BILLNO "



        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        MainClass.AddBlankSprdRow(SprdMain, ColBillNo, ConRowHeight)
        FormatSprdMain(-1, False)
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayType, SprdMain.MaxCols)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColBillAmountDC
                mDrCr = Trim(.Text)

                .Col = ColPayType
                If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then

                Else
                    MainClass.ProtectCell(SprdMain, .Row, .Row, ColDC, ColDC)
                End If

                If UCase(mDrCr) = UCase(lblDC.Text) Then
                    .Row = I
                    .Row2 = I
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                    .BlockMode = False
                End If
            Next
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDivCode, ColDivCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC)
            If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        End With

        CalcTots()
        cmdPopulate.Enabled = False
    End Sub

    Private Sub PopulateFromXLSTVSNewFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""

        Dim mSqlStr As String
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim mFileBillNo As String
        Dim mFileTrnType As String
        Dim mFileAmount As Double
        Dim mFileAmountStr As String
        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim xAccountCode As String
        Dim xAccountAlias As String
        Dim mFileBillDate As String = ""
        Dim mFileBillFromDate As String = ""
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mLocationID As String
        Dim mOnAccountPayment As Double = 0
        Dim mBillBalAmount As Double = 0
        Dim mBillBalDC As String
        Dim mPaymentPostAmount As Double = 0
        Dim mPaymentBalAmount As Double = 0

        Dim FPath As String

        Dim ErrorFile As System.IO.StreamWriter

        FPath = mPubBarCodePath & "\BillImportError.txt"

        If FILEExists(FPath) Then
            Kill(FPath)
        End If

        ErrorFile = My.Computer.FileSystem.OpenTextFileWriter(FPath, True)

        'If MainClass.ValidateWithMasterTable(Trim(lblAccountCode.Text), "SUPP_CUST_CODE", "ALIAS_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    xAccountAlias = MasterNo
        'Else
        '    xAccountAlias = ""
        'End If
        mFileBillFromDate = "01/04/2022"
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1, False)
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayType, SprdMain.MaxCols)

        '    Call GetExcelRecord

        '    FileConnStr = "DSN=PAYMENT"
        '    Set FileDBCn = New ADODB.Connection
        '    FileDBCn.Open FileConnStr

        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        '    MainClass.UOpenRecordSet mSqlStr, FileDBCn, adOpenStatic, RsFile
        '    RsFile.Open mSqlStr, FileDBCn, , adLockReadOnly, adCmdText
        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    xAccountAlias = Trim(Trim(IIf(IsDBNull(RsFile.Fields(4).Value), "", RsFile.Fields(4).Value)))
                    If MainClass.ValidateWithMasterTable(Trim(xAccountAlias), "ALIAS_NAME", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(lblAccountCode.Text) & "'") = True Then
                        mLocationID = MasterNo
                    Else
                        mLocationID = ""
                    End If

                    mFileBillNo = IIf(IsDBNull(RsFile.Fields(1).Value), "", RsFile.Fields(1).Value)
                    mFileBillDate = VB6.Format(IIf(IsDBNull(RsFile.Fields(2).Value), "", RsFile.Fields(2).Value), "DD/MM/YYYY")
                    If Val(mFileBillDate) <> 0 Then
                        mFileBillDate = VB.Left(mFileBillDate, 2) & "/" & Mid(mFileBillDate, 4, 2) & "/" & VB.Right(mFileBillDate, 4)
                    End If

                    mFileAmountStr = IIf(IsDBNull(RsFile.Fields(6).Value), "", RsFile.Fields(6).Value)
                    mFileAmount = Val(mFileAmountStr)

                    mFileAmountStr = IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value)
                    mFileAmount = mFileAmount - Val(mFileAmountStr)

                    If MainClass.ValidateWithMasterTable(mFileBillNo, "BILLNO", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "INVOICE_DATE=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
                        mFileTrnType = "B"
                    Else
                        'If mPaymentBalAmount <> 0 Then
                        ErrorFile.WriteLine("Mismatch Bill No : " & mFileBillNo & " AMOUNT : " & mFileAmount)
                        'End If
                        mFileTrnType = "O"
                        mFileBillNo = "ON ACCOUNT"

                    End If

                    mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
                    mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                    mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                    mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""

                    If mFileTrnType = "O" Or mFileTrnType = "A" Or mFileTrnType = "N" Then
                        mDivCode = Val(lblDivisionCode.Text)
                    Else
                        mDivCode = GetDivisionCode(mFileBillNo, mFileBillDate, (lblAccountCode.Text))
                    End If

                    'If mDivCode <> Val(lblDivisionCode.Text) Then GoTo NextRecord

                    If mFileTrnType = "O" And mFileAmount <> 0 Then

                        SprdMain.Row = SprdMain.MaxRows

                        SprdMain.Col = ColPayType
                        mPayType = mFileTrnType

                        SprdMain.Col = ColLocationId
                        SprdMain.Text = mLocationID

                        SprdMain.Col = ColAmount
                        SprdMain.Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                        SprdMain.Col = ColDC
                        SprdMain.Text = IIf(mFileAmount > 0, "CR", "DR")                            ''RsTempPRDetail.Fields("PAYDC").Value

                        SprdMain.Col = ColTaxableAmount
                        SprdMain.Text = "0.00"

                        SprdMain.Col = ColDueDate
                        SprdMain.Text = mFileBillDate

                        SprdMain.Col = ColDivCode
                        SprdMain.Text = Str(mDivCode)

                        SprdMain.Row = SprdMain.MaxRows
                        SprdMain.Col = ColPayType
                        SprdMain.Text = GetPayType("O")

                        SprdMain.Col = ColPayType
                        If VB.Left(SprdMain.Text, 1) = "O" Or VB.Left(SprdMain.Text, 1) = "A" Then

                        Else
                            MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColDC, ColDC)
                        End If
                        SprdMain.Col = ColPayType
                        ProtectUnProtectCell(VB.Left(SprdMain.Text, 1), (SprdMain.Row))

                        SprdMain.Col = ColBillAmountDC
                        If UCase(mDrCr) = UCase(lblDC.Text) Then
                            SprdMain.Row = SprdMain.Row
                            SprdMain.Row2 = SprdMain.Row
                            SprdMain.Col = 1
                            SprdMain.Col2 = SprdMain.MaxCols
                            SprdMain.BlockMode = True
                            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                            SprdMain.BlockMode = False
                        End If

                        SprdMain.Col = ColBillCheck
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                    Else
                        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
                            & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
                            & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
                            & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
                            & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

                        SqlStr = " Select COMPANY_CODE, CASE WHEN BillNo='ON ACCOUNT' THEN 'O' WHEN BillNo='ADVANCE' THEN 'A' ELSE  'B' END AS TRNTYPE,BillNo,BillDate, " & vbCrLf _
                            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
                            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BILLDC , " & vbCrLf _
                            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
                            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BALDC, " & vbCrLf _
                            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS Amount, " & vbCrLf _
                            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf _
                            & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo,Min(EXPDATE) AS DUEDATE  " & vbCrLf _
                            & " FROM FIN_POSTED_TRN  " & vbCrLf _
                            & " WHERE " & vbCrLf _
                            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'"

                        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
                        Else
                            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                        End If

                        SqlStr = SqlStr & vbCrLf & " And BillNo ='" & MainClass.AllowSingleQuote(mFileBillNo) & "'"
                        SqlStr = SqlStr & vbCrLf & " AND BILLDATE >=TO_DATE('" & VB6.Format(mFileBillFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                        SqlStr = SqlStr & vbCrLf & " AND BILLDATE <=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        SqlStr = SqlStr & vbCrLf & " GROUP BY COMPANY_CODE, BillNo, BillDate" & vbCrLf & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BillNo "

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTempPRDetail.EOF = True Then GoTo NextRecord


                        With SprdMain
                            Do While RsTempPRDetail.EOF = False
                                .Row = .MaxRows

                                .Col = ColPayType
                                mPayType = RsTempPRDetail.Fields("TRNTYPE").Value

                                SprdMain.Col = ColLocationId
                                SprdMain.Text = mLocationID

                                .Col = ColBillNo
                                .Text = IIf(mPayType = "O" Or mPayType = "A", "", IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value))

                                If .Text = mPayType Then .Text = ""

                                mCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTempPRDetail.Fields("COMPANY_CODE").Value)  '' GetCompanyCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text))


                                .Col = ColBillCompany
                                .Text = GetCompanyName(mCompanyCode, "S")

                                mLocCode = GetLocationCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text), mCompanyCode, mPayType)

                                .Col = ColBillDate
                                '.Text = IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value)
                                .Text = RsTempPRDetail.Fields("BillDate").Value

                                .Col = ColBillAmount
                                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BillAMT").Value), 0, RsTempPRDetail.Fields("BillAMT").Value)))

                                .Col = ColTDSAmount
                                .Text = "0.00"

                                .Col = ColInterestAmount
                                .Text = "0.00"

                                .Col = ColBillAmountDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)
                                mDrCr = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)

                                .Col = ColBalance
                                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value)))
                                mBillBalAmount = Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value))

                                .Col = ColBalanceDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)
                                mBillBalDC = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)
                                mBillBalAmount = mBillBalAmount * IIf(mBillBalDC = "D", -1, 1)

                                .Col = ColLocationId
                                .Text = mLocCode

                                'Dim mPaymentPOstAmount As Double
                                'Dim mPaymentBalAmount As Double

                                mPaymentPostAmount = IIf(mFileAmount > mBillBalAmount, mBillBalAmount, mFileAmount)
                                mPaymentBalAmount = IIf(mFileAmount > mBillBalAmount, VB6.Format(mFileAmount - mBillBalAmount, "0.00"), 0)      ''

                                mOnAccountPayment = mOnAccountPayment + mPaymentBalAmount

                                If mPaymentBalAmount <> 0 Then
                                    ErrorFile.WriteLine(.MaxRows & " Bill No : " & IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value) & " EXCESS AMOUNT : " & mPaymentBalAmount)
                                End If

                                .Col = ColAmount
                                .Text = VB6.Format(System.Math.Abs(mPaymentPostAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                                If System.Math.Abs(mBillBalAmount) > System.Math.Abs(mPaymentPostAmount) Then
                                    ErrorFile.WriteLine(.MaxRows & " Bill No : " & IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value) & " SHORT AMOUNT : " & System.Math.Abs(mBillBalAmount) - System.Math.Abs(mPaymentPostAmount))
                                End If

                                .Col = ColDC
                                .Text = "CR" 'IIf(mFileAmount > 0, "DR", "CR")                            ''RsTempPRDetail.Fields("PAYDC").Value

                                .Col = ColOldBillNo
                                .Text = (IIf(IsDBNull(RsTempPRDetail.Fields("OldBillNo").Value), "", RsTempPRDetail.Fields("OldBillNo").Value))

                                .Col = ColOldAmount
                                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("OldAmount").Value), 0, RsTempPRDetail.Fields("OldAmount").Value)))
                                'mOldAmount = .Text

                                .Col = ColOldDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("OldDC").Value), "D", RsTempPRDetail.Fields("OldDC").Value)
                                'mOldDC = .Text

                                .Col = ColTaxableAmount
                                .Text = "0.00"

                                .Col = ColDueDate
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)

                                SprdMain.Col = ColDivCode
                                SprdMain.Text = Str(mDivCode)

                                .Row = .MaxRows
                                .Col = ColPayType
                                .Text = GetPayType(mPayType)

                                .Col = ColPayType
                                If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then

                                Else
                                    MainClass.ProtectCell(SprdMain, .Row, .Row, ColDC, ColDC)
                                End If
                                .Col = ColPayType
                                ProtectUnProtectCell(VB.Left(.Text, 1), .Row)

                                .Col = ColBillAmountDC
                                If UCase(mDrCr) = UCase(lblDC.Text) Then
                                    .Row = .Row
                                    .Row2 = .Row
                                    .Col = 1
                                    .Col2 = .MaxCols
                                    .BlockMode = True
                                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                                    .BlockMode = False
                                End If

                                SprdMain.Col = ColBillCheck
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)



                                .MaxRows = .MaxRows + 1
                                '                        FormatSprdMain -1, False
                                RsTempPRDetail.MoveNext()
                            Loop
                        End With
                    End If
NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If mOnAccountPayment > 0 Then
            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColPayType
            mPayType = "O"

            SprdMain.Col = ColLocationId
            SprdMain.Text = mLocationID

            SprdMain.Col = ColAmount
            SprdMain.Text = VB6.Format(System.Math.Abs(mOnAccountPayment), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

            SprdMain.Col = ColDC
            SprdMain.Text = IIf(mOnAccountPayment > 0, "CR", "DR")                            ''RsTempPRDetail.Fields("PAYDC").Value

            SprdMain.Col = ColTaxableAmount
            SprdMain.Text = "0.00"

            SprdMain.Col = ColDueDate
            SprdMain.Text = mFileBillDate

            SprdMain.Col = ColDivCode
            SprdMain.Text = Str(mDivCode)

            SprdMain.Row = SprdMain.MaxRows
            SprdMain.Col = ColPayType
            SprdMain.Text = GetPayType("O")

            SprdMain.Col = ColPayType
            If VB.Left(SprdMain.Text, 1) = "O" Or VB.Left(SprdMain.Text, 1) = "A" Then

            Else
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColDC, ColDC)
            End If
            SprdMain.Col = ColPayType
            ProtectUnProtectCell(VB.Left(SprdMain.Text, 1), (SprdMain.Row))

            SprdMain.Col = ColBillAmountDC
            mDrCr = Trim(SprdMain.Text)
            If UCase(mDrCr) = UCase(lblDC.Text) Then
                SprdMain.Row = SprdMain.Row
                SprdMain.Row2 = SprdMain.Row
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                SprdMain.BlockMode = False
            End If

            SprdMain.Col = ColBillCheck
            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

            SprdMain.MaxRows = SprdMain.MaxRows + 1

        End If
        'SetSprdCellFormat()
        CalcTots()

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColDivCode)
        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
            '        FileDBCn = ""
        End If

        strTemp = ""
        strXLSFile = ""

        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC
        If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        '    End With


        cmdPopulate.Enabled = False

        ErrorFile.Close()

        If FILEExists(FPath) Then
            Process.Start("notepad.exe", FPath)            ''Process.Start("explorer.exe", FPath)
        End If


        Exit Sub
ErrPart:
        ErrorFile.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub


    Private Sub PopulateFromToken(ByRef pTokenNo As Double)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""

        Dim mSqlStr As String
        Dim RsFile As ADODB.Recordset = Nothing
        Dim FileConnStr As String

        Dim mFileBillNo As String
        Dim mFileTrnType As String
        Dim mFileAmount As Double
        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String
        Dim xAccountCode As String
        Dim xAccountAlias As String
        Dim mFileBillDate As String
        Dim mFileBillFromDate As String
        Dim mDivCode As Double
        Dim mFormat As String
        Dim mFileLocId As String

        'If MainClass.ValidateWithMasterTable(Trim(lblAccountCode.Text), "SUPP_CUST_CODE", "ALIAS_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    xAccountAlias = MasterNo
        'Else
        '    xAccountAlias = ""
        'End If

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1, False)
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayType, SprdMain.MaxCols)


        SqlStr = "SELECT COMPANY_CODE, TOKEN_NO, USERID, " & vbCrLf & " BILLNO, BILLDATE,  BILL_TO_LOC_ID, CUST_ALIAS, CUST_FORMAT, " & vbCrLf & " SUM(AMOUNT) AS AMOUNT" & vbCrLf & " FROM TEMP_UPLOAD_BILLDETAIL " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TOKEN_NO=" & pTokenNo & "" & vbCrLf & " GROUP BY COMPANY_CODE, TOKEN_NO, USERID, " & vbCrLf & " BILLNO, BILLDATE, BILL_TO_LOC_ID, CUST_ALIAS, CUST_FORMAT" & vbCrLf & " ORDER BY BILLDATE, BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFile)
        '    RsFile.Open mSqlStr, FileDBCn, , adLockReadOnly, adCmdText

        If RsFile.EOF = False Then
            Do While Not RsFile.EOF
                mFileBillNo = IIf(IsDBNull(RsFile.Fields("BILLNO").Value), "", RsFile.Fields("BILLNO").Value)
                mFileBillDate = VB6.Format(IIf(IsDBNull(RsFile.Fields("BILLDATE").Value), "", RsFile.Fields("BILLDATE").Value), "DD/MM/YYYY")
                mFileLocId = IIf(IsDBNull(RsFile.Fields("BILL_TO_LOC_ID").Value), "", RsFile.Fields("BILL_TO_LOC_ID").Value)
                mFormat = IIf(IsDBNull(RsFile.Fields("CUST_FORMAT").Value), "", RsFile.Fields("CUST_FORMAT").Value)
                mFileAmount = CDbl(VB6.Format(IIf(IsDBNull(RsFile.Fields("Amount").Value), 0, RsFile.Fields("Amount").Value), "0.00"))

                If CDate(mFileBillDate) >= CDate(PubGSTApplicableDate) Then
                    If CDate(mFileBillDate) >= CDate("01/04/2020") Then
                        mFileBillNo = IIf(Val(mFileBillNo) > 0, "S", "") & VB6.Format(mFileBillNo, "0000000000")
                    Else
                        mFileBillNo = IIf(Val(mFileBillNo) > 0, "S", "") & VB6.Format(mFileBillNo, "00000000")
                    End If
                Else
                    mFileBillNo = "S" & VB6.Format(mFileBillNo, "00000")
                End If

                If MainClass.ValidateWithMasterTable(mFileBillNo, "BILLNO", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
                    mFileTrnType = "B"
                Else
                    mFileTrnType = "O"
                    mFileBillNo = "ON ACCOUNT"
                End If

                mFileBillFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -4, CDate(mFileBillDate)))

                mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
                mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
                mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""

                If mFileTrnType = "O" Or mFileTrnType = "A" Or mFileTrnType = "N" Then
                    mDivCode = Val(lblDivisionCode.Text)
                Else
                    mDivCode = GetDivisionCode(mFileBillNo, mFileBillDate, (lblAccountCode.Text))
                End If
                If mDivCode <> Val(lblDivisionCode.Text) Then GoTo NextRecord

                If mFileTrnType = "O" And mFileAmount <> 0 Then

                    SprdMain.Row = SprdMain.MaxRows

                    SprdMain.Col = ColPayType
                    mPayType = mFileTrnType

                    SprdMain.Col = ColAmount
                    SprdMain.Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                    SprdMain.Col = ColDC
                    SprdMain.Text = IIf(mFileAmount > 0, "DR", "CR") ''"CR"        ''                      ''RsTempPRDetail.Fields("PAYDC").Value

                    '                    SprdMain.Col = ColOldBillNo
                    '                    SprdMain.Text = ""
                    '
                    '                    SprdMain.Col = ColOldAmount
                    '                    SprdMain.Text = ""
                    '
                    '                    SprdMain.Col = ColOldDC
                    '                    SprdMain.Text = "DR"

                    SprdMain.Col = ColTaxableAmount
                    SprdMain.Text = "0.00"

                    SprdMain.Col = ColDueDate
                    SprdMain.Text = mFileBillDate

                    '                    If mFileTrnType = "O" Or mFileTrnType = "A" Then
                    '                        mDivCode = Val(lblDivisionCode.text)
                    '                    Else
                    '                        mDivCode = GetDivisionCode(IIf(IsNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), RsTempPRDetail.Fields("BillDate").Value, lblAccountCode.text)
                    '                    End If

                    SprdMain.Col = ColDivCode
                    SprdMain.Text = Str(mDivCode)

                    SprdMain.Row = SprdMain.MaxRows
                    SprdMain.Col = ColPayType
                    SprdMain.Text = GetPayType("O")

                    SprdMain.Col = ColPayType
                    If VB.Left(SprdMain.Text, 1) = "O" Or VB.Left(SprdMain.Text, 1) = "A" Then

                    Else
                        MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColDC, ColDC)
                    End If
                    SprdMain.Col = ColPayType
                    ProtectUnProtectCell(VB.Left(SprdMain.Text, 1), (SprdMain.Row))

                    SprdMain.Col = ColBillAmountDC
                    If UCase(mDrCr) = UCase(lblDC.Text) Then
                        SprdMain.Row = SprdMain.Row
                        SprdMain.Row2 = SprdMain.Row
                        SprdMain.Col = 1
                        SprdMain.Col2 = SprdMain.MaxCols
                        SprdMain.BlockMode = True
                        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                        SprdMain.BlockMode = False
                    End If

                    SprdMain.Col = ColBillCheck
                    SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                Else
                    mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

                    ''CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN TO_DATE('" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "') ELSE

                    SqlStr = " Select CASE WHEN BillNo='ON ACCOUNT' THEN 'O' WHEN BillNo='ADVANCE' THEN 'A' ELSE  'B' END AS TRNTYPE,BillNo,BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BILLDC , " & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BALDC, " & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS Amount, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo,Min(EXPDATE) AS DUEDATE  " & vbCrLf & " FROM FIN_POSTED_TRN  " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'"

                    '                    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""
                    SqlStr = SqlStr & vbCrLf & " AND BillNo ='" & MainClass.AllowSingleQuote(mFileBillNo) & "'"
                    SqlStr = SqlStr & vbCrLf & " AND BILLDATE >=TO_DATE('" & VB6.Format(mFileBillFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    SqlStr = SqlStr & vbCrLf & " AND BILLDATE <=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    '                Sqlstr = Sqlstr & vbCrLf & " AND BILLDATE <='" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "'"

                    SqlStr = SqlStr & vbCrLf & " GROUP BY BillNo, BillDate" & vbCrLf & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BillNo "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTempPRDetail.EOF = True Then GoTo NextRecord ''MsgBox "No bill found" & mFileBillNo:


                    With SprdMain
                        Do While RsTempPRDetail.EOF = False
                            .Row = .MaxRows

                            .Col = ColPayType
                            mPayType = RsTempPRDetail.Fields("TRNTYPE").Value

                            '                        .Col = ColBillCheck
                            '                        .Value = vbChecked

                            .Col = ColBillNo
                            .Text = IIf(mPayType = "O" Or mPayType = "A", "", IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value))

                            If .Text = mPayType Then .Text = ""

                            .Col = ColBillDate
                            '.Text = IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value)
                            .Text = RsTempPRDetail.Fields("BillDate").Value

                            .Col = ColBillAmount
                            .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BillAMT").Value), 0, RsTempPRDetail.Fields("BillAMT").Value)))

                            .Col = ColTDSAmount
                            .Text = "0.00"

                            .Col = ColInterestAmount
                            .Text = "0.00"

                            .Col = ColBillAmountDC
                            .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)
                            mDrCr = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)
                            .Col = ColBalance
                            .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value)))
                            .Col = ColBalanceDC
                            .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)


                            .Col = ColAmount
                            .Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                            .Col = ColDC
                            .Text = IIf(mFileAmount > 0, "DR", "CR") ''"CR"        '                        ''RsTempPRDetail.Fields("PAYDC").Value

                            .Col = ColOldBillNo
                            .Text = (IIf(IsDBNull(RsTempPRDetail.Fields("OldBillNo").Value), "", RsTempPRDetail.Fields("OldBillNo").Value))

                            .Col = ColOldAmount
                            .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("OldAmount").Value), 0, RsTempPRDetail.Fields("OldAmount").Value)))
                            'mOldAmount = .Text

                            .Col = ColOldDC
                            .Text = IIf(IsDBNull(RsTempPRDetail.Fields("OldDC").Value), "D", RsTempPRDetail.Fields("OldDC").Value)
                            'mOldDC = .Text

                            .Col = ColTaxableAmount
                            .Text = "0.00"

                            .Col = ColDueDate
                            .Text = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)

                            '                        If mFileTrnType = "O" Or mFileTrnType = "A" Then
                            '                            mDivCode = Val(lblDivisionCode.text)
                            '                        Else
                            '                            mDivCode = GetDivisionCode(IIf(IsNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), RsTempPRDetail.Fields("BillDate").Value, lblAccountCode.text)
                            '                        End If

                            SprdMain.Col = ColDivCode
                            SprdMain.Text = Str(mDivCode)

                            .Row = .MaxRows
                            .Col = ColPayType
                            .Text = GetPayType(mPayType)

                            .Col = ColPayType
                            If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then

                            Else
                                MainClass.ProtectCell(SprdMain, .Row, .Row, ColDC, ColDC)
                            End If
                            .Col = ColPayType
                            ProtectUnProtectCell(VB.Left(.Text, 1), .Row)

                            .Col = ColBillAmountDC
                            If UCase(mDrCr) = UCase(lblDC.Text) Then
                                .Row = .Row
                                .Row2 = .Row
                                .Col = 1
                                .Col2 = .MaxCols
                                .BlockMode = True
                                .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                                .BlockMode = False
                            End If

                            SprdMain.Col = ColBillCheck
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                            .MaxRows = .MaxRows + 1
                            '                        FormatSprdMain -1, False
                            RsTempPRDetail.MoveNext()
                        Loop
                    End With
                End If
NextRecord:
                RsFile.MoveNext()
            Loop
        End If

        CalcTots()

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColDivCode)
        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing


        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC
        If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        '    End With
        cmdPopulate.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub


    Private Sub PopulateFromXLSHHMLFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""

        Dim mSqlStr As String
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim mFileBillNo As String
        Dim mFileTrnType As String
        Dim mFileAmount As Double
        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim xAccountCode As String
        Dim xAccountAlias As String
        Dim mFileBillDate As String
        Dim mFileBillFromDate As String
        Dim mDivCode As Double

        If MainClass.ValidateWithMasterTable(Trim(lblAccountCode.Text), "SUPP_CUST_CODE", "ALIAS_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAccountAlias = MasterNo
        Else
            xAccountAlias = ""
        End If
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1, False)
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayType, SprdMain.MaxCols)

        '    Call GetExcelRecord

        '    FileConnStr = "DSN=PAYMENT"
        '    Set FileDBCn = New ADODB.Connection
        '    FileDBCn.Open FileConnStr

        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        '    MainClass.UOpenRecordSet mSqlStr, FileDBCn, adOpenStatic, RsFile
        '    RsFile.Open mSqlStr, FileDBCn, , adLockReadOnly, adCmdText
        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF

                    mFileBillNo = IIf(IsDBNull(RsFile.Fields(2).Value), "", RsFile.Fields(2).Value)
                    mFileBillDate = VB6.Format(IIf(IsDBNull(RsFile.Fields(3).Value), 0, RsFile.Fields(3).Value), "DD/MM/YYYY")
                    '                If Val(mFileBillDate) <> 0 Then
                    '                    mFileBillDate = Left(mFileBillDate, 2) & "/" & Mid(mFileBillDate, 3, 2) & "/" & Right(mFileBillDate, 4)
                    '                End If
                    '

                    mFileBillFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -4, CDate(mFileBillDate)))

                    If CDate(mFileBillDate) >= CDate(PubGSTApplicableDate) Then
                        mFileBillNo = IIf(Val(mFileBillNo) > 0, "S", "") & VB6.Format(mFileBillNo, "00000000")
                    Else
                        mFileBillNo = "S" & VB6.Format(mFileBillNo, "00000")
                    End If

                    If MainClass.ValidateWithMasterTable(mFileBillNo, "BILLNO", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
                        mFileTrnType = "B"
                    Else
                        mFileTrnType = "O"
                        mFileBillNo = "ON ACCOUNT"
                    End If

                    '                If Val(mFileBillNo) = 0 Then
                    '                    mFileTrnType = "O"
                    '                    mFileBillNo = "ON ACCOUNT"
                    '                Else
                    '                    mFileTrnType = "B"
                    '
                    '                End If

                    mFileAmount = IIf(IsDBNull(RsFile.Fields(7).Value), 0, RsFile.Fields(7).Value)

                    mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
                    mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                    mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                    ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
                    mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""

                    If mFileTrnType = "O" Or mFileTrnType = "A" Or mFileTrnType = "N" Then
                        mDivCode = Val(lblDivisionCode.Text)
                    Else
                        mDivCode = GetDivisionCode(mFileBillNo, mFileBillDate, (lblAccountCode.Text))
                    End If
                    '                If mDivCode <> Val(lblDivisionCode.text) Then GoTo NextRecord

                    If mFileTrnType = "O" And mFileAmount <> 0 Then

                        SprdMain.Row = SprdMain.MaxRows

                        SprdMain.Col = ColPayType
                        mPayType = mFileTrnType

                        '
                        SprdMain.Col = ColAmount
                        SprdMain.Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                        SprdMain.Col = ColDC
                        SprdMain.Text = IIf(mFileAmount > 0, "DR", "CR") ''RsTempPRDetail.Fields("PAYDC").Value


                        SprdMain.Col = ColTaxableAmount
                        SprdMain.Text = "0.00"

                        SprdMain.Col = ColDueDate
                        SprdMain.Text = mFileBillDate

                        '                    If mFileTrnType = "O" Or mFileTrnType = "A" Then
                        '                        mDivCode = Val(lblDivisionCode.text)
                        '                    Else
                        '                        mDivCode = GetDivisionCode(IIf(IsNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), RsTempPRDetail.Fields("BillDate").Value, lblAccountCode.text)
                        '                    End If

                        SprdMain.Col = ColDivCode
                        SprdMain.Text = Str(mDivCode)

                        SprdMain.Row = SprdMain.MaxRows
                        SprdMain.Col = ColPayType
                        SprdMain.Text = GetPayType("O")

                        SprdMain.Col = ColPayType
                        If VB.Left(SprdMain.Text, 1) = "O" Or VB.Left(SprdMain.Text, 1) = "A" Then

                        Else
                            MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColDC, ColDC)
                        End If
                        SprdMain.Col = ColPayType
                        ProtectUnProtectCell(VB.Left(SprdMain.Text, 1), (SprdMain.Row))

                        SprdMain.Col = ColBillAmountDC
                        If UCase(mDrCr) = UCase(lblDC.Text) Then
                            SprdMain.Row = SprdMain.Row
                            SprdMain.Row2 = SprdMain.Row
                            SprdMain.Col = 1
                            SprdMain.Col2 = SprdMain.MaxCols
                            SprdMain.BlockMode = True
                            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                            SprdMain.BlockMode = False
                        End If

                        SprdMain.Col = ColBillCheck
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                    Else
                        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

                        ''CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN TO_DATE('" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "') ELSE

                        SqlStr = " Select CASE WHEN BillNo='ON ACCOUNT' THEN 'O' WHEN BillNo='ADVANCE' THEN 'A' ELSE  'B' END AS TRNTYPE,BillNo,BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BILLDC , " & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BALDC, " & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS Amount, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo,Min(EXPDATE) AS DUEDATE  " & vbCrLf & " FROM FIN_POSTED_TRN  " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'"

                        '                    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""
                        SqlStr = SqlStr & vbCrLf & " AND BillNo ='" & MainClass.AllowSingleQuote(mFileBillNo) & "'"
                        '                    SqlStr = SqlStr & vbCrLf & " AND BILLDATE >='" & vb6.Format(mFileBillFromDate, "DD-MMM-YYYY") & "'"
                        SqlStr = SqlStr & vbCrLf & " AND BILLDATE =TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                        '                Sqlstr = Sqlstr & vbCrLf & " AND BILLDATE <='" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "'"

                        SqlStr = SqlStr & vbCrLf & " GROUP BY BillNo, BillDate" & vbCrLf & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BillNo "

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTempPRDetail.EOF = True Then GoTo NextRecord


                        With SprdMain
                            Do While RsTempPRDetail.EOF = False
                                .Row = .MaxRows

                                .Col = ColPayType
                                mPayType = RsTempPRDetail.Fields("TRNTYPE").Value

                                '                        .Col = ColBillCheck
                                '                        .Value = vbChecked

                                .Col = ColBillNo
                                .Text = IIf(mPayType = "O" Or mPayType = "A", "", IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value))

                                If .Text = mPayType Then .Text = ""

                                .Col = ColBillDate
                                '.Text = IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value)
                                .Text = RsTempPRDetail.Fields("BillDate").Value

                                .Col = ColBillAmount
                                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BillAMT").Value), 0, RsTempPRDetail.Fields("BillAMT").Value)))

                                .Col = ColTDSAmount
                                .Text = "0.00"

                                .Col = ColInterestAmount
                                .Text = "0.00"

                                .Col = ColBillAmountDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)
                                mDrCr = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)
                                .Col = ColBalance
                                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value)))
                                .Col = ColBalanceDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)


                                .Col = ColAmount
                                .Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                                .Col = ColDC
                                .Text = IIf(mFileAmount > 0, "DR", "CR") ''RsTempPRDetail.Fields("PAYDC").Value

                                .Col = ColOldBillNo
                                .Text = (IIf(IsDBNull(RsTempPRDetail.Fields("OldBillNo").Value), "", RsTempPRDetail.Fields("OldBillNo").Value))

                                .Col = ColOldAmount
                                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("OldAmount").Value), 0, RsTempPRDetail.Fields("OldAmount").Value)))
                                'mOldAmount = .Text

                                .Col = ColOldDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("OldDC").Value), "D", RsTempPRDetail.Fields("OldDC").Value)
                                'mOldDC = .Text

                                .Col = ColTaxableAmount
                                .Text = "0.00"

                                .Col = ColDueDate
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)

                                '                        If mFileTrnType = "O" Or mFileTrnType = "A" Then
                                '                            mDivCode = Val(lblDivisionCode.text)
                                '                        Else
                                '                            mDivCode = GetDivisionCode(IIf(IsNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), RsTempPRDetail.Fields("BillDate").Value, lblAccountCode.text)
                                '                        End If

                                SprdMain.Col = ColDivCode
                                SprdMain.Text = Str(mDivCode)

                                .Row = .MaxRows
                                .Col = ColPayType
                                .Text = GetPayType(mPayType)

                                .Col = ColPayType
                                If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then

                                Else
                                    MainClass.ProtectCell(SprdMain, .Row, .Row, ColDC, ColDC)
                                End If
                                .Col = ColPayType
                                ProtectUnProtectCell(VB.Left(.Text, 1), .Row)

                                .Col = ColBillAmountDC
                                If UCase(mDrCr) = UCase(lblDC.Text) Then
                                    .Row = .Row
                                    .Row2 = .Row
                                    .Col = 1
                                    .Col2 = .MaxCols
                                    .BlockMode = True
                                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                                    .BlockMode = False
                                End If

                                SprdMain.Col = ColBillCheck
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                                .MaxRows = .MaxRows + 1
                                '                        FormatSprdMain -1, False
                                RsTempPRDetail.MoveNext()
                            Loop
                        End With
                    End If
NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        CalcTots()
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColDivCode)

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC
        If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        '    End With
        cmdPopulate.Enabled = False
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdPopFromFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPopFromFile.Click
        On Error GoTo ErrPart
        Dim strFilePath As String
        Dim mFormat As String

        ' Let user locate the Excel file.
        '

        DataLoading = True
        strFilePath = My.Application.Info.DirectoryPath

        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        '
        ' Load it into the grid.

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mFormat = InputBox("Press 1 for TVS, 2 for HHML Format : ", "Format", "")

            If Val(mFormat) = 1 Then
                Call PopulateFromXLSTVSNewFile(strFilePath)
            ElseIf Val(mFormat) = 2 Then
                Call PopulateFromXLSHHMLFile(strFilePath)
            End If

        ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            mFormat = InputBox("Press 1 for Honda, 2 for Honda SPD, 3 For Yamaha & 4 For JCB Format : ", "Format", "")

            Call PopulateFromFile_RR(strFilePath, mFormat)


        Else
            mFormat = InputBox("Press 1 for Format : ", "Format", "")
        End If




        DataLoading = False
NormalExit:
        Exit Sub
ErrPart:
    End Sub
    Private Sub PopulateFromFile_RR(ByVal strXLSFile As String, ByRef mFormat As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""

        Dim mSqlStr As String
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim mFileBillNo As String
        Dim mFileTrnType As String
        Dim mFileAmount As Double
        Dim mFileAmountStr As String

        Dim mFileTDSAmount As Double
        Dim mFileTDSAmountStr As String

        Dim mFileDiscAmount As Double
        Dim mFileDiscAmountStr As String


        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim xAccountCode As String
        Dim xAccountAlias As String
        Dim mFileBillDate As String = ""
        Dim mFileBillFromDate As String = ""
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mLocationID As String
        Dim mOnAccountPayment As Double = 0
        Dim mBillBalAmount As Double = 0
        Dim mBillBalDC As String
        Dim mPaymentPostAmount As Double = 0
        Dim mPaymentBalAmount As Double = 0

        Dim FPath As String

        Dim ErrorFile As System.IO.StreamWriter

        Dim mBillNoField As Integer
        Dim mBillDateField As Integer
        Dim mTDSField As Integer
        Dim mDiscountField As Integer
        Dim mPayAmountField As Integer

        FPath = mPubBarCodePath & "\BillImportError.txt"

        If FILEExists(FPath) Then
            Kill(FPath)
        End If

        ErrorFile = My.Computer.FileSystem.OpenTextFileWriter(FPath, True)

        mFileBillFromDate = "01/04/2022"
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1, False)
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayType, SprdMain.MaxCols)

        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF

                    If mFormat = 1 Then
                        mBillNoField = 0
                        mBillDateField = 1
                        mTDSField = 2
                        mDiscountField = 5
                        mPayAmountField = 9
                    ElseIf mFormat = 2 Then
                        mBillNoField = 1
                        mBillDateField = 0
                        mTDSField = -1
                        mDiscountField = 4
                        mPayAmountField = 5
                    ElseIf mFormat = 3 Then
                        mBillNoField = 2
                        mBillDateField = 3
                        mTDSField = -1
                        mDiscountField = -1
                        mPayAmountField = 7
                    ElseIf mFormat = 4 Then
                        mBillNoField = 0
                        mBillDateField = 1
                        mTDSField = -1
                        mDiscountField = -1
                        mPayAmountField = 3
                    Else
                        Exit Sub
                    End If
                    If mBillNoField = -1 Then
                        mFileBillNo = ""
                    Else
                        mFileBillNo = IIf(IsDBNull(RsFile.Fields(mBillNoField).Value), "", RsFile.Fields(mBillNoField).Value)
                    End If

                    If mBillDateField = -1 Then
                        mFileBillDate = ""
                    Else
                        mFileBillDate = VB6.Format(IIf(IsDBNull(RsFile.Fields(mBillDateField).Value), "", Replace(RsFile.Fields(mBillDateField).Value, ".", "/")), "DD/MM/YYYY")
                    End If

                    If mPayAmountField = -1 Then
                        mFileAmountStr = 0
                    Else
                        mFileAmountStr = IIf(IsDBNull(RsFile.Fields(mPayAmountField).Value), "", RsFile.Fields(mPayAmountField).Value)
                    End If
                    mFileAmount = Val(mFileAmountStr)

                    If mTDSField = -1 Then
                        mFileTDSAmountStr = 0
                    Else
                        mFileTDSAmountStr = IIf(IsDBNull(RsFile.Fields(mTDSField).Value), "", RsFile.Fields(mTDSField).Value)
                    End If
                    mFileTDSAmount = Val(mFileTDSAmountStr)

                    If mDiscountField = -1 Then
                        mFileDiscAmountStr = 0
                    Else
                        mFileDiscAmountStr = IIf(IsDBNull(RsFile.Fields(mDiscountField).Value), "", RsFile.Fields(mDiscountField).Value)
                    End If

                    mFileDiscAmount = Val(mFileDiscAmountStr)


                    If MainClass.ValidateWithMasterTable(mFileBillNo, "BILLNO", "BILLNO", "FIN_POSTED_TRN", PubDBCn, MasterNo, , "") = True Then   ''INVOICE_DATE=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')
                        mFileTrnType = "B"
                    Else
                        'If mPaymentBalAmount <> 0 Then
                        'ErrorFile.WriteLine("Mismatch Bill No : " & mFileBillNo & " AMOUNT : " & mFileAmount)
                        'End If
                        mFileTrnType = "N"
                        mFileBillNo = mFileBillNo       ''"ON ACCOUNT"

                    End If

                    mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
                    mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
                    mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                    mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

                    mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""

                    If mFileTrnType = "O" Or mFileTrnType = "A" Or mFileTrnType = "N" Then
                        mDivCode = Val(lblDivisionCode.Text)
                    Else
                        mDivCode = GetDivisionCode(mFileBillNo, mFileBillDate, (lblAccountCode.Text))
                    End If
                    If mDivCode <> Val(lblDivisionCode.Text) Then GoTo NextRecord

                    If (mFileTrnType = "O" Or mFileTrnType = "N") And mFileAmount <> 0 Then

                        SprdMain.Row = SprdMain.MaxRows

                        SprdMain.Col = ColPayType
                        mPayType = mFileTrnType

                        SprdMain.Col = ColLocationId
                        SprdMain.Text = mLocationID

                        SprdMain.Col = ColTDSAmount
                        SprdMain.Text = VB6.Format(System.Math.Abs(mFileTDSAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                        SprdMain.Col = ColInterestAmount
                        SprdMain.Text = VB6.Format(System.Math.Abs(mFileDiscAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                        SprdMain.Col = ColAmount
                        SprdMain.Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                        SprdMain.Col = ColDC
                        SprdMain.Text = IIf(mFileAmount > 0, "CR", "DR")                            ''RsTempPRDetail.Fields("PAYDC").Value

                        SprdMain.Col = ColTaxableAmount
                        SprdMain.Text = "0.00"

                        SprdMain.Col = ColDueDate
                        SprdMain.Text = mFileBillDate

                        SprdMain.Col = ColDivCode
                        SprdMain.Text = Str(mDivCode)

                        SprdMain.Row = SprdMain.MaxRows
                        SprdMain.Col = ColPayType
                        SprdMain.Text = GetPayType(mFileTrnType)

                        SprdMain.Col = ColBillNo
                        SprdMain.Text = mFileBillNo


                        SprdMain.Col = ColPayType
                        If VB.Left(SprdMain.Text, 1) = "O" Or VB.Left(SprdMain.Text, 1) = "A" Then

                        Else
                            MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColDC, ColDC)
                        End If
                        SprdMain.Col = ColPayType
                        ProtectUnProtectCell(VB.Left(SprdMain.Text, 1), (SprdMain.Row))

                        SprdMain.Col = ColBillAmountDC
                        If UCase(mDrCr) = UCase(lblDC.Text) Then
                            SprdMain.Row = SprdMain.Row
                            SprdMain.Row2 = SprdMain.Row
                            SprdMain.Col = 1
                            SprdMain.Col2 = SprdMain.MaxCols
                            SprdMain.BlockMode = True
                            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                            SprdMain.BlockMode = False
                        End If

                        SprdMain.Col = ColBillCheck
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                    Else
                        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

                        SqlStr = " Select COMPANY_CODE, CASE WHEN BillNo='ON ACCOUNT' THEN 'O' WHEN BillNo='ADVANCE' THEN 'A' ELSE  'B' END AS TRNTYPE,BillNo,BillDate, " & vbCrLf _
                            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
                            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BILLDC , " & vbCrLf _
                            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
                            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS BALDC, " & vbCrLf _
                            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS Amount, " & vbCrLf _
                            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf _
                            & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo,Min(EXPDATE) AS DUEDATE  " & vbCrLf _
                            & " FROM FIN_POSTED_TRN  " & vbCrLf _
                            & " WHERE " & vbCrLf _
                            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'"

                        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
                        Else
                            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                        End If

                        SqlStr = SqlStr & vbCrLf & " And BillNo ='" & MainClass.AllowSingleQuote(mFileBillNo) & "'"
                        'SqlStr = SqlStr & vbCrLf & " AND BILLDATE >=TO_DATE('" & VB6.Format(mFileBillFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        'If IsDate(mFileBillDate) Then
                        '    SqlStr = SqlStr & vbCrLf & " AND BILLDATE <=TO_DATE('" & VB6.Format(mFileBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                        'End If

                        SqlStr = SqlStr & vbCrLf & " GROUP BY COMPANY_CODE, BillNo, BillDate"

                        'SqlStr = SqlStr & vbCrLf _
                        '    & " HAVING " & mBalAmtStr & " <>0 "


                        SqlStr = SqlStr & vbCrLf & " ORDER BY BillDate, BillNo "

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTempPRDetail.EOF = True Then GoTo NextRecord


                        With SprdMain
                            Do While RsTempPRDetail.EOF = False
                                .Row = .MaxRows

                                .Col = ColPayType
                                mPayType = RsTempPRDetail.Fields("TRNTYPE").Value

                                SprdMain.Col = ColLocationId
                                SprdMain.Text = mLocationID

                                .Col = ColBillNo
                                .Text = IIf(mPayType = "O" Or mPayType = "A", "", IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value))

                                If .Text = mPayType Then .Text = ""

                                mCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTempPRDetail.Fields("COMPANY_CODE").Value)  '' GetCompanyCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text))


                                .Col = ColBillCompany
                                .Text = GetCompanyName(mCompanyCode, "S")

                                mLocCode = GetLocationCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text), mCompanyCode, mPayType)

                                .Col = ColBillDate
                                '.Text = IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value)
                                .Text = RsTempPRDetail.Fields("BillDate").Value

                                .Col = ColBillAmount
                                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BillAMT").Value), 0, RsTempPRDetail.Fields("BillAMT").Value)))


                                .Col = ColBillAmountDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)
                                mDrCr = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDC").Value), "", RsTempPRDetail.Fields("BILLDC").Value)

                                .Col = ColBalance
                                .Text = CStr(Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value)))
                                mBillBalAmount = Val(IIf(IsDBNull(RsTempPRDetail.Fields("BALANCE").Value), 0, RsTempPRDetail.Fields("BALANCE").Value))

                                .Col = ColBalanceDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)
                                mBillBalDC = IIf(IsDBNull(RsTempPRDetail.Fields("BALDC").Value), "", RsTempPRDetail.Fields("BALDC").Value)
                                mBillBalAmount = mBillBalAmount * IIf(mBillBalDC = "D", -1, 1)

                                .Col = ColLocationId
                                .Text = mLocCode

                                'Dim mPaymentPOstAmount As Double
                                'Dim mPaymentBalAmount As Double

                                SprdMain.Col = ColTDSAmount
                                SprdMain.Text = VB6.Format(System.Math.Abs(mFileTDSAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                                SprdMain.Col = ColInterestAmount
                                SprdMain.Text = VB6.Format(System.Math.Abs(mFileDiscAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)


                                mPaymentPostAmount = mFileAmount '' IIf(mFileAmount > mBillBalAmount, mBillBalAmount, mFileAmount)
                                mPaymentBalAmount = 0   ''IIf(mFileAmount > mBillBalAmount, VB6.Format(mFileAmount - mBillBalAmount, "0.00"), 0)      ''

                                mOnAccountPayment = mOnAccountPayment + mPaymentBalAmount

                                'If mPaymentBalAmount <> 0 Then
                                '    ErrorFile.WriteLine(.MaxRows & " Bill No : " & IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value) & " EXCESS AMOUNT : " & mPaymentBalAmount)
                                'End If

                                .Col = ColAmount
                                .Text = VB6.Format(System.Math.Abs(mPaymentPostAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                                'If System.Math.Abs(mBillBalAmount) > System.Math.Abs(mPaymentPostAmount) Then
                                '    ErrorFile.WriteLine(.MaxRows & " Bill No : " & IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value) & " SHORT AMOUNT : " & System.Math.Abs(mBillBalAmount) - System.Math.Abs(mPaymentPostAmount))
                                'End If

                                .Col = ColDC
                                .Text = IIf(mFileAmount > 0, "CR", "DR")           '' "CR" '                     ''RsTempPRDetail.Fields("PAYDC").Value

                                .Col = ColOldBillNo
                                .Text = (IIf(IsDBNull(RsTempPRDetail.Fields("OldBillNo").Value), "", RsTempPRDetail.Fields("OldBillNo").Value))

                                .Col = ColOldAmount
                                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("OldAmount").Value), 0, RsTempPRDetail.Fields("OldAmount").Value)))
                                'mOldAmount = .Text

                                .Col = ColOldDC
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("OldDC").Value), "D", RsTempPRDetail.Fields("OldDC").Value)
                                'mOldDC = .Text

                                .Col = ColTaxableAmount
                                .Text = "0.00"

                                .Col = ColDueDate
                                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)

                                SprdMain.Col = ColDivCode
                                SprdMain.Text = Str(mDivCode)

                                .Row = .MaxRows
                                .Col = ColPayType
                                .Text = GetPayType(mPayType)

                                .Col = ColPayType
                                If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then

                                Else
                                    MainClass.ProtectCell(SprdMain, .Row, .Row, ColDC, ColDC)
                                End If
                                .Col = ColPayType
                                ProtectUnProtectCell(VB.Left(.Text, 1), .Row)

                                .Col = ColBillAmountDC
                                If UCase(mDrCr) = UCase(lblDC.Text) Then
                                    .Row = .Row
                                    .Row2 = .Row
                                    .Col = 1
                                    .Col2 = .MaxCols
                                    .BlockMode = True
                                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                                    .BlockMode = False
                                End If

                                SprdMain.Col = ColBillCheck
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)



                                .MaxRows = .MaxRows + 1
                                '                        FormatSprdMain -1, False
                                RsTempPRDetail.MoveNext()
                            Loop
                        End With
                    End If
NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If mOnAccountPayment > 0 Then
            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColPayType
            mPayType = "O"

            SprdMain.Col = ColLocationId
            SprdMain.Text = mLocationID

            SprdMain.Col = ColAmount
            SprdMain.Text = VB6.Format(System.Math.Abs(mOnAccountPayment), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

            SprdMain.Col = ColDC
            SprdMain.Text = IIf(mOnAccountPayment > 0, "CR", "DR")                            ''RsTempPRDetail.Fields("PAYDC").Value

            SprdMain.Col = ColTaxableAmount
            SprdMain.Text = "0.00"

            SprdMain.Col = ColDueDate
            SprdMain.Text = mFileBillDate

            SprdMain.Col = ColDivCode
            SprdMain.Text = Str(mDivCode)

            SprdMain.Row = SprdMain.MaxRows
            SprdMain.Col = ColPayType
            SprdMain.Text = GetPayType("O")

            SprdMain.Col = ColPayType
            If VB.Left(SprdMain.Text, 1) = "O" Or VB.Left(SprdMain.Text, 1) = "A" Then

            Else
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColDC, ColDC)
            End If
            SprdMain.Col = ColPayType
            ProtectUnProtectCell(VB.Left(SprdMain.Text, 1), (SprdMain.Row))

            SprdMain.Col = ColBillAmountDC
            mDrCr = Trim(SprdMain.Text)
            If UCase(mDrCr) = UCase(lblDC.Text) Then
                SprdMain.Row = SprdMain.Row
                SprdMain.Row2 = SprdMain.Row
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                SprdMain.BlockMode = False
            End If

            SprdMain.Col = ColBillCheck
            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

            SprdMain.MaxRows = SprdMain.MaxRows + 1

        End If
        'SetSprdCellFormat()
        CalcTots()

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColDivCode)
        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
            '        FileDBCn = ""
        End If

        strTemp = ""
        strXLSFile = ""

        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC
        If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        '    End With


        cmdPopulate.Enabled = False

        ErrorFile.Close()

        If FILEExists(FPath) Then
            Process.Start("notepad.exe", FPath)            ''Process.Start("explorer.exe", FPath)
        End If


        Exit Sub
ErrPart:
        ErrorFile.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""

        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mSearchCompanyCode As Double



        If SprdMain.MaxRows > 1 Then
            If MsgQuestion("Data Already in Grid Want to clear and Continue ? ") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1, True)

        DataLoading = True

        If Trim(txtDefaultCompanyName.Text) = "" Then
            mSearchCompanyCode = -1
        Else
            If MainClass.ValidateWithMasterTable(Trim(txtDefaultCompanyName.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mSearchCompanyCode = MasterNo
            Else
                mSearchCompanyCode = -1
            End If
        End If
        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        ''GETDIVISIONCODE()
        ''CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN TO_DATE('" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "') ELSE
        ''  ''BillNo='ON ACCOUNT' THEN 'ON ACCOUNT' WHEN BillNo='ADVANCE' THEN 'ADVANCE' ELSE  'BILL' END DECODE(TRNTYPE='O', VNO,BillNo) AS
        SqlStr = " Select CASE WHEN BillNo='ON ACCOUNT' THEN 'ON ACCOUNT' WHEN BillNo='ADVANCE' THEN 'ADVANCE' ELSE  'BILL' END AS TRNTYPE," & vbCrLf _
            & " '0' as BillCheck," & vbCrLf _
            & " CASE WHEN TRNTYPE='A' OR TRNTYPE='O' THEN '' ELSE BillNo END AS BillNo," & vbCrLf _
            & " BillDate, MAX(LOCATION_ID) AS LOCATION_ID, " & vbCrLf _
            & " ABS(" & mBillAmtStr & ") AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEN 'DR' ELSE 'CR' END AS BILLDC ,  " & vbCrLf _
            & " ABS(" & mBalAmtStr & ") AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEN 'DR' ELSE 'CR' END AS BALDC," & vbCrLf _
            & " 0 AS TDSAmount,0 AS DiscAmount, " & vbCrLf _
            & " ABS(" & mBalAmtStr & ") AS Amount, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEN 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf _
            & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo, " & vbCrLf _
            & " '', 0, CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN DIV_CODE ELSE GetDivisionCode(TRN.COMPANY_CODE,TRN.FYEAR, BillNo, TRN.AccountCode) END AS DIV_CODE, " & vbCrLf _
            & " CMST.COMPANY_SHORTNAME, '', " & vbCrLf _
            & " Min(EXPDATE) AS DUEDATE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, GEN_COMPANY_MST CMST  " & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND TRN.AccountCode = '" & MainClass.AllowSingleQuote((lblAccountCode.Text)) & "'"


        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
            If mSearchCompanyCode > 0 Then
                SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & mSearchCompanyCode & ""
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""

        If optAsPerBill.Checked = True Then
            If IsDate(txtBillSearchFrom.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND BILLDATE >=TO_DATE('" & VB6.Format(txtBillSearchFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If IsDate(txtDate.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND BILLDATE <=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        Else
            If IsDate(txtBillSearchFrom.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND EXPDATE >=TO_DATE('" & VB6.Format(txtBillSearchFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If IsDate(txtDate.Text) = True Then
                SqlStr = SqlStr & vbCrLf & " AND EXPDATE <=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If
        '    SqlStr = SqlStr & vbCrLf & " AND BILLNO='6959'"

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY CMST.COMPANY_SHORTNAME, TRN.COMPANY_CODE,CASE WHEN BillNo='ON ACCOUNT' THEN 'ON ACCOUNT' WHEN BillNo='ADVANCE' THEN 'ADVANCE' ELSE  'BILL' END,CASE WHEN TRNTYPE='A' OR TRNTYPE='O' THEN '' ELSE BillNo END ,BillDATE,CASE WHEN BillNo='ON ACCOUNT' OR BillNo='ADVANCE' THEN DIV_CODE ELSE GetDivisionCode(TRN.COMPANY_CODE,TRN.FYEAR, BillNo, TRN.AccountCode) END " & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf _
            & " ORDER BY BillDate, BillNo "


        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1, True)

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        MainClass.AddBlankSprdRow(SprdMain, ColBillNo, ConRowHeight)

        FormatSprdMain(-1, True)
        SetSprdCellFormat()

        CalcTots()
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColDivCode)

        If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)

        DataLoading = False
    End Sub
    Private Sub cmdToken_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdToken.Click
        'Dim strFilePath As String
        Dim mFormat As String


        mFormat = InputBox("Enter Token No :   ", "Token No", "")
        DataLoading = True
        Call PopulateFromToken(Val(mFormat))
        DataLoading = False

NormalExit:
    End Sub

    Private Sub frmPaymentDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If MainClass.ValidateWithMasterTable((lblAccountName.Text), "SUPP_CUST_NAME", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblAccountCode.Text = MasterNo
        Else
            ErrorMsg("Invalid Account Name", "", MsgBoxStyle.Information)
        End If

        If FormLoaded = False Then
            FormatSprdMain(-1, False)
            optShow(1).Checked = True
            Show1()
            txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            txtBillSearchFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
            FormLoaded = True
        End If
    End Sub

    Private Sub frmPaymentDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        'Call SetChildFormCordinate(Me)

        MainClass.SetControlsColor(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        ADDMode = False
        MODIFYMode = False
        FormLoaded = False


        XRIGHT = "AMD"
        FormatSprdMain(-1, False)
        MainClass.SetControlsColor(Me)
        DataLoading = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBalance As Double
        Dim mPayType As String
        Dim mBillAmount As Double
        Dim mPaymentAmt As Double
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mDC As String
        Dim mPRAmount As Double
        Dim mCompanyCode As Long
        Dim mCompanyName As String

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        SqlStr = "Select * From FIN_TEMPBILL_TRN " & vbCrLf _
            & " Where UserID='" & PubUserID & "' AND TEMPMKEY=" & Val(lblTempProcessKey.Text) & "" & vbCrLf _
            & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "' " & vbCrLf _
            & " AND TRNDTLSUBROWNO=" & Val(lblTrnRowNo.Text) & "" & vbCrLf _
            & " AND BookType='" & UCase(Trim(lblBookType.Text)) & "'" & vbCrLf _
            & " ORDER BY BILLDATE,BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempPRDetail.EOF = True Then Exit Sub
        cmdPopulate.Enabled = False
        CmdPopFromFile.Enabled = True       ''IIf(PubUserID = "G0416", True, False)

        FormatSprdMain(-1, False)
        With SprdMain


            Do While RsTempPRDetail.EOF = False
                .Row = .MaxRows

                .Col = ColPayType
                mPayType = IIf(IsDBNull(RsTempPRDetail.Fields("TRNTYPE").Value), "O", RsTempPRDetail.Fields("TRNTYPE").Value)

                .Col = ColBillCheck
                .Value = CStr(System.Windows.Forms.CheckState.Checked)

                .Row = .MaxRows
                .Col = ColBillNo
                mBillNo = IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value)
                .Text = IIf(mPayType = "A", "", RsTempPRDetail.Fields("BILLNO").Value) ''IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BILLNO").Value)

                If .Text = mPayType Then .Text = ""

                .Col = ColBillDate
                mBillDate = IIf(IsDBNull(RsTempPRDetail.Fields("BillDate").Value), "", VB6.Format(RsTempPRDetail.Fields("BillDate").Value, "dd/mm/yyyy"))
                .Text = IIf(mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value) ''IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value)

                .Col = ColTDSAmount
                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("TDS_AMOUNT").Value), 0, RsTempPRDetail.Fields("TDS_AMOUNT").Value)))

                .Col = ColInterestAmount
                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("INTEREST_AMOUNT").Value), 0, RsTempPRDetail.Fields("INTEREST_AMOUNT").Value)))



                .Col = ColAmount
                mPRAmount = Val(RsTempPRDetail.Fields("Amount").Value)
                .Text = Str(mPRAmount)

                .Col = ColDC
                .Text = IIf(RsTempPRDetail.Fields("DC").Value = "D", "Dr", "Cr")

                .Col = ColOldBillNo
                .Text = (IIf(IsDBNull(RsTempPRDetail.Fields("OldBillNo").Value), "", RsTempPRDetail.Fields("OldBillNo").Value))

                .Col = ColOldAmount
                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("OldAmount").Value), 0, RsTempPRDetail.Fields("OldAmount").Value)))
                'mOldAmount = .Text

                .Col = ColOldDC
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("OldDC").Value), "D", RsTempPRDetail.Fields("OldDC").Value)
                'mOldDC = .Text

                .Col = ColRefNo
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("REF_NO").Value), "", RsTempPRDetail.Fields("REF_NO").Value)

                .Col = ColTaxableAmount
                .Text = Str(Val(IIf(IsDBNull(RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value), 0, RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value)))

                .Col = ColPONo
                If IsDBNull(RsTempPRDetail.Fields("PONO").Value) Then
                    .Text = ""
                Else
                    .Text = Str(IIf(IsDBNull(RsTempPRDetail.Fields("PONO").Value), "", RsTempPRDetail.Fields("PONO").Value))
                End If

                .Col = ColDivCode
                If IsDBNull(RsTempPRDetail.Fields("DIV_CODE").Value) Then
                    .Text = Str(CDbl(lblDivisionCode.Text))
                Else
                    .Text = Str(IIf(IsDBNull(RsTempPRDetail.Fields("DIV_CODE").Value), 1, RsTempPRDetail.Fields("DIV_CODE").Value))
                End If

                .Col = ColLocationId
                If IsDBNull(RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value) Then
                    .Text = GetDefaultLocation(lblAccountCode.Text)
                Else
                    .Text = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value), "", RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value)
                End If


                mCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value)
                mCompanyName = GetCompanyName(mCompanyCode, "S")
                .Row = .MaxRows
                .Col = ColBillCompany
                .Text = mCompanyName


                .Col = ColDueDate
                .Text = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)

                'Get Balance Amount
                Call GetBalanceAmount(.Row, (lblAccountCode.Text), mBillNo, mBillDate, mPayType)



                .Row = .MaxRows
                .Col = ColPayType
                .Text = GetPayType(mPayType)

                If VB.Left(.Text, 1) = "O" Or VB.Left(.Text, 1) = "A" Then
                    .Col = ColBillAmount
                    .Text = ""
                    .Col = ColBalance
                    .Text = ""
                End If

                ProtectUnProtectCell(VB.Left(mPayType, 1), .MaxRows)

                .MaxRows = .MaxRows + 1

                RsTempPRDetail.MoveNext()
            Loop
            '        ProtectUnProtectCell Left(mPayType, 1), -1
            'SetSprdCellFormat()
            CalcTots()

            If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub PickUpBillPayment(ByRef mPayType As String, ByRef mBillNo As String, ByRef mOldAmount As Double, ByRef mOldDC As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        SqlStr = " Select * From FIN_TEMPBILL_TRN " & vbCrLf _
            & " Where UserID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMPMKEY=" & Val(lblTempProcessKey.Text) & "" & vbCrLf _
            & " AND AccountCode = '" & Val(lblAccountCode.Text) & "' " & vbCrLf _
            & " AND TRNType='" & mPayType & "'" & vbCrLf _
            & " AND BillNo='" & mBillNo & "'" & vbCrLf _
            & " AND BookType='" & UCase(Trim(lblBookType.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        With SprdMain
            If RS.EOF = True Then
                mOldAmount = 0
                mOldDC = "D"
            Else
                mOldAmount = RS.Fields("OldAmount").Value
                mOldDC = IIf(IsDBNull(RS.Fields("OldDC").Value), "D", RS.Fields("OldDC").Value)
            End If
        End With
        RS.Close()
        Exit Sub
ERR1:
        RS.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef mFromPopulate As Boolean)

        On Error GoTo ErrPart
        Dim RsTRN As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT * FROM FIN_POSTED_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRN, ADODB.LockTypeEnum.adLockReadOnly)


        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = 0
            .set_ColWidth(0, 3)

            .Col = ColPayType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .set_ColUserSortIndicator(ColPayType, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorDescending)

            .set_ColWidth(.Col, 10)
            .TypeEditLen = 10

            'If mFromPopulate = False Then
            '    .Text = "Bill"
            'End If

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTRN.Fields("BillNo").DefinedSize ''
            .set_ColWidth(.Col, 12)

            .ColsFrozen = ColBillNo


            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 8)

            .Col = ColLocationId
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTRN.Fields("LOCATION_ID").DefinedSize ''
            .set_ColWidth(.Col, 10)

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColTDSAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColInterestAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)


            .Col = ColBillAmountDC
            .CellType = SS_CELL_TYPE_EDIT
            If mFromPopulate = False Then
                .Text = IIf(VB.Left(lblDC.Text, 1) = "C", "Cr", "Dr")
            End If
            .set_ColWidth(.Col, 3)

            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8.25)

            .Col = ColBalanceDC
            .CellType = SS_CELL_TYPE_EDIT
            If mFromPopulate = False Then
                .Text = IIf(VB.Left(lblDC.Text, 1) = "C", "Cr", "Dr")
            End If
            .set_ColWidth(.Col, 3)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8.5)

            .Col = ColDC
            .CellType = SS_CELL_TYPE_EDIT
            If mFromPopulate = False Then
                .Text = IIf(VB.Left(lblDC.Text, 1) = "C", "Cr", "Dr")
            End If
            .set_ColWidth(.Col, 3)

            .Col = ColOldBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColOldAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColOldDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = True


            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTRN.Fields("BillNo").DefinedSize ''
            .set_ColWidth(.Col, 7)

            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 6)
            .ColHidden = False

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 12
            .ColHidden = False

            .Col = ColDivCode
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 2.5)
            .ColHidden = False

            .Col = ColDueDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .ColHidden = False
            .set_ColWidth(.Col, 8)

            .Col = ColBillCheck
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 2)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Col = ColBillCompany
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("COMPANY_SHORTNAME", "GEN_COMPANY_MST", PubDBCn)
            .set_ColWidth(.Col, 15)
            .ColHidden = False



            FillHeading()
            .Row = Arow
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColBillAmount, ColBalance)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDivCode, ColDivCode)
            'MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCompanyCode, ColCompanyCode)
            MainClass.SetSpreadColor(SprdMain, -1)

            If mFromPopulate = False Then
                .Row = IIf(Arow = -1, 1, Arow)
                .Col = ColPayType
                .Text = IIf(.Text = "", "Bill", "")
            End If

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub


    Private Sub ProtectUnProtectCell(ByRef mPayType As String, ByRef mRow As Integer)

        Dim mUnCol1 As Integer
        Dim mUnCol2 As Integer
        Dim mCol1 As Integer
        Dim mCol2 As Integer

        Dim mCol3 As Integer
        Dim mCol4 As Integer

        If mPayType = "B" Or mPayType = "D" Or mPayType = "C" Or mPayType = "T" Then
            mUnCol1 = ColBillNo
            mUnCol2 = ColBillNo
            mCol1 = ColBillDate
            mCol2 = ColBillDate

            mCol3 = ColBillAmount
            mCol4 = ColBalanceDC

        ElseIf mPayType = "N" Then
            mUnCol1 = ColBillNo
            mUnCol2 = ColBillDate
            mCol1 = ColBillAmount
            mCol2 = ColBalanceDC

            mCol3 = ColBillAmount
            mCol4 = ColBalanceDC
        ElseIf mPayType = "A" Or mPayType = "O" Then
            mUnCol1 = ColPayType
            mUnCol2 = ColPayType
            mCol1 = ColBillNo
            mCol2 = ColBillDate

            mCol3 = ColBillAmount
            mCol4 = ColBalanceDC
        End If

        MainClass.UnProtectCell(SprdMain, mRow, mRow, ColPayType, SprdMain.MaxCols)

        '    MainClass.UnProtectCell SprdMain, mRow, mRow, ColBillNo, SprdMain.MaxCols
        '    MainClass.UnProtectCell SprdMain, mRow, mRow, mUnCol1, mUnCol2
        If mPayType = "A" Or mPayType = "N" Or mPayType = "O" Then
            '        MainClass.UnProtectCell SprdMain, mRow, mRow, ColDC, ColDC
        Else
            MainClass.ProtectCell(SprdMain, mRow, mRow, ColDC, ColDC)
        End If

        If mPayType = "O" Then
            MainClass.ProtectCell(SprdMain, mRow, mRow, ColBillNo, ColBillNo)
            MainClass.ProtectCell(SprdMain, mRow, mRow, ColBillAmount, ColBalanceDC)
        Else
            MainClass.ProtectCell(SprdMain, mRow, mRow, mCol1, mCol2)
            MainClass.ProtectCell(SprdMain, mRow, mRow, mCol3, mCol4)
        End If

        MainClass.ProtectCell(SprdMain, mRow, mRow, ColDivCode, ColDivCode)


        '    If mPayType = "A" Then
        '        MainClass.UnProtectCell SprdMain, mRow, mRow, ColBillDate, ColBillDate
        '    End If

    End Sub

    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mNetAmt As Double
        Dim MTotalAmt As Double
        Dim cntRow As Integer
        Dim mDC As String
        Dim mDrCr As String = ""

        With SprdMain
            For cntRow = 1 To .MaxRows - 1 Step 1
                .Row = cntRow

                .Col = ColBillCheck
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColDC
                    mDC = VB.Left(.Text, 1)

                    .Col = ColAmount
                    If mDC = "D" Then
                        mDAmt = mDAmt + Val(.Value)
                    Else
                        mCAmt = mCAmt + Val(.Value)
                    End If

                    mNetAmt = System.Math.Abs(mCAmt - mDAmt)
                End If
NextRow:
            Next cntRow
        End With

        LblDrAmt.Text = VB6.Format(mDAmt, "0.00")
        LblCrAmt.Text = VB6.Format(mCAmt, "0.00")
        LblNetAmt.Text = VB6.Format(mNetAmt, "0.00")
        lblNetDC.Text = IIf(mDAmt > mCAmt, "Dr", "Cr")
        lblAmount.Text = VB6.Format(Val(LblNetAmt.Text), "0.00")
        lblDiffAmt.Text = CStr(Val(lblAmount.Text) - Val(LblNetAmt.Text))
        lblDiffDC.Text = ""
ErrSprdTotal:
    End Sub
    Private Sub SetSprdCellFormat()
        On Error GoTo ErrSprdTotal

        Dim cntRow As Integer
        Dim mDrCr As String = ""

        With SprdMain
            For cntRow = 1 To .MaxRows - 1 Step 1
                .Row = cntRow

                .Col = ColPayType
                ProtectUnProtectCell(VB.Left(.Text, 1), .Row)

                .Col = ColBillAmountDC
                mDrCr = Trim(.Text)
                If UCase(mDrCr) = UCase(lblDC.Text) Then
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                    .BlockMode = False
                End If

            Next cntRow
        End With
ErrSprdTotal:
    End Sub
    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
            On Error GoTo ErrPart
            Dim cntRow As Integer
            Dim mBillDate As String

            If Not IsDate(txtDate.Text) Then Exit Sub

            DataLoading = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            FormLoaded = False
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    If Index = 0 Then
                        .Row = cntRow
                        .Col = ColBillDate
                        mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                        If IsDate(mBillDate) Then
                            .Col = ColBillCheck
                            If CDate(mBillDate) <= CDate(txtDate.Text) Then
                                .Value = CStr(System.Windows.Forms.CheckState.Checked)
                            Else
                                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                            End If
                        End If
                    ElseIf Index = 2 Then
                        .Row = cntRow
                        .Col = ColDueDate
                        mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                        If IsDate(mBillDate) Then
                            .Col = ColBillCheck
                            If CDate(mBillDate) <= CDate(txtDate.Text) Then
                                .Value = CStr(System.Windows.Forms.CheckState.Checked)
                            Else
                                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                            End If
                        End If
                    Else
                        .Row = cntRow
                        .Col = ColBillCheck
                        .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If
                Next
            End With
            FormLoaded = True
            DataLoading = False
            CalcTots()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
ErrPart:
            'Resume
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        If FormLoaded = True Then
            If DataLoading = False Then
                CalcTots()
            End If
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(frmAtrn.cmdSave, lblADDMode.Text, lblModifyMode.Text)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyCode As Long
        Dim mShortName As String

        Select Case eventArgs.col
            Case 0
                If eventArgs.row > 0 Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColBillNo)
                    MainClass.SaveStatus(Me.cmdOk, ADDMode, MODIFYMode)
                    CalcTots()
                    SprdMain.UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort
                End If
            Case ColBillNo
                If eventArgs.row = 0 Then
                    SearchBill()
                End If
            Case ColPONo
                If eventArgs.row = 0 Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColBillCompany
                    mShortName = Trim(SprdMain.Text)
                    If mShortName = "" Then Exit Sub
                    If MainClass.ValidateWithMasterTable(mShortName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = MasterNo
                    End If

                    SqlStr = "SELECT POMain.AUTO_KEY_PO  As AUTO_KEY_PO , POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE, SUM(PODETAIL.GROSS_AMT) AS GROSS_AMT " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail" & vbCrLf _
                        & " WHERE POMain.MKEY=PODetail.MKEY" & vbCrLf _
                        & " AND POMain.Company_Code=" & mCompanyCode & ""   '' AND PUR_TYPE IN ('P','R','L')"

                    'If Trim(lblAccountCode.Text) <> "" Then
                    '    If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '        xSuppCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(lblAccountCode.Text) & "'"
                    '    End If
                    'End If

                    'SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

                    'If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
                    '    SqlStr = SqlStr & vbCrLf & " AND POMain.ORDER_TYPE NOT IN " & vbCrLf & " CASE WHEN SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)<" & RsCompany.Fields("FYEAR").Value & " THEN ('O') ELSE ('-1') END"
                    'Else
                    '    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)>= " & vbCrLf & " CASE WHEN POMain.ORDER_TYPE='O' THEN " & ConOPENPO_CONTINOUS_YEAR & " ELSE 1 END"
                    'End If

                    'If IsDate(txtMRRDate.Text) Then
                    '    SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    'End If

                    'If ADDMode = True Then
                    SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND POMain.PO_CLOSED='N'"
                    'End If


                    'SqlStr = SqlStr & vbCrLf & "  AND PO_ITEM_STATUS='N' " & vbCrLf & " AND POMain.AUTO_KEY_PO Like '" & xPoNo & "%'"

                    SqlStr = SqlStr & vbCrLf & " GROUP BY POMain.AUTO_KEY_PO, POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE"

                    SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(POMain.AUTO_KEY_PO),POMain.PUR_ORD_DATE"

                    If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColPONo
                        SprdMain.Text = AcName
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPONo)
                    End If

                    'If MainClass.SearchGridMaster("", "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "PUR_ORD_DATE", , , "COMPANY_CODE=" & mCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "' AND PO_STATUS='Y'") = True Then
                    '    If AcName <> "" Then
                    '        SprdMain.Row = SprdMain.ActiveRow
                    '        SprdMain.Col = ColPONo
                    '        SprdMain.Text = AcName

                    '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPONo)
                    '    End If
                    'End If
                End If
            Case ColBillCompany
                If eventArgs.row = 0 Then
                    If MainClass.SearchGridMaster("", "GEN_COMPANY_MST", "COMPANY_SHORTNAME", "COMPANY_NAME", , , "") = True Then
                        If AcName <> "" Then
                            SprdMain.Row = SprdMain.ActiveRow
                            SprdMain.Col = ColBillCompany
                            SprdMain.Text = AcName

                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBillCompany)
                        End If
                    End If
                End If
            Case ColLocationId
                If eventArgs.row = 0 Then
                    SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                        & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'"

                    If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                        If AcName <> "" Then
                            SprdMain.Row = SprdMain.ActiveRow
                            SprdMain.Col = ColLocationId
                            SprdMain.Text = AcName

                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColLocationId)
                        End If
                    End If
                End If
        End Select
        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchBill()

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
            & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
            & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
            & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
            & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select BillNo, BillDate, LOCATION_ID," & vbCrLf _
            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC , " & vbCrLf _
            & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf _
            & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, MAX(DUEDATE) AS DUEDATE,COMPANY_CODE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & lblAccountCode.Text & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""

        SqlStr = SqlStr & vbCrLf & " GROUP BY  BillDate, BillNo,COMPANY_CODE,LOCATION_ID" & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf _
            & " ORDER BY BillDate, BillNo "

        MainClass.SearchGridMasterBySQL("", SqlStr)

        If AcName <> "" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColBillNo
            SprdMain.Text = AcName
            SprdMain.Col = ColBillDate
            SprdMain.Text = AcName1
            SprdMain.Col = ColLocationId
            SprdMain.Text = AcName2

            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBillNo)
        End If
        Exit Sub

        '        lblBillNo.text = ""
        '        frmViewOuts.txtName.Text = lblAccountName.text
        '        frmViewOuts.lblFromMenu.text = "No"
        '        frmViewOuts.txtDateTo = RunDate      ''lblVDate.text
        '
        '        frmViewOuts.CboCostC.Text = UCase(IIf(lblCostCName.text = "", "ALL", lblCostCName.text))
        '
        '        If RsCompany.Fields("Type").Value = "R" Then
        '            frmViewOuts.cboConsolidated.ListIndex = 1
        '        ElseIf RsCompany.Fields("Type").Value = "B" Then
        '            frmViewOuts.cboConsolidated.ListIndex = 2
        '        ElseIf RsCompany.Fields("Type").Value = "D" Then
        '            frmViewOuts.cboConsolidated.ListIndex = 3
        '        End If
        '        frmViewOuts.cmdShow = True
        '        frmViewOuts.Show 1
        '        frmViewOuts.lblBillNo.text = IIf(frmViewOuts.lblBillNo.text = "lblBillNo", "", frmViewOuts.lblBillNo.text)
        '
        '        If frmViewOuts.lblBillNo.text <> "" Then
        '            SprdMain.Col = ColBillNo
        '            SprdMain.Row = SprdMain.ActiveRow
        '            SprdMain.Text = frmViewOuts.lblBillNo.text
        '
        '            SprdMain.Col = ColBillDate
        '            SprdMain.Text = frmViewOuts.lblBillDate.text
        '
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColAmount
        '            Call SprdMain_LeaveCell(ColBillNo, SprdMain.ActiveRow, ColAmount, SprdMain.ActiveRow, False)
        '        End If
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mPayType As String
        Dim mActiveCol As Integer
        Dim mActiveRow As Integer

        mActiveCol = SprdMain.ActiveCol
        mActiveRow = SprdMain.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            'If mActiveCol = ColAmount Then
            '    SprdMain.Row = SprdMain.ActiveRow
            '    SprdMain.Col = ColAmount
            '    If Val(SprdMain.Text) <> 0 Then
            '        If SprdMain.MaxRows = SprdMain.ActiveRow Then
            '            SprdMain.Col = ColPayType
            '            mPayType = VB.Left(UCase(Trim(SprdMain.Text)), 1)
            '            ''MainClass.AddBlankSprdRow(SprdMain, IIf(mPayType = "B" Or mPayType = "N", ColBillNo, ColAmount), ConRowHeight)
            '            MainClass.AddBlankSprdRow(SprdMain, ColPayType, ConRowHeight)
            '            FormatSprdMain((SprdMain.MaxRows), False)
            '            '                Else
            '            'MainClass.SetFocusToCell(SprdMain, mActiveRow, ColDC)
            '        End If
            '    End If

            'End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If SprdMain.ActiveCol = ColBillNo Then SearchBill()
            If SprdMain.ActiveCol = ColBillCompany Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColBillCompany, 0))
        End If
        'eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        Dim mPayType As String
        Dim mBillNo As String
        Dim mAmount As Double
        Dim mBillDate As String
        Dim mDueDays As Double
        Dim mPayCode As String
        Dim mPONo As String
        Dim mAccountCode As String = ""
        Dim mPrevBillAmount As Double
        Dim mCurrBillAmount As Double
        Dim mPOAmount As Double
        Dim mCompanyCode As Long
        Dim mCurrCompanyCode As Long
        Dim mBillCompanyName As String

        If eventArgs.newRow = -1 Then Exit Sub


        SprdMain.Row = eventArgs.row

        SprdMain.Col = ColPayType
        mPayType = VB.Left(SprdMain.Text, 1)

        SprdMain.Col = ColBillNo
        mBillNo = SprdMain.Text

        SprdMain.Col = ColBillDate
        mBillDate = SprdMain.Text

        SprdMain.Col = ColBillCompany


        If mPayType = "B" Then
            mCompanyCode = GetCompanyCode(mBillNo, mBillDate, lblAccountCode.Text)       ' IIf(Val(SprdMain.Text) <= 0, RsCompany.Fields("COMPANY_CODE").Value, Val(SprdMain.Text))
        Else
            SprdMain.Col = ColBillCompany
            If Trim(SprdMain.Text) = "" Then
                mBillCompanyName = IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "-1", RsCompany.Fields("COMPANY_SHORTNAME").Value)
                SprdMain.Text = mBillCompanyName
            Else
                mBillCompanyName = Trim(SprdMain.Text)
            End If

            If MainClass.ValidateWithMasterTable(mBillCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mCompanyCode = MasterNo
            Else
                mCompanyCode = -1
            End If
        End If
        'mBillCompanyName = 1


        Dim mAccountName As String
        Select Case eventArgs.col
            Case ColPayType
                SprdMain.Row = eventArgs.row


                'Sandeep 'Set focus new Next Col
                'SprdMain.Col = ColPayType
                'If InStr(1, "BNOADCT", UCase(VB.Left(SprdMain.Text, 1))) = 0 Then SprdMain.Text = "B"
                'Call PayTypeAction(UCase(VB.Left(SprdMain.Text, 1)), eventArgs.row)

                SprdMain.Col = ColPayType
                SprdMain.Text = GetPayType(UCase(VB.Left(SprdMain.Text, 1)))

                SprdMain.Col = ColLocationId
                SprdMain.Row = eventArgs.row
                If Trim(SprdMain.Text) = "" Then
                    SprdMain.Text = GetLocationCode(mBillNo, mBillDate, lblAccountCode.Text, mCompanyCode, mPayType)
                End If

                'MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBillCheck)

            Case ColBillCheck
                SprdMain.Row = eventArgs.row

                SprdMain.Col = ColPayType
                If InStr(1, "BNOADCT", UCase(VB.Left(SprdMain.Text, 1))) = 0 Then SprdMain.Text = "B"
                Call PayTypeAction(UCase(VB.Left(SprdMain.Text, 1)), eventArgs.row)

            Case ColBillNo

                If DuplicateBillNo() = False Then
                    If CheckBillNo() = True Then

                    End If
                    SprdMain.Row = eventArgs.row

                    SprdMain.Col = ColBillNo
                    mBillNo = SprdMain.Text
                    SprdMain.Col = ColPayType
                    mPayType = VB.Left(SprdMain.Text, 1)

                    ProtectUnProtectCell(UCase(VB.Left(SprdMain.Text, 1)), (SprdMain.ActiveRow))

                    SprdMain.Row = eventArgs.row
                    SprdMain.Col = ColLocationId
                    If Trim(SprdMain.Text) = "" Then
                        SprdMain.Text = GetLocationCode(mBillNo, mBillDate, lblAccountCode.Text, mCompanyCode, mPayType)
                    End If

                    '-------- FILLING BILL AMT TO AMT COL


                    SprdMain.Col = ColBalance
                    mAmount = Val(SprdMain.Text)
                    SprdMain.Col = ColAmount
                    If Val(SprdMain.Text) = 0 Then
                        SprdMain.Text = IIf(Val(lblDiffAmt.Text) >= mAmount, mAmount, Val(lblDiffAmt.Text))
                    End If
                    '                MainClass.SetFocusToCell SprdMain, Row, ColAmount
                    '                SprdMain.Col = ColPayType
                End If
            Case ColBillDate
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColPayType
                mPayType = VB.Left(SprdMain.Text, 1)

                SprdMain.Col = ColLocationId
                SprdMain.Row = eventArgs.row
                If Trim(SprdMain.Text) = "" Then
                    SprdMain.Text = GetLocationCode(mBillNo, mBillDate, lblAccountCode.Text, mCompanyCode, mPayType)
                End If

                If DuplicateBillNo() = False Then
                    If CheckBillNo() = True Then

                    End If
                    If mPayType = "N" Then
                        SprdMain.Row = eventArgs.row
                        SprdMain.Col = ColBillDate
                        mBillDate = SprdMain.Text


                        If MainClass.ValidateWithMasterTable((lblAccountCode.Text), "SUPP_CUST_CODE", "PAYMENT_CODE", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                            mPayCode = MasterNo
                            If MainClass.ValidateWithMasterTable(mPayCode, "PAY_TERM_CODE", "FROM_DAYS", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                                mDueDays = MasterNo
                            Else

                            End If
                        Else
                            mDueDays = 0
                        End If


                        '                If MainClass.ValidateWithMasterTable(Val(lblAccountCode.text), "CODE", "BILLDUEDAYS", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo) = True Then
                        '                    mDueDays = MasterNo
                        '                Else
                        '                    mDueDays = 0
                        '                End If

                        ''Bill date maybe less then current FY Year....
                        '                If FYChk(SprdMain.Text) = False Then MainClass.SetFocusToCell SprdMain, Row, ColBillDate

                        SprdMain.Col = ColAmount
                        If Val(SprdMain.Text) = 0 Then SprdMain.Text = CStr(Val(lblDiffAmt.Text))

                        SprdMain.Col = ColDueDate
                        If IsDate(mBillDate) Then
                            SprdMain.Text = mBillDate ''DateAdd("D", mDueDays, CDate(mBillDate))
                        End If
                    End If
                End If
            Case ColAmount
                SprdMain.Row = eventArgs.row        ''SprdMain.ActiveRow
                SprdMain.Col = ColBillNo
                mBillNo = SprdMain.Text
                SprdMain.Col = ColPayType
                mPayType = VB.Left(SprdMain.Text, 1)
                SprdMain.Col = ColAmount
                If mPayType = "B" Then
                    If CheckAmount() = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAmount)
                        Exit Sub
                    End If
                End If

                If mPayType = "A" Or mPayType = "O" Then
                    SprdMain.Col = ColBillCompany
                    If Trim(SprdMain.Text) = "" Then
                        mCurrCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
                        SprdMain.Text = GetCompanyName(mCurrCompanyCode, "S")
                    End If
                End If



                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColAmount
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = eventArgs.row Then
                        SprdMain.Col = ColPayType
                        mPayType = VB.Left(UCase(Trim(SprdMain.Text)), 1)
                        ''MainClass.AddBlankSprdRow(SprdMain, IIf(mPayType = "B" Or mPayType = "N", ColBillNo, ColAmount), ConRowHeight)
                        MainClass.AddBlankSprdRow(SprdMain, ColPayType, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows), False)
                        '                Else
                        'MainClass.SetFocusToCell(SprdMain, mActiveRow, ColDC)
                    End If
                End If



                'SprdMain.Col = ColAmount
                'If Val(SprdMain.Text) <> 0 Then
                '    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                '        MainClass.AddBlankSprdRow(SprdMain, IIf(mPayType = "B" Or mPayType = "N", ColBillNo, ColAmount), ConRowHeight)
                '        FormatSprdMain((SprdMain.MaxRows), False)

                '    End If
                'End If

                ''If Row <> NewRow Then CheckForEqualAmount
                '                Call CheckForEqualAmount
            Case ColDC
                SprdMain.Col = ColDC
                SprdMain.Row = eventArgs.row
                If UCase(SprdMain.Text) = "DR" Or UCase(SprdMain.Text) = "D" Then
                    SprdMain.Text = "Dr"
                    Exit Sub
                ElseIf UCase(SprdMain.Text) = "CR" Or UCase(SprdMain.Text) = "C" Then
                    SprdMain.Text = "Cr"
                    Exit Sub
                Else
                    eventArgs.col = ColDC
                    SprdMain.Text = "Dr"
                    Exit Sub
                End If
                '            If Row <> NewRow Then CheckForEqualAmount
            Case ColLocationId
                SprdMain.Row = eventArgs.row

                SprdMain.Col = ColLocationId

                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColLocationId, "Invalid Location ID for such Party")
                    End If
                Else
                    'MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo, "Invalid Location ID for such Party")
                End If
            Case ColPONo
                SprdMain.Row = eventArgs.row

                SprdMain.Col = ColBillDate
                mBillDate = SprdMain.Text

                SprdMain.Col = ColPONo
                mPONo = SprdMain.Text
                If Val(mPONo) <> 0 Then
                    If MainClass.ValidateWithMasterTable(lblAccountName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                        mAccountCode = MasterNo
                    End If
                    If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "AUTO_KEY_PO", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & " AND SUPP_CUST_CODE='" & mAccountCode & "' AND PO_STATUS='Y'") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo, "Invalid PO No for such Party")
                    End If
                ElseIf Trim(mPONo) <> "" And Val(mPONo) <> 0 Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo, "Invalid PO No for such Party")
                End If

                If Trim(mPONo) <> "" Then
                    If ValidatePOAmount(mPONo, mBillDate, mAccountCode, mPOAmount, mPrevBillAmount, mCurrBillAmount) = True Then
                        If mPOAmount > 0 Then
                            If mPrevBillAmount > mPOAmount Then
                                If MsgQuestion("Amount aleady exceed ( Rs. " & mPrevBillAmount & ") from PO/ WO Amount " & mPOAmount & " . Are you Want to Continue ? ") = CStr(MsgBoxResult.No) Then
                                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                                    Exit Sub
                                End If
                            End If
                            If mPrevBillAmount + mCurrBillAmount > mPOAmount Then
                                If MsgQuestion("Amount aleady exceed ( Rs. " & mPrevBillAmount + mCurrBillAmount & ") from PO/ WO Amount " & mPOAmount & " . Are you Want to Continue ? ") = CStr(MsgBoxResult.No) Then
                                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                                End If
                            End If
                        End If
                    End If
                End If
        End Select
        CalcTots()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Function CheckAmount() As Boolean
        Dim mDC As String
        Dim mBalance As Double
        Dim mBalanceDC As String
        Dim mOldAmount As Double
        Dim mOldDC As String
        Dim mNetBalance As Double
        Dim mCurrAmount As Double

        With SprdMain
            .Col = ColOldDC
            mOldDC = VB.Left(.Text, 1)

            .Col = ColOldAmount
            mOldAmount = Val(.Text) * IIf(mOldDC = "D", -1, 1)


            .Col = ColBalanceDC
            mBalanceDC = VB.Left(.Text, 1)

            .Col = ColBalance
            mBalance = Val(.Text) * IIf(mBalanceDC = "D", 1, -1)

            mNetBalance = mBalance + mOldAmount

            .Col = ColDC
            mDC = VB.Left(.Text, 1)

            .Col = ColAmount
            mCurrAmount = Val(.Text) * IIf(mDC = "D", 1, -1)

            '        If mBalanceDC = mDC And mCurrAmount <> 0 Then
            '            ErrorMsg "Dr. / Cr. Mismatch.", "", vbCritical
            '            CheckAmount = False
            '        Else

            If System.Math.Abs(mCurrAmount) > System.Math.Abs(mNetBalance) Then
                ErrorMsg("Amount Exceeds", "", MsgBoxStyle.Critical)
                CheckAmount = False
            Else
                CheckAmount = True
            End If


        End With
    End Function
    Private Function CheckBillNo() As Boolean
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mPayType As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mDC As String
        Dim mPaymentAmt As Double

        Dim mBalance As Double
        Dim mRow As Integer
        Dim cntRow As Integer
        Dim mOldAmount As Double

        With SprdMain
            mRow = .ActiveRow
            .Row = mRow
            .Col = ColBillNo
            mBillNo = Trim(.Text)

            SprdMain.Col = ColPayType
            mPayType = VB.Left(.Text, 1)

            If mBillNo = "" And mPayType <> "O" Then
                .Row = mRow
                .Col = ColBillCheck
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

                .Row = mRow
                .Col = ColBillNo
                .Text = ""

                .Col = ColBillDate
                .Text = ""

                .Col = ColBillAmount
                .Text = "0.00"

                .Col = ColTDSAmount
                .Text = "0.00"

                .Col = ColInterestAmount
                .Text = "0.00"


                .Col = ColBalance
                .Text = "0.00"

                .Col = ColAmount
                .Text = "0.00"

                .Col = ColOldAmount
                .Text = "0.00"

                .Col = ColOldBillNo
                .Text = ""

                .Col = ColRefNo
                .Text = ""

                .Col = ColTaxableAmount
                .Text = "0.00"

                .Col = ColPONo
                .Text = ""

                .Col = ColDivCode
                .Text = "0"

                .Col = ColDueDate
                .Text = ""

                CheckBillNo = True
                Exit Function
            End If



            .Col = ColBillDate
            mBillDate = .Text

            Call GetBalanceAmount(mRow, (lblAccountCode.Text), mBillNo, mBillDate, mPayType)
            Call PickUpBillPayment(mPayType, mBillNo, mOldAmount, "D")

        End With
        CheckBillNo = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DuplicateBillNo() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckBillNo As String
        Dim mBillNo As String
        Dim mFYear As Integer

        With SprdMain
            .Row = .ActiveRow
            .Col = ColBillNo
            mCheckBillNo = Trim(UCase(.Text))

            .Col = ColPayType
            mCheckBillNo = VB.Left(Trim(UCase(.Text)), 1) & mCheckBillNo
            If mCheckBillNo = "B" Or mCheckBillNo = "N" Then
                DuplicateBillNo = False
                Exit Function
            End If
            mCheckBillNo = IIf(mCheckBillNo = "O", "", mCheckBillNo)

            .Col = ColBillDate
            If Trim(.Text) <> "" Then
                If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                    mFYear = CInt(VB6.Format(.Text, "YYYY"))
                Else
                    mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                End If
            End If

            mCheckBillNo = mCheckBillNo & ":" & VB6.Format(mFYear, "0000")

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillNo
                mBillNo = Trim(UCase(.Text))

                .Col = ColPayType
                mBillNo = VB.Left(Trim(UCase(.Text)), 1) & mBillNo

                .Col = ColBillDate
                If Trim(.Text) <> "" Then
                    If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                        mFYear = CInt(VB6.Format(.Text, "YYYY"))
                    Else
                        mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                    End If
                End If
                mBillNo = mBillNo & ":" & VB6.Format(mFYear, "0000")

                If (mBillNo = mCheckBillNo And mCheckBillNo <> "") And mCheckBillNo <> "A" Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateBillNo = True
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColBillNo, "Duplicate Bill No. : " & Mid(mCheckBillNo, 2))
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function BillNoNotIsGrid(ByRef pCheckBillNo As String, ByRef pCheckBillDate As String) As Boolean
        Dim cntRow As Integer
        'Dim mCount As Byte
        Dim mBillNo As String
        Dim mBillDate As String

        BillNoNotIsGrid = True

        If pCheckBillNo = "" Then Exit Function
        With SprdMain

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillNo
                mBillNo = Trim(UCase(.Text))

                .Col = ColBillDate
                mBillDate = Trim(UCase(.Text))


                If (mBillNo = pCheckBillNo) And (mBillDate = pCheckBillDate) Then
                    BillNoNotIsGrid = False
                    Exit Function
                End If

            Next
        End With
    End Function
    Private Sub GetBalanceAmount(ByRef pRow As Integer, ByRef pAccountCode As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pPayType As String)

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBalance As Double
        Dim mActBillAmount As Double
        Dim mBillAmount As Double
        Dim mPaymentAmt As Double
        Dim mDueDays As Double
        Dim mBillDate As String
        Dim mPayCode As String
        Dim mBillDC As String
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mCompanyName As String

        SqlStr = " Select Company_Code,BillNo, BillDate,MAX(EXPDATE) AS DueDate , " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT, " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) AS PayAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ACCOUNTCODE = '" & MainClass.AllowSingleQuote(pAccountCode) & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        End If
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(pBillNo) & "'"

        ''18-03-2010  ''Check New Bill Also.....
        If pPayType = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND BillDate>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BillDate<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If pBillDate <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY Company_Code,BillNo, BillDate " & vbCrLf & " ORDER BY BillNo, BillDate,ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount))-SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            .Row = pRow
            If RsTemp.EOF = False Then
                .Col = ColOldBillNo
                If pPayType = "N" And Trim(.Text) <> Trim(pBillNo) Then
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol, "Bill No. already exits")
                    Exit Sub
                End If


                If pPayType = "N" Then
                    mDivCode = IIf(Val(lblDivisionCode.Text) = 0, 1, Val(lblDivisionCode.Text))
                Else
                    mDivCode = GetDivisionCode(IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value), (RsTemp.Fields("BillDate").Value), (lblAccountCode.Text))
                End If

                ''06-12-2012
                '            If mDivCode <> Val(lblDivisionCode.text) Then
                '                MainClass.SetFocusToCell SprdMain, .ActiveRow, .ActiveCol, "Bill Not Exists in Such Division."
                '                Exit Sub
                '            End If

                If pPayType = "B" Then
                    mCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTemp.Fields("COMPANY_CODE").Value)  '' GetCompanyCode(IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value), (RsTempPRDetail.Fields("BillDate").Value), (lblAccountCode.Text))
                    .Col = ColBillCompany
                    .Text = GetCompanyName(mCompanyCode, "S")
                Else
                    .Col = ColBillCompany
                    mCompanyName = .Text

                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = MasterNo
                    Else
                        mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
                    End If

                End If


                mLocCode = GetLocationCode(pBillNo, (RsTemp.Fields("BillDate").Value), pAccountCode, mCompanyCode, pPayType)


                .Col = ColBillDate
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))
                .Text = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))

                .Col = ColBillAmount
                mActBillAmount = GetBillAmount(pAccountCode, pBillNo, mBillDate, Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value)))
                mBillAmount = Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value))
                .Text = Str(System.Math.Abs(mActBillAmount))

                .Col = ColBillAmountDC
                .Text = IIf(mActBillAmount >= 0, "Dr", "Cr")
                mBillDC = IIf(mBillAmount >= 0, "Dr", "Cr")

                .Col = ColBalance
                mPaymentAmt = Val(IIf(IsDBNull(RsTemp.Fields("PAYAMT").Value), 0, RsTemp.Fields("PAYAMT").Value))
                mBalance = mBillAmount + mPaymentAmt
                .Text = Str(System.Math.Abs(mBalance))
                '.Text = Str(Abs(mBalance) + Abs(mPRAmount))

                .Col = ColBalanceDC
                If mBalance = 0 Then
                    .Text = mBillDC
                Else
                    .Text = IIf(mBalance > 0, "Dr", "Cr")
                End If

                .Col = ColDueDate
                If pPayType = "N" Then
                    .Text = VB6.Format(mBillDate, "DD/MM/YYYY")
                Else
                    .Text = IIf(IsDBNull(RsTemp.Fields("DueDate").Value), "", VB6.Format(RsTemp.Fields("DueDate").Value, "DD/MM/YYYY"))
                End If

                .Col = ColLocationId
                '.Text = IIf(mPayType = "O" Or mPayType = "A", "", RsTempPRDetail.Fields("BillDate").Value)
                .Text = mLocCode



                .Col = ColDivCode
                .Text = Str(mDivCode)

                '********************
                .Row = .MaxRows
                .Row2 = .MaxRows
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(IIf(UCase(mBillDC) = UCase(lblDC.Text), &H8000000F, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White))) ''&H80FF80
                .BlockMode = False
                '********************

            Else
                Select Case pPayType
                    Case "B", "D", "C", "T"
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol, "No Such Bill No")
                    Case "N"
                        .Col = ColBillDate
                        If .Text = "" Then .Text = CStr(CDate(lblVDate.Text))
                        mBillDate = .Text

                        mDivCode = IIf(Val(lblDivisionCode.Text) = 0, 1, Val(lblDivisionCode.Text))

                        .Col = ColDivCode
                        .Text = Str(mDivCode)

                        If MainClass.ValidateWithMasterTable(pAccountCode, "SUPP_CUST_CODE", "PAYMENT_CODE", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPayCode = MasterNo
                            If MainClass.ValidateWithMasterTable(mPayCode, "PAY_TERM_CODE", "FROM_DAYS", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mDueDays = MasterNo
                            Else

                            End If
                        Else
                            mDueDays = 0
                        End If

                        .Col = ColDueDate
                        .Text = mBillDate ''DateAdd("D", mDueDays, CDate(mBillDate))
                        '                    MainClass.SetFocusToCell SprdMain, .ActiveRow, ColBillDate
                End Select
            End If
        End With
    End Sub

    Private Function GetBillAmount(ByRef xAccountCode As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xBillAmount As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheck As Integer
        Dim mBillYear As Integer


        mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)

        If mBillYear = RsCompany.Fields("FYEAR").Value Or mBillYear = -1 Then
            GetBillAmount = xBillAmount
            Exit Function
        End If

        mCheck = 1

NextSearch:
        GetBillAmount = 0
        SqlStr = " Select SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " ACCOUNTCODE = '" & MainClass.AllowSingleQuote(xAccountCode) & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        End If
        If mCheck = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='O'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(xBillNo) & "'"
        SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        '        ''18-03-2010  ''Check New Bill Also.....
        '    If pPayType = "N" Then
        '        SqlStr = SqlStr & vbCrLf & " AND BillDate>='" & vb6.Format(RsCompany!Start_Date, "DD-MMM-YYYY") & "' AND BillDate<='" & vb6.Format(RsCompany!END_DATE, "DD-MMM-YYYY") & "'"
        '    Else
        '        If pBillDate <> "" Then
        '            SqlStr = SqlStr & vbCrLf & " AND BillDate='" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "'"
        '    '    Else
        '    '        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)+SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount)<>0"
        '        End If
        '    End If
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " GROUP BY BillNo, BillDate " & vbCrLf _
        ''            & " ORDER BY BillNo, BillDate,ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount))-SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBillAmount = IIf(IsDBNull(RsTemp.Fields("BillAMT").Value), 0, RsTemp.Fields("BillAMT").Value)
        Else
            If mCheck = 2 Then
                GetBillAmount = 0
            Else
                '            mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
                If mBillYear = RsCompany.Fields("FYEAR").Value Then
                    GetBillAmount = 0
                Else
                    mCheck = 2
                    GoTo NextSearch
                End If
            End If
        End If
        Exit Function
ErrPart:
        GetBillAmount = 0
    End Function
    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If optShow(0).Checked = True Then
            Call optShow_CheckedChanged(optShow.Item(0), New System.EventArgs())
        End If
        eventArgs.Cancel = Cancel
    End Sub
    '
    Private Sub txtBillSearchFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillSearchFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'If Not IsDate(txtBillSearchFrom.Text) Then
        '    Cancel = True
        'End If
        'If optShow(0).Checked = True Then
        '    Call optShow_CheckedChanged(optShow.Item(0), New System.EventArgs())
        'End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDefaultCompanyName_TextChanged(sender As Object, e As EventArgs) Handles txtDefaultCompanyName.TextChanged

    End Sub

    Private Sub txtDefaultCompanyName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDefaultCompanyName.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCompanyName()
    End Sub

    Private Sub txtDefaultCompanyName_DoubleClick(sender As Object, e As EventArgs) Handles txtDefaultCompanyName.DoubleClick
        Call SearchCompanyName()
    End Sub

    Private Sub txtDefaultCompanyName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDefaultCompanyName.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDefaultCompanyName.Text)
        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub SearchCompanyName()
        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = SqlStr & " STATUS='O'"


        MainClass.SearchGridMaster((txtDefaultCompanyName.Text), "GEN_COMPANY_MST", "COMPANY_NAME", "COMPANY_SHORTNAME", , , SqlStr)
        If AcName <> "" Then
            txtDefaultCompanyName.Text = AcName
            txtDefaultCompanyName_Validating(txtDefaultCompanyName.Text, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDefaultCompanyName_Validating(sender As Object, e As CancelEventArgs) Handles txtDefaultCompanyName.Validating
        Dim Cancel As Boolean = e.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If Trim(txtDefaultCompanyName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtDefaultCompanyName.Text, "COMPANY_NAME", "COMPANY_NAME", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "STATUS='O'") = False Then
            MsgBox("Invaild Company Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        e.Cancel = Cancel
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
            If MainClass.SearchIntoFullGrid(SprdMain, ColPayType, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPayType)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

End Class
