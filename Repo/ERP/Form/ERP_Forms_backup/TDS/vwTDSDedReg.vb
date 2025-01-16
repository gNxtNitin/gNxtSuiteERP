Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTDSDedReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection					
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColPartyName As Short = 6
    Private Const ColAmountPaid As Short = 7
    Private Const ColTDSRate As Short = 8
    Private Const ColDeductAmt As Short = 9

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Dim Font1 As String
        Dim Font2 As String
        Dim Font3 As String
        Dim mOption As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ' Set printing options for spreadsheet					

        Call SetColWidth()

        SprdLedg.PrintJobName = RsCompany.Fields("COMPANY_NAME").Value
        Font1 = "/fn""Arial""/fz""14""/fb1"
        Font2 = "/fn""Arial""/fz""10""/fb0"
        Font3 = "/fn""Arial""/fz""10""/fb1"

        If OptType(0).Checked = True Then
            mOption = " - (All)"
        ElseIf OptType(1).Checked = True Then
            mOption = " - (Companies)"
        ElseIf OptType(2).Checked = True Then
            mOption = " - (Other Than Companies)"
        End If

        SprdLedg.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("COMPANY_NAME").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1" & TxtAccount.Text & mOption & "FROM : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""

        'SprdMain.PrintFooter = "/cPrint Footer/rPage #/p/n2nd Line"					


        Call SpreadPrint(SprdLedg)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)

        LedgInfo()

        CalcSubTotal()
        FormatSprdLedg()
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1

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

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Public Sub frmViewTDSDedReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmViewTDSDedReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        OptType(0).Checked = True

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

        SqlStr = " Select '1' ,BookType,BookSubType,TO_CHAR(Vdate,'DD/MM/YYYY') AS VDate, " & vbCrLf & " Vno AS V_No, ACM.SUPP_CUST_NAME AS PartyName, " & vbCrLf & " TO_CHAR(AMOUNTPAID) AS AMOUNTPAID,TO_CHAR(TDSRATE) AS TDSRATE, " & vbCrLf & " TO_CHAR(TDSAMOUNT) As Amount " & vbCrLf & " FROM TDS_TRN TDSTRN, FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE TDSTRN.PARTYCODE = ACM.SUPP_CUST_CODE " & vbCrLf & " AND TDSTRN.Company_Code= ACM.Company_Code  " & vbCrLf _
            & " AND TDSTRN.Vdate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TDSTRN.Vdate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TDSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TDSTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TDSTRN.AccountCode = '" & mAccountCode & "'"


        If OptType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.CTYPE='C'"
        ElseIf OptType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.CTYPE='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TDSTRN.PARTYNAME<>'-1'"
        SqlStr = SqlStr & vbCrLf & " AND TDSTRN.CANCELLED='N'"
        SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME,TDSTRN.Vdate,TDSTRN.Vno, " & vbCrLf & " TDSTRN.BOOKTYPE,TDSTRN.BOOKSUBTYPE,TDSTRN.SUBROWNO "

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Sub FormatSprdLedg()
        With SprdLedg
            .MaxCols = ColDeductAmt
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColLocked, 1)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookType, 1)
            .ColHidden = True

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookSubType, 1)
            .ColHidden = True


            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVDate, 8)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVNo, 11)

            .Col = ColDeductAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDeductAmt, 10)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 38)

            .Col = ColAmountPaid
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColAmountPaid, 10)

            .Col = ColTDSRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColTDSRate, 7)

            Call FillHeading()

            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub frmViewTDSDedReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
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
                    If UCase(VB.Right(mFilename, 3)) = "XLS" Then
                        If SprdLedg.ExportToExcel(mFilename, "TDSREG", "") = True Then
                            MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                        End If
                    Else
                        If SprdLedg.ExportToHTML(mFilename, False, "") = True Then
                            MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                        End If
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
        ''Resume					
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

        FraPreview.Visible = True
        FraPreview.BringToFront()

        Call SetColWidth()

        If OptType(0).Checked = True Then
            mOption = " - (All)"
        ElseIf OptType(1).Checked = True Then
            mOption = " - (Companies)"
        ElseIf OptType(2).Checked = True Then
            mOption = " - (Other Than Companies)"
        End If
        ''SprdMain.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!CompanyName & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1" & cboAccount.Text & " " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""					
        SprdLedg.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("COMPANY_NAME").Value & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1" & TxtAccount.Text & mOption & " From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""

        Call SpreadSheetPreview(SprdLedg, SprdPreview, SprdCommand, ClientRectangle.Width - 300, ClientRectangle.Height - 300)
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
    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True

        SqlStr = "DELETE FROM PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertSelectedAcct()

        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "TDS Account Ledger"
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        mReportFileName = "TDSLedger.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        SqlStr = "DELETE FROM PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume					
    End Sub
    Private Sub InsertSelectedAcct()
        On Error GoTo ERR1
        Dim mLocked As String
        Dim mVDate As String
        Dim mVNo As String
        Dim mPartyName As String
        Dim mSection As String
        Dim mNarration As String
        Dim mAmountPaid As String
        Dim mTdsRate As String
        Dim mDeductAmt As String
        Dim mMkey As String
        Dim mSubRowNo As String
        Dim mTDSAccountName As String
        Dim SqlStr As String
        Dim cntRow As Integer


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        mTDSAccountName = TxtAccount.Text

        SqlStr = ""
        With SprdLedg

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColLocked
                mLocked = Trim(.Text)
                .Col = ColVDate
                mVDate = Trim(.Text)
                .Col = ColVNo
                mVNo = Trim(.Text)
                .Col = ColPartyName
                mPartyName = Trim(.Text)

                .Col = ColAmountPaid
                mAmountPaid = Trim(.Text)
                .Col = ColTDSRate
                mTdsRate = Trim(.Text)
                .Col = ColDeductAmt
                mDeductAmt = Trim(.Text)


                SqlStr = "Insert into PrintDummyData (UserID,SubRow,Field1," & vbCrLf & " Field2,Field3,Field4,Field5,Field6,Field7," & vbCrLf & " Field8,Field9,Field10,Field11,Field12,Field13) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow + 1 & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mLocked) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVDate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSection) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mNarration) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mAmountPaid) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mTdsRate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDeductAmt) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mMkey) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSubRowNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mTDSAccountName) & "') "

                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume					
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies					
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY Field1,SUBROW"

        FetchRecordForReport = mSqlStr

    End Function






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
            .Col = ColLocked
            .Text = "Locked"

            .Col = ColVDate
            .Text = "Date"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColDeductAmt
            .Text = "Deducted Amount"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColAmountPaid
            .Text = "Amount Paid / Credited"

            .Col = ColTDSRate
            .Text = "Rate at Which deducted"

        End With

    End Sub

    Private Sub CalcSubTotal()
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mPartyName As String
        Dim StartRow As Integer
        Dim EndRow As Integer
        Dim GAmountPaid As Double
        Dim GDeductAmt As Double

        Call MainClass.AddBlankfpSprdRow(SprdLedg, ColVDate)
        With SprdLedg
            .Row = .MaxRows
            .Col = ColPartyName
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "GRAND TOTAL"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H80FF80)
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmountPaid
                GAmountPaid = GAmountPaid + Val(.Text)

                .Col = ColDeductAmt
                GDeductAmt = GDeductAmt + Val(.Text)
            Next

            .Row = .MaxRows
            .Col = ColAmountPaid
            .Text = CStr(GAmountPaid)
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColDeductAmt
            .Text = CStr(GDeductAmt)
            .Font = VB6.FontChangeBold(.Font, True)

        End With
        ''    Call CalcRowTotal(SprdLedg, ColAmountPaid, 1, ColAmountPaid, SprdLedg.MaxRows - 1, SprdLedg.MaxRows, ColAmountPaid)					
        ''    Call CalcRowTotal(SprdLedg, ColDeductAmt, 1, ColDeductAmt, SprdLedg.MaxRows - 1, SprdLedg.MaxRows, ColDeductAmt)					

        cntRow = 1
        StartRow = 1
        With SprdLedg
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColPartyName
                If mPartyName <> .Text And cntRow <> 1 Then
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow


                    EndRow = cntRow
                    .Font = VB6.FontChangeBold(.Font, True)
                    .Text = "SUB TOTAL"

                    Call CalcRowTotal(SprdLedg, ColAmountPaid, StartRow, ColAmountPaid, EndRow - 1, EndRow, ColAmountPaid)
                    Call CalcRowTotal(SprdLedg, ColDeductAmt, StartRow, ColDeductAmt, EndRow - 1, EndRow, ColDeductAmt)

                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF00)
                    .BlockMode = False

                    cntRow = cntRow + 1
                    .Row = cntRow
                    StartRow = cntRow
                End If
                .Col = ColPartyName
                mPartyName = .Text
                cntRow = cntRow + 1
            Loop
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
End Class
