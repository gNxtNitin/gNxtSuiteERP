Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmChildBOMStock
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColGrossQty As Short = 4
    Private Const ColStockQty As Short = 5

    Public Sub FormatSprdDlv(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdDlv
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 28)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 4)

            .Col = ColGrossQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            MainClass.ProtectCell(SprdDlv, 1, .MaxRows, ColItemCode, ColStockQty)

        End With
        MainClass.SetSpreadColor(SprdDlv, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub frmChildBOMStock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub


        Call ShowChildBOMStock()
        FormLoaded = True

    End Sub
    Private Sub frmChildBOMStock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)
        Call SetMainFormCordinate(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)


        'Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        'Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        MainClass.ClearGrid(SprdDlv)
        FormatSprdDlv(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmChildBOMStock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub ShowChildBOMStock()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mLastDay As Integer
        Dim pSDate As String
        Dim mItemCode As String
        Dim pPackUnit As String
        'Dim mGrossQty As Double
        Dim mStockQty As Double
        Dim mStockType As String

        Dim mIsInHouse As Boolean = False
        Dim mDeptCode As String = ""
        Dim xWareHouse As String

        mStockQty = 0
        MainClass.ClearGrid(SprdDlv)
        FormatSprdDlv(-1)
        SqlStr = "SELECT RM_CODE,ITEM_SHORT_DESC, INVMST.ISSUE_UOM, (STD_QTY+GROSS_WT_SCRAP) AS GROSS_QTY,STOCK_TYPE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And IH.MKEY =ID.MKEY" & vbCrLf _
            & " And IH.COMPANY_CODE =INVMST.COMPANY_CODE" & vbCrLf _
            & " And ID.RM_CODE =INVMST.ITEM_CODE" & vbCrLf _
            & " And IH.PRODUCT_CODE ='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'" & vbCrLf _
            & " AND IH.WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR SH" & vbCrLf _
            & " WHERE SH.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
            & " AND SH.PRODUCT_CODE=IH.PRODUCT_CODE" & vbCrLf _
            & " AND SH.WEF<=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
            & "SELECT ALTER_RM_CODE RM_CODE, '*' || ITEM_SHORT_DESC, INVMST.ISSUE_UOM, (ALTER_STD_QTY+ALETRSCRAP) AS GROSS_QTY,ALTER_STOCK_TYPE STOCK_TYPE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And IH.MKEY =ID.MKEY" & vbCrLf _
            & " And IH.COMPANY_CODE =INVMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ALTER_RM_CODE =INVMST.ITEM_CODE" & vbCrLf _
            & " And IH.PRODUCT_CODE ='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'" & vbCrLf _
            & " AND IH.WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR SH" & vbCrLf _
            & " WHERE SH.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
            & " AND SH.PRODUCT_CODE=IH.PRODUCT_CODE" & vbCrLf _
            & " AND SH.WEF<=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdDlv
                    .Row = I

                    .Col = ColItemCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("RM_CODE").Value), lblItemCode.Text, RsTemp.Fields("RM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("RM_CODE").Value), lblItemCode.Text, RsTemp.Fields("RM_CODE").Value)

                    .Col = ColItemName
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                    .Col = ColItemUOM
                    .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)


                    .Col = ColGrossQty
                    .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("GROSS_QTY").Value), "0", RsTemp.Fields("GROSS_QTY").Value)))

                    pPackUnit = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                    mStockType = IIf(IsDBNull(RsTemp.Fields("STOCK_TYPE").Value), "", RsTemp.Fields("STOCK_TYPE").Value)



                    xWareHouse = "PH"
                    mIsInHouse = IsInHouseItem(mItemCode)
                    mDeptCode = Trim(lblDeptCode.Text)
                    If mIsInHouse = True And CheckAutoIssueProd(VB6.Format(lblDate.Text, "DD/MM/YYYY"), mItemCode) = True Then
                        mDeptCode = GetProductFinalDept(mItemCode, (lblDate.Text))
                        If GetDeptType(mDeptCode) = "3" Then
                            mDeptCode = Trim(lblDeptCode.Text)
                        Else
                            If mDeptCode = "STR" Or mDeptCode = "" Then
                                xWareHouse = "WH"
                                mDeptCode = Trim(lblDeptCode.Text)
                            End If
                        End If

                    End If
                    mStockQty = GetBalanceStockQty(mItemCode, lblDate.Text, pPackUnit, mDeptCode, mStockType, "", xWareHouse, Val(lblDivision.Text), lblRefType.Text, Val(lblRefNo.Text))
                    .Col = ColStockQty
                    .Text = VB6.Format(mStockQty, "0.000")

                End With

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    SprdDlv.MaxRows = I + 1
                End If
            Loop
        End If
        FormatSprdDlv(-1)
    End Sub
End Class
