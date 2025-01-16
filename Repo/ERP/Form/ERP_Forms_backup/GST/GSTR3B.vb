Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTR3B
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection

    Private Const RowHeight As Short = 20

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Const mPageWidth As Short = 150
    Private Const mDelimited As String = ","


    Private Sub PrintStatus(ByRef pPrintEnable As Boolean)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification = False Then Exit Sub

        Call PrintStatus(False)
        Call Clear1()
        Show1()
        FormatSprdView()

        Call PrintStatus(True)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mCompanyCode As String

        FieldsVerification = True
        If txtDateFrom.Text = "" Then
            MsgBox("From Date is Blank", MsgBoxStyle.Information)
            FieldsVerification = False
            txtDateFrom.Focus()
            Exit Function
        ElseIf MainClass.ChkIsdateF(txtDateFrom) = False Then
            FieldsVerification = False
            txtDateFrom.Focus()
            Exit Function
        End If
        If txtDateTo.Text = "" Then
            MsgBox("To Date is Blank", MsgBoxStyle.Information)
            FieldsVerification = False
            txtDateTo.Focus()
            Exit Function
        ElseIf MainClass.ChkIsdateF(txtDateTo) = False Then
            FieldsVerification = False
            txtDateTo.Focus()
            Exit Function
        End If

        mCompanyCode = Trim(cboGSTNO.Text)

        If mCompanyCode = "" Then
            MsgInformation("Please Select GST No.")
            FieldsVerification = False
            Exit Function
        End If

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
    Public Sub frmGSTR3B_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call PrintStatus(False)
        lblTile.ForeColor = System.Drawing.Color.Blue
        FormatSprdView()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmGSTR3B_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim mStartDate As String
        Dim mLastDate As String
        Dim Rs As ADODB.Recordset
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        SSTab1.SelectedIndex = 0

        lblTile.Text = "FORM GSTR - 3B" & vbNewLine & "[See rule 61(5)]"

        mStartDate = "01" & "/" & Month(RunDate) & "/" & Year(RunDate)
        txtDateFrom.Text = VB6.Format(mStartDate, "DD/MM/YYYY")
        mLastDate = MainClass.LastDay(Month(RunDate), Year(RunDate)) & "/" & Month(RunDate) & "/" & Year(RunDate)
        txtDateTo.Text = VB6.Format(mLastDate, "DD/MM/YYYY")

        ''WHERE COMPANY_GST_RGN_NO='" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'

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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If ShowDetail1 = False Then GoTo ErrPart
        If ShowDetail3 = False Then GoTo ErrPart
        If ShowDetail4 = False Then GoTo ErrPart
        If ShowDetail5 = False Then GoTo ErrPart
        If ShowDetail6 = False Then GoTo ErrPart
        If ShowDetail7 = False Then GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Function ShowDetail4() As Boolean
        On Error GoTo ErrPart1
        Dim mCntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pRowNo As Integer

        'Dim mAgtCT3 As String
        ShowDetail4 = False
        SprdView4.MaxRows = 1

        pRowNo = 1
        If InsertDataIntoSprdView4(CStr(6), pRowNo, "Supplies made to unregistered Persons") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView4(CStr(7), pRowNo, "Supplies made to Composition Taxable Persons") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView4(CStr(8), pRowNo, "Supplies made to UIN Holders") = False Then GoTo ErrPart1

        SprdView4.Row = SprdView4.MaxRows
        SprdView4.Row2 = SprdView4.MaxRows
        SprdView4.Col = 1
        SprdView4.col2 = SprdView4.MaxCols
        SprdView4.BlockMode = True
        SprdView4.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
        SprdView4.Font = VB6.FontChangeBold(SprdView4.Font, False)
        SprdView4.BlockMode = False


        Call CalcRowTotal(SprdView4, 3, 1, 3, SprdView4.MaxRows - 1, (SprdView4.MaxRows), 3)
        Call CalcRowTotal(SprdView4, 4, 1, 4, SprdView4.MaxRows - 1, (SprdView4.MaxRows), 4)

        ShowDetail4 = True
        Exit Function
