Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGSTR4
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonST(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mHeading As String

        Report1.Reset()
        mTitle = "FORM GSTR - 1"
        mSubTitle = "(See Rule : )"
        mHeading = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GSTR1.RPT"

        '    SqlStr = MakeSQL
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mHeading)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef pHeading As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "QuarterEnded=""" & UCase(pHeading) & """")
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim mCompanyCode As Integer
        Dim pDateFrom As String
        Dim pDateTo As String
        Dim cntRow As Integer
        Dim mRGPNo As String
        Dim mItemCode As String
        Dim mTaxableValue As Double
        Dim mItemQty As Double
        Dim mItemRate As Double
        Dim mGSTNO As String

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        MainClass.ClearGrid(SprdMain5, RowHeight)


        pDateFrom = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        pDateTo = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
        mGSTNO = Trim(cboGSTNO.Text)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '********************************
        ''4
        SqlStr = ""
        If Show_Detail4(SqlStr, PubDBCnView, mCompanyCode, mGSTNO, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        ''REGEXP_REPLACE(ITEM_SHORT_DESC, '[^0-9A-Za-z()]', ' ')  

        '    ''5
        SqlStr = ""
        If Show_Detail5(SqlStr, PubDBCnView, mCompanyCode, mGSTNO, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain5, StrConn, "Y")

        '********************************

        'With SprdMain5
        '    For cntRow = 1 To .MaxRows
        '        .Row = cntRow
        '        .Col = 4
        '        mRGPNo = Trim(.Text)

        '        .Col = 11
        '        mItemCode = Trim(.Text)

        '        .Col = 14
        '        mItemQty = CDbl(Trim(.Text))

        '        mItemRate = GetTaxableValue(mCompanyCode, mRGPNo, mItemCode)

        '        mTaxableValue = mItemQty * mItemRate

        '        .Col = 15
        '        .Text = VB6.Format(mTaxableValue, "0.00")

        '    Next
        'End With

        Call PrintStatus(True)
        CalcSprdTotal()
        Call FormatSpreadSheet()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default



        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetTaxableValue(ByRef mCompanyCode As Integer, ByRef mRGPNo As String, ByRef mItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetTaxableValue = 0

        SqlStr = "SELECT ID.ITEM_RATE FROM " & vbCrLf _
            & " INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " AND IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO " & vbCrLf _
            & " AND IH.AUTO_KEY_PASSNO=" & Val(mRGPNo) & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & mItemCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetTaxableValue = IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
        End If
        Exit Function
ErrPart:
        GetTaxableValue = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function Show_Detail4(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As Integer, ByRef mGSTNO As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr

        ''regexp_replace('abc+de)fg','\+|\)')
        ''REGEXP_REPLACE('abc+de)fg', '[~!@#$%^&*()_+=\\{}[\]:”;’<,>.\/?]', '') ''""
        ''initcap(EMP_name)

        SqlStr = " SELECT " & vbCrLf _
            & "  DECODE(CMST.GST_REGD,'Y',CMST.GST_RGN_NO,'') AS GSTNO, " & vbCrLf _
            & "  SMST.STATE_CODE || '-' || INITCAP(SMST.NAME) AS SNAME, " & vbCrLf _
            & "  IH.CHALLAN_PREFIX||IH.GATEPASS_NO, TO_CHAR(IH.GATEPASS_DATE,'DD-MM-YYYY') AS GATEPASS_DATE, " & vbCrLf _
            & "  ID.ITEM_CODE, REGEXP_REPLACE(SUBSTR(INVMST.ITEM_SHORT_DESC,1,50), '[^0-9A-Za-z()]', ' ') AS ITEM_SHORT_DESC, ID.ITEM_UOM, " & vbCrLf _
            & "  ID.ITEM_QTY, ID.ITEM_QTY * ID.ITEM_RATE AS AMOUNT, " & vbCrLf _
            & "  DECODE(CATMST.PRD_TYPE,'A','CAPITAL GOODS','INPUTS') AS GOODS_TYPE, " & vbCrLf _
            & "  ID.CGST_PER, ID.SGST_PER, ID.IGST_PER, 0 As CESS"

        ''REGEXP_REPLACE(INVMST.ITEM_SHORT_DESC, '[^0-9A-Za-z()]', ' ')  
        ''TRANSLATE(INVMST.ITEM_SHORT_DESC,'-&_#:/()+*.;!?',' ') 

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST, INV_GENERAL_MST CATMST " & vbCrLf _
            & " WHERE IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND INVMST.COMPANY_CODE=CATMST.COMPANY_CODE " & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=CATMST.GEN_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME AND CATMST.GEN_TYPE='C'"

        SqlStr = SqlStr & vbCrLf _
            & " AND DECODE(CMST.GST_RGN_NO,NULL,'',CMST.GST_RGN_NO)<>DECODE(GMST.COMPANY_GST_RGN_NO,NULL,'',GMST.COMPANY_GST_RGN_NO)"

        'SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_CODE=" & pCompanyCode & ""

        SqlStr = SqlStr & vbCrLf & " And IH.GATEPASS_TYPE='R'"

        SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & mGSTNO & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.PURPOSE NOT IN ('G','H')"

        '    SqlStr = SqlStr & vbCrLf & " AND IH.PURPOSE IN ('B','C','D','E','F')"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.GATEPASS_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.GATEPASS_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        '     SqlStr = SqlStr & vbCrLf _
        ''            & " AND CMST.GST_REGD='Y' " & vbCrLf _
        ''            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
        ''            & " AND IH.REF_DESP_TYPE<>'U' "




        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.GATEPASS_DATE,IH.AUTO_KEY_PASSNO"
        Show_Detail4 = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_Detail4 = False
        '    Resume
    End Function

    Private Function Show_Detail5(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As Integer, ByRef mGSTNO As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr
        'Dim SqlStr As String=""=""
        'Dim RsTemp As ADODB.Recordset = Nothing

        ''ID.BILL_QTY * ID.ITEM_RATE

        '    SqlStr = " SELECT " & vbCrLf _
        ''            & " DECODE(CMST.GST_REGD,'Y',CMST.GST_RGN_NO,SMST.NAME) AS GSTNO, " & vbCrLf _
        ''            & " 'RECEIVED BACK', " & vbCrLf _
        ''            & " ID.REF_AUTO_KEY_NO, GH.GATEPASS_DATE, " & vbCrLf _
        ''            & " '','','', " & vbCrLf _
        ''            & " '' AS BILL_NO, '' AS BILL_DATE,  " & vbCrLf _
        ''            & " ID.RGP_ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, " & vbCrLf _
        ''            & " ID.BILL_QTY, 0 AS TAXBALE"
        '
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_GATEPASS_HDR GH, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf _
        ''            & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=GH.COMPANY_CODE " & vbCrLf _
        ''            & " AND ID.REF_AUTO_KEY_NO=GH.AUTO_KEY_PASSNO " & vbCrLf _
        ''            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
        ''            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
        ''            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
        ''            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
        ''            & " AND CMST.SUPP_CUST_STATE=SMST.NAME AND IH.REF_TYPE='R'"
        '
        '
        '    SqlStr = SqlStr & vbCrLf & " AND GH.PURPOSE IN ('B','C','D','E','F') AND GH.GATEPASS_TYPE='R'"

        ''TO_CHAR(IH.GATEPASS_DATE,'DD-MM-YYYY')

        SqlStr = " SELECT " & vbCrLf _
            & " DECODE(CMST.GST_REGD,'Y',CMST.GST_RGN_NO,'') AS GSTNO, " & vbCrLf _
            & "  SMST.STATE_CODE || '-' || INITCAP(SMST.NAME) AS SNAME, " & vbCrLf _
            & " 'RECEIVED BACK', " & vbCrLf _
            & " TRN.CHALLAN_NO, TO_CHAR(TRN.RGP_DATE,'DD-MM-YYYY') AS SRGP_DATE, " & vbCrLf _
            & " '','','', " & vbCrLf _
            & " BILL_NO AS BILL_NO, TO_CHAR(BILL_DATE,'DD-MM-YYYY') AS BILL_DATE,  " & vbCrLf _
            & " TRN.OUTWARD_ITEM_CODE, REGEXP_REPLACE(SUBSTR(INVMST.ITEM_SHORT_DESC,1,50) , '[^0-9A-Za-z()]', ' ')AS ITEM_SHORT_DESC, INVMST.ISSUE_UOM, " & vbCrLf _
            & " TRN.RGP_QTY, GETRGPRATE (GH.AUTO_KEY_PASSNO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE) * TRN.RGP_QTY AS TAXBALE"

        'REGEXP_REPLACE(INVMST.ITEM_SHORT_DESC, '[^0-9A-Za-z()]', ' ')
        'TRANSLATE(INVMST.ITEM_SHORT_DESC,'-&_#:/()+*;.!?',' ')

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_RGP_REG_TRN TRN, INV_GATEPASS_HDR GH, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & pCompanyCode & " " & vbCrLf _
            & " AND TRN.COMPANY_CODE=GH.COMPANY_CODE " & vbCrLf _
            & " AND TRN.RGP_NO=GH.AUTO_KEY_PASSNO " & vbCrLf _
            & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND TRN.OUTWARD_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND TRN.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME AND TRN.ITEM_IO='I' " ''TRN.BOOKTYPE='M'

        SqlStr = SqlStr & vbCrLf & " AND GH.GATEPASS_TYPE='R'"

        SqlStr = SqlStr & vbCrLf & " AND GH.PURPOSE NOT IN ('G','H')"

        '    SqlStr = SqlStr & vbCrLf & " AND GH.PURPOSE IN ('B','C','D','E','F')"

        SqlStr = SqlStr & vbCrLf & " AND DECODE(CMST.GST_RGN_NO,NULL,'',CMST.GST_RGN_NO)<>DECODE(GMST.COMPANY_GST_RGN_NO,NULL,'',GMST.COMPANY_GST_RGN_NO)"

        'SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_CODE=" & pCompanyCode & ""

        SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & mGSTNO & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND GH.GATEPASS_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.REF_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TRN.REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        '
        '     SqlStr = SqlStr & vbCrLf _
        ''            & " AND CMST.GST_REGD='Y' " & vbCrLf _
        ''            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
        ''            & " AND IH.REF_DESP_TYPE<>'U' "


        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.REF_DATE"

        Show_Detail5 = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_Detail5 = False
        '    Resume
    End Function
    Private Sub FormatSpreadSheet()
        On Error GoTo ErrPart

        FormatSprdMain(-1)
        FormatSprdMain5(-1)


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR4_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Form GST ITC-04"

        FormatSpreadSheet()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR4_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

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

        SqlStr = "SELECT DISTINCT COMPANY_GST_RGN_NO  FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_GST_RGN_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboGSTNO.SelectedIndex = -1
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboGSTNO.Items.Add(Rs.Fields("COMPANY_GST_RGN_NO").Value)
                Rs.MoveNext()
            Loop
            cboGSTNO.SelectedIndex = 0
        End If

        Call PrintStatus(True)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        SSTab1.SelectedIndex = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGSTR4_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Double
        Dim mFrameWidth As Double
        Dim mSSTabWidth As Double
        Dim mSprdMainWidth As Double

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        mFrameWidth = VB6.PixelsToTwipsX(Me.Width) - 2 ''Frame4.Width
        mSSTabWidth = VB6.PixelsToTwipsX(Me.Width) - 220 ''SSTab1.Width
        mSprdMainWidth = VB6.PixelsToTwipsX(Me.Width) - 500 ''SprdMain.Width


        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mFrameWidth, mReFormWidth), 11364.5, 748)
        SSTab1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 220, mSSTabWidth, mReFormWidth))
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain5.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))


        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR4_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = 14
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 15)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 10)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 10)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 10)


            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 15)

            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(7, 10)


            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(9, 10)


            .Col = 10
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(11, 10)


            .Col = 12
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(12, 10)


            .Col = 13
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 11)


            .Col = 14
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(14, 10)


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub



    Private Sub FormatSprdMain5(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain5
            .MaxCols = 15
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 12)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 12)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 12)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 12)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 12)

            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(7, 12)


            .Col = 8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(8, 12)

            .Col = 9
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(9, 12)

            .Col = 10
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(10, 12)

            .Col = 11
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(11, 12)

            .Col = 12
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(12, 15)

            .Col = 13
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(13, 12)

            .Col = 14
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(14, 10)

            .Col = 15
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(15, 10)

            MainClass.SetSpreadColor(SprdMain5, -1)
            MainClass.ProtectCell(SprdMain5, 1, .MaxRows, 1, .MaxCols)
            SprdMain5.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain5.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain5.DAutoCellTypes = True
            SprdMain5.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain5.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mQty As Double
        Dim mItemAmount As Double
        Dim mTaxableAmount As Double
        Dim mIGSTAmount As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = 9
                mTaxableAmount = mTaxableAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, 2)
            .Col = 4
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = 9
            .Text = VB6.Format(mTaxableAmount, "0.00")


        End With

        mTaxableAmount = 0
        mIGSTAmount = 0
        mItemAmount = 0
        mCGSTAmount = 0
        mSGSTAmount = 0

        With SprdMain5
            For cntRow = 1 To .MaxRows
                .Row = cntRow


                .Col = 15
                mTaxableAmount = mTaxableAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain5, 1)
            .Col = 1
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = 15
            .Text = VB6.Format(mTaxableAmount, "0.00")

        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

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
