Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBonusReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDept As Short = 3
    Private Const ColDesg As Short = 4
    Private Const ColDOJ As Short = 5
    Private Const ColPeriod As Short = 6
    Private Const ColBankAcct As Short = 7
    Private Const ColPaymentMode As Short = 8
    Private Const ColTotBasic As Short = 9
    Private Const ColTotalBonus As Short = 10
    Private Const ColDOL As Short = 11
    Private Const ColBankIFSC As Short = 12

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAddDeduct
            .MaxCols = ColDOL '' ColTotalBonus
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 8)
            .TypeMaxEditLen = 5000

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 30)
            .TypeMaxEditLen = 5000

            .ColsFrozen = ColName

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 18)

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesg, 18)
            '.ColHidden = True

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOJ, 12)

            .Col = ColPeriod
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColPeriod, 10)

            .Col = ColBankAcct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 10)
            .ColHidden = True

            .Col = ColPaymentMode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 10)
            .ColHidden = True


            For cntCol = ColTotBasic To ColTotalBonus
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColTotBasic
            .ColHidden = True

            .Col = ColDOL
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOL, 10)
            .ColHidden = False
        End With

        Call FillHeading()

        MainClass.SetSpreadColor(sprdAddDeduct, -1)
        sprdAddDeduct.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
        sprdAddDeduct.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        sprdAddDeduct.DAutoCellTypes = True
        sprdAddDeduct.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdAddDeduct.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)


        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 1, sprdAddDeduct.MaxCols)
        '    MainClass.SetSpreadColor sprdAddDeduct, mRow
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        With sprdAddDeduct
            .MaxCols = ColDOL '' ColTotalBonus
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Department"

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColDOJ
            .Text = "DOJ"

            .Col = ColPeriod
            .Text = "Service Period"

            .Col = ColBankAcct
            .Text = "Bank Account"

            .Col = ColPaymentMode
            .Text = "Payment Mode"

            .Col = ColTotBasic
            .Text = "Total payable"

            .Col = ColTotalBonus
            .Text = "Total Bonus"

            .Col = ColDOL
            .Text = "Date of Leaving"
        End With
    End Sub

    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintCommand(False)
    End Sub
    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub cmdAccountPost_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAccountPost.Click
        '' myMenu = "mnuBankPayment"
        myMenu = "mnuBonus"

        ''    frmAtrn.lblBookType.Caption = ConBankPayment
        ''    frmAtrn.TxtVDate.Text = RunDate
        ''    mYM = Format(Year(RunDate), "0000") & vb6.Format(Month(RunDate), "00")
        ''    frmAtrn.lblYM.Caption = mYM
        ''    frmAtrn.lblSR.Caption = "SR"
        ''    frmAtrn.lblBookType.Caption = ConJournal
        ''    frmAtrn.Show
        ''    If CheckSalVoucher(mYM, mVNo, mVDate, mBankCode) = True Then
        ''
        ''        frmAtrn.Form_Activate
        ''        frmAtrn.TxtVDate = Format(mVDate, "dd/mm/yyyy")
        ''        frmAtrn.txtVNo1 = Format(Month(mVDate), "00")
        ''        frmAtrn.txtVno = Mid(mVNo, 3)
        ''        'If mainclass.ValidateWithMasterTable(mBankCode, "Code", "Name", "ACM", PubDBCn, MasterNo) = True Then
        ''        '    frmAtrn.CboBookName = Trim(MasterNo)
        ''        'End If
        ''
        ''        frmAtrn.txtVno_LostFocus
        ''        frmAtrn.cmdAdd.Enabled = False
        ''    End If

    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()

        Dim mRptFileName As String
        Dim mBankName As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mChequeAmount As String
        Dim pNarr As String
        'Dim mBankName As String

        'Insert Data from Grid to PrintDummyData Table...

        Call MainClass.ClearCRptFormulas(Report1)

        frmPrintOTReg.optCheckList.Text = "Register"
        frmPrintOTReg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintOTReg.optCheckList.Checked = True Then
            mTitle = "Bonus -  Register" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")

            If chkSummarised.CheckState = System.Windows.Forms.CheckState.Checked Then
                mRptFileName = "BonusSumm.Rpt"
            Else
                mRptFileName = "Bonus.Rpt"
            End If
            'FillPrintDummyData(sprdAddDeduct, 0, sprdAddDeduct.MaxRows, ColCard, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo Err1
            If FillPrintDummyData(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 0, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo ERR1

            mSubTitle = "FROM : " & txtFrom.Text & " To " & txtTo.Text

        ElseIf frmPrintOTReg.optBank.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, "CHEQUE", False) = False Then GoTo ERR1

            If frmPrintOTReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintOTReg.txtBankName.Text
            End If
            If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, ColCode, ColName, 0, ColPaymentMode, ColTotalBonus, ColBankAcct, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1



            mRptFileName = "BankSheet.Rpt"

            '        mBankName = InputBox("Please Enter Bank Name. :", "Bank Name")

            mTitle = "BANK ANNEXURES OF " & mBankName

            mSubTitle = "Bonus For the Preiod From : " & txtFrom.Text & " To " & txtTo.Text

        ElseIf frmPrintOTReg.optCash.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, "CASH", False) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, ColCode, ColName, 0, ColPaymentMode, ColTotalBonus, ColBankAcct, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1

            mRptFileName = "SalCashSheet.Rpt"
            mTitle = "Bonus (Cash)" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
        ElseIf frmPrintOTReg.optBankTxt.Checked = True Then
            '        If CreateTxtFileForBankold = False Then GoTo ERR1
            pNarr = "BY BONUS OF " & Year(CDate(txtFrom.Text)) & "-" & Year(CDate(txtTo.Text))
            If frmPrintOTReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintOTReg.txtBankName.Text
            End If
            If CreateTxtFileForBank(sprdAddDeduct, ColCode, ColName, ColPaymentMode, ColBankAcct, ColTotalBonus, mBankName, pNarr, sprdAddDeduct.MaxRows - 1) = False Then GoTo ERR1

            frmPrintOTReg.Close()
            Exit Sub
        End If


        'Select Record for print...

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        frmPrintOTReg.Close()
        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Function GetTotalAmount() As Double
        On Error GoTo ErrPart1
        Dim cntRow As Integer
        Dim mAmount As Double
        With sprdAddDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColTotalBonus
                mAmount = mAmount + IIf(IsNumeric(.Text), .Text, 0)
            Next
        End With
        GetTotalAmount = mAmount
        Exit Function

