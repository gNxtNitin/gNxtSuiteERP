Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamScrapRecStm
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColQty As Short = 4
    Private Const ColWtPerPc As Short = 5
    Private Const ColNetWt As Short = 6


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mcntRow As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
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


        '    Report1.Reset
        '
        '    mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        '
        '
        '    mTitle = Me.text
        '
        '    Report1.ReportFileName = App.path & "\Reports\DespVsIssueReport.rpt"
        '
        '    If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr
        '
        '    SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
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
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim mOpeningScrap As Double
        Dim mProcessScrap As Double
        Dim mComponentScrap As Double
        Dim mSaleScrap As Double
        Dim mClosingScrap As Double
        Dim mDivisionCode As Double
        Dim mOthScrap As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        TabMain.SelectedIndex = 0

        mDivisionCode = -1
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(sprdProcess, RowHeight)
        MainClass.ClearGrid(SprdSale, RowHeight)
        MainClass.ClearGrid(sprdComponent, RowHeight)
        MainClass.ClearGrid(sprdOthScrap, RowHeight)
        MainClass.ClearGrid(sprdOpening, RowHeight)
        MainClass.ClearGrid(sprdClosing, RowHeight)
        MainClass.ClearGrid(sprdSummary, RowHeight)

        Call Show1("PS", sprdProcess, mDivisionCode)
        Call FormatSprdMain(-1, sprdProcess)

        Call Show1("SS", SprdSale, mDivisionCode)
        Call FormatSprdMain(-1, SprdSale)

        '    Call Show1("OS", sprdOthScrap, mDivisionCode)  ''CR Scrap
        '    Call FormatSprdMain(-1, sprdOthScrap)

        Call Show1("CS", sprdComponent, mDivisionCode)
        Call FormatSprdMain(-1, sprdComponent)

        Call Show1("OP", sprdOpening, mDivisionCode)
        Call FormatSprdMain(-1, sprdOpening)

        Call Show1("CL", sprdClosing, mDivisionCode)
        Call FormatSprdMain(-1, sprdClosing)

        Call CalcTots(sprdOpening)
        Call CalcTots(sprdProcess)
        Call CalcTots(SprdSale)
        Call CalcTots(sprdComponent)
        Call CalcTots(sprdOthScrap)
        Call CalcTots(sprdClosing)

        sprdOpening.Col = ColNetWt
        sprdOpening.Row = sprdOpening.MaxRows
        mOpeningScrap = Val(sprdOpening.Text)

        sprdProcess.Col = ColNetWt
        sprdProcess.Row = sprdProcess.MaxRows
        mProcessScrap = Val(sprdProcess.Text)

        sprdComponent.Col = ColNetWt
        sprdComponent.Row = sprdComponent.MaxRows
        mComponentScrap = Val(sprdComponent.Text)

        sprdOthScrap.Col = ColNetWt
        sprdOthScrap.Row = sprdOthScrap.MaxRows
        mOthScrap = Val(sprdOthScrap.Text)

        SprdSale.Col = ColNetWt
        SprdSale.Row = SprdSale.MaxRows
        mSaleScrap = Val(SprdSale.Text)


        mClosingScrap = mOpeningScrap + mProcessScrap + mComponentScrap + mOthScrap - mSaleScrap

        sprdSummary.Row = sprdSummary.MaxRows
        sprdSummary.Col = 1
        sprdSummary.Text = VB6.Format(mOpeningScrap, "0.00")

        sprdSummary.Col = 2
        sprdSummary.Text = VB6.Format(mProcessScrap, "0.00")

        sprdSummary.Col = 3
        sprdSummary.Text = VB6.Format(mComponentScrap, "0.00")

        sprdSummary.Col = 4
        sprdSummary.Text = VB6.Format(mOthScrap, "0.00")

        sprdSummary.Col = 5
        sprdSummary.Text = VB6.Format(mSaleScrap, "0.00")

        sprdSummary.Col = 6
        sprdSummary.Text = VB6.Format(mClosingScrap, "0.00")


        Call FormatSprdMain(-1, sprdSummary)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Call PrintStatus(True)

        Exit Sub
