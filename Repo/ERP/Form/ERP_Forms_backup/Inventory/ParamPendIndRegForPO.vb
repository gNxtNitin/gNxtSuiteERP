Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPendIndRegForPO
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Private PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColIndentNo As Short = 2
    Private Const colIndentDate As Short = 3
    Private Const ColDeptCode As Short = 4
    Private Const colIndEmpCode As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDesc As Short = 7
    Private Const ColUnit As Short = 8
    Private Const ColIndentQty As Short = 9
    Private Const ColPONo As Short = 10
    Private Const ColPODate As Short = 11
    Private Const colSupplier As Short = 12
    Private Const ColPOQty As Short = 13
    Private Const ColBalQty As Short = 14
    Private Const ColMKEY As Short = 15
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAppStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAppStatus.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboPriority_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPriority.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllIndentor_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllIndentor.CheckStateChanged
        Call PrintStatus(False)
        If chkAllIndentor.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtIndentor.Enabled = False
            cmdsearchIndentor.Enabled = False
        Else
            txtIndentor.Enabled = True
            cmdsearchIndentor.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonIndent(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonIndent(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonIndent(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mRPTName As String = ""

        Report1.Reset()
        mTitle = "Indent Pending Register"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If InsertPrintDummy = False Then GoTo ReportErr

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        If optShow(0).Checked = True Then
            mRPTName = "IndentPendRegSumm.RPT"
        Else
            mRPTName = "IndentPendRegDet.RPT"
        End If

        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mIndentNo As String
        Dim mIndentDate As String
        Dim mDeptCode As String
        Dim mIndEmpCode As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mIndentQty As String
        Dim mPONo As String
        Dim mPODate As String
        Dim mSupplier As String
        Dim mPOQty As String
        Dim mBalQty As String
        Dim mGroup As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow


                .Col = ColIndentNo
                mIndentNo = .Text

                .Col = colIndentDate
                mIndentDate = .Text

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                .Col = colIndEmpCode
                mIndEmpCode = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColUnit
                mUnit = Trim(.Text)

                .Col = ColIndentQty
                mIndentQty = .Text

                .Col = ColPONo
                mPONo = .Text

                .Col = ColPODate
                mPODate = .Text

                .Col = colSupplier
                mSupplier = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColPOQty
                mPOQty = .Text

                .Col = ColBalQty
                mBalQty = .Text

                If OptOrderBy(0).Checked = True Then
                    mGroup = mIndentNo
                ElseIf OptOrderBy(1).Checked = True Then
                    mGroup = mDeptCode
                ElseIf OptOrderBy(2).Checked = True Then
                    mGroup = mItemDesc
                ElseIf OptOrderBy(3).Checked = True Then
                    mGroup = mIndEmpCode
                End If

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf & " Field1,Field2,Field3,Field4,Field5," & vbCrLf & " Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11,Field12,Field13,Field14) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & CntRow & ", " & vbCrLf & " '" & mGroup & "', " & vbCrLf & " '" & mIndentNo & "', " & vbCrLf & " '" & mIndentDate & "', " & vbCrLf & " '" & mDeptCode & "', " & vbCrLf & " '" & mIndEmpCode & "', " & vbCrLf & " '" & mItemCode & "', " & vbCrLf & " '" & mItemDesc & "', " & vbCrLf & " '" & mUnit & "', " & vbCrLf & " '" & mIndentQty & "','" & mPONo & "', " & vbCrLf & " '" & mPODate & "','" & mSupplier & "', " & vbCrLf & " '" & mPOQty & "','" & mBalQty & "') "


                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        '    Resume
        MsgInformation(Err.Description)
        InsertPrintDummy = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW,Field1"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)


        Dim mIsSurpress As String = ""
        Dim mTitleName As String = ""

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        If OptOrderBy(0).Checked = True Then
            mIsSurpress = "Y"
            mTitleName = ""
        ElseIf OptOrderBy(1).Checked = True Then
            mIsSurpress = "N"
            mTitleName = "DEPT :"
        ElseIf OptOrderBy(2).Checked = True Then
            mIsSurpress = "N"
            mTitleName = "ITEM NAME :"
        ElseIf OptOrderBy(3).Checked = True Then
            mIsSurpress = "N"
            mTitleName = "INDENTOR :"
        End If

        MainClass.AssignCRptFormulas(Report1, "IsSurpress=""" & mIsSurpress & """")
        MainClass.AssignCRptFormulas(Report1, "TitleName=""" & mTitleName & """")

        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdsearchIndentor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchIndentor.Click
        SearchIndentor()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPendIndRegForPO_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Pending Indent Register - (For PO)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPendIndRegForPO_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdsearch.Enabled = False

        txtIndentor.Enabled = False
        cmdsearchIndentor.Enabled = False

        Call PrintStatus(True)
        Call FillIndentCombo()
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamPendIndRegForPO_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPendIndRegForPO_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub



    Private Sub optOrderBy_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptOrderBy.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optOrderBy.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset=Nothing= Nothing=Nothing = Nothing

        'Dim xVDate As String
        'Dim xMkey As String
        'Dim xVNo As String
        'Dim xBookType As String
        'Dim xBookSubType As String


        '    SprdMain.Row = SprdMain.ActiveRow
        '
        '    SprdMain.Col = ColMkey
        '    xMkey = Me.SprdMain.Text
        '    sqlstr = "SELECT * from FIN_INVOICE_HDR WHERE MKEY='" & xMkey & "'"
        '    MainClass.UOpenRecordSet sqlstr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        xVDate = RsTemp!INVOICE_DATE
        '        xVNo = RsTemp!BILLNO
        '
        '    Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "")
        '    End If
    End Sub

    Private Sub txtIndentor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentor.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtIndentor_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentor.DoubleClick
        SearchIndentor()
    End Sub


    Private Sub txtIndentor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIndentor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIndentor.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIndentor_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIndentor.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchIndentor()
    End Sub

    Private Sub txtIndentor_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIndentor.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtIndentor.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtIndentor.Text), "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtIndentor.Text = UCase(Trim(txtIndentor.Text))
        Else
            MsgInformation("No Such Employee in Employee Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchIndentor()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtIndentor.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr)
        If AcName <> "" Then
            txtIndentor.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtItemName.Text = UCase(Trim(txtItemName.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.3)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColIndentNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColIndentNo, 9)

            .Col = colIndentDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colIndentDate, 8)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptCode, 5)

            .Col = colIndEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colIndEmpCode, 10)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 7)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 28)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            .Col = ColIndentQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColIndentQty, 8)

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalQty, 8)


            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPONo, 9)
            If OptShow(0).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPODate, 8)
            If OptShow(0).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 25)
            If OptShow(0).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColPOQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColPOQty, 8)
            If OptShow(0).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            'SprdMain.DAutoCellTypes = True
            'SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            'SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim mSqlStr As String
        Dim RsPO As ADODB.Recordset = Nothing
        Dim mItemCode As String = ""

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTempTable = False Then GoTo LedgError
        SqlStr = MakeSQL
        If optShow(0).Checked = True Then
            MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        End If

        CntRow = 1
        If RsShow.EOF = False Then
            With SprdMain
                Do While Not RsShow.EOF
                    .MaxRows = CntRow
                    .Row = CntRow

                    .Col = ColLocked
                    .Text = ""

                    .Col = ColIndentNo
                    .Text = CStr(IIf(IsDbNull(RsShow.Fields("AUTO_KEY_INDENT").Value), "", RsShow.Fields("AUTO_KEY_INDENT").Value))

                    .Col = colIndentDate
                    .Text = VB6.Format(IIf(IsDbNull(RsShow.Fields("INDENT_DATE").Value), "", RsShow.Fields("INDENT_DATE").Value), "DD/MM/YYYY")

                    .Col = ColDeptCode
                    .Text = IIf(IsDbNull(RsShow.Fields("DEPT_CODE").Value), "", RsShow.Fields("DEPT_CODE").Value)

                    .Col = colIndEmpCode
                    .Text = IIf(IsDbNull(RsShow.Fields("EMP_NAME").Value), "", RsShow.Fields("EMP_NAME").Value)

                    .Col = ColItemCode
                    .Text = IIf(IsDbNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)

                    .Col = ColItemDesc
                    .Text = IIf(IsDbNull(RsShow.Fields("ITEM_SHORT_DESC").Value), "", RsShow.Fields("ITEM_SHORT_DESC").Value)

                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RsShow.Fields("ITEM_UOM").Value), "", RsShow.Fields("ITEM_UOM").Value)

                    .Col = ColIndentQty
                    .Text = IIf(IsDbNull(RsShow.Fields("INDENT_QTY").Value), "", RsShow.Fields("INDENT_QTY").Value)

                    .Col = ColBalQty
                    .Text = IIf(IsDbNull(RsShow.Fields("BAL_QTY").Value), "", RsShow.Fields("BAL_QTY").Value)

                    .Col = ColMKEY
                    .Text = IIf(IsDbNull(RsShow.Fields("AUTO_KEY_INDENT").Value), "", RsShow.Fields("AUTO_KEY_INDENT").Value)

                    mSqlStr = "SELECT IH.AUTO_KEY_PO,IH.PUR_ORD_DATE,IH.SUPP_CUST_CODE, " & vbCrLf _
                        & " SUM(IT.INDENT_QTY) AS ITEM_QTY FROM " & vbCrLf _
                        & " PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, PUR_POCONS_IND_TRN IT " & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                        & " AND ID.MKEY=IT.MKEY AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND ID.ITEM_CODE=IT.ITEM_CODE AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" & vbCrLf _
                        & " AND IT.AUTO_KEY_INDENT=" & RsShow.Fields("AUTO_KEY_INDENT").Value & " " & vbCrLf _
                        & " AND IT.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf _
                        & " GROUP BY IH.AUTO_KEY_PO,IH.PUR_ORD_DATE,IH.SUPP_CUST_CODE"

                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsPO.EOF = False Then
                        Do While Not RsPO.EOF
                            .Row = CntRow

                            .Col = ColIndentNo
                            .Text = CStr(IIf(IsDbNull(RsShow.Fields("AUTO_KEY_INDENT").Value), "", RsShow.Fields("AUTO_KEY_INDENT").Value))

                            .Col = colIndentDate
                            .Text = VB6.Format(IIf(IsDbNull(RsShow.Fields("INDENT_DATE").Value), "", RsShow.Fields("INDENT_DATE").Value), "DD/MM/YYYY")

                            .Col = ColDeptCode
                            .Text = IIf(IsDbNull(RsShow.Fields("DEPT_CODE").Value), "", RsShow.Fields("DEPT_CODE").Value)

                            .Col = colIndEmpCode
                            .Text = IIf(IsDbNull(RsShow.Fields("EMP_NAME").Value), "", RsShow.Fields("EMP_NAME").Value)

                            .Col = ColItemCode
                            .Text = IIf(IsDbNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)

                            .Col = ColItemDesc
                            .Text = IIf(IsDbNull(RsShow.Fields("ITEM_SHORT_DESC").Value), "", RsShow.Fields("ITEM_SHORT_DESC").Value)

                            .Col = ColUnit
                            .Text = IIf(IsDbNull(RsShow.Fields("ITEM_UOM").Value), "", RsShow.Fields("ITEM_UOM").Value)

                            .Col = ColPONo
                            .Text = CStr(IIf(IsDbNull(RsPO.Fields("AUTO_KEY_PO").Value), "", RsPO.Fields("AUTO_KEY_PO").Value))

                            .Col = ColPODate
                            .Text = VB6.Format(IIf(IsDbNull(RsPO.Fields("PUR_ORD_DATE").Value), "", RsPO.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")


                            .Col = colSupplier
                            If MainClass.ValidateWithMasterTable(RsPO.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                .Text = MasterNo
                            End If

                            .Col = ColPOQty
                            .Text = VB6.Format(IIf(IsDbNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value), "0.00")

                            RsPO.MoveNext()
                            If RsPO.EOF = False Then
                                CntRow = CntRow + 1
                                .MaxRows = CntRow
                            End If
                        Loop
                    End If

                    RsShow.MoveNext()
                    CntRow = CntRow + 1
                Loop
            End With
        End If
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        'Resume
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mIndentor As String
        Dim mDivision As Double

        ''SELECT CLAUSE...

        MakeSQL = " SELECT '',IH.AUTO_KEY_INDENT, IH.INDENT_DATE, " & vbCrLf _
            & " IH.DEPT_CODE, EMP.EMP_NAME, IH.ITEM_CODE," & vbCrLf _
            & " INVMST.ITEM_SHORT_DESC, IH.ITEM_UOM, " & vbCrLf _
            & " TO_CHAR(IH.INDENT_QTY) INDENT_QTY, " & vbCrLf _
            & " '', '', '',TO_CHAR(PO_QTY), " & vbCrLf _
            & " TO_CHAR(IH.INDENT_QTY - NVL(PO_QTY,0)) BAL_QTY,IH.AUTO_KEY_INDENT"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM TEMP_INDENT_REG IH,  " & vbCrLf & " INV_ITEM_MST INVMST, ATH_PASSWORD_MST EMP"

        '', FIN_SUPP_CUST_MST CMST

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
            & " AND IH.IND_EMP_CODE=EMP.USER_ID"

        If CboStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND (IH.INDENT_QTY - NVL(PO_QTY,0) > 0 AND IH.INDENT_STATUS = 'N')"
        ElseIf CboStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.INDENT_QTY - NVL(PO_QTY,0) <= 0 "
        ElseIf CboStatus.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.INDENT_QTY - NVL(PO_QTY,0) > 0 AND IH.INDENT_STATUS = 'Y'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If


        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If


        If chkAllIndentor.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtIndentor.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mIndentor = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.IND_EMP_CODE='" & MainClass.AllowSingleQuote(mIndentor) & "'"
            End If
        End If

        If cboAppStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_STATUS = 'Y'"
        ElseIf cboAppStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_STATUS = 'N'"
        End If


        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.INDENT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INDENT_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"




        ''ORDER CLAUSE...
        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.AUTO_KEY_INDENT, IH.INDENT_DATE,IH.SERIAL_NO"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.DEPT_CODE,IH.AUTO_KEY_INDENT, IH.INDENT_DATE,INVMST.ITEM_SHORT_DESC"
        ElseIf OptOrderBy(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IH.AUTO_KEY_INDENT, IH.INDENT_DATE"
        ElseIf OptOrderBy(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY EMP.EMP_NAME,INVMST.ITEM_SHORT_DESC, IH.AUTO_KEY_INDENT, IH.INDENT_DATE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
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
    Private Sub FillIndentCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        cboDept.Items.Clear()

        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDept.SelectedIndex = 0

        cboPriority.Items.Clear()
        cboPriority.Items.Add("ALL")
        cboPriority.Items.Add("Regular")
        cboPriority.Items.Add("Urgent")
        cboPriority.Items.Add("Most Urgent")
        cboPriority.SelectedIndex = 0

        cboAppStatus.Items.Clear()
        cboAppStatus.Items.Add("BOTH")
        cboAppStatus.Items.Add("Approval")
        cboAppStatus.Items.Add("Non Approval")
        cboAppStatus.SelectedIndex = 1

        CboStatus.Items.Clear()
        CboStatus.Items.Add("Both")
        CboStatus.Items.Add("Pending")
        CboStatus.Items.Add("Complete")
        CboStatus.Items.Add("Short Closed")
        CboStatus.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo InsertErr


        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mDept As String
        Dim mDivision As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_INDENT_REG NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr1 = ""
        SqlStr2 = ""

        SqlStr1 = " INSERT INTO TEMP_INDENT_REG (" & vbCrLf _
            & " USERID, COMPANY_CODE, AUTO_KEY_INDENT, INDENT_DATE, " & vbCrLf _
            & " DEPT_CODE , SERIAL_NO, ITEM_CODE, ITEM_UOM, INDENT_STATUS, " & vbCrLf _
            & " APP_STATUS, INDENT_QTY, PO_QTY, IND_EMP_CODE,DIV_CODE) "



        SqlStr2 = " SELECT " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', IH.COMPANY_CODE, IH.AUTO_KEY_INDENT, IH.INDENT_DATE," & vbCrLf _
            & " IH.DEPT_CODE , ID.SERIAL_NO, ID.ITEM_CODE, ID.ITEM_UOM, ID.INDENT_STATUS, " & vbCrLf _
            & " IH.APPROVAL_STATUS, " & vbCrLf _
            & " ID.REQ_QTY, GETPOIndentQty(IH.COMPANY_CODE,IH.AUTO_KEY_INDENT,ID.ITEM_CODE) AS POQTY, IH.IND_EMP_CODE, IH.DIV_CODE" & vbCrLf _
            & " FROM PUR_INDENT_HDR IH, PUR_INDENT_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT AND IH.HOD_EMP_CODE IS NOT NULL"

        ''CASE WHEN IH.APP_EMP_CODE IS NULL  OR IH.APP_EMP_CODE='' THEN 'N' ELSE 'Y' END
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr2 = SqlStr2 & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                SqlStr2 = SqlStr2 & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                SqlStr2 = SqlStr2 & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboPriority.Text <> "ALL" Then
            SqlStr2 = SqlStr2 & vbCrLf & "AND ID.PRIORITY_LEVEL='" & VB.Left(cboPriority.Text, 1) & "'"
        End If

        If cboAppStatus.SelectedIndex = 1 Then
            SqlStr2 = SqlStr2 & vbCrLf & " AND IH.APPROVAL_STATUS = 'Y'"
        ElseIf cboAppStatus.SelectedIndex = 2 Then
            SqlStr2 = SqlStr2 & vbCrLf & " AND IH.APPROVAL_STATUS = 'N'"
        End If

        '    If cboStatus.ListIndex = 1 Then
        '        SqlStr2 = SqlStr2 & vbCrLf & " AND ID.INDENT_STATUS = 'N'"
        '    ElseIf cboStatus.ListIndex = 2 Then
        '        SqlStr2 = SqlStr2 & vbCrLf & " AND ID.INDENT_STATUS = 'Y'"
        '    End If

        SqlStr2 = SqlStr2 & vbCrLf _
            & " AND IH.INDENT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INDENT_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr1 & SqlStr2

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        InsertIntoTempTable = True

        Exit Function
InsertErr:
        PubDBCn.RollbackTrans() ''
        InsertIntoTempTable = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

        ''Resume
    End Function
End Class
