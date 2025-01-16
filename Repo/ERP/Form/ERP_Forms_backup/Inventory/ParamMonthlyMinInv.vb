Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMonthlyMinInv
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 22


    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColMinDays As Short = 4
    Private Const ColSchdQty As Short = 5
    Private Const ColConsQty As Short = 6
    Private Const ColMinQty As Short = 7
    Private Const ColRate As Short = 8
    Private Const ColValue As Short = 9

    Private Const mStockQtyStr As String = "Stock Qty On "
    Private Const mPlanQtyStr As String = "Plan Qty On "

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mFixedCol As Short

    Dim mMaxRow As Integer
    Dim mMaxCol As Integer
    Dim mColWidth As Single


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItem.CheckStateChanged
        txtItemName.Enabled = IIf(chkItem.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchItem.Enabled = IIf(chkItem.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '    If chkFG.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '            mCategoryCode = MasterNo
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '    End If

        '    If MainClass.SearchGridMaster(TxtItemName.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
        If MainClass.SearchGridMaster((txtItemName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(TxtItemName, New System.ComponentModel.CancelEventArgs(False))
            txtItemName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Show1()
        FormatSprdMain(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mRMCode As String
        Dim mRMName As String
        Dim mUnit As String
        Dim mPlanQty As Double
        Dim mcntRow As Integer
        Dim mMinInv As Double
        Dim mWorkingDays As Double
        Dim mDate As String
        Dim mRate As Double
        Dim mValue As Double
        Dim mMinDays As Double
        Dim mLandedCost As Double
        Dim mFactor As Double
        Dim mConsQty As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mDate = VB6.Format("01/" & Trim(cboMonth.Text) & " / " & Trim(txtYear.Text), "DD/MM/YYYY")

        mWorkingDays = GetWorkingDays(mDate)

        SqlStr = ""

        SqlStr = " SELECT IH.RM_CODE,ITEM2.ITEM_SHORT_DESC AS RM_NAME, ITEM2.ISSUE_UOM AS RM_UOM,ITEM2.ECONOMIC_QTY, " & vbCrLf & " SUM(IH.RM_QTY) AS DPLAN_QTY, " & vbCrLf & " ROUND(SUM(IH.RM_QTY) / " & mWorkingDays & ",0) AS CONS_QTY, " & vbCrLf & " ROUND(SUM(IH.RM_QTY * ITEM2.ECONOMIC_QTY) / " & mWorkingDays & ",0) AS MIN_QTY,UOM_FACTOR "

        SqlStr = MakeCondSQL(SqlStr, False, False)

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.RM_CODE,ITEM2.ITEM_SHORT_DESC, ITEM2.ISSUE_UOM ,ITEM2.ECONOMIC_QTY,UOM_FACTOR "

        SqlStr = SqlStr & vbCrLf & " Order By IH.RM_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        mDate = MainClass.LastDay(Month(CDate(mDate)), Year(CDate(mDate))) & "/" & VB6.Format(mDate, "MM/YYYY")
        mcntRow = 1

        With SprdMain
            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    mRMName = IIf(IsDbNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)
                    mUnit = IIf(IsDbNull(RsShow.Fields("RM_UOM").Value), "", RsShow.Fields("RM_UOM").Value)
                    mMinDays = IIf(IsDbNull(RsShow.Fields("ECONOMIC_QTY").Value), "", RsShow.Fields("ECONOMIC_QTY").Value)
                    mPlanQty = Val(IIf(IsDbNull(RsShow.Fields("DPLAN_QTY").Value), "", RsShow.Fields("DPLAN_QTY").Value))
                    mConsQty = Val(IIf(IsDbNull(RsShow.Fields("CONS_QTY").Value), "", RsShow.Fields("CONS_QTY").Value))
                    mMinInv = Val(IIf(IsDbNull(RsShow.Fields("MIN_QTY").Value), "", RsShow.Fields("MIN_QTY").Value))
                    mFactor = Val(IIf(IsDbNull(RsShow.Fields("UOM_FACTOR").Value), 1, RsShow.Fields("UOM_FACTOR").Value))

                    .Row = mcntRow
                    .Col = ColItemCode
                    .Text = mRMCode

                    .Col = ColItemName
                    .Text = mRMName

                    .Col = ColUnit
                    .Text = mUnit


                    .Col = ColMinDays
                    .Text = VB6.Format(mMinDays, "0.00")

                    .Col = ColSchdQty
                    .Text = VB6.Format(mPlanQty, "0.00")

                    .Col = ColConsQty
                    .Text = VB6.Format(mConsQty, "0.00")

                    .Col = ColMinQty
                    .Text = VB6.Format(mMinInv, "0.00")

                    If GetLatestItemCostFromPO(mRMCode, mRate, mLandedCost, mDate, "ST", "", mUnit, mFactor) = False Then GoTo LedgError

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.00")

                    mValue = mMinInv * mRate

                    .Col = ColValue
                    .Text = VB6.Format(mValue, "0.00")

                    RsShow.MoveNext()

                    If RsShow.EOF = False Then
                        mcntRow = mcntRow + 1
                        .MaxRows = mcntRow
                    End If

                Loop
            End If
        End With
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsShow.Cancel()
        RsShow.Close()
        RsShow = Nothing
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
        '    Resume
    End Sub

    Private Function GetWorkingDays(ByRef pDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        GetWorkingDays = MainClass.LastDay(Month(CDate(pDate)), Year(CDate(pDate)))

        SqlStr = " SELECT COUNT(1) AS LDAYS FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(pDate, "MMM-YYYY")) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetWorkingDays = GetWorkingDays - IIf(IsDbNull(RsTemp.Fields("LDAYS").Value), 0, RsTemp.Fields("LDAYS").Value)
        End If
        Exit Function
ErrPart:
        GetWorkingDays = 0
    End Function
    Private Function MakeCondSQL(ByRef mSqlStr As String, ByRef mFinishItemReq As Boolean, ByRef mPartyWiseQry As Boolean) As String

        On Error GoTo ERR1
        Dim mFGCode As String
        Dim mItemCode As String
        Dim mDate As String

        mDate = VB6.Format("01/" & Trim(cboMonth.Text) & " / " & Trim(txtYear.Text), "DD/MM/YYYY")

        If mFinishItemReq = True Then
            mSqlStr = mSqlStr & vbCrLf & " FROM INV_PROCESS_MONTHLY_SCHLD IH,INV_ITEM_MST ITEM1,INV_ITEM_MST ITEM2 " & vbCrLf & " WHERE IH.PRODUCT_CODE = ITEM1.ITEM_CODE(+) " & vbCrLf & " AND IH.COMPANY_CODE = ITEM1.COMPANY_CODE(+) " & vbCrLf & " AND IH.RM_CODE = ITEM2.ITEM_CODE(+) " & vbCrLf & " AND IH.COMPANY_CODE = ITEM2.COMPANY_CODE(+) "
        Else
            If mPartyWiseQry = False Then
                mSqlStr = mSqlStr & vbCrLf & " FROM INV_PROCESS_MONTHLY_SCHLD IH,INV_ITEM_MST ITEM2 " & vbCrLf & " WHERE IH.RM_CODE = ITEM2.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE = ITEM2.COMPANY_CODE "
            Else
                mSqlStr = mSqlStr & vbCrLf & " FROM INV_PROCESS_MONTHLY_SCHLD IH, INV_ITEM_MST ITEM2, " & vbCrLf & " FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_DET ACMDetail" & vbCrLf & " WHERE IH.COMPANY_CODE = ITEM2.COMPANY_CODE " & vbCrLf & " AND IH.RM_CODE = ITEM2.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE = ACMDetail.COMPANY_CODE " & vbCrLf & " AND IH.RM_CODE = ACMDetail.ITEM_CODE" & vbCrLf & " AND ACMDetail.COMPANY_CODE = ACM.COMPANY_CODE " & vbCrLf & " AND ACMDetail.SUPP_CUST_CODE = ACM.SUPP_CUST_CODE"
            End If

        End If

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.BOOKTYPE='" & lblBookType.Text & "' " & vbCrLf & " AND IH.BOOKSUBTYPE='" & lblBookSubType.Text & "' " & vbCrLf & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM') ='" & VB6.Format(mDate, "YYYYMM") & "' "

        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                mSqlStr = mSqlStr & vbCrLf & " AND IH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "
            End If
        End If

        MakeCondSQL = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeCondSQL = ""
    End Function
    Public Sub frmParamMonthlyMinInv_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        '    If lblBookType.text = Left(ConDespatchPlan, 1) And lblBookSubType.text = Right(ConDespatchPlan, 1) Then
        '        Me.text = "Revised Despatch & Production Planning"
        '        optDetSummarised(2).Enabled = False
        '    ElseIf lblBookType.text = Left(ConPurchase, 1) And lblBookSubType.text = Right(ConPurchase, 1) Then
        '        Me.text = "Monthly Schedule Planning"
        '    End If
        FormatSprdMain(False)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamMonthlyMinInv_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        'Me.Height = VB6.TwipsToPixelsY(7440)
        ''Me.Width = VB6.TwipsToPixelsX(11625)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        txtYear.Text = CStr(Year(RunDate)) ''VB6.Format(RsCompany.fields("FYEAR").value, "0000")
        txtYear.Enabled = False

        chkItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkItem_CheckStateChanged(chkItem, New System.EventArgs())
        Call FillMonthCombo()
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain(ByRef mFillColHeading As Boolean)

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColValue
            .set_RowHeight(-1, RowHeight * 0.75)

            .Row = -1
            .set_ColWidth(0, 4)


            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 25)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)


            For cntCol = ColMinDays To ColValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next

            .ColsFrozen = ColItemName

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

        End With

    End Sub

    Private Sub frmParamMonthlyMinInv_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMonthlyMinInv_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSchedule(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mColStart As Integer
        Dim FieldSeq As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


                mColStart = 1


                For cntCol = mColStart To .MaxCols
                    .Col = cntCol


                    FieldSeq = cntCol


                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & FieldSeq
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'"
                    Else
                        mFieldStr = "FIELD" & FieldSeq & ","
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
        Exit Sub
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ReportOnSchedule(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mCustDealer As String
        Dim SqlStr As String = ""

        Report1.Reset()
        SqlStr = ""
        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        MainClass.ClearCRptFormulas(Report1)

        Call InsertIntoPrintdummyData()

        '*************** Fetching Record For Report ***************************
        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " ORDER BY SUBROW"

        '    If lblBookType.text = Left(ConDespatchPlan, 1) And lblBookSubType.text = Right(ConDespatchPlan, 1) Then
        '        mTitle = "Revised Despatch & Production Planning"
        '        Report1.ReportFileName = App.path & "\Reports\RevDespProdPlan.rpt"
        '    ElseIf lblBookType.text = Left(ConPurchase, 1) And lblBookSubType.text = Right(ConPurchase, 1) Then
        '        mTitle = "MONTHLY SCHEDULE Planning"
        '        If optDetSummarised(2).Value = True Then
        '            Report1.ReportFileName = App.path & "\Reports\MonthlySchedulePartyWise.rpt"
        '        Else
        '            Report1.ReportFileName = App.path & "\Reports\MonthlySchedule.rpt"
        '        End If
        '    End If

        '    If optDetSummarised(0).Value = True Then
        '        mTitle = mTitle & " [Detailed]"
        '    ElseIf optDetSummarised(1).Value = True Then
        '        mTitle = mTitle & " [Summarised]"
        '    ElseIf optDetSummarised(2).Value = True Then
        '        mTitle = mTitle & " [Party Wise]"
        '    End If
        '
        '    mSubTitle = "FOR THE MONTH OF : " & cboMonth.Text & " - " & txtYear.Text
        '
        '    If chkFG.Value = vbUnchecked And Trim(txtFGName.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [Category : " & txtFGName.Text & "]"
        '    End If
        '    If chkItem.Value = vbUnchecked And Trim(txtItemName.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [Sub Category : " & txtItemName.Text & "]"
        '    End If
        '    If chkAllParty.Value = vbUnchecked And Trim(txtPartyName.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [Supplier : " & txtPartyName.Text & "]"
        '    End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub



ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim ii As Integer
        'Dim mHeadStr As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        With SprdMain
            .Row = 0
            '        If optDetSummarised(2).Value = True Then
            '            For ii = ColPartyName To .MaxCols
            '                .Col = ii
            '                mHeadStr = "FldHead" & ii & "=""" & .Text & """"
            '                MainClass.AssignCRptFormulas Report1, mHeadStr
            '            Next
            '
            '        Else
            '            For ii = ColMainProd To .MaxCols
            '                .Col = ii
            '                mHeadStr = "FldHead" & ii - 1 & "=""" & .Text & """"
            '                MainClass.AssignCRptFormulas Report1, mHeadStr
            '            Next
            '        End If
        End With
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSchedule(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FillHeading()
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        '    SqlStr = ""
        '    SqlStr = " SELECT DISTINCT TO_CHAR(IH.PROCESS_DATE,'DD-MM-YYYY') AS  PROCESS_DATE "
        '
        '    SqlStr = MakeCondSQL(SqlStr, True, False)
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY PROCESS_DATE "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    SprdMain.MaxCols = ColUnit
        '    MainClass.ClearGrid SprdMain
        '
        '    SprdMain.Row = 0
        '
        '    SprdMain.Col = ColPartyName
        '    SprdMain.Text = "Party Name"
        '
        '    SprdMain.Col = ColMainProd
        '    SprdMain.Text = "Main Product"
        '
        '    SprdMain.Col = ColProdCode
        '    SprdMain.Text = "Item Code"
        '
        '    SprdMain.Col = ColProdName
        '    SprdMain.Text = "Item Name"
        '
        '    SprdMain.Col = ColStdQty
        '    SprdMain.Text = "No. Off"
        '
        '    SprdMain.Col = ColUnit
        '    SprdMain.Text = "Unit"
        '
        '    Do While Not RsTemp.EOF = True
        '        With SprdMain
        '            .Row = 0
        '            .MaxCols = .MaxCols + 1
        '            .Col = .MaxCols
        '            .Text = mStockQtyStr & VB6.Format(IIf(IsNull(RsTemp!PROCESS_DATE), " ", RsTemp!PROCESS_DATE), "DD-MM-YYYY")
        '
        '            .RowHeight(0) = .MaxTextCellHeight
        '            .Row = -1
        '            .CellType = SS_CELL_TYPE_FLOAT
        '            .TypeFloatDecimalPlaces = 3
        '            .TypeFloatDecimalChar = Asc(".")
        '            .TypeFloatMax = "999999999999999.999"
        '            .TypeFloatMin = "-999999999999999.999"
        '            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
        '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '            .ColWidth(.Col) = 10
        '            If lblBookType.text = Left(ConDespatchPlan, 1) And lblBookSubType.text = Right(ConDespatchPlan, 1) Then
        '                .ColHidden = True
        '            Else
        '                .ColHidden = False
        '            End If
        '
        '            .Row = 0
        '            .MaxCols = .MaxCols + 1
        '            .Col = .MaxCols
        '            .Text = mPlanQtyStr & VB6.Format(IIf(IsNull(RsTemp!PROCESS_DATE), " ", RsTemp!PROCESS_DATE), "DD-MM-YYYY")
        '
        '            .RowHeight(0) = .MaxTextCellHeight
        '            .Row = -1
        '            .CellType = SS_CELL_TYPE_FLOAT
        '            .TypeFloatDecimalPlaces = 3
        '            .TypeFloatDecimalChar = Asc(".")
        '            .TypeFloatMax = "999999999999999.999"
        '            .TypeFloatMin = "-999999999999999.999"
        '            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
        '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '            .ColWidth(.Col) = 10
        '
        '        End With
        '        RsTemp.MoveNext
        '    Loop

    End Sub



    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    If chkFG.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '            mCategoryCode = MasterNo
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '    End If

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            '    If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = False Then
            ErrorMsg("Invalid BOP Item Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblSubCatCode.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillMonthCombo()
        On Error GoTo FillERR
        Dim ii As Integer
        Dim mMonth As Integer

        mMonth = Month(RunDate)

        cboMonth.Items.Clear()
        For ii = 4 To 12
            cboMonth.Items.Add((MonthName(ii, True)))
        Next

        For ii = 1 To 3
            cboMonth.Items.Add((MonthName(ii, True)))
        Next

        If mMonth >= 4 And mMonth <= 12 Then
            cboMonth.SelectedIndex = mMonth - 4
        Else
            cboMonth.SelectedIndex = 8 + mMonth
        End If

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    'Private Sub UpDYear_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDYear.DownClick
    '    txtYear.Text = VB6.Format(Val(txtYear.Text) - 1, "0000")
    'End Sub

    'Private Sub UpDYear_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDYear.UpClick
    '    txtYear.Text = VB6.Format(Val(txtYear.Text) + 1, "0000")
    'End Sub
End Class
