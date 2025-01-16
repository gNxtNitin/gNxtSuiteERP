Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDDrApproval
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColCustomerName As Short = 2
    Private Const ColBillNO As Short = 3
    Private Const ColBillDate As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemName As Short = 6
    Private Const ColShortQty As Short = 7
    Private Const ColPostStatus As Short = 8

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        TxtAccount.Enabled = IIf(ChkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
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
        Dim mPONo As Double
        Dim mWEF As String
        Dim mSupplier As String
        Dim mUpdateCount As Integer
        Dim mPOAmendNo As Integer
        Dim mCanPostPO As Boolean
        Dim mPOSeq As Double
        Dim mAuthorisation As String
        Dim mBillNO As String
        Dim mBillDate As String

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            MsgBox("You have no Right to Post PO. ", MsgBoxStyle.Critical)
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                mCanPostPO = False
                .Row = cntRow

                .Col = ColMKEY
                mMKey = CDbl(Trim(.Text))

                .Col = ColBillNO
                mBillNO = Trim(.Text)

                .Col = ColBillDate
                mBillDate = Trim(.Text)

                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    If lblBookType.Text = "1" Then
                        SqlStr = "INSERT INTO FIN_DDR_APPROVAL_MST " & vbCrLf _
                                & " ( COMPANY_CODE, BILL_NO, BILL_DATE, FIRST_APP,SECOND_APP, ADDUSER, ADDDATE )" & vbCrLf _
                                & " VALUES (" & vbCrLf _
                                & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mBillNO & "', TO_DATE('" & VB6.Format(mBillDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                                & " 'Y','N','" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"

                    Else
                        SqlStr = "UPDATE FIN_DDR_APPROVAL_MST SET SECOND_APP='Y'," & vbCrLf _
                               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                               & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                               & " WHERE BILL_NO='" & mBillNO & "'" & vbCrLf _
                               & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIRST_APP='Y'"
                    End If

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1

                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " DDR Approved.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification() = False Then Exit Sub
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

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Select Account")
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmDDrApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmDDrApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked

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
        Dim mSuppCustCode As String

        SqlStr = " SELECT IH.MKEY, CMST.SUPP_CUST_NAME, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE, INV.ITEM_SHORT_DESC, ITEM_SHORT_RECD_QTY,'' " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INV" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                    & " AND IH.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                    & " AND ID.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                    & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                    & " AND ITEM_SHORT_RECD_QTY>0"

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If


        If lblBookType.Text = "1" Then
            SqlStr = SqlStr & vbCrLf & " And IH.BILLNO NOT IN (SELECT BILL_NO FROM FIN_DDR_APPROVAL_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIRST_APP='Y')"
        Else
            SqlStr = SqlStr & vbCrLf & " And IH.BILLNO IN (SELECT BILL_NO FROM FIN_DDR_APPROVAL_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIRST_APP='Y' AND SECOND_APP='Y')"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INVOICE_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColPostStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 11)
            .ColHidden = True


            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 18)

            .Col = ColBillNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillNO, 12)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillDate, 9)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemName, 15)



            .Col = ColShortQty
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColShortQty, 8)


            .Col = ColPostStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColPostStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColShortQty)
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
            .Text = "MKey"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColBillNO
            .Text = "Bill NO"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Name"

            .Col = ColShortQty
            .Text = "Short Qty"

            .Col = ColPostStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmDDrApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColPostStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim xPoNo As Double
        'Dim xAmendPONo As Double
        'Dim mOrderType As String
        'Dim mWEF As String

        ''Dim ss As New frmPO

        'SprdMain.Row = SprdMain.ActiveRow

        'SprdMain.Col = ColPONo
        'xPoNo = Val(SprdMain.Text)

        'SprdMain.Col = ColPOAmendNo
        'xAmendPONo = Val(SprdMain.Text)

        'SprdMain.Col = ColWEF

        'SqlStr = "SELECT * from DSP_SALEORDER_HDR WHERE AUTO_KEY_SO=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        'If RsTemp.EOF = False Then
        '    mWEF = VB6.Format(IIf(IsDBNull(RsTemp.Fields("AMEND_WEF_FROM").Value), "", RsTemp.Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")
        '    mOrderType = IIf(IsDBNull(RsTemp.Fields("ORDER_TYPE").Value), "O", RsTemp.Fields("ORDER_TYPE").Value)

        '    frmSalesOrderGST.MdiParent = Me.MdiParent

        '    frmSalesOrderGST.lblType.Text = mOrderType
        '    frmSalesOrderGST.lblAddItem.Text = "N"
        '    frmSalesOrderGST.Show()
        '    frmSalesOrderGST.frmSalesOrderGST_Activated(Nothing, New System.EventArgs())


        '    frmSalesOrderGST.txtSONo.Text = RsTemp.Fields("AUTO_KEY_SO").Value
        '    frmSalesOrderGST.txtAmendNo.Text = RsTemp.Fields("AMEND_NO").Value

        '    'frmSalesOrderGST.Show()
        '    'frmSalesOrderGST.frmSalesOrderGST_Activated(Nothing, New System.EventArgs())

        '    frmSalesOrderGST.txtSONo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        'End If
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

    Private Sub frmDDrApproval_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 6 Then
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
            If MainClass.SearchIntoFullGrid(SprdMain, ColBillNO, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPostStatus)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

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
            If MainClass.SearchIntoFullGrid(SprdMain, ColBillNO, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPostStatus)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
End Class
