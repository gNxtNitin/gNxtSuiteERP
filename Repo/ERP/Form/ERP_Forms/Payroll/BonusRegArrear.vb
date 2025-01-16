Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBonusRegArrear
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
    Private Const ColBonusApr As Short = 6
    Private Const ColBonusMay As Short = 7
    Private Const ColBonusJun As Short = 8
    Private Const ColBonusJul As Short = 9
    Private Const ColBonusAug As Short = 10
    Private Const ColBonusSep As Short = 11
    Private Const ColBonusOct As Short = 12
    Private Const ColBonusNov As Short = 13
    Private Const ColBonusDec As Short = 14
    Private Const ColBonusJan As Short = 15
    Private Const ColBonusFeb As Short = 16
    Private Const ColBonusMar As Short = 17
    Private Const ColBonusArrear As Short = 18
    Private Const ColTotBasic As Short = 19
    Private Const ColTotalBonus As Short = 20
    Private Const ColDOL As Short = 21
    Private Const ColBankIFSC As Short = 22

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


            For cntCol = ColBonusApr To ColTotalBonus
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 9)
            Next

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

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColBankAcct
            .Text = "Bank Account"

            .Col = ColPaymentMode
            .Text = "Payment Mode"

            .Col = ColBonusApr
            .Text = "April"

            .Col = ColBonusMay
            .Text = "May"

            .Col = ColBonusJun
            .Text = "June"

            .Col = ColBonusJul
            .Text = "July"

            .Col = ColBonusAug
            .Text = "August"

            .Col = ColBonusSep
            .Text = "September"

            .Col = ColBonusOct
            .Text = "October"

            .Col = ColBonusNov
            .Text = "November"

            .Col = ColBonusDec
            .Text = "December"

            .Col = ColBonusJan
            .Text = "January"

            .Col = ColBonusFeb
            .Text = "February"

            .Col = ColBonusMar
            .Text = "March"

            .Col = ColBonusArrear
            .Text = "Arrear"

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

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub


    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
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

    Private Sub chkDiv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDiv.CheckStateChanged
        If chkDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
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
            mTitle = "Bonus Arrear Register" & IIf(chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboDivision.Text, "") & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")

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
            mTitle = "Bonus (Cash)" & IIf(chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboDivision.Text, "") & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
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
    Private Function GetBonusAmount(ByRef mCode As String, ByRef mSalDate As String) As Double

        On Error GoTo ErrPart1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSalDate As String

        xSalDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        xSalDate = VB6.Format(xSalDate, "DD/MM/YYYY")

        SqlStr = " SELECT SALARYDEF.PERCENTAGE, SALARYDEF.AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE=" & ConBonus & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND TYPE=" & ConBonus & "" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND TYPE=" & ConBonus & "" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBonusAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        Else
            GetBonusAmount = 0
        End If
        Exit Function

ErrPart1:
        GetBonusAmount = 0
    End Function


    Private Function GetBonusPer(ByRef mCode As String, ByRef mSalDate As String, ByRef mCheckFieldName As String, ByRef mAddDays As Double) As Double

        On Error GoTo ErrPart1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSalDate As String
        Dim mWef As String

        xSalDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        xSalDate = VB6.Format(xSalDate, "DD/MM/YYYY")
        mAddDays = 0

        SqlStr = " SELECT SALARYDEF.SALARY_EFF_DATE, SALARYDEF.PERCENTAGE, SALARYDEF.AMOUNT, SALARYDEF.ADDDAYS_IN " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE=" & ConBonus & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND " & mCheckFieldName & " <= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND TYPE=" & ConBonus & "" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND TYPE=" & ConBonus & "" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBonusPer = IIf(IsDbNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
            mWef = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SALARY_EFF_DATE").Value), "", RsTemp.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")
            If CDate(mWef) > CDate(xSalDate) Then
                mAddDays = IIf(IsDbNull(RsTemp.Fields("ADDDAYS_IN").Value), 0, RsTemp.Fields("ADDDAYS_IN").Value)
            End If
        Else
            GetBonusPer = 0
        End If
        Exit Function

