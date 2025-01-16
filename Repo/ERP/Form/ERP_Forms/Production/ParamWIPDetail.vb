Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamWIPDetail
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColProductCode As Short = 1
    Private Const ColProductName As Short = 2
    Private Const ColProductUOM As Short = 3
    Private Const ColOP As Short = 4
    Private Const ColIN As Short = 5
    Private Const ColClosing As Short = 6
    Private Const ColDespatch As Short = 7
    Private Const ColRGP As Short = 8
    Private Const ColScrap As Short = 9
    Private Const ColProduction As Short = 10

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ERR1
        Dim I As Integer
        Dim mField As String

        With SprdMain
            .Row = 0

            .Col = 0
            .Text = "S.No."

            .Col = ColProductCode
            .Text = "Product Code"

            .Col = ColProductName
            .Text = "Product Name"

            .Col = ColProductUOM
            .Text = "Product UOM"

            .Col = ColOP
            .Text = "Opening"

            .Col = ColIN
            .Text = "IN"

            .Col = ColClosing
            .Text = "Closing"

            .Col = ColDespatch
            .Text = "Despatch"

            .Col = ColRGP
            .Text = "RGP"

            .Col = ColScrap
            .Text = "Scrap"

            .Col = ColProduction
            .Text = "Production"

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
            .set_RowHeight(0, 1.75 * RowHeight)
            .Row = Arow
            .set_ColWidth(0, 5)

            .Col = ColProductCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColProductCode, 10)

            .Col = ColProductName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColProductName, 25)

            .Col = ColProductUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColProductUOM, 5)

            For I = ColOP To ColProduction
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 8)
            Next

            .ColsFrozen = ColProductUOM

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportForStockOnHand(crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForStockOnHand(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mRPTName As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "")


        mRPTName = "WIPDetail.rpt"
        mTitle = "Detail of Despatch   From : " & VB6.Format(txtFromDate.Text, "DD/MM/YYYY") & " - To : " & VB6.Format(txtToDate.Text, "DD/MM/YYYY")
        mSubTitle = MainClass.AllowSingleQuote(txtItemName.Text)
        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
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
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportForStockOnHand(crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim I As Integer
        Dim mCategoryCode As String = ""
        Dim mCond As String

        FieldsVarification = True

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1() As Boolean

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsFG As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mOpQty As Double
        Dim mPhyOPQty As Double
        Dim mWIPOPQty As Double
        Dim mPhyWIPOPQty As Double
        Dim mPurchaseQty As Double
        Dim mWIPInQty As Double
        Dim mClosingQty As Double
        Dim mPhyClosingQty As Double
        Dim mWIPClosingQty As Double
        Dim mPhyWIPClosingQty As Double
        Dim mCons_Book As Double
        Dim mCons_Phy As Double
        Dim mDespatch As Double
        Dim mRMSales As Double
        Dim mScrap As Double
        Dim mRate As Double
        Dim mConsValue_Book As Double
        Dim mConsValue_Phy As Double
        Dim mPMSalesValue As Double
        Dim mRGP As Double
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim mUOMFactor As Double
        Dim mProduction As Double
        Dim mDataShow As Double
        Dim mProductCode As String = ""
        Dim mProductdesc As String = ""
        Dim mProductUOM As String = ""
        Dim mStdQty As Double
        Dim mDeptCode As String
        Dim mCheckRMCode As String

        Dim mTotStdQty As Double
        Dim mReworkQty As Double
        Dim mAlterCode As String
        Dim mOPReworkQty As Double
        Dim mAdjQty As Double
        Dim mReWorkDone As Double
        Dim mLevel As Integer

        If Trim(LblItemCode.Text) <> "" Then
            '        Do While RsTemp.EOF = False
            mWIPOPQty = 0
            mWIPClosingQty = 0
            mPhyWIPOPQty = 0
            mPhyWIPClosingQty = 0

            mItemCode = Trim(LblItemCode.Text)
            mItemUOM = Trim(lblItemUom.Text)
            I = 1

            Call InsertTempTable(mItemCode)

            '            pSqlStr = " SELECT distinct RM_CODE ,FG_CODE ,STD_QTY,FG_LEVEL,DEPT_CODE " & vbCrLf _
            ''                    & " From TEMP_DESPVSISSUE " & vbCrLf _
            ''                    & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf _
            ''                    & " START WITH RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            ''                    & " CONNECT BY PRIOR FG_CODE=RM_CODE AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            '
            pSqlStr = " SELECT DISTINCT FG_CODE,DEPT_CODE " & vbCrLf & " From TEMP_DESPVSISSUE " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf & " START WITH RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR FG_CODE=RM_CODE AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFG, ADODB.LockTypeEnum.adLockReadOnly)

            If RsFG.EOF = False Then
                Do While RsFG.EOF = False
                    mWIPOPQty = 0
                    mWIPInQty = 0
                    mWIPClosingQty = 0
                    mDespatch = 0
                    mProduction = 0
                    mRGP = 0
                    mReworkQty = 0
                    mAlterCode = ""
                    mOPReworkQty = 0
                    mAdjQty = 0
                    mReWorkDone = 0
                    mStdQty = 1
                    mTotStdQty = 0
                    mScrap = 0
                    mProductCode = Trim(IIf(IsDbNull(RsFG.Fields("FG_CODE").Value), "", RsFG.Fields("FG_CODE").Value))
                    mDeptCode = Trim(IIf(IsDbNull(RsFG.Fields("DEPT_CODE").Value), "", RsFG.Fields("DEPT_CODE").Value))
                    pSqlStr = " SELECT RM_CODE ,FG_CODE ,STD_QTY,LEVEL,DEPT_CODE,FG_LEVEL " & vbCrLf & " From TEMP_DESPVSISSUE " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf & " START WITH FG_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR RM_CODE=FG_CODE AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


                    If RsTemp.EOF = False Then
                        Do While RsTemp.EOF = False
                            mLevel = CInt(Trim(IIf(IsDbNull(RsTemp.Fields("Level").Value), "", RsTemp.Fields("Level").Value)))
                            mCheckRMCode = Trim(IIf(IsDbNull(RsTemp.Fields("RM_CODE").Value), "", RsTemp.Fields("RM_CODE").Value))
                            mStdQty = mStdQty * Val(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value))
                            If mCheckRMCode = Trim(mItemCode) Then
                                mTotStdQty = mTotStdQty + mStdQty
                                mStdQty = 1
                            End If
                            '                            mDeptCode = Trim(IIf(IsNull(RsTemp!DEPT_CODE), "", RsTemp!DEPT_CODE))

                            RsTemp.MoveNext()
                        Loop

                        If GetItemWIP(mProductCode, mWIPOPQty, mWIPInQty, mWIPClosingQty, mReworkQty, mAlterCode, mOPReworkQty, mAdjQty, mDespatch, mRGP, mScrap, mReWorkDone, mTotStdQty, mDeptCode, mProduction, mLevel) = False Then GoTo ErrPart


                    End If

                    '                    mProduction = GetItemDespatch(mItemCode, "P")

                    SprdMain.Row = I
                    SprdMain.Col = ColProductCode
                    SprdMain.Text = mProductCode ''IIf(IsNull(RsTemp!FG_CODE), "", RsTemp!FG_CODE)


                    SprdMain.Col = ColProductName
                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mProductdesc = MasterNo
                    End If
                    SprdMain.Text = mProductdesc

                    SprdMain.Col = ColProductUOM
                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mProductUOM = MasterNo
                    End If
                    SprdMain.Text = mProductUOM
                    '                    mScrap = GetScrapQty(mProductCode, txtFromDate.Text, txtToDate.Text)

                    SprdMain.Col = ColOP
                    SprdMain.Text = VB6.Format(mWIPOPQty, "0.000")

                    SprdMain.Col = ColIN
                    SprdMain.Text = VB6.Format(mWIPInQty, "0.000")

                    SprdMain.Col = ColClosing
                    SprdMain.Text = VB6.Format(mWIPClosingQty, "0.000")

                    SprdMain.Col = ColDespatch
                    SprdMain.Text = VB6.Format(mDespatch, "0.000")

                    SprdMain.Col = ColRGP
                    SprdMain.Text = VB6.Format(mRGP, "0.000")

                    SprdMain.Col = ColScrap
                    SprdMain.Text = VB6.Format(mScrap, "0.000")

                    SprdMain.Col = ColProduction
                    SprdMain.Text = VB6.Format(mProduction, "0.000")

                    RsFG.MoveNext()
                    If RsFG.EOF = False Then
                        I = I + 1
                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                    End If
                Loop
            End If

            ''170902009
            '            mProduction = GetItemDespatch(mItemCode, "P")      ''GetProductionQty(mItemCode)      ''
            '            mReworkDoneQty = GetReworkQty(mItemCode)
            '            mRMSales = GetDespatchQty(mItemCode, True, "D")
            '            mRGP = mRGP + GetDespatchQty(mItemCode, True, "R")
            '
            '            mScrap = mScrap + GetScrapQty(mItemCode, txtDateFrom.Text, txtDateTo.Text, "SC")
            '
            '            mReWorkDone = mReWorkDone + GetReWorkDoneQty(mItemCode, txtDateFrom.Text, txtDateTo.Text)

        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetItemDespatch(ByRef mItemCode As String, ByRef mIsDespatch As String, Optional ByRef xIsSPDChecked As String = "") As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mDeptCode As String
        Dim mProductdesc As String = ""
        Dim mStdQty As Double
        Dim mDespatchQty As Double
        Dim mFGScrap As Double
        Dim mTotDespatchQty As Double
        Dim mTotFGScrap As Double
        Dim mCheckRMCode As String
        Dim mString As String = ""
        Dim mLevel As Double

        mTotDespatchQty = 0
        mTotFGScrap = 0

        SqlStr = ""

        SqlStr = " SELECT RM_CODE ,FG_CODE ,STD_QTY,FG_LEVEL,DEPT_CODE " & vbCrLf & " From TEMP_DESPVSISSUE " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf & " START WITH RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR FG_CODE=RM_CODE AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" '' & vbCrLf |
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsRM.EOF = False Then
            Do While RsRM.EOF = False
                mCheckRMCode = Trim(IIf(IsDbNull(RsRM.Fields("RM_CODE").Value), "", RsRM.Fields("RM_CODE").Value))
                If Trim(mCheckRMCode) = Trim(mItemCode) Then
                    mStdQty = 1
                End If
                mProductCode = Trim(IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value))
                mLevel = CDbl(Trim(IIf(IsDbNull(RsRM.Fields("FG_LEVEL").Value), 0, RsRM.Fields("FG_LEVEL").Value)))
                mDeptCode = IIf(IsDbNull(RsRM.Fields("DEPT_CODE").Value), "", RsRM.Fields("DEPT_CODE").Value)
                mDespatchQty = 0
                mFGScrap = 0

                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mProductdesc = MasterNo
                End If
                mStdQty = mStdQty * IIf(IsDbNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)

                If InStr(1, mString, mProductCode) = 0 Then
                    If mIsDespatch = "D" Then
                        mDespatchQty = GetDespatchQty(mProductCode, False, "D", IIf(xIsSPDChecked = "Y", IIf(mDeptCode = "ASY", "N", ""), ""))
                    ElseIf mIsDespatch = "R" Then
                        mDespatchQty = GetDespatchQty(mProductCode, False, "R")
                    ElseIf mIsDespatch = "S" Then
                        mDespatchQty = GetScrapQty(mProductCode, (txtFromDate.Text), (txtToDate.Text), "SC")
                    ElseIf mIsDespatch = "RJ" Then
                        mDespatchQty = GetScrapQty(mProductCode, (txtFromDate.Text), (txtToDate.Text), "RJ")
                    ElseIf mIsDespatch = "WR" Then
                        mDespatchQty = GetReWorkDoneQty(mProductCode, (txtFromDate.Text), (txtToDate.Text))
                    Else
                        If mLevel = 1 Then
                            mDespatchQty = GetProductionQtyOld(mProductCode, mDeptCode, IIf(mDeptCode = "ASY", "N", ""))
                        Else
                            mDespatchQty = 0
                        End If
                    End If
                End If
                mTotDespatchQty = mTotDespatchQty + (mDespatchQty * mStdQty)
                mString = mString & "," & mProductCode
                RsRM.MoveNext()

            Loop
        End If
        GetItemDespatch = mTotDespatchQty
        Exit Function
