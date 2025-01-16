Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmQuotationAppReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColIndentNo As Short = 2
    Private Const colIndentDate As Short = 3
    Private Const colDeptCode As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColUnit As Short = 7
    Private Const ColQuotation As Short = 8
    Private Const ColSupplier As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColDiscount As Short = 11
    Private Const ColDeliveryTime As Short = 12
    Private Const ColCreditbility As Short = 13
    Private Const ColRemarks As Short = 14
    Private Const ColCompanyName As Short = 15
    Private Const ColStatus As Short = 16
    Private Const ColMKEY As Short = 17


    Dim mClickProcess As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mShow As Boolean
    Private Sub SaveStatus(ByRef pSaveEnable As Boolean)
        cmdSave.Enabled = pSaveEnable
    End Sub

    Private Sub cboSendBack_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call SaveStatus(False)
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
        Dim CntRow As Long

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)
        If Show1() = False Then GoTo ErrPart
        mShow = True
        cmdShow.Enabled = True
        Call SaveStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'For CntRow = 1 To SprdMain.MaxRows
        FormatSprdMain(-1)
        'Next

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmQuotationAppReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Pending Indent for Approval"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        mShow = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmQuotationAppReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        txtdateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmQuotationAppReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmQuotationAppReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Dim RowCnt As Integer
        Dim mCheckIndentNo As Double
        Dim mIndentNo As Double
        Dim mStatus As String
        Dim mRejStatus As String
        Dim mCheckItemCode As String
        Dim mItemCode As String

        If mShow = False Then Exit Sub
        '    If ButtonDown = 0 Then Exit Sub

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColIndentNo
        mCheckIndentNo = Val(SprdMain.Text)

        SprdMain.Col = ColItemCode
        mCheckItemCode = Trim(SprdMain.Text)

        SprdMain.Col = ColStatus
        mStatus = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

        mShow = False

        If mStatus = "Y" Then
            With SprdMain
                For RowCnt = 1 To .MaxRows
                    .Row = RowCnt
                    .Col = ColIndentNo
                    mIndentNo = Val(.Text)

                    .Col = ColItemCode
                    mItemCode = Trim(SprdMain.Text)

                    If mCheckIndentNo = mIndentNo And mCheckItemCode = mItemCode Then
                        .Col = ColStatus
                        .Value = System.Windows.Forms.CheckState.Unchecked
                    End If
                Next
            End With

            SprdMain.Row = eventArgs.row

            SprdMain.Col = ColStatus
            SprdMain.Value = System.Windows.Forms.CheckState.Checked

        End If
        'With SprdMain
        '    For RowCnt = 1 To .MaxRows
        '        .Row = RowCnt
        '        .Col = ColIndentNo
        '        mIndentNo = Val(.Text)
        '        If mCheckIndentNo = mIndentNo Then
        '            .Col = ColStatus
        '            .Value = IIf(mStatus = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        '        End If
        '    Next
        'End With
        mShow = True
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        'Dim SqlStr As String = ""
        Dim xIndentNo As Double
        Dim xCompanyName As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColIndentNo
        xIndentNo = Val(SprdMain.Text)

        SprdMain.Col = ColCompanyName
        xCompanyName = Trim(SprdMain.Text)

        If Trim(RsCompany.Fields("Company_ShortName").Value) <> Trim(xCompanyName) Then
            MsgInformation("Cann't be see Other Unit Indent.")
        End If

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "mnuIndentApp", PubDBCn)
        If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
            Exit Sub
        End If

        FrmIndentEntry.MdiParent = Me.MdiParent

        FrmIndentEntry.Show()
        FrmIndentEntry.lblBookType.Text = "IA"

        FrmIndentEntry.FrmIndentEntry_Activated(Nothing, New System.EventArgs())

        FrmIndentEntry.txtIndentNo.Text = xIndentNo
        FrmIndentEntry.txtIndentNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call SaveStatus(False)
    End Sub
    Private Sub cboCompany_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call SaveStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtdateTo.TextChanged
        Call SaveStatus(False)
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
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = Arow

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColIndentNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColIndentNo, 9)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColQuotation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColQuotation, 9)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = colIndentDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colIndentDate, 9)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = colDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colDeptCode, 6)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 30)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSupplier, 20)

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

            .Col = ColDiscount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDiscount, 9)

            .Col = ColDeliveryTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeliveryTime, 12)


            .Col = ColCreditbility
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCreditbility, 12)



            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 12)

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 12)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter ''SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColRemarks)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCompanyName, ColCompanyName)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMKEY, ColMKEY)



            '        SprdMain.OperationMode = OperationModeSingle
            '        SprdMain.DAutoCellTypes = True
            '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '        SprdMain.GridColor = &HC00000
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
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        Dim CntLst As Long
        ''SELECT CLAUSE...

        MakeSQL = " SELECT '', " & vbCrLf _
            & " ID.AUTO_KEY_INDENT," & vbCrLf _
            & " TO_CHAR(IIH.INDENT_DATE,'DD/MM/YYYY'),IIH.DEPT_CODE As DEPT_CODE," & vbCrLf _
            & " IID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " IID.ITEM_UOM, IH.AUTO_KEY_QUOT, SUPP_CUST_NAME, TO_CHAR(ID.ITEM_PRICE), " & vbCrLf _
            & " ID.DISCOUNT, DELIVERY_TIME, ID.CREDIBILITY, ID.REMARKS, COMPANY_SHORTNAME," & vbCrLf _
            & " '0', IH.AUTO_KEY_QUOT "


        'Private Const ColLocked As Short = 1
        'Private Const ColIndentNo As Short = 2
        'Private Const colIndentDate As Short = 3
        'Private Const colDeptCode As Short = 4
        'Private Const ColItemCode As Short = 5
        'Private Const ColItemDesc As Short = 6
        'Private Const ColUnit As Short = 7
        'Private Const ColQuotation As Short = 8
        'Private Const ColSupplier As Short = 9
        'Private Const ColRate As Short = 10
        'Private Const ColDiscount As Short = 11
        'Private Const ColDeliveryTime As Short = 12
        'Private Const ColCreditbility As Short = 13
        'Private Const ColRemarks As Short = 14
        'Private Const ColCompanyName As Short = 15
        'Private Const ColStatus As Short = 16
        'Private Const ColMKEY As Short = 17


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM PUR_QUOTATION_HDR IH, PUR_QUOTATION_DET ID, PUR_INDENT_HDR IIH, PUR_INDENT_DET IID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_QUOT=ID.AUTO_KEY_QUOT"

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IIH.AUTO_KEY_INDENT=IID.AUTO_KEY_INDENT" & vbCrLf _
            & " AND IH.COMPANY_CODE=IID.COMPANY_CODE" & vbCrLf _
            & " AND ID.AUTO_KEY_INDENT=IID.AUTO_KEY_INDENT" & vbCrLf _
            & " AND ID.SERIAL_NO=IID.SERIAL_NO" & vbCrLf _
            & " AND IID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IID.ITEM_CODE=INVMST.ITEM_CODE"

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQL = MakeSQL & vbCrLf & " And IH.COMPANY_CODE=GMST.COMPANY_CODE"

        ' And IID.INDENT_STATUS='Y'

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If


        MakeSQL = MakeSQL & vbCrLf & "AND IH.QUOTATION_STATUS='Y' AND NVL(IID.QUOTATION_APP,'N')='N'"

        MakeSQL = MakeSQL & vbCrLf & " AND IIH.INDENT_DATE<=TO_DATE('" & VB6.Format(txtdateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        ''ORDER CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.AUTO_KEY_INDENT, IIH.INDENT_DATE,ID.SERIAL_NO, ID.AUTO_KEY_QUOT"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mIndentNo As Double
        Dim mPrevIndentNo As Double
        Dim mPrevRJIndentNo As Double
        Dim mUpdateCount As Integer
        Dim mUpdateRJCount As Integer
        Dim mEmpCode As String
        Dim mItemCode As String
        Dim mQuotation As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        mPrevIndentNo = 0
        mPrevRJIndentNo = 0
        Dim mCompanyName As String
        Dim mCompanyCode As String = 0
        Dim mAPPRemarks As String

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColIndentNo
                mIndentNo = Val(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColQuotation
                mQuotation = Val(.Text)

                .Col = ColCompanyName
                mCompanyName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                    mCompanyCode = MasterNo
                End If

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mEmpCode = IIf(Trim(PubUserEMPCode) = "", "SUPER", PubUserEMPCode)

                    SqlStr = "UPDATE PUR_QUOTATION_DET SET QUOTATION_APP='Y'" & vbCrLf _
                         & " WHERE AUTO_KEY_INDENT=" & mIndentNo & " AND AUTO_KEY_QUOT=" & mQuotation & " "


                    SqlStr = SqlStr & vbCrLf & " AND SERIAL_NO IN (SELECT SERIAL_NO FROM PUR_INDENT_DET WHERE AUTO_KEY_INDENT=" & mIndentNo & " AND ITEM_CODE= '" & MainClass.AllowSingleQuote(mItemCode) & "')"
                    'SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE='" & MainClass.AllowSingleQuote(mCompanyCode) & "'"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE PUR_INDENT_DET SET QUOTATION_APP='Y'" & vbCrLf _
                        & " WHERE AUTO_KEY_INDENT=" & mIndentNo & ""

                    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE='" & MainClass.AllowSingleQuote(mCompanyCode) & "'"


                    PubDBCn.Execute(SqlStr)

                    If mPrevIndentNo <> mIndentNo Then
                        mUpdateCount = mUpdateCount + 1
                    End If
                    mPrevIndentNo = mIndentNo
                End If


                'If mAPPRemarks <> "" Then
                '    SqlStr = "UPDATE PUR_INDENT_DET SET " & vbCrLf _
                '        & " APPROVAL_REMARKS='" & MainClass.AllowSingleQuote(mAPPRemarks) & "'" & vbCrLf _
                '        & " WHERE AUTO_KEY_INDENT=" & mIndentNo & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                '    PubDBCn.Execute(SqlStr)

                'End If


            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Quotation Approved.", MsgBoxStyle.Information)
        cmdShow.Enabled = True
        Call cmdShow_Click(cmdShow, New System.EventArgs())
        Call SaveStatus(False)
        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtdateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtdateTo.Text))) = False Then txtdateTo.Focus()
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

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtdateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtdateTo) = False Then
            txtdateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtdateTo.Text))) = False Then
            txtdateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillIndentCombo()
        On Error GoTo FillErr2
        Dim mRights As String = ""
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim pCompanyCode As Long
        Dim CntLst As Long

        cboSendBack.Items.Clear()
        cboSendBack.Items.Add("ALL")
        cboSendBack.Items.Add("Yes")
        cboSendBack.Items.Add("No")
        cboSendBack.SelectedIndex = 2

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_CODE, COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                pCompanyCode = RS.Fields("COMPANY_CODE").Value
                mRights = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn, pCompanyCode)
                If mRights <> "" Then
                    lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                    lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                    CntLst = CntLst + 1 'IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False)
                End If
                RS.MoveNext()
            Loop
        End If

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As EventArgs)
        cmdShow.Enabled = True
    End Sub
End Class
