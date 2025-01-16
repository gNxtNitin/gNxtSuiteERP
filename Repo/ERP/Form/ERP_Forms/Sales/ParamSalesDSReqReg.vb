Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSalesDSReqReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColDSNo As Short = 2
    Private Const ColDSDate As Short = 3
    Private Const ColScheduleMonth As Short = 4
    Private Const ColAmendNo As Short = 5
    Private Const colSupplier As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemDesc As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColWeek1Qty As Short = 10
    Private Const ColWeek2Qty As Short = 11
    Private Const ColWeek3Qty As Short = 12
    Private Const ColWeek4Qty As Short = 13
    Private Const ColWeek5Qty As Short = 14
    Private Const ColTotQty As Short = 15
    Private Const ColRate As Short = 16
    Private Const ColValue As Short = 17
    Private Const ColMKEY As Short = 18


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
        Me.Close()
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


        Report1.Reset()
        SqlStr = MakeSQL


        mTitle = "Sales Delivery Schedule Requisition Register"

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DSReg.RPT"


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
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSalesDSReqReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sales Delivery Schedule Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSalesDSReqReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        lblTrnType.Text = CStr(-1)

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
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSalesDSReqReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamSalesDSReqReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xDSNo As Double
        Dim xAmendPONo As Double
        'Dim ss As New frmPO

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColDSNo
        xDSNo = Val(SprdMain.Text)

        SprdMain.Col = ColAmendNo
        xAmendPONo = Val(SprdMain.Text)

        SqlStr = "SELECT * from DSP_DELV_SCHLD_REQ_HDR WHERE AUTO_KEY_DELV=" & xDSNo & "" '' AND DELV_AMEND_NO=" & xAmendPONo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            frmSalesDSRequisition.MdiParent = Me.MdiParent
            frmSalesDSRequisition.Show()
            '        frmDS.lblBookType.text = RsTemp!PUR_TYPE & RsTemp!ORDER_TYPE

            frmSalesDSRequisition.frmSalesDSRequisition_Activated(Nothing, New System.EventArgs())

            frmSalesDSRequisition.txtDSNo.Text = RsTemp.Fields("AUTO_KEY_DELV").Value
            '        frmDS.txtAmendNo = RsTemp!AMEND_NO

            frmSalesDSRequisition.txtDSNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))




        End If
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

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = False Then
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

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
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

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
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
            .MaxCols = ColMKEY
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

            .Col = ColDSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDSNo, 9)

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAmendNo, 5)

            .Col = ColDSDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDSDate, 9)

            .Col = ColScheduleMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColScheduleMonth, 9)

            .ColsFrozen = ColItemCode

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 20)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 30)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColWeek1Qty To ColValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

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

        SqlStr = MakeSQL
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
        Dim mLastDate As Date

        ''SELECT CLAUSE...
        If optShow(2).Checked = True Then
            MakeSQL = " SELECT ''," & vbCrLf _
                & " IH.AUTO_KEY_DELV," & vbCrLf _
                & " TO_CHAR(IH.DELV_SCHLD_DATE,'DD/MM/YYYY'), TO_CHAR(ID.SERIAL_DATE,'MON-YYYY')," & vbCrLf _
                & " DELV_AMEND_NO," & vbCrLf _
                & " CMST.SUPP_CUST_NAME," & vbCrLf _
                & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " INVMST.ISSUE_UOM, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='01' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='07' THEN PLANNED_QTY ELSE 0 END)) AS WEEK1_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='08' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='14' THEN PLANNED_QTY ELSE 0 END)) AS WEEK2_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='15' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='21' THEN PLANNED_QTY ELSE 0 END)) AS WEEK3_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='22' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='28' THEN PLANNED_QTY ELSE 0 END)) AS WEEK4_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>'29' THEN PLANNED_QTY ELSE 0 END)) AS WEEK5_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(PLANNED_QTY)), " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)), " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)* SUM(PLANNED_QTY)), " & vbCrLf _
                & " IH.AUTO_KEY_DELV "

            '& vbCrLf _
            '    & " IH.AUTO_KEY_DELV," & vbCrLf _
            '    & " TO_CHAR(IH.DELV_SCHLD_DATE,'DD/MM/YYYY'), TO_CHAR(ID.SERIAL_DATE,'MON-YYYY')," & vbCrLf _
            '    & " DELV_AMEND_NO," & vbCrLf _
            '    & " CMST.SUPP_CUST_NAME," & vbCrLf _
            '    & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            '    & " INVMST.ISSUE_UOM, " & vbCrLf _
            '    & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='01' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='07' THEN PLANNED_QTY ELSE 0 END)) AS WEEK1_QTY, " & vbCrLf _
            '    & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='08' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='14' THEN PLANNED_QTY ELSE 0 END)) AS WEEK2_QTY, " & vbCrLf _
            '    & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='15' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='21' THEN PLANNED_QTY ELSE 0 END)) AS WEEK3_QTY, " & vbCrLf _
            '    & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>='22' AND TO_CHAR(ID.SERIAL_DATE,'DD')<='28' THEN PLANNED_QTY ELSE 0 END)) AS WEEK4_QTY, " & vbCrLf _
            '    & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ID.SERIAL_DATE,'DD')>'29' THEN PLANNED_QTY ELSE 0 END)) AS WEEK5_QTY, " & vbCrLf _
            '    & " TO_CHAR(SUM(PLANNED_QTY)), " & vbCrLf _
            '    & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)), " & vbCrLf _
            '    & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)* SUM(PLANNED_QTY)), " & vbCrLf _
            '    & " IH.AUTO_KEY_DELV "

            ''FROM CLAUSE...
            MakeSQL = MakeSQL & vbCrLf _
                & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DAILY_SCHLD_REQ_DET ID," & vbCrLf _
                & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"
        Else
            MakeSQL = " SELECT ''," & vbCrLf _
                & " IH.AUTO_KEY_DELV," & vbCrLf _
                & " TO_CHAR(IH.DELV_SCHLD_DATE,'DD/MM/YYYY'), TO_CHAR(IH.SCHLD_DATE,'MON-YYYY')," & vbCrLf _
                & " DELV_AMEND_NO," & vbCrLf _
                & " CMST.SUPP_CUST_NAME," & vbCrLf _
                & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " ID.ITEM_UOM, TO_CHAR(WEEK1_QTY), " & vbCrLf _
                & " TO_CHAR(WEEK2_QTY),TO_CHAR(WEEK3_QTY),TO_CHAR(WEEK4_QTY), " & vbCrLf _
                & " TO_CHAR(WEEK5_QTY),TO_CHAR(ITEM_QTY), " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)), " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)* ITEM_QTY), " & vbCrLf _
                & " IH.AUTO_KEY_DELV "

            '

            ''FROM CLAUSE...
            MakeSQL = MakeSQL & vbCrLf _
                & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DELV_SCHLD_REQ_DET ID," & vbCrLf _
                & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"
        End If

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "

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

        If cboStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SCHLD_STATUS='O'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SCHLD_STATUS='C'"
        End If

        If chkHideZeroSchedule.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND ITEM_QTY>0"
        End If

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND IH.DELV_SCHLD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.DELV_SCHLD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND IH.SCHLD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.SCHLD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                & " AND ID.SERIAL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND ID.SERIAL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''ORDER CLAUSE...
        If optShow(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " GROUP BY" & vbCrLf _
                & " IH.COMPANY_CODE, IH.AUTO_KEY_DELV,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO, IH.SUPP_CUST_CODE," & vbCrLf _
                & " TO_CHAR(IH.DELV_SCHLD_DATE,'DD/MM/YYYY'), TO_CHAR(ID.SERIAL_DATE,'MON-YYYY')," & vbCrLf _
                & " DELV_AMEND_NO," & vbCrLf _
                & " CMST.SUPP_CUST_NAME," & vbCrLf _
                & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " INVMST.ISSUE_UOM, " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_SO,ID.ITEM_CODE)) "
        End If

        If optShow(2).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.AUTO_KEY_DELV,IH.SUPP_CUST_CODE, IH.DELV_SCHLD_DATE"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_DELV"
            End If
        Else
            If OptOrderBy(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.AUTO_KEY_DELV,IH.SUPP_CUST_CODE, IH.DELV_SCHLD_DATE,ID.SERIAL_NO"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IH.DELV_SCHLD_DATE,IH.AUTO_KEY_DELV"
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
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
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
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
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillPOCombo()
        On Error GoTo FillErr2
        Dim SqlStr As String = ""

        cboStatus.Items.Clear()
        cboStatus.Items.Add("BOTH")
        cboStatus.Items.Add("Open")
        cboStatus.Items.Add("Closed")
        cboStatus.SelectedIndex = 0

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
End Class
