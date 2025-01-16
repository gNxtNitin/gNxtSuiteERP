Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSRNReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemDesc As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColFromStock As Short = 7
    Private Const ColToStock As Short = 8
    Private Const ColQty As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColValue As Short = 11
    Private Const ColWt As Short = 12
    Private Const ColRemarks As Short = 13
    Private Const ColBookType As Short = 14
    Private Const ColBookSubType As Short = 15
    Private Const ColMKEY As Short = 16
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
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

    Private Sub chkNotPosted_Click()
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnIssue(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        mTitle = "STORE RETURN NOTE Register "

        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        If chkRate.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SRNREG.rpt"
            SqlStr = MakeSQL
        Else
            If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr
            SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SRNREGWIThRate.rpt"
        End If


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
    Private Sub frmParamSRNReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Store Return Note Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSRNReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        Call FillSRNCombo()

        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSRNReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamSRNReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
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
        Dim xSTRNo As Double
        Dim mBookType As String
        Dim mBookSubType As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xSTRNo = Val(SprdMain.Text)

        SprdMain.Col = ColBookType
        mBookType = Trim(SprdMain.Text)

        SprdMain.Col = ColBookSubType
        mBookSubType = Trim(SprdMain.Text)

        If cboStatus.SelectedIndex = 2 Then
            myMenu = "mnuStoreRtnNoteFeedBack"
            frmStoreRetNote.lblBookType.Text = mBookType
            frmStoreRetNote.lblBookSubType.Text = mBookSubType
            frmStoreRetNote.lblUpdate.Text = "Y"
            frmStoreRetNote.lblAction.Text = "F"
        Else
            frmStoreRetNote.lblBookType.Text = mBookType
            frmStoreRetNote.lblBookSubType.Text = mBookSubType
            frmStoreRetNote.lblUpdate.Text = "N"
            frmStoreRetNote.lblAction.Text = "E"

            If mBookType = "S" And mBookSubType = "O" Then
                myMenu = "mnuStoreRtnNoteConvertion"
            ElseIf mBookType = "P" And mBookSubType = "O" Then
                myMenu = "mnuStoreRtnNoteGeneral"
            ElseIf mBookType = "S" And mBookSubType = "F" Then
                myMenu = "mnuStoreRtnNoteGeneralFG"
            ElseIf mBookType = "P" And mBookSubType = "L" Then
                myMenu = "mnuStoreRtnNoteLR"
            ElseIf mBookType = "P" And mBookSubType = "S" Then
                myMenu = "mnuStoreRtnNoteScrap"
            ElseIf mBookType = "P" And mBookSubType = "W" Then
                myMenu = "mnuStoreRtnNoteWIPScrap"
            End If
        End If
        frmStoreRetNote.MdiParent = Me.MdiParent
        frmStoreRetNote.Show()
        frmStoreRetNote.frmStoreRetNote_Activated(Nothing, New System.EventArgs())

        frmStoreRetNote.txtSTNNo.Text = CStr(xSTRNo)
        frmStoreRetNote.txtSTNNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
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

        'Dim cntCol As Integer
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

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 9)
            .ColHidden = IIf(OptShow(0).Checked = True, True, False)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 9)
            .ColHidden = IIf(OptShow(0).Checked = True, True, False)

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
            .set_ColWidth(ColItemDesc, 25)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            .Col = ColFromStock
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColFromStock, 6)

            .Col = ColToStock
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColToStock, 6)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColQty, 9)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRate, 9)
            .ColHidden = IIf(chkRate.CheckState = System.Windows.Forms.CheckState.UnChecked, True, False)

            .Col = ColValue
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColValue, 9)
            .ColHidden = IIf(chkRate.CheckState = System.Windows.Forms.CheckState.UnChecked, True, False)

            .Col = ColWt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColValue, 9)
            .ColHidden = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 15)
            .ColHidden = IIf(OptShow(0).Checked = True, True, False)

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 8)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 8)
            .ColHidden = True

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
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

        If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call FillItemRate()
        End If
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub FillItemRate()

        'Dim mGroup As String
        Dim cntRow As Integer
        Dim mQty As Double
        Dim mItemRate As Double
        Dim mItemValue As Double
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mVDate As String = ""
        Dim mCostType As String
        Dim mStockType As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow


                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                .Col = ColFromStock
                mStockType = Trim(.Text)

                .Col = ColRefDate
                If OptShow(1).Checked = True Then
                    mVDate = VB6.Format(.Text, "DD/MM/YYYY")
                Else
                    mVDate = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
                End If

                .Col = ColQty
                mQty = Val(.Text)

                If mQty = 0 Then
                    mItemRate = CDbl("0.00")
                    mItemCode = ""
                    mUOM = ""
                Else

                    If cboType.SelectedIndex = 6 Or cboType.SelectedIndex = 7 Then
                        SqlStr = "SELECT GetLastSaleRate (" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mItemCode & "','" & VB6.Format(mVDate, "DD-MMM-YYYY") & "') AS RATE FROM DUAL"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mItemRate = IIf(IsDbNull(RsTemp.Fields("Rate").Value), 0, RsTemp.Fields("Rate").Value)
                        End If

                    Else
                        mQty = IIf(mQty = 0, 1, mQty)
                        mCostType = "P"
                        If CheckItemBom(mItemCode) = True And mCostType <> "S" Then
                            mItemValue = GetLatestWIPCost(mItemCode, mUOM, System.Math.Abs(mQty), mVDate, mCostType, mStockType, "STR")
                        Else