LedgError:
        GetItemDespatch = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetScrapQty(ByRef pProductCode As String, ByRef mDateFrom As String, ByRef mDateTo As String, ByRef pStockType As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetScrapQty = 0

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConWH & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='I'"
        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE ='" & pStockType & "'"
        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetScrapQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetScrapQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Public Sub frmParamWIPDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call Show1()
        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub


    Private Sub frmParamWIPDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(9450)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        MainClass.SetControlsColor(Me)

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamWIPDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub

    Private Function InsertTempTable(ByRef mItemCode As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim xItemCode As String = ""
        Dim xSTDQty As Double
        Dim mLevel As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        ''(STD_QTY + GROSS_WT_SCRAP)

        SqlStr = " DELETE FROM TEMP_DESPVSISSUE WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "', RM_CODE, PRODUCT_CODE,  STD_QTY, DEPT_CODE,LEVEL " & vbCrLf & " From VW_PRD_BOM_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STATUS='O' " & vbCrLf & " START WITH RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'" & vbCrLf & " CONNECT BY  (TRIM(RM_CODE) || COMPANY_CODE || ' ')=PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ') AND STATUS='O'"

        'RM_CODE= PRIOR PRODUCT_CODE AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND STATUS='O'

        '    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf _
        ''            & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''            & " '" & mItemCode & "', ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) ,ID.DEPT_CODE, 1 " & vbCrLf _
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''            & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"
        '
        '    PubDBCn.Execute SqlStr
        '
        '    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf _
        ''            & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''            & " '" & mItemCode & "',ID.ITEM_CODE, IH.PRODUCT_CODE, (ITEM_QTY + SCRAP_QTY),'J/W',  1 " & vbCrLf _
        ''            & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O' AND IS_INHOUSE='N'"       ''
        '
        '    PubDBCn.Execute SqlStr
        '
        '    mLevel = 1
        '
        '    For mLevel = 1 To 5
        '        SqlStr = " SELECT *  FROM TEMP_DESPVSISSUE " & vbCrLf _
        ''                & " WHERE FG_LEVEL=" & mLevel & " AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsRM, adLockReadOnly
        '
        '        If RsRM.EOF = False Then
        '            Do While Not RsRM.EOF
        '                xItemCode = IIf(IsNull(RsRM!FG_CODE), "", RsRM!FG_CODE)
        '                xSTDQty = 1 'IIf(IsNull(RsRM!STD_QTY), 0, RsRM!STD_QTY)
        '
        '                SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf _
        ''                        & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                        & " '" & mItemCode & "',ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) * " & xSTDQty & ",ID.DEPT_CODE,  " & mLevel + 1 & " " & vbCrLf _
        ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                        & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''                        & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' AND STATUS='O'"     ''& vbCrLf _
        ''                        & " AND IH.WEF=("
        '
        ''                SqlStr = SqlStr & vbCrLf & " SELECT MAX(WEF) AS WEF" & vbCrLf _
        '                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
        '                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        '                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        '                        & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
        '                        & " AND WEF<='" & VB6.Format(txttoDate.Text, "DD-MMM-YYYY") & "')"
        ''
        '
        '                PubDBCn.Execute SqlStr
        '
        '                SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf _
        ''                        & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                        & " '" & mItemCode & "',ID.ITEM_CODE, IH.PRODUCT_CODE, (ITEM_QTY + SCRAP_QTY),'J/W',  " & mLevel + 1 & " " & vbCrLf _
        ''                        & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                        & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''                        & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' AND STATUS='O' AND IS_INHOUSE='N'"       '' AND STATUS='O'
        '
        '                PubDBCn.Execute SqlStr
        '
        '                RsRM.MoveNext
        '
        '            Loop
        '        End If
        '    Next

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Exit Function
LedgError:
        '    Resume
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetItemWIPOld(ByRef mProductCode As String, ByRef mStdQty As Double, ByRef mDeptCode As String, ByRef mWIPOPQty As Double, ByRef mWIPInQty As Double, ByRef mWIPCLQty As Double, ByRef mDespatchQty As Double, ByRef mProductionQty As Double, ByRef mRGP As Double) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim RsTempRel As ADODB.Recordset = Nothing

        Dim mItemUOM As String = ""
        Dim mCheckRMCode As String
        Dim mString As String = ""

        mWIPOPQty = 0
        mWIPInQty = 0
        mWIPCLQty = 0
        mDespatchQty = 0
        mProductionQty = 0
        mRGP = 0
        SqlStr = " SELECT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

        SqlStr = SqlStr & " UNION " & " SELECT '" & mProductCode & "' FROM DUAL"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempRel, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempRel.EOF = False Then
            Do While RsTempRel.EOF = False
                mProductCode = IIf(IsDbNull(RsTempRel.Fields("REF_ITEM_CODE").Value), "", RsTempRel.Fields("REF_ITEM_CODE").Value)

                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If

                mWIPOPQty = mWIPOPQty + (GetBalanceStockQtyRefDate(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mItemUOM, "", "'FG','ST','QC','CR'", "", ConWH) * mStdQty)
                mWIPCLQty = mWIPCLQty + (GetBalanceStockQtyRefDate(mProductCode, (txtToDate.Text), mItemUOM, "", "'FG','ST','QC','CR'", "", ConWH) * mStdQty)


                If mDeptCode = "J/W" Then
                    mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mItemUOM, "", "ST", "", ConPH, -1) * mStdQty)
                    mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtToDate.Text), mItemUOM, "", "ST", "", ConPH, -1) * mStdQty)
                Else
                    mWIPOPQty = mWIPOPQty + (GetWIPQty(mProductCode, ConPH, mDeptCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text)))) * mStdQty)
                    mWIPCLQty = mWIPCLQty + (GetWIPQty(mProductCode, ConPH, mDeptCode, (txtToDate.Text)) * mStdQty)
                End If

                mWIPInQty = mWIPInQty + (GetPurchaseQty(mProductCode, mItemUOM) * mStdQty)
                mDespatchQty = mDespatchQty + (GetDespatchQty(mProductCode, False, "D") * mStdQty)
                mRGP = mRGP + (GetDespatchQty(mProductCode, False, "R") * mStdQty)
                mProductionQty = mProductionQty + (GetProductionQtyOld(mProductCode, mDeptCode) * mStdQty)
                RsTempRel.MoveNext()
            Loop
        End If
        GetItemWIPOld = True
        Exit Function
