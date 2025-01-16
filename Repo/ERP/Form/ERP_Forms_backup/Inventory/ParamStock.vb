Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamStock
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Dim GroupOnItem As Boolean


    Private Const RowHeight As Short = 12

    Private Const ColGrouping As Short = 1
    Private Const ColCode As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColPartNo As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColRefNo As Short = 6
    Private Const ColVDate As Short = 7
    Private Const ColDesc As Short = 8
    Private Const ColStockType As Short = 9
    Private Const ColOpening As Short = 10
    Private Const ColReceipt As Short = 11
    Private Const ColIssue As Short = 12
    Private Const ColClosing As Short = 13
    Private Const ColRunBalance As Short = 14
    Private Const ColPrice As Short = 15
    Private Const ColValue As Short = 16
    Private Const ColAgeDays As Short = 17
    Private Const ColRefDate As Short = 18
    Private Const ColRefType As Short = 19
    Private Const ColRefFlag As Short = 20
    Private Const ColDespatchQty As Short = 21
    Private Const ColProductionQtyIn As Short = 22
    Private Const ColProductionQtyOut As Short = 23


    Private Const ColItemName1 As Short = 1
    Private Const ColDivision1 As Short = 2
    Private Const ColPartyCode1 As Short = 3
    Private Const ColCategory1 As Short = 4
    Private Const ColSubCategory1 As Short = 5
    Private Const ColLotNo1 As Short = 6
    Private Const ColModel1 As Short = 7
    Private Const ColMake1 As Short = 8
    Private Const ColColor1 As Short = 9
    Private Const ColDept1 As Short = 10
    Private Const ColTarrif1 As Short = 11
    Private Const ColCapital1 As Short = 12
    Private Const ColItemType1 As Short = 13
    Private Const ColRefType1 As Short = 14

    Dim mClickProcess As Boolean = False
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mFYear As Long
    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboRef_Change()
        PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemType.SelectedIndexChanged, cboCapital.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cboRef_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRef.SelectedIndexChanged
        Dim I As Long
        'Dim mClickProcess As Boolean

        '    mClickProcess = False

        '    If mClickProcess = False Then
        '        If cboRef.Selected(0) = True Then
        ''            mClickProcess = True
        '            For I = 1 To cboRef.ListCount - 1
        '                cboRef.Selected(I) = True
        '            Next
        '        Else
        ''            mClickProcess = True
        '            For I = 1 To cboRef.ListCount - 1
        '                cboRef.Selected(I) = False
        '            Next
        '        End If
        '    End If
    End Sub

    Private Sub cboRef_ItemCheck(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ItemCheckEventArgs) Handles cboRef.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If eventArgs.Index = 0 Then
                If eventArgs.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To cboRef.Items.Count - 1
                        cboRef.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To cboRef.Items.Count - 1
                        cboRef.SetItemChecked(I, False)
                    Next
                End If
            Else
                If eventArgs.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    cboRef.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub chkIncludOp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkIncludOp.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        FraOption.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        PrintStatus(False)
    End Sub

    Private Sub chkRate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRate.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ERR1
        Dim I As Integer
        Dim mField As String

        With SprdMain
            .Row = 0

            .Col = 0
            .Text = "S.No."

            .Col = ColCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Short Name"

            .Col = ColPartNo
            .Text = "Part No"

            .Col = ColUnit
            .Text = "Unit"

            .Col = ColGrouping
            For I = 1 To SprdOption.MaxCols
                SprdOption.Row = 1
                SprdOption.Col = I
                If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Text = FillFieldName(I)
                    Exit For
                End If
            Next

            .Col = ColStockType
            .Text = "TYPE"

            .Col = ColOpening
            .Text = "Opening"

            .Col = ColReceipt
            .Text = "Receipt"

            .Col = ColIssue
            .Text = "Issue"

            .Col = ColClosing
            .Text = "Closing"

            .Col = ColRunBalance
            .Text = "Running Balance"

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColDesc
            .Text = "Description"

            .Col = ColVDate
            .Text = "Ref Date"

            .Col = ColPrice
            If optVal(0).Checked = True Then
                .Text = "Cost Price"
            ElseIf optVal(1).Checked = True Then
                .Text = "Purchase Price"
            ElseIf optVal(2).Checked = True Then
                .Text = "Sale Price"
            ElseIf optVal(3).Checked = True Then
                .Text = "Current Price"
            End If

            .Col = ColValue
            .Text = "Item Value"

            .Col = ColAgeDays
            .Text = "Stock Age"

            .Col = ColRefDate
            .Text = "Ref Date"

            .Col = ColRefType
            .Text = "Ref Type"

            .Col = ColRefFlag
            .Text = "Ref Flag"

            .Col = ColDespatchQty
            .Text = "Despatch Qty"

            .Col = ColProductionQtyIn
            .Text = "Production Qty In"

            .Col = ColProductionQtyOut
            .Text = "Production Qty Out"


        End With

        Call HideUnHide()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub FormatSprdOption(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdOption
            .MaxCols = ColRefType1
            .set_RowHeight(0, 1.2 * RowHeight)

            .Row = 0
            .Col = 0
            .Text = "Grouping"

            For I = 1 To .MaxCols
                .Col = I

                .Row = 1
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                .set_RowHeight(1, 10) 'RowHeight

                .Row = 2
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .Value = CStr(System.Windows.Forms.CheckState.Checked)
                .set_RowHeight(2, 10) 'RowHeight

                .Row = 3
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .set_RowHeight(3, 12) 'RowHeight
                MainClass.ProtectCell(SprdOption, 3, 3, I, I)
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

                .set_ColWidth(I, 9)
            Next

            MainClass.SetSpreadColor(SprdOption, -1)
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mColWidth As Integer

        With SprdMain
            .set_RowHeight(0, 1.5 * RowHeight)
            .Row = Arow
            .set_ColWidth(0, 5)

            For I = ColGrouping To ColUnit
                .Col = I

                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .set_RowHeight(Arow, RowHeight)
                Select Case I
                    Case ColGrouping
                        mColWidth = IIf(optType(2).Checked = True, 25, 10)
                    Case ColCode
                        mColWidth = 8
                    Case ColItemName
                        mColWidth = 18
                    Case ColPartNo
                        mColWidth = 10
                    Case ColUnit
                        mColWidth = 4
                End Select
                .set_ColWidth(I, mColWidth)
                .ColsFrozen = ColGrouping
                .ColHidden = False
            Next

            .set_RowHeight(Arow, RowHeight)

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColRefNo, 12)
            .ColHidden = True

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColVDate, 10)
            .ColHidden = True

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY

            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColRefDate, 10)
            .ColHidden = True

            .Col = ColRefType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColRefType, 6)
            .ColHidden = IIf(optType(0).Checked = True, False, True)

            .Col = ColRefFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColRefFlag, 6)
            .ColHidden = IIf(optType(0).Checked = True, False, True)

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColDesc, IIf(PubUserID = "A00001", 45, 22))
            .ColHidden = True

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColStockType, 6)
            .ColHidden = True

            For I = ColOpening To ColClosing
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

                '             .RowHeight(Arow) = RowHeight
                .set_ColWidth(I, 10)
                .ColHidden = False
            Next

            For I = ColRunBalance To ColValue
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

                '             .RowHeight(Arow) = RowHeight
                .set_ColWidth(I, 8)
                .ColHidden = True
            Next

            .Col = ColDespatchQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColDespatchQty, 8)
            .ColHidden = True

            .Col = ColProductionQtyIn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColProductionQtyIn, 8)
            .ColHidden = True

            .Col = ColProductionQtyOut
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColProductionQtyOut, 8)
            .ColHidden = True


            .Col = ColAgeDays
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '        .RowHeight(Arow) = RowHeight
            .set_ColWidth(ColAgeDays, 8)
            .ColHidden = True

            If PubUserID = "A00001" Then
                .Col = ColOpening
                .ColHidden = True

                .Col = ColReceipt
                .ColHidden = True

                .Col = ColIssue
                .ColHidden = True

                .Col = ColRunBalance
                .ColHidden = True

                .Col = ColPrice
                .ColHidden = True

                .Col = ColValue
                .ColHidden = True

                .Col = ColAgeDays
                .ColHidden = True

                .Col = ColRefDate
                .ColHidden = True

                .Col = ColRefType
                .ColHidden = True

                .Col = ColRefFlag
                .ColHidden = True

                .Col = ColDespatchQty
                .ColHidden = True

                .Col = ColProductionQtyIn
                .ColHidden = True

                .Col = ColProductionQtyOut
                .ColHidden = True

            End If


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
            '        SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForStock(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForStock(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mRPTName As String = ""
        Dim mWiseField As String = ""
        Dim mCondTitle As String
        Dim mCondValue As String
        Dim mSubTitle1 As String = ""

        Dim I As Integer



        If InsertIntoPrintTable() = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Select Case lblLabelType.Text
            Case "StockStmt"
                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    mRPTName = IIf(PubUserID = "A00001", "StockStmtSummDummy.rpt", "StockStmtSumm.rpt")
                    mTitle = "Stock Statement"
                Else
                    mRPTName = IIf(PubUserID = "A00001", "StockStmtDummy.rpt", "StockStmt.rpt")
                    mTitle = "Stock Statement (Summarised)"
                End If
            Case "StockReg"
                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    mRPTName = "StockLdgr.rpt"
                    mTitle = "Stock Register"
                Else
                    mRPTName = "StockStmtSumm.rpt"
                    mTitle = "Stock Register (Summarised)"
                End If
            Case "StockVal"
                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    mRPTName = "StockVal.rpt"
                    mTitle = "Stock Valuation"
                Else
                    mRPTName = "StockValSumm.rpt"
                    mTitle = "Stock Valuation (Summarised)"
                End If
            Case "StockMax"
                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    mRPTName = "StockStmt.rpt"
                    mTitle = "Maximum Inventory Level"
                Else
                    mRPTName = "StockStmtSumm.rpt"
                    mTitle = "Maximum Inventory Level Summarised"
                End If
            Case "StockMin"
                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    mRPTName = "StockStmt.rpt"
                    mTitle = "Minimum Inventory Level"
                Else
                    mRPTName = "StockStmtSumm.rpt"
                    mTitle = "Minimum Inventory Level Summarised"
                End If
            Case "StockReOrder"
                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    mRPTName = "StockStmt.rpt"
                    mTitle = "Reorder Inventory Level"
                Else
                    mRPTName = "StockStmtSumm.rpt"
                    mTitle = "Reorder Inventory Level Summarised"
                End If
            Case "StockAge"
                mRPTName = "StockAge.rpt"
                mTitle = "Stock Ageing Report"
        End Select

        If lblLabelType.Text = "StockVal" Then
            If optVal(0).Checked = True Then
                mTitle = mTitle & " (Cost Price)"
            ElseIf optVal(1).Checked = True Then
                mTitle = mTitle & " (Sale Price)"
            ElseIf optVal(2).Checked = True Then
                mTitle = mTitle & " (Transfer Price)"
            End If

            mSubTitle = "FROM : " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY")
            mSubTitle = mSubTitle & " TO : " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        Else

            mSubTitle = "FROM : " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY")
            mSubTitle = mSubTitle & " TO : " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

            For I = 1 To SprdOption.MaxCols
                SprdOption.Row = 1
                SprdOption.Col = I
                If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mSubTitle = mSubTitle & " (Group By " & FillFieldName(I) & ")"
                    mWiseField = FillFieldName(I)
                End If
                SprdOption.Row = 2
                If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    mCondTitle = " " & FillFieldName(I) & " : "
                    SprdOption.Row = 3
                    mCondValue = SprdOption.Text
                    mSubTitle1 = IIf(mSubTitle1 = "", mSubTitle1, mSubTitle1 & " AND ") & mCondTitle & mCondValue
                    mCondValue = ""
                    mCondTitle = ""
                End If
            Next
            mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (") & mSubTitle1 & IIf(mSubTitle1 = "", "", ")")
        End If

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle, mWiseField)
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
    Private Function InsertIntoPrintTable() As Boolean

        On Error GoTo PrintDummyErr

        Dim SqlStr As String = ""
        Dim RowNum As Integer
        Dim mGrouping As String
        Dim mCode As String
        Dim mItemName As String
        Dim mUnit As String
        Dim mRefNo As String
        Dim mVDate As String = ""
        Dim mDesc As String
        Dim mOpening As String
        Dim mReceipt As String
        Dim mIssue As String
        Dim mClosing As String
        Dim mRunBal As String
        Dim mPrice As String
        Dim mValue As String
        Dim mStockType As String = ""
        Dim mPartNo As String = ""

        ''Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)



        For RowNum = 1 To SprdMain.MaxRows - 1
            With SprdMain
                .Row = RowNum

                .Col = ColGrouping
                If GroupOnItem = True And (optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True) Then 'And lblLabelType.text <> "Stock Age" Then
                    mGrouping = ""
                Else
                    mGrouping = MainClass.AllowSingleQuote(Trim(.Text))
                End If

                .Col = ColCode
                mCode = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColItemName
                mItemName = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColRefNo
                mRefNo = .Text

                .Col = ColVDate
                mVDate = .Text

                .Col = ColDesc
                mDesc = .Text

                .Col = ColOpening
                mOpening = CStr(Val(.Text))

                .Col = ColReceipt
                mReceipt = CStr(Val(.Text))

                .Col = ColIssue
                mIssue = CStr(Val(.Text))

                .Col = ColClosing
                mClosing = CStr(Val(.Text))

                .Col = ColPrice
                mPrice = CStr(Val(.Text))

                .Col = ColValue
                mValue = CStr(Val(.Text))

                .Col = ColStockType
                mStockType = .Text

                .Col = ColPartNo
                mPartNo = .Text

            End With
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                & " Field1, Field2, Field3, Field4, " & vbCrLf _
                & " Field5, Field6, Field7, Field8, " & vbCrLf _
                & " Field9, Field10, Field11, Field12, " & vbCrLf _
                & " Field13,Field14,Field15) " & vbCrLf _
                & " VALUES ( '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'," & RowNum & ", " & vbCrLf _
                & " '" & mGrouping & "', '" & mCode & "', '" & mItemName & "'," & vbCrLf _
                & " '" & mUnit & "',  '" & mRefNo & "'," & vbCrLf _
                & " '" & mVDate & "', '" & mDesc & "', '" & mOpening & "'," & vbCrLf _
                & " '" & mReceipt & "', '" & mIssue & "', '" & mClosing & "'," & vbCrLf _
                & " '" & mPrice & "', '" & mValue & "', '" & mStockType & "','" & mPartNo & "') "

            PubDBCn.Execute(SqlStr)
