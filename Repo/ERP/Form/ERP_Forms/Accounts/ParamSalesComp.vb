Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSalesComp
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection					

    Dim mAccountCode As Integer
    Private Const ColLocked As Short = 1
    Private Const ColPartyName As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColFromApr As Short = 4
    Private Const ColToApr As Short = 5
    Private Const ColFromMay As Short = 6
    Private Const ColToMay As Short = 7
    Private Const ColFromJun As Short = 8
    Private Const ColToJun As Short = 9
    Private Const ColFromJul As Short = 10
    Private Const ColToJul As Short = 11
    Private Const ColFromAug As Short = 12
    Private Const ColToAug As Short = 13
    Private Const ColFromSep As Short = 14
    Private Const ColToSep As Short = 15
    Private Const ColFromOct As Short = 16
    Private Const ColToOct As Short = 17
    Private Const ColFromNov As Short = 18
    Private Const ColToNov As Short = 19
    Private Const ColFromDec As Short = 20
    Private Const ColToDec As Short = 21
    Private Const ColFromJan As Short = 22
    Private Const ColToJan As Short = 23
    Private Const ColFromFeb As Short = 24
    Private Const ColToFeb As Short = 25
    Private Const ColFromMar As Short = 26
    Private Const ColToMar As Short = 27
    Private Const ColFromTotal As Short = 28
    Private Const ColToTotal As Short = 29

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExport_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExport.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4					
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSalesComp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        fraYear.Visible = IIf(lblReportType.Text = "Y", True, False)
        FraMonth.Visible = IIf(lblReportType.Text = "M", True, False)

        fraYear.Enabled = IIf(lblReportType.Text = "Y", True, False)
        FraMonth.Enabled = IIf(lblReportType.Text = "M", True, False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSalesComp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Me.Top = 0					
        'Me.Left = 0					
        'Me.Height = VB6.TwipsToPixelsY(7245)					
        'Me.Width = VB6.TwipsToPixelsX(11355)					

        lblTrnType.Text = CStr(-1)

        Call FillInvoiceType()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        Call FillComboYear()
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboYear()
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRec As Integer

        cboFrom.Items.Clear()
        cboTo.Items.Clear()
        cboMonth.Items.Clear()

        SqlStr = "SELECT FYEAR FROM GEN_CMPYRDTL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY FYEAR"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRec = -1
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                cntRec = cntRec + 1
                cboFrom.Items.Add(RsTemp.Fields("FYEAR").Value)
                cboTo.Items.Add(RsTemp.Fields("FYEAR").Value)
                RsTemp.MoveNext()
            Loop
        End If
        cboFrom.SelectedIndex = cntRec - 1
        cboTo.SelectedIndex = cntRec

        For cntRec = 1 To 12
            cboMonth.Items.Add(MonthName(cntRec, True))
        Next

        cboMonth.SelectedIndex = 0

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSalesComp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Dim MainClass_Renamed As Object
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
    Private Sub frmParamSalesComp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub



    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub FillInvoiceType()
        Dim MainClass_Renamed As Object
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer


        Dim pCompanyCode As Long
        Dim mRights As String

        lstInvoiceType.Items.Clear()

        SqlStr = "SELECT DISTINCT B.SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND A.CATEGORY='S' ORDER BY B.SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstInvoiceType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_CODE FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                pCompanyCode = RS.Fields("COMPANY_CODE").Value
                mRights = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn, pCompanyCode)
                If mRights <> "" Then
                    lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                    lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                    CntLst = CntLst + 1
                End If
                RS.MoveNext()
            Loop
        End If

        lstCompanyName.SelectedIndex = 0


        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboRejection.Items.Clear()
        cboCancelled.Items.Clear()
        cboExport.Items.Clear()
        cboCT3.Items.Clear()
        cboLocation.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboRejection.Items.Add("BOTH")
        cboRejection.Items.Add("YES")
        cboRejection.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboExport.Items.Add("BOTH")
        cboExport.Items.Add("YES")
        cboExport.Items.Add("NO")

        cboCT3.Items.Add("BOTH")
        cboCT3.Items.Add("YES")
        cboCT3.Items.Add("NO")

        cboAgtD3.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboRejection.SelectedIndex = 0
        cboCancelled.SelectedIndex = 0
        cboExport.SelectedIndex = 0
        cboCT3.SelectedIndex = 0

        SqlStr = "SELECT DISTINCT DESP_LOCATION FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " ORDER BY DESP_LOCATION"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboLocation.Items.Clear()
        cboLocation.Items.Add("All")

        Do While RS.EOF = False
            cboLocation.Items.Add(IIf(IsDBNull(RS.Fields("DESP_LOCATION").Value), "", RS.Fields("DESP_LOCATION").Value))
            RS.MoveNext()
        Loop

        cboLocation.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim MainClass_Renamed As Object
        Dim mCol1 As Integer
        Dim mCol2 As Integer

        If eventArgs.row = 0 Then
            mCol1 = eventArgs.col
            '        mCol2 = IIf(mCol1 = 1, 2, 1)					
            mCol2 = IIf(optWise(1).Checked = True, 2, 1)
            Call MainClass.SortGrid(SprdMain, mCol1, mCol2)
            Exit Sub
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        '    SprdMain.Row = -1					
        '    SprdMain.Col = Col					
        '    SprdMain.DAutoCellTypes = True					
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH					
        '    SprdMain.TypeEditLen = 1000					
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub cboFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFrom.TextChanged
        Call PrintStatus(False)
    End Sub



    Private Sub cboTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr					
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        Dim MainClass_Renamed As Object
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = IIf(lblReportType.Text = "Y", ColToTotal, ColToJul)
            .set_RowHeight(0, RowHeight * 2.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True


            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 25)
            .ColHidden = IIf(optWise(0).Checked = True Or optWise(2).Checked = True Or optWise(3).Checked = True, False, True)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 25)
            .ColHidden = IIf(optWise(1).Checked = True Or optWise(2).Checked = True, False, True)

            .ColsFrozen = IIf(optWise(0).Checked = True Or optWise(2).Checked = True, ColPartyName, ColItemName)

            For cntCol = ColFromApr To IIf(lblReportType.Text = "Y", ColToTotal, ColToJul)
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

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo LedgError
        Dim SqlStr As String

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
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mAmount As String

        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String
        Dim mSubCatCode As String
        Dim mAccountCode As String
        Dim mItemCode As String
        Dim mShowAll As Boolean

        Dim mEDAmount As String
        Dim mCessAmount As String
        Dim mSHECESSAmount As String
        Dim mItemAmount As String
        Dim mQty As String

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String



        ''''SELECT CLAUSE...					
        If lblReportType.Text = "Y" Then
            If optWise(0).Checked = True Or optWise(3).Checked = True Then
                If optBase(1).Checked = True Then
                    mAmount = "TOTQTY"
                Else
                    If optAmountType(0).Checked = True Then
                        mAmount = "ITEMVALUE"
                    Else
                        mAmount = "NETCGST_AMOUNT + NETSGST_AMOUNT + NETIGST_AMOUNT "
                    End If
                End If
            Else
                If optBase(1).Checked = True Then
                    mAmount = "ID.ITEM_QTY"
                Else
                    If optAmountType(0).Checked = True Then
                        mAmount = "ID.ITEM_AMT"
                    Else
                        mAmount = "ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT "

                    End If
                End If
            End If
        End If

        If optWise(0).Checked = True Then
            MakeSQL = " SELECT '', SUPP_CUST_NAME, '', "
        ElseIf optWise(1).Checked = True Then
            MakeSQL = " SELECT '', '', INVMST.ITEM_SHORT_DESC, "
        ElseIf optWise(2).Checked = True Then
            MakeSQL = " SELECT '', SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC, "
        ElseIf optWise(3).Checked = True Then
            MakeSQL = " SELECT '', IMST.NAME, '', "
        End If

        If lblReportType.Text = "Y" Then
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='APR' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS ARP_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='APR' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS ARP_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAY' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS MAY_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAY' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS MAY_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUN' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS JUN_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUN' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS JUN_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUL' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS JUL_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUL' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS JUL_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='AUG' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS AUG_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='AUG' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS AUG_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='SEP' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS SEP_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='SEP' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS SEP_TO, "

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='OCT' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS OCT_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='OCT' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS OCT_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='NOV' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS NOV_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='NOV' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS NOV_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='DEC' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS DEC_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='DEC' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS DEC_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JAN' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS JAN_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JAN' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS JAN_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='FEB' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS FEB_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='FEB' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS FEB_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAR' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS MAR_FROM, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAR' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS MAR_TO, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.FYEAR='" & cboFrom.Text & "' THEN " & mAmount & " ELSE 0 END)) AS TOTAL_FROM, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN IH.FYEAR='" & cboTo.Text & "' THEN " & mAmount & " ELSE 0 END)) AS TOTAL_TO "
        Else
            If optWise(0).Checked = True Or optWise(3).Checked = True Then
                mQty = "IH.TOTQTY"
                mItemAmount = "IH.ITEMVALUE"
                mEDAmount = "IH.NETCGST_AMOUNT"
                mCessAmount = "IH.NETSGST_AMOUNT"
                mSHECESSAmount = "IH.NETIGST_AMOUNT"
            Else
                mQty = "ID.ITEM_QTY"
                mItemAmount = "ID.ITEM_AMT"
                mEDAmount = "ID.CGST_AMOUNT"
                mCessAmount = "ID.SGST_AMOUNT"
                mSHECESSAmount = "ID.IGST_AMOUNT"
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mItemAmount & " ELSE 0 END)) AS ASS_FROM, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mEDAmount & " ELSE 0 END)) AS MDT_FROM, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mCessAmount & " ELSE 0 END)) AS CESS_FROM, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboFrom.Text & "' THEN " & mSHECESSAmount & " ELSE 0 END)) AS SHECESS_FROM, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mItemAmount & " ELSE 0 END)) AS ASS_TO, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mEDAmount & " ELSE 0 END)) AS MDT_TO, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mCessAmount & " ELSE 0 END)) AS CESS_TO, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='" & UCase(cboMonth.Text) & "' AND IH.FYEAR='" & cboTo.Text & "' THEN " & mSHECESSAmount & " ELSE 0 END)) AS SHECESS_TO "

        End If

        ''''FROM CLAUSE...					

        If optWise(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH,FIN_SUPP_CUST_MST CMST"
        ElseIf optWise(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, INV_ITEM_MST INVMST"
        ElseIf optWise(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID,FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"
        Else
            MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH,FIN_SUPP_CUST_MST CMST,FIN_INVTYPE_MST IMST"
        End If

        ''''WHERE CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE "

        '& vbCrLf _
        '    & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If optWise(0).Checked = True Or optWise(2).Checked = True Or optWise(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"
        End If

        If optWise(1).Checked = True Or optWise(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"
        End If

        If optWise(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IH.TRNTYPE=IMST.CODE"
        End If

        mShowAll = True
        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst					
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
            End If
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            '        MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTD3='" & Left(cboAgtD3.Text, 1) & "'"					
            If cboAgtD3.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboCT3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If
        '					
        '    If Trim(txtTariffHeading.Text) <> "" Then					
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"					
        '    End If					
        '					
        '    If Trim(txtVehicleNo.Text) <> "" Then					
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicleNo.Text) & "'"					
        '    End If					
        '					
        If cboExport.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboLocation.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DESP_LOCATION='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If


        '    MakeSQL = MakeSQL & vbCrLf _					
        ''            & " AND TO_CHAR(IH.INVOICE_DATE,'YYYY')>='" & cboFrom.Text & "'" & vbCrLf _					
        ''            & " ANDTO_CHAR(IH.INVOICE_DATE,'YYYY')<='" & cboTo.Text & "'"					

        MakeSQL = MakeSQL & vbCrLf & " AND IH.FYEAR IN ('" & cboFrom.Text & "','" & cboTo.Text & "')"

        If lblReportType.Text = "M" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'MON')>='" & UCase(cboMonth.Text) & "'" & vbCrLf _
                & " AND TO_CHAR(IH.INVOICE_DATE,'MON')<='" & UCase(cboMonth.Text) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            'txtAccount_Validating(TxtAccount, New System.ComponentModel.CancelEventArgs(False))

            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "") = True Then
                lblAcCode.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
            End If
        End If

        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.INVOICE_DATE<='20-sep-2012'"					

        ''''GROUP BY CLAUSE...					
        If optWise(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY SUPP_CUST_NAME"
        ElseIf optWise(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY INVMST.ITEM_SHORT_DESC"
        ElseIf optWise(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC"
        Else
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY IMST.NAME"
        End If
        ''''ORDER BY CLAUSE...					
        If optWise(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY SUPP_CUST_NAME"
        ElseIf optWise(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC"
        ElseIf optWise(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC"
        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IMST.NAME"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If cboFrom.SelectedIndex = -1 Then
            MsgInformation("Please select From Year")
            cboFrom.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If cboTo.SelectedIndex = -1 Then
            MsgInformation("Please select To Year")
            cboTo.Focus()
            FieldsVerification = False
            Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mFromApr As Double
        Dim mToApr As Double
        Dim mFromMay As Double
        Dim mToMay As Double
        Dim mFromJun As Double
        Dim mToJun As Double
        Dim mFromJul As Double
        Dim mToJul As Double
        Dim mFromAug As Double
        Dim mToAug As Double
        Dim mFromSep As Double
        Dim mToSep As Double
        Dim mFromOct As Double
        Dim mToOct As Double
        Dim mFromNov As Double
        Dim mToNov As Double
        Dim mFromDec As Double
        Dim mToDec As Double
        Dim mFromJan As Double
        Dim mToJan As Double
        Dim mFromFeb As Double
        Dim mToFeb As Double
        Dim mFromMar As Double
        Dim mToMar As Double
        Dim mFromTotal As Double
        Dim mToTotal As Double


        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColFromApr
                mFromApr = mFromApr + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColToApr
                mToApr = mToApr + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))


                .Col = ColFromMay
                mFromMay = mFromMay + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColToMay
                mToMay = mToMay + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColFromJun
                mFromJun = mFromJun + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColToJun
                mToJun = mToJun + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColFromJul
                mFromJul = mFromJul + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColToJul
                mToJul = mToJul + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                If lblReportType.Text = "Y" Then
                    '.Col = ColFromJul
                    'mFromJul = mFromJul + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    '.Col = ColToJul
                    'mToJul = mToJul + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromAug
                    mFromAug = mFromAug + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToAug
                    mToAug = mToAug + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromSep
                    mFromSep = mFromSep + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToSep
                    mToSep = mToSep + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromOct
                    mFromOct = mFromOct + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToOct
                    mToOct = mToOct + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromNov
                    mFromNov = mFromNov + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToNov
                    mToNov = mToNov + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromDec
                    mFromDec = mFromDec + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToDec
                    mToDec = mToDec + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromJan
                    mFromJan = mFromJan + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToJan
                    mToJan = mToJan + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromFeb
                    mFromFeb = mFromFeb + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToFeb
                    mToFeb = mToFeb + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromMar
                    mFromMar = mFromMar + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToMar
                    mToMar = mToMar + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColFromTotal
                    mFromTotal = mFromTotal + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColToTotal
                    mToTotal = mToTotal + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                End If
            Next


            Call MainClass.AddBlankfpSprdRow(SprdMain, IIf(optWise(0).Checked = True, ColPartyName, ColItemName))

            .Col = IIf(optWise(0).Checked = True, ColPartyName, ColItemName)
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H80FF80)
            .BlockMode = False

            .Row = .MaxRows



            .Col = ColFromApr
            .Text = VB6.Format(mFromApr, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColToApr
            .Text = VB6.Format(mToApr, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColFromMay
            .Text = VB6.Format(mFromMay, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColToMay
            .Text = VB6.Format(mToMay, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColFromJun
            .Text = VB6.Format(mFromJun, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColToJun
            .Text = VB6.Format(mToJun, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColFromJul
            .Text = VB6.Format(mFromJul, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColToJul
            .Text = VB6.Format(mToJul, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            If lblReportType.Text = "Y" Then
                '.Col = ColFromJul
                '.Text = VB6.Format(mFromJul, "0.00")
                '.Font = VB6.FontChangeBold(.Font, True)

                '.Col = ColToJul
                '.Text = VB6.Format(mToJul, "0.00")
                '.Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromAug
                .Text = VB6.Format(mFromAug, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToAug
                .Text = VB6.Format(mToAug, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromSep
                .Text = VB6.Format(mFromSep, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToSep
                .Text = VB6.Format(mToSep, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromOct
                .Text = VB6.Format(mFromOct, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToOct
                .Text = VB6.Format(mToOct, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromNov
                .Text = VB6.Format(mFromNov, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToNov
                .Text = VB6.Format(mToNov, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromDec
                .Text = VB6.Format(mFromDec, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToDec
                .Text = VB6.Format(mToDec, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromJan
                .Text = VB6.Format(mFromJan, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToJan
                .Text = VB6.Format(mToJan, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromFeb
                .Text = VB6.Format(mFromFeb, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToFeb
                .Text = VB6.Format(mToFeb, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromMar
                .Text = VB6.Format(mFromMar, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToMar
                .Text = VB6.Format(mToMar, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColFromTotal
                .Text = VB6.Format(mFromTotal, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColToTotal
                .Text = VB6.Format(mToTotal, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)
            End If

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillHeading()

        With SprdMain
            .Row = 0

            .Col = ColLocked
            .Text = "Locked"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColItemName
            .Text = "Item Name"

            If lblReportType.Text = "M" Then
                .Col = ColFromApr
                .Text = "Assessable Amount - " & vbNewLine & Val(cboFrom.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColToApr
                .Text = "CGST Amount - " & vbNewLine & Val(cboFrom.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColFromMay
                .Text = "SGST Amount - " & vbNewLine & Val(cboFrom.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColToMay
                .Text = "IGST Amount - " & vbNewLine & Val(cboFrom.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColFromJun
                .Text = "Assessable Amount - " & vbNewLine & Val(cboTo.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColToJun
                .Text = "CGST Amount - " & vbNewLine & Val(cboTo.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColFromJul
                .Text = "SGST Amount - " & vbNewLine & Val(cboTo.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

                .Col = ColToJul
                .Text = "IGST Amount - " & vbNewLine & Val(cboTo.Text) + IIf(cboMonth.Text = "Jan" Or cboMonth.Text = "Feb" Or cboMonth.Text = "Mar", 1, 0)

            Else
                .Col = ColFromApr
                .Text = "Apr - " & cboFrom.Text

                .Col = ColToApr
                .Text = "Apr - " & cboTo.Text

                .Col = ColFromMay
                .Text = "May - " & cboFrom.Text

                .Col = ColToMay
                .Text = "May - " & cboTo.Text

                .Col = ColFromJun
                .Text = "Jun - " & cboFrom.Text

                .Col = ColToJun
                .Text = "Jun - " & cboTo.Text

                .Col = ColFromJul
                .Text = "Jul - " & cboFrom.Text

                .Col = ColToJul
                .Text = "Jul - " & cboTo.Text

                .Col = ColFromAug
                .Text = "Aug - " & cboFrom.Text

                .Col = ColToAug
                .Text = "Aug - " & cboTo.Text

                .Col = ColFromSep
                .Text = "Sep - " & cboFrom.Text

                .Col = ColToSep
                .Text = "Sep - " & cboTo.Text

                .Col = ColFromOct
                .Text = "Oct - " & cboFrom.Text

                .Col = ColToOct
                .Text = "Oct - " & cboTo.Text

                .Col = ColFromNov
                .Text = "Nov - " & cboFrom.Text

                .Col = ColToNov
                .Text = "Nov - " & cboTo.Text

                .Col = ColFromDec
                .Text = "Dec - " & cboFrom.Text

                .Col = ColToDec
                .Text = "Dec - " & cboTo.Text

                .Col = ColFromJan
                .Text = "Jan - " & Val(cboFrom.Text) + 1

                .Col = ColToJan
                .Text = "Jan - " & Val(cboTo.Text) + 1

                .Col = ColFromFeb
                .Text = "Feb - " & Val(cboFrom.Text) + 1

                .Col = ColToFeb
                .Text = "Feb - " & Val(cboTo.Text) + 1

                .Col = ColFromMar
                .Text = "Mar - " & Val(cboFrom.Text) + 1

                .Col = ColToMar
                .Text = "Mar - " & Val(cboTo.Text) + 1

                .Col = ColFromTotal
                .Text = "Total - " & cboFrom.Text

                .Col = ColToTotal
                .Text = "Total - " & cboTo.Text
            End If

        End With

    End Sub
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String
        Dim mRPTName As String

        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mSelected As Boolean

        SqlStr = ""

        mSelected = True
        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                '            mSubTitle = IIf(mSubTitle = "", mInvoiceType, mSubTitle & "/" & mInvoiceType)					
            Else
                mSelected = False
            End If
        Next
        If mSelected = True Then
            mSubTitle = ""
        Else
            mSubTitle = " (" & mSubTitle & ")"
        End If

        '    mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text & mSubTitle					

        If cboAgtD3.SelectedIndex = 1 Then
            mSubTitle1 = "AGT D3"
        End If

        If cboFOC.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "FOC", "/FOC")
        End If

        If cboRejection.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Rejection", "/Rejetion")
        End If

        If cboCancelled.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Cancelled", "/Cancelled")
        End If

        If cboCT3.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "CT3", "/CT3")
        End If

        If cboExport.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Export", "/Export")
        End If

        '    If cboLocation.ListIndex = 1 Then					
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Export", "/Export")					
        '    End If					

        mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")

        mSubTitle = Mid(mSubTitle, 1, 254)

        If lblReportType.Text = "Y" Then

            If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, ColPartyName, ColToTotal, PubDBCn) = False Then GoTo ERR1

        Else
            If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, ColPartyName, ColToJul, PubDBCn) = False Then GoTo ERR1
        End If

        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        If lblReportType.Text = "Y" Then
            mTitle = "Sales Comparison (Year Wise)"
            mRPTName = "SalesCompYear.Rpt"
        Else
            mTitle = "Sales Comparison for the Month : " & UCase(cboMonth.Text)
            mRPTName = "SalesCompMonth.Rpt"
        End If

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume					
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        Dim MainClass_Renamed As Object
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim MainClass_Renamed As Object
        Dim mMonth As String
        Dim mFromYear As String
        Dim mToYear As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        If lblReportType.Text = "Y" Then
            mFromYear = cboFrom.Text
            mToYear = cboTo.Text
        Else
            mFromYear = cboMonth.Text & " - " & cboFrom.Text
            mToYear = cboMonth.Text & " - " & cboTo.Text
        End If

        MainClass.AssignCRptFormulas(Report1, "FromYear=""" & mFromYear & """")
        MainClass.AssignCRptFormulas(Report1, "ToYear=""" & mToYear & """")
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
End Class
