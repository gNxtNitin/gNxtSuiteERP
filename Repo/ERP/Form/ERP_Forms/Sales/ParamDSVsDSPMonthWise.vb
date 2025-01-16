Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDSVsDSPMonthWise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColSupplierCode As Short = 1
    Private Const ColSupplierName As Short = 2
    Private Const ColDSNo As Short = 3
    Private Const ColAmendNo As Short = 4
    'Private Const ColPONo = 5	
    'Private Const ColPODate = 6	
    'Private Const ColPOAmendNo = 7	
    Private Const ColItemCode As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColType As Short = 7
    Private Const ColStockQty As Short = 8
    Dim ColMaxCol As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboExportItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExportItem_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CboItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CboItemType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemType.SelectedIndexChanged
        Call PrintStatus(False)
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

    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            txtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mTillDateSchdValue As Double
        Dim mTillDateDespatchValue As Double
        Dim mSchdValue As Double
        Dim mDespatchValue As Double
        Dim cntRow As Integer
        Dim mTillDateSchdQty As Double
        Dim mTillDateDespQty As Double

        Report1.Reset()

        If InsertIntoPrintdummyData = False Then GoTo ReportErr

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColType
                If .Text = "P" Then
                    .Col = ColMaxCol - 2
                    mSchdValue = mSchdValue + Val(.Text)

                    .Col = ColMaxCol - 1
                    mTillDateSchdValue = mTillDateSchdValue + Val(.Text)

                    .Col = ColMaxCol
                    mTillDateSchdQty = mTillDateSchdQty + Val(.Text)

                Else
                    .Col = ColMaxCol - 2
                    mDespatchValue = mDespatchValue + Val(.Text)

                    .Col = ColMaxCol - 1
                    mTillDateDespatchValue = mTillDateDespatchValue + Val(.Text)

                    .Col = ColMaxCol
                    mTillDateDespQty = mTillDateDespQty + Val(.Text)
                End If
            Next
        End With

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Schedule Vs Despatch (Month Wise) Register for the month of " & VB6.Format(lblNewDate.Text, "MMMM , YYYY")
        mSubTitle = ""
        If OptShow(0).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ShortageFollowup_dSP.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ShortageFollowupWeek.rpt"
        End If

        '----------	


        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mTillDateSchdValue, mTillDateDespatchValue, mSchdValue, mDespatchValue, mTillDateSchdQty, mTillDateDespQty)

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function InsertIntoPrintdummyData() As Boolean

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


                For cntCol = 1 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'" & ","
                    End If
                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ")"
                mValueSQL = mValueSQL & ")"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoPrintdummyData = True
        Exit Function