ErrPart:
        '    PubDBCn.RollbackTrans
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CalcTots(ByRef pSprd As AxFPSpreadADO.AxfpSpread)

        On Error GoTo ERR1
        Dim mNetWt As Double
        Dim cntRow As Integer

        mNetWt = 0

        With pSprd
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColItemCode

                If Trim(.Text) <> "" Then
                    .Col = ColNetWt
                    mNetWt = mNetWt + Val(.Text)
                End If

            Next cntRow
        End With

        MainClass.AddBlankSprdRow(pSprd, ColItemCode, RowHeight)
        Call FormatSprdMain((pSprd.MaxRows), pSprd)
        With pSprd
            .Row = .MaxRows

            .Col = ColItemDesc
            .Text = "TOTAL :"

            .Col = ColNetWt
            .Text = VB6.Format(mNetWt, "0.00")

            MainClass.ProtectCell(pSprd, 0, .MaxRows, 0, .MaxCols)


            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .Font = VB6.FontChangeBold(.Font, True)
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub Show1(ByRef pScrapType As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef mDivisionCode As Double)

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mQty As Double
        Dim mWtPerPc As Double
        Dim mNetWt As Double

        If pScrapType = "PS" Then
            pSqlStr = GetProcessScrapQuery(mDivisionCode)
        ElseIf pScrapType = "OS" Then
            pSqlStr = GetOtherScrapQuery(mDivisionCode)
        ElseIf pScrapType = "SS" Then
            pSqlStr = GetSaleScrapQuery(mDivisionCode)
        ElseIf pScrapType = "CS" Then
            pSqlStr = GetComponentScrapQuery(mDivisionCode)
        ElseIf pScrapType = "OP" Or pScrapType = "CL" Then
            pSqlStr = GetOpeningScrapQuery(mDivisionCode, pScrapType)
        End If

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            i = 1
            Do While RsTemp.EOF = False

                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mItemDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
                mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                '            mUOMFactor = IIf(IsNull(RsTemp!UOM_FACTOR), "", RsTemp!UOM_FACTOR)
                mQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.0000"))
                If mItemUOM = "KGS" Then
                    mWtPerPc = 1
                ElseIf mItemUOM = "TON" Or mItemUOM = "MT" Then
                    mWtPerPc = 1000
                Else
                    mWtPerPc = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_WEIGHT").Value), 0, RsTemp.Fields("ITEM_WEIGHT").Value), "0.000"))
                    mWtPerPc = CDbl(VB6.Format(mWtPerPc / 1000, "0.000"))
                End If


                mNetWt = mQty * mWtPerPc

                If Trim(mItemCode) <> "" Then
                    pSprd.Row = i
                    pSprd.Col = ColItemCode
                    pSprd.Text = mItemCode

                    pSprd.Col = ColItemDesc
                    pSprd.Text = mItemDesc

                    pSprd.Col = ColUnit
                    pSprd.Text = mItemUOM

                    pSprd.Col = ColQty
                    pSprd.Text = VB6.Format(mQty, "0.000")

                    pSprd.Col = ColWtPerPc
                    pSprd.Text = VB6.Format(mWtPerPc, "0.000")

                    pSprd.Col = ColNetWt
                    pSprd.Text = VB6.Format(mNetWt, "0.000")
                End If

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    i = i + 1
                    pSprd.MaxRows = i
                End If
            Loop
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetComponentScrapQuery(ByRef mDivisionCode As Double) As String
        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String


        SqlStr = ""
        GetComponentScrapQuery = ""


        SqlStr = " SELECT IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC, IGD.ITEM_UOM AS ISSUE_UOM, " & vbCrLf & " TO_CHAR(SUM(IGD.RTN_QTY)) AS ITEM_QTY, INVMST.ITEM_WEIGHT" & vbCrLf & " FROM INV_SRN_HDR IGH, INV_SRN_DET IGD, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_SRN=IGD.AUTO_KEY_SRN" & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IGH.STATUS='Y'" & vbCrLf & " AND IGH.BOOKTYPE='P' AND (IGH.BOOKSUBTYPE='S' OR IGH.BOOKSUBTYPE='R')"

        If mDivisionCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND IGH.DIV_CODE=" & mDivisionCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND INVMST.CATEGORY_CODE IN (" & vbCrLf & " SELECT GEN_CODE FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C'" & vbCrLf & " AND (PRD_TYPE IN ('P','R','B','I','J','3') OR ER_ITEM='Y'))"


        SqlStr = SqlStr & vbCrLf & " AND IGH.SRN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.SRN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & "GROUP BY IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM, INVMST.ITEM_WEIGHT "
        SqlStr = SqlStr & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC"


        GetComponentScrapQuery = SqlStr

        Exit Function
