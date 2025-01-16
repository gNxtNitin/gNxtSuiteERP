Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRejectionReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColGRDate As Short = 1
    Private Const ColGRNo As Short = 2
    Private Const ColPartyName As Short = 3
    Private Const ColItemName As Short = 4
    Private Const ColQty As Short = 5
    Private Const ColReQty As Short = 6
    Private Const ColRate As Short = 7
    Private Const ColAmount As Short = 8
    Private Const ColSendQty As Short = 9
    Private Const ColDCNo As Short = 10
    Private Const ColDNNo As Short = 11
    Private Const ColStatus As Short = 12


    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
        Else
            TxtAccount.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE IN ('C','S'))")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForRejection(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mQty As Double
        Dim mReQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mGTotal As Double

        Dim mMRRNo As Double
        Dim mSupplierCode As String
        Dim mItemCode As String
        Dim mBillNo As String = ""
        Dim mDNNo As String = ""
        Dim mDNQty As Double
        Dim mBalQty As Double
        Dim mShow As Boolean
        Dim mShowBill As Boolean

        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain()

        SqlStr = ShowRejection
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1

        If Not RsTemp.EOF Then
            With SprdMain
                Do While Not RsTemp.EOF
                    mShow = False
                    mMRRNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value)
                    mQty = Val(IIf(IsDbNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value))
                    mReQty = Val(IIf(IsDbNull(RsTemp.Fields("REOFFER_QTY").Value), 0, RsTemp.Fields("REOFFER_QTY").Value))
                    mSupplierCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    If GetSaleDetail(mMRRNo, mSupplierCode, mItemCode, mBillNo, mDNNo, mDNQty) = False Then GoTo ErrPart
                    mBalQty = mQty - mReQty - mDNQty

                    If optStatus(0).Checked = True Then
                        mShow = True
                    ElseIf optStatus(1).Checked = True Then
                        mShow = IIf(mBalQty > 0, True, False)
                    ElseIf optStatus(2).Checked = True Then
                        mShow = IIf(mBalQty <= 0, True, False)
                    End If

                    If optDNStatus(0).Checked = True Then
                        mShowBill = True
                    ElseIf optDNStatus(1).Checked = True Then
                        mShowBill = IIf(mBillNo = "", True, False)
                    ElseIf optDNStatus(2).Checked = True Then
                        mShowBill = IIf(mBillNo <> "", True, False)
                    End If

                    If mShow = True And mShowBill = True Then
                        .Row = cntRow

                        .Col = ColGRDate
                        .Text = IIf(IsDbNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value)

                        .Col = ColGRNo

                        .Text = Str(mMRRNo)

                        .Col = ColPartyName
                        .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                        .Col = ColItemName
                        .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                        .Col = ColQty
                        .Text = IIf(IsDbNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value)

                        .Col = ColReQty
                        .Text = IIf(IsDbNull(RsTemp.Fields("REOFFER_QTY").Value), 0, RsTemp.Fields("REOFFER_QTY").Value)

                        .Col = ColRate
                        .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                        mRate = Val(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value))

                        .Col = ColAmount
                        mAmount = mQty * mRate
                        .Text = VB6.Format(mAmount, "0.00") 'IIf(IsNull(RsTemp!Amount), "", RsTemp!Amount)
                        mGTotal = mGTotal + CDbl(VB6.Format(mAmount, "0.00"))

                        .Col = ColSendQty
                        .Text = VB6.Format(mDNQty, "0.00")

                        .Col = ColDCNo
                        .Text = mDNNo

                        .Col = ColDNNo
                        .Text = mBillNo

                        .Col = ColStatus
                        .Text = IIf(mBalQty <= 0, "YES", "NO")

                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                    RsTemp.MoveNext()
                Loop
            End With
        End If


        LblTotalAmt.Text = VB6.Format(mGTotal, "0.00")
        '    FormatSprdMain
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If CDbl(Trim(TxtAccount.Text)) = -1 Then
                MsgInformation("Please Select Account")
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmRejectionReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmRejectionReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("Start_Date").Value
        txtDateTo.Text = CStr(RunDate)
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Function ShowRejection() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "SELECT GRMain.MRR_DATE, GRMain.AUTO_KEY_MRR, " & vbCrLf & " ACM.SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC, " & vbCrLf & " ACM.SUPP_CUST_CODE, INVMST.ITEM_CODE, " & vbCrLf & " TO_CHAR(SUM(GRDETAIL.REJECTED_QTY)) AS REJECTED_QTY, " & vbCrLf & " TO_CHAR(GETREOFFERQTY_NEW(" & RsCompany.Fields("COMPANY_CODE").Value & ", GRMain.AUTO_KEY_MRR, GRMain.MRR_DATE, ACM.SUPP_CUST_CODE, INVMST.ITEM_CODE,GRDETAIL.REF_AUTO_KEY_NO)) AS REOFFER_QTY, " & vbCrLf & " TO_CHAR(MAX(GRDETAIL.ITEM_RATE)) AS ITEM_RATE, " & vbCrLf & " TO_CHAR(SUM(REJECTED_QTY*ITEM_RATE),'999999999.99') AS AMOUNT " & vbCrLf & " FROM " & vbCrLf & " INV_GATE_HDR GRMain, INV_GATE_DET GRDETAIL," & vbCrLf & " FIN_SUPP_CUST_MST ACM,INV_ITEM_MST INVMST " & vbCrLf & " WHERE GRMain.AUTO_KEY_MRR=GRDETAIL.AUTO_KEY_MRR" & vbCrLf & " AND GRMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND GRMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND GRDetail.Company_Code=INVMST.Company_Code " & vbCrLf & " AND GRDetail.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND GRMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MRR_STATUS='N' "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND GRDetail.REJECTED_QTY>0 "

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY GRMain.MRR_DATE, GRMain.AUTO_KEY_MRR, " & vbCrLf & " ACM.SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC,ACM.SUPP_CUST_CODE, INVMST.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " ORDER BY GRMain.MRR_DATE,GRMain.AUTO_KEY_MRR"

        ShowRejection = SqlStr

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function GetSaleDetail(ByRef pMRRNo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mBillNo As String, ByRef mDNNo As String, ByRef mDNQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsSale As ADODB.Recordset = Nothing

        GetSaleDetail = False
        mBillNo = ""
        mDNNo = ""
        mDNQty = 0

        SqlStr = "SELECT IH.VNO, IH.SALEINVOICENO, SUM(ID.ITEM_QTY) AS QTY " & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND IH.DEBITACCOUNTCODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DNCNTYPE='R'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.VNO, IH.SALEINVOICENO "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                mBillNo = mBillNo & IIf(mBillNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("SALEINVOICENO").Value), "", RsSale.Fields("SALEINVOICENO").Value)
                mDNNo = mDNNo & IIf(mDNNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("VNO").Value), "", RsSale.Fields("VNO").Value)
                mDNQty = mDNQty + IIf(IsDbNull(RsSale.Fields("QTY").Value), 0, RsSale.Fields("QTY").Value)
                RsSale.MoveNext()
            Loop
        Else
            mBillNo = ""
            mDNNo = ""
            mDNQty = 0
        End If
        GetSaleDetail = True

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain()

        Dim I As Integer

        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColGRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColGRDate, 8)

            .Col = ColGRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColGRNo, 9)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 27)
            .ColsFrozen = ColPartyName

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 20)

            For I = ColQty To ColSendQty
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8)
            Next

            .Col = ColDCNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDCNo, 9)

            .Col = ColDNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDNNo, 8)

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColStatus, 8)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColGRDate
            .Text = "MRR Date"

            .Col = ColGRNo
            .Text = "MRR No."

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColItemName
            .Text = "Item Description"

            .Col = ColQty
            .Text = "Rejected Qty"

            .Col = ColReQty
            .Text = "Re-Offer Qty"

            .Col = ColRate
            .Text = "Item Rate"

            .Col = ColAmount
            .Text = "Item Value"

            .Col = ColSendQty
            .Text = "Send Qty"

            .Col = ColDCNo
            .Text = "DN/CN No."

            .Col = ColDNNo
            .Text = "Sale Bill No"



            .Col = ColStatus
            .Text = "Status"

        End With
    End Sub
    Private Sub frmRejectionReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub optDNStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDNStatus.CheckedChanged
        If eventSender.Checked Then
            'Dim Index As Short = optDNStatus.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optStatus.CheckedChanged
        If eventSender.Checked Then
            'Dim Index As Short = OptStatus.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
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
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForRejection(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForRejection(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True


        SqlStr = ""

        If InsertPrintDummy = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Rejection Register"

        If optStatus(1).Checked = True Then
            mTitle = mTitle & " (Material Not Send)"
        ElseIf optStatus(2).Checked = True Then
            mTitle = mTitle & " (Material Send)"
        End If

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mReportFileName = "RejectionReg.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Sub
    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mGRDate As String
        Dim mGRNo As String
        Dim mPartyName As String
        Dim mItemName As String
        Dim mQty As String
        Dim mReQty As String
        Dim mRate As String
        Dim mAmount As String
        Dim mSendQty As String
        Dim mDCNo As String
        Dim mDNNo As String
        Dim mStatus As String

        InsertPrintDummy = False
        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)



        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColGRDate
                mGRDate = MainClass.AllowSingleQuote(.Text)
                .Col = ColGRNo
                mGRNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemName
                mItemName = MainClass.AllowSingleQuote(.Text)
                .Col = ColQty
                mQty = .Text
                .Col = ColReQty
                mReQty = .Text
                .Col = ColRate
                mRate = .Text
                .Col = ColAmount
                mAmount = .Text
                .Col = ColSendQty
                mSendQty = .Text
                .Col = ColDCNo
                mDCNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColDNNo
                mDNNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColStatus
                mStatus = MainClass.AllowSingleQuote(.Text)


                SqlStr = " Insert into Temp_PrintDummyData ( " & vbCrLf & " UserID, SubRow, " & vbCrLf & " Field1, Field2, Field3, " & vbCrLf & " Field4, Field5, Field6, " & vbCrLf & " Field7, Field8, Field9, " & vbCrLf & " Field10, Field11, Field12) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & cntRow & ", " & vbCrLf & " '" & mGRDate & "', '" & Trim(mGRNo) & "', '" & Trim(mPartyName) & "', " & vbCrLf & " '" & Trim(mItemName) & "', '" & Trim(mQty) & "', '" & Trim(mReQty) & "', " & vbCrLf & " '" & Trim(mRate) & "', '" & Trim(mAmount) & "','" & Trim(mSendQty) & "', " & vbCrLf & " '" & Trim(mDCNo) & "','" & Trim(mDNNo) & "','" & Trim(mStatus) & "') "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        PubDBCn.RollbackTrans()
        InsertPrintDummy = False
        MsgInformation(Err.Description)
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String


        mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
