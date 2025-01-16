Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPendIndRegDeptWise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Private PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 20

    Private Const ColDeptName As Short = 1
    Private Const ColTotalIndent As Short = 2
    Private Const ColPendForPO As Short = 3
    Private Const ColPendAfterPO As Short = 4
    Private Const ColExecuted As Short = 5
    Private Const ColDeptCode As Short = 6


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
        mTitle = "Department Wise Pending Indent Summary"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IndentDeptWiseSumm.RPT"


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next
        PubDBCn.CommitTrans()
        FillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
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
    Private Sub frmParamPendIndRegDeptWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Department Wise Pending Indent Summary"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPendIndRegDeptWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

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



    Private Sub frmParamPendIndRegDeptWise_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamPendIndRegDeptWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        With SprdMain
            .MaxCols = ColDeptCode
            .set_RowHeight(0, RowHeight * 1.3)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColDeptName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptName, 25)

            .Col = ColTotalIndent
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColTotalIndent, 12)

            .Col = ColPendForPO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColPendForPO, 12)

            .Col = ColPendAfterPO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColPendAfterPO, 12)

            .Col = ColExecuted
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColExecuted, 12)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColDeptCode, 10)
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
        Dim cntRow As Integer
        Dim mDeptDesc As String
        Dim mDeptCode As String
        Dim mTotIndent As Double
        Dim mTotPendingPo As Double
        Dim mTotPendingAfterPo As Double
        Dim mTotExecute As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTemp_Indent = False Then GoTo LedgError

        SqlStr = MakeSQL
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        With SprdMain
            If RsShow.EOF = False Then
                .MaxRows = cntRow
                Do While Not RsShow.EOF
                    .Row = cntRow
                    .Col = ColDeptName
                    mDeptDesc = IIf(IsDbNull(RsShow.Fields("DEPT_DESC").Value), "", RsShow.Fields("DEPT_DESC").Value)
                    mDeptCode = IIf(IsDbNull(RsShow.Fields("DEPT_CODE").Value), "", RsShow.Fields("DEPT_CODE").Value)

                    .Text = mDeptDesc

                    .Col = ColTotalIndent
                    mTotIndent = IIf(IsDbNull(RsShow.Fields("TOTINDENT").Value), "", RsShow.Fields("TOTINDENT").Value)
                    .Text = CStr(mTotIndent)

                    .Col = ColPendForPO
                    mTotPendingPo = IIf(IsDbNull(RsShow.Fields("PENDPO").Value), "", RsShow.Fields("PENDPO").Value)
                    .Text = CStr(mTotPendingPo)

                    mTotPendingAfterPo = PendingIndentAfterPO(mDeptCode)

                    .Col = ColExecuted
                    mTotExecute = mTotIndent - mTotPendingPo - mTotPendingAfterPo ''GetExecuteIndent(mDeptCode)
                    .Text = CStr(mTotExecute)

                    .Col = ColPendAfterPO
                    '                mTotPendingAfterPo = PendingIndentAfterPO(mDeptCode)  ''mTotIndent - mTotPendingPo - mTotExecute
                    .Text = CStr(mTotPendingAfterPo)

                    RsShow.MoveNext()
                    If RsShow.EOF = False Then
                        If mDeptDesc <> IIf(IsDbNull(RsShow.Fields("DEPT_DESC").Value), "", RsShow.Fields("DEPT_DESC").Value) Then
                            cntRow = cntRow + 1
                            .MaxRows = cntRow
                        End If
                    End If
                Loop
            End If
        End With

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function InsertIntoTemp_Indent() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mIndentNo As String
        Dim SqlStr As String = ""
        Dim mIndentSlNo As String
        Dim mQty As Double
        Dim SqlStr1 As String
        Dim mDept As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PUR_PENDING_IND_REP " & vbCrLf & "WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' "

        PubDBCn.Execute(SqlStr)

        SqlStr1 = "INSERT INTO TEMP_PUR_PENDING_IND_REP " & vbCrLf & "(USERID, " & vbCrLf & " INDENT_NO, DEPT_CODE," & vbCrLf & " ITEM_CODE, REQ_QTY, PO_QTY, " & vbCrLf & " REC_QTY,INDENT_STATUS )"

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'," & vbCrLf & " IH.AUTO_KEY_INDENT, IH.DEPT_CODE, ID.ITEM_CODE, " & vbCrLf & " REQ_QTY,GETPOIndentQty(IH.COMPANY_CODE,IH.AUTO_KEY_INDENT,ID.ITEM_CODE), " & vbCrLf & " GETMRRPOIndentQty(IH.COMPANY_CODE,IH.AUTO_KEY_INDENT,ID.ITEM_CODE), ID.INDENT_STATUS  "

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM PUR_INDENT_HDR IH, PUR_INDENT_DET ID  "

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT AND IH.HOD_EMP_CODE IS NOT NULL"

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboAppStatus.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.APPROVAL_STATUS = 'Y'"
        ElseIf cboAppStatus.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.APPROVAL_STATUS = 'N'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.INDENT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INDENT_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        SqlStr = SqlStr1 & vbCrLf & SqlStr

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        InsertIntoTemp_Indent = True
        Exit Function