ErrPart1:
        ShowDetail4 = False
    End Function

    Private Sub FormatSprdView()
        Call FormatSprdView3()
        Call FormatSprdView4()
        Call FormatSprdView5()
        Call FormatSprdView6()
        Call FormatSprdView7()
        Call FormatSprdView8()

    End Sub
    Private Sub FormatSprdView3()

        Dim I As Integer
        With SprdView3
            .MaxCols = 6

            .set_RowHeight(0, RowHeight * 3.5)
            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 40)

            For I = 2 To 6
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 10)
            Next

            FillHeadingSprdView3()
            MainClass.SetSpreadColor(SprdView3, -1)
            MainClass.ProtectCell(SprdView3, 1, .MaxRows, 1, .MaxCols)
            SprdView3.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdView3.DAutoCellTypes = True
            SprdView3.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdView3.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        End With
    End Sub
    Private Sub FormatSprdView4()

        Dim I As Integer
        With SprdView4
            .MaxCols = 4

            .set_RowHeight(0, RowHeight * 3.5)
            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 50)

            For I = 2 To 4
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 10)
            Next

            FillHeadingSprdView4()
            MainClass.SetSpreadColor(SprdView4, -1)
            MainClass.ProtectCell(SprdView4, 1, .MaxRows, 1, .MaxCols)
            SprdView4.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdView4.DAutoCellTypes = True
            SprdView4.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdView4.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        End With
    End Sub
    Private Sub FormatSprdView5()

        Dim I As Integer
        With SprdView5
            .MaxCols = 5

            .set_RowHeight(0, RowHeight * 3.5)
            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 45)

            For I = 2 To 5
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 10)
            Next

            FillHeadingSprdView5()
            MainClass.SetSpreadColor(SprdView5, -1)
            MainClass.ProtectCell(SprdView5, 1, .MaxRows, 1, .MaxCols)
            SprdView5.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdView5.DAutoCellTypes = True
            SprdView5.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdView5.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        End With
    End Sub
    Private Sub FormatSprdView7()

        Dim I As Integer
        With SprdView7
            .MaxCols = 10

            .set_RowHeight(0, RowHeight * 2.5)
            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 12)

            For I = 2 To 9
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 10)
            Next

            FillHeadingSprdView7()
            MainClass.SetSpreadColor(SprdView7, -1)
            MainClass.ProtectCell(SprdView7, 1, .MaxRows, 1, .MaxCols)
            SprdView7.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdView7.DAutoCellTypes = True
            SprdView7.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdView7.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        End With
    End Sub
    Private Sub FormatSprdView6()

        Dim I As Integer
        With SprdView6
            .MaxCols = 3

            .set_RowHeight(0, RowHeight * 3.5)
            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 50)

            For I = 2 To 3
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .set_ColWidth(.Col, 12)
            Next

            FillHeadingSprdView6()
            MainClass.SetSpreadColor(SprdView6, -1)
            MainClass.ProtectCell(SprdView6, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdView8()

        Dim I As Integer
        With SprdView8
            .MaxCols = 4

            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 25)


            For I = 2 To 4
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .set_ColWidth(I, 12)
            Next

            FillHeadingSprdView8()
            MainClass.SetSpreadColor(SprdView8, -1)
            MainClass.ProtectCell(SprdView8, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub frmGSTR3B_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub Clear1()

        txtCompanyName.Text = ""
        txtAddress.Text = ""
        txtRegnNo.Text = ""

        MainClass.ClearGrid(SprdView3, RowHeight)
        MainClass.ClearGrid(SprdView4, RowHeight)
        MainClass.ClearGrid(SprdView5, RowHeight)
        MainClass.ClearGrid(SprdView6, RowHeight)
        MainClass.ClearGrid(SprdView7, RowHeight)
        MainClass.ClearGrid(SprdView8, RowHeight)

    End Sub
    Private Sub FillHeadingSprdView3()

        With SprdView3
            .Row = 0

            .Col = 1
            .Text = "Nature of Supplies"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Total Taxable Value"
            .Font = VB6.FontChangeBold(.Font, True)


            .Col = 3
            .Text = "Interated Tax"
            .Font = VB6.FontChangeBold(.Font, True)


            .Col = 4
            .Text = "Central Tax"
            .Font = VB6.FontChangeBold(.Font, True)


            .Col = 5
            .Text = "State / UT Tax"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Cess"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub

    Private Sub FillHeadingSprdView4()

        With SprdView4
            .Row = 0

            .Col = 1
            .Text = "Details"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Place of Supply (State / UT)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Total Taxable Value"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Amount of Integrated Tax"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
    Private Sub FillHeadingSprdView7()

        With SprdView7
            .Row = 0

            .Col = 1
            .Text = "Description"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Tax Payable"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Paid Through ITC (Integrated Tax)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Paid Through ITC (Central Tax)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Paid Through ITC (State / UT Tax)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Paid Through ITC (Cess)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 7
            .Text = "Tax Paid TDS / TCS"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "Tax / Cess Paid in Cash"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "Interest"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "Late Fee"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
    Private Sub FillHeadingSprdView5()

        With SprdView5
            .Row = 0

            .Col = 1
            .Text = "Details"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Integrated Tax"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Central Tax"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "State / UT Tax"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Cess"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
    Private Sub FillHeadingSprdView8()

        With SprdView8
            .Row = 0

            .Col = 1
            .Text = "Details"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Integrated Tax"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Central Tax"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "State / UT Tax"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub


    Private Sub FillHeadingSprdView6()

        With SprdView6
            .Row = 0

            .Col = 1
            .Text = "Nature of Supplies"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Inter-State Supplies"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Intra-State Supplies"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
    Private Function ShowDetail1() As Boolean
        On Error GoTo ErrPart1
        Dim mCompanyAdd As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSqlStr As String

        mSqlStr = "SELECT * From GEN_COMPANY_MST WHERE COMPANY_GST_RGN_NO='" & Trim(cboGSTNO.Text) & "'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtCompanyName.Text = IIf(IsDBNull(RsTemp.Fields("Company_Name").Value), "", RsTemp.Fields("Company_Name").Value)

            mCompanyAdd = IIf(IsDBNull(RsTemp.Fields("COMPANY_ADDR").Value), "", RsTemp.Fields("COMPANY_ADDR").Value)
            mCompanyAdd = mCompanyAdd & ", " & IIf(IsDBNull(RsTemp.Fields("COMPANY_CITY").Value), "", RsTemp.Fields("COMPANY_CITY").Value)
            mCompanyAdd = mCompanyAdd & ", " & IIf(IsDBNull(RsTemp.Fields("COMPANY_STATE").Value), "", RsTemp.Fields("COMPANY_STATE").Value)
            mCompanyAdd = mCompanyAdd & ", " & IIf(IsDBNull(RsTemp.Fields("COMPANY_PIN").Value), "", RsTemp.Fields("COMPANY_PIN").Value)
            txtAddress.Text = mCompanyAdd

            txtRegnNo.Text = IIf(IsDBNull(RsTemp.Fields("COMPANY_GST_RGN_NO").Value), "", RsTemp.Fields("COMPANY_GST_RGN_NO").Value)
        End If

        ShowDetail1 = True
        Exit Function
ErrPart1:
        ShowDetail1 = False
    End Function
    Private Function ShowDetail3() As Boolean
        On Error GoTo ErrPart1
        Dim mCntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        'Dim mAgtCT3 As String
        ShowDetail3 = False
        SprdView3.MaxRows = 7


        If InsertDataIntoSprdView3(CStr(1), 1, "(a) Outward taxable supplies (Other than Zero rated, nil rated and Exempted") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView3(CStr(2), 2, "(b) Outward taxable supplies (Zero rated)") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView3(CStr(3), 3, "(c) Other outward supplies (Nil rated, exempted") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView3(CStr(4), 4, "(d) Inward supplies (liable to reverse charge)") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView3(CStr(5), 5, "(e) Non-GST outward supplies") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView3("X", 6, "(f) Customer Debit Note") = False Then GoTo ErrPart1

        SprdView3.Row = SprdView3.MaxRows
        SprdView3.Row2 = SprdView3.MaxRows
        SprdView3.Col = 1
        SprdView3.col2 = SprdView3.MaxCols
        SprdView3.BlockMode = True
        SprdView3.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
        SprdView3.Font = VB6.FontChangeBold(SprdView3.Font, False)
        SprdView3.BlockMode = False


        Call CalcRowTotal(SprdView3, 2, 1, 2, SprdView3.MaxRows - 1, (SprdView3.MaxRows), 2)
        Call CalcRowTotal(SprdView3, 3, 1, 3, SprdView3.MaxRows - 1, (SprdView3.MaxRows), 3)
        Call CalcRowTotal(SprdView3, 4, 1, 4, SprdView3.MaxRows - 1, (SprdView3.MaxRows), 4)
        Call CalcRowTotal(SprdView3, 5, 1, 5, SprdView3.MaxRows - 1, (SprdView3.MaxRows), 5)
        Call CalcRowTotal(SprdView3, 6, 1, 6, SprdView3.MaxRows - 1, (SprdView3.MaxRows), 6)

        ShowDetail3 = True
        Exit Function
ErrPart1:
        ShowDetail3 = False
    End Function
    Private Function ShowDetail5() As Boolean
        On Error GoTo ErrPart1
        Dim mCntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddress As String
        'Dim mUOM As String
        'Dim mTariffDesc As String
        'Dim mRemarks As String
        'Dim mNotification As String

        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mValue As Double

        ShowDetail5 = False
        SprdView5.MaxRows = 13

        If InsertDataIntoSprdView5(CStr(0), 1, "(A) ITC Available (Whether in full or Part)") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(1), 2, "(1) Import of Goods") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(2), 3, "(2) Imports of Services") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(3), 4, "(3) Inward Supplies liable to reverse charges (Other than 1 & 2 above)") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(4), 5, "(4) Inward Supplies from ISD") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(5), 6, "(5) All other ITC") = False Then GoTo ErrPart1

        If InsertDataIntoSprdView5(CStr(0), 7, "(B) ITC Reversed") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(6), 8, "(1) As per rules 42 & 43 of CGST Rules") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(7), 9, "(2) Others") = False Then GoTo ErrPart1

        If InsertDataIntoSprdView5(CStr(0), 10, "(C) Net ITC Available (A)-(B)") = False Then GoTo ErrPart1

        If InsertDataIntoSprdView5(CStr(0), 11, "(D) Ineligible") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(8), 12, "(1) As per section 17(5)") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView5(CStr(9), 13, "(2) Others") = False Then GoTo ErrPart1

        ''Grand Total
        '    Call CalcRowTotal(SprdView5, 6, 1, 6, SprdView5.MaxRows - 1, SprdView5.MaxRows, 6)

        SprdView5.Row = 10 ''SprdView5.MaxRows
        SprdView5.Row2 = 10 ''SprdView5.MaxRows
        SprdView5.Col = 1
        SprdView5.col2 = SprdView5.MaxCols
        SprdView5.BlockMode = True
        SprdView5.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
        SprdView5.Font = VB6.FontChangeBold(SprdView5.Font, True)
        SprdView5.BlockMode = False

        With SprdView5
            For cntCol = 2 To 5
                mValue = 0
                For cntRow = 1 To 6
                    .Col = cntCol
                    .Row = cntRow
                    mValue = mValue + Val(.Text)
                Next

                For cntRow = 7 To 9
                    .Col = cntCol
                    .Row = cntRow
                    mValue = mValue - Val(.Text)
                Next
                .Col = cntCol
                .Row = 10
                .Text = VB6.Format(mValue, "0.00")
            Next
        End With

        '    Call CalcRowTotal(SprdView5, 2, 1, 2, SprdView3.MaxRows - 1, SprdView3.MaxRows, 2)
        '    Call CalcRowTotal(SprdView5, 3, 1, 3, SprdView3.MaxRows - 1, SprdView3.MaxRows, 3)

        ShowDetail5 = True
        Exit Function