NextVal:
                            mItemValue = GetLatestItemCostFromMRR(mItemCode, mUOM, System.Math.Abs(mQty), mVDate, mCostType, mStockType, "STR")
                        End If
                        mItemRate = System.Math.Abs(mItemValue / mQty)
                    End If
                End If

                .Col = ColRate
                .Text = VB6.Format(System.Math.Abs(mItemRate), "0.000")

                .Col = ColValue
                mItemValue = CDbl(VB6.Format(mItemRate * mQty, "0.000"))
                .Text = CStr(mItemValue)
            Next
        End With
    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mDivision As Double

        ''SELECT CLAUSE...


        If OptShow(1).Checked = True Then
            MakeSQL = " SELECT ''," & vbCrLf & " IGH.AUTO_KEY_SRN," & vbCrLf & " TO_CHAR(IGH.SRN_DATE,'DD/MM/YYYY')," & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf & " IGD.ITEM_UOM, FROM_STOCK_TYPE, TO_STOCK_TYPE, TO_CHAR(IGD.RTN_QTY), '0','0',TO_CHAR(IGD.RTN_QTY*INVMST.ITEM_WEIGHT)," & vbCrLf & " IGD.REMARKS, IGH.BOOKTYPE, IGH.BOOKSUBTYPE, IGH.AUTO_KEY_SRN"

        Else
            MakeSQL = " SELECT ''," & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf & " IGD.ITEM_UOM, FROM_STOCK_TYPE, TO_STOCK_TYPE, TO_CHAR(SUM(IGD.RTN_QTY)), '0','0',TO_CHAR(SUM(IGD.RTN_QTY*INVMST.ITEM_WEIGHT))," & vbCrLf & " '', IGH.BOOKTYPE, IGH.BOOKSUBTYPE, ''"

        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM INV_SRN_HDR IGH, INV_SRN_DET IGD," & vbCrLf & " INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_SRN=IGD.AUTO_KEY_SRN" & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        If cboStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.STATUS='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.STATUS='N'"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.PRD_FLOOR='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.PRD_FLOOR='Y'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND INVMST.TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
        'End If

        If cboType.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='S' AND IGH.BOOKSUBTYPE='O'"
        ElseIf cboType.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='P' AND IGH.BOOKSUBTYPE='L'"
        ElseIf cboType.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='P' AND IGH.BOOKSUBTYPE='S'"
        ElseIf cboType.SelectedIndex = 4 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='P' AND IGH.BOOKSUBTYPE='O'"
        ElseIf cboType.SelectedIndex = 5 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='P' AND IGH.BOOKSUBTYPE='W'"
        ElseIf cboType.SelectedIndex = 6 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='S' AND IGH.BOOKSUBTYPE='F'"
        ElseIf cboType.SelectedIndex = 7 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='S' AND IGH.BOOKSUBTYPE='C'"
        ElseIf cboType.SelectedIndex = 8 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.BOOKTYPE='P' AND IGH.BOOKSUBTYPE='R'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IGH.SRN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.SRN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptShow(1).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.AUTO_KEY_SRN, IGH.SRN_DATE ,IGD.SERIAL_NO"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_SRN, IGH.SRN_DATE"
            End If
        Else
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM, FROM_STOCK_TYPE, TO_STOCK_TYPE,IGH.BOOKTYPE, IGH.BOOKSUBTYPE"
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC"
        End If
        'End If
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
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            TxtItemName.Text = MasterNo
            Else
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
    Private Sub FillSRNCombo()

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

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("STORE")
        cboShow.Items.Add("PRODUCTION")
        cboShow.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("ALL")
        cboStatus.Items.Add("Posted")
        cboStatus.Items.Add("Not Posted")
        cboStatus.SelectedIndex = 0

        cboType.Items.Clear()
        cboType.Items.Add("ALL")
        cboType.Items.Add("Store Convertion")
        cboType.Items.Add("Line Rejection")
        cboType.Items.Add("Scrap")
        cboType.Items.Add("General")
        cboType.Items.Add("WIP Scrap")
        cboType.Items.Add("FG Scrap (Excisable)")
        cboType.Items.Add("CR Scrap (Excisable)")
        cboType.Items.Add("Rework Scrap")
        cboType.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
End Class
