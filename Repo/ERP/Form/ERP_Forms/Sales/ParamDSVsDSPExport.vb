Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb

Friend Class frmParamDSVsDSPExport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String


    Dim mCurrRowPos As Integer
    Dim CurrCol As Integer

    Dim IsSorted As Boolean
    Dim lastsearchrow As Long
    Dim lastsearchlen As Long
    Dim lastcol As Long


    Private Const RowHeight As Short = 20

    Private Const ColSupplierCode As Short = 1
    Private Const ColSupplierName As Short = 2
    Private Const ColSupplierLoc As Short = 3
    Private Const ColItemType As Short = 3 + 1
    Private Const ColDSNo As Short = 4 + 1
    Private Const ColAmendNo As Short = 5 + 1
    Private Const ColOurSONo As Short = 6 + 1
    Private Const ColOurSoDate As Short = 7 + 1
    Private Const ColPONo As Short = 8 + 1
    Private Const ColPODate As Short = 9 + 1
    Private Const ColItemCode As Short = 10 + 1
    Private Const ColItemDesc As Short = 11 + 1
    Private Const ColItemPartNo As Short = 12 + 1
    Private Const ColStoreLOc As Short = 13 + 1
    Private Const ColPreviousMonthPlan As Short = 14 + 1
    Private Const ColPreviousMonthSchedule As Short = 15 + 1
    Private Const ColDSQty As Short = 16 + 1
    Private Const ColDSOPBalQty As Short = 17 + 1
    Private Const ColDSCurrBalQty As Short = 18 + 1
    Private Const ColPDay1 As Short = 19 + 1
    Private Const ColDDay1 As Short = 20 + 1
    Private Const ColPDay2 As Short = 21 + 1
    Private Const ColDDay2 As Short = 22 + 1
    Private Const ColPDay3 As Short = 23 + 1
    Private Const ColDDay3 As Short = 24 + 1
    Private Const ColPDay4 As Short = 25 + 1
    Private Const ColDDay4 As Short = 26 + 1
    Private Const ColPDay5 As Short = 27 + 1
    Private Const ColDDay5 As Short = 28 + 1
    Private Const ColPDay6 As Short = 29 + 1
    Private Const ColDDay6 As Short = 30 + 1
    Private Const ColPDay7 As Short = 31 + 1
    Private Const ColDDay7 As Short = 32 + 1
    Private Const ColPDay8 As Short = 33 + 1
    Private Const ColDDay8 As Short = 34 + 1
    Private Const ColPDay9 As Short = 35 + 1
    Private Const ColDDay9 As Short = 36 + 1
    Private Const ColPDay10 As Short = 37 + 1
    Private Const ColDDay10 As Short = 38 + 1
    Private Const ColPDay11 As Short = 39 + 1
    Private Const ColDDay11 As Short = 40 + 1
    Private Const ColPDay12 As Short = 41 + 1
    Private Const ColDDay12 As Short = 42 + 1
    Private Const ColPDay13 As Short = 43 + 1
    Private Const ColDDay13 As Short = 44 + 1
    Private Const ColPDay14 As Short = 45 + 1
    Private Const ColDDay14 As Short = 46 + 1
    Private Const ColPDay15 As Short = 47 + 1
    Private Const ColDDay15 As Short = 48 + 1
    Private Const ColPDay16 As Short = 49 + 1
    Private Const ColDDay16 As Short = 50 + 1
    Private Const ColPDay17 As Short = 51 + 1
    Private Const ColDDay17 As Short = 52 + 1
    Private Const ColPDay18 As Short = 53 + 1
    Private Const ColDDay18 As Short = 54 + 1
    Private Const ColPDay19 As Short = 55 + 1
    Private Const ColDDay19 As Short = 56 + 1
    Private Const ColPDay20 As Short = 57 + 1
    Private Const ColDDay20 As Short = 58 + 1
    Private Const ColPDay21 As Short = 59 + 1
    Private Const ColDDay21 As Short = 60 + 1
    Private Const ColPDay22 As Short = 61 + 1
    Private Const ColDDay22 As Short = 62 + 1
    Private Const ColPDay23 As Short = 63 + 1
    Private Const ColDDay23 As Short = 64 + 1
    Private Const ColPDay24 As Short = 65 + 1
    Private Const ColDDay24 As Short = 66 + 1
    Private Const ColPDay25 As Short = 67 + 1
    Private Const ColDDay25 As Short = 68 + 1
    Private Const ColPDay26 As Short = 69 + 1
    Private Const ColDDay26 As Short = 70 + 1
    Private Const ColPDay27 As Short = 71 + 1
    Private Const ColDDay27 As Short = 72 + 1
    Private Const ColPDay28 As Short = 73 + 1
    Private Const ColDDay28 As Short = 74 + 1
    Private Const ColPDay29 As Short = 75 + 1
    Private Const ColDDay29 As Short = 76 + 1
    Private Const ColPDay30 As Short = 77 + 1
    Private Const ColDDay30 As Short = 78 + 1
    Private Const ColPDay31 As Short = 79 + 1
    Private Const ColDDay31 As Short = 80 + 1
    Private Const ColTotalDesp As Short = 81 + 1
    Private Const ColSaleReturnQty As Short = 82 + 1
    Private Const ColDDRQty As Short = 83 + 1
    Private Const ColTotalNetDesp As Short = 84 + 1
    Private Const ColDSBalQty As Short = 85 + 1

    Private Const ColRate As Short = 86 + 1
    Private Const ColTotalSchdAmount As Short = 87 + 1
    Private Const ColTotalDespAmount As Short = 88 + 1
    Private Const ColSaleReturnAmount As Short = 89 + 1
    Private Const ColDDRAmount As Short = 90 + 1
    Private Const ColTotalNetDespAmount As Short = 91 + 1
    Private Const ColBalAmount As Short = 92 + 1
    Private Const ColRTVQty As Short = 93 + 1
    Private Const ColRTVAmount As Short = 94 + 1

    Private Const ColOriginalSchdQty As Short = 95 + 1
    Private Const ColAmendedScheduleQty As Short = 96 + 1

    'Dim ColMaxCol As Long

    'Dim ColTotalDesp As Short
    'Dim ColDSBalQty As Short
    'Dim ColRate As Short
    'Dim ColBalAmount As Short

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
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        'Dim strFilePath As String

        ''strFilePath = My.Application.Info.DirectoryPath
        ''If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
        ''    Exit Sub
        ''End If

        ''If Trim(strFilePath) = "" Then
        ''    Exit Sub
        ''End If
        'strFilePath = "G:\SAMPL1.xlsx"
        'SprdMain.SaveExcel2007File(strFilePath, "", 0, "")      ''0, 1 32
        'Exit Sub

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

        'If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        'SqlStr = ""
        'SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        'Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)

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

        'PubDBCn.Errors.Clear()

        'PubDBCn.BeginTrans()

        'SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        'PubDBCn.Execute(SqlStr)

        'SqlStr = ""
        'With SprdMain
        '    For cntRow = 1 To .MaxRows
        '        .Row = cntRow

        '        mInsertSQL = ""
        '        mValueSQL = ""
        '        SqlStr = ""

        '        mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
        '        mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


        '        For cntCol = 1 To .MaxCols
        '            .Col = cntCol

        '            If cntCol = .MaxCols Then
        '                mFieldStr = "FIELD" & cntCol
        '                mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'"
        '            Else
        '                mFieldStr = "FIELD" & cntCol & ","
        '                mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'" & ","
        '            End If
        '            mInsertSQL = mInsertSQL & mFieldStr
        '            mValueSQL = mValueSQL & mValueStr


        '        Next
        '        mInsertSQL = mInsertSQL & ")"
        '        mValueSQL = mValueSQL & ")"

        '        SqlStr = mInsertSQL & vbCrLf & mValueSQL
        '        PubDBCn.Execute(SqlStr)
        '    Next
        'End With
        'PubDBCn.CommitTrans()
        InsertIntoPrintdummyData = True
        Exit Function
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoPrintdummyData = False
        MsgInformation(Err.Description)
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Report1.WindowShowGroupTree = False
        'Report1.Action = 1
        Dim mRPTName As String

        mRPTName = "SchdVsDesp.rpt"
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
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
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)

        lastcol = -1

        If Show1("S") = False Then GoTo ErrPart
        'Call InsertRecdQty()
        'FormatSprdMain(-1)
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPExport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim tempstr As Object = Nothing

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        'FormatSprdMain(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPExport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        'txtMonth.Enabled = False
        lblNewDate.Text = CStr(PubCurrDate)
        txtDateFrom.Text = "01/" & VB6.Format(RunDate, "MM/YYYY")
        txtDateTo.Text = CStr(PubCurrDate)
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


        optShow(0).Checked = True

        Call Show1("L")
        Call PrintStatus(True)



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
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
    Private Sub frmParamDSVsDSPExport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPExport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) 
    '    SprdMain.Row = -1
    '    SprdMain.Col = eventArgs.col
    '    SprdMain.DAutoCellTypes = True
    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdMain.TypeEditLen = 1000
    'End Sub


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

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
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

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
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
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
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
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mAccountCode As String

        SqlStr = " SELECT DISTINCT ITEMMST.ITEM_SHORT_DESC, ID.ITEM_CODE,  ID.CUSTOMER_PART_NO "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        MainClass.SearchGridMasterBySQL2(TxtItemName.Text, SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If

        '

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , SqlStr)
        'If AcName <> "" Then
        '    TxtItemName.Text = AcName
        'End If
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

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
    'Private Sub FormatSprdMain(ByRef Arow As Integer)

    '    Dim cntCol As Integer
    '    Dim mMonthDays As Integer

    '    Dim I As Integer
    '    Dim tempstr As Object = Nothing
    '    Dim mMaxColLen As Long

    '    With SprdMain

    '        .MaxCols = ColBalAmount
    '        .set_RowHeight(0, RowHeight * 2)
    '        .set_ColWidth(0, 4.5)

    '        .set_RowHeight(-1, RowHeight * 1)
    '        .Row = -1

    '        .Col = ColSupplierCode
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColSupplierCode, 6)

    '        .Col = ColSupplierName
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColSupplierName, 22)

    '        .Col = ColDSNo
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 0
    '        .TypeFloatMin = CDbl("-99999999999")
    '        .TypeFloatMax = CDbl("99999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .set_ColWidth(ColDSNo, 8)
    '        .ColHidden = True

    '        .Col = ColAmendNo
    '        .CellType = SS_CELL_TYPE_INTEGER
    '        .CellType = SS_CELL_TYPE_INTEGER
    '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
    '        .set_ColWidth(ColAmendNo, 8)
    '        .ColHidden = True

    '        .Col = ColPONo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColPONo, 8)

    '        .Col = ColPODate
    '        .CellType = SS_CELL_TYPE_DATE
    '        .TypeDateCentury = True
    '        .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
    '        .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
    '        .set_ColWidth(ColPODate, 8)


    '        .Col = ColItemCode
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColItemCode, 6)

    '        .Col = ColItemDesc
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColItemDesc, 22)

    '        .ColsFrozen = ColItemDesc

    '        For cntCol = ColPreviousMonthPlan To ColDSCurrBalQty
    '            .Col = cntCol
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalPlaces = 0
    '            .TypeFloatMin = CDbl("-99999999999")
    '            .TypeFloatMax = CDbl("99999999999")
    '            .TypeFloatMoney = False
    '            .TypeFloatSeparator = False
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatSepChar = Asc(",")
    '            .set_ColWidth(cntCol, 8)
    '        Next

    '        .Col = ColOurSONo
    '        .ColHidden = True

    '        .Col = ColOurSoDate
    '        .ColHidden = True

    '        .Col = ColDSOPBalQty
    '        .ColHidden = IIf(optShow(1).Checked = True, False, True)

    '        .Col = ColDSCurrBalQty
    '        .ColHidden = IIf(optShow(1).Checked = True, False, True)


    '        For cntCol = ColPDay1 To ColDSBalQty
    '            .Col = cntCol
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalPlaces = 0
    '            .TypeFloatMin = CDbl("-99999999999")
    '            .TypeFloatMax = CDbl("99999999999")
    '            .TypeFloatMoney = False
    '            .TypeFloatSeparator = False
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatSepChar = Asc(",")
    '            .set_ColWidth(cntCol, 8)
    '        Next

    '        For cntCol = ColRate To ColBalAmount
    '            .Col = cntCol
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalPlaces = 2
    '            .TypeFloatMin = CDbl("-99999999999.99")
    '            .TypeFloatMax = CDbl("99999999999.99")
    '            .TypeFloatMoney = False
    '            .TypeFloatSeparator = False
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatSepChar = Asc(",")
    '            .set_ColWidth(cntCol, 8)
    '        Next

    '        Dim mTillDay As Integer
    '        mTillDay = Microsoft.VisualBasic.DateAndTime.Day(txtDateTo.Text)
    '        mTillDay = 15 + (mTillDay * 2)

    '        For cntCol = 16 To mTillDay
    '            .Col = cntCol
    '            .ColHidden = False
    '        Next

    '        For cntCol = mTillDay + 1 To ColTotalDesp - 1
    '            .Col = cntCol
    '            .ColHidden = True
    '        Next

    '        MainClass.SetSpreadColor(SprdMain, -1)
    '        MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
    '        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

    '        SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
    '        SprdMain.DAutoCellTypes = True
    '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

    '    End With
    'End Sub
    Private Function Show1(pShowType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim inti As Integer
        Dim mToDay As Integer

        Dim mFromDay As Integer

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mFromDay = ColDSCurrBalQty + (Microsoft.VisualBasic.DateAndTime.Day(txtDateFrom.Text) * 2) - 1
        mToDay = ColDSCurrBalQty + (Microsoft.VisualBasic.DateAndTime.Day(txtDateTo.Text) * 2)


        If optShow(1).Checked = True And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then 'ERP_CUSTOMER_ID
            SqlStr = MakeSQLClose(pShowType)
        Else
            SqlStr = MakeSQL(pShowType)
        End If

        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        FillUltraGrid(SqlStr)

        For inti = ColPDay1 - 1 To ColDDay31 - 1
            UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Hidden = True
        Next

        If optShow(0).Checked = True Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
            For inti = mFromDay - 1 To mToDay - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Hidden = False
            Next
        End If

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL(pShowType As String) As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mStateDate As String
        Dim mCurrMonth As String
        Dim mLastDay As Integer
        Dim mMonthLastDate As String
        Dim mMonth As String
        Dim mTillDay As Integer
        Dim mPreviousDate As String
        Dim mPreviousFromDate As String

        Dim mMonthStartDate As String
        Dim mCustPoNoField As String
        Dim mCustPoDateField As String
        Dim mSoAmendNoField As String

        If optShow(0).Checked = True Then
            mCustPoNoField = "IH.CUST_SO_NO"
            mCustPoDateField = "IH.CUST_SO_DATE"
            mSoAmendNoField = "IH.SO_AMEND_NO"
        Else
            mCustPoNoField = "IH.CUST_PO_NO"
            mCustPoDateField = "IH.CUST_PO_DATE"
            mSoAmendNoField = "IH.AMEND_NO"
        End If

        mStateDate = "01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mPreviousDate = DateAdd("d", -1, mStateDate)
        mPreviousFromDate = "01/" & VB6.Format(mPreviousDate, "MM/YYYY")

        mMonth = VB6.Format(lblNewDate.Text, "MM/YYYY")

        mCurrMonth = VB6.Format(lblNewDate.Text, "MMYYYY")

        mMonthStartDate = VB6.Format("01/" & VB6.Format(lblNewDate.Text, "MM/YYYY"))

        mLastDay = MainClass.LastDay(Month(CDate(mStateDate)), Year(CDate(mStateDate)))
        mMonthLastDate = VB6.Format(mLastDay, "00") & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mTillDay = Microsoft.VisualBasic.DateAndTime.Day(txtDateTo.Text)

        MakeSQL = " SELECT " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IHS.BILL_TO_LOC_ID AS BILL_TO_LOC_ID, DECODE(INVMST.ITEM_CLASSIFICATION,'B','BOP',DECODE(INVMST.ITEM_CLASSIFICATION,'I','INHOUSE','')), "

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), "
        Else
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(IH.AUTO_KEY_SO), TO_CHAR(IH.AMEND_NO), "
        End If


        MakeSQL = MakeSQL & vbCrLf _
            & " IH.AUTO_KEY_SO, IH.SO_DATE, " & mCustPoNoField & ", " & mCustPoDateField & "," '' SO_AMEND_NO, "

        MakeSQL = MakeSQL & vbCrLf _
            & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, GetITEMPARTNO(IH.COMPANY_CODE,IH.AUTO_KEY_SO, " & mSoAmendNoField & ", TRIM(ID.ITEM_CODE)) PARTNO, NVL(ID.LOC_CODE,' '),"

        MakeSQL = MakeSQL & vbCrLf _
                & " GETSALESCHEDULEQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mPreviousFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))  AS  PREV_DSQTY,"

        MakeSQL = MakeSQL & vbCrLf _
                & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mPreviousFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS  PREV_DESPQTY, "


        MakeSQL = MakeSQL & vbCrLf _
            & "TO_CHAR(SUM(PLANNED_QTY)) AS PLANNED_QTY, "

        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " SUM(PLANNED_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mPreviousFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) DSBLQTY,"
            MakeSQL = MakeSQL & vbCrLf _
                & " SUM(CASE WHEN SERIAL_DATE <= TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN PLANNED_QTY ELSE 0 END)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS CURRBAL, "

        Else
            MakeSQL = MakeSQL & vbCrLf & " SUM(PLANNED_QTY) DSBLQTY,"
            MakeSQL = MakeSQL & vbCrLf & " SUM(PLANNED_QTY) AS CURRBAL,"
        End If

        For CntCol = 1 To mTillDay
            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & VB6.Format(CntCol, "00") & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY" & CntCol & "," & vbCrLf _
                & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(VB6.Format(CntCol, "00") & "/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY" & CntCol & ","
        Next

        For CntCol = mTillDay + 1 To 31
            MakeSQL = MakeSQL & vbCrLf _
              & " '' AS DAY" & CntCol & "," & vbCrLf _
              & " '' AS DDAY" & CntCol & ","
        Next

        'ColTotalDesp
        MakeSQL = MakeSQL & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " AS TotalDesp, "

        'ColSaleReturnQty
        MakeSQL = MakeSQL & vbCrLf _
                & " GETSRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
                & "  AS  SRQTY,"

        ''ColDDRQty

        MakeSQL = MakeSQL & vbCrLf _
            & " GETDDRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " AS DDRQTY, "


        ''ColTotalNetDesp
        MakeSQL = MakeSQL & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " - GETSRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " - GETDDRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " AS TotalNetDesp, "

        ''ColDSBalQty
        MakeSQL = MakeSQL & vbCrLf _
             & " SUM(PLANNED_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " AS DSBalQty, "

        ''Private Const ColTotalDesp As Short = 81 + 1
        ''Private Const ColSaleReturnQty As Short = 82 + 1
        ' 'Private Const ColDDRQty As Short = 83 + 1
        ''Private Const ColTotalNetDesp As Short = 84 + 1
        'Private Const ColDSBalQty As Short = 85 + 1

        ''ColRate
        MakeSQL = MakeSQL & vbCrLf _
             & " GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) AS Rate, "

        ''ColTotalSchdAmount
        MakeSQL = MakeSQL & vbCrLf _
            & " SUM(PLANNED_QTY) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) " & vbCrLf _
            & " AS SchdAmount, "

        '' ColTotalDespAmount
        MakeSQL = MakeSQL & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) " & vbCrLf _
            & " AS DespatchAmount, "

        ''ColSaleReturnAmount
        MakeSQL = MakeSQL & vbCrLf _
                & " GETSRAMOUNT(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
                & "  AS  SRAMOUNT,"


        ''ColDDRAmount
        MakeSQL = MakeSQL & vbCrLf _
            & " GETDDRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) " & vbCrLf _
            & " AS DDRAMOUNT, "

        ''ColTotalNetDespAmount
        MakeSQL = MakeSQL & vbCrLf _
            & " ((GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " -GETDDRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " ) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)))" & vbCrLf _
            & " - GETSRAMOUNT(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " AS TotalNetDespAmount, "


        ''ColBalAmount
        MakeSQL = MakeSQL & vbCrLf _
            & " (SUM(PLANNED_QTY) - (((GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " -GETDDRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " ) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)))" & vbCrLf _
            & " - GETSRAMOUNT(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
            & " )) AS BalAmount, "

        'MakeSQL = MakeSQL & vbCrLf _
        '    & " (SUM(PLANNED_QTY) " & vbCrLf _
        '    & " - GETDSDESPATCHQTY(IH.COMPANY_CODE, IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO, Trim(ID.ITEM_CODE), NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)" & vbCrLf _
        '    & " - GETSRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
        '    & " - GETDDRQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf _
        '    & " ) " & vbCrLf _
        '    & " AS BalAmount, "


        ''    & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS TotalDesp, " & vbCrLf _
        ''    & vbCrLf _
        ''    & "  & vbCrLf _
        ''    &  & vbCrLf _
        ''    & "  & vbCrLf _




        MakeSQL = MakeSQL & vbCrLf _
                & " GETRTVQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))  AS  RTVQTY,"

        MakeSQL = MakeSQL & vbCrLf _
                & " GETRTVAMOUNT(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))  AS  RTVAMOUNT,"




        MakeSQL = MakeSQL & vbCrLf _
                & " GETSALEORIGINALSCHEDULEQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))  AS  ORIGINAL_DSQTY,"

        MakeSQL = MakeSQL & vbCrLf _
                & " GETSALESCHEDULEQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))  AS  CURR_DSQTY"


        ''FROM CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID, DSP_SALEORDER_HDR IHS," & vbCrLf _
                    & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH, DSP_DAILY_SCHLD_DET ID, DSP_SALEORDER_HDR IHS," & vbCrLf _
                    & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"
        End If


        ''WHERE CLAUSE...
        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV AND ID.BOOKTYPE='D'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=IHS.AUTO_KEY_SO" & vbCrLf _
                    & " AND IH.COMPANY_CODE=IHS.COMPANY_CODE"

        Else
            MakeSQL = MakeSQL & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " IH.AUTO_KEY_SO=ID.AUTO_KEY_DELV AND ID.BOOKTYPE='S'" & vbCrLf _
                    & " AND IH.MKEY=IHS.MKEY" & vbCrLf _
                    & " AND IH.COMPANY_CODE=IHS.COMPANY_CODE"
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IHS.SO_STATUS='O' AND IHS.SO_APPROVED='Y'" '' AND IHS.ORDER_TYPE='C'"

        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IHS.ORDER_TYPE='C'"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IHS.ORDER_TYPE='O'"
        End If
        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & RsCompany!FYEAR & ""

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



        MakeSQL = MakeSQL & vbCrLf & " AND SERIAL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SERIAL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.SCHLD_DATE>=TO_DATE('" & VB6.Format(mMonthStartDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                 & " AND IH.SCHLD_DATE<=TO_DATE('" & VB6.Format(mMonthLastDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        End If

        If pShowType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & "AND 1=2"
        End If

        ''GROUP BY CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                    & "GROUP BY " & vbCrLf _
                    & " IH.COMPANY_CODE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,  " & vbCrLf _
                    & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), IH.AUTO_KEY_SO, IH.SO_DATE, " & mSoAmendNoField & ", " & mCustPoNoField & ", " & mCustPoDateField & ", " & vbCrLf _
                    & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, NVL(ID.LOC_CODE,' '), IHS.BILL_TO_LOC_ID, DECODE(INVMST.ITEM_CLASSIFICATION,'B','BOP',DECODE(INVMST.ITEM_CLASSIFICATION,'I','INHOUSE',''))"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                    & "GROUP BY " & vbCrLf _
                    & " IH.COMPANY_CODE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,  " & vbCrLf _
                    & " TO_CHAR(IH.AUTO_KEY_SO), TO_CHAR(IH.AMEND_NO), IH.AUTO_KEY_SO, IH.SO_DATE," & mSoAmendNoField & ", " & mCustPoNoField & ", " & mCustPoDateField & ", " & vbCrLf _
                    & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, NVL(ID.LOC_CODE,' '), IHS.BILL_TO_LOC_ID, DECODE(INVMST.ITEM_CLASSIFICATION,'B','BOP',DECODE(INVMST.ITEM_CLASSIFICATION,'I','INHOUSE',''))"

        End If

        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & ", GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IHS.AUTO_KEY_SO,ID.ITEM_CODE,NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        ''ORDER CLAUSE...
        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & "HAVING SUM(PLANNED_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IHS.AUTO_KEY_SO,ID.ITEM_CODE,NVL(ID.LOC_CODE,' '),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))>0 "
        End If

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                    & "ORDER BY " & vbCrLf _
                    & " IH.COMPANY_CODE,IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
                    & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf _
                    & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                   & "ORDER BY " & vbCrLf _
                   & " IH.COMPANY_CODE,IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
                   & " TO_CHAR(IH.AUTO_KEY_SO), TO_CHAR(IH.AMEND_NO), " & vbCrLf _
                   & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"
        End If


        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLClose(pShowType As String) As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mStateDate As String
        Dim mCurrMonth As String
        Dim mLastDay As Integer
        Dim mMonthLastDate As String
        Dim mMonth As String
        Dim mTillDay As Integer
        Dim mPreviousDate As String
        Dim mPreviousFromDate As String

        Dim mMonthStartDate As String

        mStateDate = "01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mPreviousDate = DateAdd("d", -1, mStateDate)
        mPreviousFromDate = "01/" & VB6.Format(mPreviousDate, "MM/YYYY")

        mMonth = VB6.Format(lblNewDate.Text, "MM/YYYY")

        mCurrMonth = VB6.Format(lblNewDate.Text, "MMYYYY")

        mMonthStartDate = VB6.Format("01/" & VB6.Format(lblNewDate.Text, "MM/YYYY"))

        mLastDay = MainClass.LastDay(Month(CDate(mStateDate)), Year(CDate(mStateDate)))
        mMonthLastDate = VB6.Format(mLastDay, "00") & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mTillDay = Microsoft.VisualBasic.DateAndTime.Day(txtDateTo.Text)

        MakeSQLClose = " SELECT " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID,  DECODE(INVMST.ITEM_CLASSIFICATION,'B','BOP',DECODE(INVMST.ITEM_CLASSIFICATION,'I','INHOUSE','')), " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_SO), TO_CHAR(IH.SO_DATE), "

        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " IH.AUTO_KEY_SO, IH.SO_DATE, CUST_PO_NO, CUST_PO_DATE," '' SO_AMEND_NO, "

        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, GetITEMPARTNO(IH.COMPANY_CODE,IH.AUTO_KEY_SO, IH.AMEND_NO, TRIM(ID.ITEM_CODE)) PARTNO, NVL(ID.CUST_STORE_LOC,' '),"

        MakeSQLClose = MakeSQLClose & vbCrLf _
                & " 0 AS  PREV_DSQTY,"

        MakeSQLClose = MakeSQLClose & vbCrLf _
                & " 0 AS  PREV_DESPQTY, "


        MakeSQLClose = MakeSQLClose & vbCrLf _
            & "TO_CHAR(SUM(SO_QTY)) AS PLANNED_QTY, "


        If optShow(1).Checked = True Then
            MakeSQLClose = MakeSQLClose & vbCrLf _
                & " SUM(SO_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(mPreviousFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) DSBLQTY,"
            MakeSQLClose = MakeSQLClose & vbCrLf _
                & " SUM(SO_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS CURRBAL, "

        Else
            MakeSQLClose = MakeSQLClose & vbCrLf & " SUM(SO_QTY),"
            MakeSQLClose = MakeSQLClose & vbCrLf & " SUM(SO_QTY),"
        End If

        For CntCol = 1 To mTillDay
            MakeSQLClose = MakeSQLClose & vbCrLf _
                & " '' AS DAY" & CntCol & "," & vbCrLf _
                & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(VB6.Format(CntCol, "00") & "/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY" & CntCol & ","
        Next

        For CntCol = mTillDay + 1 To 31
            MakeSQLClose = MakeSQLClose & vbCrLf _
              & " '' AS DAY" & CntCol & "," & vbCrLf _
              & " '' AS DDAY" & CntCol & ","
        Next

        'Private Const ColTotalDesp As Short = 81 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS TotalDesp, "

        'Private Const ColSaleReturnQty As Short = 82 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf & " 0,"

        'Private Const ColDDRQty As Short = 83 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf & " 0,"

        'Private Const ColTotalNetDesp As Short = 84 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS TotalDesp, "

        'Private Const ColDSBalQty As Short = 85 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " SUM(SO_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DSBalQty, "

        'Private Const ColRate As Short = 86 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) AS Rate, "

        'Private Const ColTotalSchdAmount As Short = 87 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " SUM(SO_QTY) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) AS SchdAmount, "

        'Private Const ColTotalDespAmount As Short = 88 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) AS DespatchAmount, "

        'Private Const ColSaleReturnAmount As Short = 89 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf & " 0,"

        'Private Const ColDDRAmount As Short = 90 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf & " 0,"

        'Private Const ColTotalNetDespAmount As Short = 91 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) AS DespatchAmount, "


        'Private Const ColBalAmount As Short = 92 + 1
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " (SUM(SO_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))) * GetSORATE(IH.COMPANY_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)) AS BalAmount, "


        'Private Const ColRTVQty As Short = 93 + 1
        'Private Const ColRTVAmount As Short = 94 + 1
        'Private Const ColOriginalSchdQty As Short = 95 + 1
        'Private Const ColAmendedScheduleQty As Short = 96 + 1

        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " 0,0,0,0 "



        ''FROM CLAUSE...
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID," & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"

        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " AND IH.SO_STATUS='O' AND SO_APPROVED='Y'" '' AND IHS.ORDER_TYPE='C'"

        If optShow(1).Checked = True Then
            MakeSQLClose = MakeSQLClose & vbCrLf & " AND IH.ORDER_TYPE='C'"
        Else
            MakeSQLClose = MakeSQLClose & vbCrLf & " AND IH.ORDER_TYPE='O'"
        End If
        MakeSQLClose = MakeSQLClose & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & RsCompany!FYEAR & ""

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQLClose = MakeSQLClose & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQLClose = MakeSQLClose & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQLClose = MakeSQLClose & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLClose = MakeSQLClose & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If



        MakeSQLClose = MakeSQLClose & vbCrLf & " AND SO_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SO_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        'MakeSQLClose = MakeSQLClose & vbCrLf & " AND IH.SO_DATE>=TO_DATE('" & VB6.Format(mMonthStartDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '    & " AND IH.SO_DATE<=TO_DATE('" & VB6.Format(mMonthLastDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"


        If pShowType = "L" Then
            MakeSQLClose = MakeSQLClose & vbCrLf & "AND 1=2"
        End If

        ''GROUP BY CLAUSE...

        MakeSQLClose = MakeSQLClose & vbCrLf _
            & "GROUP BY " & vbCrLf _
            & " IH.COMPANY_CODE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_SO), TO_CHAR(IH.AMEND_NO), IH.AUTO_KEY_SO, IH.SO_DATE, IH.AMEND_NO, CUST_PO_NO, CUST_PO_DATE, " & vbCrLf _
            & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, IH.BILL_TO_LOC_ID,  DECODE(INVMST.ITEM_CLASSIFICATION,'B','BOP',DECODE(INVMST.ITEM_CLASSIFICATION,'I','INHOUSE','')),  NVL(ID.CUST_STORE_LOC,' ')"

        If optShow(1).Checked = True Then
            MakeSQLClose = MakeSQLClose & vbCrLf _
                & ", GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,ID.ITEM_CODE,NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "')"
        End If

        ''ORDER CLAUSE...
        If optShow(1).Checked = True Then
            MakeSQLClose = MakeSQLClose & vbCrLf _
                & "HAVING SUM(SO_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,ID.ITEM_CODE,NVL(ID.CUST_STORE_LOC,' '),TO_DATE('" & VB6.Format(mPreviousDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "')>0 "
        End If

        MakeSQLClose = MakeSQLClose & vbCrLf _
            & "ORDER BY " & vbCrLf _
            & " IH.COMPANY_CODE,IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_SO), TO_CHAR(IH.AMEND_NO), " & vbCrLf _
            & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, NVL(ID.CUST_STORE_LOC,' ')"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        Dim Tempdate As String = ""
        Dim NewDate As String = ""

        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus

        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateTo.SetFocus

        Tempdate = "01/" & Month(txtMonth.Text) & "/" & Year(txtMonth.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblNewDate.Text = CStr(NewDate)

        If VB6.Format(lblNewDate.Text, "YYYYMM") < VB6.Format(txtDateFrom.Text, "YYYYMM") Then
            MsgInformation("Month as on Cann't be Less than Schedule Date.")
            txtSupplier.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If optShow(0).Checked = True Then
            If VB6.Format(txtDateFrom.Text, "YYYYMM") <> VB6.Format(txtDateTo.Text, "YYYYMM") Then
                MsgInformation("Schedule Month Should be same.")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If

            If VB6.Format(txtDateFrom.Text, "YYYYMM") <> VB6.Format(lblNewDate.Text, "YYYYMM") Then
                MsgInformation("Schedule Month & Month as on Should be same.")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If

        End If
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
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
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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

    '    Private Sub InsertRecdQty()
    '        On Error GoTo ErrPart
    '        Dim cntRow As Integer
    '        Dim I As Integer
    '        Dim mType As String
    '        Dim mPartyCode As String
    '        Dim mPartyName As String
    '        Dim mItemCode As String
    '        Dim mItemName As String
    '        Dim pDateSeries As Integer
    '        Dim mRecdQty As Double
    '        Dim mTotRecdQty As Double
    '        Dim mItemUOM As String
    '        Dim mLastDate As String
    '        Dim mSONo As Double
    '        Dim mDSOPBalQty As Double
    '        Dim mDSNo As Double
    '        Dim mAmendNO As Double
    '        Dim mOurSoDate As String
    '        Dim mPONo As String
    '        Dim mPODate As String
    '        Dim mDSQty As Double
    '        Dim mDSCurrBalQty As Double

    '        '
    '        '    mLastDate = ""
    '        '    mLastDate = MainClass.LastDay(Month(lblNewDate.Caption), Year(lblNewDate.Caption)) '& "/" & vb6.Format(Month(lblNewDate.Caption), "MM/YYYY")
    '        '    mLastDate = mLastDate & "/" & vb6.Format(lblNewDate.Caption, "MM/YYYY")


    '        With SprdMain
    '            cntRow = 1
    '            While cntRow <= .DataRowCnt
    '                .Row = cntRow

    '                .Col = ColSupplierCode
    '                mPartyCode = Trim(.Text)

    '                .Col = ColSupplierName
    '                mPartyName = Trim(.Text)

    '                .Col = ColDSNo
    '                mDSNo = Val(.Text)

    '                .Col = ColAmendNo
    '                mAmendNO = Val(.Text)

    '                .Col = ColOurSONo
    '                mSONo = Val(.Text)

    '                .Col = ColOurSoDate
    '                mOurSoDate = Trim(.Text)

    '                .Col = ColPONo
    '                mPONo = Trim(.Text)

    '                .Col = ColPODate
    '                mPODate = Trim(.Text)


    '                .Col = ColItemCode
    '                mItemCode = Trim(.Text)

    '                .Col = ColItemDesc
    '                mItemName = Trim(.Text)

    '                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                    mItemUOM = MasterNo
    '                End If

    '                .Col = ColDSQty
    '                mDSQty = Val(.Text)

    '                .Col = ColDSOPBalQty
    '                mDSOPBalQty = Val(.Text)

    '                .Col = ColDSCurrBalQty
    '                mDSCurrBalQty = Val(.Text)


    '                If mType = "P" Then
    '                    .Row = cntRow + 1
    '                    .MaxRows = .MaxRows + 1
    '                    .Action = SS_ACTION_INSERT_ROW

    '                    .Col = ColSupplierCode
    '                    .Text = mPartyCode

    '                    .Col = ColSupplierName
    '                    .Text = mPartyName

    '                    .Col = ColItemCode
    '                    .Text = mItemCode

    '                    .Col = ColItemDesc
    '                    .Text = mItemName

    '                    .Col = ColDSNo
    '                    .Text = CStr(mDSNo)

    '                    .Col = ColAmendNo
    '                    .Text = CStr(mAmendNO)

    '                    .Col = ColOurSONo
    '                    .Text = CStr(mSONo)

    '                    .Col = ColOurSoDate
    '                    .Text = mOurSoDate

    '                    .Col = ColPONo
    '                    .Text = mPONo

    '                    .Col = ColPODate
    '                    .Text = mPODate

    '                    .Col = ColDSQty
    '                    .Text = CStr(mDSQty)

    '                    .Col = ColDSOPBalQty
    '                    .Text = CStr(mDSOPBalQty)

    '                    .Col = ColDSCurrBalQty
    '                    .Text = CStr(mDSCurrBalQty)

    '                    If FillRecdQty(cntRow + 1, mSONo, mPartyCode, mItemCode, mItemUOM, mDSOPBalQty) = False Then GoTo ErrPart
    '                End If

    '                cntRow = cntRow + 1
    '                .Row = .Row + 1

    '            End While

    '        End With

    '        Exit Sub
    'ErrPart:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Sub



    '    Private Function FillRecdQty(ByRef pRow As Integer, ByRef pSONo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pOPDSQty As Double) As Boolean

    '        On Error GoTo ErrPart
    '        Dim SqlStr As String = ""
    '        Dim RsDesp As ADODB.Recordset
    '        Dim mDespDate As String
    '        Dim mDate As Integer
    '        Dim mTotQty(31) As Double
    '        Dim I As Integer
    '        Dim mTotalQty As Double
    '        'Dim mAvgQty As Double
    '        'Dim mAvgQtyAchieved As Double
    '        'Dim mRejDesp As Double
    '        Dim mTillDateDesp As Double

    '        Dim mRate As Double
    '        Dim mLastDate As String

    '        SqlStr = ""
    '        For mDate = 1 To 31
    '            mTotQty(mDate) = 0
    '        Next

    '        I = I

    '        mLastDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

    '        '    For I = ColStockQty + 1 To ColMaxCol - 6
    '        '        SprdMain.Row = pRow
    '        '        SprdMain.Col = I
    '        '        SprdMain.Text = Format(0, "0.00")
    '        '    Next

    '        mTotalQty = 0

    '        SqlStr = "SELECT IH.DESP_DATE, SUM(PACKED_QTY) AS TOTQTY"

    '        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH,DSP_DESPATCH_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

    '        If optShow(1).Checked = True Then
    '            SqlStr = SqlStr & vbCrLf & " AND ID.SONO =" & pSONo & ""
    '        End If

    '        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('E','P') "

    '        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.DESP_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

    '        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.DESP_DATE "
    '        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.DESP_DATE "

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesp, ADODB.LockTypeEnum.adLockReadOnly)
    '        If Not RsDesp.EOF Then
    '            Do While Not RsDesp.EOF
    '                mDespDate = IIf(IsDBNull(RsDesp.Fields("DESP_DATE").Value), "", RsDesp.Fields("DESP_DATE").Value)
    '                mDate = VB.Day(CDate(mDespDate))
    '                mTotQty(mDate) = mTotQty(mDate) + Val(IIf(IsDBNull(RsDesp.Fields("TOTQTY").Value), 0, RsDesp.Fields("TOTQTY").Value))
    '                RsDesp.MoveNext()
    '            Loop
    '            mDate = 1

    '            'For I = ColDay1 To ColDay31
    '            '    SprdMain.Row = pRow
    '            '    SprdMain.Col = I
    '            '    SprdMain.Text = VB6.Format(mTotQty(mDate), "0.00")
    '            '    mTotalQty = mTotalQty + mTotQty(mDate)
    '            '    mDate = mDate + 1
    '            'Next
    '        End If

    '        SprdMain.Row = pRow
    '        SprdMain.Col = ColTotalDesp
    '        SprdMain.Text = VB6.Format(mTotalQty, "0.00")

    '        SprdMain.Col = ColDSBalQty
    '        SprdMain.Text = VB6.Format(pOPDSQty - mTotalQty, "0.00")

    '        SprdMain.Col = ColRate
    '        mRate = GetSORate(pSupplierCode, pItemCode, VB6.Format(mLastDate, "DD/MM/YYYY"))
    '        SprdMain.Text = VB6.Format(mRate, "0.00")

    '        SprdMain.Col = ColBalAmount
    '        SprdMain.Text = VB6.Format((pOPDSQty - mTotalQty) * mRate, "0.00")

    '        FillRecdQty = True
    '        Exit Function
    'ErrPart:
    '        'Resume
    '        FillRecdQty = False
    '        MsgBox(Err.Description)
    '    End Function

    Private Function GetSORate(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef pDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT ITEM_PRICE AS ITEM_PRICE,IH.CUST_AMEND_NO,ID.AMEND_WEF FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "' AND SO_APPROVED='Y'" & vbCrLf & " AND IH.CUST_AMEND_NO= ("

        SqlStr = SqlStr & vbCrLf & " SELECT MAX(IHS.CUST_AMEND_NO) FROM  DSP_SALEORDER_HDR IHS, DSP_SALEORDER_DET IDS" & vbCrLf & " WHERE IHS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IHS.MKEY=IDS.MKEY AND IHS.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND IDS.ITEM_CODE='" & pItemCode & "' AND SO_APPROVED='Y'" & vbCrLf _
            & " AND IDS.AMEND_WEF<=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSORate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
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
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        'UltraGrid1.DataSource.Rows.Clear()
        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()

            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox("Can not open connection ! ")
        End Try
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierCode - 1).Header.Caption = "Customer Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierName - 1).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierLoc - 1).Header.Caption = "Customer Location"
            '
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemType - 1).Header.Caption = "Item Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSNo - 1).Header.Caption = "Schedule No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmendNo - 1).Header.Caption = "Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSONo - 1).Header.Caption = "Sale Order No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSoDate - 1).Header.Caption = "Sale Order No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPODate - 1).Header.Caption = "Customer PO Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPartNo - 1).Header.Caption = "Item Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLOc - 1).Header.Caption = "Store Location"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Header.Caption = "Item Desciption"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPreviousMonthPlan - 1).Header.Caption = "Previous Month Schedule Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPreviousMonthSchedule - 1).Header.Caption = "Previous Month Despatch Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSQty - 1).Header.Caption = "Schedule Qty"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSOPBalQty - 1).Header.Caption = "Opening Balance Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSCurrBalQty - 1).Header.Caption = "Current Balance Qty"

            Dim mDays As Long = 1
            For inti = ColPDay1 - 1 To ColDDay31 - 1 Step 2
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Caption = "Plan " & mDays
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti + 1).Header.Caption = "Desp " & mDays
                'If optShow(1).Checked = True Then

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then 'ERP_CUSTOMER_ID

                Else
                    UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Hidden = IIf(optShow(1).Checked = True, True, False)
                    UltraGrid1.DisplayLayout.Bands(0).Columns(inti + 1).Hidden = IIf(optShow(1).Checked = True, True, False)
                End If

                mDays = mDays + 1
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTotalDesp - 1).Header.Caption = "Total Despatch Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleReturnQty - 1).Header.Caption = "Sale Return Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDDRQty - 1).Header.Caption = "DDR Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTotalNetDesp - 1).Header.Caption = "Net Despatch Qty"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSBalQty - 1).Header.Caption = "Schedule Balance Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Header.Caption = "Rate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTotalSchdAmount - 1).Header.Caption = "Total Schedule Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTotalDespAmount - 1).Header.Caption = "Total Despatch Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleReturnAmount - 1).Header.Caption = "Sale Return Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDDRAmount - 1).Header.Caption = "DDR Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTotalNetDespAmount - 1).Header.Caption = "Net Despatch Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalAmount - 1).Header.Caption = "Balance Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRTVQty - 1).Header.Caption = "RTV Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRTVAmount - 1).Header.Caption = "RTV Amount"



            '        rivate Const ColRTVQty As Short = 87 + 1
            'Private Const ColRTVAmount As Short = 88 + 1
            '        Private Const ColSaleReturnQty As Short = 89 + 1
            '        Private Const ColSaleReturnAmount As Short = 90 + 1


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOriginalSchdQty - 1).Header.Caption = "Original Schedule Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmendedScheduleQty - 1).Header.Caption = "Lastest Schedule Qty"




            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            For inti = ColPreviousMonthPlan - 1 To ColAmendedScheduleQty - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Style = UltraWinGrid.ColumnStyle.Double
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellAppearance.TextHAlign = HAlign.Right
            Next

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSOPBalQty - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSCurrBalQty - 1).Hidden = True


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierLoc - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemType - 1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDSNo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmendNo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSONo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSoDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPODate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 80

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPartNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLOc - 1).Width = 60


            For inti = ColPreviousMonthPlan - 1 To ColAmendedScheduleQty - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Width = 75
            Next

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
End Class
