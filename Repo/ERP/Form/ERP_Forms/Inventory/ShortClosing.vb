Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmShortClosing
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColPONo As Short = 2
    Private Const ColAmendNo As Short = 3
    Private Const ColPOType As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColSNo As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemName As Short = 8
    Private Const ColItemUOM As Short = 9
    Private Const ColOrdQty As Short = 10
    Private Const ColRecdQty As Short = 11
    Private Const ColBalQty As Short = 12
    Private Const ColStatus As Short = 13

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub cboPurType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurType.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboPurType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurType.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub


    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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

                .Col = ColPOType
                mPurType = VB.Left(.Text, 1)

                .Col = ColStatus
                If mPurType = "J" Then
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        SqlStr = "UPDATE PUR_PURCHASE_DET SET PO_ITEM_STATUS='Y'" & vbCrLf _
                            & " WHERE MKEY=" & mMKey & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                        PubDBCn.Execute(SqlStr)
                        mUpdateCount = mUpdateCount + 1
                        'Else
                        '    SqlStr = "UPDATE PUR_PURCHASE_DET SET PO_ITEM_STATUS='N' WHERE MKEY=" & mMKey & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                        '    PubDBCn.Execute(SqlStr)
                    End If
                Else
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        SqlStr = "UPDATE PUR_PURCHASE_DET SET PO_ITEM_STATUS='Y' WHERE MKEY=" & mMKey & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                        PubDBCn.Execute(SqlStr)
                        mUpdateCount = mUpdateCount + 1
                    End If
                End If

                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    SqlStr = "UPDATE PUR_PURCHASE_HDR SET UPDATE_FROM='N'," & vbCrLf _
                        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD/MMM/YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE MKEY=" & mMKey & ""

                    PubDBCn.Execute(SqlStr)


                End If

            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Item Closed.", MsgBoxStyle.Information)
        Show1()

        FormatSprdMain()
        cmdSave.Enabled = False
        cmdShow.Enabled = True
        Exit Sub
ErrPart:
        'Resume
        ErrorMsg(Err.Description, err.NUmber, MsgBoxStyle.Critical)
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
    Public Sub frmShortClosing_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmShortClosing_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        TxtAccount.Enabled = False
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        cboStatus.Items.Clear()
        cboStatus.Items.Add("All")
        cboStatus.Items.Add("Complete")
        cboStatus.Items.Add("Pending")
        cboStatus.SelectedIndex = 2
        cboStatus.Enabled = False

        cboPurType.Items.Clear()
        cboPurType.Items.Add("ALL")
        cboPurType.Items.Add("Purchase Order")
        cboPurType.Items.Add("Work Order")
        cboPurType.Items.Add("Job Order")
        cboPurType.SelectedIndex = 0

        MainClass.ClearGrid(SprdMain)
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

        MainClass.ClearGrid(SprdMain)

        SqlStr = " SELECT POMain.MKEY,POMain.AUTO_KEY_PO,POMain.AMEND_NO, POMain.PUR_TYPE||POMain.ORDER_TYPE, " & vbCrLf & " ACM.SUPP_CUST_NAME,PODetail.SERIAL_NO, " & vbCrLf & " PODetail.ITEM_CODE,DECODE(PODetail.ITEM_CODE,NULL,WO_DESCRIPTION,INVMST.Item_Short_Desc), " & vbCrLf & " PODetail.ITEM_UOM, TO_CHAR(PODetail.ITEM_QTY), " & vbCrLf & " TO_CHAR(GETMRRQTYFORPO (POMAIN.COMPANY_CODE, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE)), " & vbCrLf & " TO_CHAR(PODetail.ITEM_QTY-GETMRRQTYFORPO (POMAIN.COMPANY_CODE, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE)),DECODE(PODetail.PO_ITEM_STATUS,'N','0','1') " & vbCrLf & " FROM PUR_PURCHASE_HDR POMain, PUR_PURCHASE_DET PODetail, " & vbCrLf & " FIN_SUPP_CUST_MST ACM, INV_ITEM_MST INVMST" & vbCrLf & " WHERE POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND POMain.MKEY=PoDetail.MKEY " & vbCrLf & " AND POMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND POMain.Company_Code=INVMST.Company_Code " & vbCrLf & " AND PoDetail.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND POMain.ORDER_TYPE<>'O' AND PO_STATUS='Y' AND PO_CLOSED='N' AND PODetail.PO_ITEM_STATUS = CASE WHEN POMain.PUR_TYPE='J' THEN PODetail.PO_ITEM_STATUS ELSE 'N' END "

        'AND PODetail.PO_ITEM_STATUS='N'

        If cboPurType.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND PUR_TYPE='" & VB.Left(cboPurType.Text, 1) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " And POMain.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
            End If
        End If

        If cboStatus.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND PODetail.ITEM_QTY-GETMRRQTYFORPO (POMAIN.COMPANY_CODE, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE)=0"
        ElseIf cboStatus.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND PODetail.ITEM_QTY-GETMRRQTYFORPO (POMAIN.COMPANY_CODE, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE)<>0"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY POMain.PUR_TYPE,POMain.ORDER_TYPE,POMain.PUR_ORD_DATE,POMain.AUTO_KEY_PO"

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

            .Col = ColPOType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPOType, 4)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            '    .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            .ColsFrozen = ColPartyName

            .Col = ColSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColSNo, 4)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            '    .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 15)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemUOM, 4)

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)



            .Col = ColOrdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOrdQty, 9)

            .Col = ColRecdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRecdQty, 9)

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 9)


           
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
            .Text = "PO No."

            .Col = ColAmendNo
            .Text = "Amend No."

            .Col = ColPOType
            .Text = "PO Type"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColSNo
            .Text = "S.No."

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Desc"

            .Col = ColItemUOM
            .Text = "UOM"

            .Col = ColOrdQty
            .Text = "Order Qty."

            .Col = ColRecdQty
            .Text = "Received Qty"

            .Col = ColBalQty
            .Text = "Balance Qty."

            .Col = ColStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmShortClosing_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
            cmdSave.Enabled = True
            cmdShow.Enabled = False
        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        cmdSave.Enabled = True
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
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub
End Class
