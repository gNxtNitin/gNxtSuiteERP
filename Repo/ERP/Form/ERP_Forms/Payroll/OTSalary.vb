Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOTSalary
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean

    Private Const ColSNO As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDept As Short = 3
    Private Const ColBankNo As Short = 4
    Private Const ColPaymentType As Short = 5
    Private Const ColCatgeory As Short = 6
    Private Const ColBSalary As Short = 7
    Private Const ColOT As Short = 8
    Private Const ColRate As Short = 9
    Private Const ColAmount As Short = 10
    Private Const ColESIC As Short = 11
    Private Const ColAdvance As Short = 12
    Private Const ColNetAmount As Short = 13
    Private Const ColRemarks As Short = 14
    Private Const ColDesg As Short = 15
    Private Const ColBankIFSC As Short = 16

    Private Const ConRowHeight As Short = 12

    Private Sub FillHeading(ByRef xDate As Date)

        Dim Tempdate As String
        Dim cntCol As Integer
        Dim NewDate As Date

        MainClass.ClearGrid(sprdAttn)

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        With sprdAttn
            .MaxCols = ColDesg

            .Row = ColSNO
            .set_RowHeight(ColSNO, ConRowHeight * 2)

            .Col = ColSNO
            .Text = "S. No."
            .set_ColWidth(ColSNO, 5)

            .Col = ColCard
            .Text = "Emp Card No"
            .set_ColWidth(ColCard, 6)

            .Col = ColName
            .Text = "Employees' Name "
            .set_ColWidth(ColName, 23)
            .ColsFrozen = ColName

            .Col = ColDept
            .Text = "Department"
            .set_ColWidth(ColDept, 10)
            .ColHidden = True

            .Col = ColDesg
            .Text = "Desgination"
            .set_ColWidth(ColDesg, 10)
            .ColHidden = True


            .Col = ColBankNo
            .Text = "Bank Account"
            .set_ColWidth(ColBankNo, 8)
            .ColHidden = True

            .Col = ColPaymentType
            .Text = "Payment Mode"
            .set_ColWidth(ColPaymentType, 8)
            .ColHidden = True

            .Col = ColCatgeory
            .Text = "Catgeory"
            .set_ColWidth(ColCatgeory, 8)
            .ColHidden = True

            .Col = ColBSalary
            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '    .Text = "Basic Salary"
            'Else
            .Text = "Gross Salary"
            'End If
            .set_ColWidth(ColBSalary, 8)

            .Col = ColOT
            .Text = "O.T."
            .set_ColWidth(ColOT, 8)

            .Col = ColRate
            .Text = "Rate P/H"
            .set_ColWidth(ColRate, 7)

            .Col = ColAmount
            .Text = "Amount"
            .set_ColWidth(ColAmount, 9)

            .Col = ColESIC
            .Text = "ESIC"
            .set_ColWidth(ColESIC, 7)

            .Col = ColAdvance
            .Text = "Advance"
            .set_ColWidth(ColAdvance, 7)


            .Col = ColNetAmount
            .Text = "Net Amount"
            .set_ColWidth(ColNetAmount, 9)

            .Col = ColRemarks
            .Text = "Remarks"
            .set_ColWidth(ColRemarks, 12)
            .ColHidden = True

            .Row = -1
            For cntCol = ColBSalary To ColNetAmount
                .Col = cntCol
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            Next
            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)
            MainClass.SetSpreadColor(sprdAttn, -1)
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub chkDiv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDiv.CheckStateChanged
        If chkDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
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

        If lblShowType.Text = "D" Then
            Exit Sub
        End If

        myMenu = "mnuOTSalary" ''"mnuJournal"
        mm.lblBookType.Text = ConJournal
        mm.MdiParent = Me.MdiParent

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Please Select Division First.")
            Exit Sub
        Else
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            Else
                MsgBox("Invaild Division.")
                Exit Sub
            End If
        End If

        mm.txtVDate.Text = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text))
        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)

        If lblBookType.Text = "Y" Then
            mBType = "X"
        Else
            mBType = "O"
        End If

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
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String
        Dim mBankName As String
        Dim pNarr As String

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        frmPrintOTReg.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        mSubTitle = "For the period : " & lblYear.Text

        If frmPrintOTReg.optCheckList.Checked = True Then
            mTitle = "Over Time Register" ''Production Incentive
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mTitle = mTitle & " (" & cboCategory.Text & ")"
            End If

            If lblBookType.Text = "Y" Then
                mTitle = mTitle & " - Arrear"
            End If

            mSubTitle = "For the period Paid: " & lblYear.Text

            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mSubTitle = mSubTitle & " (" & cboDept.Text & ")"
            End If

            If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mSubTitle = mSubTitle & " (" & cboDivision.Text & ")"
            End If

            mRptFileName = "OTList.Rpt"
            If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 3, "", True) = False Then GoTo ERR1
        ElseIf frmPrintOTReg.optBank.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 3, "CHEQUE", False) = False Then GoTo ERR1

            If frmPrintOTReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintOTReg.txtBankName.Text
            End If
            If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 3, ColCard, ColName, 0, ColPaymentType, ColNetAmount, ColBankNo, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1




            mRptFileName = "BankSheet.Rpt"

            '        mBankName = InputBox("Please Enter Bank Name. :", "Bank Name")

            If mBankName = "" Then
                mTitle = "BANK ANNEXURES"
            Else
                mTitle = "BANK ANNEXURES OF " & mBankName
            End If
            mSubTitle = ""
            If lblBookType.Text = "Y" Then
                mSubTitle = "Arrear - "
            End If

            mSubTitle = mSubTitle & " Over Time For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))

        ElseIf frmPrintOTReg.optCash.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 3, "CASH", False) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 3, ColCard, ColName, 0, ColPaymentType, ColNetAmount, ColBankNo, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1

            mRptFileName = "SalCashSheet.Rpt"

            If lblBookType.Text = "Y" Then
                mTitle = "Arrear - "
            End If

            mTitle = mTitle & " Over Time (Cash)"

        ElseIf frmPrintOTReg.optBankTxt.Checked = True Then
            '        If CreateTxtFileForBankOLD = False Then GoTo ERR1

            mBankName = frmPrintOTReg.txtBankName.Text
            If lblBookType.Text = "Y" Then
                pNarr = "BY OT-ARREAR OF " & UCase(lblYear.Text)
            Else
                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    pNarr = " Over Time For the Month : " & UCase(lblYear.Text)
                Else
                    pNarr = "BY OT OF " & UCase(lblYear.Text)
                End If
            End If

            If CreateTxtFileForBank(sprdAttn, ColCard, ColName, ColPaymentType, ColBankNo, ColNetAmount, mBankName, pNarr, sprdAttn.MaxRows - 3) = False Then GoTo ERR1


            frmPrintOTReg.Close()
            Exit Sub
        End If

        'Select Record for print...

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mTitle = mTitle & IIf(lblShowType.Text = "D", " (Checking Purpose)", "")
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

        With sprdAttn
            If .MaxRows >= 1 Then

                FileOpen(1, pFileName, OpenMode.Output)
                For cntRow = 1 To .MaxRows - 3
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                    End If

                    .Row = cntRow

                    .Col = ColPaymentType
                    If UCase(.Text) = "CHEQUE" Then
                        .Col = ColNetAmount
                        If Val(.Text) > 0 Then

                            .Col = ColBankNo
                            Print(1, TAB(0), Trim(.Text))

                            .Col = ColName
                            mEmpName = VB.Left(Trim(.Text), 60)
                            Print(1, TAB(17), mEmpName)


                            .Col = ColNetAmount
                            mAmount = New String(" ", 16 - Len(Trim(.Text))) & Trim(.Text)
                            Print(1, TAB(76), mAmount)

                            If lblBookType.Text = "Y" Then
                                Print(1, TAB(94), "BY OT-ARREAR OF " & UCase(lblYear.Text))
                            Else
                                Print(1, TAB(94), "BY OT OF " & UCase(lblYear.Text))
                            End If


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
    Private Function FillBankDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, Optional ByRef mPaymentMode As Integer = 0) As Boolean

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

        SqlStr = "DELETE FROM PrintDummyData WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()

        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            GridName.Col = 2
            mEmpCode = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = 3
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = GridName.MaxCols - 3
            mNetPay = GridName.Text

            GridName.Col = GridName.MaxCols - 1
            mBankAcct = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = GridName.MaxCols
            If mPaymentMode = Val(GridName.Text) Then
                SqlStr = " INSERT INTO PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mWDays & "', " & vbCrLf & " '" & mNetPay & "','" & mBankAcct & "') "
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
    Private Function FillDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentType As String, ByRef mAllData As Boolean) As Boolean

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
        Dim mRemarks As String
        Dim mDesg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = "DELETE FROM Temp_PrintDummyData WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()

        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            If mAllData = True Then GoTo NextRow1
            GridName.Col = ColPaymentType
            If UCase(GridName.Text) = UCase(mPaymentType) Then
