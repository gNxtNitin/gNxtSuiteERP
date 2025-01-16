Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGSTR2
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

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
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
        mTitle = "FORM GSTR - 2"
        mSubTitle = "(See Rule : )"
        mHeading = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GSTR2.RPT"

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
        Dim mCompanyCode As String

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        MainClass.ClearGrid(SprdMain1, RowHeight)
        MainClass.ClearGrid(SprdMain2, RowHeight)
        MainClass.ClearGrid(SprdMain3, RowHeight)
        MainClass.ClearGrid(SprdMain4, RowHeight)
        MainClass.ClearGrid(SprdMain5, RowHeight)
        MainClass.ClearGrid(SprdMain6, RowHeight)
        MainClass.ClearGrid(SprdMain7, RowHeight)
        MainClass.ClearGrid(SprdMain8, RowHeight)
        MainClass.ClearGrid(SprdMain9, RowHeight)
        MainClass.ClearGrid(SprdMain10, RowHeight)


        If Trim(cboGSTNO.Text) = "" Then
            MsgInformation("Please Select GST No.")
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '********************************
        ''0
        SqlStr = ""
        If B2BQuery(SqlStr) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        ''1
        SqlStr = ""


        'If B2BQuery(SqlStr, 6, ConSalesBookCode, "Y", "N", "Y", ">", 250000, "'P','G','J','S','E'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain1, StrConn, "Y")

        ''2
        SqlStr = ""
        If DNCNQuery(SqlStr) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain2, StrConn, "Y")

        ''3
        SqlStr = ""
        'If B2BQuery(SqlStr, 8, "" & ConSalesBookCode & "", "Y", "", "Y", "", 0, "'U'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData3, StrConn, "Y"

        ''4
        SqlStr = ""
        'If B2BQuery(SqlStr, 8, "" & ConSalesBookCode & "", "Y", "", "Y", "", 0, "'U'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData4, StrConn, "Y"

        ''5
        SqlStr = ""
        'If B2BQuery(SqlStr, 8, "" & ConSalesBookCode & "", "Y", "", "Y", "", 0, "'U'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData5, StrConn, "Y"

        ''6
        SqlStr = ""
        'If B2BQuery(SqlStr, 8, "" & ConSalesBookCode & "", "Y", "", "Y", "", 0, "'U'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData6, StrConn, "Y"

        ''7
        SqlStr = ""
        'If B2BQuery(SqlStr, 8, "" & ConSalesBookCode & "", "Y", "", "Y", "", 0, "'U'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData7, StrConn, "Y"

        ''8
        SqlStr = ""
        'If B2BQuery(SqlStr, 8, "" & ConSalesBookCode & "", "Y", "", "Y", "", 0, "'U'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData8, StrConn, "Y"

        ''9
        SqlStr = ""
        'If B2BQuery(SqlStr, 10, ConSalesBookCode, "", "", "N", "", 0, "'E'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData9, StrConn, "Y"

        ''10
        SqlStr = ""
        'If B2BQuery(SqlStr, 11, ConAdvance, "Y", "", "", "", 0, "'R'") = False Then GoTo ErrPart
        'MainClass.AssignDataInSprd SqlStr, AData10, StrConn, "Y"

        '********************************

        Call PrintStatus(True)
        CalcSprdTotal()
        Call FormatSpreadSheet()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default



        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSpreadSheet()
        On Error GoTo ErrPart

        FormatSprdMain(-1)
        FormatSprdMain1(-1)
        FormatSprdMain2(-1)
        FormatSprdMain3(-1)
        FormatSprdMain4(-1)
        FormatSprdMain5(-1)

        FormatSprdMain6(-1)
        FormatSprdMain7(-1)
        FormatSprdMain8(-1)
        FormatSprdMain9(-1)
        FormatSprdMain10(-1)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR2_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Form GSTR-2 (Inward Supplies)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR2_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim CntLst As Long
        Dim Rs As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7470)
        'Me.Width = VB6.TwipsToPixelsX(11460)


        Call PrintStatus(True)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        SSTab1.SelectedIndex = 0


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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGSTR2_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
        SprdMain1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain3.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain5.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain6.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain7.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain8.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain9.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))

        SprdMain10.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR2_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDatefrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtdateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = 27
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            For cntCol = 1 To 7
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 10)
            Next

            For cntCol = 8 To 9
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next

            For cntCol = 10 To 15
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 10)
            Next

            For cntCol = 16 To 21
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next


            For cntCol = 22 To 22
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 10)
            Next

            For cntCol = 23 To 26
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next

            .Col = 27
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain1(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain1
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain1, -1)
            MainClass.ProtectCell(SprdMain1, 1, .MaxRows, 1, .MaxCols)
            SprdMain1.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain1.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain1.DAutoCellTypes = True
            SprdMain1.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain1.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain2(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain2
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 25)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(4, 8)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 8)

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
            .set_ColWidth(7, 8)

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
            .set_ColWidth(9, 8)

            ''			Differential Value (Plus or Minus)	IGST Rate	IGST Amount	CGST Rate	CGST Amount	SGST Rate	SGST Amount
            For cntCol = 10 To 16
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next



            .Col = 17
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(17, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain2, -1)
            MainClass.ProtectCell(SprdMain2, 1, .MaxRows, 1, .MaxCols)
            SprdMain2.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain2.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain2.DAutoCellTypes = True
            SprdMain2.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain2.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain3(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain3
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain3, -1)
            MainClass.ProtectCell(SprdMain3, 1, .MaxRows, 1, .MaxCols)
            SprdMain3.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain3.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain3.DAutoCellTypes = True
            SprdMain3.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain3.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain4(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain4
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain4, -1)
            MainClass.ProtectCell(SprdMain4, 1, .MaxRows, 1, .MaxCols)
            SprdMain4.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain4.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain4.DAutoCellTypes = True
            SprdMain4.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain4.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain5(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain5
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain5, -1)
            MainClass.ProtectCell(SprdMain5, 1, .MaxRows, 1, .MaxCols)
            SprdMain5.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain5.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain5.DAutoCellTypes = True
            SprdMain5.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain5.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain6(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain6
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain6, -1)
            MainClass.ProtectCell(SprdMain6, 1, .MaxRows, 1, .MaxCols)
            SprdMain6.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain6.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain6.DAutoCellTypes = True
            SprdMain6.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain6.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain7(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain7
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain7, -1)
            MainClass.ProtectCell(SprdMain7, 1, .MaxRows, 1, .MaxCols)
            SprdMain7.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain7.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain7.DAutoCellTypes = True
            SprdMain7.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain7.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain8(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain8
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain8, -1)
            MainClass.ProtectCell(SprdMain8, 1, .MaxRows, 1, .MaxCols)
            SprdMain8.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain8.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain8.DAutoCellTypes = True
            SprdMain8.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain8.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain9(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain9
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain9, -1)
            MainClass.ProtectCell(SprdMain9, 1, .MaxRows, 1, .MaxCols)
            SprdMain9.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain9.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain9.DAutoCellTypes = True
            SprdMain9.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain9.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain10(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain10
            .MaxCols = 16
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
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
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(13, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(14, 12)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 12)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain10, -1)
            MainClass.ProtectCell(SprdMain10, 1, .MaxRows, 1, .MaxCols)
            SprdMain10.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain10.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain10.DAutoCellTypes = True
            SprdMain10.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain10.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function DNCNQuery(ByRef SqlStr As String) As Boolean
        On Error GoTo LedgError


        ''SELECT CLAUSE...
        SqlStr = ""
        ''		CGST Rate	CGST Amount	SGST Rate	SGST Amount	Mkey

        SqlStr = " SELECT NVL(GSTIN,'') AS GSTIN, DEBIT_CREDIT_NOTE_TYPE,NAME_OF_PARTY, VOUCHER_NO, VOUCHER_DATE, " & vbCrLf _
            & " NVL(DEBIT_CREDIT_NOTE_NO,'') AS DEBIT_CREDIT_NOTE_NO," & vbCrLf _
            & " NVL(DEBIT_CREDIT_NOTE_DATE,'') AS DEBIT_CREDIT_NOTE_DATE," & vbCrLf _
            & " NVL(ORIGINAL_INVOICE_NO,'') AS ORIGINAL_INVOICE_NO," & vbCrLf _
            & " NVL(ORIGINAL_INVOICE_DATE,'') AS ORIGINAL_INVOICE_DATE," & vbCrLf _
            & " NVL(DIFF_VALUE,0) AS DIFF_VALUE, " & vbCrLf _
            & " NVL(DIFF_TAX_IGST_RATE,0) AS DIFF_TAX_IGST_RATE, NVL(DIFF_TAX_IGST_AMOUNT,0) AS DIFF_TAX_IGST_AMOUNT," & vbCrLf _
            & " NVL(DIFF_TAX_CGST_RATE,0) AS DIFF_TAX_CGST_RATE, NVL(DIFF_TAX_CGST_AMOUNT,0) AS DIFF_TAX_CGST_AMOUNT," & vbCrLf _
            & " NVL(DIFF_TAX_SGST_RATE,0) AS DIFF_TAX_SGST_RATE, NVL(DIFF_TAX_SGST_AMOUNT,0) AS DIFF_TAX_SGST_AMOUNT," & vbCrLf _
            & " MKEY" & vbCrLf _
            & " FROM ( "


        SqlStr = SqlStr & vbCrLf _
            & " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS GSTIN,  DECODE(IH.BOOKTYPE,'E','Credit','Debit') AS DEBIT_CREDIT_NOTE_TYPE, CMST.SUPP_CUST_NAME AS NAME_OF_PARTY," & vbCrLf _
            & " IH.VNO AS VOUCHER_NO, IH.VDATE AS VOUCHER_DATE," & vbCrLf _
            & " CASE WHEN IH.PARTY_DNCN_NO IS NULL OR IH.PARTY_DNCN_NO='' THEN VNO ELSE IH.PARTY_DNCN_NO END AS DEBIT_CREDIT_NOTE_NO," & vbCrLf _
            & " CASE WHEN IH.PARTY_DNCN_DATE IS NULL OR IH.PARTY_DNCN_DATE='' THEN VDATE ELSE IH.PARTY_DNCN_DATE END AS DEBIT_CREDIT_NOTE_DATE, " & vbCrLf _
            & " ID.SUPP_REF_NO AS ORIGINAL_INVOICE_NO, ID.SUPP_REF_DATE AS ORIGINAL_INVOICE_DATE," & vbCrLf _
            & " ID.ITEM_AMT * DECODE(IH.BOOKTYPE,'E',-1,1) AS DIFF_VALUE," & vbCrLf _
            & " ID.IGST_PER AS DIFF_TAX_IGST_RATE, ID.IGST_AMOUNT * DECODE(IH.BOOKTYPE,'E',-1,1) AS DIFF_TAX_IGST_AMOUNT, " & vbCrLf _
            & " ID.CGST_PER AS DIFF_TAX_CGST_RATE, ID.CGST_AMOUNT * DECODE(IH.BOOKTYPE,'E',-1,1) AS DIFF_TAX_CGST_AMOUNT," & vbCrLf _
            & " ID.SGST_PER AS DIFF_TAX_SGST_RATE, ID.SGST_AMOUNT * DECODE(IH.BOOKTYPE,'E',-1,1) AS DIFF_TAX_SGST_AMOUNT," & vbCrLf _
            & " IH.MKEY"



        '    & " CMST.SUPP_CUST_STATE AS STATE, SMST.STATE_CODE AS POS," & vbCrLf _
        '    & " CASE WHEN IH.PARTY_DNCN_NO IS NULL OR IH.PARTY_DNCN_NO='' THEN VNO ELSE IH.PARTY_DNCN_NO END AS DEBIT_CREDIT_NOTE_NO," & vbCrLf _
        '    & " CASE WHEN IH.PARTY_DNCN_DATE IS NULL OR IH.PARTY_DNCN_DATE='' THEN VDATE ELSE IH.PARTY_DNCN_DATE END AS DEBIT_CREDIT_NOTE_DATE, " & vbCrLf _
        '    & " ID.SUPP_REF_NO AS ORIGINAL_INVOICE_NO, ID.SUPP_REF_DATE AS ORIGINAL_INVOICE_DATE," & vbCrLf _
        '    & " ID.ITEM_AMT AS DIFF_VALUE," & vbCrLf _
        '    & " ID.IGST_PER AS DIFF_TAX_IGST_RATE, ID.IGST_AMOUNT AS DIFF_TAX_IGST_AMOUNT, " & vbCrLf _
        '    & " ID.CGST_PER AS DIFF_TAX_CGST_RATE, ID.CGST_AMOUNT AS DIFF_TAX_CGST_AMOUNT," & vbCrLf _
        '    & " ID.SGST_PER AS DIFF_TAX_SGST_RATE, ID.SGST_AMOUNT AS DIFF_TAX_SGST_AMOUNT," & vbCrLf _
        '    & " 0 AS DIFF_TAX_CESS_RATE, 0 AS DIFF_TAX_CESS_AMOUNT," & vbCrLf _
        '    & " DECODE(IH.DNCNTYPE,'O','Input services','Inputs') AS ELIGIBILITY_FOR_ITC," & vbCrLf _
        '    & " ID.IGST_AMOUNT AS TOTAL_TAX_AVAILABLE_ITC_IGST, ID.CGST_AMOUNT  AS TOTAL_TAX_AVAILABLE_ITC_CGST, ID.SGST_AMOUNT  AS TOTAL_TAX_AVAILABLE_ITC_SGST, 0 AS TOTAL_TAX_AVAILABLE_ITC_CESS," & vbCrLf _
        '    & " " & vbCrLf _
        '    & " 'No' AS REVERSE_CHARGE, " & vbCrLf _
        '    & " '07-Others' AS REASON_FOR_ISSUING, " & vbCrLf _
        '    & " CASE WHEN ID.SUPP_REF_DATE<TO_DATE('2017-07-01','yyyy-MM-dd') THEN 'Y' ELSE 'N' END AS PRE_GST_REGIME," & vbCrLf _
        '    & " IH.VNO AS VOUCHER_NO, IH.VDATE AS VOUCHER_DATE,"

        'SqlStr = SqlStr & vbCrLf _
        '    & " '' AS SUPPLY_TYPE," & vbCrLf _
        '    & " '' AS INELIGIBLE_ITC," & vbCrLf _
        '    & " ID.ITEM_CODE, IH.MKEY, " & vbCrLf _
        '    & " ID.ITEM_DESC, ID.HSNCODE AS HSN_CODE," & vbCrLf _
        '    & " ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_UOM, " & vbCrLf _
        '    & " GMST.COMPANY_CODE, GMST.COMPANY_NAME," & vbCrLf _
        '    & " GMST.COMPANY_GST_RGN_NO AS COMPANY_GSTIN "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_BUSINESS_MST CMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND DECODE(IH.BOOKTYPE,'E',IH.DEBITACCOUNTCODE,IH.CREDITACCOUNTCODE)=CMST.SUPP_CUST_CODE  AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND GMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND GMST.COMPANY_STATE=SMST.NAME " & vbCrLf _
            & " AND APPROVED='Y' AND CANCELLED='N' --AND (IH.PARTY_DNCN_DATE IS NOT NULL OR IH.PARTY_DNCN_DATE<>'')" & vbCrLf _
            & " --AND IH.DNCNTYPE<>'R' " & vbCrLf _
            & " AND IH.ISGSTREFUND IN ('I','G')" & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>NVL(CMST.GST_RGN_NO,'-') AND ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT<>0 "


        SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "' "

        ''SqlStr = SqlStr & vbCrLf & " AND IH.PURCHASESEQTYPE<>2 AND IH.CANCELLED='N'" ''AND IH.REJECTION='N'

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "



        SqlStr = SqlStr & vbCrLf _
            & " UNION ALL "

        SqlStr = SqlStr & vbCrLf _
            & " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS GSTIN,  'Debit' AS DEBIT_CREDIT_NOTE_TYPE, CMST.SUPP_CUST_NAME AS NAME_OF_PARTY," & vbCrLf _
            & " IH.VNO AS VOUCHER_NO, IH.VDATE AS VOUCHER_DATE," & vbCrLf _
            & " IH.BILLNO AS DEBIT_CREDIT_NOTE_NO," & vbCrLf _
            & " IH.INVOICE_DATE AS DEBIT_CREDIT_NOTE_DATE, " & vbCrLf _
            & " ID.BILL_NO AS ORIGINAL_INVOICE_NO, ID.BILLDATE AS ORIGINAL_INVOICE_DATE," & vbCrLf _
            & " ID.AMOUNT AS DIFF_VALUE," & vbCrLf _
            & " ID.IGST_PER AS DIFF_TAX_IGST_RATE, ID.IGST_AMOUNT AS DIFF_TAX_IGST_AMOUNT, " & vbCrLf _
            & " ID.CGST_PER AS DIFF_TAX_CGST_RATE, ID.CGST_AMOUNT AS DIFF_TAX_CGST_AMOUNT," & vbCrLf _
            & " ID.SGST_PER AS DIFF_TAX_SGST_RATE, ID.SGST_AMOUNT AS DIFF_TAX_SGST_AMOUNT," & vbCrLf _
            & " IH.MKEY"

        'SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf _
        '    & " CMST.GST_RGN_NO AS GSTIN, CMST.SUPP_CUST_NAME AS NAME_OF_PARTY," & vbCrLf _
        '    & " CMST.SUPP_CUST_STATE AS STATE, SMST.STATE_CODE AS POS," & vbCrLf _
        '    & " IH.BILLNO AS DEBIT_CREDIT_NOTE_NO, IH.INVOICE_DATE AS DEBIT_CREDIT_NOTE_DATE, " & vbCrLf _
        '    & " ID.BILL_NO AS ORIGINAL_INVOICE_NO, ID.BILLDATE AS ORIGINAL_INVOICE_DATE," & vbCrLf _
        '    & " ID.AMOUNT AS DIFF_VALUE," & vbCrLf _
        '    & " ID.IGST_PER AS DIFF_TAX_IGST_RATE, ID.IGST_AMOUNT AS DIFF_TAX_IGST_AMOUNT, " & vbCrLf _
        '    & " ID.CGST_PER AS DIFF_TAX_CGST_RATE, ID.CGST_AMOUNT AS DIFF_TAX_CGST_AMOUNT," & vbCrLf _
        '    & " ID.SGST_PER AS DIFF_TAX_SGST_RATE, ID.SGST_AMOUNT AS DIFF_TAX_SGST_AMOUNT," & vbCrLf _
        '    & " 0 AS DIFF_TAX_CESS_RATE, 0 AS DIFF_TAX_CESS_AMOUNT," & vbCrLf _
        '    & " 'Inputs' AS ELIGIBILITY_FOR_ITC," & vbCrLf _
        '    & " ID.IGST_AMOUNT AS TOTAL_TAX_AVAILABLE_ITC_IGST, ID.CGST_AMOUNT AS TOTAL_TAX_AVAILABLE_ITC_CGST, ID.SGST_AMOUNT AS TOTAL_TAX_AVAILABLE_ITC_SGST, 0 AS TOTAL_TAX_AVAILABLE_ITC_CESS," & vbCrLf _
        '    & " 'Debit' AS DEBIT_CREDIT_NOTE_TYPE, " & vbCrLf _
        '    & " 'No' AS REVERSE_CHARGE, " & vbCrLf _
        '    & " '07-Others' AS REASON_FOR_ISSUING, " & vbCrLf _
        '    & " CASE WHEN ID.BILLDATE<TO_DATE('2017-07-01','yyyy-MM-dd') THEN 'Y' ELSE 'N' END AS PRE_GST_REGIME," & vbCrLf _
        '    & " IH.VNO ||'-'||TRIM(TO_CHAR(IH.COMPANY_CODE,'00')) AS VOUCHER_NO, IH.VDATE AS VOUCHER_DATE," & vbCrLf _
        '    & " '' AS SUPPLY_TYPE," & vbCrLf _
        '    & " '' AS INELIGIBLE_ITC," & vbCrLf _
        '    & " ID.ITEM_CODE, IH.MKEY, " & vbCrLf _
        '    & " ID.ITEM_DESC, ID.HSNCODE AS HSN_CODE," & vbCrLf _
        '    & " ID.QTY AS ITEM_QTY, ID.RATE AS ITEM_RATE, ID.ITEM_UOM, " & vbCrLf _
        '    & " GMST.COMPANY_CODE, GMST.COMPANY_NAME," & vbCrLf _
        '    & " GMST.COMPANY_GST_RGN_NO AS COMPANY_GSTIN "


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_BUSINESS_MST CMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND GMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND GMST.COMPANY_STATE=SMST.NAME " & vbCrLf _
            & " AND ISFINALPOST='Y' AND CANCELLED='N' AND IH.GST_CLAIM IN ('Y','A') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>NVL(CMST.GST_RGN_NO,'-')  AND ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT<>0"

        SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "' "

        ''SqlStr = SqlStr & vbCrLf & " AND IH.PURCHASESEQTYPE<>2 AND IH.CANCELLED='N'" ''AND IH.REJECTION='N'

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = SqlStr & vbCrLf & ")"

        DNCNQuery = True
        Exit Function