ErrPart1:
        GetBonusPer = 0
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
                For cntCol = ColBonusApr To ColTotalBonus
                    .Col = cntCol
                    arrsal(cntCol) = arrsal(cntCol) + Val(sprdAddDeduct.Text)
                Next
            Next

            .Row = .MaxRows
            For cntCol = ColBonusApr To ColTotalBonus
                .Col = cntCol
                sprdAddDeduct.Text = CStr(arrsal(cntCol))
                .Font = VB6.FontChangeBold(.Font, True)
            Next


        End With

    End Sub
    Private Sub frmBonusRegArrear_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmBonusRegArrear_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkDiv.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub RefreshScreenOld()

        On Error GoTo refreshErrPart
        Dim mDeptCode As String
        Dim mBonusPer As Double
        Dim cntRow As Integer
        Dim mFromDate As String
        Dim mToDate As String
        Dim CntMonth As Integer
        Dim mEmpCode As String
        Dim mBonusAmount As Double
        Dim mWDays As Double

        MainClass.ClearGrid(sprdAddDeduct)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mBonusPer = Val(IIf(IsDbNull(RsCompany.Fields("BonusLimit").Value), 0, RsCompany.Fields("BonusLimit").Value))


        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, GETEMPDESG ('" & RsCompany.Fields("COMPANY_CODE").Value & "',EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS April," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS May, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS June, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS July, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS August, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS September, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS October, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS November, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS December, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS January, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR) ELSE 0 END)) AS February, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, TRN.ISARREAR)ELSE 0 END)) AS March, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN ISARREAR='Y' OR ISARREAR='O' THEN TRN.PAYABLESALARY + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, ISARREAR) + DECODE(GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE),NULL,0,GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE)) ELSE 0 END)) AS Arrear, " & vbCrLf & " TO_CHAR(SUM(TRN.PAYABLESALARY + DECODE(GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE),NULL,0,GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE)) + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, ISARREAR))) AS Basic, " & vbCrLf & " TO_CHAR(ROUND(SUM(TRN.PAYABLESALARY + DECODE(GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE),NULL,0,GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE)) + GETPayableBonusAmount (TRN.COMPANY_CODE, EMP.EMP_CODE,SAL_DATE, ISARREAR))* " & mBonusPer & " /100,0)) A,"

        Else

            SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, GETEMPDESG ('" & RsCompany.Fields("COMPANY_CODE").Value & "',EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS April," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS May, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS June, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS July, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS August, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS September, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS October, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS November, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS December, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS January, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS February, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' AND (ISARREAR='N' OR ISARREAR='V' OR ISARREAR='F') THEN TRN.PAYABLESALARY ELSE 0 END)) AS March, " & vbCrLf & " TO_CHAR(SUM((CASE WHEN ISARREAR='Y' OR ISARREAR='O' THEN TRN.PAYABLESALARY ELSE 0 END))) AS Arrear, " & vbCrLf & " TO_CHAR(SUM((TRN.PAYABLESALARY))) AS Basic, " & vbCrLf & " TO_CHAR('0.00') AS Bonus, "
        End If

        ''ROUND(SUM(TRN.PAYABLESALARY)* " & mBonusPer & " /100,0)

        ''+ CASE WHEN ISARREAR='N' THEN DECODE(GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE),NULL,0,GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE)) ELSE 0 END
        ''+ CASE WHEN ISARREAR='N' THEN DECODE(GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE),NULL,0,GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE)) ELSE 0 END
        ''+                             DECODE(GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE),NULL,0,GETFFARREAR(TRN.COMPANY_CODE, EMP.EMP_CODE, TRN.SAL_DATE)
        SqlStr = SqlStr & vbCrLf & " EMP.EMP_LEAVE_DATE "

        SqlStr = SqlStr & vbCrLf & " From PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST SALHEAD, PAY_SAL_TRN TRN " & vbCrLf & " WHERE  TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE " & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND SALHEAD.TYPE=" & ConPF & ""

        SqlStr = SqlStr & vbCrLf & " AND TRN.SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TRN.SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        '" & vbCrLf _
        '& " OR TRN.EMP_CODE IN ( SELECT EMP_CODE FROM PAY_SALARYDEF_MST " & vbCrLf _
        '& " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '& " AND SALARY_EFF_DATE>='" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "' " & vbCrLf _
        '& " AND SALARY_EFF_DATE<='" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "' " & vbCrLf _
        '& " AND ARREAR_DATE>'" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "' " & vbCrLf _
        '& " AND IS_ARREAR='Y'))"


        '    SqlStr = SqlStr & vbCrLf & " AND EMP.BONUS_PER >0 "

        If optExisting.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
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

        SqlStr = SqlStr & vbCrLf & " GROUP by EMP.EMP_CODE, EMP.EMP_NAME,EMP.EMP_BANK_NO, EMP.PAYMENTMODE,EMP.EMP_LEAVE_DATE"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAddDeduct, StrConn, "Y")

        With sprdAddDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mEmpCode = Trim(.Text)

                '            For CntMonth = Format(mFromDate, "YYYYMM") To Format(mToDate, "YYYYMM")
                '            mFromDate = Format(txtFrom.Text, "DD/MM/YYYY")
                '            mToDate = Format(txtTo.Text, "DD/MM/YYYY")
                '            mBonusAmount = 0

                '            Do While Format(mFromDate, "YYYYMM") <= Format(mToDate, "YYYYMM")
                '                mWDays = CalcAttn(mEmpCode, mEMPDOJ, mDOL, mFromDate, mLeaveWop)
                '                mBonusAmount = mBonusAmount + GetBonusAmount(mEmpCode, mFromDate)
                '                mFromDate = DateAdd("m", 1, mFromDate)
                '            Loop

                mFromDate = RsCompany.Fields("START_DATE").Value
                For CntMonth = ColBonusApr To ColBonusMar
                    mBonusAmount = GetBonusAmount(mEmpCode, mFromDate)
                    If mBonusAmount = 0 Then
                        .Col = CntMonth
                        .Text = "0.00"
                    End If
                    mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mFromDate)))
                Next
            Next
        End With
        Call PrintCommand(True)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim mDeptCode As String
        Dim mBonusPer As Double
        Dim RsTempReg As ADODB.Recordset
        Dim mEmpCode As String
        Dim mFromDate As String
        Dim I As Integer
        Dim mWDays As Double
        Dim mMonthWDays As Double
        Dim mEmpDOJ As String
        Dim mDOL As String
        Dim mBonusAmount As Double
        Dim mPayableBS As Double
        Dim mPayableArrearBS As Double
        Dim CntMonth As Integer
        Dim mTotPayableBS As Double
        Dim mTotBonusAmount As Double
        Dim mDivisionCode As Double
        Dim mBonusAppPer As Double
        Dim mAddDays As Double

        MainClass.ClearGrid(sprdAddDeduct)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mBonusPer = Val(IIf(IsDBNull(RsCompany.Fields("BonusLimit").Value), 0, RsCompany.Fields("BonusLimit").Value))

        SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, GETEMPDESG ('" & RsCompany.Fields("COMPANY_CODE").Value & "',EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, EMP.EMP_LEAVE_DATE,EMP_DOJ"


        SqlStr = SqlStr & vbCrLf & " From PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE  EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf
        If optExisting.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN ( " & vbCrLf & " SELECT DISTINCT EMP_CODE FROM PAY_SAL_TRN " & vbCrLf & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        End If
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "'"
            End If
        End If

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.IS_CORPORATE='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.IS_CORPORATE='Y'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='000888'"

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        '    SqlStr = SqlStr & vbCrLf & " GROUP by EMP.EMP_CODE, EMP.EMP_NAME,EMP.EMP_BANK_NO, EMP.PAYMENTMODE,EMP.EMP_LEAVE_DATE"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempReg, ADODB.LockTypeEnum.adLockReadOnly)

        I = 1

        With sprdAddDeduct

            If RsTempReg.EOF = False Then
                Do While RsTempReg.EOF = False
                    .Row = I
                    '                .Col = ColCode
                    '                .Text = IIf(IsNull(RsTempReg!EMP_CODE), "", RsTempReg!EMP_CODE)
                    mEmpCode = Trim(IIf(IsDbNull(RsTempReg.Fields("EMP_CODE").Value), "", RsTempReg.Fields("EMP_CODE").Value))

                    '                .Col = ColName
                    '                .Text = IIf(IsNull(RsTempReg!EMP_NAME), "", RsTempReg!EMP_NAME)
                    '
                    '                .Col = ColDesg
                    '                .Text = IIf(IsNull(RsTempReg!DESG_DESC), "", RsTempReg!DESG_DESC)
                    '
                    '                .Col = ColBankAcct
                    '                .Text = IIf(IsNull(RsTempReg!EMP_BANK_NO), "", RsTempReg!EMP_BANK_NO)
                    '
                    '                .Col = ColPaymentMode
                    '                .Text = IIf(IsNull(RsTempReg!PAYMENTMODE), "", RsTempReg!PAYMENTMODE)

                    '                .Col = ColDOL
                    '                .Text = IIf(IsNull(RsTempReg!EMP_LEAVE_DATE), "", RsTempReg!EMP_LEAVE_DATE)
                    mDOL = IIf(IsDbNull(RsTempReg.Fields("EMP_LEAVE_DATE").Value), "", RsTempReg.Fields("EMP_LEAVE_DATE").Value)
                    mEmpDOJ = IIf(IsDbNull(RsTempReg.Fields("EMP_DOJ").Value), "", RsTempReg.Fields("EMP_DOJ").Value)

                    mBonusAmount = 0
                    mTotPayableBS = 0
                    mTotBonusAmount = 0
                    mBonusAppPer = 0

                    ''--APR
                    mFromDate = RsCompany.Fields("START_DATE").Value

                    For CntMonth = ColBonusApr To ColBonusMar
                        mMonthWDays = CalcAttn(mEmpCode, mEmpDOJ, mDOL, mFromDate)
                        mPayableBS = GetPayableBasic(mEmpCode, mFromDate, "N") ''GetPayableBasic(mEmpCode, mFromDate, "V") +
                        '                    mPayableBS = mPayableBS + GetPayableBasic(mEmpCode, mFromDate, "F")

                        mPayableBS = mPayableBS * mMonthWDays / MainClass.LastDay(Month(CDate(mFromDate)), Year(CDate(mFromDate)))
                        mBonusAmount = 0
                        mBonusPer = 0

                        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                            If mEmpCode = "000029" Then
                                mBonusAmount = 0
                                GoTo NextRecd
                            End If
                            If (mEmpCode = "001252" Or mEmpCode = "001229") Then
                                If CDate(mFromDate) >= CDate("01/04/2011") And CDate(mFromDate) <= CDate("31/08/2011") Then
                                    mBonusAmount = mPayableBS * 20 / 100
                                Else
                                    mBonusAmount = 0
                                End If
                                GoTo NextRecd
                            End If
                            If (mEmpCode = "001257" Or mEmpCode = "001186" Or mEmpCode = "001167") Then
                                If CDate(mFromDate) >= CDate("01/10/2011") And CDate(mFromDate) <= CDate("31/01/2012") Then
                                    mBonusAmount = mPayableBS * 20 / 100
                                Else
                                    mBonusAmount = 0
                                End If
                                GoTo NextRecd
                            End If
                        End If

                        mBonusAppPer = GetBonusPer(mEmpCode, mFromDate, "SALARY_APP_DATE", mAddDays)
                        If mBonusAppPer = 0 Then
                            mAddDays = 0
                            mBonusPer = GetBonusPer(mEmpCode, mFromDate, "SALARY_EFF_DATE- ADDDAYS_IN", mAddDays)
                        Else
                            mBonusPer = 0
                        End If
                        If mAddDays = 0 Then
                            mBonusAmount = mPayableBS * mBonusPer / 100
                        Else
                            mPayableBS = mPayableBS * mAddDays / mMonthWDays
                            mBonusAmount = mPayableBS * mBonusPer / (100)
                        End If

NextRecd:
                        mTotBonusAmount = mTotBonusAmount + mBonusAmount
                        mTotPayableBS = mTotPayableBS + IIf(mBonusAmount = 0, 0, mPayableBS)

                        .Col = CntMonth
                        .Text = VB6.Format(IIf(mBonusAmount = 0, 0, mPayableBS), "0.00")
                        mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mFromDate)))
                    Next

                    If mTotBonusAmount > 0 Then

                        .Col = ColCode
                        .Text = IIf(IsDbNull(RsTempReg.Fields("EMP_CODE").Value), "", RsTempReg.Fields("EMP_CODE").Value)

                        .Col = ColName
                        .Text = IIf(IsDbNull(RsTempReg.Fields("EMP_NAME").Value), "", RsTempReg.Fields("EMP_NAME").Value)

                        .Col = ColDesg
                        .Text = IIf(IsDbNull(RsTempReg.Fields("DESG_DESC").Value), "", RsTempReg.Fields("DESG_DESC").Value)

                        .Col = ColBankAcct
                        .Text = IIf(IsDbNull(RsTempReg.Fields("EMP_BANK_NO").Value), "", RsTempReg.Fields("EMP_BANK_NO").Value)

                        .Col = ColPaymentMode
                        .Text = IIf(IsDbNull(RsTempReg.Fields("PAYMENTMODE").Value), "", RsTempReg.Fields("PAYMENTMODE").Value)

                        .Col = ColDOL
                        .Text = IIf(IsDbNull(RsTempReg.Fields("EMP_LEAVE_DATE").Value), "", RsTempReg.Fields("EMP_LEAVE_DATE").Value)

                        .Col = ColBonusArrear
                        .Text = VB6.Format(0, "0.00")

                        .Col = ColTotBasic
                        .Text = VB6.Format(mTotPayableBS, "0.00")

                        .Col = ColTotalBonus
                        .Text = VB6.Format(System.Math.Round(mTotBonusAmount, 0), "0.00")


                        I = I + 1
                        .MaxRows = I
                    End If
                    mBonusPer = 0
                    RsTempReg.MoveNext()
                Loop
            End If
        End With


        'Private Const ColBonusArrear = 18
        'Private Const ColTotBasic = 19
        'Private Const ColTotalBonus = 20
        'Private Const  = 21
        '

        '            For CntMonth = Format(mFromDate, "YYYYMM") To Format(mToDate, "YYYYMM")
        '            mFromDate = Format(txtFrom.Text, "DD/MM/YYYY")
        '            mToDate = Format(txtTo.Text, "DD/MM/YYYY")
        '            mBonusAmount = 0

        '            Do While Format(mFromDate, "YYYYMM") <= Format(mToDate, "YYYYMM")
        '                mWDays = CalcAttn(mEmpCode, mEMPDOJ, mDOL, mFromDate, mLeaveWop)
        '                mBonusAmount = mBonusAmount + GetBonusAmount(mEmpCode, mFromDate)
        '                mFromDate = DateAdd("m", 1, mFromDate)
        '            Loop

        '                    mFromDate = RsCompany!START_DATE
        '                    For CntMonth = ColBonusApr To ColBonusMar
        '                        mBonusAmount = GetBonusAmount(mEmpCode, mFromDate)
        '                        If mBonusAmount = 0 Then
        '                            .Col = CntMonth
        '                            .Text = "0.00"
        '                        End If
        '                        mFromDate = DateAdd("m", 1, mFromDate)
        '                    Next
        '                Next
        '            End With
        '            RsTempReg.MoveNext
        '        Loop
        '    End If

        Call PrintCommand(True)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub

    Private Function GetPayableBasic(ByRef pEmpCode As String, ByRef mSalDate As String, ByRef mIsArrear As String) As Double

        On Error GoTo refreshErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mAmount As Double

        GetPayableBasic = 0
        If mIsArrear = "V" Then
            SqlStr = " SELECT DISTINCT PAYABLESALARY,SAL_TYPE " & vbCrLf & " FROM PAY_SALVOUCHER_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(mSalDate, "YYYYMM") & "'" & vbCrLf & " AND SAL_TYPE IN ('S','B')"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    GetPayableBasic = GetPayableBasic + IIf(IsDbNull(RsTemp.Fields("PAYABLESALARY").Value), 0, RsTemp.Fields("PAYABLESALARY").Value)
                    '        mWDays = IIf(IsNull(RsTemp!WDAYS), 0, RsTemp!WDAYS)
                    RsTemp.MoveNext()
                Loop
            End If
        Else
            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                If mIsArrear = "N" Then
                    SqlStr = " SELECT DISTINCT PAYABLESALARY + GETPayableBonusAmount (COMPANY_CODE, EMP_CODE,SAL_DATE, ISARREAR) AS PAYABLESALARY"
                Else
                    SqlStr = " SELECT DISTINCT PAYABLESALARY "
                End If
            Else
                SqlStr = " SELECT DISTINCT PAYABLESALARY"
            End If

            SqlStr = SqlStr & vbCrLf & " FROM PAY_SAL_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(mSalDate, "YYYYMM") & "'"


            SqlStr = SqlStr & vbCrLf & " AND ISARREAR = '" & mIsArrear & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetPayableBasic = IIf(IsDbNull(RsTemp.Fields("PAYABLESALARY").Value), 0, RsTemp.Fields("PAYABLESALARY").Value)
                '        mWDays = IIf(IsNull(RsTemp!WDAYS), 0, RsTemp!WDAYS)
            End If
        End If



        Exit Function
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing

        cboDept.Items.Clear()

        SqlStr = "Select DEPT_DESC from PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC "
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

        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Only Plant")
        cboShow.Items.Add("Only Corporate")
        cboShow.SelectedIndex = 0
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

    Private Sub frmBonusRegArrear_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