InsertErr:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoTemp_Indent = False
        MsgBox(Err.Description)
    End Function
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String

        ''SELECT CLAUSE...



        MakeSQL = " SELECT DEPT.DEPT_DESC, TO_CHAR(COUNT(DISTINCT IH.INDENT_NO)) AS TOTINDENT, " & vbCrLf _
            & " TO_CHAR(COUNT(DISTINCT CASE WHEN REQ_QTY>PO_QTY AND IH.INDENT_STATUS = 'N' THEN IH.INDENT_NO END)) AS PENDPO, " & vbCrLf _
            & " 0, " & vbCrLf _
            & " 0, IH.DEPT_CODE "


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM TEMP_PUR_PENDING_IND_REP IH, PAY_DEPT_MST DEPT  "

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " AND DEPT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.DEPT_CODE=DEPT.DEPT_CODE"

        ''GROUP BY CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "GROUP BY DEPT.DEPT_DESC,IH.DEPT_CODE"

        ''ORDER BY CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY DEPT.DEPT_DESC "


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

        cboAppStatus.Items.Clear()
        cboAppStatus.Items.Add("BOTH")
        cboAppStatus.Items.Add("Approval")
        cboAppStatus.Items.Add("Non Approval")
        cboAppStatus.SelectedIndex = 1

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Function GetExecuteIndent(ByRef pDeptCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetExecuteIndent = 0

        SqlStr = " SELECT COUNT(DISTINCT INDENT_NO) AS CNTEXECUTE" & vbCrLf & " FROM TEMP_PUR_PENDING_IND_REP " & vbCrLf & " WHERE " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf & " AND REC_QTY>=REQ_QTY " '& vbCrLf |            & " AND INDENT_NO NOT IN (" & vbCrLf |            & " SELECT DISTINCT INDENT_NO " & vbCrLf |            & " FROM TEMP_PUR_PENDING_IND_REP " & vbCrLf |            & " WHERE " & vbCrLf |            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf |            & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf |            & " AND (REQ_QTY> PO_QTY AND PO_QTY> REC_QTY) AND INDENT_STATUS = 'Y') "


        'If cboStatus.ListIndex = 1 Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND (IH.INDENT_QTY - NVL(PO_QTY,0) > 0 AND IH.INDENT_STATUS = 'N')"
        '    ElseIf cboStatus.ListIndex = 2 Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.INDENT_QTY - NVL(PO_QTY,0) <= 0 "
        '     ElseIf cboStatus.ListIndex = 3 Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.INDENT_QTY - NVL(PO_QTY,0) > 0 AND IH.INDENT_STATUS = 'Y'"
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetExecuteIndent = IIf(IsDbNull(RsTemp.Fields("CNTEXECUTE").Value), 0, RsTemp.Fields("CNTEXECUTE").Value)
        End If
        Exit Function
ErrPart:
        GetExecuteIndent = 0
    End Function
    Private Function PendingIndentAfterPO(ByRef pDeptCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        PendingIndentAfterPO = 0

        SqlStr = " SELECT COUNT(DISTINCT INDENT_NO) AS CNTINDENT" & vbCrLf & " FROM TEMP_PUR_PENDING_IND_REP " & vbCrLf & " WHERE " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf & " AND PO_QTY>=REQ_QTY AND REQ_QTY>REC_QTY AND INDENT_STATUS = 'N'" '& vbCrLf |            & " AND INDENT_NO NOT IN (" & vbCrLf |            & " SELECT DISTINCT INDENT_NO " & vbCrLf |            & " FROM TEMP_PUR_PENDING_IND_REP " & vbCrLf |            & " WHERE " & vbCrLf |            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf |            & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf |            & " AND (REQ_QTY> PO_QTY AND PO_QTY> REC_QTY) AND INDENT_STATUS = 'Y') "


        'If cboStatus.ListIndex = 1 Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND (IH.INDENT_QTY - NVL(PO_QTY,0) > 0 AND IH.INDENT_STATUS = 'N')"
        '    ElseIf cboStatus.ListIndex = 2 Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.INDENT_QTY - NVL(PO_QTY,0) <= 0 "
        '     ElseIf cboStatus.ListIndex = 3 Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.INDENT_QTY - NVL(PO_QTY,0) > 0 AND IH.INDENT_STATUS = 'Y'"
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            PendingIndentAfterPO = IIf(IsDbNull(RsTemp.Fields("CNTINDENT").Value), 0, RsTemp.Fields("CNTINDENT").Value)
        End If
        Exit Function
ErrPart:
        PendingIndentAfterPO = 0
    End Function
End Class