LedgError:
        SqlStr = ""
        DNCNQuery = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function B2BQuery(ByRef SqlStr As String) As Boolean
        On Error GoTo LedgError


        ''SELECT CLAUSE...
        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " IH.GST_CLAIM_NEW_DATE AS financial_period, " & vbCrLf _
            & " CMST.GST_RGN_NO AS counter_party_gstin," & vbCrLf _
            & " CMST.SUPP_CUST_NAME AS supplier_name," & vbCrLf _
            & " IH.BILLNO AS invoice_number, " & vbCrLf _
            & " IH.INVOICE_DATE AS invoice_date, " & vbCrLf _
            & " GST_CLAIM_NEW_NO AS DOC_NO, " & vbCrLf _
            & " GST_CLAIM_NEW_DATE AS DOC_DATE," & vbCrLf _
            & " IH.NETVALUE AS invoice_value, " & vbCrLf _
            & " IH.TOTTAXABLEAMOUNT AS TOT_TAXABLE_VALUE," & vbCrLf _
            & " STATE_CODE AS place_of_supply, " & vbCrLf _
            & " '' AS SUPPLY_TYPE, " & vbCrLf _
            & " 'R' AS invoice_type, " & vbCrLf _
            & " 'N' AS reverse_charge, "

        SqlStr = SqlStr & vbCrLf _
            & " ID.SUBROWNO As item_number, " & vbCrLf _
            & " ID.HSNCODE AS hsn_sac_code, " & vbCrLf _
            & " ID.GSTABLE_AMT AS taxable_value," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate," & vbCrLf _
            & " IGST_AMOUNT AS igst_amount, " & vbCrLf _
            & " CGST_AMOUNT AS cgst_amount, " & vbCrLf _
            & " SGST_AMOUNT AS sgst_amount, " & vbCrLf _
            & " 0 AS cess_amount, " & vbCrLf _
            & " DECODE(IH.PURCHASE_TYPE,'G',DECODE(IH.ISCAPITAL,'Y','cg','ip'),DECODE(IH.PURCHASE_TYPE,'J','is',DECODE(IH.PURCHASE_TYPE,'R','is',DECODE(GST_CREDITAPP,'Y',DECODE(GOODS_SERVICE,'G','ip','is'),'no')))) AS eligibility, " & vbCrLf _
            & " IGST_AMOUNT AS TAX_IGST, " & vbCrLf _
            & " CGST_AMOUNT AS TAX_CGST, " & vbCrLf _
            & " SGST_AMOUNT AS TAX_SGST, " & vbCrLf _
            & " 0 AS TAX_CESS, " & vbCrLf _
            & " IH.MKEY AS MKEY"

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND GMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND GMST.COMPANY_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.ISGSTAPPLICABLE='G'" & vbCrLf _
            & " AND IH.PURCHASE_TYPE IN ('G','J','S','W','R')" & vbCrLf _
            & " AND CMST.GST_REGD='Y' " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' AND IH.GST_CLAIM='Y' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO  "

        'AND IH.PURCHASESEQTYPE=2 AND IH.CANCELLED='N'

        SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "' "

        SqlStr = SqlStr & vbCrLf & " AND IH.PURCHASESEQTYPE<>2 AND IH.CANCELLED='N'" ''AND IH.REJECTION='N'

        SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "



        'SqlStr = SqlStr & vbCrLf & " AND GMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        B2BQuery = True
        Exit Function
