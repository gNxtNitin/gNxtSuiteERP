Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmIndentShortClosing
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColIndentNo As Short = 1
    Private Const colIndentDate As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemName As Short = 4
    Private Const ColItemUOM As Short = 5
    Private Const ColIndentQty As Short = 6
    Private Const ColOrderQty As Short = 7
    Private Const ColBalQty As Short = 8
    Private Const ColStatus As Short = 9

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        CmdSave.Enabled = False
        cmdShow.Enabled = True
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
        CmdSave.Enabled = False
        cmdShow.Enabled = True
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


        Report1.Reset()
        mTitle = "Indented Items Short Closing"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IndentShortReg.RPT"

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColBalQty, PubDBCn) = False Then GoTo ReportErr

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        If UCase(Trim(cboDept.Text)) <> "ALL" Then
            MainClass.AssignCRptFormulas(Report1, "mDept=""" & UCase(Trim(cboDept.Text)) & """")
        End If
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mIndentNo As Double
        Dim mUpdateCount As Integer
        Dim mItemCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColIndentNo
                mIndentNo = CDbl(Trim(.Text))

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    SqlStr = "UPDATE PUR_INDENT_DET SET INDENT_STATUS='Y'" & vbCrLf & " WHERE AUTO_KEY_INDENT=" & mIndentNo & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE PUR_INDENT_HDR SET " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_INDENT=" & mIndentNo & ""
                    PubDBCn.Execute(SqlStr)

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

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If InsertIntoTempTable() = False Then GoTo ErrPart
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = False
        CmdSave.Enabled = True
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        cmdShow.Enabled = True
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Public Sub frmIndentShortClosing_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain()
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmIndentShortClosing_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        Call FillIndentCombo()
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

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

        SqlStr = " SELECT IH.AUTO_KEY_INDENT, IH.INDENT_DATE, IH.ITEM_CODE, " & vbCrLf & " INVMST.Item_Short_Desc, IH.ITEM_UOM, " & vbCrLf & " TO_CHAR(IH.INDENT_QTY), " & vbCrLf & " TO_CHAR(IH.PO_QTY), " & vbCrLf & " TO_CHAR(IH.INDENT_QTY-IH.PO_QTY),'' "

        SqlStr = SqlStr & vbCrLf & " FROM TEMP_INDENT_REG IH, " & vbCrLf & " INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.Company_Code=INVMST.Company_Code " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.INDENT_STATUS='N'" & vbCrLf & " AND IH.APP_STATUS='Y' " & vbCrLf & " AND IH.INDENT_QTY<>NVL(IH.PO_QTY,0)"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.AUTO_KEY_INDENT,IH.ITEM_CODE,IH.SERIAL_NO"
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo InsertErr


        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mDept As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_INDENT_REG NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr1 = ""
        SqlStr2 = ""

        SqlStr1 = " INSERT INTO TEMP_INDENT_REG (" & vbCrLf & " USERID, COMPANY_CODE, AUTO_KEY_INDENT, INDENT_DATE, " & vbCrLf & " DEPT_CODE , SERIAL_NO, ITEM_CODE, ITEM_UOM, INDENT_STATUS, " & vbCrLf & " APP_STATUS, INDENT_QTY, PO_QTY) "



        SqlStr2 = " SELECT " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', IH.COMPANY_CODE, IH.AUTO_KEY_INDENT, IH.INDENT_DATE," & vbCrLf _
            & " IH.DEPT_CODE , ID.SERIAL_NO, ID.ITEM_CODE, ID.ITEM_UOM, ID.INDENT_STATUS, " & vbCrLf _
            & " IH.APPROVAL_STATUS, ID.REQ_QTY, GETMRRPOIndentQty(IH.COMPANY_CODE,IH.AUTO_KEY_INDENT,ID.ITEM_CODE) AS POQTY" & vbCrLf _
            & " FROM PUR_INDENT_HDR IH, PUR_INDENT_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT " & vbCrLf _
            & " AND IH.HOD_EMP_CODE IS NOT NULL" & vbCrLf _
            & " AND IH.APPROVAL_STATUS = 'Y'"

        If Val(TxtIndentNo.Text) > 0 Then
            SqlStr2 = SqlStr2 & vbCrLf & "AND IH.AUTO_KEY_INDENT=" & Val(TxtIndentNo.Text) & ""
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                SqlStr2 = SqlStr2 & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        SqlStr2 = SqlStr2 & vbCrLf & " AND IH.INDENT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INDENT_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


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
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColIndentNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIndentNo, 9)

            .Col = colIndentDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(colIndentDate, 10)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            '    .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 23)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemUOM, 4)


            .Col = ColIndentQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIndentQty, 9)

            .Col = ColOrderQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOrderQty, 9)

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 9)


            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

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

            .Col = ColIndentNo
            .Text = "Indent No."

            .Col = colIndentDate
            .Text = "Indent Date"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Desc"

            .Col = ColItemUOM
            .Text = "UOM"

            .Col = ColIndentQty
            .Text = "Indented Qty."

            .Col = ColOrderQty
            .Text = "Recd. Qty"

            .Col = ColBalQty
            .Text = "Balance Qty."

            .Col = ColStatus
            .Text = "Status"
        End With
    End Sub
    Private Sub frmIndentShortClosing_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub FillIndentCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

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
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        CmdSave.Enabled = False
        cmdShow.Enabled = True
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        CmdSave.Enabled = False
        cmdShow.Enabled = True
    End Sub

    Private Sub TxtIndentNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIndentNo.TextChanged
        CmdSave.Enabled = False
        cmdShow.Enabled = True
    End Sub


    Private Sub TxtIndentNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtIndentNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
