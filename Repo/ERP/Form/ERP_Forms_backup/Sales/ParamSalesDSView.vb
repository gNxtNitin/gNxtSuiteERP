Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSalesDSView
    Inherits System.Windows.Forms.Form
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColCustPartNo As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColItemUOM As Short = 4
    Private Const ColStoreLoc As Short = 5
    Private Const ColOriginalQty As Short = 6
    Private Const ColPreviousQty As Short = 7
    Private Const ColRate As Short = 8
    Private Const ColQty As Short = 9
    Private Const ColAmount As Short = 10
    Private Const ColAmendQty As Short = 11
    Private Const ColAmendAmount As Short = 12
    Private Const ColAmendReason As Short = 13
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Public Sub frmParamSalesDSView_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Sale Delivery Schedule - View"

        Clear1()
        ShowDetail1()
        '    FormatSprdMain -1				

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamSalesDSView_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection				
        'PvtDBCn.Open StrConn				
        Call SetMainFormCordinate(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

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
        Dim SqlStr As String
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

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 4)


            For cntCol = ColOriginalQty To ColAmendAmount
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

    Private Sub frmParamSalesDSView_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub

    Private Sub ShowDetail1()
        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim RsDSSDetail As ADODB.Recordset
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mRate As Double
        Dim mQty As Double
        Dim mPreviousQty As Double
        Dim mStoreLoc As String
        Dim mOriginalQty As Double

        SqlStr = ""
        SqlStr = " SELECT ID.SERIAL_NO, ID.ITEM_CODE, ID.LOC_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO,  " & vbCrLf _
            & " ID.ITEM_UOM, ID.ITEM_QTY, ID.AMEND_REASON" & vbCrLf _
            & " FROM DSP_DELV_SCHLD_REQ_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " AND ID.COMPANY_CODE = INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE = INVMST.ITEM_CODE" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDSSDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1				
            I = 1
            '        .MoveFirst				

            Do While Not .EOF

                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColCustPartNo
                mPartNo = Trim(IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))
                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColItemName
                mItemDesc = Trim(IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColStoreLoc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value))
                mStoreLoc = Trim(IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value))


                SprdMain.Col = ColOriginalQty
                If Val(lblAmendNo.Text) = 0 Then
                    mOriginalQty = 0
                Else
                    mOriginalQty = GetOriginalQty(mItemCode, mStoreLoc)
                End If
                SprdMain.Text = CStr(Val(CStr(mOriginalQty)))

                SprdMain.Col = ColPreviousQty
                If Val(lblAmendNo.Text) = 0 Then
                    mPreviousQty = 0
                Else
                    mPreviousQty = GetPreviousQty(mItemCode, mStoreLoc)
                End If
                SprdMain.Text = CStr(Val(CStr(mPreviousQty)))

                SprdMain.Col = ColRate
                mRate = GetSORate((lblCustomerCode.Text), Val(lblSONo.Text), (lblDate.Text), Val(lblMkey.Text), mItemCode, mStoreLoc)
                SprdMain.Text = CStr(Val(CStr(mRate)))



                SprdMain.Col = ColQty
                mQty = Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value))
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(CStr(mRate * mQty)))

                SprdMain.Col = ColAmendQty
                SprdMain.Text = CStr(mQty - mPreviousQty)

                SprdMain.Col = ColAmendAmount
                SprdMain.Text = CStr(mRate * (mQty - mPreviousQty))

                SprdMain.Col = ColAmendReason
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("AMEND_REASON").Value), "", .Fields("AMEND_REASON").Value))

                If mPreviousQty <> mQty And Val(lblAmendNo.Text) > 0 Then
                    SprdMain.Row = I
                    SprdMain.Row2 = I
                    SprdMain.Col = ColItemCode
                    SprdMain.Col2 = ColAmendReason ''SprdMain.ActiveCol				
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                    SprdMain.BlockMode = False
                Else
                    SprdMain.Row = I
                    SprdMain.Row2 = I
                    SprdMain.Col = ColItemCode
                    SprdMain.Col2 = ColAmendReason '' SprdMain.ActiveCol				
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    SprdMain.BlockMode = False
                End If

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
    Private Function GetPreviousQty(ByRef mItemCode As String, ByRef mStoreLoc As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double

        GetPreviousQty = 0
        SqlStr = " SELECT ID.ITEM_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(lblCustomerCode.Text) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(lblSONo.Text) & "" & vbCrLf _
            & " AND IH.SCHLD_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        If mStoreLoc <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousQty = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        Else
            GetPreviousQty = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPreviousQty = 0
    End Function
    Private Function GetOriginalQty(ByRef mItemCode As String, ByRef mStoreLoc As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double

        GetOriginalQty = 0
        SqlStr = " SELECT SUM(ID.PLANNED_QTY) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_LOG_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(lblCustomerCode.Text) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(lblSONo.Text) & "" & vbCrLf _
            & " AND IH.SCHLD_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        If mStoreLoc <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.AMEND_NO=0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetOriginalQty = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        Else
            GetOriginalQty = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetOriginalQty = 0
    End Function
    Private Function GetSORate(ByRef mCustomerCode As String, ByRef mSONo As Double, ByRef mSchdDate As String, ByRef pRefNo As Double, ByRef pItemCode As String, ByRef mStoreLoc As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double

        GetSORate = 0
        SqlStr = " SELECT ID.ITEM_CODE,"

        SqlStr = SqlStr & vbCrLf _
            & "  (SELECT ITEM_PRICE AS ITEM_PRICE FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
            & " WHERE SIH.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND SID.ITEM_CODE=ID.ITEM_CODE" & vbCrLf _
            & " AND NVL(SID.CUST_STORE_LOC,' ')=NVL(ID.LOC_CODE,' ')" & vbCrLf _
            & " AND SIH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & " AND SIH.SO_APPROVED='Y'" & vbCrLf _
            & " AND SIH.MKEY = ("

        SqlStr = SqlStr & vbCrLf _
            & "SELECT MAX(SSIH.MKEY) FROM  DSP_SALEORDER_HDR SSIH, DSP_SALEORDER_DET SSID" & vbCrLf _
            & " WHERE SSIH.COMPANY_CODE=SIH.COMPANY_CODE" & vbCrLf _
            & " AND SSIH.MKEY=SSID.MKEY AND SSIH.SUPP_CUST_CODE=SIH.SUPP_CUST_CODE" & vbCrLf _
            & " AND SSID.ITEM_CODE=SID.ITEM_CODE" & vbCrLf _
            & " AND NVL(SSID.CUST_STORE_LOC,' ')=NVL(SID.CUST_STORE_LOC,' ') AND SO_STATUS='O'" & vbCrLf _
            & " AND SSIH.AUTO_KEY_SO=SIH.AUTO_KEY_SO AND SSIH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & "  AND SSIH.SO_APPROVED='Y'" & vbCrLf _
            & " AND SSID.AMEND_WEF <=IH.SCHLD_DATE)) AS RATE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DELV_SCHLD_REQ_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If mStoreLoc <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetSORate = Val(IIf(IsDBNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value))
                RsTemp.MoveNext()
            Loop
        Else
            GetSORate = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSORate = 0
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim mAmendQty As Double

Reset:
            With SprdMain
                For mRow = 1 To .MaxRows
                    'If .MaxRows < 100 Then
                    .Row = mRow
                    .Col = ColAmendQty
                    mAmendQty = Trim(.Text)
                    If mAmendQty = 0 Then
                        .Col = ColItemCode
                        .Row = mRow
                        .RowHidden = True
                    End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