ErrPart1:
        GetTotalAmount = 0
    End Function
    Private Function FillBankDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentMode As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mWDays As String
        Dim mNetPay As String
        Dim mBankAcct As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()

        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            GridName.Col = ColCode
            mEmpCode = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColName
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColTotalBonus
            mNetPay = GridName.Text

            GridName.Col = ColBankAcct
            mBankAcct = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColPaymentMode
            If mPaymentMode = Val(GridName.Text) Then
                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mWDays & "', " & vbCrLf & " '" & mNetPay & "','" & mBankAcct & "') "
                PubDBCn.Execute(SqlStr)
            End If
        Next
        PubDBCn.CommitTrans()
        FillBankDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillBankDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FillDataIntoPrintDummy(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentType As String, ByRef mAllData As Boolean) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim mRowNum As Integer
        Dim SqlStr As String = ""
        Dim mSNo As String
        Dim mEmpCode As String
        Dim mEmpCard As String
        Dim mEmpName As String
        Dim mBankAcct As String
        Dim mNetAmt As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = "DELETE FROM Temp_PrintDummyData WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()

        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            If mAllData = True Then GoTo NextRow1
            GridName.Col = ColPaymentMode
            If UCase(GridName.Text) = UCase(mPaymentType) Then
NextRow1:
                GridName.Col = ColCode
                mEmpCode = MainClass.AllowSingleQuote(GridName.Text)

                mRowNum = IIf(mEmpCode = "", 10000 + RowNum, RowNum)

                GridName.Col = ColName
                mEmpName = MainClass.AllowSingleQuote(GridName.Text)

                GridName.Col = ColBankAcct
                mBankAcct = MainClass.AllowSingleQuote(GridName.Text)

                GridName.Col = ColTotalBonus
                mNetAmt = VB6.Format(GridName.Text, "0.00")

                SqlStr = " INSERT INTO Temp_PrintDummyData (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD4, FIELD5) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & mRowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', " & vbCrLf & " '" & mNetAmt & "','" & mBankAcct & "') "

                PubDBCn.Execute(SqlStr)
            End If
        Next
        PubDBCn.CommitTrans()
        FillDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function CreateTxtFileForBankOLD() As Boolean
        On Error GoTo ErrPart
        Dim mLineCount As Integer
        Dim pPageNo As Integer
        Dim cntRow As Double
        Dim pFileName As String
        Dim mAmount As String
        Dim mEmpName As String

        mLineCount = 1
        pFileName = mLocalPath & "\BankList.txt"
        ''Shell "ATTRIB +A -R " & pFileName

        Call ShellAndContinue("ATTRIB +A -R " & pFileName)

        With sprdAddDeduct
            If .MaxRows >= 1 Then

                FileOpen(1, pFileName, OpenMode.Output)
                For cntRow = 1 To .MaxRows - 1
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                    End If

                    .Row = cntRow

                    .Col = ColPaymentMode
                    If UCase(.Text) = "CHEQUE" Then
                        .Col = ColTotalBonus
                        If Val(.Text) > 0 Then

                            .Col = ColBankAcct
                            Print(1, TAB(0), Trim(.Text))

                            .Col = ColName
                            mEmpName = VB.Left(Trim(.Text), 60)
                            Print(1, TAB(17), mEmpName)


                            .Col = ColTotalBonus
                            mAmount = New String(" ", 18 - Len(Trim(.Text))) & Trim(.Text)
                            Print(1, TAB(76), mAmount)

                            Print(1, TAB(94), "BY BONUS OF " & Year(CDate(txtFrom.Text)) & "-" & Year(CDate(txtTo.Text)))

                            PrintLine(1, TAB(124), "C")

                            mLineCount = mLineCount + 1
                            If mLineCount = 60 Then
                                mLineCount = 1
                            End If
                        End If
                    End If
                Next
                FileClose(1)
            End If
        End With

        Shell("ATTRIB +R -A " & pFileName)
        Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)

        CreateTxtFileForBankOLD = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreateTxtFileForBankOLD = False
        ''Resume
        FileClose(1)
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        cmdAccountPost.Enabled = mPrintEnable
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        MainClass.ClearGrid(sprdAddDeduct)
        RefreshScreen()
        CalcSubTotal()
        FormatSprd(-1)
    End Sub
    Private Sub CalcSubTotal()

        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim arrsal() As Double

        Call MainClass.AddBlankfpSprdRow(sprdAddDeduct, ColName)

        ReDim arrsal(sprdAddDeduct.MaxCols)

        With sprdAddDeduct
            .Row = .MaxRows
            .Col = ColName
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "GRAND TOTAL"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                For cntCol = ColTotBasic To ColTotalBonus
                    .Col = cntCol
                    arrsal(cntCol) = arrsal(cntCol) + Val(sprdAddDeduct.Text)
                Next
            Next

            .Row = .MaxRows
            For cntCol = ColTotBasic To ColTotalBonus
                .Col = cntCol
                sprdAddDeduct.Text = CStr(arrsal(cntCol))
                .Font = VB6.FontChangeBold(.Font, True)
            Next


        End With

    End Sub
    Private Sub frmBonusReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmBonusReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        If PubSourceData = ConAccess Then
            cmdAccountPost.Enabled = False
        End If

        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        optCardNo.Checked = True
        FillHeading()
        FillDeptCombo()
        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")
        txtAsOn.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim mDeptCode As String
        Dim mBonusPer As Double
        Dim mDate As String
        MainClass.ClearGrid(sprdAddDeduct)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mDate = VB6.Format(txtTo.Text, "YYYY-MM-DD")

        SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, DEPT_DESC, GETEMPDESG ('" & RsCompany.Fields("COMPANY_CODE").Value & "',EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf _
            & " EMP.EMP_DOJ_BONUS, " & vbCrLf _
            & " ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1) AS PERIOD, " & vbCrLf _
            & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, " & vbCrLf _
            & " 0 AS Basic, "

        SqlStr = SqlStr & vbCrLf _
            & " CASE WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=1 THEN 1100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=2 THEN 1100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=3 THEN 2100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=4 THEN 3100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=5 THEN 4100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=6 THEN 5100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=7 THEN 6100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=8 THEN 7100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=9 THEN 8100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)<=10 THEN 9100" & vbCrLf _
            & " WHEN ROUND(FLOOR(months_between( DATE '" & mDate & "',EMP_DOJ_BONUS))/12,1)>10 THEN 11000 ELSE 0" & vbCrLf _
            & " END AS Bonus, "

        SqlStr = SqlStr & vbCrLf & " EMP.EMP_LEAVE_DATE "

        SqlStr = SqlStr & vbCrLf & " From PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE  EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND  EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND  EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE"


        If optExisting.Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND (EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IS_BONUS_PAYABLE='Y' "

        If chkStopSal.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_STOP_SALARY='N' "
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "'"
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        'SqlStr = SqlStr & vbCrLf & " GROUP by EMP.EMP_CODE, EMP.EMP_NAME,EMP.EMP_BANK_NO, EMP.PAYMENTMODE,EMP.EMP_LEAVE_DATE"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by DEPT.DEPT_DESC,EMP.EMP_CODE"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAddDeduct, StrConn, "Y")
        Call PrintCommand(True)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub

    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        cboDept.Items.Clear()

        SqlStr = "Select DEPT_DESC from PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        '    cboCategory.Clear
        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function FillDataInSprd(ByRef mCode As Integer, ByRef mRow As Integer, ByRef mEmpCode As String, ByRef mEmpName As String, ByRef mBankAcct As String, ByRef mPaymentMode As String) As Boolean

        'Dim RsEmpSal As ADODB.Recordset
        'Dim mStartYM As Long
        'Dim mEndYM As Long
        'Dim mSalYM As Long
        'Dim mYM As String
        'Dim mEarn As Double
        'Dim mDeduct As Double
        'Dim mNetPay As Double
        'Dim mBasicSalary As Double
        'Dim mTotBasic As Double
        'Dim mBPayable As Double
        'Dim mTotBonus As Double
        'Dim mBonusRate As Double
        'Dim mBonuscalc As Double
        'Dim mDepartment As String
        'Dim mActualBasicSal As Double
        '
        'Dim mPeriod As String
        '
        '
        '    FillDataInSprd = False
        '
        '    mStartYM = Year(txtFrom.Text) & vb6.Format(Month(txtFrom.Text), "00")
        '    mEndYM = Year(txtTo.Text) & vb6.Format(Month(txtTo.Text), "00")
        '
        '
        '    mYM = "YM BETWEEN " & mStartYM & " AND " & mEndYM & ""
        '
        '    SqlStr = " SELECT * " & vbCrLf _
        ''        & " FROM SALTRN WHERE" & vbCrLf _
        ''        & " EMPCODE =" & mCode & " AND  " & vbCrLf _
        ''        & " " & mYM & " And ISARREAR='N' AND " & vbCrLf _
        ''        & " COMPANYCODE =" & RsCompany!CompanyCode & ""
        '
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY YM"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsEmpSal, adLockOptimistic
        '
        '    sprdAddDeduct.Row = mRow
        '
        '    If RsEmpSal.EOF = False Then
        '        sprdAddDeduct.Row = mRow
        '        mYM = RsEmpSal!YM
        '        Do While Not RsEmpSal.EOF
        '            mSalYM = RsEmpSal!YM
        '            mBasicSalary = RsEmpSal!PAYABLESALARY
        '            mPeriod = Mid(MonthName(RsEmpSal!SalMONTH), 1, 3) & ", " & RsEmpSal!SALYEAR
        '            mDepartment = IIf(IsNull(RsEmpSal!Department), "", RsEmpSal!Department)
        '
        '            mActualBasicSal = RsEmpSal!BASICSALARY
        '
        '            RsEmpSal.MoveNext
        '
        '             If chkTypeAll.Value = vbUnchecked Then
        '                If cboType.ListIndex = 0 Then
        '                    If mActualBasicSal > IIf(IsNull(RsCompany!BonusLimit), 3500, RsCompany!BonusLimit) & "" Then
        '                        GoTo NextRecset
        '                    End If
        '                ElseIf cboType.ListIndex = 1 Then
        '                    If mActualBasicSal <= IIf(IsNull(RsCompany!BonusLimit), 3500, RsCompany!BonusLimit) & "" Then
        '                        GoTo NextRecset
        '                    End If
        '                End If
        '            End If
        '
        '            If Not RsEmpSal.EOF Then
        '                If mSalYM = RsEmpSal!YM Then
        '                    GoTo NextRecset
        '                End If
        '            End If
        '            With sprdAddDeduct
        '                FillDataInSprd = True
        '                .Col = ColCode
        '                .Text = mEmpCode
        '
        '                .Col = ColName
        '                .Text = mEmpName
        '
        '                .Col = ColBankAcct
        '                .Text = mBankAcct
        '
        '                 .Col = ColPaymentMode
        '                .Text = mPaymentMode
        '
        '                .Col = ColPeriod
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & mPeriod
        '
        '                .Col = ColBasic
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBasicSalary)
        '                mTotBasic = mTotBasic + mBasicSalary
        '
        '                mBPayable = CalcBonusPayable(mCode, mBasicSalary, mBonuscalc, mBonusRate, mDepartment)
        '
        '                .Col = ColBonusCalc
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBonuscalc)
        '
        '                .Col = ColBonusRate
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBonusRate)
        '
        '                .Col = ColBonusPayable
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBPayable)
        '                mTotBonus = mTotBonus + (mBPayable)
        '
        '                .Col = ColTotalBonus
        '
        '
        '            End With
        'NextRecset:
        '        Loop
        '
        '        If FillDataInSprd = True Then
        '            With sprdAddDeduct
        '                .Col = ColPeriod
        '                .Text = .Text + Chr(13) + Chr(13) & "Total :"
        '
        '                .Col = ColBasic
        '                .Text = .Text + Chr(13) + Chr(13) & MainClass.FormatRupees(mTotBasic)
        '
        '                .Col = ColBonusPayable
        '                .Text = .Text + Chr(13) + Chr(13) & MainClass.FormatRupees(mTotBonus)
        '
        '                .Col = ColTotalBonus
        '                .Text = MainClass.FormatRupees(mTotBonus)
        '            End With
        '        End If
        '        sprdAddDeduct.RowHeight(mRow) = sprdAddDeduct.MaxTextRowHeight(mRow)
        '    End If
    End Function

    Private Sub frmBonusReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAddDeduct.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub optAllEmp_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAllEmp.CheckedChanged
        If eventSender.Checked Then
            txtAsOn.Enabled = IIf(optAllEmp.Checked = True, False, True)
        End If
    End Sub

    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub optExisting_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optExisting.CheckedChanged
        If eventSender.Checked Then
            txtAsOn.Enabled = IIf(optExisting.Checked = True, True, False)
        End If
    End Sub

    Private Sub txtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtFrom.Text)) = False Then
            Cancel = True
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtTo.Text)) = False Then
            Cancel = True
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