ErrPart1:
        ShowDetail5 = False
    End Function
    Private Function InsertDataIntoSprdView3(ByRef pType As String, ByRef pRowNo As Integer, ByRef pDesc As String) As Boolean
        On Error GoTo ErrPart1
        Dim mTaxableAmount As Double
        Dim mIGSTAmt As Double
        Dim mCGSTAmt As Double
        Dim mSGSTAmt As Double
        Dim mCESSAmt As Double

        InsertDataIntoSprdView3 = False
        mTaxableAmount = 0
        mIGSTAmt = 0
        mCGSTAmt = 0
        mSGSTAmt = 0
        mCESSAmt = 0

        If pType = "X" Then
            If GetOutwardValueDebitNote(pType, mTaxableAmount, mIGSTAmt, mCGSTAmt, mSGSTAmt, mCESSAmt) = False Then GoTo ErrPart1
        Else
            If GetOutwardValue(pType, mTaxableAmount, mIGSTAmt, mCGSTAmt, mSGSTAmt, mCESSAmt) = False Then GoTo ErrPart1
        End If

        With SprdView3
            .Row = pRowNo

            .Col = 1
            .Text = pDesc

            .Col = 2
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = 3
            .Text = VB6.Format(mIGSTAmt, "0.00")

            .Col = 4
            .Text = VB6.Format(mCGSTAmt, "0.00")

            .Col = 5
            .Text = VB6.Format(mSGSTAmt, "0.00")

            .Col = 6
            .Text = VB6.Format(mCESSAmt, "0.00")
        End With
        InsertDataIntoSprdView3 = True
        Exit Function
ErrPart1:
        InsertDataIntoSprdView3 = False
    End Function

    Private Function InsertDataIntoSprdView5(ByRef pType As String, ByRef pRowNo As Integer, ByRef pDesc As String) As Boolean
        On Error GoTo ErrPart1
        Dim mTaxableAmount As Double
        Dim mIGSTAmt As Double
        Dim mCGSTAmt As Double
        Dim mSGSTAmt As Double
        Dim mCESSAmt As Double

        InsertDataIntoSprdView5 = False
        mTaxableAmount = 0
        mIGSTAmt = 0
        mCGSTAmt = 0
        mSGSTAmt = 0
        mCESSAmt = 0

        If CDbl(pType) <> 0 Then
            If GetInwardValue(pType, mTaxableAmount, mIGSTAmt, mCGSTAmt, mSGSTAmt, mCESSAmt) = False Then GoTo ErrPart1
        End If

        With SprdView5
            .Row = pRowNo

            .Col = 1
            .Text = pDesc
            If CDbl(pType) = 0 Then
                '            .FontBold = True
                SprdView5.Row = pRowNo ''SprdView5.MaxRows
                SprdView5.Row2 = pRowNo ''SprdView5.MaxRows
                SprdView5.Col = 1
                SprdView5.col2 = SprdView5.MaxCols
                SprdView5.BlockMode = True
                SprdView5.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                SprdView5.Font = VB6.FontChangeBold(SprdView5.Font, True)
                SprdView5.BlockMode = False
            Else
                '            .Col = 2
                '            .Text = Format(mTaxableAmount, "0.00")

                .Col = 2
                .Text = VB6.Format(mIGSTAmt, "0.00")

                .Col = 3
                .Text = VB6.Format(mCGSTAmt, "0.00")

                .Col = 4
                .Text = VB6.Format(mSGSTAmt, "0.00")

                .Col = 5
                .Text = VB6.Format(mCESSAmt, "0.00")
            End If
        End With
        InsertDataIntoSprdView5 = True
        Exit Function
ErrPart1:
        InsertDataIntoSprdView5 = False
    End Function

    Private Function InsertDataIntoSprdView6(ByRef pType As String, ByRef pRowNo As Integer, ByRef pDesc As String) As Boolean
        On Error GoTo ErrPart1

        Dim mInterStateAmt As Double
        Dim mIntraStateAmt As Double

        InsertDataIntoSprdView6 = False

        mInterStateAmt = 0
        mIntraStateAmt = 0


        If GetOtherInwardValue(pType, mInterStateAmt, mIntraStateAmt) = False Then GoTo ErrPart1

        With SprdView6
            .Row = pRowNo

            .Col = 1
            .Text = pDesc

            .Col = 2
            .Text = VB6.Format(mInterStateAmt, "0.00")

            .Col = 3
            .Text = VB6.Format(mIntraStateAmt, "0.00")

        End With
        InsertDataIntoSprdView6 = True
        Exit Function
