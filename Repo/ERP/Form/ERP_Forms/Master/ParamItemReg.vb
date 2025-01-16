Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamItemReg
   Inherits System.Windows.Forms.Form
   Dim XRIGHT As String
   Private Const RowHeight As Short = 20

   Private FormSize As New Resizeclass

   Private Const ColCode As Short = 1
   Private Const ColName As Short = 2
   Private Const ColPartNo As Short = 3
   Private Const ColPurUOM As Short = 4
   Private Const ColIssueUOM As Short = 5
   Private Const ColHSNCode As Short = 6
   Private Const ColGSTClass As Short = 7
   Private Const ColFactor As Short = 8
   Private Const ColItemModel As Short = 9
   Private Const ColCategory As Short = 10
   Private Const ColSubCategory As Short = 11
   Private Const ColMinQty As Short = 12
   Private Const ColMaxQty As Short = 13
   Private Const ColReoderQty As Short = 14
   Private Const ColInventoryDays As Short = 15
   Private Const ColScrapItemCode As Short = 16
   Private Const ColProductType As Short = 17
   Private Const ColGUID As Short = 18
   Private Const ColLockQty As Short = 19

    Private Const ColITEM_STATUS As Short = 20
    Private Const ColITEM_WEIGHT As Short = 21
    Private Const ColITEM_TECH_DESC As Short = 22
    Private Const ColITEM_GRADE As Short = 23
    Private Const ColITEM_QAS_NO As Short = 24
    Private Const ColIDENT_MARK As Short = 25
    Private Const ColITEM_SURFACE_AREA As Short = 26
    Private Const ColDRW_REVNO As Short = 27
    Private Const ColDRW_REVEFF_DATE As Short = 28
    Private Const ColPACK_STD As Short = 29
    Private Const ColMAT_DESC As Short = 30
    Private Const ColMAT_LEN As Short = 31
    Private Const ColMAT_WIDTH As Short = 32
    Private Const ColMAT_THICHNESS As Short = 33
    Private Const ColMAT_DENSITY As Short = 34
    Private Const ColITEM_LOCATION As Short = 35
    Private Const ColOUTER_PACK_STD_PER_INNER As Short = 36
    Private Const ColOUTER_PACK_STD_PER_UOM As Short = 37
    Private Const ColOUTER_PACK_ITEM_CODE As Short = 38
    Private Const ColAUTO_QC As Short = 39
    Private Const ColITEM_JW_UOM As Short = 40
    Private Const ColHEAT_NO_REQ As Short = 41


    Dim CurrFormWidth As Integer
   Dim CurrFormHeight As Integer
   Dim mActiveRow As Integer
   Dim FormActive As Boolean
   Private Sub PrintStatus(ByRef pPrintEnable As Object)
      CmdPreview.Enabled = pPrintEnable
      cmdPrint.Enabled = pPrintEnable
   End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged, cboTransaction.TextChanged
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

   Private Sub chkDate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDate.CheckStateChanged
      Call PrintStatus(False)
      If chkDate.CheckState = System.Windows.Forms.CheckState.Checked Then
         txtDateFrom.Enabled = False
         txtDateTo.Enabled = False
      Else
         txtDateFrom.Enabled = True
         txtDateTo.Enabled = True
      End If
   End Sub

   Private Sub ChkSearch_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSearch.CheckStateChanged
      Call PrintStatus(False)
      If ChkSearch.CheckState = System.Windows.Forms.CheckState.Checked Then
         cboFieldName.Enabled = False
         txtFieldValue.Enabled = False
      Else
         cboFieldName.Enabled = True
         txtFieldValue.Enabled = True
      End If
   End Sub


   Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

   Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

      On Error GoTo ERR1
      Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        'Select Record for print...

        SqlStr = ""

        SqlStr = MakeSQL("S")

        mTitle = "ITEM Register"
        If cboCategory.SelectedIndex <> 0 Then
            mSubTitle = "List of " & cboCategory.Text
        End If

        mReportFileName = "ItemReg.Rpt"

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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamItemReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamItemReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        'Dim mFieldValue As Variant

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        'Me.Top = 0
        'Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        '    cboCategory.Clear
        '    cboCategory.AddItem "All"
        '    cboCategory.AddItem "Customer"
        '    cboCategory.AddItem "Supplier"
        '    cboCategory.AddItem "Employee"
        '    cboCategory.AddItem "1- Cash"
        '    cboCategory.AddItem "2- Bank"
        '    cboCategory.AddItem "Other"
        '    cboCategory.AddItem "Fixed Assets"

        '    If MainClass.FillCombo(cboCategory, "INV_GENERAL_MST", "GEN_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then GoTo BSLError
        Call MainClass.FillCombo(cboCategory, "INV_GENERAL_MST", "GEN_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'")
        cboCategory.SelectedIndex = 0


        SqlStr = "SELECT * FROM INV_ITEM_MST WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        '    mFieldName = ""
        cboFieldName.Items.Clear()
        For I = 0 To RsTemp.Fields.Count - 1
            cboFieldName.Items.Add(RsTemp.Fields(I).Name)
        Next
        cboFieldName.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Active")
        cboShow.Items.Add("Inactive")
        cboShow.SelectedIndex = 0

        cboLockMRR.Items.Clear()
        cboLockMRR.Items.Add("All")
        cboLockMRR.Items.Add("Yes")
        cboLockMRR.Items.Add("No")
        cboLockMRR.SelectedIndex = 0

        cboOverMax.Items.Clear()
        cboOverMax.Items.Add("All")
        cboOverMax.Items.Add("Yes")
        cboOverMax.Items.Add("No")
        cboOverMax.SelectedIndex = 0

        cboLockSchedule.Items.Clear()
        cboLockSchedule.Items.Add("All")
        cboLockSchedule.Items.Add("Yes")
        cboLockSchedule.Items.Add("No")
        cboLockSchedule.SelectedIndex = 0

        cboTransaction.Items.Clear()
        cboTransaction.Items.Add("All")
        cboTransaction.Items.Add("With Transaction")
        cboTransaction.Items.Add("Without Transaction")
        cboTransaction.SelectedIndex = 0



        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdsearch.Enabled = False

        chkDate.CheckState = System.Windows.Forms.CheckState.Checked
        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False

        ChkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        cboFieldName.Enabled = False
        txtFieldValue.Enabled = False

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "dd-MMM-yyyy")
        txtDateTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd-MMM-yyyy")

        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", TxtItemName)

        Call PrintStatus(True)
        Call Show1("L")
        FormSize.Init(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamItemReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)

        '    FormSize.formResize Me

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamItemReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'Dim xCode As String

        'SprdMain.Row = SprdMain.ActiveRow

        'SprdMain.Col = ColCode
        'xCode = Trim(Me.SprdMain.Text)

        'If Trim(xCode) <> "" Then
        '    frmItemMaster.MdiParent = Me.MdiParent

        '    frmItemMaster.Show()
        '    frmItemMaster.frmItemMaster_Activated(Nothing, New System.EventArgs())

        '    frmItemMaster.txtItemCode.Text = xCode
        '    frmItemMaster.txtItemCode_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        '    'frmItemMaster.Show()
        '    'frmItemMaster.Form_Activate
        '    'frmItemMaster.txtCode = xCode
        '    'frmItemMaster.txtCode_Validate False
        'End If

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent)
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            'UltraGrid1_DoubleClick(UltraGrid1, New UltraGrid1_DoubleClick(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub txtFieldValue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFieldValue.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtFieldValue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFieldValue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFieldValue.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster txtItemName, "INV_ITEM_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
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
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        'Dim cntCol As Integer
        'With SprdMain
        '    .MaxCols = ColLockQty
        '    .set_RowHeight(0, RowHeight * 1.25)
        '    .set_ColWidth(0, 4.5)

        '    .set_RowHeight(-1, RowHeight)
        '    .Row = -1

        '    .Col = ColCode
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColCode, 6)

        '    .Col = ColName
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColName, 25)
        '    .ColsFrozen = ColName

        '    .Col = ColPartNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColPartNo, 5)

        '    .Col = ColCategory
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColCategory, 15)

        '    .Col = ColSubCategory
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColSubCategory, 15)

        '    .Col = ColPurUOM
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColPurUOM, 5)

        '    .Col = ColIssueUOM
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColIssueUOM, 5)

        '    .Col = ColHSNCode
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColHSNCode, 6)

        '    .Col = ColGSTClass
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColGSTClass, 8)

        '    .Col = ColFactor
        '    .CellType = SS_CELL_TYPE_FLOAT
        '    .TypeFloatDecimalPlaces = 2
        '    .TypeFloatDecimalChar = Asc(".")
        '    .TypeFloatMax = CDbl("999999999.999")
        '    .TypeFloatMin = CDbl("-999999999.999")
        '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '    .set_ColWidth(ColFactor, 5)

        '    .Col = ColMinQty
        '    .CellType = SS_CELL_TYPE_FLOAT
        '    .TypeFloatDecimalPlaces = 2
        '    .TypeFloatDecimalChar = Asc(".")
        '    .TypeFloatMax = CDbl("999999999.999")
        '    .TypeFloatMin = CDbl("-999999999.999")
        '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '    .set_ColWidth(ColMinQty, 5)

        '    .Col = ColMaxQty
        '    .CellType = SS_CELL_TYPE_FLOAT
        '    .TypeFloatDecimalPlaces = 2
        '    .TypeFloatDecimalChar = Asc(".")
        '    .TypeFloatMax = CDbl("999999999.999")
        '    .TypeFloatMin = CDbl("-999999999.999")
        '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '    .set_ColWidth(ColMaxQty, 5)

        '    .Col = ColReoderQty
        '    .CellType = SS_CELL_TYPE_FLOAT
        '    .TypeFloatDecimalPlaces = 2
        '    .TypeFloatDecimalChar = Asc(".")
        '    .TypeFloatMax = CDbl("999999999.999")
        '    .TypeFloatMin = CDbl("-999999999.999")
        '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '    .set_ColWidth(ColReoderQty, 5)

        '    .Col = ColInventoryDays
        '    .CellType = SS_CELL_TYPE_FLOAT
        '    .TypeFloatDecimalPlaces = 2
        '    .TypeFloatDecimalChar = Asc(".")
        '    .TypeFloatMax = CDbl("999999999.999")
        '    .TypeFloatMin = CDbl("-999999999.999")
        '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '    .set_ColWidth(ColInventoryDays, 5)

        '    .Col = ColProductType
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColProductType, 15)

        '    .Col = ColGUID
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColGUID, 15)

        '    .Col = ColLockQty
        '    .CellType = SS_CELL_TYPE_FLOAT
        '    .TypeFloatDecimalPlaces = 2
        '    .TypeFloatDecimalChar = Asc(".")
        '    .TypeFloatMax = CDbl("999999999.999")
        '    .TypeFloatMin = CDbl("-999999999.999")
        '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '    .set_ColWidth(ColLockQty, 5)


        '    MainClass.SetSpreadColor(SprdMain, -1)
        '    MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
        '    SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
        '    SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        '    SprdMain.DAutoCellTypes = True
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        'End With
    End Sub
    Private Function Show1(pShow As String) As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pShow)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        FillUltraGrid(SqlStr)
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
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
            'If pShowType = "L" Then
            '    Me.UltraGrid1.DataSource = Me.UltraDataSource2
            '    Me.UltraDataSource2.Band.Columns.Add("Item Code", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Item Description", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Customer Part No", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Purchase UOM", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Issue UOM", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("HSN Code", GetType(String))
            '    'Me.UltraDataSource2.Band.Columns.Add("GST Relevant", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("GST Class", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("UOM Factor", GetType(String))

            '    Me.UltraDataSource2.Band.Columns.Add("Item Model", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Category", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Sub Category", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Minimum Qty", GetType(Decimal))
            '    Me.UltraDataSource2.Band.Columns.Add("Maximum Qty", GetType(Decimal))
            '    Me.UltraDataSource2.Band.Columns.Add("Reorder Qty", GetType(Decimal))
            '    Me.UltraDataSource2.Band.Columns.Add("Economic Qty", GetType(Decimal))

            '    Me.UltraDataSource2.Band.Columns.Add("Scrap Item Code", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Product Type", GetType(String))
            '    Me.UltraDataSource2.Band.Columns.Add("Group Item Code", GetType(String))

            '    Me.UltraDataSource2.Band.Columns.Add("Stock Lock Qty", GetType(Decimal))

            'Else
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColName - 1).Header.Caption = "Item Description"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartNo - 1).Header.Caption = "Customer Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPurUOM - 1).Header.Caption = "Purchase UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIssueUOM - 1).Header.Caption = "Issue UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColHSNCode - 1).Header.Caption = "HSN Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTClass - 1).Header.Caption = "GST Class"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColFactor - 1).Header.Caption = "UOM Factor"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemModel - 1).Header.Caption = "Item Model"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCategory - 1).Header.Caption = "Category"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Header.Caption = "Sub Category"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMinQty - 1).Header.Caption = "Minimum Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMaxQty - 1).Header.Caption = "Maximum Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColReoderQty - 1).Header.Caption = "Reorder Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInventoryDays - 1).Header.Caption = "Economic Qty"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColScrapItemCode - 1).Header.Caption = "Scrap Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProductType - 1).Header.Caption = "Product Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGUID - 1).Header.Caption = "Group Item Code"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLockQty - 1).Header.Caption = "Stock Lock Qty"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_STATUS - 1).Header.Caption = "ITEM_STATUS"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_WEIGHT - 1).Header.Caption = "ITEM_WEIGHT"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_TECH_DESC - 1).Header.Caption = "ITEM_TECH_DESC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_GRADE - 1).Header.Caption = "ITEM_GRADE"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_QAS_NO - 1).Header.Caption = "ITEM_QAS_NO"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIDENT_MARK - 1).Header.Caption = "IDENT_MARK"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_SURFACE_AREA - 1).Header.Caption = "ITEM_SURFACE_AREA"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDRW_REVNO - 1).Header.Caption = "DRW_REVNO"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDRW_REVEFF_DATE - 1).Header.Caption = "DRW_REVEFF_DATE"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPACK_STD - 1).Header.Caption = "PACK_STD"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_DESC - 1).Header.Caption = "MAT_DESC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_LEN - 1).Header.Caption = "MAT_LEN"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_WIDTH - 1).Header.Caption = "MAT_WIDTH"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_THICHNESS - 1).Header.Caption = "MAT_THICHNESS"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_DENSITY - 1).Header.Caption = "MAT_DENSITY"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_LOCATION - 1).Header.Caption = "ITEM_LOCATION"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_STD_PER_INNER - 1).Header.Caption = "OUTER_PACK_STD_PER_INNER"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_STD_PER_UOM - 1).Header.Caption = "OUTER_PACK_STD_PER_UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_ITEM_CODE - 1).Header.Caption = "OUTER_PACK_ITEM_CODE"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAUTO_QC - 1).Header.Caption = "AUTO_QC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_JW_UOM - 1).Header.Caption = "ITEM_JW_UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColHEAT_NO_REQ - 1).Header.Caption = "HEAT_NO_REQ"



            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMinQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMaxQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColReoderQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInventoryDays - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMinQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMaxQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColReoderQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInventoryDays - 1).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPACK_STD - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_DESC - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_LEN - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_WIDTH - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_THICHNESS - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_DENSITY - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_STD_PER_INNER - 1).CellAppearance.TextHAlign = HAlign.Right


            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLockQty - 1).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPurUOM - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIssueUOM - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColHSNCode - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTClass - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColFactor - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemModel - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCategory - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMinQty - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMaxQty - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColReoderQty - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInventoryDays - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColScrapItemCode - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProductType - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGUID - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLockQty - 1).Width = 90



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_STATUS - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_WEIGHT - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_TECH_DESC - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_GRADE - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_QAS_NO - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIDENT_MARK - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_SURFACE_AREA - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDRW_REVNO - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDRW_REVEFF_DATE - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPACK_STD - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_DESC - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_LEN - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_WIDTH - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_THICHNESS - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMAT_DENSITY - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_LOCATION - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_STD_PER_INNER - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_STD_PER_UOM - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOUTER_PACK_ITEM_CODE - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAUTO_QC - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColITEM_JW_UOM - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColHEAT_NO_REQ - 1).Width = 90



            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Function MakeSQL(pShow As String) As String
        On Error GoTo ERR1
        Dim mAccountCode As String
        Dim mFieldName As String
        Dim mFieldValue As Object

        ''SELECT CLAUSE...
        MakeSQL = " Select INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC As DESCRIPTION, " & vbCrLf _
           & " INVMST.CUSTOMER_PART_NO As PART_NO, INVMST.PURCHASE_UOM As PUR_UNIT, INVMST.ISSUE_UOM As ISS_UNIT, " & vbCrLf _
           & " HSN_CODE, DECODE(GST_ITEMCLASS,0,'0-GST Relevant',DECODE(GST_ITEMCLASS,1,'1-Non GST','2-GST Exempt'))," & vbCrLf _
           & " INVMST.UOM_FACTOR, INVMST.ITEM_MODEL, " & vbCrLf _
           & " GMST.GEN_DESC AS CATEGORY, SMST.SUBCATEGORY_DESC AS SUB_CATEGORY, " & vbCrLf _
           & " INVMST.MINIMUM_QTY,INVMST.MAXIMUM_QTY,INVMST.REORDER_QTY," & vbCrLf _
           & " INVMST.ECONOMIC_QTY, INVMST.SCRAP_ITEM_CODE," & vbCrLf _
           & " INVMST.PRODTYPE_DESC,GROUP_ITEM_CODE,STOCK_LOCK_QTY, "

        MakeSQL = MakeSQL & vbCrLf _
            & " INVMST.ITEM_STATUS, INVMST.ITEM_WEIGHT, ITEM_TECH_DESC," & vbCrLf _
            & " ITEM_GRADE, ITEM_QAS_NO, IDENT_MARK," & vbCrLf _
            & " ITEM_SURFACE_AREA, DRW_REVNO, DRW_REVEFF_DATE," & vbCrLf _
            & " PACK_STD, MAT_DESC, MAT_LEN, MAT_WIDTH," & vbCrLf _
            & " MAT_THICHNESS,MAT_DENSITY,ITEM_LOCATION," & vbCrLf _
            & " OUTER_PACK_STD_PER_INNER,OUTER_PACK_STD_PER_UOM,OUTER_PACK_ITEM_CODE," & vbCrLf _
            & " AUTO_QC,ITEM_JW_UOM,HEAT_NO_REQ"


        MakeSQL = MakeSQL & vbCrLf & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST GMST, INV_SUBCATEGORY_MST SMST " & vbCrLf _
           & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
           & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf _
           & " AND INVMST.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf _
           & " AND INVMST.CATEGORY_CODE=SMST.CATEGORY_CODE" & vbCrLf _
           & " AND INVMST.SUBCATEGORY_CODE=SMST.SUBCATEGORY_CODE" & vbCrLf _
           & " AND GMST.GEN_TYPE='C'"

        If cboLockSchedule.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.SCHEDULE_LOCK='" & VB.Left(cboLockSchedule.Text, 1) & "'"
        End If

        If cboLockMRR.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.MRR_LOCK='" & VB.Left(cboLockMRR.Text, 1) & "'"
        End If

        If cboOverMax.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.MRR_LOCK_OVERMAX='" & VB.Left(cboOverMax.Text, 1) & "'"
        End If

        If cboCategory.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND GMST.GEN_DESC='" & cboCategory.Text & "'"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ITEM_STATUS='A'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ITEM_STATUS='I'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ITEM_CODE='" & mAccountCode & "'"
            End If
        End If

        If ChkSearch.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mFieldName = "INVMST." & Trim(cboFieldName.Text)
            mFieldValue = Trim(txtFieldValue.Text)
            MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & " LIKE '%" & mFieldValue & "%'"
        End If

        If chkDate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ADDDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ADDDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
        End If

        '        And NVL(INVMST.ITEM_CODE,'')  IN ( SELECT ITEM_CODE FROM ( 
        '    Select Case DISTINCT NVL(ITEM_CODE,'') AS ITEM_CODE FROM INV_STOCK_REC_TRN
        'UNION ALL
        'Select Case DISTINCT NVL(ITEM_CODE,'')  AS ITEM_CODE FROM PUR_INDENT_DET
        'UNION ALL
        'Select Case DISTINCT NVL(ITEM_CODE,' ')  AS ITEM_CODE FROM PUR_PURCHASE_DET
        'UNION ALL
        'Select Case DISTINCT NVL(PRODUCT_CODE,'')  AS ITEM_CODE FROM PRD_NEWBOM_HDR
        'UNION ALL
        'Select Case DISTINCT NVL(RM_CODE,'')  AS ITEM_CODE FROM PRD_NEWBOM_DET
        ') )
        'ORDER BY ITEM_SHORT_DESC

        'SELECT  FROM 

        If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
            If cboTransaction.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf _
                    & " AND  INVMST.ITEM_CODE IN (SELECT ITEM_CODE FROM (" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM INV_STOCK_REC_TRN" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_INDENT_DET" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_PURCHASE_DET" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(PRODUCT_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_HDR" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(RM_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_DET" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(MACHINE_ITEM_CODE,' ') ITEM_CODE FROM MAN_MACHINE_MST" & vbCrLf _
                    & " ))"
            ElseIf cboTransaction.SelectedIndex = 2 Then
                MakeSQL = MakeSQL & vbCrLf _
                    & " AND INVMST.ITEM_CODE NOT IN (SELECT ITEM_CODE FROM (" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM INV_STOCK_REC_TRN" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_INDENT_DET" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_PURCHASE_DET" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(PRODUCT_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_HDR" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(RM_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_DET" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(MACHINE_ITEM_CODE,' ') ITEM_CODE FROM MAN_MACHINE_MST" & vbCrLf _
                    & " ))"
            End If
        Else
            If cboTransaction.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf _
                    & " AND INVMST.ITEM_CODE IN (SELECT ITEM_CODE FROM (" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM INV_STOCK_REC_TRN WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_INDENT_DET WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_PURCHASE_DET WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(PRODUCT_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_HDR WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(RM_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_DET WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(MACHINE_ITEM_CODE,' ') ITEM_CODE FROM MAN_MACHINE_MST WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " ))"
            ElseIf cboTransaction.SelectedIndex = 2 Then
                MakeSQL = MakeSQL & vbCrLf _
                    & " AND INVMST.ITEM_CODE NOT IN (SELECT ITEM_CODE FROM (" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM INV_STOCK_REC_TRN WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_INDENT_DET WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(ITEM_CODE,' ') ITEM_CODE FROM PUR_PURCHASE_DET WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(PRODUCT_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_HDR WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(RM_CODE,' ') ITEM_CODE FROM PRD_NEWBOM_DET WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT  NVL(MACHINE_ITEM_CODE,' ') ITEM_CODE FROM MAN_MACHINE_MST WHERE COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf _
                    & " ))"
            End If
        End If



        If pShow = "L" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If

        ''ORDER CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY INVMST.ITEM_SHORT_DESC"
        Else
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY INVMST.ITEM_CODE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean

      On Error GoTo ERR1

      If chkDate.CheckState = System.Windows.Forms.CheckState.UnChecked Then
         If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
         If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
      End If

      If ChkSearch.CheckState = System.Windows.Forms.CheckState.UnChecked Then
         If Trim(cboFieldName.Text) = "" Then
            MsgInformation("Invaild Field Name.")
            cboFieldName.Focus()
            FieldsVerification = False
            Exit Function
         End If

         If Trim(txtFieldValue.Text) = "" Then
            MsgInformation("Invaild Field Value.")
            txtFieldValue.Focus()
            FieldsVerification = False
            Exit Function
         End If
      End If

      If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
         If Trim(txtItemName.Text) = "" Then
            MsgInformation("Invaild Account Name")
            txtItemName.Focus()
            FieldsVerification = False
            Exit Function
         End If
         If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invaild Account Name")
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

   Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
      Call PrintStatus(False)
   End Sub

   Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
      Call PrintStatus(False)
   End Sub

   Private Sub txtPaidDays_Change()
      Call PrintStatus(False)
   End Sub


   Private Sub txtPaidDays_KeyPress(ByRef KeyAscii As Short)

      KeyAscii = MainClass.SetNumericField(KeyAscii)
   End Sub

    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim xCode As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        xCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        If Trim(xCode) <> "" Then
            frmItemMaster.MdiParent = Me.MdiParent

            frmItemMaster.Show()
            frmItemMaster.frmItemMaster_Activated(Nothing, New System.EventArgs())

            frmItemMaster.txtItemCode.Text = xCode
            frmItemMaster.txtItemCode_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

            'frmItemMaster.Show()
            'frmItemMaster.Form_Activate
            'frmItemMaster.txtCode = xCode
            'frmItemMaster.txtCode_Validate False
        End If

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
