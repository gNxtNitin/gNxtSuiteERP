Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSuppCostRegComp
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
    Private Const ColRMGrade As Short = 6
    Private Const ColGrossRMCost As Short = 7
    Private Const ColGrossWt As Short = 8
    Private Const ColScrapWt As Short = 9
    Private Const ColNetWt As Short = 10
    Private Const ColScrapRMCost As Short = 11
    Private Const ColNetRMCost As Short = 12

    Private Const ColGrossRMCost1 As Short = 13
    Private Const ColGrossWt1 As Short = 14
    Private Const ColScrapWt1 As Short = 15
    Private Const ColNetWt1 As Short = 16
    Private Const ColScrapRMCost1 As Short = 17
    Private Const ColNetRMCost1 As Short = 18

    Private Const ColNetRMDiff As Short = 19
    Private Const ColPONO As Short = 20
    Private Const ColPODate As Short = 21
    Private Const ColPOWEF As Short = 22
    Private Const ColPORate As Short = 23
    Private Const ColDiff As Short = 24

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


        mTitle = "Supplier Costing Comparision Register"
        mSubTitle = "As On Date : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") ''& " To : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")		
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SUPPCOSTREG.rpt"
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
    Private Sub frmParamSuppCostRegComp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Supplier BOP Costing Register"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamSuppCostRegComp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamSuppCostRegComp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")		

        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSuppCostRegComp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamSuppCostRegComp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        With SprdMain
            .MaxCols = ColDiff
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

            '.Col = ColCostWEF
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '.TypeEditLen = 255
            '.TypeEditMultiLine = False
            '.set_ColWidth(ColCostWEF, 12)
            '.ColHidden = False
            .ColsFrozen = ColCustomerName

            For cntCol = ColGrossRMCost To ColNetRMDiff
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

            For cntCol = ColPORate To ColDiff
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
        With SprdMain
            .MaxCols = ColDiff
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

            .Col = ColRMGrade
            .Text = "RM Grade"

            .Col = ColGrossRMCost
            .Text = "RM Rate - A"

            .Col = ColGrossWt
            .Text = "Gross Wt. - A"

            .Col = ColScrapWt
            .Text = "Scrap Wt. - A"

            .Col = ColNetWt
            .Text = "Scrap Wt. - A"

            .Col = ColScrapRMCost
            .Text = "Scrap Cost - A"

            .Col = ColNetRMCost
            .Text = "Gross RM Cost - A"

            .Col = ColGrossRMCost1
            .Text = "RM Rate - B"

            .Col = ColGrossWt1
            .Text = "Gross Wt. - B"

            .Col = ColScrapWt1
            .Text = "Scrap Wt. - B"

            .Col = ColNetWt1
            .Text = "Scrap Wt. - B"

            .Col = ColScrapRMCost1
            .Text = "Scrap Cost - B"

            .Col = ColNetRMCost1
            .Text = "Gross RM Cost - B"

            .Col = ColNetRMDiff
            .Text = "Net RM Diff"

            .Col = ColPONO
            .Text = "PO No"

            .Col = ColPODate
            .Text = "PO Date"

            .Col = ColPOWEF
            .Text = "PO W.E.F."

            .Col = ColPORate
            .Text = "PO Rate"

            .Col = ColDiff
            .Text = "PO Rate Diff"
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String
        Dim i As Integer
        Dim mCustCode As String
        Dim mProdCode As String
        Dim mSalePrice As Double
        Dim pPORate As Double
        Dim pPONO As String
        Dim pPODate As String
        Dim pPOWEF As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = MakeSQL()


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColCustomerCode
                mCustCode = Trim(.Text)

                .Col = ColProdCode
                mProdCode = Trim(.Text)

                '.Col = ColNetCost
                'mSalePrice = Val(.Text)

                pPONO = ""
                pPODate = ""
                pPOWEF = ""
                pPORate = 0

                If GetPODetail(mProdCode, mCustCode, pPONO, pPODate, pPOWEF, pPORate) = False Then GoTo LedgError

                .Col = ColPONO
                .Text = Trim(pPONO)

                .Col = ColPODate
                .Text = Trim(pPODate)

                .Col = ColPOWEF
                .Text = Trim(pPOWEF)

                .Col = ColPORate
                .Text = VB6.Format(pPORate, "0.000")

                .Col = ColDiff
                .Text = VB6.Format(mSalePrice - pPORate, "0.000")
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

    Private Function GetPODetail(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pPONO As String, ByRef pPODate As String, ByRef pPOWEF As String, ByRef pPORate As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        'Dim pAUTO_KEY_PO As Double		

        '        If GetActivePO(pItemCode, xCustomerCode, pAUTO_KEY_SO) = False Then GoTo ErrPart		

        SqlStr = "SELECT IH.AUTO_KEY_PO, IH.PUR_ORD_DATE, ID.PO_WEF_DATE, ITEM_PRICE - (ITEM_PRICE*ITEM_DIS_PER/100) AS ITEM_PRICE" & vbCrLf & " FROM  PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "' AND IH.PUR_TYPE='P' AND IH.ORDER_TYPE='O'" & vbCrLf & " AND IH.MKEY = ("


        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  PUR_PURCHASE_HDR SIH, PUR_PURCHASE_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND SID.ITEM_CODE='" & pItemCode & "' AND SIH.PUR_TYPE='P' AND SIH.ORDER_TYPE='O'" & vbCrLf & " AND SID.PO_WEF_DATE <=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"


        ''AND SUBSTR(SIH.AUTO_KEY_PO,LENGTH(SIH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""		
        'SqlStr = SqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""		

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '            Do While RsTemp.EOF = False		
            '                pAUTO_KEY_SO = Val(IIf(IsNull(RsTemp.Fields("AUTO_KEY_SO").Value), -1, RsTemp.Fields("AUTO_KEY_SO").Value))		
            '                If GetActivePO(pItemCode, xCustomerCode, pAUTO_KEY_SO) = True Then		
            pPONO = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value)
            pPODate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUR_ORD_DATE").Value), 0, RsTemp.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
            pPOWEF = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PO_WEF_DATE").Value), 0, RsTemp.Fields("PO_WEF_DATE").Value), "DD/MM/YYYY")
            pPORate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
            '                    Exit Do		
            '                End If		
            '                RsTemp.MoveNext		
            '            Loop		
        End If
        GetPODetail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPODetail = False
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

        MakeSQL = " SELECT  " & vbCrLf _
            & " SUPP_CUST_CODE, SUPP_CUST_NAME, " & vbCrLf _
            & " ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, MTRL_DESC, MAX(RATE_PCS) AS RATE_PCS, " & vbCrLf _
            & " MAX(GROSS_WT_PCS) AS GROSS_WT_PCS, MAX(GROSS_WT_SCRAP), MAX(NET_WT_PCS) AS NET_WT_PCS, MAX(COST_SCRAP) AS COST_SCRAP, MAX(NET_COST_PCS) AS NET_COST_PCS," & vbCrLf _
            & " MAX(RATE_PCS1) AS RATE_PCS1, " & vbCrLf _
            & " MAX(GROSS_WT_PCS1) AS GROSS_WT_PCS1,MAX(GROSS_WT_SCRAP1) AS GROSS_WT_SCRAP1, MAX(NET_WT_PCS1) AS NET_WT_PCS1, MAX(COST_SCRAP1) AS COST_SCRAP1, MAX(NET_COST_PCS1) AS NET_COST_PCS1," & vbCrLf _
            & " MAX(NET_COST_PCS1)-MAX(NET_COST_PCS), 0, 0, 0, 0, 0, 0 FROM (" & vbCrLf

        MakeSQL = MakeSQL & vbCrLf & " SELECT  " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " IH.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, RM.MTRL_DESC, ID.RATE_PCS, " & vbCrLf _
            & " ID.GROSS_WT_PCS,ID.GROSS_WT_SCRAP, ID.NET_WT_PCS, ID.COST_SCRAP, ID.NET_COST_PCS," & vbCrLf _
            & " 0 AS RATE_PCS1, " & vbCrLf _
            & " 0 AS GROSS_WT_PCS1,0 AS GROSS_WT_SCRAP1,0 AS NET_WT_PCS1, 0 AS COST_SCRAP1,0 AS NET_COST_PCS1," & vbCrLf _
            & " 0, 0, 0, 0, 0, 0 " & vbCrLf _
            & " FROM PRD_BOP_COST_HDR IH, PRD_BOP_COST_DET ID, PRD_MTRL_MST RM,FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY AND IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.COMPANY_CODE=RM.COMPANY_CODE" & vbCrLf _
            & " AND ID.RM_CODE=RM.MTRL_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.ITEM_CODE=IMST.ITEM_CODE"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND CMST.SUPP_CUST_NAME ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "
        End If

        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND IMST.ITEM_SHORT_DESC ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.WEF = TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        MakeSQL = MakeSQL & vbCrLf & " UNION ALL  "

        MakeSQL = MakeSQL & vbCrLf & " SELECT  " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " IH.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, RM.MTRL_DESC, " & vbCrLf _
            & " 0 AS RATE_PCS, 0 AS GROSS_WT_PCS,0 AS GROSS_WT_SCRAP, 0 AS NET_WT_PCS, 0 AS COST_SCRAP, 0 AS NET_COST_PCS," & vbCrLf _
            & " ID.RATE_PCS AS RATE_PCS1, " & vbCrLf _
            & " ID.GROSS_WT_PCS AS GROSS_WT_PCS1,ID.GROSS_WT_SCRAP AS GROSS_WT_SCRAP1, ID.NET_WT_PCS AS NET_WT_PCS1, ID.COST_SCRAP AS COST_SCRAP1, ID.NET_COST_PCS AS NET_COST_PCS1," & vbCrLf _
            & " 0, 0, 0, 0, 0, 0 " & vbCrLf _
            & " FROM PRD_BOP_COST_HDR IH, PRD_BOP_COST_DET ID, PRD_MTRL_MST RM,FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY And IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And IH.COMPANY_CODE=RM.COMPANY_CODE" & vbCrLf _
            & " And ID.RM_CODE=RM.MTRL_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & " And IH.ITEM_CODE=IMST.ITEM_CODE"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf _
                & " And CMST.SUPP_CUST_NAME ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "
        End If

        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND IMST.ITEM_SHORT_DESC ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.WEF = TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        MakeSQL = MakeSQL & vbCrLf & ")"
        MakeSQL = MakeSQL & vbCrLf & "GROUP BY SUPP_CUST_CODE, SUPP_CUST_NAME, ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, MTRL_DESC"

        MakeSQL = MakeSQL & vbCrLf & "  ORDER BY 2,3, 6"
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
End Class
