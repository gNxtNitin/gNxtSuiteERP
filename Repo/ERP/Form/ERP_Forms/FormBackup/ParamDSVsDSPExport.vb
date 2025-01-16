Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDSVsDSPExport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColSupplierCode As Short = 1
    Private Const ColSupplierName As Short = 2
    Private Const ColDSNo As Short = 3
    Private Const ColAmendNo As Short = 4
    Private Const ColOurSONo As Short = 5
    Private Const ColOurSoDate As Short = 6
    Private Const ColPONo As Short = 7
    Private Const ColPODate As Short = 8
    Private Const ColItemCode As Short = 9
    Private Const ColItemDesc As Short = 10
    Private Const ColPreviousMonthPlan As Short = 11
    Private Const ColPreviousMonthSchedule As Short = 12
    Private Const ColDSQty As Short = 13
    Private Const ColDSOPBalQty As Short = 14
    Private Const ColDSCurrBalQty As Short = 15
    Private Const ColPDay1 As Short = 17
    Private Const ColDDay1 As Short = 18
    Private Const ColPDay2 As Short = 19
    Private Const ColDDay2 As Short = 20
    Private Const ColPDay3 As Short = 21
    Private Const ColDDay3 As Short = 22
    Private Const ColPDay4 As Short = 23
    Private Const ColDDay4 As Short = 24
    Private Const ColPDay5 As Short = 25
    Private Const ColDDay5 As Short = 26
    Private Const ColPDay6 As Short = 27
    Private Const ColDDay6 As Short = 28
    Private Const ColPDay7 As Short = 29
    Private Const ColDDay7 As Short = 30
    Private Const ColPDay8 As Short = 31
    Private Const ColDDay8 As Short = 32
    Private Const ColPDay9 As Short = 33
    Private Const ColDDay9 As Short = 34
    Private Const ColPDay10 As Short = 35
    Private Const ColDDay10 As Short = 36
    Private Const ColPDay11 As Short = 37
    Private Const ColDDay11 As Short = 38
    Private Const ColPDay12 As Short = 39
    Private Const ColDDay12 As Short = 40
    Private Const ColPDay13 As Short = 41
    Private Const ColDDay13 As Short = 42
    Private Const ColPDay14 As Short = 43
    Private Const ColDDay14 As Short = 44
    Private Const ColPDay15 As Short = 45
    Private Const ColDDay15 As Short = 46
    Private Const ColPDay16 As Short = 47
    Private Const ColDDay16 As Short = 48
    Private Const ColPDay17 As Short = 49
    Private Const ColDDay17 As Short = 50
    Private Const ColPDay18 As Short = 51
    Private Const ColDDay18 As Short = 52
    Private Const ColPDay19 As Short = 53
    Private Const ColDDay19 As Short = 54
    Private Const ColPDay20 As Short = 55
    Private Const ColDDay20 As Short = 56
    Private Const ColPDay21 As Short = 57
    Private Const ColDDay21 As Short = 58
    Private Const ColPDay22 As Short = 59
    Private Const ColDDay22 As Short = 60
    Private Const ColPDay23 As Short = 61
    Private Const ColDDay23 As Short = 62
    Private Const ColPDay24 As Short = 63
    Private Const ColDDay24 As Short = 64
    Private Const ColPDay25 As Short = 65
    Private Const ColDDay25 As Short = 66
    Private Const ColPDay26 As Short = 67
    Private Const ColDDay26 As Short = 68
    Private Const ColPDay27 As Short = 69
    Private Const ColDDay27 As Short = 70
    Private Const ColPDay28 As Short = 71
    Private Const ColDDay28 As Short = 72
    Private Const ColPDay29 As Short = 73
    Private Const ColDDay29 As Short = 74
    Private Const ColPDay30 As Short = 75
    Private Const ColDDay30 As Short = 76
    Private Const ColPDay31 As Short = 77
    Private Const ColDDay31 As Short = 78
    Private Const ColTotalDesp As Short = 79
    Private Const ColDSBalQty As Short = 80
    Private Const ColRate As Short = 81
    Private Const ColBalAmount As Short = 82

    'Dim ColMaxCol As Long

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

        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        'Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mTillDateSchdValue, mTillDateDespatchValue, mSchdValue, mDespatchValue, mTillDateSchdQty, mTillDateDespQty)

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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mDate As Integer

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
        'Call InsertRecdQty()
        FormatSprdMain(-1)
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPExport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Schedule Vs Despatch Register"
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

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            optShow(1).Checked = True
        Else
            optShow(0).Checked = True
        End If

        Call PrintStatus(True)



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
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

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDSVsDSPExport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        Dim mMonthDays As Integer

        With SprdMain

            .MaxCols = ColBalAmount
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight * 1)
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
            .set_ColWidth(ColSupplierName, 22)

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

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPONo, 8)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY

            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            .set_ColWidth(ColPODate, 8)
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
            .set_ColWidth(ColItemDesc, 22)

            .ColsFrozen = ColItemDesc

            For cntCol = ColPreviousMonthPlan To ColDSCurrBalQty
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

            .Col = ColOurSONo
            .ColHidden = True

            .Col = ColOurSoDate
            .ColHidden = True

            .Col = ColDSOPBalQty
            .ColHidden = IIf(optShow(1).Checked = True, False, True)

            .Col = ColDSCurrBalQty
            .ColHidden = IIf(optShow(1).Checked = True, False, True)


            For cntCol = ColPDay1 To ColBalAmount
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

            '.Col = ColSupplierCode
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '.Col = ColSupplierName
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

            '.Col = ColDSNo
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '.Col = ColAmendNo
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

            '.Col = ColOurSONo
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

            '.Col = ColOurSoDate
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

            '.Col = ColPONo
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '.Col = ColPODate
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            ''        .Col = ColPOAmendNo
            ''        .ColMerge = MergeAlways
            '.Col = ColAmendNo
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways


            '.Col = ColItemDesc
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '.Col = ColItemCode
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways


            '.Col = ColDSQty
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '.Col = ColDSOPBalQty
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '.Col = ColDSCurrBalQty
            '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways


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
        Dim mStateDate As String
        Dim mCurrMonth As String
        Dim mLastDay As Integer
        Dim mMonthLastDate As String
        Dim mMonth As String

        mStateDate = "01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mMonth = VB6.Format(lblNewDate.Text, "MM/YYYY")

        mCurrMonth = VB6.Format(lblNewDate.Text, "MMYYYY")

        mLastDay = MainClass.LastDay(Month(CDate(mStateDate)), Year(CDate(mStateDate)))
        mMonthLastDate = VB6.Format(mLastDay, "00") & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        MakeSQL = " SELECT " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), "

        MakeSQL = MakeSQL & vbCrLf & " IH.AUTO_KEY_SO, IH.SO_DATE, CUST_SO_NO, CUST_SO_DATE," '' SO_AMEND_NO, "

        MakeSQL = MakeSQL & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, "

        MakeSQL = MakeSQL & vbCrLf & "TO_CHAR(SUM(PLANNED_QTY)) AS PLANNED_QTY, "

        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " SUM(PLANNED_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,ID.ITEM_CODE,TO_DATE('" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) DSBLQTY,"
            MakeSQL = MakeSQL & vbCrLf & " SUM(CASE WHEN SERIAL_DATE <= TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN PLANNED_QTY ELSE 0 END)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,ID.ITEM_CODE,TO_DATE('" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS CURRBAL, "

        Else
            MakeSQL = MakeSQL & vbCrLf & " SUM(PLANNED_QTY),"
            MakeSQL = MakeSQL & vbCrLf & " SUM(PLANNED_QTY),"
        End If


        MakeSQL = MakeSQL & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "01" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY1," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("01/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY1," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "02" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY2," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("02/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY2," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "03" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY3," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("03/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY3," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "04" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY4," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("04/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY4," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "05" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY5," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("05/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY5," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "06" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY6," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("06/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY6," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "07" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY7," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("07/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY7,"

        MakeSQL = MakeSQL & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "08" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY8," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("08/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY8," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "09" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY9," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("09/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY9," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "10" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY10," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("10/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY10," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "11" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY11," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("11/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY11," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "12" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY12," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("12/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY12," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "13" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY13," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("13/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY13," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "14" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY14," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("14/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY14,"

        MakeSQL = MakeSQL & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "15" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY15," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("15/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY15," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "16" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY16," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("16/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY16," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "17" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY17," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("17/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY17," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "18" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY18," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("18/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY18," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "19" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY19," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("19/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY19," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "20" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY20," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("20/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY20," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "21" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY21," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("21/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY21,"

        MakeSQL = MakeSQL & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "22" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY22," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("22/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY22," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "23" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY23," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("23/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY23," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "24" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY24," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("24/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY24," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "25" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY25," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("25/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY25," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "26" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY26," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("26/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY26," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "27" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY27," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("27/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY27," & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "28" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY28," & vbCrLf _
            & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("28/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY28,"

        If mLastDay >= 29 Then
            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "29" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY29," & vbCrLf _
                & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("29/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY29,"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                & " '' AS DAY29," & vbCrLf _
                & " '' AS DDAY29,"
        End If

        If mLastDay >= 30 Then
            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "30" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY30," & vbCrLf _
                & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("30/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY30,"
        Else
            MakeSQL = MakeSQL & vbCrLf _
              & " '' AS DAY30," & vbCrLf _
              & " '' AS DDAY30,"
        End If

        If mLastDay >= 31 Then
            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DDMMYYYY')= '" & "31" & mCurrMonth & "' THEN PLANNED_QTY ELSE 0 END)) AS DAY31," & vbCrLf _
                & " GETDSDAYDEPATCHQTY (IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format("31/" & mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DDAY31,"
        Else
            MakeSQL = MakeSQL & vbCrLf _
              & " '' AS DAY31," & vbCrLf _
              & " '' AS DDAY31,"
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS TotalDesp, " & vbCrLf _
            & " SUM(PLANNED_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('" & VB6.Format(mMonthLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DSBalQty, " & vbCrLf _
            & " 0 AS Rate, " & vbCrLf _
            & " 0 AS BalAmount "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID, DSP_SALEORDER_HDR IHS," & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_SO=IHS.AUTO_KEY_SO" & vbCrLf & " AND IH.COMPANY_CODE=IHS.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"

        MakeSQL = MakeSQL & vbCrLf & " AND IHS.SO_STATUS='O' AND SO_APPROVED='Y'" '' AND IHS.ORDER_TYPE='C'"

        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IHS.ORDER_TYPE='C'"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IHS.ORDER_TYPE='O'"
        End If
        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & RsCompany!FYEAR & ""

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

        MakeSQL = MakeSQL & vbCrLf & " AND IH.SCHLD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.SCHLD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        ''GROUP BY CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "GROUP BY " & vbCrLf & " IH.COMPANY_CODE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), IH.AUTO_KEY_SO, IH.SO_DATE, CUST_SO_NO, CUST_SO_DATE, " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"

        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & ", GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IHS.AUTO_KEY_SO,ID.ITEM_CODE,'" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "')"
        End If

        ''ORDER CLAUSE...
        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "HAVING SUM(PLANNED_QTY)-GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IHS.AUTO_KEY_SO,ID.ITEM_CODE,'" & VB6.Format(mStateDate, "DD-MMM-YYYY") & "')>0 "
        End If

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY " & vbCrLf & " IH.COMPANY_CODE,IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"

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

    Private Sub InsertRecdQty()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim I As Integer
        Dim mType As String
        Dim mPartyCode As String
        Dim mPartyName As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim pDateSeries As Integer
        Dim mRecdQty As Double
        Dim mTotRecdQty As Double
        Dim mItemUOM As String
        Dim mLastDate As String
        Dim mSONo As Double
        Dim mDSOPBalQty As Double
        Dim mDSNo As Double
        Dim mAmendNO As Double
        Dim mOurSoDate As String
        Dim mPONo As String
        Dim mPODate As String
        Dim mDSQty As Double
        Dim mDSCurrBalQty As Double

        '
        '    mLastDate = ""
        '    mLastDate = MainClass.LastDay(Month(lblNewDate.Caption), Year(lblNewDate.Caption)) '& "/" & Format(Month(lblNewDate.Caption), "MM/YYYY")
        '    mLastDate = mLastDate & "/" & Format(lblNewDate.Caption, "MM/YYYY")


        With SprdMain
            cntRow = 1
            While cntRow <= .DataRowCnt
                .Row = cntRow

                .Col = ColSupplierCode
                mPartyCode = Trim(.Text)

                .Col = ColSupplierName
                mPartyName = Trim(.Text)

                .Col = ColDSNo
                mDSNo = Val(.Text)

                .Col = ColAmendNo
                mAmendNO = Val(.Text)

                .Col = ColOurSONo
                mSONo = Val(.Text)

                .Col = ColOurSoDate
                mOurSoDate = Trim(.Text)

                .Col = ColPONo
                mPONo = Trim(.Text)

                .Col = ColPODate
                mPODate = Trim(.Text)


                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If

                .Col = ColDSQty
                mDSQty = Val(.Text)

                .Col = ColDSOPBalQty
                mDSOPBalQty = Val(.Text)

                .Col = ColDSCurrBalQty
                mDSCurrBalQty = Val(.Text)


                If mType = "P" Then
                    .Row = cntRow + 1
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

                    .Col = ColDSNo
                    .Text = CStr(mDSNo)

                    .Col = ColAmendNo
                    .Text = CStr(mAmendNO)

                    .Col = ColOurSONo
                    .Text = CStr(mSONo)

                    .Col = ColOurSoDate
                    .Text = mOurSoDate

                    .Col = ColPONo
                    .Text = mPONo

                    .Col = ColPODate
                    .Text = mPODate

                    .Col = ColDSQty
                    .Text = CStr(mDSQty)

                    .Col = ColDSOPBalQty
                    .Text = CStr(mDSOPBalQty)

                    .Col = ColDSCurrBalQty
                    .Text = CStr(mDSCurrBalQty)

                    If FillRecdQty(cntRow + 1, mSONo, mPartyCode, mItemCode, mItemUOM, mDSOPBalQty) = False Then GoTo ErrPart
                End If

                cntRow = cntRow + 1
                .Row = .Row + 1

            End While

        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Function FillRecdQty(ByRef pRow As Integer, ByRef pSONo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pOPDSQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDesp As ADODB.Recordset
        Dim mDespDate As String
        Dim mDate As Integer
        Dim mTotQty(31) As Double
        Dim I As Integer
        Dim mTotalQty As Double
        'Dim mAvgQty As Double
        'Dim mAvgQtyAchieved As Double
        'Dim mRejDesp As Double
        Dim mTillDateDesp As Double

        Dim mRate As Double
        Dim mLastDate As String

        SqlStr = ""
        For mDate = 1 To 31
            mTotQty(mDate) = 0
        Next

        I = I

        mLastDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        '    For I = ColStockQty + 1 To ColMaxCol - 6
        '        SprdMain.Row = pRow
        '        SprdMain.Col = I
        '        SprdMain.Text = Format(0, "0.00")
        '    Next

        mTotalQty = 0

        SqlStr = "SELECT IH.DESP_DATE, SUM(PACKED_QTY) AS TOTQTY"

        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH,DSP_DESPATCH_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.SONO =" & pSONo & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('E','P') "

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.DESP_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.DESP_DATE "
        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.DESP_DATE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsDesp.EOF Then
            Do While Not RsDesp.EOF
                mDespDate = IIf(IsDbNull(RsDesp.Fields("DESP_DATE").Value), "", RsDesp.Fields("DESP_DATE").Value)
                mDate = VB.Day(CDate(mDespDate))
                mTotQty(mDate) = mTotQty(mDate) + Val(IIf(IsDbNull(RsDesp.Fields("TOTQTY").Value), 0, RsDesp.Fields("TOTQTY").Value))
                RsDesp.MoveNext()
            Loop
            mDate = 1

            'For I = ColDay1 To ColDay31
            '    SprdMain.Row = pRow
            '    SprdMain.Col = I
            '    SprdMain.Text = VB6.Format(mTotQty(mDate), "0.00")
            '    mTotalQty = mTotalQty + mTotQty(mDate)
            '    mDate = mDate + 1
            'Next
        End If

        SprdMain.Row = pRow
        SprdMain.Col = ColTotalDesp
        SprdMain.Text = VB6.Format(mTotalQty, "0.00")

        SprdMain.Col = ColDSBalQty
        SprdMain.Text = VB6.Format(pOPDSQty - mTotalQty, "0.00")

        SprdMain.Col = ColRate
        mRate = GetSORate(pSupplierCode, pItemCode, VB6.Format(mLastDate, "DD/MM/YYYY"))
        SprdMain.Text = VB6.Format(mRate, "0.00")

        SprdMain.Col = ColBalAmount
        SprdMain.Text = VB6.Format((pOPDSQty - mTotalQty) * mRate, "0.00")

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

        SqlStr = "SELECT ITEM_PRICE AS ITEM_PRICE,IH.CUST_AMEND_NO,ID.AMEND_WEF FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "' AND SO_APPROVED='Y'" & vbCrLf & " AND IH.CUST_AMEND_NO= ("

        SqlStr = SqlStr & vbCrLf & " SELECT MAX(IHS.CUST_AMEND_NO) FROM  DSP_SALEORDER_HDR IHS, DSP_SALEORDER_DET IDS" & vbCrLf & " WHERE IHS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IHS.MKEY=IDS.MKEY AND IHS.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND IDS.ITEM_CODE='" & pItemCode & "' AND SO_APPROVED='Y'" & vbCrLf _
            & " AND IDS.AMEND_WEF<=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

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