LedgError:
        GetItemWIPOld = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetItemWIP(ByRef mProductCode As String, ByRef mWIPOPQty As Double, ByRef mWIPInQty As Double, ByRef mWIPCLQty As Double, ByRef mReworkQty As Double, ByRef mAlterCode As String, ByRef mOPReworkQty As Double, ByRef mAdjQty As Double, ByRef mDespatch As Double, ByRef mRGP As Double, ByRef mScrap As Double, ByRef mReWorkDone As Double, ByRef mTotStdQty As Double, ByRef mDeptCode As String, ByRef mProduction As Double, ByRef mLevel As Integer) As Boolean
        On Error GoTo LedgError
        'Dim SqlStr As String = ""
        'Dim RsTempRel As ADODB.Recordset=Nothing
        'Dim RsFG As ADODB.Recordset=Nothing
        'Dim RsRM As ADODB.Recordset=Nothing
        'Dim RsSeq As ADODB.Recordset=Nothing
        'Dim mProductCode As String = ""
        'Dim mDeptCode As String
        Dim mItemUOM As String = ""
        'Dim mCheckRMCode As String
        'Dim mString As String = ""
        'Dim mRMCode As String
        'Dim mLevel As Long
        'Dim CheckLevel As Long
        'Dim mTotStdQty As Double


        If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemUOM = MasterNo
        End If

        mWIPOPQty = mWIPOPQty + (GetBalanceStockQtyRefDate(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mItemUOM, "", "'FG','ST','QC','CR'", "", ConWH) * mTotStdQty)
        mWIPCLQty = mWIPCLQty + (GetBalanceStockQtyRefDate(mProductCode, (txtToDate.Text), mItemUOM, "", "'FG','ST','QC','CR'", "", ConWH) * mTotStdQty)

        If mDeptCode = "J/W" Then
            mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mItemUOM, "", "ST", "", ConPH, -1) * mTotStdQty)
            mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtToDate.Text), mItemUOM, "", "ST", "", ConPH, -1) * mTotStdQty)
        Else
            mWIPOPQty = mWIPOPQty + (GetWIPQty(mProductCode, ConPH, mDeptCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text)))) * mTotStdQty)
            mWIPCLQty = mWIPCLQty + (GetWIPQty(mProductCode, ConPH, mDeptCode, (txtToDate.Text)) * mTotStdQty)
        End If

        mOPReworkQty = mOPReworkQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mItemUOM, "", "WR", "", ConPH, -1) * mTotStdQty)
        mReworkQty = mReworkQty + (GetBalanceStockQty(mProductCode, (txtToDate.Text), mItemUOM, "", "WR", "", ConPH, -1) * mTotStdQty)

        '                mAdjQty = mAdjQty + (GetPeriodStockQty(mProductCode, txtFromDate.Text, txtToDate.Text, mItemUOM, "", "", ConStockRefType_ADJ) * mTotStdQty)

        mWIPInQty = mWIPInQty + (GetPurchaseQty(mProductCode, mItemUOM) * mTotStdQty)

        '                mWIPPhyOPQty = mWIPPhyOPQty + (GetPhysicalWIPQty(mProductCode, ConPH, mDeptCode, DateAdd("d", -1, txtFromDate.Text)) * mTotStdQty)
        '                mWIPPhyCLQty = mWIPPhyCLQty + (GetPhysicalWIPQty(mProductCode, ConPH, mDeptCode, txtToDate.Text) * mTotStdQty)
        mDespatch = mDespatch + (GetDespatchQty(mProductCode, False, "D", "N") * mTotStdQty)
        mRGP = mRGP + (GetDespatchQty(mProductCode, False, "R", "N") * mTotStdQty)
        mScrap = mScrap + (GetScrapQty(mProductCode, (txtFromDate.Text), (txtToDate.Text), "SC") * mTotStdQty)
        mReWorkDone = mReWorkDone + (GetReWorkDoneQty(mProductCode, (txtFromDate.Text), (txtToDate.Text)) * mTotStdQty)
        If mLevel = 1 Then
            mProduction = mProduction + (GetProductionQtyOld(mProductCode, mDeptCode, "N") * mTotStdQty)
        End If

        GetItemWIP = True
        Exit Function
