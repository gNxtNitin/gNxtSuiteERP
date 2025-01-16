Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmShortClosingSO
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColPONo As Short = 2
    Private Const ColAmendNo As Short = 3
    Private Const ColPODate As Short = 4
    Private Const ColCustPONo As Short = 5
    Private Const ColCustPODate As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColSNO As Short = 8
    Private Const ColItemCode As Short = 9
    Private Const ColItemName As Short = 10
    Private Const ColItemUOM As Short = 11
    Private Const ColOrdQty As Short = 12
    Private Const ColRecdQty As Short = 13
    Private Const ColBalQty As Short = 14
    Private Const ColStatus As Short = 15

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKey As Double
        Dim mUpdateCount As Integer
        Dim mItemCode As String
        Dim mPurType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColMKEY
                mMKey = CDbl(Trim(.Text))

                .Col = ColItemCode
                mItemCode = Trim(.Text)


                .Col = ColStatus

                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    If optSOType(1).Checked = True Then
                        SqlStr = "UPDATE DSP_SALEORDER_DET SET SO_ITEM_STATUS='Y' WHERE MKEY=" & mMKey & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                        PubDBCn.Execute(SqlStr)


                        SqlStr = "UPDATE DSP_SALEORDER_HDR SET " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY=" & mMKey & ""

                        PubDBCn.Execute(SqlStr)
                    Else
                        SqlStr = "UPDATE DSP_SALEORDER_HDR SET SO_STATUS='C', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY=" & mMKey & ""

                        PubDBCn.Execute(SqlStr)
                    End If

                    mUpdateCount = mUpdateCount + 1
                    End If

            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Item Closed.", MsgBoxStyle.Information)
        CmdSave.Enabled = False
        cmdShow.Enabled = True
        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = False
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmShortClosingSO_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmShortClosingSO_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        TxtAccount.Enabled = False
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked

        cboStatus.Items.Clear()
        cboStatus.Items.Add("All")
        cboStatus.Items.Add("Complete")
        cboStatus.Items.Add("Pending")
        cboStatus.SelectedIndex = 2
        cboStatus.Enabled = False

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSuppCode As String

        If optSOType(1).Checked = True Then


            SqlStr = " SELECT POMain.MKEY,POMain.AUTO_KEY_SO,POMain.AMEND_NO, TO_CHAR(POMain.SO_DATE,'DD/MM/YYYY'), " & vbCrLf _
                    & " POMain.CUST_PO_NO, POMain.CUST_PO_DATE,  " & vbCrLf _
                    & " ACM.SUPP_CUST_NAME,PODetail.SERIAL_NO, " & vbCrLf _
                    & " PODetail.ITEM_CODE,INVMST.Item_Short_Desc, " & vbCrLf _
                    & " PODetail.UOM_CODE, " & vbCrLf _
                    & " TO_CHAR(SO_QTY) AS ITEM_QTY, " & vbCrLf _
                    & " TO_CHAR(GETDSDESPATCHQTY (POMAIN.COMPANY_CODE,  POMain.SUPP_CUST_CODE,POMain.AUTO_KEY_SO, PODetail.ITEM_CODE,NVL(PODetail.CUST_STORE_LOC,' '),POMain.SO_DATE,TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))), " & vbCrLf _
                    & " TO_CHAR(SO_QTY-GETDSDESPATCHQTY (POMAIN.COMPANY_CODE,  POMain.SUPP_CUST_CODE,POMain.AUTO_KEY_SO, PODetail.ITEM_CODE,NVL(PODetail.CUST_STORE_LOC,' '),POMain.SO_DATE,TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))), " & vbCrLf _
                    & " DECODE(PODetail.SO_ITEM_STATUS,'N','0','1') " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR POMain, DSP_SALEORDER_DET PODetail, " & vbCrLf _
                    & " FIN_SUPP_CUST_MST ACM, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""



            SqlStr = SqlStr & vbCrLf _
                    & " AND POMain.MKEY=PoDetail.MKEY " & vbCrLf _
                    & " AND POMain.Company_Code=ACM.Company_Code " & vbCrLf _
                    & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                    & " AND POMain.Company_Code=INVMST.Company_Code " & vbCrLf _
                    & " AND PoDetail.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND POMain.ORDER_TYPE='C' AND SO_APPROVED='Y' AND SO_STATUS='O' " & vbCrLf _
                    & " AND PODetail.SO_ITEM_STATUS = 'N' "
            '    End If

            If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSuppCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & " And POMain.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
                End If
            End If

            If cboStatus.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND SO_QTY-GETDSDESPATCHQTY (POMAIN.COMPANY_CODE,  POMain.SUPP_CUST_CODE,POMain.AUTO_KEY_SO, PODetail.ITEM_CODE,NVL(PODetail.CUST_STORE_LOC,' '),POMain.SO_DATE,TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))=0"
            ElseIf cboStatus.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & " AND SO_QTY-GETDSDESPATCHQTY (POMAIN.COMPANY_CODE,  POMain.SUPP_CUST_CODE,POMain.AUTO_KEY_SO, PODetail.ITEM_CODE,NVL(PODetail.CUST_STORE_LOC,' '),POMain.SO_DATE,TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))<>0"
            End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY POMain.ORDER_TYPE,POMain.SO_DATE,POMain.AUTO_KEY_SO"
        Else
            SqlStr = " SELECT POMain.MKEY,POMain.AUTO_KEY_SO,POMain.AMEND_NO, TO_CHAR(POMain.SO_DATE,'DD/MM/YYYY'), " & vbCrLf _
                    & " POMain.CUST_PO_NO, POMain.CUST_PO_DATE,  " & vbCrLf _
                    & " ACM.SUPP_CUST_NAME,'', " & vbCrLf _
                    & " '','', " & vbCrLf _
                    & " '', " & vbCrLf _
                    & " '' AS ITEM_QTY, " & vbCrLf _
                    & " '', " & vbCrLf _
                    & " '', " & vbCrLf _
                    & " DECODE(POMain.SO_STATUS,'O','0','1') " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR POMain, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                    & " WHERE POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""


            SqlStr = SqlStr & vbCrLf _
                    & " AND POMain.Company_Code=ACM.Company_Code " & vbCrLf _
                    & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                    & " AND POMain.ORDER_TYPE='O' AND SO_APPROVED='Y' AND SO_STATUS='O' "
            '    End If

            If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSuppCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & " And POMain.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
                End If
            End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY POMain.ORDER_TYPE,POMain.SO_DATE,POMain.AUTO_KEY_SO"
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 9)
            .ColHidden = True

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPONo, 9)

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColAmendNo, 9)
            .ColsFrozen = ColAmendNo

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPODate, 10)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            '    .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            .ColsFrozen = ColPartyName

            .Col = ColSNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColSNO, 4)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCode, 8)
            .ColHidden = IIf(optSOType(1).Checked = True, False, True)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            '    .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 15)
            .ColHidden = IIf(optSOType(1).Checked = True, False, True)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemUOM, 4)
            .ColHidden = IIf(optSOType(1).Checked = True, False, True)


            .Col = ColOrdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOrdQty, 9)
            .ColHidden = IIf(optSOTypeOpen.Checked = True, True, False)

            .Col = ColRecdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRecdQty, 9)
            .ColHidden = IIf(optSOType(1).Checked = True, False, True)

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 9)
            .ColHidden = IIf(optSOType(1).Checked = True, False, True)


            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 6)
            '    .Value = vbUnchecked

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColBalQty)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "mKEY"

            .Col = ColPONo
            .Text = "SO No."

            .Col = ColAmendNo
            .Text = "Amend No."

            .Col = ColPODate
            .Text = "SO Date"

            .Col = ColCustPONo
            .Text = "Customer PO No."

            .Col = ColCustPODate
            .Text = "Customer PO Date"


            .Col = ColPartyName
            .Text = "Customer Name"

            .Col = ColSNO
            .Text = "S.No."

            .Col = ColItemCode
            .Text = "Product Code"

            .Col = ColItemName
            .Text = "Product Desc"

            .Col = ColItemUOM
            .Text = "UOM"

            .Col = ColOrdQty
            .Text = "Order Qty."

            .Col = ColRecdQty
            .Text = "Despatched Qty"

            .Col = ColBalQty
            .Text = "Balance Qty."

            .Col = ColStatus
            .Text = "Post Status"
        End With
    End Sub

    Private Sub frmShortClosingSO_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmShortClosingSO_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub


    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
            CmdSave.Enabled = True
            cmdShow.Enabled = False
        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        CmdSave.Enabled = True
        cmdShow.Enabled = False
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        On Error GoTo ERR1
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE IN ('S','C'))")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        TxtAccount.Enabled = IIf(ChkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub _OptSelection_0_CheckedChanged(sender As Object, e As EventArgs) Handles _OptSelection_0.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptSelection_1_CheckedChanged(sender As Object, e As EventArgs) Handles _OptSelection_1.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub optSOType_CheckedChanged(sender As Object, e As EventArgs) Handles optSOType.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub optSOTypeClose_CheckedChanged(sender As Object, e As EventArgs) Handles optSOTypeClose.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub optSOTypeOpen_CheckedChanged(sender As Object, e As EventArgs) Handles optSOTypeOpen.CheckedChanged
        cmdShow.Enabled = True
    End Sub
End Class