NextRow1:
                GridName.Col = ColCard
                mEmpCode = MainClass.AllowSingleQuote(GridName.Text)

                mRowNum = IIf(mEmpCode = "", 10000 + RowNum, RowNum)

                GridName.Col = ColName
                mEmpName = MainClass.AllowSingleQuote(GridName.Text)

                GridName.Col = ColBankNo
                mBankAcct = MainClass.AllowSingleQuote(GridName.Text)

                GridName.Col = ColOT
                mOTHour = GridName.Text

                GridName.Col = ColRate
                mOTRate = GridName.Text

                GridName.Col = ColAmount
                mOTAmount = GridName.Text

                GridName.Col = ColESIC
                mESIC = GridName.Text

                GridName.Col = ColNetAmount
                mNetAmt = GridName.Text

                GridName.Col = ColBSalary
                mBasicSal = GridName.Text

                GridName.Col = ColAdvance
                mAdvance = GridName.Text

                GridName.Col = ColRemarks
                mRemarks = GridName.Text

                GridName.Col = ColDesg
                mDesg = GridName.Text

                SqlStr = " INSERT INTO Temp_PrintDummyData (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5, FIELD6, " & vbCrLf & " FIELD7, FIELD8, FIELD9, FIELD10, FIELD11, FIELD12) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & mRowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', " & vbCrLf & " '" & mOTHour & "','" & mNetAmt & "','" & mBankAcct & "'," & vbCrLf & " '" & mOTRate & "','" & mOTAmount & "', " & vbCrLf & " '" & mESIC & "', " & vbCrLf & " '" & mBasicSal & "','" & mAdvance & "','" & mRemarks & "','" & mDesg & "') "


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

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        FillHeading(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub


    Private Sub frmOTSalary_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen

        If lblBookType.Text = "N" Then
            Me.Text = "Over Time Register" & IIf(lblShowType.Text = "D", " (Checking Purpose)", "")
        Else
            Me.Text = "Arrear Over Time Register" & IIf(lblShowType.Text = "D", " (Checking Purpose)", "")
        End If

    End Sub

    Private Sub frmOTSalary_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)
        FillHeading(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        OptCC.Checked = True
        FillDeptCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkDiv.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    Private Sub sprdAttn_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles sprdAttn.DblClick
        Dim mDays As String
        Dim mCode As String

        If eventArgs.col < 4 Or eventArgs.col > sprdAttn.MaxCols - 3 Then Exit Sub

        sprdAttn.Row = eventArgs.row
        sprdAttn.Col = 1
        If Trim(sprdAttn.Text) = "" Then Exit Sub
        frmOverTimeHead.lblCode.Text = sprdAttn.Text
        mCode = sprdAttn.Text

        sprdAttn.Col = 3
        frmOverTimeHead.lblEmpName.Text = sprdAttn.Text

        sprdAttn.Row = 0
        sprdAttn.Col = eventArgs.col
        If Val(VB.Left(sprdAttn.Text, 2)) = 0 Then Exit Sub
        frmOverTimeHead.lblDate.Text = Mid(Trim(sprdAttn.Text), 1, 2) & " " & lblYear.Text
        mDays = Mid(LTrim(sprdAttn.Text), 1, 2) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))

        frmOverTimeHead.lblType.Text = CStr(1)
        frmOverTimeHead.ShowDialog()
        RefreshScreen()
    End Sub

    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        FillHeading(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        FillHeading(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen
        Dim mDeptCode As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim LastDayofMon As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mArrearStr As String
        Dim mDivisionCode As Double
        Dim mTable As String
        Dim mESIAmount As Double

        MainClass.ClearGrid(sprdAttn, -1)

        If lblShowType.Text = "D" Then
            mTable = "PAY_MONTHLY_DUMMY_OT_TRN"
        Else
            mTable = "PAY_MONTHLY_OT_TRN"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mMonth = CShort(VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mYear = Year(CDate(lblRunDate.Text))

        LastDayofMon = MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))

        If CDate(lblRunDate.Text) < CDate("01/01/2014") Then
            SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
                & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE, EMP.EMP_DEPT_CODE, EMP.EMP_CATG, " & vbCrLf & " GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",EMP.EMP_CODE,OT.OT_DATE) AS DESG_DESC, " & vbCrLf & " OT.BASICSALARY , OT.RATE * 2 AS RATE, OT.OT_HOUR/2 AS OT_HOUR, OT_AMOUNT,ESIC_AMOUNT, ADV_AMOUNT, OT.NET_AMOUNT "
        Else
            SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
                & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE, EMP.EMP_DEPT_CODE, EMP.EMP_CATG, " & vbCrLf _
                & " GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",EMP.EMP_CODE,OT.OT_DATE) AS DESG_DESC, " & vbCrLf _
                & " OT.BASICSALARY , OT.RATE, OT.OT_HOUR, OT_AMOUNT,ESIC_AMOUNT, ADV_AMOUNT, OT.NET_AMOUNT "
        End If


        SqlStr = SqlStr & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, " & mTable & " OT " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf & " AND EMP.COMPANY_CODE=OT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CODE=OT.EMP_CODE" & vbCrLf & " AND TO_CHAR(OT.OT_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & "AND IS_ARREAR='" & lblBookType.Text & "'"
        SqlStr = SqlStr & vbCrLf & "AND OT_HOUR<>0"



        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            '25-01-2012
            '        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & vb.Left(cboCategory, 1) & "' "  ''If Category Change....

            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE IN ( " & vbCrLf & " SELECT EMP_CODE FROM PAY_SAL_TRN" & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'" & vbCrLf & " AND CATEGORY='" & VB.Left(cboCategory.Text, 1) & "')"

        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf OptCN.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CATG,EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CATG,EMP.EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow
                    .Row = cntRow

                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    .Col = ColBankNo
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NO").Value), "", RsAttn.Fields("EMP_BANK_NO").Value)

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENTMODE").Value = "1", "Cash", "Cheque")

                    .Col = ColCatgeory
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_CATG").Value), "", RsAttn.Fields("EMP_CATG").Value)

                    .Col = ColBSalary
                    .Text = VB6.Format(RsAttn.Fields("BASICSALARY").Value, "0.00")

                    .Col = ColOT
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("OT_HOUR").Value), 0, RsAttn.Fields("OT_HOUR").Value), "0.00")

                    .Col = ColRate
                    .Text = VB6.Format(RsAttn.Fields("Rate").Value, "0.00")

                    .Col = ColAmount
                    .Text = VB6.Format(RsAttn.Fields("OT_AMOUNT").Value, "0.00")

                    .Col = ColESIC
                    mESIAmount = CDbl(VB6.Format(RsAttn.Fields("ESIC_AMOUNT").Value, "0.00"))
                    If mESIAmount > Int(mESIAmount) Then
                        mESIAmount = Int(mESIAmount) + 1
                    Else
                        mESIAmount = System.Math.Round(mESIAmount, 0)
                    End If
                    .Text = VB6.Format(mESIAmount, "0.00")

                    .Col = ColAdvance
                    .Text = VB6.Format(RsAttn.Fields("ADV_AMOUNT").Value, "0.00")

                    .Col = ColNetAmount
                    .Text = VB6.Format(RsAttn.Fields("NET_AMOUNT").Value, "0.00")

                    .Col = ColRemarks
                    If lblBookType.Text = "Y" Then
                        mArrearStr = GetEMPWEFDate(mCode, (lblRunDate.Text))
                    Else
                        mArrearStr = ""
                    End If
                    .Text = mArrearStr

                    RsAttn.MoveNext()
                    cntRow = cntRow + 1
                Loop

                .MaxRows = .MaxRows + 1
                .Row = .MaxRows
                For cntCol = ColName To sprdAttn.MaxCols
                    .Col = cntCol
                    .Text = New String("-", 100)
                Next

                ColTotal(sprdAttn, .MaxCols - 6, .MaxCols)

                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)

                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
        End If
        MainClass.SetFocusToCell(sprdAttn, 1, 1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrRefreshScreen:
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

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
End Class
