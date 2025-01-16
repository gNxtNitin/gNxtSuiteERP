Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb


Friend Class frmParamBOMAppReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColTopProductCode As Short = 2
    Private Const ColProductCode As Short = 3
    Private Const colProductName As Short = 4
    Private Const colProductPartNo As Short = 5
    Private Const colWEFDate As Short = 6
    Private Const ColStatus As Short = 7
    Private Const ColMKEY As Short = 8


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mShow As Boolean
    Private Sub SaveStatus(ByRef pSaveEnable As Boolean)
        cmdSave.Enabled = pSaveEnable
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call SaveStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        'FormatSprdMain(-1)
        If Show1("S") = False Then GoTo ErrPart
        mShow = True
        Call SaveStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamBOMAppReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Pending BOM for Approval"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'FormatSprdMain(-1)
        FormActive = True
        mShow = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamBOMAppReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
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


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        Call SaveStatus(True)
        Call FillIndentCombo()
        Call Show1("L")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamBOMAppReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamBOMAppReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    'Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent)
    '    Dim RowCnt As Integer
    '    Dim mMKEY As String
    '    Dim mStatus As String

    '    If mShow = False Then Exit Sub
    '    '    If ButtonDown = 0 Then Exit Sub

    '    SprdMain.Row = eventArgs.row

    '    SprdMain.Col = ColStatus
    '    mStatus = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")


    '    mShow = False

    '    With SprdMain
    '        For RowCnt = 1 To .MaxRows
    '            .Row = RowCnt

    '            .Col = ColStatus
    '            .Value = IIf(mStatus = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

    '        Next
    '    End With
    '    mShow = True
    'End Sub

    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent)
    '    SprdMain.Row = -1
    '    SprdMain.Col = eventArgs.col
    '    SprdMain.DAutoCellTypes = True
    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdMain.TypeEditLen = 1000
    'End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mProductCode As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mProductCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColProductCode - 1))

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "MNUBOMPRODAPPROVAL", PubDBCn)
        If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
            Exit Sub
        End If

        FrmBOMNew.MdiParent = Me.MdiParent

        FrmBOMNew.Show()
        FrmBOMNew.lblType.Text = "P"
        FrmBOMNew.lblApproval.Text = "Y"

        FrmBOMNew.FrmBOMNew_Activated(Nothing, New System.EventArgs())

        FrmBOMNew.txtProductCode.Text = mProductCode
        FrmBOMNew.txtProductCode_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

    End Sub

    'Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)

    '    'Dim SqlStr As String = ""
    '    Dim mMKEY As String
    '    Dim mProductCode As String
    '    'Dim xQCStatus As String

    '    If Trim(RsCompany.Fields("Company_Name").Value) <> Trim(cboCompany.Text) Then
    '        MsgInformation("Cann't be see Other Unit BOM.")
    '    End If

    '    SprdMain.Row = SprdMain.ActiveRow

    '    SprdMain.Col = ColMKEY
    '    mMKEY = Trim(SprdMain.Text)

    '    SprdMain.Col = ColProductCode
    '    mProductCode = Trim(SprdMain.Text)

    '    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "MNUBOMPRODAPPROVAL", PubDBCn)
    '    If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
    '        Exit Sub
    '    End If

    '    FrmBOMNew.MdiParent = Me.MdiParent

    '    FrmBOMNew.Show()
    '    FrmBOMNew.lblType.Text = "P"
    '    FrmBOMNew.lblApproval.Text = "Y"

    '    FrmBOMNew.FrmBOMNew_Activated(Nothing, New System.EventArgs())

    '    FrmBOMNew.txtProductCode.Text = mProductCode
    '    FrmBOMNew.txtProductCode_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    'End Sub
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call SaveStatus(False)
    End Sub
    Private Sub cboCompany_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCompany.TextChanged
        Call SaveStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call SaveStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        Dim mSqlStr As String

        mSqlStr = " SELECT INV.ITEM_SHORT_DESC, IH.PRODUCT_CODE, IH.WEF,  INV.ISSUE_UOM " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf _
            & " AND BOM_TYPE='P'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(TxtItemName.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND INV.ITEM_SHORT_DESC='" & Trim(TxtItemName.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            TxtItemName.Text = AcName
            If TxtItemName.Enabled = True Then TxtItemName.Focus()
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        'If AcName <> "" Then
        '    TxtItemName.Text = AcName
        'End If
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
    '    With SprdMain
    '        .MaxCols = ColMKEY
    '        .set_RowHeight(0, RowHeight * 1.2)
    '        .set_ColWidth(0, 4.5)

    '        .set_RowHeight(-1, RowHeight)
    '        .Row = -1

    '        .Col = ColLocked
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColLocked, 15)
    '        .ColHidden = True

    '        .Col = ColProductCode
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColProductCode, 8)

    '        .Col = colProductName
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(colProductName, 30)

    '        .Col = colWEFDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(colWEFDate, 10)

    '        .Col = ColStatus
    '        .CellType = SS_CELL_TYPE_CHECKBOX
    '        .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter ''SS_CELL_H_ALIGN_CENTER
    '        .set_ColWidth(ColStatus, 6)
    '        .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

    '        .Col = ColMKEY
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColMKEY, 8)
    '        .ColHidden = True


    '        MainClass.SetSpreadColor(SprdMain, -1)
    '        MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, colWEFDate)
    '        MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMKEY, ColMKEY)

    '        '        SprdMain.OperationMode = OperationModeSingle
    '        '        SprdMain.DAutoCellTypes = True
    '        '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '        '        SprdMain.GridColor = &HC00000
    '    End With
    'End Sub
    Private Function Show1(pShowType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pShowType)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader()


        oledbAdapter.Dispose()
        oledbCnn.Close()

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
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String


        'SELECT CLAUSE...

        MakeSQL = " SELECT '', '' ," & vbCrLf _
            & " IH.PRODUCT_CODE," & vbCrLf _
            & " INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO," & vbCrLf _
            & " IH.WEF, " & vbCrLf _
            & " '', IH.MKEY "


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE "

        'If cboCompany.SelectedIndex > 0 Then
        mCompanyName = Trim(cboCompany.Text)
        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mCompanyCode = MasterNo
        End If
        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=" & MainClass.AllowSingleQuote(mCompanyCode) & ""
        'End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_APPROVED='N'"

        If pShowType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & "AND 1=2"
        End If

        'MakeSQL = MakeSQL & vbCrLf & "CONNECT BY NOCYCLE PRIOR ID.RM_CODE =  IH.PRODUCT_CODE"

        ''ORDER CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.PRODUCT_CODE"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim mUpdateCount As Integer
        Dim mEmpCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mMaxRow As Long
        Dim mRow As UltraGridRow
        Dim mFlag As String

        mMaxRow = UltraGrid1.Rows.Count - 1

        With UltraGrid1
            For cntRow = 0 To mMaxRow
                mRow = Me.UltraGrid1.Rows(cntRow)
                mFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColStatus - 1))
                mMKey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))

                If UCase(mFlag) = "TRUE" Then
                    mEmpCode = Trim(PubUserID)
                    ''Closed all PO
                    SqlStr = "UPDATE PRD_NEWBOM_HDR SET APP_EMP_CODE='" & mEmpCode & "', IS_APPROVED='Y', " & vbCrLf _
                        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE MKEY='" & mMKey & "'"

                    'If cboCompany.SelectedIndex > 0 Then
                    mCompanyName = Trim(cboCompany.Text)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = MasterNo
                    End If
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE='" & MainClass.AllowSingleQuote(mCompanyCode) & "'"
                    'End If

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " BOM Approved.", MsgBoxStyle.Information)
        Call cmdShow_Click(cmdShow, New System.EventArgs())
        Call SaveStatus(True)
        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
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


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Caption = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTopProductCode - 1).Header.Caption = "Top Parent Product Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProductCode - 1).Header.Caption = "Product Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colProductName - 1).Header.Caption = "Product Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colProductPartNo - 1).Header.Caption = "Product Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colWEFDate - 1).Header.Caption = "WEF Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStatus - 1).Header.Caption = "Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "Mkey"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStatus - 1).Style = UltraWinGrid.ColumnStyle.CheckBox

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.FixedHeaderIndicator = FixedHeaderIndicator.None
                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Fixed = True
            Next

            For inti = 0 To UltraGrid1.Rows.Count - 1
                UltraGrid1.Rows(inti).Cells(ColStatus - 1).Value = False
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStatus - 1).CellActivation = Activation.AllowEdit
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTopProductCode - 1).Hidden = True

            'col = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(intLoop + 1)
            'strCelltype = col.Style

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTopProductCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProductCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(colProductName - 1).Width = 350
            UltraGrid1.DisplayLayout.Bands(0).Columns(colProductPartNo - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(colWEFDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStatus - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 120

            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            'Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            'Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")

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

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1

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
    Private Sub FillIndentCombo()
        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing


        cboCompany.Items.Clear()

        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        'cboCompany.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCompany.Items.Add(RS.Fields("Company_Name").Value)
                RS.MoveNext()
            Loop
        End If
        cboCompany.Text = RsCompany.Fields("Company_Name").Value


        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
End Class
