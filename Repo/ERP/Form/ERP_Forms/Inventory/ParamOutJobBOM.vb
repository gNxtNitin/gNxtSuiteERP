Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamOutJobBOM
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 12

    Dim mPartyC4 As String
    Private Const ColLocked As Short = 1
    Private Const ColInItemCat As Short = 2
    Private Const ColInItemCode As Short = 3
    Private Const ColInItemDesc As Short = 4
    Private Const ColInItemUOM As Short = 5
    Private Const ColInItemCon_Unit As Short = 6
    Private Const ColWEFDate As Short = 7
    Private Const ColAmendNo As Short = 8
    Private Const ColOutItemCode As Short = 9
    Private Const ColOutItemDesc As Short = 10
    Private Const ColOutItemUOM As Short = 11
    Private Const ColOutItemCon_Unit As Short = 12
    Private Const ColOutAlterItemCode As Short = 13
    Private Const ColOutAlterItemDesc As Short = 14
    Private Const ColOutAlterItemUOM As Short = 15
    Private Const ColOutAlterItemCon_Unit As Short = 16


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkOutwardAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOutwardAll.CheckStateChanged
        Call PrintStatus(False)
        If chkOutwardAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOutwardItemDesc.Enabled = False
            cmdOutwardSearch.Enabled = False
        Else
            txtOutwardItemDesc.Enabled = True
            cmdOutwardSearch.Enabled = True
        End If
    End Sub

    Private Sub ChkInwardAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkInwardAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkInwardAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtInwardItemDesc.Enabled = False
            cmdInwardSearch.Enabled = False
        Else
            txtInwardItemDesc.Enabled = True
            cmdInwardSearch.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdOutwardSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOutwardSearch.Click
        SearchOutItem()
    End Sub

    Private Sub cmdInwardSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdInwardSearch.Click
        SearchInItem()
    End Sub


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonBOM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ReportonBOM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonBOM(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""

        SqlStr = ""

        If InsertPrintDummy = False Then GoTo ReportErr

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Report1.Reset()

        mSubTitle = ""

        mTitle = "Consumption Report"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\Consumption.RPT"


        If ChkInwardAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & "( Inward Item : " & txtInwardItemDesc.Text & ")"
        End If

        If chkOutwardAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & "( Outward Item : " & txtOutwardItemDesc.Text & ")"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function

    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mInItemCode As String
        Dim mInItemDesc As String
        Dim mInItemUOM As String
        Dim mInItemCon_Unit As String
        Dim mOutItemCode As String
        Dim mOutItemDesc As String
        Dim mOutItemUOM As String
        Dim mOutItemCon_Unit As String
        Dim mAmendNo As String
        Dim mWef As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                '            .Col = ColInItemCat

                .Col = ColInItemCode
                mInItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColInItemDesc
                mInItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColInItemUOM
                mInItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColInItemCon_Unit
                mInItemCon_Unit = MainClass.AllowSingleQuote(.Text)

                .Col = ColOutItemCode
                mOutItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColOutItemDesc
                mOutItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColOutItemUOM
                mOutItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColOutItemCon_Unit
                mOutItemCon_Unit = MainClass.AllowSingleQuote(.Text)

                .Col = ColWEFDate
                mWef = MainClass.AllowSingleQuote(.Text)

                .Col = ColAmendNo
                mAmendNo = MainClass.AllowSingleQuote(.Text)

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf & " Field1,Field2,Field3,Field4,Field5," & vbCrLf & " Field6,Field7,Field8,Field9,Field10) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mInItemCode & "', " & vbCrLf & " '" & mInItemDesc & "', " & vbCrLf & " '" & mInItemUOM & "', " & vbCrLf & " '" & mInItemCon_Unit & "', " & vbCrLf & " '" & mOutItemCode & "', " & vbCrLf & " '" & mOutItemDesc & "', " & vbCrLf & " '" & mOutItemUOM & "', " & vbCrLf & " '" & mOutItemCon_Unit & "','" & mWef & "','" & mAmendNo & "') "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        InsertPrintDummy = False
        PubDBCn.RollbackTrans()
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
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
    Private Sub frmParamOutJobBOM_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Outward Jobwork Consumption Detail Report"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamOutJobBOM_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        ChkInwardAll.CheckState = System.Windows.Forms.CheckState.Checked
        cmdInwardSearch.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamOutJobBOM_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamOutJobBOM_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Dispose()
        Me.Close()
    End Sub




    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SearchOutItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtOutwardItemDesc.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtOutwardItemDesc.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchInItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtInwardItemDesc.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtInwardItemDesc.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        'Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColOutAlterItemCon_Unit
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 10)
            .ColHidden = True

            .Col = ColWEFDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColWEFDate, 8)

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAmendNo, 6)

            .Col = ColInItemCat
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInItemCat, 6)

            .Col = ColInItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInItemCode, 6)

            .Col = ColInItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInItemDesc, 25)

            .Col = ColInItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInItemUOM, 5)


            .Col = ColInItemCon_Unit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColInItemCon_Unit, 6)

            .Col = ColOutItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOutItemCode, 6)

            .Col = ColOutItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOutItemDesc, 25)

            .Col = ColOutItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOutItemUOM, 5)

            .Col = ColOutItemCon_Unit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOutItemCon_Unit, 6)

            .Col = ColOutAlterItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOutAlterItemCode, 6)

            .Col = ColOutAlterItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOutAlterItemDesc, 25)

            .Col = ColOutAlterItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOutAlterItemUOM, 5)

            .Col = ColOutAlterItemCon_Unit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOutAlterItemCon_Unit, 6)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            '        SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub GroupByColor()
        'Dim mGroup As String
        Dim cntRow As Integer
        Dim mBlackColor As Integer
        'Dim mItemCode As String
        Dim mPrevItemCode As String
        Dim mCurrItemCode As String

        mPrevItemCode = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColInItemCode
                mCurrItemCode = Trim(.Text)

                .Col = ColAmendNo
                mCurrItemCode = Trim(.Text) & "-" & mCurrItemCode

                If mPrevItemCode <> mCurrItemCode Then
                    If mBlackColor = &HFFFF00 Then
                        mBlackColor = &H80FF80
                    Else
                        mBlackColor = &HFFFF00
                    End If
                    .Col = ColInItemCode
                    mPrevItemCode = Trim(.Text)

                    .Col = ColAmendNo
                    mPrevItemCode = Trim(.Text) & "-" & mPrevItemCode
                End If

                .Row = cntRow
                .Row2 = cntRow
                .Col = 1
                .col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(mBlackColor) ''&HFFFF00
                .BlockMode = False
            Next
        End With
        '
        '    mGroup = ""
        '
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '            .Col = ColCode          ''ColGrouping
        '            If mCheckItemCode <> Trim(.Text) Then
        ''                If mBlackColor = &HFFFF00 Then
        ''                    mBlackColor = &H80FF80
        ''                Else
        ''                    mBlackColor = &HFFFF00
        ''                End If
        '                mCheckItemCode = Trim(.Text)
        '                mTotClosing = 0
        '            End If
        ''
        ''            .Row = cntRow
        ''            .Row2 = cntRow
        ''            .Col = 1
        ''            .col2 = .MaxCols
        ''            .BlockMode = True
        ''            .BackColor = mBlackColor            ''&HFFFF00
        ''            .BlockMode = False
        '
        '            If lblLabelType.text = "StockReg" Or lblLabelType.text = "StockVal" Or lblLabelType.text = "StockStmt" Then      ''
        '                .Col = ColOpening
        '                mOpening = Val(.Text)
        '
        '                .Col = ColReceipt
        '                mReceipt = Val(.Text)
        '
        '                .Col = ColIssue
        '                mIssue = Val(.Text)
        '
        '                mClosing = mOpening + mReceipt - mIssue
        '                mTotClosing = mTotClosing + mClosing
        '
        '                .Col = ColClosing
        '                .Text = VB6.Format(mTotClosing, "0.000")
        '
        '                If lblLabelType.text = "StockVal" Then
        '
        '                    .Col = ColCode
        '                    mItemCode = Trim(.Text)
        '
        '                    .Col = ColUnit
        '                    mUOM = Trim(.Text)
        '
        '                    If optType(1).Value = True Then
        '                        mVDate = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        '                    Else
        '                        .Col = ColVDate
        '                        mVDate = VB6.Format(.Text, "DD/MM/YYYY")
        '                    End If
        '
        ''                    If mItemCode <> mPrevItemCode Or mUOM <> mPrevUOM Then
        '                        If mClosing = 0 Then
        '                            mItemRate = "0.00"
        '                            mItemCode = ""
        '                            mUOM = ""
        '                        Else
        '                            mCostType = IIf(optVal(0).Value = True, "L", IIf(optVal(1).Value = True, "P", "S"))
        '                            mItemValue = GetLatestItemCostFromMRR(mItemCode, mUOM, Abs(mClosing), mVDate, mCostType)
        '                            mItemRate = Abs(mItemValue / mClosing)
        '                        End If
        ''                    End If
        '
        '                    mPrevItemCode = mItemCode
        '                    mPrevUOM = mUOM
        '
        '                    .Col = ColPrice
        '                    .Text = VB6.Format(Abs(mItemRate), "0.00")
        '
        '                    .Col = ColValue
        '                    mItemValue = VB6.Format(mItemRate * mClosing, "0.00")
        '                    .Text = mItemValue
        '                End If
        '
        '            End If
        '        Next
        '    End With
    End Sub

    Private Function Show1() As Boolean

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

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Call GroupByColor()
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
        Dim mSuppCode As String

        ''SELECT CLAUSE...

        MakeSQL = " SELECT '', GMST.GEN_DESC, " & vbCrLf & " IH.PRODUCT_CODE, A.ITEM_SHORT_DESC, A.ISSUE_UOM, TO_CHAR(1), IH.WEF, IH.AMEND_NO," & vbCrLf & " ID.ITEM_CODE, B.ITEM_SHORT_DESC, B.ISSUE_UOM, TO_CHAR(ID.ITEM_QTY), "

        MakeSQL = MakeSQL & vbCrLf & " IA.ALTER_ITEM_CODE, C.ITEM_SHORT_DESC, C.ISSUE_UOM, TO_CHAR(IA.ALTER_ITEM_QTY) "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, PRD_OUTBOM_ALTER_DET IA, INV_ITEM_MST A,INV_ITEM_MST B,INV_ITEM_MST C, INV_GENERAL_MST GMST "

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.MKEY=IA.MKEY(+) AND ID.ITEM_CODE=IA.ITEM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=A.ITEM_CODE " & vbCrLf & " AND A.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf & " AND A.CATEGORY_CODE=GMST.GEN_CODE " & vbCrLf & " AND ID.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND IA.COMPANY_CODE=C.COMPANY_CODE(+)" & vbCrLf & " AND IA.ALTER_ITEM_CODE=C.ITEM_CODE(+) "

        MakeSQL = MakeSQL & vbCrLf & " AND GMST.GEN_TYPE='C'"

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.STATUS='O'"
        ElseIf optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.STATUS='C'"
        End If

        If ChkInwardAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtInwardItemDesc.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkOutwardAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtOutwardItemDesc.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND (ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' OR IA.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "')"
            End If

        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND GMST.GEN_DESC='" & MainClass.AllowSingleQuote(txtCategory.Text) & "'"
        End If

        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY  IH.PRODUCT_CODE, IH.AMEND_NO,ID.SERIAL_NO, IA.ALTER_SERIAL_NO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1

        If ChkInwardAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtInwardItemDesc.Text) = "" Then
                MsgInformation("Inward Item Code cann't be Blank")
                txtInwardItemDesc.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtInwardItemDesc.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Inward Item Code.")
                txtInwardItemDesc.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkOutwardAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtOutwardItemDesc.Text) = "" Then
                MsgInformation("Outward Item Code cann't be Blank")
                txtOutwardItemDesc.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtOutwardItemDesc.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Outward Item Code.")
                txtOutwardItemDesc.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

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

    Private Sub txtOutwardItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOutwardItemDesc.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtOutwardItemDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOutwardItemDesc.DoubleClick
        SearchOutItem()
    End Sub


    Private Sub txtOutwardItemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOutwardItemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOutwardItemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOutwardItemDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOutwardItemDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchOutItem()
    End Sub


    Private Sub txtOutwardItemDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOutwardItemDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtOutwardItemDesc.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtOutwardItemDesc.Text), "ITEM_SHORT_DESC", "ITEm_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Outward Item Code.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtInwardItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInwardItemDesc.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtInwardItemDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInwardItemDesc.DoubleClick
        SearchInItem()
    End Sub


    Private Sub txtInwardItemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInwardItemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInwardItemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInwardItemDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInwardItemDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchInItem()
    End Sub


    Private Sub txtInwardItemDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInwardItemDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtInwardItemDesc.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtInwardItemDesc.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Inward Item Description.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
