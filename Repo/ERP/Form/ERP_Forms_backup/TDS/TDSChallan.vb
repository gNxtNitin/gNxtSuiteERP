Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTDSChallan
    Inherits System.Windows.Forms.Form
    Dim RSTDSChallan As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection				
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xRefNo As Integer
    Dim SqlStr As String
    Private Const ColLocked As Short = 1
    Private Const ColCompanyCode As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColSection As Short = 5
    Private Const ColTDSPer As Short = 6
    Private Const ColDeductAmt As Short = 7
    Private Const ColCessAmt As Short = 8
    Private Const ColSurcharge As Short = 9
    Private Const ColTDSAmount As Short = 10
    Private Const ColMKEY As Short = 11
    Private Const ColChallanMkey As Short = 12
    Private Const ColPartyPAN As Short = 13


    Private Const RowHeight As Short = 12
    Private Sub SetTextLength()
        On Error GoTo ERR1
        TxtAccount.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtDateFrom.MaxLength = 10
        txtDateTo.MaxLength = 10
        txtBankName.MaxLength = RSTDSChallan.Fields("BANKNAME").DefinedSize
        txtBankCode.MaxLength = RSTDSChallan.Fields("BANKCODE").DefinedSize
        txtChallanDate.MaxLength = 10
        txtChallanNo.MaxLength = RSTDSChallan.Fields("CHALLANNO").DefinedSize
        txtAmountPaid.MaxLength = RSTDSChallan.Fields("AMOUNT").Precision

        txtSectionName.MaxLength = MainClass.SetMaxLength("NAME", "TDS_SECTION_MST", PubDBCn)
        txtChqNo.MaxLength = RSTDSChallan.Fields("CHQ_NO").DefinedSize
        txtChqDate.MaxLength = 10
        txtTDSAmount.MaxLength = RSTDSChallan.Fields("TDS_AMOUNT").Precision
        txtSurcharge.MaxLength = RSTDSChallan.Fields("SURCHARGE").Precision
        txtCess.MaxLength = RSTDSChallan.Fields("EDU_CESS").Precision
        txtInterest.MaxLength = RSTDSChallan.Fields("INTEREST_AMOUNT").Precision
        txtOthers.MaxLength = RSTDSChallan.Fields("OTHER_AMOUNT").Precision


        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()
        txtRefNo.Text = ""
        TxtAccount.Text = ""
        txtAmountPaid.Text = "0.00"

        txtChallanNo.Text = ""
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtChallanDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblMKey.Text = ""

        txtSectionName.Text = ""
        txtChqNo.Text = ""
        txtChqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtTDSAmount.Text = "0.00"
        txtSurcharge.Text = "0.00"
        txtCess.Text = "0.00"
        txtInterest.Text = "0.00"
        txtOthers.Text = "0.00"

        txtRefNo.Enabled = True

        txtSectionName.Enabled = True
        cmdSection.Enabled = True
        TxtAccount.Enabled = True
        CmdSearch.Enabled = True

        MainClass.ClearGrid(SprdMain)
        MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtRefNo.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            If RSTDSChallan.EOF = False Then RSTDSChallan.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume				
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If TxtAccount.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RSTDSChallan.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
                If Delete1() = False Then GoTo DelErrPart
                If RSTDSChallan.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "TDS_Challan", (lblMKey.Text), RSTDSChallan, "", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "TDS_Challan", "MKEY", (lblMKey.Text)) = False Then GoTo DeleteErr

        If DeleteFromTDSTRN((lblMKey.Text)) = False Then GoTo DeleteErr

        SqlStr = "Delete from TDS_Challan where MKey='" & lblMKey.Text & "' "
        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()
        RSTDSChallan.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RSTDSChallan.Requery()
        MsgBox(Err.Description)
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForChallan(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForChallan(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForChallan(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        PrintStatus = True

        frmPrintPO.OptPrint(0).Text = "Form No 181"
        frmPrintPO.OptPrint(1).Text = "Form No 17"

        frmPrintPO.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        '''''Select Record for print...				

        SqlStr = ""

        If frmPrintPO.OptPrint(0).Checked = True Then
            mReportFileName = "TDSChallan.Rpt"
        Else
            If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1
            mReportFileName = "TDSChallan17.Rpt"
        End If

        SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD3")

        mTitle = "T.D.S. / T.C.S. Challan"
        mSubTitle = ""



        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
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

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mAYEAR As String
        Dim mTaxType As String
        Dim mCompanyTan As String
        Dim mCompanyPhone As String
        Dim mCompanyPin As String
        Dim mPaymentCode As String
        Dim mTotalInWords As String
        Dim mAmountStr As String
        Dim CompanyAdd As String

        Dim mAmount As String
        Dim mCroreStr As String
        Dim mLacsStr As String
        Dim mThousandStr As String
        Dim mHundredStr As String
        Dim mTenStr As String
        Dim mUnitStr As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        CompanyAdd = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        CompanyAdd = CompanyAdd & " " & IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        CompanyAdd = CompanyAdd & " " & IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        '    CompanyAdd = CompanyAdd & " " & IIf(IsNull(RsCompany!REGD_STATE), "", RsCompany!REGD_STATE)				
        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & CompanyAdd & """")

        mAYEAR = Year(RsCompany.Fields("END_DATE").Value) & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")) + 1, "00")

        MainClass.AssignCRptFormulas(Report1, "AYear=""" & mAYEAR & """")

        mTaxType = "0020"
        MainClass.AssignCRptFormulas(Report1, "TaxType=""" & mTaxType & """")

        mCompanyTan = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyTan=""" & mCompanyTan & """")

        mCompanyPhone = "" ''IIf(IsNull(RsCompany!REGD_PHONE), "", RsCompany!REGD_PHONE)				
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")

        mCompanyPin = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyPin=""" & mCompanyPin & """")
        MainClass.AssignCRptFormulas(Report1, "PaymentCode=""" & mPaymentCode & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtTDSAmount.Text, "0"))) & VB6.Format(txtTDSAmount.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "IncomeTax=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtSurcharge.Text, "0"))) & VB6.Format(txtSurcharge.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Surcharge=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtCess.Text, "0"))) & VB6.Format(txtCess.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "EduCess=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtInterest.Text, "0"))) & VB6.Format(txtInterest.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Interest=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtOthers.Text, "0"))) & VB6.Format(txtOthers.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Penalty=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Total=""" & mAmountStr & """")

        mTotalInWords = MainClass.RupeesConversion(txtNetAmount.Text)

        MainClass.AssignCRptFormulas(Report1, "TotalInWords=""" & mTotalInWords & """")
        MainClass.AssignCRptFormulas(Report1, "ChequeNo=""" & Trim(txtChqNo.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "ChequeDate=""" & Trim(txtChqDate.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "BankName=""" & Trim(txtBankName.Text) & """")

        mAmount = New String("0", 9 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
        mAmountStr = VB.Left(mAmount, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mCroreStr = mTotalInWords

        mAmountStr = Mid(mAmount, 3, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mLacsStr = mTotalInWords


        mAmountStr = Mid(mAmount, 5, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mThousandStr = mTotalInWords

        mAmountStr = Mid(mAmount, 7, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mHundredStr = mTotalInWords

        mAmountStr = Mid(mAmount, 8, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mTenStr = mTotalInWords

        mAmountStr = VB.Right(mAmount, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mUnitStr = mTotalInWords

        MainClass.AssignCRptFormulas(Report1, "CroreStr=""" & mCroreStr & """")
        MainClass.AssignCRptFormulas(Report1, "LacsStr=""" & mLacsStr & """")
        MainClass.AssignCRptFormulas(Report1, "ThousandStr=""" & mThousandStr & """")
        MainClass.AssignCRptFormulas(Report1, "HundredStr=""" & mHundredStr & """")
        MainClass.AssignCRptFormulas(Report1, "TenStr=""" & mTenStr & """")
        MainClass.AssignCRptFormulas(Report1, "UnitStr=""" & mUnitStr & """")

        ' Report1.CopiesToPrinter = PrintCopies				
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearch.Click
        Dim mFieldName As String
        If MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            TxtAccount.Text = AcName
            TxtAccount.Focus()
        End If
    End Sub

    Private Sub cmdSection_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSection.Click
        Dim mFieldName As String
        If MainClass.SearchMaster(txtSectionName.Text, "TDS_Section_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSectionName.Text = AcName
            txtSectionName.Focus()
        End If
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        'If FieldsVarification = False Then Exit Sub				
        If Trim(TxtAccount.Text) = "" Then
            MsgInformation("TDS Account Name is empty. Cannot Save")
            TxtAccount.Focus()
            Exit Sub
        End If


        If Trim(txtSectionName.Text) = "" Then
            If MsgQuestion("TDS Section is blank, want to continue ..") = vbYes Then
            Else
                'If Trim(txtSectionName.Text) = "" Then
                '    MsgInformation("Section Cann't be Blank")
                '    txtSectionName.Focus()
                '    Exit Sub
                'End If
                txtSectionName.Focus()
                Exit Sub
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Section.")
            txtSectionName.Focus()
            Exit Sub
        End If

        MainClass.ClearGrid(SprdMain, RowHeight)
        LedgInfo()
        FormatSprdMain()
        Call ReFormatSprdMain()
        SprdMain.Focus()
        MainClass.SetFocusToCell(SprdMain, 1, 4)
    End Sub
    Private Sub FormatSprdMain()
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColPartyPAN
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(ColLocked, 5.5)

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColCompanyCode, 5)
            .ColHidden = True

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVDate, 8)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 22)

            .Col = ColPartyPAN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyPAN, 22)
            .ColHidden = True

            .Col = ColSection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSection, 5)


            For cntCol = ColTDSPer To ColTDSAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, IIf(.Col = ColTDSPer, 4, IIf(.Col = ColTDSAmount, 8, 7)))
            Next


            .ColsFrozen = ColDeductAmt

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColChallanMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColHidden = True

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 2, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh				
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraView.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSChallan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            Dim mTDSPer As Double

            For cntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = cntRow
                SprdMain.Col = ColTDSPer
                mTDSPer = Val(SprdMain.Text)

                SprdMain.Col = ColLocked
                If Index = 2 Then
                    If Val(txtTdsPer.Text) = mTDSPer Then
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                    Else
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If
                Else
                    SprdMain.Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                End If
            Next
            CalcChallanAmount()
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        If eventArgs.row = 0 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColLocked
        SprdMain.Value = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
        CalcChallanAmount()
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtRefNo.Text = Trim(SprdView.Text)

        txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmTDSChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        MainClass.UOpenRecordSet("Select * From TDS_CHALLAN Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SetTextLength()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call AssignGrid(False)
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(9945)
        FormatSprdMain()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSChallan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        RSTDSChallan = Nothing
        Me.Dispose()
        Me.Close()

        '    PubDBCn.Cancel				
        '    PvtDBCn.Close				
        '    Set PvtDBCn = Nothing				
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mSection As String

        Shw = True
        If Not RSTDSChallan.EOF Then
            txtRefNo.Enabled = True
            With RSTDSChallan
                If MainClass.ValidateWithMasterTable(.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtAccount.Text = MasterNo
                End If
                txtRefNo.Text = IIf(IsDBNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value)
                txtDateFrom.Text = VB6.Format(IIf(IsDBNull(.Fields("FROMDATE").Value), "", .Fields("FROMDATE").Value), "DD/MM/YYYY")
                txtDateTo.Text = VB6.Format(IIf(IsDBNull(.Fields("TODATE").Value), "", .Fields("TODATE").Value), "DD/MM/YYYY")
                txtBankName.Text = IIf(IsDBNull(.Fields("BANKNAME").Value), "", .Fields("BANKNAME").Value)
                txtBankCode.Text = IIf(IsDBNull(.Fields("BANKCODE").Value), "", .Fields("BANKCODE").Value)
                txtChallanDate.Text = VB6.Format(IIf(IsDBNull(.Fields("CHALLANDATE").Value), "", .Fields("CHALLANDATE").Value), "DD/MM/YYYY")
                txtChallanNo.Text = IIf(IsDBNull(.Fields("CHALLANNO").Value), "", .Fields("CHALLANNO").Value)
                txtAmountPaid.Text = VB6.Format(IIf(IsDBNull(.Fields("Amount").Value), 0, .Fields("Amount").Value), "0.00")
                lblMKey.Text = RSTDSChallan.Fields("mKey").Value

                mSection = IIf(IsDBNull(.Fields("SECTIONCODE").Value), "", .Fields("SECTIONCODE").Value)
                If MainClass.ValidateWithMasterTable(mSection, "CODE", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSectionName.Text = MasterNo
                End If

                txtChqNo.Text = IIf(IsDBNull(.Fields("CHQ_NO").Value), "", .Fields("CHQ_NO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDBNull(.Fields("CHQ_DATE").Value), "", .Fields("CHQ_DATE").Value), "DD/MM/YYYY")
                txtTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TDS_AMOUNT").Value), 0, .Fields("TDS_AMOUNT").Value), "0.00")
                txtSurcharge.Text = VB6.Format(IIf(IsDBNull(.Fields("SURCHARGE").Value), 0, .Fields("SURCHARGE").Value), "0.00")
                txtCess.Text = VB6.Format(IIf(IsDBNull(.Fields("EDU_CESS").Value), 0, .Fields("EDU_CESS").Value), "0.00")
                txtInterest.Text = VB6.Format(IIf(IsDBNull(.Fields("INTEREST_AMOUNT").Value), 0, .Fields("INTEREST_AMOUNT").Value), "0.00")
                txtOthers.Text = VB6.Format(IIf(IsDBNull(.Fields("OTHER_AMOUNT").Value), 0, .Fields("OTHER_AMOUNT").Value), "0.00")

                xRefNo = RSTDSChallan.Fields("REFNO").Value
                txtSectionName.Enabled = False
                cmdSection.Enabled = False

                TxtAccount.Enabled = False
                CmdSearch.Enabled = False

            End With
            Call cmdShow_Click(cmdShow, New System.EventArgs())
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
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
        Dim mTDSCode As String
        Dim mRefNo As Integer
        Dim pMkey As String
        Dim mSectionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTDSCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = -1
        End If



        If ADDMode = True Then
            mRefNo = MaxRefNo()
            pMkey = (RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mRefNo

            txtRefNo.Text = CStr(mRefNo)

            SqlStr = "INSERT INTO TDS_CHALLAN (MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " REFNO, ACCOUNTCODE, FROMDATE, TODATE, " & vbCrLf _
                & " BANKNAME, BANKCODE, CHALLANDATE, CHALLANNO, AMOUNT, " & vbCrLf _
                & " SECTIONCODE, CHQ_NO, CHQ_DATE," & vbCrLf _
                & " TDS_AMOUNT, SURCHARGE, EDU_CESS, " & vbCrLf _
                & " INTEREST_AMOUNT, OTHER_AMOUNT, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( " & vbCrLf _
                & " '" & pMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & ", " & mRefNo & ",'" & mTDSCode & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBankName.Text) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBankCode.Text) & "',  " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "', " & Val(txtNetAmount.Text) & ", " & vbCrLf _
                & " " & mSectionCode & ", '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtTDSAmount.Text) & "," & vbCrLf _
                & " " & Val(txtSurcharge.Text) & ", " & Val(txtCess.Text) & "," & vbCrLf _
                & " " & Val(txtInterest.Text) & ", " & Val(txtOthers.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        Else
            SqlStr = "UPDATE TDS_CHALLAN SET " & vbCrLf _
                & " ACCOUNTCODE='" & mTDSCode & "', " & vbCrLf _
                & " FROMDATE=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TODATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " BANKNAME='" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf _
                & " BANKCODE='" & MainClass.AllowSingleQuote(txtBankCode.Text) & "', " & vbCrLf _
                & " CHALLANDATE=TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " CHALLANNO='" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "', " & vbCrLf _
                & " SECTIONCODE=" & mSectionCode & "," & vbCrLf _
                & " CHQ_NO='" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', " & vbCrLf _
                & " CHQ_DATE=TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TDS_AMOUNT=" & Val(txtTDSAmount.Text) & ", " & vbCrLf _
                & " SURCHARGE=" & Val(txtSurcharge.Text) & ", " & vbCrLf _
                & " EDU_CESS=" & Val(txtCess.Text) & ", " & vbCrLf _
                & " INTEREST_AMOUNT=" & Val(txtInterest.Text) & ", " & vbCrLf _
                & " OTHER_AMOUNT=" & Val(txtOthers.Text) & ", " & vbCrLf _
                & " AMOUNT=" & Val(txtNetAmount.Text) & ", " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE REFNO=" & txtRefNo.Text & " AND MKEY=" & lblMKey.Text & ""


            pMkey = lblMKey.Text
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)

        If UpdateTDSTRN(pMkey) = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        RSTDSChallan.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RSTDSChallan.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        ''Resume				
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        cmdsearch_Click(CmdSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(CmdSearch, New System.EventArgs())
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True
        If Trim(TxtAccount.Text) = "" Then
            MsgInformation("TDS Account Name is empty. Cannot Save")
            TxtAccount.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBankName.Text) = "" Then
            MsgInformation("Bank Name is empty. Cannot Save")
            txtBankName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBankCode.Text) = "" Then
            MsgInformation("Bank Code is empty. Cannot Save")
            txtBankCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Len(txtBankCode.Text) <> 7 Then
            MsgInformation("Invalid Bank Code. Cannot Save")
            txtBankCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtChallanDate.Text) = "" Then
            MsgInformation("Challan Date is empty. Cannot Save")
            txtChallanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtChallanDate.Text) Then
            MsgInformation("Invalid Challan Date. Cannot Save")
            txtChallanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtChallanNo.Text) = "" Then
            MsgInformation("Challan No is empty. Cannot Save")
            txtChallanNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtNetAmount.Text) = 0 Then
            MsgInformation("Net Amount is zero. Cannot Save")
            SprdMain.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSectionName.Text) = "" Then
            MsgInformation("Section Cann't be Blank")
            txtSectionName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Section.")
            txtSectionName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        ''If MODIFYMode = True And (RSTDSChallan.EOF=true Or RSTDSChallan.EOF = True) Then Exit Function				
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(TxtAccount.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = False Then
            MsgBox("Invalid TDS Account Name.", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAmountPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmountPaid.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmountPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmountPaid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBankCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCess_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCess.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCess.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCess_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCess.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChallanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChallanDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtChallanDate.Text) Then
            MsgBox("Invalid Challan Date", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtChallanNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtChqDate.Text) Then
            MsgBox("Invalid Cheque / DD Date", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateFrom.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDateFrom.Text) Then
            MsgBox("Invalid Date", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateTo.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDateTo.Text) Then
            MsgBox("Invalid Date", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInterest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInterest.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInterest_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInterest.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInterest_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInterest.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNetAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNetAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthers.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOthers_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthers.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RSTDSChallan.EOF = False Then xRefNo = RSTDSChallan.Fields("REFNO").Value

        SqlStr = ""
        SqlStr = "Select * from  TDS_Challan Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ANd RefNo=" & txtRefNo.Text & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
        If RSTDSChallan.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                MainClass.UOpenRecordSet("Select * From TDS_Challan Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RefNo=" & xRefNo & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub LedgInfo()
        On Error GoTo LedgError
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        CalcChallanAmount()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mAccountCode As String
        Dim mChallanNo As String
        Dim mSectionCode As Double

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            mAccountCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        End If

        mChallanNo = lblMKey.Text

        SqlStr = " Select DECODE(CHALLANMKEY,NULL,'0','1') AS LOCKED ,TDSTRN.COMPANY_CODE, TO_CHAR(Vdate,'DD/MM/YYYY') AS VDate, " & vbCrLf _
            & " DECODE(ACM1.SUPP_CUST_NAME,'-1','',ACM1.SUPP_CUST_NAME) AS PartyName, " & vbCrLf _
            & " TDSSection.Name As SectionName,  TO_CHAR(TDSRATE) AS TDSRATE, " & vbCrLf _
            & " TO_CHAR(TDSAMOUNT) As Amount, "



        SqlStr = SqlStr & vbCrLf & " '0.00' As CessAmount, " & vbCrLf _
            & " '0.00' As SurAmount, " & vbCrLf _
            & " TO_CHAR(TDSAMOUNT) As TDSAmount,"

        ''            & " --AND TDSTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _

        SqlStr = SqlStr & vbCrLf _
            & " TDSTRN.Mkey,TDSTRN.ChallanMKey,ACM1.PAN_NO " & vbCrLf _
            & " FROM TDS_TRN TDSTRN, TDS_Section_MST TDSSection, FIN_SUPP_CUST_MST ACM,FIN_SUPP_CUST_MST ACM1, GEN_COMPANY_MST GMST " & vbCrLf _
            & " WHERE "

        SqlStr = SqlStr & vbCrLf _
            & " GMST.COMPANY_CODE = TDSTRN.COMPANY_CODE " & vbCrLf _
            & " AND GMST.PAN_NO='" & RsCompany.Fields("PAN_NO").Value & "'"  ''

        SqlStr = SqlStr & vbCrLf _
            & " AND TDSTRN.COMPANY_CODE = ACM.COMPANY_CODE " & vbCrLf _
            & " AND TDSTRN.COMPANY_CODE = TDSSection.COMPANY_CODE(+) " & vbCrLf _
            & " AND TDSTRN.AccountCode = ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND TDSTRN.SectionCode = TDSSection.Code(+) " & vbCrLf _
            & " AND TDSTRN.COMPANY_CODE = ACM1.COMPANY_CODE " & vbCrLf _
            & " AND TDSTRN.PARTYCODE = ACM1.SUPP_CUST_CODE " & vbCrLf _
            & " AND TDSTRN.Vdate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TDSTRN.Vdate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TDSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND TDSTRN.AccountCode = '" & mAccountCode & "'"

        If mChallanNo = "" Then
            SqlStr = SqlStr & vbCrLf & " AND (TDSTRN.CHALLANMKEY='' OR TDSTRN.CHALLANMKEY IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND (TDSTRN.CHALLANMKEY='" & MainClass.AllowSingleQuote(mChallanNo) & "' OR TDSTRN.CHALLANMKEY='' OR TDSTRN.CHALLANMKEY IS NULL)"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TDSTRN.CANCELLED='N'"

        If Trim(txtSectionName.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TDSTRN.SECTIONCODE=" & mSectionCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TDSTRN.Vdate,TDSTRN.Vno,TDSTRN.BOOKTYPE,TDSTRN.BOOKSUBTYPE,TDSTRN.SUBROWNO "

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Sub FillHeading()


        With SprdMain
            .Row = 0
            .Col = ColLocked
            .Text = "Update"

            .Col = ColCompanyCode
            .Text = "Company Code"

            .Col = ColVDate
            .Text = "Date"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColSection
            .Text = "Section Name"

            .Col = ColTDSPer
            .Text = "TDS %"

            .Col = ColDeductAmt
            .Text = "Amount"

            .Col = ColCessAmt
            .Text = "Cess"

            .Col = ColSurcharge
            .Text = "Surcharge"

            .Col = ColTDSAmount
            .Text = "TDS Amount"

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColChallanMkey
            .Text = "ChallanMkey"

            .Col = ColPartyPAN
            .Text = "PAN No"

        End With

    End Sub

    Private Sub CalcChallanAmount()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mNetAmount As Double
        Dim mCESSAmount As Double
        Dim mSURAmount As Double
        Dim mTDSAmount As Double

        mNetAmount = 0
        mCESSAmount = 0
        mSURAmount = 0
        mTDSAmount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColLocked

                If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then GoTo NextRow

                .Col = ColDeductAmt
                mNetAmount = mNetAmount + Val(.Text)

                .Col = ColCessAmt
                mCESSAmount = mCESSAmount + Val(.Text)

                .Col = ColSurcharge
                mSURAmount = mSURAmount + Val(.Text)

                .Col = ColTDSAmount
                mTDSAmount = mTDSAmount + Val(.Text)


NextRow:
            Next
        End With
        mCESSAmount = System.Math.Round(mCESSAmount, 0)
        mSURAmount = System.Math.Round(mSURAmount, 0)

        '    mCESSAmount = mCESSAmount				
        '    mSURAmount = mSURAmount				
        mTDSAmount = mNetAmount - (mCESSAmount + mSURAmount)

        txtAmountPaid.Text = VB6.Format(mNetAmount, "0.00")
        txtCess.Text = VB6.Format(mCESSAmount, "0.00")
        txtSurcharge.Text = VB6.Format(mSURAmount, "0.00")
        txtTDSAmount.Text = VB6.Format(mTDSAmount, "0.00")

        Call CalcTDSAmount()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub CalcTDSAmount()
        On Error GoTo ErrPart

        txtTDSAmount.Text = CStr(Val(txtAmountPaid.Text) - (System.Math.Round(Val(txtSurcharge.Text), 0) + System.Math.Round(Val(txtCess.Text), 0)))
        txtTDSAmount.Text = VB6.Format(txtTDSAmount.Text, "0.00")

        txtNetAmount.Text = CStr(Val(txtAmountPaid.Text) + Val(txtInterest.Text) + Val(txtOthers.Text))
        txtNetAmount.Text = VB6.Format(txtNetAmount.Text, "0.00")


        '    txtTDSAmount.Text = Val(txtAmountPaid.Text) - (Val(txtSurcharge.Text) + Val(txtCess.Text))				
        '    txtTDSAmount.Text = Format(txtTDSAmount.Text, "0.00")				
        '				
        '    txtNetAmount.Text = Val(txtAmountPaid.Text) + Val(txtInterest.Text) + Val(txtOthers.Text)				
        '    txtNetAmount.Text = Format(txtNetAmount.Text, "0.00")				

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function MaxRefNo() As Integer
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mMaxNo As Double

        SqlStr = "SELECT MAX(REFNO) AS REFNO FROM TDS_CHALLAN " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MaxRefNo = 1
        Else
            mMaxNo = IIf(IsDBNull(RsTemp.Fields("REFNO").Value), 0, RsTemp.Fields("REFNO").Value)
            MaxRefNo = mMaxNo + 1
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function

    Private Function UpdateTDSTRN(ByRef pChallanMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mKey As String
        Dim mChallanDate As String
        Dim mChallanNo As String
        Dim mBankName As String
        Dim mBankCode As String
        Dim mChallanMkey As String
        Dim mChqNo As String
        Dim mChqDate As String
        Dim mSurcharge As Double
        Dim mCess As Double
        Dim mInterest As Double
        Dim mOtherCharges As Double
        Dim mNetAmount As Double

        If DeleteFromTDSTRN(pChallanMKey) = False Then GoTo ErrPart

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColLocked

                If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    mChallanDate = ""
                    mChallanNo = ""
                    mBankName = ""
                    mBankCode = ""
                    mChallanMkey = ""
                    mChqNo = ""
                    mChqDate = ""
                    mSurcharge = 0
                    mCess = 0
                    mInterest = 0
                    mOtherCharges = 0
                Else
                    mChallanDate = txtChallanDate.Text
                    mChallanNo = txtChallanNo.Text
                    mBankName = txtBankName.Text
                    mBankCode = txtBankCode.Text
                    mChallanMkey = pChallanMKey
                    mChqNo = txtChqNo.Text
                    mChqDate = txtChqDate.Text
                    mSurcharge = 0
                    mCess = 0
                    mInterest = 0
                    mOtherCharges = 0
                End If

                .Col = ColMKEY
                mKey = Trim(.Text)

                '                & " SURCHARGE=" & Val(mSurcharge) & ", " & vbCrLf _				
                ''                & " EDU_CESS=" & Val(mCESS) & ", " & vbCrLf _				
                ''                & " INTEREST_AMOUNT=" & Val(mInterest) & ", " & vbCrLf _				
                ''                & " OTHER_AMOUNT=" & Val(mOtherCharges) & ", " & vbCrLf _				
                ''				
                SqlStr = " UPDATE TDS_TRN SET " & vbCrLf _
                & " CHALLANDATE=TO_DATE('" & VB6.Format(mChallanDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), UPDATE_FROM='H'," & vbCrLf _
                & " CHALLANNO='" & MainClass.AllowSingleQuote(mChallanNo) & "', " & vbCrLf _
                & " BANKNAME='" & MainClass.AllowSingleQuote(mBankName) & "', " & vbCrLf _
                & " BANKCODE='" & MainClass.AllowSingleQuote(mBankCode) & "', " & vbCrLf _
                & " CHQ_NO='" & MainClass.AllowSingleQuote(mChqNo) & "', " & vbCrLf _
                & " CHQ_DATE=TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " CHALLANMKEY='" & MainClass.AllowSingleQuote(mChallanMkey) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE MKey= '" & mKey & "'"

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                    & " UPDATE_FROM='H'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE MKey= '" & mKey & "'" & vbCrLf _
                    & " AND BookType<>'D' " & vbCrLf _
                    & " AND BookSubType<>'D'"

                PubDBCn.Execute(SqlStr)
            Next

        End With

        UpdateTDSTRN = True

        Exit Function
ErrPart:
        UpdateTDSTRN = False
    End Function


    Private Function DeleteFromTDSTRN(ByRef pChallanMKey As String) As Boolean
        On Error GoTo ErrPart

        ''COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "

        SqlStr = " UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
            & " UPDATE_FROM='H'," & vbCrLf _
            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " WHERE MKey IN ( " & vbCrLf & " SELECT MKEY FROM TDS_TRN" & vbCrLf _
            & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND CHALLANMKEY='" & MainClass.AllowSingleQuote(pChallanMKey) & "')"

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE TDS_TRN SET " & vbCrLf _
            & " CHALLANDATE='', UPDATE_FROM='H'," & vbCrLf & " CHALLANNO='', " & vbCrLf _
            & " BANKNAME='', BANKCODE='', CHQ_NO='', CHQ_DATE=''," & vbCrLf _
            & " CHALLANMKEY=''," & vbCrLf _
            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND CHALLANMKEY='" & MainClass.AllowSingleQuote(pChallanMKey) & "'"

        PubDBCn.Execute(SqlStr)

        DeleteFromTDSTRN = True
        Exit Function
ErrPart:
        DeleteFromTDSTRN = False
    End Function
    Private Sub ReFormatSprdMain()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mChallanNo As String

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColChallanMkey
                mChallanNo = Trim(.Text)

                .Col = ColLocked
                If mChallanNo = "" Then
                    .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                Else
                    .Value = CStr(System.Windows.Forms.CheckState.Checked)
                End If
            Next
        End With

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo ERR1
        SqlStr = ""

        SqlStr = " Select TO_CHAR(REFNO,'00000') AS REFNO,TO_CHAR(FROMDATE,'DD/MM/YYYY') AS FROMDATE, " & vbCrLf _
            & " TO_CHAR(TODATE,'DD/MM/YYYY') AS TODATE," & vbCrLf _
            & " SMST.NAME AS SECTION,BANKNAME, CHALLANNO, " & vbCrLf _
            & " TO_CHAR(CHALLANDATE,'DD/MM/YYYY') AS ChallanDate, " & vbCrLf _
            & " TO_CHAR(AMOUNT) As Amount " & vbCrLf _
            & " FROM TDS_Challan TDSChallan, TDS_SECTION_MST SMST" & vbCrLf _
            & " WHERE TDSChallan.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND TDSChallan.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TDSChallan.Company_Code=SMST.Company_Code  " & vbCrLf _
            & " AND TDSChallan.SECTIONCODE=SMST.CODE " & vbCrLf _
            & " ORDER BY TDSChallan.REFNO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 2500)
            .set_ColWidth(6, 1500)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1000)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtSectionName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSectionName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionName.DoubleClick
        cmdSection_Click(cmdSection, New System.EventArgs())
    End Sub


    Private Sub txtSectionName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSectionName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSectionName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSectionName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSectionName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSection_Click(cmdSection, New System.EventArgs())
    End Sub

    Private Sub txtSectionName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSectionName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtSectionName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Section Name.", vbInformation)
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSurcharge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurcharge.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurcharge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurcharge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSurcharge_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSurcharge.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTdsPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTdsPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim cntRow As Integer
        Dim mTDSPer As Double

        If OptSelection(2).Checked = True Then
            For cntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = cntRow
                SprdMain.Col = ColTDSPer
                mTDSPer = Val(SprdMain.Text)

                SprdMain.Col = ColLocked
                If Val(txtTdsPer.Text) = mTDSPer Then
                    SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                Else
                    SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                End If
            Next
        End If
        CalcChallanAmount()
        eventArgs.Cancel = Cancel
    End Sub
End Class