LedgError:
        GetItemWIP = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetReWorkDoneQty(ByRef pProductCode As String, ByRef mDateFrom As String, ByRef mDateTo As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetReWorkDoneQty = 0


        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    Else
        mTable = "PRD_REWORK_HDR"
        '    End If

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(REWORK_QTY) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND PRODUCT_CODE IN " & mSameItemCode & ""

        SqlStr = SqlStr & vbCrLf & " AND PROD_DATE >= TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND PROD_DATE <= TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetReWorkDoneQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetReWorkDoneQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Private Function GetProductionQtyOld(ByRef pProductCode As String, ByRef pDeptCode As String, Optional ByRef xIsSPD As String = "") As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetProductionQtyOld = 0

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        '    mSameItemCode = GetSameItemCode(pProductCode, xIsSPD)
        '    If mSameItemCode = "" Then
        mSameItemCode = "('" & pProductCode & "')"
        '    Else
        '        mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        '    End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,0)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & "" ''& vbCrLf |            & " AND "


        '    SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='I'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE = 'ST'"

        If pDeptCode = "J/W" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConWH & "'"
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_MRR & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_PMEMODEPT & "')"
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_FROM='" & pDeptCode & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQtyOld = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetProductionQtyOld = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetDespatchQty(ByRef pProductCode As String, Optional ByRef mISRMSale As Boolean = False, Optional ByRef pRefType As String = "", Optional ByRef pIsSPDItem As String = "") As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetDespatchQty = 0

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        mSameItemCode = GetSameItemCode(pProductCode, pIsSPDItem)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConWH & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='O'"

        '    If mISRMSale = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('CR')"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('RJ','CR')"
        '    End If

        If pRefType = "" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')"
        ElseIf pRefType = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_DSP & "')"
        ElseIf pRefType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')"
        End If
        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDespatchQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetDespatchQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetPurchaseQty(ByRef pItemCode As String, ByRef pPackUnit As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String


        SqlStr = " SELECT SUM(ID.APPROVED_QTY) AS APPROVED_QTY" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            If IsDbNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If

        RsBalStock = Nothing

        If mBalQty <> 0 Then
            RsTemp = Nothing

            SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                If pPackUnit = mIssueUOM Then
                    mBalQty = mBalQty * mFactor
                End If

                RsTemp = Nothing
                '            RsTemp.Close
            End If
        End If

        GetPurchaseQty = mBalQty

        Exit Function
ErrPart:
        GetPurchaseQty = 0
    End Function

    Private Function GetWIPQty(ByRef pProductCode As String, ByRef mStockID As String, ByRef mDeptCode As String, ByRef pDate As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsDeptSeq As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer

        Dim xCheckDept As String

        Dim mTable As String
        Dim mCheckSeq As Integer
        GetWIPQty = 0
        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        SqlStr = "SELECT SERIAL_NO FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "' ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptSeq, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDeptSeq.EOF = False Then
            mDeptSeq = IIf(IsDbNull(RsDeptSeq.Fields("SERIAL_NO").Value), 0, RsDeptSeq.Fields("SERIAL_NO").Value)
        Else
            GetWIPQty = 0
            Exit Function
        End If

        SqlStr = "SELECT * FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND SERIAL_NO >=" & mDeptSeq & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptSeq, ADODB.LockTypeEnum.adLockReadOnly)

        mCheckSeq = 1
        If RsDeptSeq.EOF = False Then
            Do While RsDeptSeq.EOF = False
                xCheckDept = IIf(IsDbNull(RsDeptSeq.Fields("DEPT_CODE").Value), "", RsDeptSeq.Fields("DEPT_CODE").Value)

                SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID = '" & ConPH & "'" & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pProductCode) & "'"

                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

                '                If Trim(xCheckDept) = Trim(mDeptCode) Then
                SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & xCheckDept & "'"
                '                Else
                '                    SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='-1'"
                '                End If

                '                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE = '" & ConStockRefType_PMEMODEPT & "'"
                If mCheckSeq = 1 Then
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE<>'WP'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE <>'WR'"

                SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                '                SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= '" & VB6.Format(pToDate, "DD-MMM-YYYY") & "'"


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    GetWIPQty = GetWIPQty + IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                End If
                mCheckSeq = mCheckSeq + 1
                RsDeptSeq.MoveNext()
            Loop
            '        If mIsOpening <> "P" Then
            '            GetWIPQty = GetWIPQty + GetOtherDeptWIP(pProductCode, mIsOpening, mTable)
            '        End If
        End If
        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
