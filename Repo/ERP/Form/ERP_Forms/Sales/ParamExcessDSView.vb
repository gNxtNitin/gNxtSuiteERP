Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamExcessDSView
    Inherits System.Windows.Forms.Form
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColCustPartNo As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColItemUOM As Short = 4
    Private Const ColRate As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColAmendReason As Short = 8
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Public Sub frmParamExcessDSView_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Excess Delivery Schedule Approval - View"

        Clear1()
        ShowDetail1()
        '    FormatSprdMain -1

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamExcessDSView_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)

        'Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        'Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        MainClass.SetControlsColor(Me)

        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 7)


            .Col = ColCustPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 7)
            .TypeEditMultiLine = True


            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 20)
            '        .ColUserSortIndicator(ColItemName) = ColUserSortIndicatorAscending
            .TypeEditMultiLine = True

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 4)


            .ColsFrozen = ColItemUOM


            For cntCol = ColRate To ColAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2 ''4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColAmendReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 15)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmendReason)
            MainClass.SetSpreadColor(SprdMain, Arow)

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub frmParamExcessDSView_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.hide()
        Me.Close()
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsDSSDetail As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mRate As Double
        Dim mQty As Double
        Dim mLastDate As String

        mLastDate = MainClass.LastDay(Month(CDate(lblDate.Text)), Year(CDate(lblDate.Text))) & "/" & VB6.Format(lblDate.Text, "MM/YYYY")

        SqlStr = ""
        SqlStr = " SELECT ID.SERIAL_NO, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO,  " & vbCrLf & " INVMST.PURCHASE_UOM, ID.APP_QTY, " & vbCrLf & " (SELECT (((NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,4))) * EXCHANGERATE)" & vbCrLf & " FROM PUR_PURCHASE_HDR PH, PUR_PURCHASE_DET PD " & vbCrLf & " WHERE PH.COMPANY_CODE = PD.COMPANY_CODE AND PH.MKEY = PD.MKEY And PD.ITEM_CODE = ID.ITEM_CODE" & vbCrLf & " AND PH.SUPP_CUST_CODE= ID.SUPP_CUST_CODE" & vbCrLf & " AND PD.MKEY =  (SELECT MAX(SPH.MKEY) " & vbCrLf & " FROM PUR_PURCHASE_HDR SPH, PUR_PURCHASE_DET SPD" & vbCrLf & " WHERE SPH.MKEY = SPD.MKEY " & vbCrLf & " AND SPH.SUPP_CUST_CODE= ID.SUPP_CUST_CODE" & vbCrLf & " AND SPD.ITEM_CODE= ID.ITEM_CODE" & vbCrLf & " AND SPD.PO_WEF_DATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND PO_STATUS='Y' AND ISGSTENABLE_PO='Y' AND ORDER_TYPE='O')) AS PORATE," & vbCrLf & " ID.REMARKS" & vbCrLf & " FROM INV_EXCESS_DS_APP_DET ID, INV_ITEM_MST INVMST " & vbCrLf & " WHERE ID.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.MKEY = '" & lblMkey.Text & "'" & vbCrLf & " AND ID.COMPANY_CODE = INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE = INVMST.ITEM_CODE" & vbCrLf & " AND ID.BOOKTYPE='D'" & vbCrLf & " Order By SERIAL_NO"

        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDSSDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColCustPartNo
                mPartNo = Trim(IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))
                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColItemName
                mItemDesc = Trim(IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value))

                SprdMain.Col = ColRate
                mRate = Val(IIf(IsDbNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value)) '' GetSORate(lblCustomerCode.text, lblDate.text, mItemCode)
                SprdMain.Text = CStr(Val(CStr(mRate)))

                SprdMain.Col = ColQty
                mQty = Val(IIf(IsDbNull(.Fields("APP_QTY").Value), 0, .Fields("APP_QTY").Value)) ''PORATE
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("APP_QTY").Value), 0, .Fields("APP_QTY").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(CStr(mRate * mQty)))

                SprdMain.Col = ColAmendReason
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                .MoveNext()
                If .EOF = False Then
                    I = I + 1
                    SprdMain.MaxRows = I
                End If
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function GetSORate(ByRef mCustomerCode As String, ByRef mSchdDate As String, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double

        ''& " TO_CHAR(GetITEMPRICE_NEW(1,1,ID.SCHD_DATE,IH.AUTO_KEY_PO,ID.ITEM_CODE))" & vbCrLf _
        '
        '        GetSORate = 0
        '        SqlStr = " SELECT ID.ITEM_CODE,"
        '
        '        SqlStr = SqlStr & "  (SELECT ITEM_PRICE AS ITEM_PRICE FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
        ''                & " WHERE SIH.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
        ''                & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
        ''                & " AND SID.ITEM_CODE=ID.ITEM_CODE" & vbCrLf _
        ''                & " AND SIH.AUTO_KEY_SO=" & Val(mSONo) & " AND SIH.SO_APPROVED='Y'" & vbCrLf _
        ''                & " AND SIH.MKEY = ("
        '
        '        SqlStr = SqlStr & "SELECT MAX(SSIH.MKEY) FROM  DSP_SALEORDER_HDR SSIH, DSP_SALEORDER_DET SSID" & vbCrLf _
        ''             & " WHERE SSIH.COMPANY_CODE=SIH.COMPANY_CODE" & vbCrLf _
        ''             & " AND SSIH.MKEY=SSID.MKEY AND SSIH.SUPP_CUST_CODE=SIH.SUPP_CUST_CODE" & vbCrLf _
        ''             & " AND SSID.ITEM_CODE=SID.ITEM_CODE" & vbCrLf _
        ''             & " AND SSIH.AUTO_KEY_SO=SIH.AUTO_KEY_SO AND SSIH.SO_APPROVED='Y'" & vbCrLf _
        ''             & " AND SSID.AMEND_WEF <=IH.SCHLD_DATE)) AS RATE"
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DELV_SCHLD_REQ_DET ID" & vbCrLf _
        ''                & " WHERE IH.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
        ''                & " AND IH.AUTO_KEY_DELV=" & Val(pRefNo) & "" & vbCrLf _
        ''                & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        '
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '        If RsTemp.EOF = False Then
        '            Do While RsTemp.EOF = False
        '                GetSORate = Val(IIf(IsNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value))
        '                RsTemp.MoveNext
        '            Loop
        '        Else
        '            GetSORate = 0
        '        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSORate = 0
    End Function
End Class
