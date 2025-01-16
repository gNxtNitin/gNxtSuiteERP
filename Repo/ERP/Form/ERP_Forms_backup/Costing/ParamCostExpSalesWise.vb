Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamCostExpSalesWise
    Inherits System.Windows.Forms.Form

    'Dim PvtDBCn As ADODB.Connection			

    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    Private Const ColCustomerCode As Short = 1
    Private Const ColCustomerName As Short = 2
    Private Const ColProdCode As Short = 3
    Private Const ColProdDesc As Short = 4
    Private Const ColCustPartNo As Short = 5
    Private Const ColCostWEF As Short = 6
    Private Const ColRMCost As Short = 7
    Private Const ColBOPCost As Short = 8
    Private Const ColPaintCost As Short = 9
    Private Const ColPowderCost As Short = 10
    Private Const ColPlatingCost As Short = 11
    Private Const ColOprCost As Short = 12
    Private Const ColProcessCost As Short = 13
    Private Const ColOHCost As Short = 14
    Private Const ColHandlingCost As Short = 15
    Private Const ColToolCost As Short = 16
    Private Const ColInterest As Short = 17
    Private Const ColPackMaterialCost As Short = 18
    Private Const ColRejCost As Short = 19
    Private Const ColProfit As Short = 20
    Private Const ColTransport As Short = 21
    Private Const ColSalePrice As Short = 22
    Private Const ColPONO As Short = 23
    Private Const ColPODate As Short = 24
    Private Const ColPOWEF As Short = 25
    Private Const ColPORate As Short = 26
    Private Const ColSalesQty As Short = 27

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

    Private Sub chkAllProduct_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllProduct.CheckStateChanged
        Call PrintStatus(False)
        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtProduct.Enabled = False
            cmdsearchProduct.Enabled = False
        Else
            txtProduct.Enabled = True
            cmdsearchProduct.Enabled = True
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
        mSubTitle = "As On Date : " & VB6.Format(txtDateAsOn.Text, "DD/MM/YYYY") ''& " To : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")			
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
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtProduct.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtProduct.Text = AcName
            txtProduct.Focus()
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchProduct.Click
        SearchItem()
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
    Private Sub frmParamCostExpSalesWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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

    Private Sub frmParamCostExpSalesWise_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamCostExpSalesWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllProduct.CheckState = System.Windows.Forms.CheckState.Checked
        txtProduct.Enabled = False
        cmdsearchProduct.Enabled = False

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamCostExpSalesWise_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamCostExpSalesWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub txtDateAsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateAsOn.TextChanged
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

    Private Sub txtdateAsOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateAsOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateAsOn.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateAsOn.Text) = True Then
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
        With SprdMain
            .MaxCols = ColSalesQty
            .set_RowHeight(0, RowHeight * 1)
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

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdCode, 6)

            .Col = ColProdDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdDesc, 25)

            .Col = ColCustPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustPartNo, 12)
            .ColHidden = False

            .Col = ColCostWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCostWEF, 12)
            .ColHidden = False
            .ColsFrozen = ColCustomerName

            For cntCol = ColRMCost To ColSalePrice
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColPONO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColPONO, 12)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColPODate, 8)

            .Col = ColPOWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColPOWEF, 8)
            .ColHidden = False

            For cntCol = ColPORate To ColSalesQty
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

            .Row = 0
            .Col = ColSalesQty
            .Text = "Sale Qty"
        End With
        Call FillHeading()
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mCustCode As String
        Dim mProdCode As String
        Dim mSalePrice As Double
        Dim pPORate As Double
        Dim pPONO As String
        Dim pPODate As String
        Dim pPOWEF As String
        Dim mSaleQty As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = MakeSQL()


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColCustomerCode
                mCustCode = Trim(.Text)

                .Col = ColProdCode
                mProdCode = Trim(.Text)

                .Col = ColSalePrice
                mSalePrice = Val(.Text)

                pPONO = ""
                pPODate = ""
                pPOWEF = ""
                pPORate = 0

                If GetSODetail(mProdCode, mCustCode, pPONO, pPODate, pPOWEF, pPORate) = False Then GoTo LedgError

                .Col = ColPONO
                .Text = Trim(pPONO)

                .Col = ColPODate
                .Text = Trim(pPODate)

                .Col = ColPOWEF
                .Text = Trim(pPOWEF)

                .Col = ColPORate
                .Text = VB6.Format(pPORate, "0.000")

                .Col = ColSalesQty
                mSaleQty = GetSaleQty(mProdCode, mCustCode)
                .Text = VB6.Format(mSaleQty, "0.000")
            Next
        End With


        '''********************************			
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetSODetail(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pPONO As String, ByRef pPODate As String, ByRef pPOWEF As String, ByRef pPORate As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim pAUTO_KEY_SO As Double

        If GetActivePO(pItemCode, xCustomerCode, pAUTO_KEY_SO) = False Then GoTo ErrPart

        SqlStr = "SELECT IH.CUST_PO_NO, IH.CUST_PO_DATE, ID.AMEND_WEF, ITEM_PRICE AS ITEM_PRICE, AUTO_KEY_SO" & vbCrLf & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.MKEY = ("


        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.AUTO_KEY_SO=" & pAUTO_KEY_SO & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtDateAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '            Do While RsTemp.EOF = False			
            '                pAUTO_KEY_SO = Val(IIf(IsNull(RsTemp.Fields("AUTO_KEY_SO").Value), -1, RsTemp.Fields("AUTO_KEY_SO").Value))			
            '                If GetActivePO(pItemCode, xCustomerCode, pAUTO_KEY_SO) = True Then			
            pPONO = IIf(IsDBNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
            pPODate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CUST_PO_DATE").Value), 0, RsTemp.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")
            pPOWEF = VB6.Format(IIf(IsDBNull(RsTemp.Fields("AMEND_WEF").Value), 0, RsTemp.Fields("AMEND_WEF").Value), "DD/MM/YYYY")
            pPORate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
            '                    Exit Do			
            '                End If			
            '                RsTemp.MoveNext			
            '            Loop			
        End If
        GetSODetail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSODetail = False
    End Function
    Private Function GetSaleQty(ByRef pItemCode As String, ByRef xCustomerCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetSaleQty = 0
        SqlStr = "SELECT SUM(ITEM_QTY) AS ITEM_QTY" & vbCrLf & " FROM  FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_INVTYPE_MST IV " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND CANCELLED='N' AND REJECTION='N' AND AGTD3='N' AND FOC='N'" ''AGTCT3='N'			

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=IV.COMPANY_CODE AND IH.TRNTYPE=IV.CODE"
        SqlStr = SqlStr & vbCrLf & " AND IV.ISSUPPBILL='N'"

        SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSaleQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetActivePO(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pAUTO_KEY_SO As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        'Dim mFieldName As String			

        GetActivePO = False
        pAUTO_KEY_SO = -1

        SqlStr = "SELECT AUTO_KEY_SO FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.SO_STATUS='O' " ''& vbCrLf |'                & " AND IH.AMEND_NO = ("			
        '			
        '        SqlStr = SqlStr & "SELECT MAX(SIH.AMEND_NO) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _			
        ''                & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _			
        ''                & " AND SIH.AUTO_KEY_SO=" & pAUTO_KEY_SO & " " & vbCrLf _			
        ''                & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _			
        ''                & " AND SID.ITEM_CODE='" & pItemCode & "')"			

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pAUTO_KEY_SO = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SO").Value), -1, RsTemp.Fields("AUTO_KEY_SO").Value)
        End If
        GetActivePO = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetActivePO = False
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        ''''SELECT CLAUSE...			
        '			
        '    MakeSQL = " SELECT  " & vbCrLf _			
        ''            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _			
        ''            & " IH.PRODUCT_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, " & vbCrLf _			
        ''            & " IH.WEF, IH.TOT_RM_COST, IH.TOT_BOP_COST, " & vbCrLf _			
        ''            & " PLT_TOT_COST_PC +  PNT_TOT_COST_PC + PDR_TOT_COST_PC, TOT_OPR_COST, " & vbCrLf _			
        ''            & " OVERHEAD_COST, TOT_PACK_COST, REJ_COST, " & vbCrLf _			
        ''            & " PROFIT_COST, TRANSPORT_COST, TOT_SALE_PRICE, " & vbCrLf _			
        ''            & " '', '', '', 0, 0 "			

        MakeSQL = " SELECT  " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " IH.PRODUCT_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, " & vbCrLf & " IH.WEF, IH.TOT_RM_COST, IH.TOT_BOP_COST, " & vbCrLf & " PNT_TOT_COST_PC, PDR_TOT_COST_PC, PLT_TOT_COST_PC, TOT_WELD_COST, TOT_PROCESS_COST, " & vbCrLf & " OVERHEAD_COST, TOT_HANDLING_COST,TOT_TOOL_COST,TOT_INTEREST_COST, TOT_PACK_MAT_COST, REJ_COST, " & vbCrLf & " PROFIT_COST, TRANSPORT_COST, TOT_SALE_PRICE, " & vbCrLf & " '', '', '', 0, 0 "

        ''''FROM CLAUSE...			
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_CUST_FG_COST_HDR IH, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST"


        ''''WHERE CLAUSE...			
        MakeSQL = MakeSQL & vbCrLf & " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=IMST.ITEM_CODE"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND CMST.SUPP_CUST_NAME ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "
        End If

        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND IMST.ITEM_SHORT_DESC ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' "
        End If


        MakeSQL = MakeSQL & vbCrLf & " AND MKEY = (SELECT MAX(MKEY) FROM PRD_CUST_FG_COST_HDR A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND A.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND A.PRODUCT_CODE=IMST.ITEM_CODE " & vbCrLf & " AND A.WEF <= TO_DATE('" & VB6.Format(txtDateAsOn.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')) "

        '    MakeSQL = MakeSQL & vbCrLf _			
        ''            & " GROUP BY SUPP_CUST_CODE, SUPP_CUST_NAME, " & vbCrLf _			
        ''            & " ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO"			

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME,IH.PRODUCT_CODE "
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1


        If Trim(txtDateAsOn.Text) = "" Then
            MsgInformation("Date is blank.")
            txtDateAsOn.Focus()
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
        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtProduct.Text) = "" Then
                MsgInformation("Product is blank.")
                txtProduct.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateTo.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateTo.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtProduct_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.DoubleClick
        SearchItem()
    End Sub

    Private Sub txtProduct_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProduct.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProduct.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
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
    Private Sub txtProduct_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProduct.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub

    Private Sub txtProduct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProduct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtProduct.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable(txtProduct.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtProduct.Text = UCase(Trim(txtProduct.Text))
        Else
            MsgInformation("No Such Product in Product Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillHeading()
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColSalesQty
            .Row = 0
            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColProdCode
            .Text = "Product Code"

            .Col = ColProdDesc
            .Text = "Product Name"

            .Col = ColCustPartNo
            .Text = "Cust Part No"

            .Col = ColCostWEF
            .Text = "W.E.F."

            .Col = ColRMCost
            .Text = "R.M. Cost"

            .Col = ColBOPCost
            .Text = "B.O.P. Cost"

            .Col = ColPaintCost
            .Text = "Paint Cost"

            .Col = ColPowderCost
            .Text = "Powder Cost"

            .Col = ColPlatingCost
            .Text = "Plating Cost"

            .Col = ColOprCost
            .Text = "Weld Cost"

            .Col = ColProcessCost
            .Text = "Process Cost"

            .Col = ColOHCost
            .Text = "Over Head Cost"

            .Col = ColHandlingCost
            .Text = "Handling Cost"

            .Col = ColToolCost
            .Text = "Tool Cost"

            .Col = ColInterest
            .Text = "Interest"

            .Col = ColPackMaterialCost
            .Text = "Packing Material Cost"

            .Col = ColRejCost
            .Text = "Rejection Cost"

            .Col = ColProfit
            .Text = "Profit"

            .Col = ColTransport
            .Text = "Other Cost"

            .Col = ColSalePrice
            .Text = "Sale Price"

            .Col = ColPONO
            .Text = "PO No"

            .Col = ColPODate
            .Text = "PO Date"

            .Col = ColPOWEF
            .Text = "PO W.E.F."

            .Col = ColPORate
            .Text = "PO Rate"

            .Col = ColSalesQty
            .Text = "Sales Qty"
        End With
    End Sub
End Class
