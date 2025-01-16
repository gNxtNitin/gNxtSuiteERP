Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDailyPrd
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection				


    Private Const ColLocked As Short = 1
    Private Const ColDept As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemName As Short = 4
    Private Const ColOpening As Short = 5
    Private Const ColPlanning As Short = 6
    Private Const ColActual As Short = 7
    Private Const ColOk As Short = 8
    Private Const ColNC As Short = 9
    Private Const ColRW As Short = 10
    Private Const ColScrap As Short = 11
    Private Const ColSendToNext As Short = 12
    Private Const ColClosing As Short = 13
    Private Const ColProdLoss As Short = 14
    Private Const ColProdLossPer As Short = 15
    Private Const ColReason As Short = 16

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub chkAllDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDept.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDept.Enabled = False
            cmdSearchDept.Enabled = False
        Else
            txtDept.Enabled = True
            cmdSearchDept.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPDR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPDR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnPDR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String


        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColReason, PubDBCn) = False Then GoTo ReportErr

        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Report1.Reset()
        mTitle = "DAILY PRODUCTION REPORT"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\Daily_Prod_Report.RPT"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub



    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub
    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        SearchSupplier()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    If InsertIntoTempTable = False Then GoTo ErrPart				
        '				
        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        Call PrintStatus(True)
        Call CalcTots()

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4				
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function MakeSQL() As String

        On Error GoTo InsertErr
        Dim SqlStr As String
        Dim mDeptCode As String
        Dim mItemCode As String
        Dim mTableName As String


        mTableName = "INV_STOCK_REC_TRN"


        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " DEPT.DEPT_DESC, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC,  "


        ''OpeningFG				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS OpeningFG, "

        ''Plan FG				
        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(GET_PRODPLAN(INV.COMPANY_CODE,ITEM.ITEM_CODE,DEPT.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'))) AS PlannFG, "

        '''Actual				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Production, "
        '''OK				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Production, "
        '''NC				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN STOCK_TYPE = 'WP' " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Production, "
        ''Rework				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN STOCK_TYPE = 'WR' " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rework, "
        ''Scrap				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN STOCK_TYPE ='SC' " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Scrap, "
        ''IssueFG				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS IssueFG, "
        ''ClosingFG				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN " & vbCrLf & " STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingFG, "
        ''Production Loss				
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)- " & vbCrLf & " GET_PRODPLAN(INV.COMPANY_CODE,ITEM.ITEM_CODE,DEPT.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'))) AS ProdLoss, "


        SqlStr = SqlStr & vbCrLf & "0, ''"

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CMST, PAY_DEPT_MST DEPT "


        ''**********WHERE CLAUSE .......*************				

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND ITEM.CATEGORY_CODE=CMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND INV.DEPT_CODE_TO=DEPT.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " AND CMST.GEN_TYPE='C' "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                '            SqlStr = SqlStr & vbCrLf & " AND INV.DEPT_CODE_FROM='" & mDeptCode & "'"				
                SqlStr = SqlStr & vbCrLf & " AND ( INV.DEPT_CODE_TO='" & mDeptCode & "' OR INV.DEPT_CODE_FROM='" & mDeptCode & "')"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID =DECODE(DEPT.DEPT_CODE,'PAD','" & ConWH & "','" & ConPH & "')"


        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','WP','WR','SC','FG') "

        SqlStr = SqlStr & vbCrLf & " AND CMST.STOCKTYPE='FG'"
        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS = 'O'"
        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') "
        End If


        '    If chkOption.Value = vbChecked Then				
        '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""				
        '        mHavingClause = True				
        '    End If				

        '    If chkZeroBal.Value = vbChecked Then				
        '        If mHavingClause = False Then				
        '            SqlStr = SqlStr & vbCrLf & " HAVING "				
        '        Else				
        '            SqlStr = SqlStr & vbCrLf & " AND "				
        '        End If				
        '				
        '        SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"				
        '    End If				

        SqlStr = SqlStr & vbCrLf & " HAVING "
        SqlStr = SqlStr & vbCrLf & " GET_PRODPLAN(INV.COMPANY_CODE,ITEM.ITEM_CODE,DEPT.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
        SqlStr = SqlStr & vbCrLf & " + SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)"
        SqlStr = SqlStr & vbCrLf & " + SUM(CASE WHEN STOCK_TYPE = DECODE(DEPT.DEPT_CODE,'PAD','FG','ST') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)>0"

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " DEPT.DEPT_DESC, CMST.GEN_DESC, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "

        SqlStr = SqlStr & vbCrLf & ", GET_PRODPLAN(INV.COMPANY_CODE,ITEM.ITEM_CODE,DEPT.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " DEPT.DEPT_DESC, CMST.GEN_DESC,  ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume				
    End Function
    Private Sub CalcTots()
        On Error GoTo ErrPart
        Dim I As Integer
        Dim mDeptCode As String
        Dim mItemCode As String
        Dim mReason As String
        Dim mPlanning As Double
        Dim mActual As Double
        Dim mProdLossPer As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDept
                If MainClass.ValidateWithMasterTable(Trim(.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = MasterNo
                Else
                    mDeptCode = "-1"
                End If

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColPlanning
                mPlanning = Val(.Text)

                .Col = ColActual
                mActual = Val(.Text)

                .Col = ColProdLossPer
                If mPlanning = 0 Then
                    mProdLossPer = 0
                Else
                    mProdLossPer = (mPlanning - mActual) * 100 / mPlanning
                End If

                .Text = VB6.Format(mProdLossPer, "00")

                .Col = ColReason
                If mPlanning - mActual <> 0 Then
                    mReason = GetLossReason(mDeptCode, mItemCode)
                Else
                    mReason = ""
                End If
                .Text = Trim(mReason)

            Next
        End With
        Exit Sub
ErrPart:
        '    Resume				
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamDailyPrd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Daily Production Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDailyPrd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        txtDept.Enabled = False
        cmdSearchDept.Enabled = False

        Call FillCategory()
        Call PrintStatus(True)
        'Call FillPOCombo				
        txtDateFrom.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamDailyPrd_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamDailyPrd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub lstCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstCategory.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub FillCategory()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstCategory.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstCategory.Items.Add(RS.Fields("GEN_DESC").Value)
                lstCategory.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCategory.SelectedIndex = 0
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
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
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtDept.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtDept.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
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
    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
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

        Dim cntCol As Integer


        With SprdMain
            .MaxCols = ColReason
            .set_RowHeight(0, RowHeight * 1.2)
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

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 6)
            .ColHidden = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 6)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 15)

            For cntCol = ColOpening To ColProdLossPer
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColReason, 15)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub


    Private Function MakeSQLOld() As String

        On Error GoTo ERR1
        Dim mCategoryCode As String
        Dim mCategoryStr As String
        Dim CntLst As Integer
        Dim mCategoryDesc As String
        Dim mShowAll As Boolean


        ''''SELECT CLAUSE...				
        MakeSQLOld = " SELECT '', DEPT.DEPT_DESC, IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, " & vbCrLf & " MAX(IH.ACTUAL_QTY),  SUM(IH.PLAN_QTY), " & vbCrLf & " SUM(IH.OK_QTY+IH.RW_QTY)," & vbCrLf & " SUM(IH.OK_QTY), SUM(IH.NC_QTY), SUM(IH.RW_QTY)," & vbCrLf & " SUM(IH.SCRAP_QTY), " & vbCrLf & " SUM(SEND_QTY), " & vbCrLf & " MAX(IH.ACTUAL_QTY)+SUM(IH.OK_QTY+IH.RW_QTY-SEND_QTY)," & vbCrLf & " SUM(IH.PLAN_QTY-IH.OK_QTY) "

        ''''FROM CLAUSE...				
        MakeSQLOld = MakeSQLOld & vbCrLf & " FROM Temp_Daily_Prod IH, " & vbCrLf & " INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT"

        ''''WHERE CLAUSE...				
        MakeSQLOld = MakeSQLOld & vbCrLf & " WHERE " & vbCrLf & " UPPER(TRIM(UserID))='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE " & vbCrLf & " AND IH.DEPT_CODE=DEPT.DEPT_CODE"

        mShowAll = True
        For CntLst = 0 To lstCategory.Items.Count - 1
            If lstCategory.GetItemChecked(CntLst) = True Then
                '            lstCategory.ListIndex = CntLst				
                mCategoryDesc = VB6.GetItemString(lstCategory, CntLst)
                If MainClass.ValidateWithMasterTable(mCategoryDesc, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCategoryCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    mCategoryCode = "'" & mCategoryCode & "'"
                End If
                mCategoryStr = IIf(mCategoryStr = "", mCategoryCode, mCategoryStr & "," & mCategoryCode)
            Else
                mShowAll = False
            End If
        Next

        If mShowAll = False Then
            If mCategoryStr <> "" Then
                mCategoryStr = "(" & mCategoryStr & ")"
                MakeSQLOld = MakeSQLOld & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mCategoryStr & ""
            End If
        End If




        ''''GROUP CLAUSE...				
        MakeSQLOld = MakeSQLOld & vbCrLf & "GROUP BY DEPT.DEPT_DESC, IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC"
        ''''ORDER CLAUSE...				
        MakeSQLOld = MakeSQLOld & vbCrLf & "ORDER BY DEPT.DEPT_DESC, IH.PRODUCT_CODE"

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
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtDept.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtDept.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Dept Name")
                txtDept.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
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
            Exit Sub
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
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub

    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo ErrPart
        Dim SqlStrIns As String
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mDeptCode As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM TEMP_DAILY_PROD WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO Temp_Daily_Prod ( " & vbCrLf & " USERID, COMPANY_CODE, SERIAL_DATE, " & vbCrLf & " DEPT_CODE, PRODUCT_CODE, MIN_QTY, " & vbCrLf & " MAX_QTY, ACTUAL_QTY, PLAN_QTY, " & vbCrLf & " OK_QTY, NC_QTY, RW_QTY, " & vbCrLf & " SCRAP_QTY, TOTAL_QTY, CLOSE_QTY, " & vbCrLf & " SEND_QTY, LOSS_QTY, LOSS_RSN ) "

        SqlStr = SqlStr & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " SERIAL_DATE, IH.DEPT_CODE, IH.PRODUCT_CODE, " & vbCrLf & " MAX(ID.MIN_QTY), MAX(ID.MAX_QTY), " & vbCrLf & " GET_STK(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CASE WHEN IH.DEPT_CODE='PAD' THEN 'FG' ELSE 'ST' END,'OP'), " & vbCrLf & " SUM(DPLAN_QTY),  " & vbCrLf & " CASE WHEN IH.DEPT_CODE = 'PAD' THEN " & vbCrLf & " GET_STK(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CASE WHEN IH.DEPT_CODE='PAD' THEN 'FG' ELSE 'ST' END,'I') " & vbCrLf & " ELSE GET_PRODSTK(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE, IH.SERIAL_DATE,'Y') END, " & vbCrLf & " CASE WHEN IH.DEPT_CODE = 'PAD' THEN 0 ELSE GET_STK(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'WP','N') END, " & vbCrLf & " CASE WHEN IH.DEPT_CODE = 'PAD' THEN 0 ELSE GET_PRODSTK(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE, IH.SERIAL_DATE,'N') END, " & vbCrLf & " 0, " & vbCrLf & " 0, 0, " & vbCrLf & " CASE WHEN IH.DEPT_CODE = 'PAD' THEN " & vbCrLf & " GET_STK(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE,TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CASE WHEN IH.DEPT_CODE='PAD' THEN 'FG' ELSE 'ST' END,'O') " & vbCrLf & " ELSE GET_SENDQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE, IH.SERIAL_DATE) + " & vbCrLf & " GET_SENDQTYInPDI(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.PRODUCT_CODE, IH.DEPT_CODE, IH.SERIAL_DATE) END, " & vbCrLf & " 0, " & vbCrLf & " '' "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH, PRD_PRODSEQUENCE_DET ID "

        ''''WHERE CLAUSE...				
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.DEPT_CODE=ID.DEPT_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=ID.PRODUCT_CODE "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE=IH.PRODUCT_CODE" & vbCrLf & " AND DEPT_CODE=IH.DEPT_CODE" & vbCrLf & " AND WEF <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " AND IH.SERIAL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.SERIAL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        ''''GROUP BY CLAUSE...				

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " SERIAL_DATE, IH.DEPT_CODE, IH.PRODUCT_CODE " & vbCrLf & " " & vbCrLf
        PubDBCn.Execute(SqlStr)

        InsertIntoTempTable = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        'Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertIntoTempTable = False
        PubDBCn.RollbackTrans()
    End Function
    Private Sub GetRecdDetail(ByRef pCustCode As String, ByRef pItemCode As String, ByRef mTodayRejQtyRecd As Double)


        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim mItemCode As String
        Dim mSupplier As String

        mTodayRejQtyRecd = 0

        ''BILL_QTY				
        SqlStr = " SELECT " & vbCrLf & " SUM(ID.RECEIVED_QTY) AS TOD_REJQTY "

        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID "

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR AND IH.REF_TYPE='I'"

        SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustCode) & "'"

        SqlStr = SqlStr & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_CHAR('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTodayRejQtyRecd = IIf(IsDBNull(RsTemp.Fields("TOD_REJQTY").Value), 0, RsTemp.Fields("TOD_REJQTY").Value)
        End If

        RsTemp.Close()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsTemp.Close()
    End Sub


    Private Function GetLossReason(ByRef pDeptCode As String, ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT ID.REASON " & vbCrLf _
            & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID  " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_REF = ID.AUTO_KEY_REF" & vbCrLf _
            & " AND IH.DEPT_CODE = '" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND ID.REASON IS NOT NULL" & vbCrLf _
            & " AND IH.PROD_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.PROD_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''PRODUCTION DATE   ''12-14-2007				

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            Do While Not RsTemp.EOF
                GetLossReason = IIf(GetLossReason = "", "", GetLossReason & ", ") & IIf(IsDBNull(RsTemp.Fields("REASON").Value), "", RsTemp.Fields("REASON").Value)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetLossReason = ""
    End Function
End Class