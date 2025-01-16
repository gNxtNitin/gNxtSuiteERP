Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLTAReg
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
    Private Const ColDesg As Short = 3
    Private Const ColBankAcct As Short = 4
    Private Const ColPaymentMode As Short = 5
    Private Const ColTourAllwFeb As Short = 6
    Private Const ColTourAllwMar As Short = 7
    Private Const ColTourAllwApr As Short = 8
    Private Const ColTourAllwMay As Short = 9
    Private Const ColTourAllwJun As Short = 10
    Private Const ColTourAllwJul As Short = 11
    Private Const ColTourAllwAug As Short = 12
    Private Const ColTourAllwSep As Short = 13
    Private Const ColTourAllwOct As Short = 14
    Private Const ColTourAllwNov As Short = 15
    Private Const ColTourAllwDec As Short = 16
    Private Const ColTourAllwJan As Short = 17

    Private Const ColTourAllwArrear As Short = 18
    Private Const ColTotBasic As Short = 19
    Private Const ColTotalTourAllw As Short = 20
    Private Const ColBankIFSC As Short = 21

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAddDeduct
            .MaxCols = ColTotalTourAllw
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight * 2)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)
            .TypeMaxEditLen = 5000

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 18)
            .TypeMaxEditLen = 5000

            .ColsFrozen = ColName

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesg, 18)
            .ColHidden = True

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


            For cntCol = ColTourAllwFeb To ColTotalTourAllw
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 9)
            Next

        End With

        Call FillHeading()
        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 1, sprdAddDeduct.MaxCols)
        sprdAddDeduct.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdAddDeduct.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdAddDeduct, mRow)
        '    MainClass.SetSpreadColor sprdAddDeduct, mRow
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        With sprdAddDeduct
            .MaxCols = ColTotalTourAllw
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColBankAcct
            .Text = "Bank Account"

            .Col = ColPaymentMode
            .Text = "Payment Mode"

            .Col = ColTourAllwFeb
            .Text = "February"

            .Col = ColTourAllwMar
            .Text = "March"

            .Col = ColTourAllwApr
            .Text = "April"

            .Col = ColTourAllwMay
            .Text = "May"

            .Col = ColTourAllwJun
            .Text = "June"

            .Col = ColTourAllwJul
            .Text = "July"

            .Col = ColTourAllwAug
            .Text = "August"

            .Col = ColTourAllwSep
            .Text = "September"

            .Col = ColTourAllwOct
            .Text = "October"

            .Col = ColTourAllwNov
            .Text = "November"

            .Col = ColTourAllwDec
            .Text = "December"

            .Col = ColTourAllwJan
            .Text = "January"

            .Col = ColTourAllwArrear
            .Text = "Arrear"

            .Col = ColTotBasic
            .Text = "Total payable"

            .Col = ColTotalTourAllw
            .Text = "Total Allowance"
        End With
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


        'Insert Data from Grid to PrintDummyData Table...

        Call MainClass.ClearCRptFormulas(Report1)

        frmPrintOTReg.optCheckList.Text = "Register"
        frmPrintOTReg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintOTReg.optCheckList.Checked = True Then
            mTitle = "Tour Allowance -  Register"

            mRptFileName = "Bonus.Rpt"
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
            If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, ColCode, ColName, 0, ColPaymentMode, ColTotalTourAllw, ColBankAcct, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1


            mRptFileName = "BankSheet.Rpt"

            '        mBankName = InputBox("Please Enter Bank Name. :", "Bank Name")

            If mBankName = "" Then
                mTitle = "BANK ANNEXURES"
            Else
                mTitle = "BANK ANNEXURES OF " & mBankName
            End If

            mSubTitle = "Tour Allowance For the Preiod From : " & txtFrom.Text & " To " & txtTo.Text

        ElseIf frmPrintOTReg.optCash.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, "CASH", False) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, ColCode, ColName, 0, ColPaymentMode, ColTotalTourAllw, ColBankAcct, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1

            mRptFileName = "SalCashSheet.Rpt"
            mTitle = "Bonus (Cash)"
        ElseIf frmPrintOTReg.optBankTxt.Checked = True Then
            '        If CreateTxtFileForBankOLD = False Then GoTo ERR1
            mTitle = "BY TA OF " & Year(CDate(txtFrom.Text)) & "-" & Year(CDate(txtTo.Text))

            If CreateTxtFileForBank(sprdAddDeduct, ColCode, ColName, ColPaymentMode, ColBankAcct, ColTotalTourAllw, mBankName, mTitle, sprdAddDeduct.MaxRows - 1) = False Then GoTo ERR1


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
                .Col = ColTotalTourAllw
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

            GridName.Col = ColTotalTourAllw
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

                GridName.Col = ColTotalTourAllw
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
                        .Col = ColTotalTourAllw
                        If Val(.Text) > 0 Then

                            .Col = ColBankAcct
                            Print(1, TAB(0), Trim(.Text))

                            .Col = ColName
                            mEmpName = VB.Left(Trim(.Text), 60)
                            Print(1, TAB(17), mEmpName)

                            .Col = ColTotalTourAllw
                            mAmount = New String(" ", 16 - Len(Trim(.Text))) & Trim(.Text)
                            Print(1, TAB(76), mAmount)

                            Print(1, TAB(94), "BY TA OF " & Year(CDate(txtFrom.Text)) & "-" & Year(CDate(txtTo.Text)))

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
                For cntCol = ColTourAllwApr To ColTotalTourAllw
                    .Col = cntCol
                    arrsal(cntCol) = arrsal(cntCol) + Val(sprdAddDeduct.Text)
                Next
            Next

            .Row = .MaxRows
            For cntCol = ColTourAllwApr To ColTotalTourAllw
                .Col = cntCol
                sprdAddDeduct.Text = CStr(arrsal(cntCol))
                .Font = VB6.FontChangeBold(.Font, True)
            Next


        End With

    End Sub
    Private Sub frmLTAReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmLTAReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        OptName.Checked = True
        FillHeading()
        FillDeptCombo()
        txtFrom.Text = VB6.Format("01/02/" & Year(RsCompany.Fields("START_DATE").Value), "dd/mm/yyyy")
        txtTo.Text = VB6.Format("31/01/" & Year(RsCompany.Fields("END_DATE").Value), "dd/mm/yyyy")
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked


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

        MainClass.ClearGrid(sprdAddDeduct)


        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then

        Else
            Exit Sub
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If


        SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, MAX(TRN.DESG_DESC) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS February, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS March, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS April," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS May, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS June, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS July, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS August, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS September, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS October, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS November, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS December, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' AND ISARREAR='N' THEN TRN.PAYABLESALARY ELSE 0 END)) AS January, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN ISARREAR='Y' OR ISARREAR='O' OR ISARREAR='V' THEN TRN.PAYABLESALARY ELSE 0 END)) AS Arrear, " & vbCrLf & " TO_CHAR(SUM(TRN.PAYABLESALARY)) AS Basic, "

        If CDate(VB6.Format(txtFrom.Text, "DD/MM/YYYY")) >= CDate("01/02/2009") Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ROUND(SUM(TRN.PAYABLESALARY)* 2.33 /100,0)) AS TourAllw "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ROUND(SUM(TRN.PAYABLESALARY)* 4.33 /100,0)) AS TourAllw "
        End If

        SqlStr = SqlStr & vbCrLf & " From PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST SALHEAD, PAY_SAL_TRN TRN " & vbCrLf & " WHERE  TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE " & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND SALHEAD.TYPE=" & ConPF & ""

        SqlStr = SqlStr & vbCrLf & " AND TRN.SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TRN.SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & " AND EMP_STOP_SALARY='N' "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='2'"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.PAYABLESALARY)>0"

        SqlStr = SqlStr & vbCrLf & " GROUP by EMP.EMP_CODE, EMP.EMP_NAME,EMP.EMP_BANK_NO, EMP.PAYMENTMODE"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
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
        '                .Col = ColTourAllwCalc
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBonuscalc)
        '
        '                .Col = ColTourAllwRate
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBonusRate)
        '
        '                .Col = ColTourAllwPayable
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBPayable)
        '                mTotBonus = mTotBonus + (mBPayable)
        '
        '                .Col = ColTotalTourAllw
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
        '                .Col = ColTourAllwPayable
        '                .Text = .Text + Chr(13) + Chr(13) & MainClass.FormatRupees(mTotBonus)
        '
        '                .Col = ColTotalTourAllw
        '                .Text = MainClass.FormatRupees(mTotBonus)
        '            End With
        '        End If
        '        sprdAddDeduct.RowHeight(mRow) = sprdAddDeduct.MaxTextRowHeight(mRow)
        '    End If
    End Function

    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
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
