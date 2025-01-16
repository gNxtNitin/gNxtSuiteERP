Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAmended_DNCN
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColVMKEY As Short = 1
    Private Const ColPartyName As Short = 2
    Private Const ColVNo As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColBillDate As Short = 6
    Private Const ColMRRNo As Short = 7
    Private Const ColMRRDate As Short = 8
    Private Const ColPMKEY As Short = 9
    Private Const ColPONo As Short = 10
    Private Const ColWEF As Short = 11
    Private Const ColItemCode As Short = 12
    Private Const ColItemName As Short = 13
    Private Const ColBillQty As Short = 14
    Private Const ColApprovedQty As Short = 15
    Private Const ColBillRate As Short = 16
    Private Const ColPORate As Short = 17
    Private Const ColRateDiff As Short = 18
    Private Const ColAmount As Short = 19
    Private Const ColBookType As Short = 20
    Private Const ColBookSubType As Short = 21
    Private Const ColDNCNNo As Short = 22
    Private Const ColDNCNDate As Short = 23
    Private Const ColStatus As Short = 24

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboStatus.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboStatus.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
        Else
            TxtAccount.Enabled = True
        End If
    End Sub

    Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
        cmdShow.Enabled = True
        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
        Else
            txtItemName.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then
        '        PvtDBCn.Close
        '        Set PvtDBCn = Nothing
        '    End If
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        PubDBCn.Errors.Clear()

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""


        Call FillPrintDummy()

        '''''Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        mSubTitle = ""

        mRPTName = "RateDiff.Rpt"
        mTitle = "Rate Diff Bill after PO Amend"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
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

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = " SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW,Field1"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub FillPrintDummy()


        Dim SqlStr As String
        Dim cntRow As Integer

        Dim mPartyName As String
        Dim mPONo As String
        Dim mWef As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mMRRNO As String
        Dim mMRRDATE As String
        Dim mVNo As String
        Dim mVDate As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim mBillQty As Double
        Dim mApprovedQty As Double
        Dim mBillRate As Double
        Dim mPORate As Double
        Dim mRateDiff As Double
        Dim mAmount As Double
        Dim mPMKEY As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(.Text)

                .Col = ColVNo
                mVNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColVDate
                mVDate = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillDate
                mBillDate = MainClass.AllowSingleQuote(.Text)

                .Col = ColMRRNo
                mMRRNO = MainClass.AllowSingleQuote(.Text)

                .Col = ColMRRDate
                mMRRDATE = MainClass.AllowSingleQuote(.Text)

                .Col = ColPONo
                mPONo = MainClass.AllowSingleQuote(.Text)

                .Col = ColWEF
                mWef = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemName
                mItemName = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillQty
                mBillQty = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColApprovedQty
                mApprovedQty = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColBillRate
                mBillRate = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColPORate
                mPORate = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColRateDiff
                mRateDiff = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColAmount
                mAmount = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColPMKEY
                mPMKEY = Trim(.Text)

                If Val(CStr(mRateDiff)) <> 0 Then
                    SqlStr = "INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, " & vbCrLf & " FIELD4, FIELD5, FIELD6, " & vbCrLf & " FIELD7, FIELD8, FIELD9, " & vbCrLf & " FIELD10, FIELD11, FIELD12, " & vbCrLf & " FIELD13, FIELD14, Field15, " & vbCrLf & " FIELD16, FIELD17, FIELD18 " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & cntRow & ", " & vbCrLf & " '" & mPartyName & "', '" & mPONo & "', '" & mWef & "'," & vbCrLf & " '" & mBillNo & "', '" & mBillDate & "', '" & mMRRNO & "'," & vbCrLf & " '" & mMRRDATE & "', '" & mVNo & "', '" & mVDate & "', " & vbCrLf & " '" & mItemCode & "', '" & mItemName & "', '" & mBillQty & "'," & vbCrLf & " '" & mApprovedQty & "', '" & mBillRate & "', '" & mPORate & "'," & vbCrLf & " '" & mRateDiff & "', '" & mAmount & "','" & mPMKEY & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mMkey As Double
        Dim mPOMkey As Double
        Dim mItemCode As String
        Dim mUpdateCount As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                If Trim(.Text) = "" Then GoTo NextRow

                .Col = ColVMKEY
                mMkey = CDbl(Trim(.Text))

                .Col = ColPMKEY
                mPOMkey = CDbl(Trim(.Text))

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    SqlStr = "UPDATE FIN_DNCN_AMEND SET IS_DNCN_MADE='Y'" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND VMKEY='" & mMkey & "'" & vbCrLf & " AND POMKEY='" & mPOMkey & "'" & vbCrLf & " AND ITEM_CODE='" & mItemCode & "'" & vbCrLf
                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Posted.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()
        Call DisplayTotal()
        FormatSprdMain()
        '    CmdShow.Enabled = False
    End Sub
    Private Sub DisplayTotal()
        On Error GoTo DisplayErr
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mNextPartyName As String
        Dim mPartyName As String
        Dim mTotalAmount As Double

        cntRow = 1
        With SprdMain
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColPartyName
                mPartyName = .Text

                .Col = ColAmount
                mTotalAmount = mTotalAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                cntRow = cntRow + 1
                .Row = cntRow
                .Col = ColPartyName
                mNextPartyName = .Text
                If mPartyName <> mNextPartyName Then
                    .Row = cntRow

                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    Call GridTotal("Total :", mTotalAmount, cntRow)
                    mTotalAmount = 0
                    cntRow = cntRow + 1

                End If
            Loop

            .MaxRows = .MaxRows + 1
            Call GridTotal("Total :", mTotalAmount, .MaxRows)

            '        .MaxRows = .MaxRows + 1
            '        Call GridTotal("Grand Total :", mTotalAmount, .MaxRows)

        End With


        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub GridTotal(ByRef mTotalString As String, ByRef mTotalAmount As Double, ByRef mRow As Integer)

        With SprdMain
            .Row = mRow
            .Col = ColPartyName
            .Text = mTotalString
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColAmount
            .Text = VB6.Format(mTotalAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)


            .Row = mRow
            .Row2 = mRow
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H80FF80) ''&HFFFF00
            .BlockMode = False

        End With
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

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Please Select Item")
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please Select Valid Item")
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmAmended_DNCN_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmAmended_DNCN_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Width = VB6.TwipsToPixelsX(11355)

        chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Decrease Rate")
        cboShow.Items.Add("Increase Rate")
        cboShow.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("ALL")
        cboStatus.Items.Add("Complete")
        cboStatus.Items.Add("Pending")
        cboStatus.SelectedIndex = 0


        FormatSprdMain()
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        Call frmAmended_DNCN_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSuppCustCode As String

        SqlStr = "SELECT  IH.VMKEY, " & vbCrLf & " CMST.SUPP_CUST_NAME, IH.VNO, " & vbCrLf & " IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE, IH.POMKEY, " & vbCrLf & " IH.AUTO_KEY_PO, IH.AMEND_WEF_DATE, IH.ITEM_CODE, " & vbCrLf & " ITEMMST.ITEM_SHORT_DESC, " & vbCrLf & " IH.BILL_QTY, IH.APPROVED_QTY, IH.BILL_RATE, " & vbCrLf & " IH.PO_RATE, DIFF_RATE , " & vbCrLf & " TO_CHAR(NVL(IH.DIFF_RATE,0)*IH.APPROVED_QTY), IH.BOOKTYPE, IH.BOOKSUBTYPE, " & vbCrLf & " IH.DNCN_NO, IH.DNCN_DATE, CASE WHEN IH.IS_DNCN_MADE='Y' THEN '1' ELSE '0' END AS Status "

        ''TO_CHAR(NVL(IH.BILL_RATE,0)-NVL(IH.PO_RATE,0))
        ''TO_CHAR((NVL(IH.BILL_RATE,0)-NVL(IH.PO_RATE,0))*IH.APPROVED_QTY)

        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_AMEND IH, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST"

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.Company_Code=CMST.Company_Code " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.Company_Code=ITEMMST.Company_Code " & vbCrLf & " AND IH.ITEM_CODE=ITEMMST.ITEM_CODE " & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ITEMMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'"
        End If

        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(IH.BILL_RATE,0)-NVL(IH.PO_RATE,0)>0"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(IH.BILL_RATE,0)-NVL(IH.PO_RATE,0)<0"
        End If

        If cboStatus.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.IS_DNCN_MADE='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.IS_DNCN_MADE='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME, " & vbCrLf & " IH.AUTO_KEY_PO, IH.AMEND_WEF_DATE, " & vbCrLf & " ITEMMST.ITEM_SHORT_DESC, IH.MRRDATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColVMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVMKEY, 11)
            .ColHidden = True

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 25)
            If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVNo, 6)
            .ColHidden = False ''True

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVDate, 8)
            .ColHidden = False ''True

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillNo, 6)
            .ColHidden = False ''True

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillDate, 8)
            .ColHidden = False ''True
            .ColsFrozen = ColBillDate

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRNo, 11)
            .ColHidden = True

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRDate, 8)
            .ColHidden = False ''True

            .Col = ColPMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPMKEY, 11)
            .ColHidden = True

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPONo, 9)

            .Col = ColWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColWEF, 8)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCode, 8)
            If chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemName, 25)
            If chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            For cntCol = ColBillQty To ColAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, IIf(cntCol = ColAmount, 10, 8))
            Next

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookType, 11)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookSubType, 11)
            .ColHidden = True

            .Col = ColDNCNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDNCNNo, 11)
            .ColHidden = True

            .Col = ColDNCNDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDNCNDate, 11)
            .ColHidden = True


            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 8)
            '    .Value = vbUnchecked

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColStatus) ''ColDNCNDate
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColVMKEY
            .Text = "MKey"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColVDate
            .Text = "VDate"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColMRRNo
            .Text = "MRR No"

            .Col = ColMRRDate
            .Text = "MRR Date"

            .Col = ColPMKEY
            .Text = "PO MKey"

            .Col = ColPONo
            .Text = "PO No"

            .Col = ColWEF
            .Text = "WEF"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Name"

            .Col = ColBillQty
            .Text = "Bill Qty"

            .Col = ColApprovedQty
            .Text = "Approved Qty"

            .Col = ColBillRate
            .Text = "Bill Rate"

            .Col = ColPORate
            .Text = "PO Rate"

            .Col = ColRateDiff
            .Text = "Rate Diff"

            .Col = ColAmount
            .Text = "Amount"

            .Col = ColBookType
            .Text = "BookType"

            .Col = ColBookSubType
            .Text = "Book SubType"

            .Col = ColDNCNNo
            .Text = "DN/CN No"

            .Col = ColDNCNDate
            .Text = "DN/CN Date"

            .Col = ColStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmAmended_DNCN_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then
        '        PvtDBCn.Close
        '        Set PvtDBCn = Nothing
        '    End If
        Me.Hide()
        Me.Dispose()
        Me.Close()
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

    Private Sub SearchItem()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        If AcName <> "" Then
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColVDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColVMKEY
        xMkey = Me.SprdMain.Text

        SprdMain.Col = ColVNo
        xVNo = Me.SprdMain.Text

        Call ShowTrn(xMkey, xVDate, "", xVNo, "P", "", Me)

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
        Dim SqlStr As String
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


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        cmdShow.Enabled = True
    End Sub


    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        SearchItem()
    End Sub


    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub


    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        On Error GoTo ERR1
        If txtItemName.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtItemName.Text = UCase(Trim(txtItemName.Text))
        Else
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