NextRec:
        Next
        PubDBCn.CommitTrans()


        'If PvtDBCn.State = adStateOpen Then
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        '    End If

        InsertIntoPrintTable = True
        Exit Function
PrintDummyErr:
        InsertIntoPrintTable = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String


        mSqlStr = mSqlStr & "SELECT * " & " FROM temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SubRow,Field1,Field3"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mWiseField As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "WiseField='" & mWiseField & "'")
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForStock(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        PrintStatus(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        mFYear = Val(lblYear.Text)  ''Val(VB6.Format(lblYear.Text, "YYYY"))

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If optType(2).Checked = True Then
            If InsertTempStock() = False Then GoTo ERR1
            ShowSummaryGroup()
        Else
            Show1()
        End If

        '    SprdMain.Refresh
        FormatSprdMain(-1)
        SprdOption.Col = ColGrouping
        SprdOption.Row = 1
        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then SprdMain.ColHidden = True Else SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = False
        End If

        FillSprdMain()
        GroupByColor()

        CalcSprdTotal()

        PrintStatus(True)
        '    SprdMain.SetFocus
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer


        Dim mOpening As Double = 0
        Dim mReceipt As Double = 0
        Dim mIssue As Double = 0
        Dim mClosing As Double = 0
        Dim mValue As Double = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                '            .Col = 4
                '            mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColOpening
                mOpening = mOpening + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColReceipt
                mReceipt = mReceipt + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColIssue
                mIssue = mIssue + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColClosing
                If optType(0).Checked = True Then
                    mClosing = CDbl(IIf(IsNumeric(.Text), .Text, 0))
                Else
                    mClosing = mClosing + CDbl(IIf(IsNumeric(.Text), .Text, 0))
                End If


                .Col = ColValue
                mValue = mValue + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColItemName)
            .Col = ColItemName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .Font = VB6.FontChangeBold(.Font, True)
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColOpening
            .Text = VB6.Format(mOpening, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColReceipt
            .Text = VB6.Format(mReceipt, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColIssue
            .Text = VB6.Format(mIssue, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColClosing
            .Text = VB6.Format(mOpening + mReceipt - mIssue, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColValue
            .Text = VB6.Format(mValue, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim I As Integer
        Dim mFromFY As Integer
        Dim mToFY As Integer

        FieldsVarification = True
        If Not IsDate(txtDateFrom.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateFrom.Focus()
            Exit Function
            'ElseIf FYChk((txtDateFrom.Text)) = False Then
            '    FieldsVarification = False
            '    txtDateFrom.Focus()
            '    Exit Function
        End If

        If Not IsDate(txtDateTo.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateTo.Focus()
            Exit Function
            'ElseIf FYChk((txtDateTo.Text)) = False Then
            '    FieldsVarification = False
            '    txtDateTo.Focus()
            '    Exit Function
        End If

        mFromFY = GetCurrentFYNo(PubDBCn, txtDateFrom.Text)
        mToFY = GetCurrentFYNo(PubDBCn, txtDateTo.Text)

        If mFromFY <> mToFY Then
            FieldsVarification = False
            MsgInformation("From & To Date is not Same FY.")
            txtDateFrom.Focus()
            Exit Function
        End If

        If mFromFY <> Val(lblYear.Text) Then
            FieldsVarification = False
            MsgInformation("From Date is not Match with selected FY.")
            txtDateFrom.Focus()
            Exit Function
        End If

        If mToFY <> Val(lblYear.Text) Then
            FieldsVarification = False
            MsgInformation("From to is not Match with selected FY.")
            txtDateTo.Focus()
            Exit Function
        End If

        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 2
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                SprdOption.Row = 3
                SprdOption.Col = I
                If Trim(SprdOption.Text) = "" Then
                    FieldsVarification = False
                    MsgInformation("Blank Field.")
                    MainClass.SetFocusToCell(SprdOption, 3, I)
                    Exit Function
                End If
            End If
        Next

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGroupBy As String = ""
        Dim mSqlStr As String
        Dim mOptionalTable As String = ""
        Dim mOptionalJoining As String = ""
        Dim mStartDate As String
        Dim mDateStr As String
        Dim mQCDateStr As String
        Dim mItemRate As String
        Dim mTRNTableName As String
        Dim mQCDateStr1 As String = ""
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        Dim CntLst As Long


        mTRNTableName = ConInventoryTable

        mStartDate = txtDateFrom.Text

        If optType(4).Checked = True Then
            mQCDateStr1 = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN STOCK.E_DATE ELSE STOCK.REF_DATE END "
            mQCDateStr = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN TO_CHAR(STOCK.E_DATE,'MON-YYYY') ELSE TO_CHAR(STOCK.REF_DATE,'MON-YYYY') END "
            mDateStr = "UPPER(TO_CHAR('" & VB6.Format(mStartDate, "MMM-YYYY") & "')) ELSE " & mQCDateStr & ""
        Else
            mQCDateStr = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN STOCK.E_DATE ELSE STOCK.REF_DATE END "
            mDateStr = "TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE " & mQCDateStr & ""
        End If


        SqlStr = " SELECT "

        SqlStr = SqlStr & vbCrLf & "  --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) "

        ''Collect the Group Field...
        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 1
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                mGroupBy = GetGroupBy(I)
                If mGroupBy <> "" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                    Exit For
                End If
            End If
        Next

        SqlStr = SqlStr & vbCrLf _
            & " STOCK.ITEM_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM AS UNIT, "

        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then ''Detail Option....

            Select Case lblLabelType.Text
                Case "StockStmt"
                    SqlStr = SqlStr & vbCrLf & "'','','',"
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
                Case "StockReg", "StockVal"
                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & "' ',"
                    End If

                    If optType(0).Checked = True Or optType(3).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    End If

                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'Opening ' ELSE STOCK.REMARKS END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & "' ',"
                    End If
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
            End Select
        ElseIf optType(1).Checked = True Then  ''Summary Option....
            SqlStr = SqlStr & vbCrLf & " '','','',"
            If lblLabelType.Text = "StockStmt" Then
                SqlStr = SqlStr & vbCrLf & "'',"
            Else
                SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
            End If
        End If

        If chkIncludOp.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) END)) AS Opening, "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR('0.00') AS Opening, "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) END)) AS Receipt, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) END)) AS Issue, " & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as Closing, "

        If lblLabelType.Text = "StockVal" Then
            SqlStr = SqlStr & vbCrLf & "'','','','',"
            If optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "STOCK.REF_DATE,"
            Else
                SqlStr = SqlStr & vbCrLf & "'',"
            End If
        ElseIf lblLabelType.Text = "StockAge" Then
            SqlStr = SqlStr & vbCrLf & "'','',TO_CHAR(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')-MIN(VDate)),'',"
        Else
            SqlStr = SqlStr & vbCrLf & "'','','','','',"
        End If

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE STOCK.REF_TYPE END,"
            SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE STOCK.REF_FLAG END,"
        Else
            SqlStr = SqlStr & vbCrLf & "'','',"
        End If

        SqlStr = SqlStr & vbCrLf _
            & "0, " & vbCrLf _
            & " TO_CHAR(SUM(DECODE(REF_TYPE,'PMD',ITEM_QTY * DECODE(ITEM_IO,'I',1,0),0))) as PMDIN, " & vbCrLf _
            & " TO_CHAR(SUM(DECODE(REF_TYPE,'PMD',ITEM_QTY * DECODE(ITEM_IO,'O',1,0),0))) as PMDOUT"

        Call GetOptionTable(mOptionalTable, mOptionalJoining)

        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTRNTableName & " STOCK, " & vbCrLf _
            & " INV_ITEM_MST ITEM, INV_GENERAL_MST CAT, INV_DIVISION_MST DIV "

        SqlStr = SqlStr & IIf(mOptionalTable = "", "", vbCrLf & mOptionalTable)


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf _
            & " STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf _
            & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=CAT.COMPANY_CODE " & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=CAT.GEN_CODE AND GEN_TYPE='C'"

        SqlStr = SqlStr & vbCrLf _
            & " AND STOCK.COMPANY_CODE=DIV.COMPANY_CODE " & vbCrLf _
            & " AND STOCK.DIV_CODE=DIV.DIV_CODE "


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
            SqlStr = SqlStr & vbCrLf & " AND STOCK.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If



        SqlStr = SqlStr & vbCrLf _
            & " AND STOCK.FYEAR=" & mFYear & ""

        ''            & " AND STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _


        If CboItemType.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'"
        ElseIf CboItemType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConPH & "'"
        ElseIf CboItemType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConJW & "'"
        ElseIf CboItemType.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConSH & "'"
        End If

        If cboCapital.SelectedIndex = 0 Then
            'SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'"
        ElseIf cboCapital.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.IS_CAPITAL='Y'"
        ElseIf cboCapital.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.IS_CAPITAL='N'"
        End If

        If CboSType.SelectedIndex > 0 Then
            If CboSType.Text = "QC" Then
                SqlStr = SqlStr & vbCrLf & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            ElseIf CboSType.Text = "ST" Or CboSType.Text = "RJ" Then
                If chkQCStockType.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    SqlStr = SqlStr & vbCrLf & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND ((STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR STOCK.STOCK_TYPE='QC'))"
                End If
            Else
                SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
            End If
        End If



        SqlStr = SqlStr & IIf(GetAttributeCode() = "", "", vbCrLf & GetAttributeCode())

        If cboShow.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.ITEM_IO='" & VB.Left(cboShow.Text, 1) & "'"
        End If

        ''12/09/2011
        '    If cboRef.ListIndex > 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK.REF_TYPE='" & vb.Left(cboRef.Text, 3) & "'"
        '    End If

        Dim mRefTypeStr As String
        Dim mRefType As String

        mRefTypeStr = ""
        For CntLst = 0 To cboRef.Items.Count - 1
            If CntLst = 0 And cboRef.GetItemChecked(CntLst) = True Then
                mRefTypeStr = ""
                Exit For
            Else
                If cboRef.GetItemChecked(CntLst) = True Then
                    mRefType = "'" & VB.Left(VB6.GetItemString(cboRef, CntLst), 3) & "'"
                    mRefTypeStr = IIf(mRefTypeStr = "", mRefType, mRefTypeStr & "," & mRefType)
                End If
            End If
        Next

        If mRefTypeStr <> "" Then
            mRefTypeStr = "(" & mRefTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND STOCK.REF_TYPE IN " & mRefTypeStr & ""
        End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & " AND ( STOCK.DEPT_CODE_TO='" & Trim(MasterNo) & "' OR STOCK.DEPT_CODE_FROM='" & Trim(MasterNo) & "')"
            End If
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            SqlStr = SqlStr & vbCrLf & " AND CAT.GEN_DIV_CODE=" & Val(MasterNo) & ""
                SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & Val(MasterNo) & ""
            End If
        End If

        SqlStr = SqlStr & IIf(mOptionalJoining = "", "", vbCrLf & mOptionalJoining)

        If cboIsShowItem.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='" & VB.Left(cboIsShowItem.Text, 1) & "'"
        ElseIf cboIsShowItem.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='" & VB.Left(cboIsShowItem.Text, 1) & "'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I'"

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STATUS='O'"
        End If

        If chkIncludOp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If IsDate(txtDateFrom.Text) Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        'GROUP BY ..................

        SqlStr = SqlStr & vbCrLf & "GROUP BY "

        If mGroupBy <> "" Then
            SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ", "
        End If

        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then 'Detail Option...
            If mGroupBy <> "STOCK.ITEM_CODE" Then
                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE,"
            End If

            If optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE STOCK.REF_TYPE END,"
                SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '' ELSE STOCK.REF_FLAG END,"
            End If

            SqlStr = SqlStr & vbCrLf & " STOCK.COMPANY_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM"
            If lblLabelType.Text = "StockVal" And optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " ,STOCK.REF_DATE"
            End If
            Select Case lblLabelType.Text
                Case "StockStmt"
                    SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
                Case "StockReg", "StockVal"
                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & " ,' ',"
                    End If

                    If optType(0).Checked = True Or optType(3).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    Else
                        'SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_CHAR('" & VB6.Format(txtDateFrom.Text, "MMM-YYYY") & "') THEN " & mDateStr & " END, "
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    End If

                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'Opening ' ELSE STOCK.REMARKS END, "
                    End If
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "

            End Select
        ElseIf optType(1).Checked = True Then  'Summary Option...
            If mGroupBy <> "STOCK.ITEM_CODE" Then
                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE, "
            End If
            SqlStr = SqlStr & vbCrLf & " STOCK.COMPANY_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM"
            If lblLabelType.Text = "StockStmt" Then

            Else
                SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
            End If
        End If

        ''
        If optType(2).Checked = False Then
            SqlStr = SqlStr & vbCrLf & ", ITEM.PURCHASE_UOM,ITEM.UOM_FACTOR"
        End If

        If lblLabelType.Text = "StockMax" Then

        ElseIf chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING AVG(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        End If

        'ORDER BY ..................


        SqlStr = SqlStr & vbCrLf & "Order By "

        If mGroupBy <> "" Then
            If mGroupBy <> "STOCK.ITEM_CODE" Then
                If mGroupBy = "STOCK.BATCH_NO" Then
                    SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE, " & mGroupBy & ","
                Else
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                End If
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE,"

        If lblLabelType.Text = "StockReg" Or lblLabelType.Text = "StockVal" Then
            If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                If optType(0).Checked = True Or optType(3).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                Else
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                End If

                SqlStr = SqlStr & vbCrLf & " MIN(ITEM_IO)"
                If optType(0).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END "
                End If
            ElseIf optType(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE"
            End If
        End If

        '13-05-2011

        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then 'Detail Option...
            Select Case lblLabelType.Text
                Case "StockStmt"
                    SqlStr = SqlStr & vbCrLf & "CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
                Case "StockReg", "StockVal"
                    '                If optType(0).Value = True Then
                    '                    SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END, "
                    '                End If

                    '                If optType(0).Value = True Or optType(3).Value = True Then
                    '                    SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    '                Else
                    '                    'SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_CHAR('" & VB6.Format(txtDateFrom.Text, "MMM-YYYY") & "') THEN " & mDateStr & " END, "
                    '                    SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    '                End If

                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'Opening ' ELSE STOCK.REMARKS END "
                    End If
                    SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "

            End Select
        ElseIf optType(1).Checked = True Then  'Summary Option...
            If lblLabelType.Text = "StockStmt" Then
                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE"
            Else
                SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
            End If
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1Old() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGroupBy As String = ""
        Dim mSqlStr As String
        Dim mOptionalTable As String = ""
        Dim mOptionalJoining As String = ""
        Dim mStartDate As String
        Dim mDateStr As String
        Dim mQCDateStr As String
        Dim mItemRate As String
        Dim mTRNTableName As String
        Dim mQCDateStr1 As String = ""

        mTRNTableName = ConInventoryTable

        mStartDate = txtDateFrom.Text

        If optType(4).Checked = True Then
            mQCDateStr1 = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN STOCK.E_DATE ELSE STOCK.REF_DATE END "
            mQCDateStr = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN TO_CHAR(STOCK.E_DATE,'MON-YYYY') ELSE TO_CHAR(STOCK.REF_DATE,'MON-YYYY') END "
            mDateStr = "UPPER(TO_CHAR('" & VB6.Format(mStartDate, "MMM-YYYY") & "')) ELSE " & mQCDateStr & ""
        Else
            mQCDateStr = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN STOCK.E_DATE ELSE STOCK.REF_DATE END "
            mDateStr = "TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE " & mQCDateStr & ""
        End If


        SqlStr = " SELECT "

        ''Collect the Group Field...
        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 1
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                mGroupBy = GetGroupBy(I)
                If mGroupBy <> "" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                    Exit For
                End If
            End If
        Next

        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then ''Detail Option....
            SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE,"

            SqlStr = SqlStr & vbCrLf & " TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM AS UNIT, "

            Select Case lblLabelType.Text
                Case "StockStmt"
                    SqlStr = SqlStr & vbCrLf & "'','','',"
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
                Case "StockMin", "StockMax", "StockReOrder", "StockAge"
                    SqlStr = SqlStr & vbCrLf & "'','','','',"
                Case "StockReg", "StockVal"
                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & "' ',"
                    End If

                    If optType(0).Checked = True Or optType(3).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    Else
                        'SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_CHAR('" & VB6.Format(txtDateFrom.Text, "MON-YYYY") & "') THEN " & mDateStr & " END, "
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    End If

                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'Opening ' ELSE STOCK.REMARKS END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & "' ',"
                    End If
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
            End Select
        ElseIf optType(1).Checked = True Then  ''Summary Option....
            SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM AS UNIT,'','',''," '',"
            If lblLabelType.Text = "StockStmt" Then
                SqlStr = SqlStr & vbCrLf & "'',"
            Else
                SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
            End If
        End If

        If chkIncludOp.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) END)) AS Opening, "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR('0.00') AS Opening, "
        End If

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) END)) AS Receipt, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) END)) AS Issue, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as Closing, "

        If lblLabelType.Text = "StockVal" Then
            SqlStr = SqlStr & vbCrLf & "'','','','',"
            If optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "STOCK.REF_DATE"
            Else
                SqlStr = SqlStr & vbCrLf & "''"
            End If
        ElseIf lblLabelType.Text = "StockAge" Then
            SqlStr = SqlStr & vbCrLf & "'','',TO_CHAR(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')-MIN(VDate)),''"
        Else
            SqlStr = SqlStr & vbCrLf & "'','','','',''"
        End If

        Call GetOptionTable(mOptionalTable, mOptionalJoining)

        SqlStr = SqlStr & vbCrLf & " FROM " & mTRNTableName & " STOCK, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CAT, INV_DIVISION_MST DIV "

        SqlStr = SqlStr & IIf(mOptionalTable = "", "", vbCrLf & mOptionalTable)


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " Where " & vbCrLf & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STOCK.FYEAR=" & mFYear & ""

        If CboItemType.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'"
        ElseIf CboItemType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConPH & "'"
        ElseIf CboItemType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConJW & "'"
        ElseIf CboItemType.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConSH & "'"
        End If


        If cboCapital.SelectedIndex = 0 Then
            'SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'"
        ElseIf cboCapital.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.IS_CAPITAL='Y'"
        ElseIf cboCapital.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.IS_CAPITAL='N'"

        End If
        If CboSType.SelectedIndex > 0 Then
            If CboSType.Text = "QC" Then
                SqlStr = SqlStr & vbCrLf & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            ElseIf CboSType.Text = "ST" Or CboSType.Text = "RJ" Then
                If chkQCStockType.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    SqlStr = SqlStr & vbCrLf & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND ((STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR STOCK.STOCK_TYPE='QC'))"
                    ''AND CASE WHEN STOCK.STOCK_TYPE='QC' THEN E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') END)
                End If
            Else
                SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CAT.COMPANY_CODE " & vbCrLf & " AND ITEM.CATEGORY_CODE=CAT.GEN_CODE AND GEN_TYPE='C'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK.COMPANY_CODE=DIV.COMPANY_CODE " & vbCrLf & " AND STOCK.DIV_CODE=DIV.DIV_CODE "

        SqlStr = SqlStr & IIf(GetAttributeCode() = "", "", vbCrLf & GetAttributeCode())

        If cboShow.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.ITEM_IO='" & VB.Left(cboShow.Text, 1) & "'"
        End If

        ''12/09/2011
        '    If cboRef.ListIndex > 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK.REF_TYPE='" & vb.Left(cboRef.Text, 3) & "'"
        '    End If

        Dim CntLst As Integer
        Dim mRefTypeStr As String
        Dim mRefType As String

        mRefTypeStr = ""
        For CntLst = 0 To cboRef.Items.Count - 1
            If CntLst = 0 And cboRef.GetItemChecked(CntLst) = True Then
                mRefTypeStr = ""
                Exit For
            Else
                If cboRef.GetItemChecked(CntLst) = True Then
                    mRefType = "'" & VB.Left(VB6.GetItemString(cboRef, CntLst), 3) & "'"
                    mRefTypeStr = IIf(mRefTypeStr = "", mRefType, mRefTypeStr & "," & mRefType)
                End If
            End If
        Next

        If mRefTypeStr <> "" Then
            mRefTypeStr = "(" & mRefTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND STOCK.REF_TYPE IN " & mRefTypeStr & ""
        End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & " AND ( STOCK.DEPT_CODE_TO='" & Trim(MasterNo) & "' OR STOCK.DEPT_CODE_FROM='" & Trim(MasterNo) & "')"
            End If
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            SqlStr = SqlStr & vbCrLf & " AND CAT.GEN_DIV_CODE=" & Val(MasterNo) & ""
                SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & Val(MasterNo) & ""
            End If
        End If

        SqlStr = SqlStr & IIf(mOptionalJoining = "", "", vbCrLf & mOptionalJoining)

        If cboIsShowItem.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='" & VB.Left(cboIsShowItem.Text, 1) & "'"
        ElseIf cboIsShowItem.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='" & VB.Left(cboIsShowItem.Text, 1) & "'"
        End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STATUS='O'"
        End If

        If chkIncludOp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If IsDate(txtDateFrom.Text) Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        'GROUP BY ..................

        SqlStr = SqlStr & vbCrLf & "GROUP BY "

        If mGroupBy <> "" Then
            SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ", "
        End If

        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then 'Detail Option...
            If mGroupBy <> "STOCK.ITEM_CODE" Then
                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE,"
            End If

            SqlStr = SqlStr & vbCrLf & " STOCK.COMPANY_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM"
            If lblLabelType.Text = "StockVal" And optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " ,STOCK.REF_DATE"
            End If
            Select Case lblLabelType.Text
                Case "StockStmt"
                    SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
                Case "StockReg", "StockVal"
                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & " ,' ',"
                    End If

                    If optType(0).Checked = True Or optType(3).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    Else
                        'SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_CHAR('" & VB6.Format(txtDateFrom.Text, "MMM-YYYY") & "') THEN " & mDateStr & " END, "
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    End If

                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'Opening ' ELSE STOCK.REMARKS END, "
                    End If
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "

            End Select
        ElseIf optType(1).Checked = True Then  'Summary Option...
            If mGroupBy <> "STOCK.ITEM_CODE" Then
                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE, "
            End If
            SqlStr = SqlStr & vbCrLf & " STOCK.COMPANY_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM"
            If lblLabelType.Text = "StockStmt" Then

            Else
                SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
            End If
        Else 'Summary Group Option...
            SqlStr = SqlStr & vbCrLf & " STOCK.COMPANY_CODE"
            If lblLabelType.Text = "StockStmt" Then

            Else
                SqlStr = SqlStr & vbCrLf & " ,STOCK.STOCK_TYPE "
                '            SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
            End If
        End If

        ''
        If optType(2).Checked = False Then
            SqlStr = SqlStr & vbCrLf & ", ITEM.PURCHASE_UOM,ITEM.UOM_FACTOR"
        End If

        If lblLabelType.Text = "StockMax" Then

        ElseIf chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING AVG(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        End If

        If lblLabelType.Text = "StockReg" Or lblLabelType.Text = "StockVal" Then
            SqlStr = SqlStr & vbCrLf & "Order By "
            If optType(2).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ""
            Else
                If mGroupBy <> "" Then
                    If mGroupBy <> "STOCK.ITEM_CODE" Then
                        SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                    End If
                End If

                If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE,"

                    If optType(0).Checked = True Or optType(3).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    Else
                        'SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_CHAR('" & VB6.Format(txtDateFrom.Text, "MMM-YYYY") & "') THEN " & mDateStr & " END, "
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    End If

                    SqlStr = SqlStr & vbCrLf & " MIN(ITEM_IO)"
                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END "
                    End If
                ElseIf optType(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE"
                End If
            End If
        ElseIf lblLabelType.Text = "StockStmt" Then
            SqlStr = SqlStr & vbCrLf & "Order By "
            If optType(2).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ""
            Else
                If mGroupBy <> "" Then
                    If mGroupBy <> "STOCK.ITEM_CODE" Then
                        SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE"
            End If
        End If

        '13-05-2011

        If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then 'Detail Option...
            Select Case lblLabelType.Text
                Case "StockStmt"
                    SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
                Case "StockReg", "StockVal"
                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " ,CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ' ' ELSE TO_CHAR(STOCK.REF_NO) END, "
                    Else
                        SqlStr = SqlStr & vbCrLf & " ,' ',"
                    End If

                    If optType(0).Checked = True Or optType(3).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    Else
                        'SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_CHAR('" & VB6.Format(txtDateFrom.Text, "MMM-YYYY") & "') THEN " & mDateStr & " END, "
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr1 & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN " & mDateStr & " END, "
                    End If

                    If optType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " CASE WHEN " & mQCDateStr & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'Opening ' ELSE STOCK.REMARKS END, "
                    End If
                    SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "

            End Select
        ElseIf optType(1).Checked = True Then  'Summary Option...
            If lblLabelType.Text = "StockStmt" Then

            Else
                SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
            End If
        Else 'Summary Group Option...
            If lblLabelType.Text = "StockStmt" Then

            Else
                SqlStr = SqlStr & vbCrLf & " ,STOCK.STOCK_TYPE "
                '            SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
            End If
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1Old = True
        Exit Function
InsertErr:
        Show1Old = False
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function ShowSummaryGroup() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""


        SqlStr = " SELECT GROUP_NAME, '', '', '', '', '', '', " & vbCrLf & " STOCK_TYPE, SUM(OPENING) As OPENING, " & vbCrLf & " SUM(RECEIPT) AS RECEIPT, " & vbCrLf & " SUM(ISSUE) AS ISSUE, " & vbCrLf & " SUM(CLOSING) AS CLOSING, 0," & vbCrLf & " 0, SUM(CLOSING * RATE) AS VALUE,'','' " & vbCrLf & " FROM TEMP_STOCKREG " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " GROUP BY GROUP_NAME, STOCK_TYPE ORDER BY GROUP_NAME, STOCK_TYPE"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        ShowSummaryGroup = True
        Exit Function
InsertErr:
        ShowSummaryGroup = False
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function InsertTempStock() As Boolean

        On Error GoTo InsertErr
        Dim mSqlStr As String
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGroupBy As String = ""
        Dim mOptionalTable As String = ""
        Dim mOptionalJoining As String = ""
        Dim mStartDate As String
        Dim mDateStr As String
        Dim mQCDateStr As String
        Dim mItemRate As String
        Dim mTRNTableName As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCostType As String
        Dim mProductionType As String
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim xClosing As Double
        Dim mVDate As String = ""
        Dim mStockType As String = ""
        Dim mDeptCode As String
        Dim mItemValue As Double
        Dim pStockID As String = ""
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        Dim CntLst As Long

        If CboItemType.SelectedIndex = 0 Then
            pStockID = ConWH
        ElseIf CboItemType.SelectedIndex = 1 Then
            pStockID = ConPH
        ElseIf CboItemType.SelectedIndex = 2 Then
            pStockID = ConJW
        ElseIf CboItemType.SelectedIndex = 3 Then
            pStockID = ConSH
        End If

        mTRNTableName = ConInventoryTable


        mStartDate = txtDateFrom.Text

        mQCDateStr = " CASE WHEN REF_TYPE='MRR' AND (STOCK.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) THEN STOCK.E_DATE ELSE STOCK.REF_DATE END "
        mDateStr = "TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ELSE " & mQCDateStr & ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSqlStr = " DELETE FROM TEMP_STOCKREG WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(mSqlStr)

        mSqlStr = " INSERT INTO TEMP_STOCKREG ( " & vbCrLf _
            & " USERID, GROUP_NAME, ITEM_CODE, " & vbCrLf _
            & " ITEM_NAME, ITEM_PART_NO, ITEM_UOM, REF_NO, " & vbCrLf _
            & " V_DATE, NARRATION, STOCK_TYPE, " & vbCrLf _
            & " OPENING, RECEIPT, ISSUE, " & vbCrLf _
            & " CLOSING, RATE, VALUE, AGE_DAY, REF_DATE )"


        'TRIM(ITEM.CUSTOMER_PART_NO),

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', "

        ''Collect the Group Field...
        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 1
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                mGroupBy = GetGroupBy(I)
                If mGroupBy <> "" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                    Exit For
                End If
            End If
        Next


        SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE, TRIM(ITEM.ITEM_SHORT_DESC),  TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM AS UNIT,'','',''," '',"
        If lblLabelType.Text = "StockStmt" Then
            SqlStr = SqlStr & vbCrLf & "'',"
        Else
            SqlStr = SqlStr & vbCrLf & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END , "
        End If


        If chkIncludOp.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) END)) AS Opening, "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR('0.00') AS Opening, "
        End If

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) END)) AS Receipt, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) END)) AS Issue, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as Closing, "

        If lblLabelType.Text = "StockVal" Then
            SqlStr = SqlStr & vbCrLf & "'','','',"
            If optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "STOCK.REF_DATE"
            Else
                SqlStr = SqlStr & vbCrLf & "''"
            End If
        ElseIf lblLabelType.Text = "StockAge" Then
            SqlStr = SqlStr & vbCrLf & "'','',TO_CHAR(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')-MIN(VDate)),''"
        Else
            SqlStr = SqlStr & vbCrLf & "'','','',''"
        End If

        Call GetOptionTable(mOptionalTable, mOptionalJoining)

        SqlStr = SqlStr & vbCrLf & " FROM " & mTRNTableName & " STOCK, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CAT, INV_DIVISION_MST DIV "

        SqlStr = SqlStr & IIf(mOptionalTable = "", "", vbCrLf & mOptionalTable)


        ''**********WHERE CLAUSE .......*************
        ''& " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _


        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " STOCK.FYEAR=" & mFYear & ""

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
            SqlStr = SqlStr & vbCrLf & " AND STOCK.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If CboItemType.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'"
        ElseIf CboItemType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConPH & "'"
        ElseIf CboItemType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConJW & "'"
        ElseIf CboItemType.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConSH & "'"
        End If


        If cboCapital.SelectedIndex = 0 Then
            'SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'"
        ElseIf cboCapital.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.IS_CAPITAL='Y'"
        ElseIf cboCapital.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.IS_CAPITAL='N'"
        End If

        If CboSType.SelectedIndex > 0 Then
            If CboSType.Text = "QC" Then
                SqlStr = SqlStr & vbCrLf & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            ElseIf CboSType.Text = "ST" Or CboSType.Text = "RJ" Then
                SqlStr = SqlStr & vbCrLf & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CAT.COMPANY_CODE " & vbCrLf & " AND ITEM.CATEGORY_CODE=CAT.GEN_CODE AND GEN_TYPE='C'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK.COMPANY_CODE=DIV.COMPANY_CODE " & " AND STOCK.DIV_CODE=DIV.DIV_CODE "

        SqlStr = SqlStr & IIf(GetAttributeCode() = "", "", vbCrLf & GetAttributeCode())

        If cboShow.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.ITEM_IO='" & VB.Left(cboShow.Text, 1) & "'"
        End If

        '    If cboRef.ListIndex > 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK.REF_TYPE='" & vb.Left(cboRef.Text, 3) & "'"
        '    End If

        Dim mRefTypeStr As String
        Dim mRefType As String

        mRefTypeStr = ""
        For CntLst = 0 To cboRef.Items.Count - 1
            If CntLst = 0 And cboRef.GetItemChecked(CntLst) = True Then
                mRefTypeStr = ""
                Exit For
            Else
                If cboRef.GetItemChecked(CntLst) = True Then
                    mRefType = "'" & VB.Left(VB6.GetItemString(cboRef, CntLst), 3) & "'"
                    mRefTypeStr = IIf(mRefTypeStr = "", mRefType, mRefTypeStr & "," & mRefType)
                End If
            End If
        Next

        If mRefTypeStr <> "" Then
            mRefTypeStr = "(" & mRefTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND STOCK.REF_TYPE IN " & mRefTypeStr & ""
        End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & " AND ( STOCK.DEPT_CODE_TO='" & Trim(MasterNo) & "' OR STOCK.DEPT_CODE_FROM='" & Trim(MasterNo) & "')"
            End If
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            SqlStr = SqlStr & vbCrLf & " AND CAT.GEN_DIV_CODE=" & Val(MasterNo) & ""
                SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & Val(MasterNo) & ""
            End If
        End If

        SqlStr = SqlStr & IIf(mOptionalJoining = "", "", vbCrLf & mOptionalJoining)

        If cboIsShowItem.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='" & VB.Left(cboIsShowItem.Text, 1) & "'"
        ElseIf cboIsShowItem.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='" & VB.Left(cboIsShowItem.Text, 1) & "'"
        End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STATUS='O'"
        End If

        If chkIncludOp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If IsDate(txtDateFrom.Text) Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY "

        If mGroupBy <> "" Then
            SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ", "
        End If


        If mGroupBy <> "STOCK.ITEM_CODE" Then
            SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE, "
        End If
        SqlStr = SqlStr & vbCrLf & " STOCK.COMPANY_CODE, TRIM(ITEM.ITEM_SHORT_DESC), TRIM(ITEM.CUSTOMER_PART_NO), STOCK.ITEM_UOM"
        If lblLabelType.Text = "StockStmt" Then

        Else
            SqlStr = SqlStr & vbCrLf & " ,CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END "
        End If


        SqlStr = SqlStr & vbCrLf & ", ITEM.PURCHASE_UOM,ITEM.UOM_FACTOR"

        If lblLabelType.Text = "StockMax" Then

        ElseIf chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING AVG(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        End If

        If lblLabelType.Text = "StockReg" Or lblLabelType.Text = "StockVal" Then
            SqlStr = SqlStr & vbCrLf & "Order By "

            If mGroupBy <> "" Then
                If mGroupBy <> "STOCK.ITEM_CODE" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                End If
            End If
            SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE"

        ElseIf lblLabelType.Text = "StockStmt" Then
            SqlStr = SqlStr & vbCrLf & "Order By "

            If mGroupBy <> "" Then
                If mGroupBy <> "STOCK.ITEM_CODE" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                End If
            End If

            SqlStr = SqlStr & vbCrLf & " STOCK.ITEM_CODE"
        End If

        mSqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(mSqlStr)

        If lblLabelType.Text = "StockVal" Then
            SprdOption.Row = 1
            SprdOption.Col = ColDept1
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                SprdOption.Col = ColGrouping
                mDeptCode = Trim(SprdOption.Text)
            Else
                mDeptCode = ""
            End If
            If chkRate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mSqlStr = " SELECT DISTINCT ITEM_CODE,ITEM_UOM,CLOSING,STOCK_TYPE  FROM TEMP_STOCKREG " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND CLOSING<>0"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                        mUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                        xClosing = IIf(IsDBNull(RsTemp.Fields("Closing").Value), 0, RsTemp.Fields("Closing").Value)
                        mVDate = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
                        mStockType = IIf(IsDBNull(RsTemp.Fields("STOCK_TYPE").Value), "", RsTemp.Fields("STOCK_TYPE").Value)

                        mCostType = IIf(optVal(0).Checked = True, "L", IIf(optVal(1).Checked = True, "P", IIf(optVal(3).Checked = True, "C", "S")))
                        '                    mProductionType = GetProductionType(mItemCode)
                        '                    If CheckItemBom(mItemCode) = True And mCostType <> "S" Then
                        '                        mItemValue = GetLatestWIPCost(mItemCode, mUOM, Abs(xClosing), mVDate, mCostType, mStockType, mDeptCode)
                        '                    Else
                        mStockType = IIf(mStockType = "FG" Or mStockType = "CR", IIf(mCostType = "S", mStockType, "ST"), mStockType)
                        mItemValue = GetLatestItemCostFromMRR(mItemCode, mUOM, System.Math.Abs(xClosing), mVDate, mCostType, mStockType, mDeptCode, , , pStockID)
                        '                    End If
                        mItemRate = CStr(System.Math.Abs(mItemValue / xClosing))

                        mSqlStr = " UPDATE TEMP_STOCKREG SET RATE=" & mItemRate & "" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STOCK_TYPE='" & mStockType & "'"
                        PubDBCn.Execute(mSqlStr)
                        RsTemp.MoveNext()
                    Loop
                End If
            End If
        End If
        PubDBCn.CommitTrans()
        InsertTempStock = True
        Exit Function
InsertErr:
        InsertTempStock = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetOptionTable(ByRef pOptionalTable As String, ByRef pOptionJoining As String) As String
        On Error GoTo ERR1
        Dim pSqlStr As String = ""
        GetOptionTable = ""
        With SprdOption

            '        .Col = ColCategory1
            '        .Row = 1
            '        If .Value = vbChecked Then
            '            pOptionalTable = ", INV_GENERAL_MST ITEMCAT"
            '            pOptionJoining = " AND ITEM.COMPANY_CODE=ITEMCAT.COMPANY_CODE AND ITEM.CATEGORY_CODE=ITEMCAT.GEN_CODE AND ITEMCAT.GEN_TYPE='C'"
            '        End If

            .Col = ColSubCategory1
            .Row = 1
            If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                pOptionalTable = ", INV_SUBCATEGORY_MST ITEMSUBCAT"
                pOptionJoining = " AND ITEM.COMPANY_CODE=ITEMSUBCAT.COMPANY_CODE AND ITEM.SUBCATEGORY_CODE=ITEMSUBCAT.SUBCATEGORY_CODE AND ITEM.CATEGORY_CODE=ITEMSUBCAT.CATEGORY_CODE"
            End If

            '.Col = ColTarrif1
            '.Row = 1
            'If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            '    pOptionalTable = ", FIN_TARRIF_MST ITEMTARIFF"
            '    pOptionJoining = " AND ITEM.COMPANY_CODE=ITEMTARIFF.COMPANY_CODE AND ITEM.TARIFF_CODE=ITEMTARIFF.TARRIF_CODE"
            'End If

        End With

        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function

    Private Function GetAttributeCode() As String

        On Error GoTo ERR1
        Dim pSqlStr As String = ""
        Dim mCategoryCode As String = ""

        With SprdOption
            .Col = ColItemName1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = "STOCK.ITEM_CODE = '" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColDivision1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "DIV.DIV_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColPartyCode1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "STOCK.PARTYCODE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCategory1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                If MainClass.ValidateWithMasterTable(.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.CATEGORY_CODE='" & MasterNo & "'"
                End If
            End If

            .Col = ColSubCategory1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then

                .Col = ColCategory1
                .Row = 3
                If MainClass.ValidateWithMasterTable(.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCategoryCode = MasterNo
                End If

                .Col = ColSubCategory1
                .Row = 3

                If MainClass.ValidateWithMasterTable(.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCategoryCode & "'") = True Then
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.SUBCATEGORY_CODE='" & MasterNo & "'"
                End If
            End If

            .Col = ColLotNo1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "STOCK.BATCH_NO='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColModel1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.ITEM_MODEL='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColMake1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.ITEM_MAKE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColColor1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.ITEM_COLOR='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColDept1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "( STOCK.DEPT_CODE_TO='" & MainClass.AllowSingleQuote(.Text) & "' OR STOCK.DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(.Text) & "')"
                'SqlStr = SqlStr & vbCrLf & " AND ( STOCK.DEPT_CODE_TO='" & MasterNo & "' OR STOCK.DEPT_CODE_FROM='" & MasterNo & "')"
            End If

            .Col = ColTarrif1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "STOCK.HEAT_NO='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCapital1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "STOCK.IS_CAPITAL='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColRefType1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "STOCK.REF_TYPE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColItemType1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "STOCK.ITEM_TYPE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            '.Col = ColTarrif1
            '.Row = 2
            'If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
            '    .Row = 3
            '    If MainClass.ValidateWithMasterTable(.Text, "TARRIF_DESC", "TARRIF_CODE", "FIN_TARRIF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '        pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.TARIFF_CODE='" & MasterNo & "'"
            '    End If
            'End If

        End With
        GetAttributeCode = IIf(pSqlStr = "", "", " AND ") & pSqlStr
        Exit Function
ERR1:
        MsgBox(Err.Description)
        GetAttributeCode = ""
    End Function


    Private Sub frmParamStock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1

        Dim SqlStr As String = ""
        Dim mIsAuthorisedUser As String

        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblLabelType.Text = "StockVal" Then
            FraVal.Visible = True
            optVal(0).Checked = True
        End If

        If lblLabelType.Text = "StockAge" Then
            FraAge.Visible = True
            fraDetSum.Enabled = False
        End If

        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        If InStr(1, mIsAuthorisedUser, "S") = 0 Then
            chkViewAll.Enabled = False
        Else
            chkViewAll.Enabled = True
        End If



        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamStock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        optType(0).Checked = True

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = CStr(RunDate)

        lblYear.Value = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")

        Call FillItemCombo()

        txtDays.Text = CStr(0)
        txtCondQty.Text = CStr(0)

        FormatSprdOption(-1)
        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FillItemCombo()

        On Error GoTo FillErr1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer

        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer
        Dim pCompanyCode As Long
        Dim mRights As String

        CboItemType.Items.Clear()
        CboItemType.Items.Add("Store")
        CboItemType.Items.Add("Production")
        CboItemType.Items.Add("Jobwork")
        CboItemType.Items.Add("Sub-Store")
        CboItemType.SelectedIndex = 0

        cboCapital.Items.Clear()
        cboCapital.Items.Add("All")
        cboCapital.Items.Add("Yes")
        cboCapital.Items.Add("No")
        cboCapital.SelectedIndex = 0



        Call MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDept.SelectedIndex = 0

        Call MainClass.FillCombo(CboSType, "INV_TYPE_MST", "STOCK_TYPE_CODE", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        CboSType.SelectedIndex = 0

        Call MainClass.FillCombo(cboDivision, "INV_DIVISION_MST", "DIV_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDivision.SelectedIndex = 0

        cboCond.Items.Clear()
        cboCond.Items.Add("=")
        cboCond.Items.Add("<=")
        cboCond.Items.Add(">=")
        cboCond.Items.Add("<")
        cboCond.Items.Add(">")
        cboCond.Items.Add("<>")
        cboCond.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("BOTH")
        cboShow.Items.Add("IN")
        cboShow.Items.Add("OUT")
        cboShow.SelectedIndex = 0

        cboIsShowItem.Items.Clear()
        cboIsShowItem.Items.Add("ALL")
        cboIsShowItem.Items.Add("ACTIVE")
        cboIsShowItem.Items.Add("INACTIVE")
        cboIsShowItem.SelectedIndex = 0


        cboRef.Items.Clear()
        cboRef.Items.Add("ALL")
        cboRef.Items.Add("OPN - Opening")
        cboRef.Items.Add("BDM - Break Down Maint.")
        cboRef.Items.Add("DSP - Despatch")
        cboRef.Items.Add("ISS - Issue")
        cboRef.Items.Add("MRR - MRR")
        cboRef.Items.Add("NRG - NRGP")
        cboRef.Items.Add("PMO - P.D.I.")
        cboRef.Items.Add("PMD - Production Slip")
        cboRef.Items.Add("PIS - Production Issue Note")
        cboRef.Items.Add("PRW - Rework Send Back")
        cboRef.Items.Add("REO - Reoffer")
        cboRef.Items.Add("RGP - RGP")
        cboRef.Items.Add("SIS - Sub Store")
        cboRef.Items.Add("SRN - Store Return")
        cboRef.Items.Add("SCP - Scrap")
        cboRef.Items.Add("ADJ - Adjustment")
        cboRef.Items.Add("CON - Consumption")
        cboRef.Items.Add("RWP - Rework Production")
        cboRef.Items.Add("PBU - Production Break-up")
        cboRef.Items.Add("PMS - Preventive Maint. Schd.")
        cboRef.Items.Add("DTN - Division Transfer Note")
        cboRef.Items.Add("FBU - Finished Goods Dismantle")

        'Public Const ConStockRefType_MSL = "MSL"
        'Public Const ConStockRefType_PSL = "PSL"
        'Public Const ConStockRefType_CON = "CON"       ''For CO2

        For I = 0 To cboRef.Items.Count - 1
            cboRef.SetItemChecked(I, True)
        Next

        cboRef.SelectedIndex = 0


        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_CODE FROM GEN_COMPANY_MST " & vbCrLf _
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
                    CntLst = CntLst + 1
                End If
                RS.MoveNext()
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr1:
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

    Private Sub frmParamStock_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        SprdOption.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamStock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            PrintStatus(False)
        End If
    End Sub

    Private Sub optVal_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optVal.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optVal.GetIndex(eventSender)
            PrintStatus(False)
        End If
    End Sub


    Private Sub SprdOption_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdOption.ButtonClicked

        If eventArgs.row = 1 Then
            'GroupOnItem = False
            If eventArgs.buttonDown = System.Windows.Forms.CheckState.Checked Then Exit Sub

            If eventArgs.col = SprdOption.ActiveCol Then
                SprdOption.Row = 1
                SprdOption.Col = SprdOption.ActiveCol
                If SprdOption.Col = ColItemName1 Then
                    GroupOnItem = True
                Else
                    GroupOnItem = False
                End If

                SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked)

            End If
        End If

        If eventArgs.row = 2 Then
            If eventArgs.buttonDown = System.Windows.Forms.CheckState.Checked Then
                SprdOption.Row = 2
                SprdOption.Col = SprdOption.ActiveCol
                MainClass.ProtectCell(SprdOption, 3, 3, SprdOption.ActiveCol, SprdOption.ActiveCol)
                Exit Sub
            End If
            SprdOption.Row = 2
            SprdOption.Col = SprdOption.ActiveCol
            MainClass.UnProtectCell(SprdOption, 3, 3, SprdOption.ActiveCol, SprdOption.ActiveCol)
            MainClass.ProtectCell(SprdOption, 3, 3, IIf(SprdOption.MaxCols = SprdOption.ActiveCol, 1, SprdOption.ActiveCol + 1), IIf(SprdOption.MaxCols = SprdOption.ActiveCol, SprdOption.MaxCols - 1, SprdOption.MaxCols))
        End If
    End Sub

    Private Sub SprdOption_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdOption.Change

        SprdOption.Row = 2
        SprdOption.Col = eventArgs.col
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            SprdOption.Row = 3
            SprdOption.Col = eventArgs.col
            SprdOption.Text = ""
            Exit Sub
        End If
        PrintStatus(False)
    End Sub

    Private Sub SprdOption_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOption.ClickEvent
        Dim mColValue As String
        Dim I As Integer
        Dim mCategory As String
        Dim mCategoryCode As String = ""

        If eventArgs.col = 0 Then Exit Sub

        If eventArgs.row = 1 Then
            SprdOption.Row = 1
            SprdOption.Col = eventArgs.col
            mColValue = SprdOption.Value
            For I = 1 To SprdOption.MaxCols
                SprdOption.Col = I
                SprdOption.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            Next
        End If

        SprdOption.Row = 2
        SprdOption.Col = eventArgs.col
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then Exit Sub

        If eventArgs.row = 0 Then
            Select Case eventArgs.col
                Case ColItemName1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "CUSTOMER_PART_NO")
                Case ColDivision1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColPartyCode1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')")
                Case ColCategory1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'")
                Case ColSubCategory1
                    SprdOption.Row = 3
                    SprdOption.Col = ColCategory1
                    mCategory = Trim(SprdOption.Text)
                    If mCategory = "" Then
                        MsgInformation("Please First Select Category.")
                        Exit Sub
                    End If
                    If MainClass.ValidateWithMasterTable(mCategory, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mCategoryCode = MasterNo
                        Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCategoryCode & "' ")
                    End If
                Case ColModel1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_ITEM_MST", "ITEM_MODEL", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColMake1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_ITEM_MST", "ITEM_MAKE", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColColor1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_ITEM_MST", "ITEM_COLOR", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColDept1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    'Case ColTarrif1
                    '    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_TARRIF_MST", "TARRIF_DESC", "TARRIF_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            End Select
        End If
        PrintStatus(False)
    End Sub
    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub SearchColMaster(ByRef mRow As Integer, ByRef mCol As Integer, ByRef mTable As String, ByRef mField1 As String, ByRef mField2 As String, Optional ByRef mConditional As String = "", Optional ByRef mField3 As String = "", Optional ByRef mField4 As String = "")

        With SprdOption
            SprdOption.Row = 3
            SprdOption.Col = mCol
            If MainClass.SearchGridMaster((SprdOption.Text), mTable, mField1, mField2, mField3, mField4, mConditional) = True Then
                '        If MainClass.SearchMaster(SprdOption.Text, mTable, mField, mConditional) = True Then
                .Row = 3
                .Col = mCol
                .Text = IIf(mTable = "INV_ITEM_MST" Or mTable = "FIN_SUPP_CUST_MST", AcName1, AcName)
            End If
            MainClass.SetFocusToCell(SprdOption, SprdOption.ActiveRow, IIf(SprdOption.MaxCols > mCol, mCol + 1, 1))
        End With
    End Sub
    Private Function GetConditionalQry(ByRef mSqlStr As String, ByRef ColCheck As Integer, ByRef DataFieldName As String) As String

        On Error GoTo ERR1
        Dim FieldName As String
        GetConditionalQry = mSqlStr
        FieldName = GetGroupBy(ColCheck)
        GetConditionalQry = GetConditionalQry & vbCrLf & " AND " & FieldName & "='" & MainClass.AllowSingleQuote(Trim(DataFieldName)) & "' "

        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function GetGroupBy(ByRef ColGroup As Integer) As String
        On Error GoTo ERR1
        Dim mFieldName As String = ""
        Select Case ColGroup
            Case ColItemName1
                mFieldName = "STOCK.ITEM_CODE"
            Case ColDivision1
                mFieldName = "DIV.DIV_DESC"
            Case ColPartyCode1
                mFieldName = "STOCK.PARTYCODE"
            Case ColCategory1
                mFieldName = "CAT.GEN_DESC"
            Case ColSubCategory1
                mFieldName = "ITEMSUBCAT.SUBCATEGORY_DESC"
            Case ColLotNo1
                mFieldName = "STOCK.BATCH_NO"
            Case ColModel1
                mFieldName = "ITEM.ITEM_MODEL"
            Case ColMake1
                mFieldName = "ITEM.ITEM_MAKE"
            Case ColColor1
                mFieldName = "ITEM.ITEM_COLOR"
            Case ColDept1
                mFieldName = "DECODE(STOCK.DEPT_CODE_TO,NULL,'PAD',STOCK.DEPT_CODE_TO)"
                '            mFieldName = "STOCK.DEPT_CODE_TO, STOCK.DEPT_CODE_FROM"
            Case ColTarrif1
                mFieldName = "STOCK.HEAT_NO"

            Case ColItemType1
                mFieldName = "STOCK.ITEM_TYPE"
            Case ColCapital1
                mFieldName = "STOCK.IS_CAPITAL"
            Case ColRefType1
                mFieldName = "STOCK.REF_TYPE"
        End Select


        GetGroupBy = mFieldName

        Exit Function
ERR1:
        GetGroupBy = ""
    End Function
    Private Function FillFieldName(ByRef ColGroup As Integer) As Object
        On Error GoTo ERR1
        Dim mFieldName As String = ""
        Select Case ColGroup
            Case ColItemName1
                mFieldName = "Item Code"
            Case ColDivision1
                mFieldName = "Division"
            Case ColPartyCode1
                mFieldName = "Party Code"
            Case ColCategory1
                mFieldName = "Category"
            Case ColSubCategory1
                mFieldName = "Sub Category"
            Case ColLotNo1
                mFieldName = "Batch / Lot No"
            Case ColModel1
                mFieldName = "Model"
            Case ColMake1
                mFieldName = "Make"
            Case ColColor1
                mFieldName = "Color"
            Case ColDept1
                mFieldName = "Dept"
            Case ColTarrif1
                mFieldName = "Heat No"
            Case ColCapital1
                mFieldName = "Capital"
            Case ColItemType1
                mFieldName = "Item Type"
            Case ColRefType1
                mFieldName = "Ref Type"
        End Select

        FillFieldName = mFieldName
        Exit Function
ERR1:
        FillFieldName = ""
    End Function
    Private Sub txtCondQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCondQty.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCondQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCondQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateFrom.Text = "" Then GoTo EventExitSub
        If Not IsDate(txtDateFrom.Text) Then
            MsgBox("Invalid Date")
            Cancel = True
            'ElseIf FYChk((txtDateFrom.Text)) = False Then
            '    Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateTo.Text = "" Then GoTo EventExitSub
        If IsDate(txtDateTo.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
            'ElseIf FYChk((txtDateTo.Text)) = False Then
            '    Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub GroupByColor()

        Dim mGroup As String = ""
        Dim cntRow As Integer
        Dim mBlackColor As Integer
        Dim mOpening As Double
        Dim mReceipt As Double
        Dim mIssue As Double
        Dim mTotClosing As Double
        Dim mClosing As Double
        Dim xClosing As Double
        Dim mItemRate As Double
        Dim mItemValue As Double
        Dim mItemCode As String
        Dim mMainItemCode As String
        Dim mUOM As String = ""
        Dim mPrevItemCode As String
        Dim mPrevUOM As String
        Dim mVDate As String = ""
        Dim mCostType As String
        Dim mCheckItemCode As String = ""
        Dim mStockType As String = ""
        Dim mDeptCode As String
        Dim mISDeptGroup As String
        Dim mISLotGroup As String
        'Dim mProductionType As String
        Dim mCheckGroup As String
        Dim pStockID As String = ""
        Dim mWIPProcessCostPer As Double
        Dim xDeptCode As String = ""
        Dim mProductionType As String
        Dim mDespQty As Double

        Dim xItemCode As String = ""
        Dim xPrevItemCode As String = ""

        Dim mProdIn As Double
        Dim mProdOut As Double
        Dim mProdCommon As Double
        Dim mValue As Double

        If CboItemType.SelectedIndex = 0 Then
            pStockID = ConWH
        ElseIf CboItemType.SelectedIndex = 1 Then
            pStockID = ConPH
        ElseIf CboItemType.SelectedIndex = 2 Then
            pStockID = ConJW
        ElseIf CboItemType.SelectedIndex = 3 Then
            pStockID = ConSH
        End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xDeptCode = Trim(MasterNo)
            End If
        End If

        mBlackColor = &H80FF80
        mPrevItemCode = ""
        mPrevUOM = ""
        SprdOption.Row = 1
        SprdOption.Col = ColDept1
        mISDeptGroup = IIf(SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

        SprdOption.Col = ColLotNo1
        mISLotGroup = IIf(SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")



        If CboItemType.SelectedIndex = 1 And chkDespatchShow.CheckState = System.Windows.Forms.CheckState.Checked Then
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColProductionQtyIn ''
                    mProdIn = Val(.Text)

                    .Col = ColProductionQtyOut ''
                    mProdOut = Val(.Text)

                    If mProdIn > 0 And mProdOut > 0 Then
                        If mProdIn > mProdOut Then
                            mProdCommon = mProdOut
                            .Col = ColReceipt
                            mValue = Val(.Text)
                            .Text = VB6.Format(mValue - mProdCommon, "0.000")

                            .Col = ColIssue
                            mValue = Val(.Text)
                            .Text = VB6.Format(mValue - mProdCommon, "0.000")

                        End If
                    End If
                Next
            End With
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColGrouping
                mCheckGroup = Trim(.Text)

                If mISLotGroup = "Y" Then
                    .Col = ColCode
                    mCheckGroup = mCheckGroup & "-" & Trim(.Text)
                End If

                If mGroup <> Trim(mCheckGroup) Then
                    If mBlackColor = &HFFFF00 Then
                        mBlackColor = &H80FF80
                    Else
                        mBlackColor = &HFFFF00
                    End If

                    .Col = ColGrouping
                    mGroup = Trim(.Text)

                    If mISLotGroup = "Y" Then
                        .Col = ColCode
                        mGroup = mGroup & "-" & Trim(.Text)
                    End If

                End If

                .Row = cntRow
                .Row2 = cntRow
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(mBlackColor) ''&HFFFF00
                .BlockMode = False
            Next
        End With

        mGroup = ""

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                If optType(2).Checked = True Then
                    .Col = ColGrouping ''
                Else
                    .Col = ColCode ''ColGrouping
                End If

                mCheckGroup = Trim(.Text)
                If mISLotGroup = "Y" Then
                    .Col = ColGrouping
                    mCheckGroup = mCheckGroup & "-" & Trim(.Text)
                End If

                If mCheckItemCode <> mCheckGroup Then
                    If optType(2).Checked = True Then
                        .Col = ColGrouping ''
                    Else
                        .Col = ColCode ''ColGrouping
                    End If
                    mCheckItemCode = Trim(.Text)
                    If mISLotGroup = "Y" Then
                        .Col = ColGrouping
                        mCheckItemCode = mCheckItemCode & "-" & Trim(.Text)
                    End If

                    mTotClosing = 0
                End If

                If lblLabelType.Text = "StockReg" Or lblLabelType.Text = "StockVal" Or lblLabelType.Text = "StockStmt" Then ''
                    .Col = ColOpening
                    mOpening = Val(.Text)

                    .Col = ColReceipt
                    mReceipt = Val(.Text)

                    .Col = ColIssue
                    mIssue = Val(.Text)

                    mClosing = mOpening + mReceipt - mIssue
                    mTotClosing = mTotClosing + mClosing

                    .Col = ColClosing
                    If lblLabelType.Text = "StockReg" Or lblLabelType.Text = "StockStmt" Then
                        If optType(0).Checked = True Then
                            .Text = VB6.Format(mTotClosing, "0.000")
                        Else
                            If chkRunningBal.Checked = True Then
                                .Text = VB6.Format(mTotClosing, "0.000")
                            Else
                                .Text = VB6.Format(mClosing, "0.000")
                            End If
                        End If

                    Else
                        .Text = VB6.Format(mClosing, "0.000")
                    End If
                    If lblLabelType.Text = "StockVal" And optType(2).Checked = False Then

                        If mISDeptGroup = "Y" Then
                            .Col = ColGrouping
                            mDeptCode = Trim(.Text)
                        Else
                            If cboDept.SelectedIndex > 0 Then
                                mDeptCode = xDeptCode
                            Else
                                mDeptCode = ""
                            End If
                        End If

                        .Col = ColCode
                        mItemCode = Trim(.Text)

                        .Col = ColUnit
                        mUOM = Trim(.Text)

                        .Col = ColStockType
                        mStockType = Trim(.Text)

                        If optType(1).Checked = True Then
                            mVDate = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
                        ElseIf optType(0).Checked = True Then
                            .Col = ColRefDate
                            mVDate = VB6.Format(.Text, "DD/MM/YYYY")
                        ElseIf optType(3).Checked = True Then
                            .Col = ColVDate
                            mVDate = VB6.Format(.Text, "DD/MM/YYYY")
                        ElseIf optType(4).Checked = True Then
                            .Col = ColVDate
                            mVDate = "01/" & VB6.Format(.Text, "MM/YYYY")
                            mVDate = MainClass.LastDay(Month(CDate(mVDate)), Year(CDate(mVDate))) & "/" & VB6.Format(mVDate, "MM/YYYY")
                        End If

                        '                    If mItemCode <> mPrevItemCode Or mUOM <> mPrevUOM Then
                        If mClosing = 0 And chkRate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            mItemRate = CDbl("0.00")
                            mItemCode = ""
                            mUOM = ""
                        Else

                            xClosing = IIf(mClosing = 0, 1, mClosing)
                            mCostType = IIf(optVal(0).Checked = True, "L", IIf(optVal(1).Checked = True, "P", IIf(optVal(3).Checked = True, "C", "S")))
                            '                            mProductionType = GetProductionType(mItemCode)
                            '                            If CboItemType.ListIndex = 1 And mProductionType = "P" And mDeptCode <> "" Then
                            '                            If CheckItemBom(mItemCode) = True And mCostType <> "S" Then
                            '                                mItemValue = GetLatestWIPCost(mItemCode, mUOM, Abs(xClosing), mVDate, mCostType, mStockType, mDeptCode)
                            '                            Else
                            'NextVal:
                            '                                mItemValue = GetLatestItemCostFromMRR(mItemCode, mUOM, Abs(xClosing), mVDate, mCostType, mStockType, mDeptCode)
                            '                            End If
                            mStockType = IIf(mStockType = "FG" Or mStockType = "CR", IIf(mCostType = "S", mStockType, "ST"), mStockType)
                            mItemValue = GetLatestItemCostFromMRR(mItemCode, mUOM, System.Math.Abs(xClosing), mVDate, mCostType, mStockType, IIf(CboItemType.SelectedIndex = 0, "", mDeptCode), , , pStockID)

                            If xClosing <> 0 Then
                                mItemRate = System.Math.Abs(mItemValue / xClosing)
                            Else
                                mItemRate = CDbl("0.00")
                            End If
                            If mStockType = "FG" Or mCostType = "S" Then

                            Else
                                mMainItemCode = GetMainItemCode(mItemCode)
                                If CheckItemBom(mMainItemCode) = True Then
                                    mWIPProcessCostPer = GetWIPProcessCost()
                                    mItemRate = mItemRate + (mItemRate * mWIPProcessCostPer * 0.01)
                                End If
                            End If
                        End If
                        '                    End If

                        mPrevItemCode = mItemCode
                        mPrevUOM = mUOM

                        .Col = ColPrice
                        .Text = VB6.Format(System.Math.Abs(mItemRate), "0.000")

                        .Col = ColValue
                        mItemValue = CDbl(VB6.Format(mItemRate * mClosing, "0.000"))
                        .Text = CStr(mItemValue)
                    End If


                    If CboItemType.SelectedIndex = 1 And chkDespatchShow.CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColCode
                        xItemCode = Trim(.Text)

                        If xPrevItemCode <> xItemCode Then
                            mProductionType = GetProductionType(xItemCode)
                            If mProductionType = "P" Or mProductionType = "I" Then
                                mDespQty = GetFGQty(xItemCode)
                            End If
                        End If
                    Else
                        mDespQty = 0
                    End If
                    .Col = ColDespatchQty
                    .Text = VB6.Format(mDespQty, "0.00")

                    xPrevItemCode = xItemCode
                End If
            Next
        End With
    End Sub
    Private Function GetFGQty(ByRef pProductCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String
        GetFGQty = 0
        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)


        SqlStr = " SELECT  " & vbCrLf & " DISTINCT TRN.PRODUCT_CODE" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
        Do While RsTemp1.EOF = False

            If mSameItemCode = "" Then
                mSameItemCode = "'" & Trim(IIf(IsDBNull(RsTemp1.Fields("PRODUCT_CODE").Value), "", RsTemp1.Fields("PRODUCT_CODE").Value)) & "'"
            Else
                mSameItemCode = mSameItemCode & ",'" & Trim(IIf(IsDBNull(RsTemp1.Fields("PRODUCT_CODE").Value), "", RsTemp1.Fields("PRODUCT_CODE").Value)) & "'"
            End If

            RsTemp1.MoveNext()
        Loop

        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If

        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND STOCK_ID = '" & ConWH & "'" & vbCrLf & " AND REF_TYPE = '" & ConStockRefType_DSP & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE ='FG' "
        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetFGQty = GetFGQty + IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub HideUnHide()
        With SprdMain
            '        If optType(0).Value = True Then
            .Col = ColCode
            .ColHidden = False

            .Col = ColItemName
            .ColHidden = False

            .Col = ColPartNo
            .ColHidden = False

            '        Else
            '            .Col = ColCode
            '            .ColHidden = True
            '
            '            .Col = ColItemName
            '            .ColHidden = True
            '        End If

            Select Case lblLabelType.Text
                Case "StockStmt"
                    If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = False
                    ElseIf optType(1).Checked = True Then
                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = True
                    Else
                        .Col = ColCode
                        .ColHidden = True

                        .Col = ColItemName
                        .ColHidden = True

                        .Col = ColPartNo
                        .ColHidden = True

                        .Col = ColUnit
                        .ColHidden = True

                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = True
                    End If
                Case "StockReg"
                    If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                        .Col = ColRefNo
                        .ColHidden = IIf(optType(0).Checked = True, False, True)

                        .Col = ColVDate
                        .ColHidden = False

                        .Col = ColDesc
                        .ColHidden = IIf(optType(0).Checked = True, False, True)

                        .Col = ColStockType
                        .ColHidden = False
                    ElseIf optType(1).Checked = True Then
                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = False 'true
                    Else
                        .Col = ColCode
                        .ColHidden = True

                        .Col = ColItemName
                        .ColHidden = True

                        .Col = ColPartNo
                        .ColHidden = True

                        .Col = ColUnit
                        .ColHidden = True

                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = False 'true
                    End If
                Case "StockVal"
                    If optType(0).Checked = True Or optType(3).Checked = True Or optType(4).Checked = True Then
                        .Col = ColRefNo
                        .ColHidden = IIf(optType(0).Checked = True, False, True)

                        .Col = ColVDate
                        .ColHidden = False

                        .Col = ColDesc
                        .ColHidden = IIf(optType(0).Checked = True, False, True)

                        .Col = ColStockType
                        .ColHidden = False
                    ElseIf optType(1).Checked = True Then
                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = False 'true
                    Else
                        .Col = ColCode
                        .ColHidden = True

                        .Col = ColItemName
                        .ColHidden = True

                        .Col = ColPartNo
                        .ColHidden = True

                        .Col = ColUnit
                        .ColHidden = True

                        .Col = ColRefNo
                        .ColHidden = True

                        .Col = ColVDate
                        .ColHidden = True

                        .Col = ColDesc
                        .ColHidden = True

                        .Col = ColStockType
                        .ColHidden = False 'true
                    End If

                    .Col = ColClosing
                    .ColHidden = False

                    '                .Col = ColOpening
                    '                .ColHidden = True
                    '
                    '                .Col = ColReceipt
                    '                .ColHidden = True
                    '
                    '                .Col = ColIssue
                    '                .ColHidden = True
                    '
                    .Col = ColPrice
                    .ColHidden = False

                    .Col = ColValue
                    .ColHidden = False
                Case "StockAge"
                    .Col = ColOpening
                    .ColHidden = True

                    .Col = ColReceipt
                    .ColHidden = True

                    .Col = ColIssue
                    .ColHidden = True

                    .Col = ColAgeDays
                    .ColHidden = False
            End Select
        End With
    End Sub
    Private Sub txtDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDays_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDays.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsNumeric(txtDays.Text) Then
            MsgBox("Days must be numeric.", MsgBoxStyle.Critical)
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        Dim SqlStr As String = ""
        Dim xRefNo As Double
        Dim xRefType As String
        Dim xIssueType As String
        Dim XRIGHT As String
        Dim myxMenu As String
        Dim pCompanyCode As Long

        If optType(0).Checked = False Then Exit Sub ''Detail Option....

        If RsCompany.Fields("FYEAR").Value <> mFYear Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xRefNo = Val(SprdMain.Text)
        pCompanyCode = Mid(SprdMain.Text, Len(SprdMain.Text) - 1, 2)
        SprdMain.Col = ColRefType
        xRefType = Trim(SprdMain.Text)

        If pCompanyCode <> RsCompany.Fields("COMPANY_CODE").Value Then
            Exit Sub
        End If

        Select Case xRefType
            Case "MRR"
                myxMenu = "MNUMRR"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If
                FrmMRR.MdiParent = Me.MdiParent
                FrmMRR.Show()

                FrmMRR.lblBookType.Text = "Q"

                FrmMRR.FrmMRR_Activated(Nothing, New System.EventArgs())

                FrmMRR.txtMRRNo.Text = CStr(xRefNo)
                FrmMRR.TxtMRRNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            Case "ISS"

                myxMenu = "mnuMatIssueNote"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If

                If MainClass.ValidateWithMasterTable(xRefNo, "AUTO_KEY_ISS", "ISSUE_TYPE", "INV_ISSUE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xIssueType = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    Exit Sub
                End If
                If xIssueType = "O" Then
                    myxMenu = "mnuMatIssueNote"
                    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
                    If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
                        Exit Sub
                    End If
                    FrmStoreReq.MdiParent = Me.MdiParent
                    FrmStoreReq.Show()
                    FrmStoreReq.lblBookType.Text = "I"

                    FrmStoreReq.FrmStoreReq_Activated(Nothing, New System.EventArgs())

                    FrmStoreReq.txtReqNo.Text = CStr(xRefNo)
                    FrmStoreReq.txtReqNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                Else
                    'If xIsSuppIssue = "N" Then
                    myxMenu = "mnuBOPMatIssueNote"
                    'Else
                    '    myMenu = "mnuBOPMatIssueNoteSupp"
                    'End If

                    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
                    If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
                        Exit Sub
                    End If
                    FrmStoreReqBOP.MdiParent = Me.MdiParent
                    FrmStoreReqBOP.Show()
                    FrmStoreReqBOP.lblBookType.Text = "I"


                    FrmStoreReqBOP.lblIsSuppIssue.Text = "N"

                    FrmStoreReqBOP.FrmStoreReqBOP_Activated(Nothing, New System.EventArgs())

                    FrmStoreReqBOP.txtReqNo.Text = CStr(xRefNo)
                    FrmStoreReqBOP.txtReqNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                End If
            Case "ADJ"
            Case "DSP"
                myxMenu = "MNUDESPATCHNOTE"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If
                FrmDespatchNote.MdiParent = Me.MdiParent
                If MainClass.ValidateWithMasterTable(xRefNo, "AUTO_KEY_DESP", "DESPATCHTYPE", "DSP_DESPATCH_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    FrmDespatchNote.lblDespType.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    Exit Sub
                End If

                FrmDespatchNote.Show()

                FrmDespatchNote.FrmDespatchNote_Activated(Nothing, New System.EventArgs())

                FrmDespatchNote.txtDNNo.Text = CStr(xRefNo)
                FrmDespatchNote.txtDNNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            Case "DTN"
            Case "FBU"
            Case "NRG"

                myxMenu = "MNUGATEPASSGSTNRGP"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If

                frmGatePassGST.MdiParent = Me.MdiParent
                frmGatePassGST.lblBookType.Text = "N"
                frmGatePassGST.Show()
                frmGatePassGST.frmGatePassGST_Activated(Nothing, New System.EventArgs())
                frmGatePassGST.txtGatepassno.Text = CStr(xRefNo)
                frmGatePassGST.txtGatepassno_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

            Case "OPN"
            Case "PDM"
            Case "PIS"
                myxMenu = "mnuMatIssueNote"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If

                FrmProdIssuRecvNote.MdiParent = Me.MdiParent
                FrmProdIssuRecvNote.lblBookType.Text = "R"
                FrmProdIssuRecvNote.Show()
                FrmProdIssuRecvNote.FrmProdIssuRecvNote_Activated(Nothing, New System.EventArgs())

                FrmProdIssuRecvNote.txtIssueNo.Text = CStr(xRefNo)
                FrmProdIssuRecvNote.txtIssueNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            Case "PMD"
                myxMenu = "MNUDEPTWISEPRODUCTION"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If

                FrmPMemoDeptWise.MdiParent = Me.MdiParent
                FrmPMemoDeptWise.lblBookType.Text = "P"
                FrmPMemoDeptWise.Show()

                FrmPMemoDeptWise.FrmPMemoDeptWise_Activated(Nothing, New System.EventArgs())

                FrmPMemoDeptWise.txtPMemoNo.Text = CStr(xRefNo)

                FrmPMemoDeptWise.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            Case "PMO"
                myxMenu = "MNUPMEMO"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If

                FrmPDI.MdiParent = Me.MdiParent
                FrmPDI.Show()
                FrmPDI.FrmPDI_Activated(Nothing, New System.EventArgs())

                FrmPDI.txtPMemoNo.Text = CStr(xRefNo)
                FrmPDI.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

            Case "PMS"
            Case "PRW"
            Case "RGP"
                myxMenu = "MNUGATEPASSGST"
                XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
                If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
                    Exit Sub
                End If

                frmGatePassGST.MdiParent = Me.MdiParent
                frmGatePassGST.lblBookType.Text = "R"
                frmGatePassGST.Show()
                frmGatePassGST.frmGatePassGST_Activated(Nothing, New System.EventArgs())
                frmGatePassGST.txtGatepassno.Text = CStr(xRefNo)
                frmGatePassGST.txtGatepassno_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            Case "RWP"
            Case "SCP"
            Case "SIS"
            Case "SRN"
            Case "WBU"

        End Select



    End Sub
End Class
