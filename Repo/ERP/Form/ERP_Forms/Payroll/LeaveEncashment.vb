Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeaveEncash
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColSal As Short = 3
    Private Const ColWDays As Short = 4
    Private Const ColTotLeave As Short = 5
    Private Const ColPaidLeave As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColDedAmount As Short = 8
    Private Const ColPFAmount As Short = 9
    Private Const ColVPFAmount As Short = 10
    Private Const ColESIAmount As Short = 11
    Private Const ColNetAmount As Short = 12
    Private Const ColPaymentMode As Short = 13
    Private Const ColBankAcct As Short = 14
    Private Const ColDOB As Short = 15
    Private Const ColDOJ As Short = 16
    Private Const ColDOL As Short = 17
    Private Const ColGROUPDOJ As Short = 18
    Private Const ColBankIFSC As Short = 19

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        With sprdLeave
            .MaxCols = ColGROUPDOJ
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 2.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 20)

            .Col = ColDOB
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOB, 10)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOJ, 10)

            .Col = ColDOL
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOL, 10)

            .Col = ColGROUPDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColGROUPDOJ, 10)

            .Col = ColBankAcct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 18)
            .ColHidden = True

            .Col = ColSal
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSal, 9)
            .ColsFrozen = ColSal

            .Col = ColWDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWDays, 6)


            .Col = ColTotLeave
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotLeave, 6)

            .Col = ColPaidLeave
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPaidLeave, 5)

            For I = ColAmount To ColNetAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8)
            Next

            .Col = ColPaymentMode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPaymentMode, 18)
            .ColHidden = True

        End With

        Call FillHeading()
        '    MainClass.ProtectCell sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols
        '    sprdLeave.OperationMode = OperationModeNormal           '' OperationModeSingle
        '    MainClass.SetSpreadColor sprdLeave, mRow

        MainClass.SetSpreadColor(sprdLeave, -1)
        MainClass.ProtectCell(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols)
        sprdLeave.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
        sprdLeave.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        sprdLeave.DAutoCellTypes = True
        sprdLeave.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdLeave.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        '    MainClass.ClearGrid sprdLeave

        With sprdLeave
            .MaxCols = ColGROUPDOJ
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDOB
            .Text = "D.O.B."

            .Col = ColDOJ
            .Text = "D.O.J."

            .Col = ColGROUPDOJ
            .Text = "Group D.O.J."

            .Col = ColDOL
            .Text = "D.O.L."

            .Col = ColBankAcct
            .Text = "Bank Account No."

            .Col = ColSal
            .Text = "Gross Salary"

            .Col = ColWDays
            .Text = "Working Days"

            .Col = ColTotLeave
            .Text = "Total Balance Leave"

            .Col = ColPaidLeave
            .Text = "Paid Leave"

            .Col = ColAmount
            .Text = "Amount"

            .Col = ColDedAmount
            .Text = "Deduct Amount"

            .Col = ColPFAmount
            .Text = "PF Amount"

            .Col = ColVPFAmount
            .Text = "VPF Amount"

            .Col = ColESIAmount
            .Text = "ESI Amount"

            .Col = ColNetAmount
            .Text = "Net Amount"

            .Col = ColPaymentMode
            .Text = "Payment Mode"

        End With
    End Sub

    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged
        PrintCommand(False)
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        PrintCommand(False)
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        PrintCommand(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub
    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
        PrintCommand(False)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub



    Private Sub cmdAccountPost_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAccountPost.Click

        Dim mVNo As String
        Dim mVDate As String
        Dim mBankCode As Integer
        Dim mYM As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mm As New frmAtrn
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mDivisionCode As Double

        '    myMenu = "mnuJournal"

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Please Select Category.")
            Exit Sub
        End If

        If Trim(cboCategory.Text) = "" Then
            MsgBox("Please Select Category.")
            Exit Sub
        End If

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Please Select Division.")
            Exit Sub
        Else
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        End If

        mm.MdiParent = Me.MdiParent
        mm.lblBookType.Text = ConJournal

        mm.txtVDate.Text = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text))
        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)
        mBType = IIf(lblBookType.Text = "C", "C", "E")
        mBSType = VB.Left(cboCategory.Text, 1)

        mm.lblSR.Text = mBType & mBSType & mDivisionCode

        mm.Show()
        If CheckSalVoucher(mYM, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, mBankCode, mBType, mBSType, mDivisionCode) = True Then

            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(mVDate, "dd/mm/yyyy")
            mm.txtVType.Text = mVType
            mm.txtVNo.Text = VB6.Format(mVSeqNo, "00000")
            mm.txtVNoSuffix.Text = mVNoSuffix

            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
            mm.CmdAdd.Enabled = False
        Else
            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text)), "dd/mm/yyyy")
            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
        End If
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
        Dim mHeading As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mChequeAmount As String
        Dim mBankName As String


        'Insert Data from Grid to PrintDummyData Table...

        Call MainClass.ClearCRptFormulas(Report1)

        frmPrintOTReg.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintOTReg.optCheckList.Checked = True Then
            mSubTitle = "AS ON : " & lblYear.Text
            mTitle = IIf(lblBookType.Text = "E", "Leave Encashment", IIf(lblBookType.Text = "I", "Leave Encashment (For Insurance)", IIf(lblBookType.Text = "P", "Leave Encashment (Arrear)", "CPL Payment")))
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mTitle = mTitle & " - " & cboCategory.Text
            End If

            mRptFileName = "LeaveEncash.Rpt"

            If FillPrintDummyData(sprdLeave, 1, sprdLeave.MaxRows, 0, sprdLeave.MaxCols, PubDBCn) = False Then GoTo ERR1
        ElseIf frmPrintOTReg.optBank.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdLeave, 1, sprdLeave.MaxRows - 2, "CHEQUE", False) = False Then GoTo ERR1

            If frmPrintOTReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintOTReg.txtBankName.Text
            End If
            If FillBankSheetIntoPrintDummy(sprdLeave, 1, sprdLeave.MaxRows - 2, ColCode, ColName, 0, ColPaymentMode, ColNetAmount, ColBankAcct, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1


            mRptFileName = "BankSheet.Rpt"

            '        mBankName = InputBox("Please Enter Bank Name. :", "Bank Name")

            If mBankName = "" Then
                mTitle = "BANK ANNEXURES"
            Else
                mTitle = "BANK ANNEXURES OF " & mBankName
            End If
            mSubTitle = "Leave Encashment For the Year : " & Year(CDate(lblRunDate.Text))

        ElseIf frmPrintOTReg.optCash.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdLeave, 1, sprdLeave.MaxRows - 2, "CASH", False) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdLeave, 1, sprdLeave.MaxRows - 2, ColCode, ColName, 0, ColPaymentMode, ColNetAmount, ColBankAcct, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1

            mRptFileName = "SalCashSheet.Rpt"

            mTitle = IIf(lblBookType.Text = "E", "Leave Encashment", IIf(lblBookType.Text = "I", "Leave Encashment (For Insurance)", IIf(lblBookType.Text = "P", "Leave Encashment (Arrear)", "CPL Payment")))
            mTitle = mTitle & " (Cash)"
        ElseIf frmPrintOTReg.optBankTxt.Checked = True Then
            '        If CreateTxtFileForBankOLD = False Then GoTo ERR1
            mTitle = "BY LE OF " & UCase(lblYear.Text)
            mBankName = Trim(frmPrintOTReg.txtBankName.Text)
            If CreateTxtFileForBank(sprdLeave, ColCode, ColName, ColPaymentMode, ColBankAcct, ColNetAmount, mBankName, mTitle, sprdLeave.MaxRows - 2) = False Then GoTo ERR1


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
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub

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

        With sprdLeave
            If .MaxRows >= 1 Then

                FileOpen(1, pFileName, OpenMode.Output)
                For cntRow = 1 To .MaxRows - 2
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                    End If

                    .Row = cntRow

                    .Col = ColPaymentMode
                    If UCase(.Text) = "CHEQUE" Then
                        .Col = ColAmount
                        If Val(.Text) > 0 Then

                            .Col = ColBankAcct
                            Print(1, TAB(0), Trim(.Text))

                            .Col = ColName
                            mEmpName = VB.Left(Trim(.Text), 60)
                            Print(1, TAB(17), mEmpName)


                            .Col = ColNetAmount
                            mAmount = New String(" ", 16 - Len(Trim(.Text))) & Trim(.Text)
                            Print(1, TAB(76), mAmount)

                            Print(1, TAB(94), "BY LE OF " & UCase(lblYear.Text))

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
        Dim mOTHour As String
        Dim mOTRate As String
        Dim mOTAmount As String
        Dim mESIC As String
        Dim mNetAmt As String
        Dim mBasicSal As String
        Dim mAdvance As String

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

                GridName.Col = ColNetAmount
                mNetAmt = GridName.Text

                GridName.Col = ColSal
                mBasicSal = GridName.Text


                SqlStr = " INSERT INTO Temp_PrintDummyData (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5, FIELD6, " & vbCrLf & " FIELD7, FIELD8, FIELD9, FIELD10) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & mRowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', " & vbCrLf & " '" & mOTHour & "','" & mNetAmt & "','" & mBankAcct & "'," & vbCrLf & " '" & mOTRate & "','" & mOTAmount & "', " & vbCrLf & " '" & mESIC & "', " & vbCrLf & " '" & mBasicSal & "','" & mAdvance & "') "


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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        RefreshScreen()
        FormatSprd(-1)
    End Sub
    Private Sub frmLeaveEncash_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
        If lblBookType.Text = "E" Then
            'UpDYear.Enabled = False
            Me.Text = "Leave Encashment"
            lblRunDate.Text = CStr(RunDate)
            lblYear.Text = CStr(Year(RunDate))
        ElseIf lblBookType.Text = "I" Then
            'UpDYear.Enabled = False
            Me.Text = "Leave Encashment (For Insurance)"
            lblRunDate.Text = CStr(RunDate)
            lblYear.Text = CStr(Year(RunDate))
            cmdAccountPost.Enabled = False
        ElseIf lblBookType.Text = "P" Then
            'UpDYear.Enabled = False
            Me.Text = "Leave Encashment (Arrear)"
            lblRunDate.Text = CStr(RunDate)
            lblYear.Text = CStr(Year(RunDate))
        Else
            'UpDYear.Enabled = True
            Me.Text = "CPL Payment"
            lblRunDate.Text = CStr(RunDate)
            RunDate = RunDate
            lblYear.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)

        End If
    End Sub
    Private Sub frmLeaveEncash_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)


        OptName.Checked = True
        FillHeading()
        FillDeptCombo()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        cboCategory.Enabled = False
        optShow(1).Checked = True

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmLeaveEncash_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdLeave.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            PrintCommand(False)
        End If
    End Sub
    Private Sub OptName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptName.CheckedChanged
        If eventSender.Checked Then
            PrintCommand(False)
        End If
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mMonth As Short
        Dim mYear As Short
        Dim cntRow As Integer
        Dim mSalary As Double
        Dim mDays As Double
        Dim mBalEL As Double
        Dim mDeptCode As String
        Dim cntCol As Integer
        Dim mASOnDate As String
        Dim mDivisionCode As Double

        MainClass.ClearGrid(sprdLeave)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                MsgInformation("Please select the Division Name.")
                cboDivision.Focus()
                Exit Sub
            End If
        End If

        If lblBookType.Text = "C" Then
            mASOnDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        Else
            mASOnDate = "31/12/" & Year(CDate(lblRunDate.Text))
        End If

        SqlStr = " Select TRN.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " TRN.BASICSALARY, " & vbCrLf & " TRN.WDAYS, TRN.TOT_LEAVES, TRN.PAID_LEAVES,  " & vbCrLf & " TRN.GROSS_AMOUNT, TRN.DED_AMOUNT, TRN.PF_AMOUNT, TRN.VPFAMOUNT, TRN.ESI_AMOUNT, TRN.NET_AMOUNT, " & vbCrLf & " DECODE(EMP.PAYMENTMODE,1, 'CASH', 'CHEQUE'), " & vbCrLf & " EMP.EMP_BANK_NO, " & vbCrLf & " TO_CHAR(EMP.EMP_DOB,'DD/MM/YYYY'), TO_CHAR(EMP.EMP_DOJ,'DD/MM/YYYY'), TO_CHAR(EMP.EMP_LEAVE_DATE,'DD/MM/YYYY'), " & vbCrLf & " TO_CHAR(EMP.EMP_GROUP_DOJ,'DD/MM/YYYY')" & vbCrLf & " FROM PAY_ENCASH_TRN TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=EMP.COMPANY_CODE  " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE  " & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PAYYEAR=" & Year(CDate(mASOnDate)) & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"


        If lblBookType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')=TO_CHAR('" & VB6.Format(mASOnDate, "YYYYMM") & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.PAYYEAR=" & Val(lblYear.Text) & ""
        End If

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND TRN.NET_AMOUNT+TRN.DED_AMOUNT>0"
        End If

        If optExisting.Checked = True Then
            If Val(lblYear.Text) = 2016 Then
                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE<=TO_DATE('" & VB6.Format(mASOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR (TRN.COMPANY_CODE=1 AND IS_CORPORATE='Y' AND EMP_LEAVE_DATE=TO_DATE('" & VB6.Format(mASOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')))" '' TRN.EMP_CODE IN ('000029','000222','000293','000538','000617','000721')))"
            Else
                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE<=TO_DATE('" & VB6.Format(mASOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If Trim(cboEmpType.Text) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME, EMP.EMP_CODE"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE, EMP.EMP_NAME"
        ElseIf OptCN.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CATG, EMP.EMP_NAME, EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CATG, EMP.EMP_CODE, EMP.EMP_NAME"
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdLeave, StrConn, "Y")

        With sprdLeave
            ColTotal(sprdLeave, ColAmount, ColNetAmount)

            .Col = ColName
            .Row = .MaxRows
            .Text = "TOTAL :"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = ColCode
            .col2 = .MaxCols
            .BlockMode = True
            .Lock = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .BlockMode = False


            MainClass.ProtectCell(sprdLeave, 1, .MaxRows, 1, .MaxCols)
        End With

        PrintCommand(True)
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        SqlStr = "Select DEPT_DESC from PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

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


        cboEmpType.Items.Clear()
        cboEmpType.Items.Add("ALL")
        cboEmpType.Items.Add("1 : Staff")
        cboEmpType.Items.Add("2 : Workers")
        cboEmpType.SelectedIndex = 0


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


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        cmdAccountPost.Enabled = mPrintEnable
    End Sub
    Private Sub UpDYear_DownClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdLeave, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdLeave, -1)
        ''RefreshScreen
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(lblYear.Text, "mm"), VB6.Format(lblYear.Text, "yyyy"))
    End Sub
End Class