ERR1:
        'Resume	
        PubDBCn.RollbackTrans()
        InsertIntoPrintdummyData = False
        MsgInformation(Err.Description)
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mTillDateSchdValue As Double, ByRef mTillDateDespatchValue As Double, ByRef mSchdValue As Double, ByRef mDespatchValue As Double, ByRef mTillDateSchdQty As Double, ByRef mTillDateDespQty As Double)

        Dim mDate As Integer
        'Dim mTillDateSchdValue As Double	
        'Dim mTillDateDespatchValue As Double	
        'Dim mSchdValue As Double	
        'Dim mDespatchValue As Double	


        mDate = VB.Day(CDate(txtSaleDate.Text))
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        If OptShow(0).Checked = True Then
            MainClass.AssignCRptFormulas(Report1, "TodayDate=""" & mDate & """")

            MainClass.AssignCRptFormulas(Report1, "TillDateSchdValue=""" & mTillDateSchdValue & """")
            MainClass.AssignCRptFormulas(Report1, "TillDateDespatchValue=""" & mTillDateDespatchValue & """")
            MainClass.AssignCRptFormulas(Report1, "SchdValue=""" & mSchdValue & """")
            MainClass.AssignCRptFormulas(Report1, "DespatchValue=""" & mDespatchValue & """")

            MainClass.AssignCRptFormulas(Report1, "TillDateSchdQty=""" & mTillDateSchdQty & """")
            MainClass.AssignCRptFormulas(Report1, "TillDateDespatchQty=""" & mTillDateDespQty & """")

        End If

        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)

        If Show1 = False Then GoTo ErrPart
        Call InsertRecdQty()
        FormatSprdMain(-1)
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPMonthWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Schedule Vs Despatch (Month Wise) Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPMonthWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        txtMonth.Enabled = False
        lblNewDate.Text = CStr(PubCurrDate)
        txtSaleDate.Text = CStr(System.Date.FromOADate(PubCurrDate.ToOADate - 1))
        txtMonth.Text = MonthName(Month(PubCurrDate)) & ", " & Year(PubCurrDate)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        Call PrintStatus(True)
        Call FillPOCombo()
        txtDateTo.Text = "01/" & VB6.Format(RunDate, "MM/YYYY")
        Call FillGridHeader()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMonth.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSaleDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleDate.TextChanged
        Call PrintStatus(False)
    End Sub

    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    Call PrintStatus(False)
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    Call PrintStatus(False)
    'End Sub
    Private Sub frmParamDSVsDSPMonthWise_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamDSVsDSPMonthWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
            Call FillGridHeader()
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub


    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster(txtCategory.Text, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster(txtSubCategory.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDatefrom_Change()
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
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
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
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

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
        Dim mMonthDays As Integer

        With SprdMain
            If OptShow(0).Checked = True Then
                ColMaxCol = ColStockQty + 32 + 5
            Else
                ColMaxCol = ColStockQty + 6 + 5
            End If

            .MaxCols = ColMaxCol
            .set_RowHeight(0, RowHeight)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColSupplierCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSupplierCode, 6)

            .Col = ColSupplierName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSupplierName, 15)

            .Col = ColDSNo
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDSNo, 8)
            .ColHidden = True

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_INTEGER
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmendNo, 8)
            .ColHidden = True

            '        .Col = ColPONo	
            '        .CellType = SS_CELL_TYPE_EDIT	
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT	
            '        .TypeEditLen = 255	
            '        .TypeEditMultiLine = True	
            '        .ColWidth(ColPONo) = 8	
            '	
            '        .Col = ColPODate	
            '        .CellType = SS_CELL_TYPE_EDIT	
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT	
            '        .TypeEditLen = 255	
            '        .TypeEditMultiLine = True	
            '        .ColWidth(ColPODate) = 8	
            '	
            '        .Col = ColPOAmendNo	
            '        .CellType = SS_CELL_TYPE_INTEGER	
            '        .CellType = SS_CELL_TYPE_INTEGER	
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC	
            '        .ColWidth(ColPOAmendNo) = 8	

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 6)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColType, 4)
            .ColsFrozen = ColType

            For cntCol = ColStockQty To ColMaxCol
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 0
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
            Next

            Call FillGridHeader()

            .Col = ColSupplierCode
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColSupplierName
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColItemDesc
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColItemCode
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************	
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        ''SELECT CLAUSE...	
        MakeSQL = " SELECT " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME || '(' || SUPP_CUST_CITY ||')', " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), "

        '    MakeSQL = MakeSQL & vbCrLf _	
        ''            & " CUST_SO_NO, CUST_SO_DATE, SO_AMEND_NO, "	

        MakeSQL = MakeSQL & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, 'P' AS PLAN, "

        MakeSQL = MakeSQL & vbCrLf & " '0',"

        If OptShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='01' THEN PLANNED_QTY ELSE 0 END)) AS DAY1," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='02' THEN PLANNED_QTY ELSE 0 END)) AS DAY2," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='03' THEN PLANNED_QTY ELSE 0 END)) AS DAY3," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='04' THEN PLANNED_QTY ELSE 0 END)) AS DAY4," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='05' THEN PLANNED_QTY ELSE 0 END)) AS DAY5," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='06' THEN PLANNED_QTY ELSE 0 END)) AS DAY6," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='07' THEN PLANNED_QTY ELSE 0 END)) AS DAY7,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='08' THEN PLANNED_QTY ELSE 0 END)) AS DAY8," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='09' THEN PLANNED_QTY ELSE 0 END)) AS DAY9," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='10' THEN PLANNED_QTY ELSE 0 END)) AS DAY10," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='11' THEN PLANNED_QTY ELSE 0 END)) AS DAY11," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='12' THEN PLANNED_QTY ELSE 0 END)) AS DAY12," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='13' THEN PLANNED_QTY ELSE 0 END)) AS DAY13," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='14' THEN PLANNED_QTY ELSE 0 END)) AS DAY14,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='15' THEN PLANNED_QTY ELSE 0 END)) AS DAY15," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='16' THEN PLANNED_QTY ELSE 0 END)) AS DAY16," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='17' THEN PLANNED_QTY ELSE 0 END)) AS DAY17," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='18' THEN PLANNED_QTY ELSE 0 END)) AS DAY18," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='19' THEN PLANNED_QTY ELSE 0 END)) AS DAY19," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='20' THEN PLANNED_QTY ELSE 0 END)) AS DAY20," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='21' THEN PLANNED_QTY ELSE 0 END)) AS DAY21,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='22' THEN PLANNED_QTY ELSE 0 END)) AS DAY22," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='23' THEN PLANNED_QTY ELSE 0 END)) AS DAY23," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='24' THEN PLANNED_QTY ELSE 0 END)) AS DAY24," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='25' THEN PLANNED_QTY ELSE 0 END)) AS DAY25," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='26' THEN PLANNED_QTY ELSE 0 END)) AS DAY26," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='27' THEN PLANNED_QTY ELSE 0 END)) AS DAY27," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='28' THEN PLANNED_QTY ELSE 0 END)) AS DAY28,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='29' THEN PLANNED_QTY ELSE 0 END)) AS DAY29," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='30' THEN PLANNED_QTY ELSE 0 END)) AS DAY30," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='31' THEN PLANNED_QTY ELSE 0 END)) AS DAY31,"

        Else
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') <='07' THEN PLANNED_QTY ELSE 0 END)) AS Week1," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'07' AND TO_CHAR(SERIAL_DATE,'DD') <='14' THEN PLANNED_QTY ELSE 0 END)) AS Week2," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'14' AND TO_CHAR(SERIAL_DATE,'DD') <='21' THEN PLANNED_QTY ELSE 0 END)) AS Week3," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'21' AND TO_CHAR(SERIAL_DATE,'DD') <='28' THEN PLANNED_QTY ELSE 0 END)) AS Week4," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'28' THEN PLANNED_QTY ELSE 0 END)) AS Week5,"
        End If

        MakeSQL = MakeSQL & vbCrLf & "TO_CHAR(SUM(PLANNED_QTY)) AS PLANNED_QTY"

        ''FROM CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.SCHLD_STATUS='O' AND"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboItemType.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_TYPE='" & VB.Left(cboItemType.Text, 1) & "'"
        End If

        If cboExportItem.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IS_EXPORT_ITEM='" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

        ''GROUP BY CLAUSE...	

        MakeSQL = MakeSQL & vbCrLf & "GROUP BY " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME || '(' || SUPP_CUST_CITY ||')', " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"

        ''ORDER CLAUSE...	

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME || '(' || SUPP_CUST_CITY ||')', " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If MainClass.ChkIsdateF(txtSaleDate) = False Then Exit Function
        If FYChk(CStr(CDate(txtSaleDate.Text))) = False Then txtDateTo.Focus()

        If VB6.Format(lblNewDate.Text, "YYYYMM") <> VB6.Format(txtSaleDate.Text, "YYYYMM") Then
            MsgInformation("Sale Date not match with Schedule Date.")
            txtSupplier.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.	
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.	
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'UPGRADE_WARNING: Untranslated statement in txtdateTo_Validate. Please check source code.	
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillPOCombo()
        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing


        cboItemType.Items.Clear()
        cboItemType.Items.Add("All")
        cboItemType.Items.Add("Local")
        cboItemType.Items.Add("Imported")
        cboItemType.SelectedIndex = 0

        cboExportItem.Items.Clear()
        cboExportItem.Items.Add("All")
        cboExportItem.Items.Add("Yes")
        cboExportItem.Items.Add("No")
        cboExportItem.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub

    Private Sub FillGridHeader()
        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim I As Integer

        With SprdMain
            I = 1
            For cntCol = ColStockQty + 1 To ColMaxCol - 6
                .Row = 0
                .Col = cntCol
                .Text = IIf(OptShow(0).Checked = True, "", "Week") & VB6.Format(I, "00")
                I = I + 1
            Next

            .Row = 0
            .Col = ColMaxCol - 5
            .Text = "Total"

            .Col = ColMaxCol - 4
            .Text = "Average / Day"

            .Col = ColMaxCol - 3
            .Text = "Sale Rejection"

            .Col = ColMaxCol - 2
            .Text = "Sale Amount"

            .Col = ColMaxCol - 1
            .Text = "Till Date Sale Amount"

            .Col = ColMaxCol
            .Text = "Till Date Qty"

        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub InsertRecdQty()

        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim I As Integer
        Dim mType As String
        Dim mPartyCode As String
        Dim mPartyName As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim pDateSeries As Integer
        Dim mRecdQty As Double
        Dim mTotRecdQty As Double
        Dim mStockQty As Double
        Dim mItemUOM As String
        Dim mSchdQty As Double
        Dim mWorkingDays As Double
        Dim mWorkedDays As Double
        Dim mRejBal As Double
        Dim mLastDate As String
        Dim mAvgQty As Double
        Dim mItemRate As Double
        Dim mItemAmount As Double
        Dim mTillDateSchd As Double
        'Dim mTillDateDesp As Double	
        Dim mTillDateSchdAmount As Double
        'Dim mTillDateDespAmount As Double	
        Dim mTillDateSchdQty As Double

        If GetWokingDays(mWorkingDays, mWorkedDays) = False Then GoTo ErrPart

        mLastDate = ""
        mLastDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) '& "/" & vb6.Format(Month(lblNewDate.Caption), "MM/YYYY")	
        mLastDate = mLastDate & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")


        If OptShow(0).Checked = True Then
            ColMaxCol = ColStockQty + 32 + 5
        Else
            ColMaxCol = ColStockQty + 6 + 5
        End If

        With SprdMain
            cntCol = 1
            .MaxCols = ColMaxCol
            While cntCol <= .DataRowCnt
                .Row = cntCol

                .Col = ColType
                mType = Trim(.Text)

                .Col = ColSupplierCode
                mPartyCode = Trim(.Text)

                .Col = ColSupplierName
                mPartyName = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If

                If mType = "P" Then
                    mStockQty = GetBalanceStockQty(mItemCode, (txtDateTo.Text), mItemUOM, "STR", "FG", "", ConWH, -1)
                Else
                    mStockQty = 0
                End If

                .Col = ColStockQty
                .Text = VB6.Format(mStockQty, "0.00")

                .Col = ColMaxCol - 5
                mSchdQty = CDbl(VB6.Format(.Text, "0.00"))

                mTillDateSchdQty = GetTillDateQty(.Row)

                .Row = cntCol
                mItemRate = GetSORate(mPartyCode, mItemCode, (txtDateTo.Text))
                If mType = "P" Then
                    .Col = ColMaxCol - 4
                    mAvgQty = mSchdQty / mWorkingDays
                    .Text = VB6.Format(mAvgQty, "0.00")

                    .Col = ColMaxCol - 3
                    mRejBal = GetBalanceStockQty(mItemCode, (txtDateTo.Text), mItemUOM, "STR", "CR", "", ConWH, -1)
                    .Text = VB6.Format(mRejBal, "0.00")

                    .Col = ColMaxCol - 2
                    mItemAmount = mItemRate * mSchdQty
                    .Text = VB6.Format(mItemAmount / 1000, "0.00")

                    .Col = ColMaxCol - 1
                    mTillDateSchd = mItemRate * mTillDateSchdQty
                    .Text = VB6.Format(mTillDateSchd / 1000, "0.00")

                    .Col = ColMaxCol
                    .Text = VB6.Format(mTillDateSchdQty, "0.00")

                    .Row = cntCol + 1
                    .MaxRows = .MaxRows + 1
                    .Action = SS_ACTION_INSERT_ROW

                    .Col = ColSupplierCode
                    .Text = mPartyCode

                    .Col = ColSupplierName
                    .Text = mPartyName

                    .Col = ColItemCode
                    .Text = mItemCode

                    .Col = ColItemDesc
                    .Text = mItemName

                    .Col = ColType
                    .Text = "D"

                    If FillRecdQty(cntCol + 1, mPartyCode, mItemCode, mItemUOM, mWorkedDays, mItemRate) = False Then GoTo ErrPart

                End If
                cntCol = cntCol + 1
                .Row = .Row + 1
            End While
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Function GetTillDateQty(ByRef xRow As Object) As Double

        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim I As Integer
        Dim mDays As Integer


        If VB6.Format(txtSaleDate.Text, "YYYYMM") = VB6.Format(PubCurrDate, "YYYYMM") Then
            mDays = VB.Day(CDate(txtSaleDate.Text))
        Else
            mDays = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))
        End If

        With SprdMain
            For I = ColStockQty + 1 To ColStockQty + mDays

                .Row = xRow
                .Col = I
                GetTillDateQty = GetTillDateQty + Val(.Text)
            Next
        End With

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetWokingDays(ByRef mWorkingDays As Double, ByRef mWorkedDays As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMonthDays As Integer
        Dim mThisDay As Integer
        'Dim mWorkingDays As Long	
        'Dim mWorkedDays As Long	
        '	
        mMonthDays = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))
        mThisDay = VB.Day(CDate(txtSaleDate.Text))

        SqlStr = " SELECT COUNT(1) AS LDAYS FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mWorkingDays = mMonthDays - IIf(IsDbNull(RsTemp.Fields("LDAYS").Value), 0, RsTemp.Fields("LDAYS").Value)
        Else
            mWorkingDays = mMonthDays
        End If

        SqlStr = " SELECT COUNT(1) AS LDAYS FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND HOLIDAY_DATE<= '" & VB6.Format(txtSaleDate.Text, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mWorkedDays = mThisDay - IIf(IsDbNull(RsTemp.Fields("LDAYS").Value), 0, RsTemp.Fields("LDAYS").Value)
        Else
            mWorkedDays = mThisDay
        End If
        GetWokingDays = True
        RsTemp.Close()
        Exit Function
ErrPart:
        GetWokingDays = False
    End Function

    Private Function FillRecdQty(ByRef pRow As Integer, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mItemUOM As String, ByRef mWorkedDays As Double, ByRef mItemRate As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDesp As ADODB.Recordset
        Dim mDespDate As String
        Dim mDate As Integer
        Dim mTotQty(31) As Double
        Dim I As Integer
        Dim mTotalQty As Double
        Dim mAvgQty As Double
        Dim mAvgQtyAchieved As Double
        Dim mRejDesp As Double
        Dim mTillDateDesp As Double

        SqlStr = ""
        For mDate = 1 To 31
            mTotQty(mDate) = 0
        Next
        mRejDesp = 0
        I = I
        For I = ColStockQty + 1 To ColMaxCol - 6
            SprdMain.Row = pRow
            SprdMain.Col = I
            SprdMain.Text = VB6.Format(0, "0.00")
        Next

        mTotalQty = 0

        'UPGRADE_WARNING: Untranslated statement in FillRecdQty. Please check source code.	

        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<='" & VB6.Format(txtSaleDate.Text, "DD-MMM-YYYY") & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsDesp.EOF Then
            mRejDesp = IIf(IsDbNull(RsDesp.Fields("TOTQTY").Value), 0, RsDesp.Fields("TOTQTY").Value)
        End If

        SqlStr = "SELECT IH.DESP_DATE, SUM(PACKED_QTY) AS TOTQTY"

        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH,DSP_DESPATCH_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('E','P') "

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.DESP_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<='" & VB6.Format(txtSaleDate.Text, "DD-MMM-YYYY") & "' "

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.DESP_DATE "
        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.DESP_DATE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsDesp.EOF Then
            Do While Not RsDesp.EOF
                mDespDate = IIf(IsDbNull(RsDesp.Fields("DESP_DATE").Value), "", RsDesp.Fields("DESP_DATE").Value)
                If OptShow(0).Checked = True Then
                    mDate = VB.Day(CDate(mDespDate))
                Else
                    If VB.Day(CDate(mDespDate)) <= 7 Then
                        mDate = 1
                    ElseIf VB.Day(CDate(mDespDate)) > 7 And VB.Day(CDate(mDespDate)) <= 14 Then
                        mDate = 2
                    ElseIf VB.Day(CDate(mDespDate)) > 14 And VB.Day(CDate(mDespDate)) <= 21 Then
                        mDate = 3
                    ElseIf VB.Day(CDate(mDespDate)) > 21 And VB.Day(CDate(mDespDate)) <= 28 Then
                        mDate = 4
                    ElseIf VB.Day(CDate(mDespDate)) > 28 Then
                        mDate = 5
                    End If
                End If
                mTotQty(mDate) = mTotQty(mDate) + Val(IIf(IsDbNull(RsDesp.Fields("TOTQTY").Value), 0, RsDesp.Fields("TOTQTY").Value))
                RsDesp.MoveNext()
            Loop
            mDate = 1

            For I = ColStockQty + 1 To ColMaxCol - 6
                SprdMain.Row = pRow
                SprdMain.Col = I
                SprdMain.Text = VB6.Format(mTotQty(mDate), "0.00")
                mTotalQty = mTotalQty + mTotQty(mDate)
                mDate = mDate + 1
            Next
        End If

        SprdMain.Row = pRow
        SprdMain.Col = ColMaxCol - 5
        SprdMain.Text = VB6.Format(mTotalQty, "0.00")

        SprdMain.Col = ColMaxCol - 4
        mAvgQtyAchieved = mTotalQty / mWorkedDays
        SprdMain.Text = VB6.Format(mAvgQtyAchieved, "0.00")

        SprdMain.Col = ColMaxCol - 3
        SprdMain.Text = VB6.Format(mRejDesp, "0.00")

        SprdMain.Col = ColMaxCol - 2
        SprdMain.Text = VB6.Format(mTotalQty * mItemRate / 1000, "0.00")

        mTillDateDesp = GetTillDateQty(pRow)
        SprdMain.Row = pRow
        SprdMain.Col = ColMaxCol - 1
        SprdMain.Text = VB6.Format(mTillDateDesp * mItemRate / 1000, "0.00")

        SprdMain.Col = ColMaxCol
        SprdMain.Text = VB6.Format(mTillDateDesp, "0.00")

        FillRecdQty = True
        Exit Function
ErrPart:
        'Resume	
        FillRecdQty = False
        MsgBox(Err.Description)
    End Function

    Private Function GetSORate(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef pDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT ITEM_PRICE AS ITEM_PRICE,IH.CUST_AMEND_NO,ID.AMEND_WEF FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.CUST_AMEND_NO= ("

        SqlStr = SqlStr & vbCrLf & " SELECT MAX(IHS.CUST_AMEND_NO) FROM  DSP_SALEORDER_HDR IHS, DSP_SALEORDER_DET IDS" & vbCrLf & " WHERE IHS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IHS.MKEY=IDS.MKEY AND IHS.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND IDS.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IDS.AMEND_WEF<='" & VB6.Format(pDate, "DD-MMM-YYYY") & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSORate = Val(IIf(IsDbNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
            '            mAmendNO = IIf(IsNull(RsTemp.Fields("CUST_AMEND_NO").Value), "", RsTemp.Fields("CUST_AMEND_NO").Value)	
            '            mWef = IIf(IsNull(RsTemp.Fields("AMEND_WEF").Value), "", RsTemp.Fields("AMEND_WEF").Value)	
        Else
            GetSORate = 0
        End If

        Exit Function
ErrPart:
        'Resume	
        GetSORate = 0
        MsgBox(Err.Description)
    End Function
End Class
