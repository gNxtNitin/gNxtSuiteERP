Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTDSEnqCha
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection			
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColPartyName As Short = 1
    Private Const ColSectionName As Short = 2
    Private Const ColChallanNo As Short = 3
    Private Const ColFROMDATE As Short = 4
    Private Const ColTODATE As Short = 5
    Private Const ColTDSAmount As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColDiff As Short = 8
    Private Const ColChallanDate As Short = 9

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cboConsolidated_Click()
        Call PrintStatus(False)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'"
        If MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtAccount.Text = AcName
            txtAccount_Validating(TxtAccount, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchPartyName()
        On Error GoTo ERR1
        MainClass.SearchMaster(txtPartyName.Text, "TDS_TRN", "PARTYNAME", "PARTYNAME<>'-1'")
        If AcName <> "" Then
            txtPartyName.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SearchChallan()
        On Error GoTo ERR1
        MainClass.SearchMaster(txtChallan.Text, "TDS_Challan", "CHALLANNO")
        If AcName <> "" Then
            txtChallan.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Dim Font1 As String
        Dim Font2 As String
        Dim Font3 As String
        Dim mPartyName As String
        Dim mChallanNo As String
        Dim mChallanAmt As String
        Dim mOption As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ' Set printing options for spreadsheet			

        Call SetColWidth()

        SprdLedg.PrintJobName = RsCompany.Fields("COMPANY_NAME").Value
        Font1 = "/fn""Arial""/fz""14""/fb1"
        Font2 = "/fn""Arial""/fz""10""/fb0"
        Font3 = "/fn""Arial""/fz""10""/fb1"

        If optPartyName(0).Checked = True Then
            mPartyName = " Party Name : ALL "
        Else
            mPartyName = " Party Name : " & txtPartyName.Text
        End If

        If optChallan(0).Checked = True Then
            mChallanNo = " Challan No : ALL "
        Else
            mChallanNo = " Challan No : " & txtChallan.Text
        End If


        If optChallanAmt(0).Checked = True Then
            mChallanAmt = " ChallanAmt No : ALL "
        Else
            mChallanAmt = " ChallanAmt No : " & txtChallan.Text
        End If

        mOption = mPartyName & mChallanNo & mChallanAmt

        SprdLedg.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("COMPANY_NAME").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1" & TxtAccount.Text & mOption & "FROM : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""

        'SprdMain.PrintFooter = "/cPrint Footer/rPage #/p/n2nd Line"			


        Call SpreadPrint(SprdLedg)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts()
    End Sub

    Private Sub cmdSearchP_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchP.Click
        Call SearchPartyName()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mTotValue As Double

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)

        LedgInfo()

        With SprdLedg
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            .Col = ColPartyName
            .Text = "Grand Total"

            For cntCol = ColTDSAmount To ColDiff
                mTotValue = 0
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = cntCol
                    mTotValue = mTotValue + Val(.Text)
                Next
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotValue, "0.00")
            Next
        End With

        FormatSprdLedg()
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        FieldsVerification = False

        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            mAccountCode = MasterNo
        Else
            MsgInformation("Please Select Account")
            Exit Function
        End If

        If optPartyName(1).Checked = True And Trim(txtPartyName.Text) = "" Then
            MsgInformation("Please Select Party Name")
            txtPartyName.Focus()
            Exit Function
        End If

        If optChallan(1).Checked = True And Trim(txtChallan.Text) = "" Then
            MsgInformation("Please Select Challan No")
            txtChallan.Focus()
            Exit Function
        End If

        If optChallanAmt(1).Checked = True And Trim(txtChallanAmt.Text) = "" Then
            MsgInformation("Please Select ChallanAmt No")
            txtChallanAmt.Focus()
            Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Public Sub frmViewTDSEnqCha_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmViewTDSEnqCha_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection			
        ''PvtDBCn.Open StrConn			
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        optPartyName(0).Checked = True
        optChallan(0).Checked = True
        optChallanAmt(0).Checked = True
        txtChallanAmt.Text = CStr(0)

        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub LedgInfo()
        On Error GoTo LedgError
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = " SELECT TDSTRN.PARTYNAME, " & vbCrLf & " SMST.NAME AS SECTION, TDSCHALLAN.CHALLANNO, " & vbCrLf & " TDSCHALLAN.FROMDATE, " & vbCrLf & " TDSCHALLAN.TODATE, " & vbCrLf & " TDSTRN.TDSAMOUNT, " & vbCrLf & " TDSTRN.TDSAMOUNT, " & vbCrLf & " TDSTRN.TDSAMOUNT- TDSTRN.TDSAMOUNT AS DIFF, " & vbCrLf & " TDSCHALLAN.CHALLANDATE " & vbCrLf & " From TDS_TRN TDSTRN, TDS_CHALLAN TDSCHALLAN, FIN_SUPP_CUST_MST ACM,TDS_SECTION_MST SMST " & vbCrLf & " Where TDSTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TDSCHALLAN.mKey = TDSTRN.CHALLANMKEY " & vbCrLf & " AND TDSCHALLAN.COMPANY_CODE=TDSTRN.COMPANY_CODE AND TDSCHALLAN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TDSCHALLAN.FYEAR=TDSTRN.FYEAR" & vbCrLf & " AND TDSCHALLAN.AccountCode = ACM.SUPP_CUST_CODE" & vbCrLf & " AND TDSChallan.Company_Code=SMST.Company_Code AND TDSChallan.SECTIONCODE=SMST.CODE "

        SqlStr = SqlStr & vbCrLf & " AND TDSTRN.Vdate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TDSTRN.Vdate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TDSCHALLAN.AccountCode = '" & mAccountCode & "'"

        If optPartyName(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TDSTRN.PARTYNAME='" & MainClass.AllowSingleQuote(txtPartyName.Text) & "'"
        End If

        If optChallan(1).Checked = True Then
            '        SqlStr = SqlStr & vbCrLf & " AND TDSCHALLAN.REFNO=" & Val(txtChallan.Text) & ""			
            SqlStr = SqlStr & vbCrLf & " AND TDSCHALLAN.CHALLANNO='" & MainClass.AllowSingleQuote(txtChallan.Text) & "'" ''			
        End If

        If optChallanAmt(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TDSCHALLAN.AMOUNT=" & Val(txtChallanAmt.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TDSTRN.CANCELLED='N'"

        If optOrderBy(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY TDSTRN.PARTYNAME,TDSCHALLAN.FROMDATE,TDSCHALLAN.TODATE "
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY TDSCHALLAN.CHALLANNO,TDSCHALLAN.FROMDATE,TDSCHALLAN.TODATE "
        End If
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Sub FormatSprdLedg()
        Dim cntCol As Integer
        With SprdLedg
            .MaxCols = ColChallanDate
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPartyName, 20)

            .Col = ColSectionName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColSectionName, 8)

            .Col = ColChallanNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColChallanNo, 8)

            .Col = ColFROMDATE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColFROMDATE, 9)

            .Col = ColTODATE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColTODATE, 9)

            '        .Col = ColTDSAmount			
            '        .CellType = SS_CELL_TYPE_EDIT			
            '        .TypeHAlign = SS_CELL_H_ALIGN_RIGHT			
            '        .ColWidth(ColTDSAmount) = 9			
            '			
            '        .Col = ColAmount			
            '        .CellType = SS_CELL_TYPE_EDIT			
            '        .TypeHAlign = SS_CELL_H_ALIGN_RIGHT			
            '        .ColWidth(ColAmount) = 9			
            '			
            '        .Col = ColDiff			
            '        .CellType = SS_CELL_TYPE_EDIT			
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT			
            '        .ColWidth(ColDiff) = 8			

            For cntCol = ColTDSAmount To ColDiff
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColChallanDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColChallanDate, 9)

            Call FillHeading()

            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' = OperationModeSingle			
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub frmViewTDSEnqCha_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub optChallanAmt_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optChallanAmt.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optChallanAmt.GetIndex(eventSender)
            txtChallanAmt.Enabled = IIf(optChallanAmt(0).Checked = True, False, True)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub optChallan_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optChallan.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optChallan.GetIndex(eventSender)
            txtChallan.Enabled = IIf(optChallan(0).Checked = True, False, True)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub optPartyName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPartyName.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPartyName.GetIndex(eventSender)
            txtPartyName.Enabled = IIf(optPartyName(0).Checked = True, False, True)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdCommand_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdCommand.ButtonClicked
        On Error GoTo ERR1
        Dim cntCol As Integer

        Dim mFilename As String

        SprdCommand.Col = eventArgs.col
        SprdCommand.Row = eventArgs.row

        If SprdCommand.CellType = FPSpreadADO.CellTypeConstants.CellTypeButton Then
            Select Case eventArgs.col
                Case 2 'Next			
                    ShowNextPage(SprdLedg, SprdPreview, SprdCommand, eventArgs.col)
                Case 4 'Previous			
                    ShowPreviousPage(SprdLedg, SprdPreview, SprdCommand, eventArgs.col)
                Case 6 'Zoom			
                    SprdPreview.ZoomState = 3
                Case 8 'Print			
                    cmdPrint_Click(cmdPrint, New System.EventArgs())
                Case 10 'Export			
                    'mFilename = ExportSprdToExcel(CommonDialog1)			
                    If SprdLedg.ExportToExcel(mFilename, "TDSREG", "") = True Then
                        MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                    End If

                    '''frmPageSetup.Show 1			

                Case 16 'Close			
                    FraPreview.Visible = False
                    Call SetColWidth()
            End Select
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            Exit Sub
        End If
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdCommand_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles SprdCommand.TextTipFetch
        With SprdCommand
            .Col = eventArgs.col
            .Row = eventArgs.row
            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeButton And Not .Lock Then
                eventArgs.showTip = True
                eventArgs.tipText = .TypeButtonText
            ElseIf .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit And .Text <> "" Then
                eventArgs.showTip = True
                eventArgs.tipText = .Text
            End If
        End With
    End Sub

    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
    End Sub
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
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
        Dim SqlStr As String
        On Error GoTo ERR1
        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Dim cntCol As Integer
        Dim mOption As String
        Dim mPartyName As String
        Dim mChallanNo As String
        Dim mChallanAmt As String

        FraPreview.Visible = True
        FraPreview.BringToFront()

        Call SetColWidth()

        If optPartyName(0).Checked = True Then
            mPartyName = " Party Name : ALL "
        Else
            mPartyName = " Party Name : " & txtPartyName.Text
        End If

        If optChallan(0).Checked = True Then
            mChallanNo = " Challan No : ALL "
        Else
            mChallanNo = " Challan No : " & txtChallan.Text
        End If


        If optChallanAmt(0).Checked = True Then
            mChallanAmt = " ChallanAmt No : ALL "
        Else
            mChallanAmt = " ChallanAmt No : " & txtChallan.Text
        End If

        mOption = mPartyName & mChallanNo & mChallanAmt

        ''SprdMain.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!CompanyName & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1" & cboAccount.Text & " " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""			
        SprdLedg.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("COMPANY_NAME").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1" & TxtAccount.Text & mOption & " From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""

        Call SpreadSheetPreview(SprdLedg, SprdPreview, SprdCommand, ClientRectangle.Width - 200, ClientRectangle.Height - 200)
    End Sub
    Private Sub SetColWidth()
        Dim cntCol As Integer

        With SprdLedg
            '        .Row = -1			
            '        .ColWidth(ColVDate) = 10			
            '        .ColWidth(ColVNo) = 12			
            '        .ColWidth(ColAcctName) = 25			
            '			
            '        For cntCol = ColAcctName + 1 To .MaxCols			
            '            .ColWidth(cntCol) = 12			
            '        Next			
        End With
    End Sub
    Private Sub txtChallanAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanAmt.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtChallanAmt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanAmt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtChallan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallan.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtChallan_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallan.DoubleClick
        SearchChallan()
    End Sub

    Private Sub txtChallan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallan.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtChallan.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChallan_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtChallan.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchChallan()
    End Sub


    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub FillHeading()

        With SprdLedg
            .Row = 0
            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColSectionName
            .Text = "Section Name"

            .Col = ColChallanNo
            .Text = "Challan No"

            .Col = ColFROMDATE
            .Text = "From Date"

            .Col = ColTODATE
            .Text = "To Date"

            .Col = ColTDSAmount
            .Text = "TDS Amount"

            .Col = ColAmount
            .Text = "Paid"

            .Col = ColDiff
            .Text = "Diff"

            .Col = ColChallanDate
            .Text = "Paid On"

        End With

    End Sub


    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        SearchPartyName()
    End Sub


    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPartyName()
    End Sub
End Class