LedgError:
        GetComponentScrapQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetSaleScrapQuery(ByRef mDivisionCode As Double) As String
        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String


        SqlStr = ""
        GetSaleScrapQuery = ""

        mTable = ConInventoryTable


        SqlStr = " SELECT STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM AS ISSUE_UOM," & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY, " & vbCrLf & " ITEM.ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " STOCK, INV_ITEM_MST ITEM" & vbCrLf & " WHERE " & vbCrLf & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK.STOCK_ID IN ('" & ConWH & "','" & ConPH & "')" & vbCrLf & " AND STOCK.REF_TYPE IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')" & vbCrLf & " AND STOCK.STOCK_TYPE='SC'" & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf & " AND STATUS='O'"

        If mDivisionCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & mDivisionCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN (" & vbCrLf & " SELECT GEN_CODE FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C'" & vbCrLf & " AND (PRD_TYPE IN ('P','R','B','I','J','3') OR ER_ITEM='Y'))"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, ITEM.ITEM_WEIGHT"

        GetSaleScrapQuery = SqlStr

        Exit Function
LedgError:
        GetSaleScrapQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetScrapQty(ByRef pProductCode As String, ByRef mDateFrom As String, ByRef mDateTo As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetScrapQty = 0

        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConWH & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='I'"
        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE ='SC'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetScrapQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetScrapQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetProcessScrapQuery(ByRef mDivisionCode As Double) As String
        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String


        SqlStr = ""
        GetProcessScrapQuery = ""

        mTable = ConInventoryTable


        SqlStr = " SELECT STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM AS ISSUE_UOM," & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY, " & vbCrLf _
            & " ITEM.ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " STOCK, INV_ITEM_MST ITEM" & vbCrLf _
            & " WHERE " & vbCrLf & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK.STOCK_ID IN ('" & ConWH & "','" & ConPH & "')" & vbCrLf & " AND STOCK.REF_TYPE IN ('" & ConStockRefType_PMEMODEPT & "','" & ConStockRefType_MRR & "','" & ConStockRefType_PMEMO & "','" & ConStockRefType_ADJ & "')" & vbCrLf & " AND STOCK.STOCK_TYPE='SC'" & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf & " AND STATUS='O' "

        If mDivisionCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & mDivisionCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN (" & vbCrLf _
            & " SELECT GEN_CODE FROM INV_GENERAL_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND GEN_TYPE='C'" & vbCrLf _
            & " AND (PRD_TYPE IN ('P','R','B','I','J','3') OR ER_ITEM='Y'))"

        SqlStr = SqlStr & vbCrLf _
            & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, ITEM.ITEM_WEIGHT"

        GetProcessScrapQuery = SqlStr

        Exit Function
LedgError:
        GetProcessScrapQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetOtherScrapQuery(ByRef mDivisionCode As Double) As String
        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String


        SqlStr = ""
        GetOtherScrapQuery = ""

        SqlStr = " SELECT ID.ITEM_CODE, ITEM.ITEM_SHORT_DESC, ID.ITEM_UOM AS ISSUE_UOM," & vbCrLf & " SUM(SCRAP_QTY) AS ITEM_QTY, " & vbCrLf & " ITEM.ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_FGBREAKUP_HDR IH, PRD_FGBREAKUP_DET ID, INV_ITEM_MST ITEM" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND ID.SCRAP_QTY>0" & vbCrLf & " AND IH.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=ITEM.ITEM_CODE "

        If mDivisionCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND ID.DIV_CODE=" & mDivisionCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN (" & vbCrLf & " SELECT GEN_CODE FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C'" & vbCrLf & " AND (PRD_TYPE IN ('P','R','B','I','J','3') OR ER_ITEM='Y'))"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.ITEM_CODE, ITEM.ITEM_SHORT_DESC, ID.ITEM_UOM, ITEM.ITEM_WEIGHT"

        GetOtherScrapQuery = SqlStr

        Exit Function
LedgError:
        GetOtherScrapQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetOpeningScrapQuery(ByRef mDivisionCode As Double, ByRef pType As String) As String
        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String


        SqlStr = ""
        GetOpeningScrapQuery = ""

        mTable = ConInventoryTable

        '& " AND STOCK.REF_TYPE IN ('" & ConStockRefType_PMEMODEPT & "','" & ConStockRefType_MRR & "','" & ConStockRefType_PMEMO & "')"


        SqlStr = " SELECT STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM AS ISSUE_UOM," & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY, " & vbCrLf & " ITEM.ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " STOCK, INV_ITEM_MST ITEM" & vbCrLf & " WHERE " & vbCrLf & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK.STOCK_ID IN ('" & ConWH & "','" & ConPH & "')" & vbCrLf & " AND STOCK.STOCK_TYPE='SC'" & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf & " AND STATUS='O' "

        If mDivisionCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & mDivisionCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN (" & vbCrLf & " SELECT GEN_CODE FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C'" & vbCrLf & " AND (PRD_TYPE IN ('P','R','B','I','J','3') OR ER_ITEM='Y'))"

        If pType = "OP" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE < TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, ITEM.ITEM_WEIGHT"

        GetOpeningScrapQuery = SqlStr

        Exit Function
LedgError:
        GetOpeningScrapQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub frmParamScrapRecStm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Me.text = "Actual vs Issue (Material Wise)"
        TabMain.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamScrapRecStm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

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
    Private Sub frmParamScrapRecStm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdProcess.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        sprdSummary.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        sprdComponent.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        sprdOthScrap.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        SprdSale.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        sprdOpening.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        sprdClosing.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        Frame2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        Frame3.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        Frame8.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        Frame9.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdProcess, -1)
        MainClass.SetSpreadColor(sprdSummary, -1)
        MainClass.SetSpreadColor(sprdComponent, -1)
        MainClass.SetSpreadColor(sprdOthScrap, -1)
        MainClass.SetSpreadColor(SprdSale, -1)
        MainClass.SetSpreadColor(sprdOpening, -1)
        MainClass.SetSpreadColor(sprdClosing, -1)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamScrapRecStm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef pSprd As AxFPSpreadADO.AxfpSpread)

        Dim cntCol As Integer


        If UCase(pSprd.Name) = UCase("sprdSummary") Then
            With sprdSummary
                .MaxCols = 6
                .set_RowHeight(0, RowHeight * 1.5)
                .set_ColWidth(0, 4.5)

                .set_RowHeight(-1, RowHeight)
                .Row = -1

                For cntCol = 1 To 6
                    .Col = cntCol
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalPlaces = 3
                    .TypeFloatMin = CDbl("-99999999999")
                    .TypeFloatMax = CDbl("99999999999")
                    .TypeFloatMoney = False
                    .TypeFloatSeparator = False
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatSepChar = Asc(",")
                    .set_ColWidth(cntCol, 10)
                Next

                MainClass.SetSpreadColor(sprdSummary, -1)
                MainClass.ProtectCell(sprdSummary, 1, .MaxRows, 1, .MaxCols)
                sprdSummary.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
                sprdSummary.DAutoCellTypes = True
                sprdSummary.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
                sprdSummary.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            End With
        Else
            With pSprd
                .MaxCols = ColNetWt
                .set_RowHeight(0, RowHeight * 1.5)
                .set_ColWidth(0, 4.5)

                .set_RowHeight(-1, RowHeight)
                .Row = -1

                .Col = ColItemCode
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(ColItemCode, 7)

                .Col = ColItemDesc
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(ColItemDesc, 35)

                .Col = ColUnit
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(ColUnit, 4)

                For cntCol = ColQty To ColNetWt
                    .Col = cntCol
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalPlaces = 3
                    .TypeFloatMin = CDbl("-99999999999")
                    .TypeFloatMax = CDbl("99999999999")
                    .TypeFloatMoney = False
                    .TypeFloatSeparator = False
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatSepChar = Asc(",")
                    .set_ColWidth(cntCol, 10)
                Next

                MainClass.SetSpreadColor(pSprd, -1)
                MainClass.ProtectCell(pSprd, 1, .MaxRows, 1, .MaxCols)
                pSprd.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
                pSprd.DAutoCellTypes = True
                pSprd.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
                pSprd.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            End With
        End If



    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()


        '    If chkAll.Value = vbUnchecked Then
        '        If Trim(TxtItemName.Text) = "" Then
        '            MsgInformation "Invaild Item Name"
        '            TxtItemName.SetFocus
        '            FieldsVerification = False
        '            Exit Function
        '        End If
        '        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '            MsgInformation "Invaild Item Name"
        '            TxtItemName.SetFocus
        '            FieldsVerification = False
        '            Exit Function
        '        End If
        '    End If



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
End Class
