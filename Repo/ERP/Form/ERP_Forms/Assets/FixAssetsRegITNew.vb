Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmFixAssetsRegITNew
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    '''Private PvtDBCn As ADODB.Connection			

    Dim mAccountCode As String


    Private Const ColLocked As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColMRRNo As Short = 4
    Private Const ColMRRDate As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColItemDesc As Short = 8
    Private Const ColPartyName As Short = 9
    Private Const ColDateUsed As Short = 10
    Private Const ColBillAmount As Short = 11
    Private Const ColTotalCost As Short = 12
    Private Const ColCenvatClaimed As Short = 13
    Private Const ColCenvatRecd As Short = 14
    Private Const ColCessClaimed As Short = 15
    Private Const ColCessRecd As Short = 16
    Private Const ColServiceTax As Short = 17
    Private Const ColSTClaimed As Short = 18
    Private Const ColTotalClaimed As Short = 19
    Private Const ColExchangeRate As Short = 20
    Private Const ColSubsidy As Short = 21
    Private Const ColNetCost As Short = 22
    Private Const ColBookType As Short = 23
    Private Const ColBookSubType As Short = 24
    Private Const ColMKEY As Short = 25

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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

    Private Sub chkAllDepr_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDepr.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDeprMode.Enabled = False
            cmdsearchDepr.Enabled = False
        Else
            txtDeprMode.Enabled = True
            cmdsearchDepr.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonFA(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonFA(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonFA(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim CntLst As Integer
        Dim mInvoiceType As String

        Report1.Reset()
        mTitle = "Fixed Assets Register (As Required Under Income Tax Law, 1956)"
        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                Exit For
            End If
        Next

        mSubTitle = mInvoiceType & " (" & Year(RsCompany.Fields("START_DATE").Value) & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY") & ")"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FixedAssetsRegIT.RPT"

        SqlStr = MakeSQL()

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdsearchDepr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchDepr.Click
        SearchDepr()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
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
    Private Sub frmFixAssetsRegITNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Fixed Assets Register (Income Tax)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmFixAssetsRegITNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection			
        ''PvtDBCn.Open StrConn			

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        Call FillInvoiceType()
        optType(2).Checked = True
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        txtDeprMode.Enabled = False
        cmdsearchDepr.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmFixAssetsRegITNew_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmFixAssetsRegITNew_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String			
        'Dim xVDate As String			
        'Dim xMkey As String			
        'Dim xVNo As String			
        'Dim xBookType As String			
        'Dim xBookSubType As String			
        'Dim pIndex As Long			
        'Dim xVTYPE As String			
        '			
        '   SprdMain.Row = SprdMain.ActiveRow			
        '			
        '    SprdMain.Col = ColVDate			
        '    xVDate = Me.SprdMain.Text			
        '			
        '    SprdMain.Col = ColMkey			
        '    xMkey = Me.SprdMain.Text			
        '			
        '    SprdMain.Col = ColVNo			
        '    xVNo = Me.SprdMain.Text			
        '			
        '    SprdMain.Col = ColBookType			
        '    xBookType = Me.SprdMain.Text			
        '			
        '    SprdMain.Col = ColBookSubType			
        '    xBookSubType = Me.SprdMain.Text			
        '			
        '    If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then			
        ''            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)			
        ''            xVNo = Right(xVNo, 5)			
        '        SqlStr = "COMPANY_CODE=" & RsCompany!Company_Code & "" & vbCrLf _			
        ''                & " AND FYEAR=" & RsCompany!FYEAR & "" & vbCrLf _			
        ''                & " AND MKEY='" & xMkey & "'" & vbCrLf _			
        ''                & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf _			
        ''                & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf _			
        ''                & " AND VDATE='" & vb6.Format(xVDate, "DD-MMM-YYYY") & "'"			
        '			
        '        If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo, , SqlStr) = True Then			
        '            xVTYPE = MasterNo			
        '            xVNo = Mid(xVNo, Len(xVTYPE) + 1)			
        '        Else			
        '            Exit Sub			
        '        End If			
        '    End If			
        '			
        '    Call ShowTrn(xMkey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType)			

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True			
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        'UPGRADE_WARNING: Untranslated statement in txtAccount_Validate. Please check source code.			
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.5)
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

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 9)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 7)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 9)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 4000
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)

            .Col = ColDateUsed
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDateUsed, 8)

            .ColsFrozen = ColBillNo

            For cntCol = ColBillAmount To ColNetCost
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


            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 8)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 8)
            .ColHidden = True


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With

        Call FillHeading()
    End Sub
    Private Function Show1() As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String

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
        Dim SqlStr As String
        Dim mGroupCode As String
        Dim mAccountCode As String
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String

        SqlStr = " SELECT  '', PV_NO, TO_CHAR(PV_DATE,'DD/MM/YYYY'),  " & vbCrLf & " MRR_NO, MRR_DATE, " & vbCrLf & " BILL_NO, BILL_DATE, " & vbCrLf & " ITEM_DESC, SUPP_CUST_NAME, '', " & vbCrLf & " TOTAL_COST, CD_AMOUNT, OTH_AMOUNT, " & vbCrLf & " 0, 0," & vbCrLf & " 0, 0, 0," & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " PHY_VARIFICATION, LOCATION, REMARKS, " & vbCrLf & " 0 "

        SqlStr = SqlStr & vbCrLf & " FROM AST_ASSET_TRN " '''& vbCrLf |			
        ''''WHERE CLAUSE...			
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.			
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            End If
        Next

        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GROUP_CODE IN " & mTrnTypeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.			

            'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.			

            SqlStr = SqlStr & vbCrLf & "AND GROUP_CODE='" & MainClass.AllowSingleQuote(mGroupCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND PV_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mTrnTypeSelect As Boolean
        Dim CntLst As Integer
        Dim mInvoiceType As String

        'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.			
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus			
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus			

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        mTrnTypeSelect = False
        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnTypeSelect = True
                    Exit For
                End If
            End If
        Next

        If mTrnTypeSelect = False Then
            MsgInformation("Nothing to show")
            FieldsVerification = False
            Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mBillAmount As Double
        Dim mSaleAmount As Double
        Dim mBED As Double
        Dim mCST As Double
        Dim mHGST As Double
        Dim mSurcharge As Double
        Dim mFreight As Double
        Dim mDiscount As Double
        Dim mMSC As Double
        Dim mOthCharges As Double

        '    With SprdMain			
        '        For cntRow = 1 To .MaxRows			
        '            .Row = cntRow			
        '			
        '            .Col = ColBillAmount			
        '            mBillAmount = mBillAmount + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))			
        '			
        '            .Col = ColSALEVALUE1			
        '            mSaleAmount = mSaleAmount + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))			
        '			
        '            .Col = ColCustomDuty			
        '            mBED = mBED + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))			
        '			
        '            .Col = ColInstCharges			
        '            mCST = mCST + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))			
        '			
        '            .Col = ColTotalCost			
        '            mHGST = mHGST + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))			
        '			
        '            .Col = ColOPBal			
        '            mOthCharges = mOthCharges + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))			
        '        Next			
        '			
        '        Call MainClass.AddBlankfpSprdRow(SprdMain, ColVNo)			
        '        .Col = ColPartyName			
        '        .Row = .MaxRows			
        '        .Text = "GRAND TOTAL :"			
        '        .FontBold = True			
        '			
        '        .Row = .MaxRows			
        '        .Row2 = .MaxRows			
        '        .Col = 1			
        '        .col2 = .MaxCols			
        '        .BlockMode = True			
        '        .BackColor = &H8000000F     ''&H80FF80			
        '        .BlockMode = False			
        '			
        '        .Row = .MaxRows			
        '			
        '        .Col = ColBillAmount			
        '        .Text = Format(mBillAmount, "0.00")			
        '			
        '        .Col = ColSALEVALUE1			
        '        .Text = Format(mSaleAmount, "0.00")			
        '			
        '        .Col = ColCustomDuty			
        '        .Text = Format(mBED, "0.00")			
        '			
        '        .Col = ColInstCharges			
        '        .Text = Format(mCST, "0.00")			
        '			
        '        .Col = ColTotalCost			
        '        .Text = Format(mHGST, "0.00")			
        '			
        '        .Col = ColOPBal			
        '        .Text = Format(mOthCharges, "0.00")			
        '			
        '    End With			
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FillInvoiceType()
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE='F' ORDER BY SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'UPGRADE_WARNING: Untranslated statement in txtdateFrom_Validate. Please check source code.			
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then			
        '        txtDateFrom.SetFocus			
        '        Cancel = True			
        '        Exit Sub			
        '    End If			
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'UPGRADE_WARNING: Untranslated statement in txtdateTo_Validate. Please check source code.			
        '    If FYChk(CDate(txtDateTo.Text)) = False Then			
        '        txtDateTo.SetFocus			
        '        Cancel = True			
        '        Exit Sub			
        '    End If			
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillHeading()
        On Error GoTo ErrPart

        With SprdMain

            .Row = 0

            '        .Col = ColLocked			
            '        .Text = "Locked"			
            '			
            '        .Col = ColVNo			
            '        .Text = "VR. No."			
            '			
            '        .Col = ColVDate			
            '        .Text = "VR. Date"			
            '			
            '        .Col = ColMRRNo			
            '        .Text = "MRR No."			
            '			
            '        .Col = ColMRRDate			
            '        .Text = "MRR Date"			
            '			
            '        .Col = ColBillNo			
            '        .Text = "Bill No."			
            '			
            '        .Col = ColBillDate			
            '        .Text = "Bill Date"			
            '			
            '        .Col = ColItemDesc			
            '        .Text = "Brief Description of Asset & Identification No., If Any"			
            '			
            '        .Col = ColPartyName			
            '        .Text = "Supplier's Name"			
            '			
            '        .Col = ColBillAmount			
            '        .Text = "Invoice Value"			
            '			
            '        .Col = ColCustomDuty			
            '        .Text = "Custom Duty, Insurance & Freight"			
            '			
            '        .Col = ColInstCharges			
            '        .Text = "Installation And Other Incidental Charges etc."			
            '			
            '        .Col = ColTotalCost			
            '        .Text = "Total Cost"			
            '			
            '        .Col = ColOPBal			
            '        .Text = "Balance b/f From Earlier Years"			
            '			
            '        .Col = ColSALEVALUE1			
            '        .Text = "Additon/ Sale/ Transfer/ Scrap During The Year"			
            '			
            '        .Col = ColDEP1			
            '        .Text = "DEP. @33.33% p.a." & Year(RsCompany!Start_Date) & "-" & vb6.Format(RsCompany!END_DATE, "YY")			
            '			
            '        .Col = ColWDV1			
            '        .Text = "W.D.V. as on " & vb6.Format(RsCompany!END_DATE, "DD.MM.YYYY")			
            '			
            '        .Col = ColSALEVALUE2			
            '        .Text = "Sale/ Transfer/ Scrap During The Year"			
            '			
            '        .Col = ColDEP2			
            '        .Text = "DEP. @33.33% p.a." & Year(RsCompany!Start_Date) & "-" & vb6.Format(RsCompany!END_DATE, "YY")			
            '			
            '        .Col = ColWDV2			
            '        .Text = "W.D.V. as on " & vb6.Format(RsCompany!END_DATE, "DD.MM.YYYY")			
            '			
            '        .Col = ColSALEVALUE3			
            '        .Text = "Additon/ Sale/ Transfer/ Scrap During The Year"			
            '			
            '        .Col = ColDEP3			
            '        .Text = "DEP. @25% p.a." & Year(RsCompany!Start_Date) & "-" & vb6.Format(RsCompany!END_DATE, "YY")			
            '			
            '        .Col = ColWDV3			
            '        .Text = "W.D.V. as on " & vb6.Format(RsCompany!END_DATE, "DD.MM.YYYY")			
            '			
            '        .Col = ColPhyVari			
            '        .Text = "Physical Verification On Date & By Whom"			
            '			
            '        .Col = ColTrnName			
            '        .Text = "Remarks"			
            '			
            '        .Col = ColLoc			
            '        .Text = "Location"			
            '			
            '        .Col = ColMKEY			
            '        .Text = "MKey"			

        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub
    Private Sub txtDeprMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeprMode.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDeprMode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeprMode.DoubleClick
        SearchDepr()
    End Sub

    Private Sub SearchDepr()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster txtDeprMode, "AST_DEPRECIATION_MODE_MST", "NAME", SqlStr			
        MainClass.SearchGridMaster(txtDeprMode.Text, "AST_DEPRECIATION_MODE_MST", "MODE_DESC", "MODE_CODE", "MODE_TYPE",  , SqlStr)
        If AcName <> "" Then
            txtDeprMode.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDeprMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeprMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeprMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeprMode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeprMode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchDepr()
    End Sub

    Private Sub txtDeprMode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeprMode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtDeprMode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'UPGRADE_WARNING: Untranslated statement in txtDeprMode_Validate. Please check source code.			
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
