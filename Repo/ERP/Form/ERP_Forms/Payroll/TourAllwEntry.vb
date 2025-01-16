Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTourAllwEntry
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
    Private Const ColFName As Short = 3
    Private Const ColDesg As Short = 4
    Private Const ColBankAcct As Short = 5
    Private Const ColPaymentMode As Short = 6
    Private Const ColTourAllw As Short = 7
    Private Const ColBankIFSC As Short = 8

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAddDeduct
            .MaxCols = ColTourAllw
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight)

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
            .set_ColWidth(ColName, 25)
            .TypeMaxEditLen = 5000

            .ColsFrozen = ColName

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 25)
            .TypeMaxEditLen = 5000

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

            .Col = ColTourAllw
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColTourAllw, 12)

        End With

        Call FillHeading()
        sprdAddDeduct.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 1, ColDesg)
        MainClass.SetSpreadColor(sprdAddDeduct, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        With sprdAddDeduct
            .MaxCols = ColTourAllw
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColFName
            .Text = "Employees' Father Name "

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColBankAcct
            .Text = "Bank Account"

            .Col = ColPaymentMode
            .Text = "Payment Mode"

            .Col = ColTourAllw
            .Text = "Tour Allowance"

        End With
    End Sub
    Private Function GetAmount(ByRef mEmpCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetAmount = 0

        SqlStr = "SELECT SUM(AMOUNT) AS AMOUNT FROM PAY_TOUR_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00"))
        End If
        Exit Function
ErrPart:
        GetAmount = 0
    End Function

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
        Dim pNarr As String

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

            mSubTitle = "For Year : " & RsCompany.Fields("FYEAR").Value

        ElseIf frmPrintOTReg.optBank.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, "CHEQUE", False) = False Then GoTo ERR1

            If frmPrintOTReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintOTReg.txtBankName.Text
            End If
            If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 1, ColCode, ColName, 0, ColPaymentMode, ColTourAllw, ColBankAcct, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1



            mRptFileName = "BankSheet.Rpt"

            '        mBankName = InputBox("Please Enter Bank Name. :", "Bank Name")

            If mBankName = "" Then
                mTitle = "BANK ANNEXURES"
            Else
                mTitle = "BANK ANNEXURES OF " & mBankName
            End If

            mSubTitle = "Tour Allowance For the Year : " & RsCompany.Fields("FYEAR").Value

        ElseIf frmPrintOTReg.optCash.Checked = True Then
            '        If FillDataIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, "CASH", False) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAddDeduct, 1, sprdAddDeduct.MaxRows - 3, ColCode, ColName, 0, ColPaymentMode, ColTourAllw, ColBankAcct, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1

            mRptFileName = "SalCashSheet.Rpt"
            mTitle = "Bonus (Cash)"
        ElseIf frmPrintOTReg.optBankTxt.Checked = True Then
            '        If CreateTxtFileForBankOLD = False Then GoTo ERR1
            pNarr = "BY TA OF " & RsCompany.Fields("FYEAR").Value

            If CreateTxtFileForBank(sprdAddDeduct, ColCode, ColName, ColPaymentMode, ColBankAcct, ColTourAllw, mBankName, pNarr, sprdAddDeduct.MaxRows - 1) = False Then GoTo ERR1

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
                .Col = ColTourAllw
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

            GridName.Col = ColTourAllw
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

                GridName.Col = ColTourAllw
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
                        .Col = ColTourAllw
                        If Val(.Text) > 0 Then

                            .Col = ColBankAcct
                            Print(1, TAB(0), Trim(.Text))

                            .Col = ColName
                            mEmpName = VB.Left(Trim(.Text), 60)
                            Print(1, TAB(17), mEmpName)

                            .Col = ColTourAllw
                            mAmount = New String(" ", 18 - Len(Trim(.Text))) & Trim(.Text)
                            Print(1, TAB(76), mAmount)

                            Print(1, TAB(94), "BY TA OF " & RsCompany.Fields("FYEAR").Value)

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
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        Dim cntRow As Integer
        Dim mEmpCode As String

        MainClass.ClearGrid(sprdAddDeduct)
        RefreshScreen()
        FormatSprd(-1)
        With sprdAddDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mEmpCode = Trim(.Text)

                .Col = ColTourAllw
                .Text = CStr(GetAmount(mEmpCode))
            Next
        End With

    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Update1 = True Then
            cmdSave.Enabled = False
        Else
            cmdSave.Enabled = True
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim mAmount As Double

        SqlStr = ""
        PubDBCn.BeginTrans()

        For cntRow = 1 To sprdAddDeduct.MaxRows
            sprdAddDeduct.Col = ColCode
            sprdAddDeduct.Row = cntRow
            mCode = sprdAddDeduct.Text

            sprdAddDeduct.Col = ColTourAllw
            mAmount = Val(sprdAddDeduct.Text)

            SqlStr = " DELETE FROM PAY_TOUR_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

            PubDBCn.Execute(SqlStr)

            If mAmount > 0 Then
                SqlStr = " INSERT INTO PAY_TOUR_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, " & vbCrLf & " EMP_CODE, AMOUNT " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & mCode & "', " & mAmount & ") "

                PubDBCn.Execute(SqlStr)
            End If
        Next

        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.Errors.Clear()
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Sub frmTourAllwEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmTourAllwEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
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
        OptName.Checked = True
        FillHeading()
        FillDeptCombo()

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

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If


        SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, '' AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, " & vbCrLf & " '0.00' "

        SqlStr = SqlStr & vbCrLf & " From PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"


        '    SqlStr = SqlStr & vbCrLf & " AND EMP_STOP_SALARY='N' "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG IN ('R','S')"

        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE =2"

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
    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub
End Class