ErrPart1:
        InsertDataIntoSprdView6 = False
    End Function
    Private Function InsertDataIntoSprdView4(ByRef pType As String, ByRef pRowNo As Integer, ByRef pDesc As String) As Boolean

        On Error GoTo ErrPart1
        Dim mTaxableAmount As Double
        Dim mIGSTAmt As Double
        Dim mPlaceofSupply As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        InsertDataIntoSprdView4 = False
        mTaxableAmount = 0
        mIGSTAmt = 0
        mPlaceofSupply = ""

        If GetOutwardValue(pType, mTaxableAmount, mIGSTAmt, 0, 0, 0, pSqlStr) = False Then GoTo ErrPart1

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mPlaceofSupply = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                mTaxableAmount = IIf(IsDbNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)
                mIGSTAmt = IIf(IsDbNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)

                With SprdView4
                    .Row = pRowNo

                    .Col = 1
                    .Text = pDesc

                    .Col = 2
                    .Text = mPlaceofSupply

                    .Col = 3
                    .Text = VB6.Format(mTaxableAmount, "0.00")

                    .Col = 4
                    .Text = VB6.Format(mIGSTAmt, "0.00")


                End With

                RsTemp.MoveNext()
                pRowNo = pRowNo + 1
                SprdView4.MaxRows = SprdView4.MaxRows + 1
            Loop
        Else

            With SprdView4
                .Row = pRowNo

                .Col = 1
                .Text = pDesc

                .Col = 3
                .Text = VB6.Format(0, "0.00")

                .Col = 4
                .Text = VB6.Format(0, "0.00")

                pRowNo = pRowNo + 1
                SprdView4.MaxRows = SprdView4.MaxRows + 1
            End With
        End If

        InsertDataIntoSprdView4 = True
        Exit Function
ErrPart1:
        InsertDataIntoSprdView4 = False
    End Function


    Private Function InsertDataIntoSprdView8(ByRef pRowNo As Integer, ByRef pDesc As String, ByRef mCenvat As Double, ByRef mCess As Double, ByRef mSHECess As Double, ByRef mAED As Double, ByRef mServiceTax As Double, ByRef mCessOnService As Double, ByRef mSHECessOnService As Double) As Boolean
        On Error GoTo ErrPart1

        InsertDataIntoSprdView8 = False
        With SprdView8
            .Row = pRowNo

            .Col = 1
            .Text = pDesc

            .Col = 2
            If RsCompany.Fields("FYEAR").Value <= 2013 Then
                .Text = VB6.Format(System.Math.Abs(mCenvat + mAED), "0.00")
            Else
                .Text = VB6.Format(System.Math.Abs(mCenvat), "0.00")
            End If

            .Col = 3
            .Text = "" '' Format(Abs(mAED), "0.00")

            .Col = 4
            .Text = ""

            .Col = 5
            If RsCompany.Fields("FYEAR").Value <= 2013 Then
                .Text = ""
            Else
                .Text = VB6.Format(System.Math.Abs(mAED), "0.00")
            End If

            .Col = 6
            .Text = VB6.Format(System.Math.Abs(mCess), "0.00")

            .Col = 7
            .Text = VB6.Format(System.Math.Abs(mSHECess), "0.00")

            .Col = 8
            .Text = VB6.Format(System.Math.Abs(mServiceTax), "0.00")

            .Col = 9
            .Text = VB6.Format(System.Math.Abs(mCessOnService), "0.00")

            .Col = 10
            .Text = VB6.Format(System.Math.Abs(mSHECessOnService), "0.00")

        End With
        InsertDataIntoSprdView8 = True
        Exit Function
ErrPart1:
        InsertDataIntoSprdView8 = False
    End Function

    Private Function ShowDetail6() As Boolean
        On Error GoTo ErrPart1
        On Error GoTo ErrPart1
        Dim mCntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        ShowDetail6 = False
        SprdView6.MaxRows = 3

        If InsertDataIntoSprdView6(CStr(1), 1, "From a supplier under composition scheme, Exempt and Nil rated Supply") = False Then GoTo ErrPart1
        If InsertDataIntoSprdView6(CStr(2), 2, "Non GST Supply") = False Then GoTo ErrPart1


        ''Grand Total
        '    Call CalcRowTotal(SprdView6, 6, 1, 6, SprdView6.MaxRows - 1, SprdView6.MaxRows, 6)

        SprdView6.Row = SprdView6.MaxRows
        SprdView6.Row2 = SprdView6.MaxRows
        SprdView6.Col = 1
        SprdView6.col2 = SprdView6.MaxCols
        SprdView6.BlockMode = True
        SprdView6.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
        SprdView6.Font = VB6.FontChangeBold(SprdView6.Font, True)
        SprdView6.BlockMode = False

        Call CalcRowTotal(SprdView6, 2, 1, 2, SprdView6.MaxRows - 1, (SprdView6.MaxRows), 2)
        Call CalcRowTotal(SprdView6, 3, 1, 3, SprdView6.MaxRows - 1, (SprdView6.MaxRows), 3)

        ShowDetail6 = True
        Exit Function
ErrPart1:
        '    Resume
        ShowDetail6 = False
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
    End Function

    Private Function ShowDetail7() As Boolean

        On Error GoTo ErrPart1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer

        ShowDetail7 = False

        SprdView7.MaxRows = 4

        SqlStr = ""
        SqlStr = " SELECT GST_TYPE,SERIAL_NO, SUM(TAX_PAYABLE) AS TAX_PAYABLE, " & vbCrLf _
            & " SUM(PAID_FROM_IGST) AS PAID_FROM_IGST, " & vbCrLf _
            & " SUM(PAID_FROM_CGST) AS PAID_FROM_CGST, " & vbCrLf _
            & " SUM(PAID_FROM_SGST) AS PAID_FROM_SGST, " & vbCrLf _
            & " SUM(PAID_FROM_CESS) AS PAID_FROM_CESS, " & vbCrLf _
            & " SUM(CASH_PAID) AS CASH_PAID, " & vbCrLf _
            & " SUM(INTEREST_AMT) AS INTEREST_AMT, " & vbCrLf _
            & " SUM(LATE_FEE) AS LATE_FEE " & vbCrLf _
            & " FROM FIN_GSTCHALLAN_DET DET, GEN_COMPANY_MST GMST" & vbCrLf _
            & " WHERE DET.Company_Code=GMST.Company_Code AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') GROUP BY GST_TYPE,SERIAL_NO" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then
                SprdView7.Col = 1

                SprdView7.Row = 1
                SprdView7.Text = "Integrated Tax"

                SprdView7.Row = 2
                SprdView7.Text = "Central Tax"

                SprdView7.Row = 3
                SprdView7.Text = "State / UT Tax"

                SprdView7.Row = 4
                SprdView7.Text = "CESS"
            Else
                I = 1
                .MoveFirst()

                Do While Not .EOF

                    SprdView7.Row = I

                    SprdView7.Col = 1

                    If I = 1 Then
                        SprdView7.Text = "Integrated Tax"
                    ElseIf I = 2 Then
                        SprdView7.Text = "Central Tax"
                    ElseIf I = 3 Then
                        SprdView7.Text = "State / UT Tax"
                    ElseIf I = 4 Then
                        SprdView7.Text = "CESS"
                    End If

                    SprdView7.Col = 2
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("TAX_PAYABLE").Value), 0, .Fields("TAX_PAYABLE").Value), "0.00")

                    SprdView7.Col = 3
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_IGST").Value), 0, .Fields("PAID_FROM_IGST").Value), "0.00")

                    SprdView7.Col = 4
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_CGST").Value), 0, .Fields("PAID_FROM_CGST").Value), "0.00")

                    SprdView7.Col = 5
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_SGST").Value), 0, .Fields("PAID_FROM_SGST").Value), "0.00")

                    SprdView7.Col = 6
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_CESS").Value), 0, .Fields("PAID_FROM_CESS").Value), "0.00")

                    SprdView7.Col = 7
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("CASH_PAID").Value), 0, .Fields("CASH_PAID").Value), "0.00")

                    SprdView7.Col = 8
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("INTEREST_AMT").Value), 0, .Fields("INTEREST_AMT").Value), "0.00")

                    SprdView7.Col = 9
                    SprdView7.Text = VB6.Format(IIf(IsDbNull(.Fields("LATE_FEE").Value), 0, .Fields("LATE_FEE").Value), "0.00")

                    .MoveNext()

                    I = I + 1
                Loop
            End If
        End With

        ShowDetail7 = True
        Exit Function