LedgError:
        SqlStr = ""
        B2BQuery = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
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

        'Dim mItemAmount As Double
        'Dim mTaxableAmount As Double
        'Dim mIGSTAmount As Double
        'Dim mCGSTAmount As Double
        'Dim mSGSTAmount As Double



        Call MainClass.AddBlankfpSprdRow(SprdMain, 3)
        With SprdMain
            .Col = 3
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .FontBold = True

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            '.BackColor = &H8000000F     ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows
        End With

        '17		19	20	21	22		24	25	26	27

        CalcRowTotal(SprdMain, 16, 1, 16, SprdMain.MaxRows - 1, SprdMain.MaxRows, 16)
        CalcRowTotal(SprdMain, 18, 1, 18, SprdMain.MaxRows - 1, SprdMain.MaxRows, 18)
        CalcRowTotal(SprdMain, 19, 1, 19, SprdMain.MaxRows - 1, SprdMain.MaxRows, 19)
        CalcRowTotal(SprdMain, 20, 1, 20, SprdMain.MaxRows - 1, SprdMain.MaxRows, 20)
        CalcRowTotal(SprdMain, 21, 1, 21, SprdMain.MaxRows - 1, SprdMain.MaxRows, 21)
        CalcRowTotal(SprdMain, 23, 1, 23, SprdMain.MaxRows - 1, SprdMain.MaxRows, 23)
        CalcRowTotal(SprdMain, 24, 1, 24, SprdMain.MaxRows - 1, SprdMain.MaxRows, 24)
        CalcRowTotal(SprdMain, 25, 1, 25, SprdMain.MaxRows - 1, SprdMain.MaxRows, 25)
        CalcRowTotal(SprdMain, 26, 1, 26, SprdMain.MaxRows - 1, SprdMain.MaxRows, 26)

        Call MainClass.AddBlankfpSprdRow(SprdMain2, 3)
        With SprdMain2
            .Col = 3
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .FontBold = True

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            '.BackColor = &H8000000F     ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows
        End With

        '17		19	20	21	22		24	25	26	27

        CalcRowTotal(SprdMain2, 10, 1, 10, SprdMain2.MaxRows - 1, SprdMain2.MaxRows, 10)
        CalcRowTotal(SprdMain2, 12, 1, 12, SprdMain2.MaxRows - 1, SprdMain2.MaxRows, 12)
        CalcRowTotal(SprdMain2, 14, 1, 14, SprdMain2.MaxRows - 1, SprdMain2.MaxRows, 14)
        CalcRowTotal(SprdMain2, 16, 1, 16, SprdMain2.MaxRows - 1, SprdMain2.MaxRows, 16)


        'With SprdMain
        'For cntRow = 1 To .MaxRows
        '    .Row = cntRow

        '    .Col = ColItemAmount
        '    mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

        '    .Col = ColTaxableValue
        '    mTaxableAmount = mTaxableAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

        '    .Col = ColIGSTAmount
        '    mIGSTAmount = mIGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

        '    .Col = ColCGSTAmount
        '    mCGSTAmount = mCGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

        '    .Col = ColSGSTAmount
        '    mSGSTAmount = mSGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

        'Next

        'Call MainClass.AddBlankfpSprdRow(SprdMain, ColBillNo)
        '.Col = ColBillNo
        '.Row = .MaxRows
        '.Text = "GRAND TOTAL :"
        '.FontBold = True

        '.Row = .MaxRows
        '.Row2 = .MaxRows
        '.Col = 1
        '.Col2 = .MaxCols
        '.BlockMode = True
        '.BackColor = &H8000000F     ''&H80FF80
        '.BlockMode = False

        '.Row = .MaxRows

        '.Col = ColItemAmount
        '.Text = Format(mItemAmount, "0.00")

        '.Col = ColTaxableValue
        '.Text = Format(mTaxableAmount, "0.00")

        '.Col = ColIGSTAmount
        '.Text = Format(mIGSTAmount, "0.00")

        '.Col = ColCGSTAmount
        '.Text = Format(mCGSTAmount, "0.00")

        '.Col = ColSGSTAmount
        '.Text = Format(mSGSTAmount, "0.00")


        'End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtdateFrom.Validating
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


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtdateTo.Validating
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

    Private Sub _Lbl_7_Click(sender As Object, e As EventArgs) Handles _Lbl_7.Click

    End Sub

End Class
