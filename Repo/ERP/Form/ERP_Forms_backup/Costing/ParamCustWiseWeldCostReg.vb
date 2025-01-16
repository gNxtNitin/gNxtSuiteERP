Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamCustWiseWeldCostReg
    Inherits System.Windows.Forms.Form

    'Dim PvtDBCn As ADODB.Connection		

    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    Private Const ColCustomerCode As Short = 1
    Private Const ColCustomerName As Short = 2
    Private Const ColCostWEF As Short = 3
    Private Const ColAmendNo As Short = 4

    Dim mMaxCol As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub


    Private Sub chkAllCustomer_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCustomer.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCustomer.Enabled = False
            cmdsearchCustomer.Enabled = False
        Else
            txtCustomer.Enabled = True
            cmdsearchCustomer.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()


        mTitle = "Customer Costing Vs Purchase Order Register"
        mSubTitle = "As On Date : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") ''& " To : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")		
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\CCVsPO.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume		
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, False)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCustomer.Click
        SearchDept()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        ''MainClass.setfocusToCell SprdMain, mActiveRow, 4		
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamCustWiseWeldCostReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Customer Costing Vs Purchase Order Register"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamCustWiseWeldCostReg_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamCustWiseWeldCostReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError

        'Set PvtDBCn = New ADODB.Connection		
        'PvtDBCn.Open StrConn		

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)		
        'Me.Width = VB6.TwipsToPixelsX(11355)		


        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        txtCustomer.Enabled = False
        cmdsearchCustomer.Enabled = False

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")		

        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamCustWiseWeldCostReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamCustWiseWeldCostReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateFrom.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateFrom.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        SearchDept()
    End Sub
    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchDept()
    End Sub
    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If txtCustomer.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCustomer.Text = UCase(Trim(txtCustomer.Text))
        Else
            MsgInformation("No Such Department in Department Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer

        mMaxCol = IIf(optShow(0).Checked = True, 12, 43)

        With SprdMain
            .MaxCols = mMaxCol
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1


            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerCode, 6)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerName, 25)

            .Col = ColCostWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCostWEF, 12)
            .ColHidden = False

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColAmendNo, 6)
            .ColHidden = False

            .ColsFrozen = ColCustomerName

            For cntCol = ColAmendNo + 1 To mMaxCol
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 8)
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' = OperationModeSingle		
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
        Call FillHeading()
    End Sub

    Private Sub FillHeading()
        Dim cntCol As Integer

        mMaxCol = IIf(optShow(0).Checked = True, 12, 43)
        With SprdMain
            .MaxCols = mMaxCol
            .Row = 0
            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColCostWEF
            .Text = "W.E.F."

            .Col = ColAmendNo
            .Text = "Amend No"

            If optShow(0).Checked = True Then
                .Col = ColAmendNo + 1
                .Text = "MIG Wire Cost"

                .Col = ColAmendNo + 2
                .Text = "CO2 Cost"

                .Col = ColAmendNo + 3
                .Text = "M/C Cost"

                .Col = ColAmendNo + 4
                .Text = "Power Cost"

                .Col = ColAmendNo + 5
                .Text = "Labour Cost"

                .Col = ColAmendNo + 6
                .Text = "Consumable Cost"

                .Col = ColAmendNo + 7
                .Text = "Smoke Extraction Cost"

                .Col = ColAmendNo + 8
                .Text = "Welding Cost Per Inch"
            Else
                .Col = ColAmendNo + 1
                .Text = "Production Per Shift (Inch)"

                .Col = ColAmendNo + 2
                .Text = "Production Per Day (Inch)"

                .Col = ColAmendNo + 3
                .Text = "Production Per Month (Inch)"

                .Col = ColAmendNo + 4
                .Text = "MIG Wire Rate Per KG."

                .Col = ColAmendNo + 5
                .Text = "MIG Wire Length Per KG."

                .Col = ColAmendNo + 6
                .Text = "MIG Wire Cost Per Inch"

                .Col = ColAmendNo + 7
                .Text = "CO2 Rate Per KG."

                .Col = ColAmendNo + 8
                .Text = "Co2 Length Per KG."

                .Col = ColAmendNo + 9
                .Text = "CO2 Cost Per Inch"

                .Col = ColAmendNo + 10
                .Text = "Cost of Machine"

                .Col = ColAmendNo + 11
                .Text = "Interest %"

                .Col = ColAmendNo + 12
                .Text = "Interest Amount"

                .Col = ColAmendNo + 13
                .Text = "Depreciation %"

                .Col = ColAmendNo + 14
                .Text = "Depreciation Amount"

                .Col = ColAmendNo + 15
                .Text = "Maintance %"

                .Col = ColAmendNo + 16
                .Text = "Maintance Amount"

                .Col = ColAmendNo + 17
                .Text = "Machine Cost Per Month"

                .Col = ColAmendNo + 18
                .Text = "Machine Cost Per Inch"

                .Col = ColAmendNo + 19
                .Text = "Power Consumption"

                .Col = ColAmendNo + 20
                .Text = "Power Efficiency"

                .Col = ColAmendNo + 21
                .Text = "HSEB Consumption %"

                .Col = ColAmendNo + 22
                .Text = "HSEB Rate"

                .Col = ColAmendNo + 23
                .Text = "Net HSEB Rate"

                .Col = ColAmendNo + 24
                .Text = "DG Consumption %"

                .Col = ColAmendNo + 25
                .Text = "DG Rate"

                .Col = ColAmendNo + 26
                .Text = "Net DG Rate"

                .Col = ColAmendNo + 27
                .Text = "Net Power Rate per Unit"

                .Col = ColAmendNo + 28
                .Text = "Electric Load in an Hour"

                .Col = ColAmendNo + 29
                .Text = "No of Hour Per Day"

                .Col = ColAmendNo + 30
                .Text = "Power Cost Per Month"

                .Col = ColAmendNo + 31
                .Text = "Power Cost Per Inch"

                .Col = ColAmendNo + 32
                .Text = "Operator Cost"

                .Col = ColAmendNo + 33
                .Text = "Helper Cost"

                .Col = ColAmendNo + 34
                .Text = "Labour Cost Per Month"

                .Col = ColAmendNo + 35
                .Text = "Labour Cost Per Inch"

                .Col = ColAmendNo + 36
                .Text = "Consumable Cost Per Month"

                .Col = ColAmendNo + 37
                .Text = "Consumable Cost Per Inch"

                .Col = ColAmendNo + 38
                .Text = "Smoke Extraction Cost"

                .Col = ColAmendNo + 39
                .Text = "Welding Cost Per Inch"

            End If
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mCustCode As String


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = MakeSQL()


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")


        '''********************************		
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function MakeSQL() As String

        On Error GoTo ERR1

        If optShow(0).Checked = True Then
            MakeSQL = " SELECT  " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " IH.WEF, AMEND_NO, " & vbCrLf & " MIG_COST_PER_INCH, CO2_COST_PER_INCH, MC_COST_PER_INCH, " & vbCrLf & " POWER_COST_PER_INCH, LABOUR_COST_PER_INCH, CONS_COST_PER_INCH, " & vbCrLf & " SMOKE_COST_PER_INCH, TOT_WELD_COST_PER_INCH "
        Else
            MakeSQL = " SELECT  " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " IH.WEF, AMEND_NO, " & vbCrLf & " PROD_CAP_INCH_PER_SHIFT, PROD_CAP_INCH_PER_DAY, PROD_CAP_INCH_PER_MONTH,  " & vbCrLf & " MIG_COST_PER_KG, MIG_INCH_PER_KG, MIG_COST_PER_INCH,  " & vbCrLf & " CO2_COST_PER_KG, CO2_INCH_PER_KG, CO2_COST_PER_INCH,  " & vbCrLf & " COST_OF_MC, INTEREST_PER, INTEREST_AMOUNT,  " & vbCrLf & " DEP_PER, DEP_AMOUNT, MAINT_PER,  " & vbCrLf & " MAINT_AMOUNT, MC_COST_PER_MONTH, MC_COST_PER_INCH,  " & vbCrLf & " POWER_CONS_KW, POWER_EFFICIENCY, HSEB_PER,  " & vbCrLf & " HSBC_RATE, NET_HSBC_RATE, DG_PER,  " & vbCrLf & " DG_RATE, NET_DG_RATE, NET_POWER_RATE,  " & vbCrLf & " ELEC_LOAD_HOUR, NO_HOUR_PER_DAY, POWER_COST_PER_MONTH,  " & vbCrLf & " POWER_COST_PER_INCH, WELDER_RATE, HELPER_RATE,  " & vbCrLf & " NET_LABOUR_COST, LABOUR_COST_PER_INCH, CONS_COST_PER_MONTH,  " & vbCrLf & " CONS_COST_PER_INCH, SMOKE_COST_PER_INCH, TOT_WELD_COST_PER_INCH "
        End If

        ''''FROM CLAUSE...		
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_CUST_WELD_COST_MST IH, FIN_SUPP_CUST_MST CMST"

        ''''WHERE CLAUSE...		
        MakeSQL = MakeSQL & vbCrLf & " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND CMST.SUPP_CUST_NAME ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "
        End If



        MakeSQL = MakeSQL & vbCrLf & " AND MKEY = (SELECT MAX(MKEY) FROM PRD_CUST_WELD_COST_MST A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND A.WEF <= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')) "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1


        If Trim(txtDateFrom.Text) = "" Then
            MsgInformation("Date is blank.")
            txtDateFrom.Focus()
            FieldsVerification = False
            Exit Function
        End If

        '    If Trim(txtDateTo.Text) = "" Then		
        '        MsgInformation "Date is blank."		
        '        txtDateTo.focus		
        '        FieldsVerification = False		
        '        Exit Function		
        '    End If		

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtCustomer.Text) = "" Then
                MsgInformation("Department is blank.")
                txtCustomer.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub SearchDept()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtCustomer.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtCustomer.Text = AcName
            txtCustomer.Focus()
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