ErrPart1:
        '    Resume
        ShowDetail7 = False
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
    End Function
    Private Function ShowDetail8() As Boolean
        On Error GoTo ErrPart1

        ShowDetail8 = False

        SprdView8.MaxRows = 17


        ShowDetail8 = True
        Exit Function
ErrPart1:
        ShowDetail8 = False
    End Function

    Private Sub txtaddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddress.TextChanged
        '    Call PrintStatus(False)
    End Sub

    Private Sub txtCompanyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyName.TextChanged
        '    Call PrintStatus(False)
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegnNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Function GetOutwardValue(ByRef pType As String, ByRef mTaxableAmount As Double, ByRef mIGSTAmt As Double, ByRef mCGSTAmt As Double, ByRef mSGSTAmt As Double, ByRef mCESSAmt As Double, Optional ByRef pSqlStr As String = "") As Boolean


        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String

        pSqlStr = ""

        SqlStr = "SELECT  "

        If CDbl(pType) = 6 Or CDbl(pType) = 7 Or CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " SUPP_CUST_STATE, "
        End If

        SqlStr = SqlStr & vbCrLf & " SUM(TOTTAXABLEAMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(NETCGST_AMOUNT) AS NETCGST_AMOUNT, " & vbCrLf _
            & " SUM(NETSGST_AMOUNT) AS NETSGST_AMOUNT," & vbCrLf _
            & " SUM(NETIGST_AMOUNT) AS NETIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf _
            & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N'"

        '    SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO)) & "'"

        If CDbl(pType) = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND (NETCGST_AMOUNT+NETSGST_AMOUNT+NETIGST_AMOUNT)>0 AND IH.INVOICESEQTYPE IN (1,2,4,9) AND IH.IS_LUT='N'"
        ElseIf CDbl(pType) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (IH.INVOICESEQTYPE IN (6) OR IH.IS_LUT='Y')"
        ElseIf CDbl(pType) = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICESEQTYPE IN (0)"
        ElseIf CDbl(pType) = 4 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICESEQTYPE IN (7,8)"
        ElseIf CDbl(pType) = 5 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.GST_REGD='N' AND CMST.WITHIN_STATE='N' AND IH.INVOICESEQTYPE IN (1,2,4)"
        ElseIf CDbl(pType) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.GST_REGD='C' AND CMST.WITHIN_STATE='N' AND IH.INVOICESEQTYPE IN (1,2,4)"
        ElseIf CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If CDbl(pType) = 6 Or CDbl(pType) = 7 Or CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY SUPP_CUST_STATE "
        End If

        pSqlStr = SqlStr
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTaxableAmount = IIf(IsDBNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

            mIGSTAmt = IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            mCGSTAmt = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mSGSTAmt = IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mCESSAmt = 0
        End If
        GetOutwardValue = True

        Exit Function
ErrorPart:
        GetOutwardValue = False
    End Function
    Private Function GetOutwardValueDebitNote(ByRef pType As String, ByRef mTaxableAmount As Double, ByRef mIGSTAmt As Double, ByRef mCGSTAmt As Double, ByRef mSGSTAmt As Double, ByRef mCESSAmt As Double) As Boolean


        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String


        ''Sale Return..

        mTaxableAmount = 0
        mIGSTAmt = 0
        mCGSTAmt = 0
        mSGSTAmt = 0
        ''01-10-2018

        SqlStr = "SELECT  "
        SqlStr = SqlStr & vbCrLf & " SUM(TOTTAXABLEAMOUNT*-1) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_AMOUNT*-1) AS NETCGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTSGST_AMOUNT*-1) AS NETSGST_AMOUNT," & vbCrLf _
            & " SUM(TOTIGST_AMOUNT*-1) AS NETIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf _
            & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND IH.PURCHASESEQTYPE=2" ''AND GST_APP='Y' AND CANCELLED='N' AND ISFINALPOST='Y'" 'PurchaseSeqType.Caption = "2"

        SqlStr = SqlStr & vbCrLf & " AND (TOTCGST_AMOUNT+TOTSGST_AMOUNT+TOTIGST_AMOUNT)>0 "

        SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ') <>'" & Trim(cboGSTNO.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTaxableAmount = mTaxableAmount + IIf(IsDBNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

            mIGSTAmt = mIGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            mCGSTAmt = mCGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mSGSTAmt = mSGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mCESSAmt = 0
        End If

        ''Other Than Sale Return

        SqlStr = "SELECT  "
        SqlStr = SqlStr & vbCrLf & " SUM(TOTTAXABLEAMOUNT*DECODE(IH.BOOKTYPE,'L',-1,1)) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_AMOUNT*DECODE(IH.BOOKTYPE,'L',-1,1)) AS NETCGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTSGST_AMOUNT*DECODE(IH.BOOKTYPE,'L',-1,1)) AS NETSGST_AMOUNT," & vbCrLf _
            & " SUM(TOTIGST_AMOUNT*DECODE(IH.BOOKTYPE,'L',-1,1)) AS NETIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf _
            & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.CANCELLED='N' AND GST_APP='Y' AND CANCELLED='N' AND ISFINALPOST='Y'"

        SqlStr = SqlStr & vbCrLf _
            & " AND NVL(CMST.GST_RGN_NO,' ') <>'" & Trim(cboGSTNO.Text) & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND (TOTCGST_AMOUNT+TOTSGST_AMOUNT+TOTIGST_AMOUNT)>0 "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTaxableAmount = mTaxableAmount + IIf(IsDBNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

            mIGSTAmt = mIGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            mCGSTAmt = mCGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mSGSTAmt = mSGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mCESSAmt = 0
        End If

        GetOutwardValueDebitNote = True

        Exit Function
ErrorPart:
        GetOutwardValueDebitNote = False
    End Function
    Private Function GetInwardValue(ByRef pType As String, ByRef mTaxableAmount As Double, ByRef mIGSTAmt As Double, ByRef mCGSTAmt As Double, ByRef mSGSTAmt As Double, ByRef mCESSAmt As Double) As Boolean


        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String

        SqlStr = " SELECT  SUM(TOTTAXABLEAMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(NETCGST_AMOUNT) AS NETCGST_AMOUNT,  SUM(NETSGST_AMOUNT) AS NETSGST_AMOUNT, SUM(NETIGST_AMOUNT) AS NETIGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_AMOUNT) AS TOTCGST_AMOUNT,  SUM(TOTSGST_AMOUNT) AS TOTSGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTIGST_AMOUNT) AS TOTIGST_AMOUNT" & vbCrLf & " FROM ("


        SqlStr = SqlStr & vbCrLf & " SELECT  SUM(TOTTAXABLEAMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_REFUNDAMT) AS NETCGST_AMOUNT, SUM(TOTSGST_REFUNDAMT) AS NETSGST_AMOUNT, SUM(TOTIGST_REFUNDAMT) AS NETIGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_AMOUNT) AS TOTCGST_AMOUNT, SUM(TOTSGST_AMOUNT) AS TOTSGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTIGST_AMOUNT) AS TOTIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST"

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'"

        '& vbCrLf _
        '    & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND PURCHASESEQTYPE<>2" '' Not Considering Debit Note of Customer '01-10-2018

        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            If CDbl(pType) = 9 Then
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM IN ('A')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM IN ('Y')"
            End If
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE IN ('G','R','J')"

        If CDbl(pType) = 1 Then
            '        SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO)) & "'"
            SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE IN ('G') AND ISGSTAPPLICABLE='G' AND CMST.WITHIN_COUNTRY='N'"
        ElseIf CDbl(pType) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='G' AND PURCHASE_TYPE IN ('J','R') AND CMST.WITHIN_COUNTRY='N'"

        ElseIf CDbl(pType) = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
            '        SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='R' AND PURCHASE_TYPE IN ('G','W','J','S','R') "
        ElseIf CDbl(pType) = 4 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 5 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='G' AND PURCHASE_TYPE IN ('G','J','R') AND CMST.WITHIN_COUNTRY='Y'"
        ElseIf CDbl(pType) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 9 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='I' AND PURCHASE_TYPE IN ('G','J','R') AND CMST.WITHIN_COUNTRY='Y'"
        End If

        'If RsCompany.Fields("FYEAR").Value >= 2018 Then
        SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.GST_CLAIM_NEW_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '        & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        'End If



        SqlStr = SqlStr & vbCrLf & " UNION ALL"


        SqlStr = SqlStr & vbCrLf & " SELECT  SUM(TOTTAXABLEAMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_REFUNDAMT) AS NETCGST_AMOUNT, SUM(TOTSGST_REFUNDAMT) AS NETSGST_AMOUNT, SUM(TOTIGST_REFUNDAMT) AS NETIGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTCGST_AMOUNT) AS TOTCGST_AMOUNT, SUM(TOTSGST_AMOUNT) AS TOTSGST_AMOUNT, " & vbCrLf _
            & " SUM(TOTIGST_AMOUNT) AS TOTIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" '& vbCrLf |            & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N'"

        '    SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE IN ('G','R','J')"

        If CDbl(pType) = 1 Then
            '        SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO)) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='G' AND CMST.WITHIN_COUNTRY='N'"
        ElseIf CDbl(pType) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='G' AND CMST.WITHIN_COUNTRY='N'"

        ElseIf CDbl(pType) = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
            '        SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='R' AND PURCHASE_TYPE IN ('G','W','J','S','R') "
        ElseIf CDbl(pType) = 4 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 5 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='G' AND  CMST.WITHIN_COUNTRY='Y'"
        ElseIf CDbl(pType) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 9 Then
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='I' AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT  SUM(GSTABLE_AMT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(CGST_AMOUNT) AS NETCGST_AMOUNT, SUM(SGST_AMOUNT) AS NETSGST_AMOUNT, SUM(IGST_AMOUNT) AS NETIGST_AMOUNT, " & vbCrLf _
            & " SUM(CGST_AMOUNT) AS TOTCGST_AMOUNT, SUM(SGST_AMOUNT) AS TOTSGST_AMOUNT, " & vbCrLf _
            & " SUM(IGST_AMOUNT) AS TOTIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'"

        '& vbCrLf _
        '    & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N'"

        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            If CDbl(pType) = 9 Then
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM IN ('A')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM IN ('Y')"
            End If
        End If

        If CDbl(pType) = 1 Then
            '        SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO)) & "'"
            SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE IN ('S','W') AND GST_CREDITAPP='Y' AND GST_RCAPP='N' AND GOODS_SERVICE='G'  AND CMST.WITHIN_COUNTRY='N'"
        ElseIf CDbl(pType) = 2 Then
            SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND GST_CREDITAPP='Y' AND GST_RCAPP='N' AND GOODS_SERVICE='S'  AND CMST.WITHIN_COUNTRY='N'"

        ElseIf CDbl(pType) = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
            '        SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='R' AND PURCHASE_TYPE IN ('G','W','J','S','R') "
        ElseIf CDbl(pType) = 4 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 5 Then
            SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE IN ('S','W') AND GST_RCAPP='N' AND GST_CREDITAPP='Y'  AND CMST.WITHIN_COUNTRY='Y'"

        ElseIf CDbl(pType) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 9 Then
            SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE IN ('S','W') AND GST_RCAPP='N' AND GST_CREDITAPP='N' AND (CGST_AMOUNT+SGST_AMOUNT+IGST_AMOUNT>0) AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.GST_CLAIM_NEW_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT  SUM(TOTTAXABLEAMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(IH.TOTCGST_RC_REFUNDAMT) AS NETCGST_AMOUNT, SUM(IH.TOTSGST_RC_REFUNDAMT) AS NETSGST_AMOUNT, SUM(IH.TOTIGST_RC_REFUNDAMT) AS NETIGST_AMOUNT, " & vbCrLf _
            & " SUM(NETCGST_AMOUNT) AS TOTCGST_AMOUNT, SUM(NETSGST_AMOUNT) AS TOTSGST_AMOUNT, " & vbCrLf _
            & " SUM(NETIGST_AMOUNT) AS TOTIGST_AMOUNT" & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'"

        '& vbCrLf _
        '    & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N'"

        If CDbl(pType) = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE IN (7, 8) AND GST_RC_CLAIM='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If


        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_RC_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.GST_CLAIM_RC_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTaxableAmount = IIf(IsDBNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

            If CDbl(pType) = 9 Then
                mIGSTAmt = IIf(IsDBNull(RsTemp.Fields("TOTIGST_AMOUNT").Value), 0, RsTemp.Fields("TOTIGST_AMOUNT").Value)
                mCGSTAmt = IIf(IsDBNull(RsTemp.Fields("TOTCGST_AMOUNT").Value), 0, RsTemp.Fields("TOTCGST_AMOUNT").Value)
                mSGSTAmt = IIf(IsDBNull(RsTemp.Fields("TOTSGST_AMOUNT").Value), 0, RsTemp.Fields("TOTSGST_AMOUNT").Value)
                mCESSAmt = 0

            Else
                mIGSTAmt = IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
                mCGSTAmt = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
                mSGSTAmt = IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
                mCESSAmt = 0
            End If
        End If

        ''LC Opening
        If CDbl(pType) = 5 Then


            SqlStr = "SELECT  SUM(AMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
                & " SUM(CGST_AMOUNT) AS NETCGST_AMOUNT, SUM(SGST_AMOUNT) AS NETSGST_AMOUNT, SUM(IGST_AMOUNT) AS NETIGST_AMOUNT " & vbCrLf _
                & " FROM FIN_LCOPEN_HDR IH, FIN_LCOPEN_DET ID, GEN_COMPANY_MST GMST"

            SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf _
                & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM IN ('Y')"
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If



            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mTaxableAmount = IIf(IsDBNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

                mIGSTAmt = mIGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
                mCGSTAmt = mCGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
                mSGSTAmt = mSGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
                mCESSAmt = 0
            End If

            SqlStr = "SELECT  SUM(AMOUNT) AS TOTTAXABLEAMOUNT, " & vbCrLf _
                & " SUM(CGST_AMOUNT) AS NETCGST_AMOUNT, SUM(SGST_AMOUNT) AS NETSGST_AMOUNT, SUM(IGST_AMOUNT) AS NETIGST_AMOUNT " & vbCrLf _
                & " FROM FIN_LCDISC_HDR IH, FIN_LCDISC_DET ID, GEN_COMPANY_MST GMST"

            SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM IN ('Y')"
                SqlStr = SqlStr & vbCrLf & " AND IH.GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If



            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mTaxableAmount = IIf(IsDBNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

                mIGSTAmt = mIGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
                mCGSTAmt = mCGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
                mSGSTAmt = mSGSTAmt + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
                mCESSAmt = 0
            End If

        End If
        '' Debit Note..

        SqlStr = "SELECT  SUM(TOTTAXABLEAMOUNT * DECODE(BOOKCODE," & ConDebitNoteBookCode & ",1,-1)) AS TOTTAXABLEAMOUNT, " & vbCrLf _
            & " SUM(NETCGST_AMOUNT * DECODE(BOOKCODE," & ConDebitNoteBookCode & ",1,-1)) AS NETCGST_AMOUNT, " & vbCrLf _
            & " SUM(NETSGST_AMOUNT * DECODE(BOOKCODE," & ConDebitNoteBookCode & ",1,-1)) AS NETSGST_AMOUNT," & vbCrLf _
            & " SUM(NETIGST_AMOUNT * DECODE(BOOKCODE," & ConDebitNoteBookCode & ",1,-1)) AS NETIGST_AMOUNT" & vbCrLf _
            & " FROM FIN_DNCN_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'"

        '& vbCrLf _
        '    & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",IH.DEBITACCOUNTCODE,IH.CREDITACCOUNTCODE)=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.CANCELLED='N' AND BOOKCODE IN (" & ConDebitNoteBookCode & "," & ConCreditNoteBookCode & ") AND IH.APPROVED='Y'"


        If CDbl(pType) = 1 Then
            SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTREFUND='G' AND CMST.WITHIN_COUNTRY='N'"
        ElseIf CDbl(pType) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 3 Then
            'SqlStr = SqlStr & vbCrLf & " AND 1=2"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTREFUND='R'"
        ElseIf CDbl(pType) = 4 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 5 Then
            SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ')<>'" & Trim(cboGSTNO.Text) & "'"
            SqlStr = SqlStr & vbCrLf & " AND ISGSTREFUND='G' AND  CMST.WITHIN_COUNTRY='Y'"
        ElseIf CDbl(pType) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 8 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        ElseIf CDbl(pType) = 9 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If

        'If RsCompany.Fields("FYEAR").Value >= 2018 Then
        SqlStr = SqlStr & vbCrLf _
            & " AND IH.PARTY_DNCN_RECDDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.PARTY_DNCN_RECDDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        'Else
        '    SqlStr = SqlStr & vbCrLf _
        '        & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '        & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTaxableAmount = mTaxableAmount - IIf(IsDbNull(RsTemp.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT").Value)

            mIGSTAmt = mIGSTAmt - IIf(IsDbNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            mCGSTAmt = mCGSTAmt - IIf(IsDbNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mSGSTAmt = mSGSTAmt - IIf(IsDbNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mCESSAmt = 0
        End If

        ''ITC Reversal Voucher..

        If CDbl(pType) = 6 Or CDbl(pType) = 7 Then

            SqlStr = "SELECT " & vbCrLf _
                & " SUM(REVERSAL_CGST_AMOUNT) AS CGST_AMOUNT, SUM(REVERSAL_SGST_AMOUNT) AS SGST_AMOUNT, SUM(REVERSAL_IGST_AMOUNT) AS IGST_AMOUNT " & vbCrLf _
                & " FROM FIN_GSTREVERSAL_TRN IH, GEN_COMPANY_MST GMST"

            SqlStr = SqlStr & vbCrLf _
                & " WHERE " & vbCrLf _
                & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf _
                & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

            SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND IH.ISFINALPOST='Y'"

            If CDbl(pType) = 6 Then
                SqlStr = SqlStr & vbCrLf & " AND IH.REVERSAL_RULE IN ('b','c','d','e')"
            Else ''Or pType = 7
                SqlStr = SqlStr & vbCrLf & " AND IH.REVERSAL_RULE NOT IN ('b','c','d','e')"
            End If

            SqlStr = SqlStr & vbCrLf & " AND IH.REFDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.REFDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIGSTAmt = mIGSTAmt + IIf(IsDbNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value)
                mCGSTAmt = mCGSTAmt + IIf(IsDbNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value)
                mSGSTAmt = mSGSTAmt + IIf(IsDbNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value)
                mCESSAmt = 0
            End If
        End If


        ''Reverse Charge Voucher..

        '   If pType = 3 Then
        '        SqlStr = "SELECT " & vbCrLf _
        ''                 & " SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT " & vbCrLf _
        ''                 & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID "
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''                 & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
        ''                 & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                 & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"
        '
        '        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND IH.REVERSE_CHARGE_APP='Y'"
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
        '
        '
        '
        '       MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '       If RsTemp.EOF = False Then
        '            mIGSTAmt = mIGSTAmt + IIf(IsNull(RsTemp!IGST_AMOUNT), 0, RsTemp!IGST_AMOUNT)
        '            mCGSTAmt = mCGSTAmt + IIf(IsNull(RsTemp!CGST_AMOUNT), 0, RsTemp!CGST_AMOUNT)
        '            mSGSTAmt = mSGSTAmt + IIf(IsNull(RsTemp!SGST_AMOUNT), 0, RsTemp!SGST_AMOUNT)
        '            mCESSAmt = 0
        '       End If
        '   End If

        GetInwardValue = True

        Exit Function
ErrorPart:
        GetInwardValue = False
    End Function

    Private Function GetOtherInwardValue(ByRef pType As String, ByRef mInterStateAmt As Double, ByRef mIntraStateAmt As Double) As Boolean


        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String


        SqlStr = "SELECT SUM(CASE WHEN WITHIN_STATE='N' THEN TOTTAXABLEAMOUNT ELSE 0 END) AS TOTTAXABLEAMOUNT_INTER, " & vbCrLf _
            & " SUM(CASE WHEN WITHIN_STATE='Y' THEN TOTTAXABLEAMOUNT ELSE 0 END) AS TOTTAXABLEAMOUNT_INTRA" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE = GMST.COMPANY_CODE AND GMST.COMPANY_GST_RGN_NO='" & cboGSTNO.Text & "'" & vbCrLf & " AND IH.FYEAR=  '" & RsCompany.Fields("FYEAR").Value & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
        ''            & " AND IH.SUPP_CUST_CODE=INVMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND IH.TOTCGST_AMOUNT+IH.TOTSGST_AMOUNT+IH.TOTIGST_AMOUNT=0"

        If CDbl(pType) = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='E'" 'GST_REGD IN ('C','E') OR
        ElseIf CDbl(pType) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mInterStateAmt = IIf(IsDbNull(RsTemp.Fields("TOTTAXABLEAMOUNT_INTER").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT_INTER").Value)

            mIntraStateAmt = IIf(IsDbNull(RsTemp.Fields("TOTTAXABLEAMOUNT_INTRA").Value), 0, RsTemp.Fields("TOTTAXABLEAMOUNT_INTRA").Value)
        End If

        GetOtherInwardValue = True

        Exit Function
ErrorPart:
        GetOtherInwardValue = False
    End Function
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        Dim mUpdate As Boolean

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdate = False

        'If TxtName.Text = "" Then Exit Sub

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""



        If FillTempTable(SprdView3, 0, 0, 1, SprdView3.MaxCols, "AH") = False Then GoTo ERR1
        If FillTempTable(SprdView3, 1, SprdView3.MaxRows, 1, SprdView3.MaxCols, "AZ") = False Then GoTo ERR1

        If FillTempTable(SprdView4, 0, 0, 1, SprdView4.MaxCols, "BH") = False Then GoTo ERR1
        If FillTempTable(SprdView4, 1, SprdView4.MaxRows, 1, SprdView4.MaxCols, "BZ") = False Then GoTo ERR1

        If FillTempTable(SprdView5, 0, 0, 1, SprdView5.MaxCols, "CH") = False Then GoTo ERR1
        If FillTempTable(SprdView5, 1, SprdView5.MaxRows, 1, SprdView5.MaxCols, "CZ") = False Then GoTo ERR1

        If FillTempTable(SprdView6, 0, 0, 1, SprdView6.MaxCols, "DH") = False Then GoTo ERR1
        If FillTempTable(SprdView6, 1, SprdView6.MaxRows, 1, SprdView6.MaxCols, "DZ") = False Then GoTo ERR1

        If FillTempTable(SprdView7, 0, 0, 1, SprdView7.MaxCols, "EH") = False Then GoTo ERR1
        If FillTempTable(SprdView7, 1, SprdView7.MaxRows, 1, SprdView7.MaxCols, "EZ") = False Then GoTo ERR1

        If FillTempTable(SprdView8, 0, 0, 1, SprdView8.MaxCols, "FH") = False Then GoTo ERR1
        If FillTempTable(SprdView8, 1, SprdView8.MaxRows, 1, SprdView8.MaxCols, "FZ") = False Then GoTo ERR1


        PubDBCn.CommitTrans()
        mUpdate = True

        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD20,SUBROW")

        mTitle = "FORM GSTR 3B"
        mSubTitle = "(See Rule 61(5))"

        mRPTName = "FormGSTR3B.Rpt"

        Call ShowWindowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        Exit Sub
        If mUpdate = False Then
            PubDBCn.RollbackTrans()
        End If
        ''Resume
    End Sub

    Public Function FillTempTable(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef GroupField As String) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""


        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(VB.Left(GridName.Text, 255)) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(VB.Left(GridName.Text, 255)) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ", FIELD20) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ", '" & GroupField & "') "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next

        FillTempTable = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillTempTable = False
    End Function
    Private Sub ShowWindowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mMonth As String
        Dim mRF1 As String
        Dim mRF2 As String
        Dim mACk As String
        Dim mAmount As Double
        Dim mStartingNo As String
        Dim mEndingNo As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mDate As String

        mDate = "10/" & VB6.Format(Month(CDate(txtDateTo.Text)), "00") & "/" & VB6.Format(Year(CDate(txtDateTo.Text)), "0000")
        mDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mDate)))

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")

        MainClass.AssignCRptFormulas(Report1, "FormTitle='Central Excise Rules, 2002 and Rule 9(7) of CENVAT Credit Rules, 2004'")

        MainClass.AssignCRptFormulas(Report1, "Month=""" & VB6.Format(txtDateFrom.Text, "MMM,YYYY") & """")

        MainClass.AssignCRptFormulas(Report1, "Commissionerate=""" & IIf(IsDbNull(RsCompany.Fields("COMMISIONER_RATE").Value), "", RsCompany.Fields("COMMISIONER_RATE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDbNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDbNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "RegistrationNo=""" & UCase(txtRegnNo.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "Place=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Dated=""" & mDate & """")




        SqlStr = "SELECT SUM(TOTALAMOUNT) AS TOTALAMOUNT " & vbCrLf _
            & " FROM FIN_MODVATCHALLAN_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND CHALLANTYPE='M' " & vbCrLf & " AND REF_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            mAmount = IIf(IsDbNull(RS.Fields("TOTALAMOUNT").Value), 0, RS.Fields("TOTALAMOUNT").Value)
        End If

        SqlStr = "SELECT MAX(BILLNO) AS MAXBILLNO,MIN(BILLNO) AS MINBILLNO " & vbCrLf _
            & " FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf _
            & " AND BookSubType='E' " & vbCrLf _
            & " AND INVOICE_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            mStartingNo = Mid(IIf(IsDbNull(RS.Fields("MINBILLNO").Value), "", RS.Fields("MINBILLNO").Value), 2)
            mEndingNo = Mid(IIf(IsDbNull(RS.Fields("MAXBILLNO").Value), "", RS.Fields("MAXBILLNO").Value), 2)
        End If


        mRF1 = "During the month, total Rs. " & VB6.Format(mAmount, "0.00") & " was deposited vide TR 6 Challans (copies enclosed)."
        MainClass.AssignCRptFormulas(Report1, "RF1=""" & mRF1 & """")

        mRF2 = "During the month, invoices bearing S.No. " & mStartingNo & " to S. No. " & mEndingNo & " were issued."
        MainClass.AssignCRptFormulas(Report1, "RF2=""" & mRF2 & """")

        mACk = "Return of excisable goods and availment of CENVAT Credit for the month of " & VB6.Format(txtDateFrom.Text, "MMM-YYYY")
        MainClass.AssignCRptFormulas(Report1, "Ack=""" & mACk & """")



        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
End Class
