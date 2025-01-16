Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTDSeReturn24QNew
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection						

    Private Const RowHeight As Short = 15

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Const mPageWidth As Short = 135
    Private Const mDelimited As String = "^"
    Private Sub PrintStatus(ByRef pPrintEnable As Boolean)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkRefilling_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRefilling.CheckStateChanged
        cboCorrectionType.Enabled = IIf(chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
    End Sub

    Private Sub cmdCD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCD.Click
        Dim mQTR As String

        If Month(CDate(txtDateTo.Text)) = 4 Or Month(CDate(txtDateTo.Text)) = 5 Or Month(CDate(txtDateTo.Text)) = 6 Then
            mQTR = "Q1"
        ElseIf Month(CDate(txtDateTo.Text)) = 7 Or Month(CDate(txtDateTo.Text)) = 8 Or Month(CDate(txtDateTo.Text)) = 9 Then
            mQTR = "Q2"
        ElseIf Month(CDate(txtDateTo.Text)) = 10 Or Month(CDate(txtDateTo.Text)) = 11 Or Month(CDate(txtDateTo.Text)) = 12 Then
            mQTR = "Q3"
        ElseIf Month(CDate(txtDateTo.Text)) = 1 Or Month(CDate(txtDateTo.Text)) = 2 Or Month(CDate(txtDateTo.Text)) = 3 Then
            mQTR = "Q4"
        End If

        If FieldsVerification() = False Then Exit Sub

        If Trim(txtMobileNo.Text) = "" Then
            MsgInformation("Please Enter Valid Mobile No.")
            txtMobileNo.Focus()
            Exit Sub
        End If

        If Len(txtMobileNo.Text) <> 10 Then
            MsgInformation("Please Enter Valid Mobile No.")
            txtMobileNo.Focus()
            Exit Sub
        End If

        If mQTR <> "Q1" Then
            If txtTokenNo.Text = "" Then
                MsgInformation("Please Enter Token no. of previous regular statement.")
                txtTokenNo.Focus()
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ShowDosReport("V", mQTR)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTDS(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForTDS(ByRef Mode As Crystal.DestinationConstants)
        Dim MainClass_Renamed As Object

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        If FieldsVerification() = False Then Exit Sub

        PubDBCn.Errors.Clear()

        PrintStatus = False
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        '''''Select Record for print...						
        frmPrintTDS.OptForm26.Text = "Form 24Q"
        frmPrintTDS.OptFormChallan.Text = "Form 24Q (Challan)"
        frmPrintTDS.OptFormChallan.Enabled = False
        frmPrintTDS.OptAnnexure2.Enabled = True
        frmPrintTDS.OptAnnexure3.Enabled = True
        frmPrintTDS.fraAnnx.Enabled = False

        frmPrintTDS.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call InsertIntoPrintDummy()

        If frmPrintTDS.OptForm26.Checked = True Then
            mTitle = "Form No. 24Q"
            mSubTitle = "[See section 192 and rule 37]"

            mReportFileName = "TDSeReturn24Q.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
        ElseIf frmPrintTDS.OptForm27A.Checked = True Then

            mTitle = "Form No. 27A"
            mSubTitle = "[See rule 37B"

            mReportFileName = "TDSeReturn27A.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
        ElseIf frmPrintTDS.OptAnnexure.Checked = True Then
            mTitle = "A N N E X U R E I"
            mSubTitle = "Deductee-wise break-up of TDS"

            mReportFileName = "TDSeReturn24QAnnex_I.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
        ElseIf frmPrintTDS.OptAnnexure2.Checked = True Then
            mTitle = "A N N E X U R E II"
            mSubTitle = ""

            mReportFileName = "TDSeReturn24QAnnex_II_NEW.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
        ElseIf frmPrintTDS.OptAnnexure3.Checked = True Then
            mTitle = "A N N E X U R E III"
            mSubTitle = ""

            mReportFileName = "TDSeReturn24QAnnx_III.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
        End If


        MainClass.ClearCRptFormulas(Report1)

        PrintStatus = True
        frmPrintTDS.Close()
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        frmPrintTDS.Close()
    End Sub
    Private Sub InsertIntoPrintDummy()
        On Error GoTo ERR1

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If frmPrintTDS.OptForm26.Checked = True Then
            If InsertGridDetail(SprdViewChallan, 2, (SprdViewChallan.MaxRows), (SprdViewChallan.MaxCols)) = False Then GoTo ERR1
        ElseIf frmPrintTDS.OptAnnexure.Checked = True Then
            If InsertGridDetail(SprdViewAnnex1, 1, (SprdViewAnnex1.MaxRows), (SprdViewAnnex1.MaxCols)) = False Then GoTo ERR1
        ElseIf frmPrintTDS.OptAnnexure2.Checked = True Then
            If InsertGridDetail(SprdViewAnnex2, 1, (SprdViewAnnex2.MaxRows), (SprdViewAnnex2.MaxCols)) = False Then GoTo ERR1
        ElseIf frmPrintTDS.OptAnnexure3.Checked = True Then
            If InsertGridDetail(SprdViewAnnex3, 1, (SprdViewAnnex3.MaxRows), (SprdViewAnnex3.MaxCols)) = False Then GoTo ERR1
        End If

        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume						
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function InsertGridDetail(ByRef mSprd As Object, ByRef mRowNo As Double, ByRef mMaxRow As Integer, ByRef mMaxCol As Integer) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCol1 As String
        Dim mCol2 As String
        Dim mCol3 As String
        Dim mCol4 As String
        Dim mCol5 As String
        Dim mCol6 As String
        Dim mCol7 As String
        Dim mCol8 As String
        Dim mCol9 As String
        Dim mCol10 As String
        Dim mCol11 As String
        Dim mCol12 As String
        Dim mCol13 As String
        Dim mCol14 As String
        Dim mCol15 As String
        Dim mCol16 As String
        Dim mCol17 As String
        Dim mCol18 As String
        Dim mCol19 As String
        Dim mCol20 As String
        Dim mCol21 As String
        Dim mCol22 As String
        Dim mCol23 As String
        Dim mCol24 As String
        Dim mCol25 As String
        Dim mCol26 As String
        Dim mCol27 As String
        Dim mCol28 As String
        Dim mCol29 As String
        Dim mCol30 As String
        Dim mCol31 As String
        Dim mCol32 As String


        Dim cntRow As Integer


        SqlStr = ""

        With mSprd
            For cntRow = 1 To mMaxRow
                .Row = cntRow

                mRowNo = mRowNo + (0.00001 * cntRow)

                .Col = 1
                mCol1 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 2
                mCol2 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 3
                mCol3 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 4
                mCol4 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 5
                mCol5 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 6
                mCol6 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 7
                mCol7 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 8
                mCol8 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 9
                mCol9 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 10
                mCol10 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 11
                mCol11 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 12
                mCol12 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 13
                mCol13 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 14
                mCol14 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 15
                mCol15 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 16
                mCol16 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 17
                mCol17 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 18
                mCol18 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 19
                mCol19 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 20
                mCol20 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 21
                mCol21 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 22
                mCol22 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 23
                mCol23 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 24
                mCol24 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 25
                mCol25 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 26
                mCol26 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 27
                mCol27 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 28
                mCol28 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 29
                mCol29 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 30
                mCol30 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 31
                mCol31 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

                .Col = 32
                mCol32 = Trim(.Text)

                If mMaxCol = .Col Then GoTo InsertPart

InsertPart:
                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3, " & vbCrLf & " Field4,Field5,Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11,Field12,Field13,Field14,Field15,Field16," & vbCrLf & " Field17,Field18,Field19,Field20,Field21,Field22,Field23," & vbCrLf & " Field24,Field25,Field26,Field27,Field28," & vbCrLf & " Field29,Field30,Field31,Field32" & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol1) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol2) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol3) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol4) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol5) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol6) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol7) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol8) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol9) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol10) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol11) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol12) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol13) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol14) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol15) & "', "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol16) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol17) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol18) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol19) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol20) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol21) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol22) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol23) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol24) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol25) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol26) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol27) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol28) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol29) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol30) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol31) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol32) & "' )"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        InsertGridDetail = True
        Exit Function
ERR1:
        'Resume						
        MsgInformation(Err.Description)
        InsertGridDetail = False
    End Function



    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim mFormTitle As String
        Dim mPartyName As String
        Dim mFormName As String

        Dim mTotChallanNo As Double
        Dim mTotDeductee As Double
        Dim mChallanAmount As Double
        Dim mDeducteeAmount As Double
        Dim mTotPerquisiteRecd As Double

        Dim cntRow As Integer
        Dim mTANNo As String
        Dim mPANNo As String
        Dim mAYEAR As String
        Dim mFYear As String
        Dim mAmountPaid As Double

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        mFormName = "24"
        mTANNo = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        mPANNo = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)

        MainClass.AssignCRptFormulas(Report1, "TANNo=""" & Trim(mTANNo) & """")
        MainClass.AssignCRptFormulas(Report1, "PANNo=""" & Trim(mPANNo) & """")

        If frmPrintTDS.OptForm26.Checked = True Or frmPrintTDS.OptForm27A.Checked = True Or frmPrintTDS.OptAnnexure.Checked = True Then
            If frmPrintTDS.OptForm26.Checked = True Then
                mFormTitle = "Quarterly statement of deduction of tax under sub-section (3) of section 200 of the Income-tax Act, 1961 in respect of Salary for the quarter ended " & VB6.Format(txtDateTo.Text, "MMMM-YYYY")

                MainClass.AssignCRptFormulas(Report1, "FYEAR=""" & txtFYear.Text & """")
                MainClass.AssignCRptFormulas(Report1, "AYEAR=""" & Trim(txtAYear.Text) & """")
                MainClass.AssignCRptFormulas(Report1, "STATUS=""" & Trim(txtReturnFiled.Text) & """")
                MainClass.AssignCRptFormulas(Report1, "ProReceiptNo=""" & Trim(txtProvReceiptNo.Text) & """")

                MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & Trim(txtDeductorType.Text) & """")
                MainClass.AssignCRptFormulas(Report1, "BRANCHNAME=""" & Trim(txtBranch.Text) & """")
            ElseIf frmPrintTDS.OptAnnexure.Checked = True Then
                mFormTitle = "Please use separate Annexure for each line - item in the table at S.No. 04 of main Form 24Q"
            End If

            MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & Trim(txtDeductorType.Text) & """")

            MainClass.AssignCRptFormulas(Report1, "Flat=""" & txtFlat.Text & """")
            MainClass.AssignCRptFormulas(Report1, "Building=""" & Trim(txtBuilding.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Road=""" & Trim(txtRoad.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Area=""" & Trim(txtArea.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Town=""" & txtTown.Text & """")
            MainClass.AssignCRptFormulas(Report1, "State=""" & Trim(txtState.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "PinCode=""" & Trim(txtPinCode.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "PhoneNo=""" & Trim(txtPhone.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Email=""" & Trim(txtEmail.Text) & """")

            MainClass.AssignCRptFormulas(Report1, "PersonName=""" & txtPersonName_p.Text & """")
            MainClass.AssignCRptFormulas(Report1, "Flat_P=""" & txtFlat_p.Text & """")
            MainClass.AssignCRptFormulas(Report1, "Building_P=""" & Trim(txtBuilding_p.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Road_P=""" & Trim(txtRoad_p.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Area_P=""" & Trim(txtArea_p.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Town_P=""" & txtTown_p.Text & """")
            MainClass.AssignCRptFormulas(Report1, "State_P=""" & Trim(txtState_p.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "PinCode_P=""" & Trim(txtPinCode_p.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "PhoneNo_P=""" & Trim(txtPhone_p.Text) & """")
            MainClass.AssignCRptFormulas(Report1, "Email_P=""" & Trim(txtEmail_p.Text) & """")
        ElseIf frmPrintTDS.OptAnnexure3.Checked = True Then
            mFormTitle = "Particulars of values of perquities and amount of accretion to Employee's Provident Fund Account for the Year ending 31st March, " & VB6.Format(txtDateTo.Text, "YYYY")
        End If

        MainClass.AssignCRptFormulas(Report1, "FormTitle=""" & mFormTitle & """")
        MainClass.AssignCRptFormulas(Report1, "AuthName=""" & Trim(txtPersonName_p.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "Designation=""" & Trim(txtDesg.Text) & """")

        If frmPrintTDS.OptForm27A.Checked = True Then

            If GetChallanDetail(mTotChallanNo, mTotDeductee, mTotPerquisiteRecd, mChallanAmount, mDeducteeAmount, mAmountPaid) = False Then GoTo ErrPart

            MainClass.AssignCRptFormulas(Report1, "TotAmountPaid=""" & VB6.Format(mAmountPaid, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "TotDeduct=""" & VB6.Format(mDeducteeAmount, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "TotPerson=""" & mTotDeductee & """")
            MainClass.AssignCRptFormulas(Report1, "FormName=""" & mFormName & """")
            MainClass.AssignCRptFormulas(Report1, "TotChallanAmount=""" & VB6.Format(mChallanAmount, "0.00") & """")

            mFYear = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
            mAYEAR = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")) + 1, "0000")

            MainClass.AssignCRptFormulas(Report1, "FYEAR=""" & mFYear & """")
            MainClass.AssignCRptFormulas(Report1, "AYEAR=""" & mAYEAR & """")

            MainClass.AssignCRptFormulas(Report1, "TotAnnexNo=""1""")


            MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & UCase(Trim(txtDeductorType.Text)) & """")
            '        MainClass.AssignCRptFormulas Report1, "BRANCHNAME=""" & Trim(txtBranch.Text) & """"						

        End If
        If frmPrintTDS.OptForm26.Checked = True Or frmPrintTDS.OptForm27A.Checked = True Then
            MainClass.AssignCRptFormulas(Report1, "ProReceiptNo=""" & Trim(txtProvReceiptNo.Text) & """")
        End If

        ' Report1.CopiesToPrinter = PrintCopies						
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName

        Report1.MarginLeft = 0
        Report1.MarginRight = 0

        Report1.Action = 1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        Dim MainClass_Renamed As Object
        Dim mSection As String

        mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        mSqlStr = mSqlStr & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTDS(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub









    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification() = False Then Exit Sub

        Call PrintStatus(False)
        Call Clear1()

        Show1()
        FormatSprdView()

        Call PrintStatus(True)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1

        If MainClass.ChkIsdateF(txtDateFrom) = False Then FieldsVerification = False : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then FieldsVerification = False : txtDateFrom.Focus() : Exit Function
        If MainClass.ChkIsdateF(txtDateTo) = False Then FieldsVerification = False : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then FieldsVerification = False : txtDateTo.Focus() : Exit Function


        '    If Trim(TxtAccount) = "" Then						
        '        MsgInformation "Please Enter Valid TDS Account Name."						
        '        TxtAccount.SetFocus						
        '        FieldsVerification = False						
        '        Exit Function						
        '    End If						
        '						
        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND HEADTYPE='T'") = False Then						
        '        MsgInformation "Please Enter Valid TDS Account Name."						
        '        TxtAccount.SetFocus						
        '        FieldsVerification = False						
        '        Exit Function						
        '    End If						

        If Trim(txtResponPANNo.Text) = "" Then
            MsgInformation("Please Enter Valid PAN no of Responsibe person Name.")
            txtResponPANNo.Focus()
            FieldsVerification = False
            Exit Function
        End If


        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            If cboCorrectionType.SelectedIndex = 0 Then
                MsgInformation("Please Enter Valid Correction Type.")
                cboCorrectionType.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If



        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub cmdValidate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdValidate.Click
        Dim mFP As Boolean
        '    mFP = Shell(mLocalPath & "\TDS_FVU.bat", vbNormalFocus)						
        Shell(My.Application.Info.DirectoryPath & "\TDS_FVU.bat")
    End Sub

    Public Sub frmTDSeReturn24QNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call PrintStatus(False)
        FormatSprdView()

        cboCorrectionType.Items.Clear()
        cboCorrectionType.Items.Add("0 : None")
        cboCorrectionType.Items.Add("1 : C1 - DEDUCTOR (EXCLUDING TAN) DETAILS")
        cboCorrectionType.Items.Add("2 : C2 - DEDUCTOR (EXCLUDING TAN), AND/OR CHALLAN DETAILS")
        cboCorrectionType.Items.Add("3 : C3 - DEDUCTOR (EXCLUDING TAN), AND/OR CHALLAN, AND/OR DEDUCTEE DETAILS")
        cboCorrectionType.Items.Add("4 : C5 - PAN UPDATE")
        cboCorrectionType.Items.Add("5 : C9 - ADDITION OF CHALLAN")
        cboCorrectionType.Items.Add("6 : Y - CANCELLATION OF STATEMENT")
        cboCorrectionType.SelectedIndex = 0

        cboCorrectionType.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmTDSeReturn24QNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection						
        ''PvtDBCn.Open StrConn						
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(10155)
        SSTAB1.SelectedIndex = 0

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        txtTDSAcNo.Enabled = False
        txtPanNo.Enabled = False

        FormatSprdView()
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSectionCode As Integer
        Dim cntRow As Integer
        Dim mEmpSnoIn26 As Integer
        Dim mCompanyCode As Integer
        Dim mEmpCode As String
        Dim QtrNO As String
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Month(CDate(txtDateTo.Text)) = 4 Or Month(CDate(txtDateTo.Text)) = 5 Or Month(CDate(txtDateTo.Text)) = 6 Then
            QtrNO = "Q1"
        ElseIf Month(CDate(txtDateTo.Text)) = 7 Or Month(CDate(txtDateTo.Text)) = 8 Or Month(CDate(txtDateTo.Text)) = 9 Then
            QtrNO = "Q2"
        ElseIf Month(CDate(txtDateTo.Text)) = 10 Or Month(CDate(txtDateTo.Text)) = 11 Or Month(CDate(txtDateTo.Text)) = 12 Then
            QtrNO = "Q3"
        ElseIf Month(CDate(txtDateTo.Text)) = 1 Or Month(CDate(txtDateTo.Text)) = 2 Or Month(CDate(txtDateTo.Text)) = 3 Then
            QtrNO = "Q4"
        End If


        If ShowDetailChallan() = False Then GoTo ErrPart
        If ShowDetailAnnex1(QtrNO) = False Then GoTo ErrPart

        If QtrNO = "Q4" Then
            If ShowDetailAnnex2() = False Then GoTo ErrPart
            If ShowDetailAnnex3() = False Then GoTo ErrPart
            FormatSprdView()

            Call MainClass.SortGrid(SprdViewAnnex2, 10, 3, True, False)

            With SprdViewAnnex2
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 1
                    .Text = VB6.Format(cntRow, "0")
                Next
            End With

            With SprdViewAnnex3
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 17
                    mCompanyCode = Val(.Text)

                    .Col = 18
                    mEmpCode = Trim(.Text)

                    mEmpSnoIn26 = GetEMPSNoFROM24(mCompanyCode, mEmpCode)

                    .Col = 2
                    .Text = CStr(mEmpSnoIn26)

                Next
            End With
            Call MainClass.SortGrid(SprdViewAnnex3, 2, 1, False, False)
        End If
        FormatSprdView()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)

    End Sub
    Private Function ShowDetailAnnex3() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mAmount245 As Double
        Dim mAmount246 As Double
        Dim mAmount247 As Double
        Dim mAmount248 As Double
        Dim mAmount249 As Double
        Dim mAmount250 As Double
        Dim mAmount251 As Double
        Dim mAmount252 As Double
        Dim mAmount253 As Double
        Dim mAmount254 As Double
        Dim mAmount255 As Double
        Dim mAmount256 As Double
        Dim mAmount257 As Double
        Dim mAmount258 As Double
        Dim mAmount259 As Double
        Dim mAmount260 As Double
        Dim mEmpSnoIn26 As Integer

        Dim mFromCompanyCode As Integer
        Dim mToCompanyCode As Integer
        Dim mCntRow As Integer

        SqlStr = " Select IH.*, " & vbCrLf & " EMP.EMP_PANNO,EMP.EMP_NAME "

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITFORM12BA_HDR IH, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE || IH.EMP_CODE IN (" & GetEmpCodeQry() & ")"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_NAME,IH.COMPANY_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1

        With SprdViewAnnex3
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF

                    If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If EMPTransfer(RsTemp.Fields("EMP_CODE").Value, RsTemp.Fields("COMPANY_CODE").Value) = True Then
                            mEmpSnoIn26 = 0
                            GoTo NextRec
                        Else
                            mToCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                        End If
                    Else
                        mToCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                    End If

                    mEmpSnoIn26 = GetEMPSNoFROM24(mToCompanyCode, RsTemp.Fields("EMP_CODE").Value)

                    If mEmpSnoIn26 = 0 Then GoTo NextRec

                    .Row = cntRow
                    .Col = 1
                    .Text = CStr(cntRow)

                    .Col = 1
                    .Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)

                    .Col = 2
                    .Text = CStr(mEmpSnoIn26)

                    .Col = 3
                    mAmount247 = GetAmountFromDetail12BA(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 1)
                    .Text = VB6.Format(mAmount247, "0.00")

                    .Col = 4
                    mAmount248 = GetAmountFromDetail12BA(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 1)
                    .Text = VB6.Format(mAmount248, "0.00")

                    .Col = 5
                    mAmount249 = 0 ''GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)						
                    .Text = VB6.Format(mAmount249, "0.00")

                    .Col = 6
                    mAmount250 = mAmount249 * 0.01
                    .Text = VB6.Format(mAmount250, "0.00")

                    .Col = 7
                    mAmount251 = mAmount248 + mAmount250
                    .Text = VB6.Format(mAmount251, "0.00")

                    .Col = 8
                    mAmount252 = 0 '''GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)						
                    .Text = VB6.Format(mAmount252, "0.00")

                    .Col = 9
                    mAmount253 = mAmount247 - mAmount252
                    .Text = VB6.Format(mAmount253, "0.00")

                    .Col = 10
                    mAmount254 = GetAmountFromDetail12BA(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 2)
                    .Text = VB6.Format(mAmount254, "0.00")

                    .Col = 11
                    mAmount255 = GetAmountFromDetail12BA(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 3)
                    .Text = VB6.Format(mAmount255, "0.00")

                    .Col = 12
                    mAmount256 = 0 ''GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)						
                    .Text = VB6.Format(mAmount256, "0.00")

                    .Col = 13
                    '                mAmount257 = GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 4)						
                    '                mAmount257 = mAmount257 + GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 5)						
                    mAmount257 = 0
                    For mCntRow = 4 To 17
                        mAmount257 = mAmount257 + GetAmountFromDetail12BA(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, mCntRow)
                    Next
                    .Text = VB6.Format(mAmount257, "0.00")

                    .Col = 14
                    mAmount258 = 0 '''GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)						
                    .Text = VB6.Format(mAmount258, "0.00")

                    .Col = 15
                    mAmount259 = 0 '''GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)						
                    .Text = VB6.Format(mAmount259, "0.00")

                    .Col = 16
                    mAmount260 = mAmount253 + mAmount254 + mAmount255 + mAmount256 + mAmount257 + mAmount258 + mAmount259
                    .Text = VB6.Format(mAmount260, "0.00")


                    .Col = 17
                    .Text = CStr(IIf(IsDBNull(mToCompanyCode), "", mToCompanyCode))

                    .Col = 18
                    .Text = CStr(IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value))

                    '                Call CalcForm24(mEmpSnoIn26, mAmount260)						

NextRec:
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False And mEmpSnoIn26 <> 0 Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop
            End If
        End With
        ShowDetailAnnex3 = True
        Exit Function
ErrPart1:
        ShowDetailAnnex3 = False
        '    Resume						
    End Function

    Private Function ShowDetailAnnex1(ByRef pQtrNO As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mAmount318 As Double
        Dim mAmount319 As Double
        Dim mAmount320 As Double
        Dim mAmount321 As Double
        Dim mAmount322 As Double
        Dim mAmount323 As Double
        Dim mPANNo As String

        Dim mChallanWiseSNo As Integer
        Dim mPrevChallanMkey As String
        Dim mChallanSNo As Integer
        Dim mChallanMkey As String

        Dim mSectionCode As String
        Dim mBSRCode As String
        Dim mDepositDate As String
        Dim mChallanNo As String

        Dim mTotalTDS As Double
        Dim mTotalInerest As Double
        Dim mOtherAmt As Double
        Dim mTotalTaxDeposit As Double
        Dim mTaxableAmount As Double
        Dim mTotalTaxableAmount As Double
        Dim mCntEmpChallan As Integer

        Dim RsTempTrf As ADODB.Recordset

        Dim mToCompanyCode As Integer
        Dim mToEmpCode As String

        SqlStr = " Select IH.AUTO_KEY_REFNO,IH.COMPANY_CODE, IH.CHQ_DATE, IH.VDATE, IH.CHALLANDATE, ID.*, " & vbCrLf & " ID.EMP_CODE, EMP.EMP_PANNO, EMP.EMP_NAME "

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf & " AND ID.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND ID.EMP_CODE=EMP.EMP_CODE"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        '    SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_NAME LIKE 'PANKAJ A%'"						

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1

        With SprdViewAnnex1
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF

                    '''Transfer Emp Data ...........						
                    If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                        '                    SqlStr = " SELECT * " & vbCrLf _						
                        ''                            & " FROM PAY_EMP_TRF_MST" & vbCrLf _						
                        ''                            & " WHERE " & vbCrLf _						
                        ''                            & " FROM_COMPANY_CODE = " & RsTemp!COMPANY_CODE & "" & vbCrLf _						
                        ''                            & " AND FROM_EMP_CODE = '" & RsTemp!EMP_CODE & "'"						

                        mToCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                        mToEmpCode = RsTemp.Fields("EMP_CODE").Value

SearchRow:
                        SqlStr = GetEmpTransferSQL(mToEmpCode, mToCompanyCode, "Y")
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempTrf, ADODB.LockTypeEnum.adLockOptimistic)

                        If RsTempTrf.EOF = False Then
                            mToCompanyCode = IIf(IsDBNull(RsTempTrf.Fields("TO_COMPANY_CODE").Value), "", RsTempTrf.Fields("TO_COMPANY_CODE").Value)
                            mToEmpCode = IIf(IsDBNull(RsTempTrf.Fields("TO_EMP_CODE").Value), "", RsTempTrf.Fields("TO_EMP_CODE").Value)
                            GoTo SearchRow
                            '                    Else						
                            '                        mToCompanyCode = IIf(IsNull(RsTemp!COMPANY_CODE), "", RsTemp!COMPANY_CODE)						
                            '                        mToEmpCode = IIf(IsNull(RsTemp!EMP_CODE), "", RsTemp!EMP_CODE)						
                        End If
                    Else
                        mToCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value)
                        mToEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                    End If
                    ''''**********************************						
                    .Row = cntRow
                    .Col = 1
                    If Trim(mPrevChallanMkey) = Trim(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value)) Then
                        mChallanWiseSNo = mChallanWiseSNo + 1
                    Else
                        mChallanWiseSNo = 1
                        mChallanMkey = Trim(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value))
                        mChallanSNo = GetChallanSNO(mChallanMkey, mSectionCode, mBSRCode, mDepositDate, mChallanNo, mTotalTDS, mTotalInerest, mOtherAmt, mTotalTaxDeposit)
                    End If

                    .Text = CStr(mChallanWiseSNo) '''cntRow						

                    .Col = 2
                    .Text = mToEmpCode ''IIf(IsNull(RsTemp!EMP_CODE), "", mToEmpCode)						

                    .Col = 3
                    mPANNo = IIf(IsDBNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
                    If Len(mPANNo) = 10 Then
                        .Text = IIf(IsDBNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
                    ElseIf UCase(Trim(mPANNo)) = "A/F" Then
                        .Text = "PANAPPLIED"
                    ElseIf Trim(mPANNo) = "" Then
                        .Text = "PANNOTAVBL"
                    Else
                        .Text = "PANINVALID"
                    End If

                    .Col = 4
                    .Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)

                    .Col = 5
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY") '' Format(IIf(IsNull(RsTemp!CHQ_DATE), "", RsTemp!CHQ_DATE), "DD/MM/YYYY")						

                    .Col = 6
                    '                mTaxableAmount = GetAmountFromDetail(mToCompanyCode, RsCompany.Fields("FYEAR").Value, mToEmpCode, 65, "TOTALAMOUNT")						
                    mCntEmpChallan = GetEmpChallanNo(mToEmpCode, mToCompanyCode, RsCompany.Fields("START_DATE").Value, RsCompany.Fields("END_DATE").Value)
                    '                If mCntEmpChallan <> 0 Then						
                    '                    mTaxableAmount = Round(mTaxableAmount / mCntEmpChallan, 0)						
                    '                End If						
                    mTaxableAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("AMOUNT_PAID").Value), "", RsTemp.Fields("AMOUNT_PAID").Value), "0.00"))
                    '                If pQtrNO = "Q4" Then						
                    '                    mTotalTaxableAmount = GetAmountFromDetail(mToCompanyCode, RsCompany.Fields("FYEAR").Value, mToEmpCode, 65, "TOTALAMOUNT")						
                    '                    mTotalChallanPaidAmount = GetChallanPaidAmount(mToEmpCode, mToCompanyCode, RsCompany!START_DATE, RsCompany!END_DATE)						
                    '                    mTaxableAmount = mTotalTaxableAmount - mTotalChallanPaidAmount						
                    '                    mTaxableAmount = IIf(mTaxableAmount < 0, 0, mTaxableAmount)						
                    '                End If						
                    .Text = VB6.Format(mTaxableAmount, "0.00")

                    .Col = 7
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_AMOUNT").Value), "", RsTemp.Fields("TDS_AMOUNT").Value), "0.00")

                    .Col = 8
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURCHARGE_AMT").Value), "", RsTemp.Fields("SURCHARGE_AMT").Value), "0.00")

                    .Col = 9
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CESS_AMT").Value), "", RsTemp.Fields("CESS_AMT").Value), "0.00")

                    .Col = 10
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value), "0.00")

                    .Col = 11
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value), "0.00")

                    .Col = 12
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                    .Col = 13
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")

                    .Col = 14
                    .Text = ""

                    .Col = 15
                    .Text = Str(IIf(IsDBNull(mToCompanyCode), "", mToCompanyCode))

                    .Col = 16
                    .Text = Str(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value))
                    mPrevChallanMkey = Str(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value))

                    .Col = 17
                    .Text = Trim(mBSRCode)

                    .Col = 18
                    .Text = Trim(mDepositDate)

                    .Col = 19
                    .Text = Trim(mChallanNo)

                    .Col = 20
                    .Text = Trim(mSectionCode)

                    .Col = 21
                    .Text = VB6.Format(mTotalTDS, "0.00")

                    .Col = 22
                    .Text = VB6.Format(mTotalInerest, "0.00")

                    .Col = 23
                    .Text = VB6.Format(mOtherAmt, "0.00")

                    .Col = 24
                    .Text = VB6.Format(mTotalTaxDeposit, "0.00")
NextRec:
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop
            End If
        End With
        ShowDetailAnnex1 = True
        Exit Function
ErrPart1:
        ShowDetailAnnex1 = False
        '    Resume						
    End Function

    Private Function GetYearlyChallanAmount(ByRef pEmpCode As String, ByRef pCompanyCode As Integer) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim RsTempTrf As ADODB.Recordset
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        SqlStr = " Select SUM(ID.AMOUNT) AS AMOUNT "

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & pCompanyCode & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO "
        SqlStr = SqlStr & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetYearlyChallanAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
            '''Transfer Emp Data ...........						

            mToEmpCompany = pCompanyCode
            mToEmpCode = pEmpCode

SearchRow:
            SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempTrf, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTempTrf.EOF = False Then
                mFromEmpCompany = IIf(IsDBNull(RsTempTrf.Fields("FROM_COMPANY_CODE").Value), "", RsTempTrf.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDBNull(RsTempTrf.Fields("FROM_EMP_CODE").Value), "", RsTempTrf.Fields("FROM_EMP_CODE").Value)

                SqlStr = " Select SUM(ID.AMOUNT) AS AMOUNT "

                SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & mFromEmpCompany & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
                SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO "
                SqlStr = SqlStr & vbCrLf & " AND ID.EMP_CODE='" & mFromEmpCode & "'"

                SqlStr = SqlStr & vbCrLf _
                    & " AND IH.VDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND IH.VDATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    GetYearlyChallanAmount = GetYearlyChallanAmount + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                End If
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
                GoTo SearchRow
            End If
        End If


        Exit Function
ErrPart1:
        GetYearlyChallanAmount = 0
        '    Resume						
    End Function

    Private Function GetEmpCodeQry() As String
        On Error GoTo ErrPart1
        Dim SqlStr As String

        SqlStr = " Select IH.COMPANY_CODE || ID.EMP_CODE"

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf & " AND ID.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND ID.EMP_CODE=EMP.EMP_CODE"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        '    SqlStr = SqlStr & vbCrLf _						
        ''        & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"						



        GetEmpCodeQry = SqlStr
        Exit Function
ErrPart1:
        GetEmpCodeQry = ""
        '    Resume						
    End Function
    Private Function GetLastChallanRecd() As Integer
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetLastChallanRecd = 0
        SqlStr = " Select COUNT(1) CNTRECD"

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH" & vbCrLf & " WHERE "

        'SqlStr = SqlStr & vbCrLf _						
        '& " ID.COMPANY_CODE=EMP.COMPANY_CODE " ''& vbCrLf _						
        '& " AND ID.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _						
        '& " AND ID.EMP_CODE=EMP.EMP_CODE"						

        SqlStr = SqlStr & vbCrLf & " IH.BOOKTYPE<>'C'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLastChallanRecd = IIf(IsDBNull(RsTemp.Fields("CNTRECD").Value), 0, RsTemp.Fields("CNTRECD").Value)
        End If

        Exit Function
ErrPart1:
        GetLastChallanRecd = 0
        '    Resume						
    End Function

    Private Function GetEmpChallanNo(ByRef pEmpCode As String, ByRef pCompany_Code As Integer, ByRef mFromDate As String, ByRef mToDate As String) As Integer
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pCntEmpChallan As Integer

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim RsTempDetail As ADODB.Recordset

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        pCntEmpChallan = 0
        SqlStr = " Select Count(ID.EMP_CODE) AS CNT"

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.EMP_CODE='" & pEmpCode & "'"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & pCompany_Code & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        '    SqlStr = SqlStr & vbCrLf _						
        ''        & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"						

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pCntEmpChallan = CInt(Trim(IIf(IsDBNull(RsTemp.Fields("CNT").Value), 0, RsTemp.Fields("CNT").Value)))
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
            '''Transfer Emp Data ...........						
            '        SqlStr = " SELECT * " & vbCrLf _						
            ''                & " FROM PAY_EMP_TRF_MST" & vbCrLf _						
            ''                & " WHERE " & vbCrLf _						
            ''                & " TO_COMPANY_CODE = " & mCompanyCode & "" & vbCrLf _						
            ''                & " AND TO_EMP_CODE = '" & mEmpCode & "'"						

            mToEmpCompany = pCompany_Code
            mToEmpCode = pEmpCode

SearchRow:
            SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

                SqlStr = " Select Count(ID.EMP_CODE) AS CNT" & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf _
                    & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND ID.EMP_CODE='" & mFromEmpCode & "'"

                If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & mFromEmpCompany & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDetail.EOF = False Then
                    pCntEmpChallan = pCntEmpChallan + CDbl(Trim(IIf(IsDBNull(RsTempDetail.Fields("CNT").Value), 0, RsTempDetail.Fields("CNT").Value)))
                End If
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
                GoTo SearchRow
            End If
        End If

        GetEmpChallanNo = pCntEmpChallan
        Exit Function
ErrPart1:
        pCntEmpChallan = 0
    End Function
    Private Function GetChallanPaidAmount(ByRef pEmpCode As String, ByRef pCompany_Code As Integer, ByRef mFromDate As String, ByRef mToDate As String) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pCntEmpChallan As Integer

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim RsTempDetail As ADODB.Recordset

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        GetChallanPaidAmount = 0
        SqlStr = " Select SUM(ID.AMOUNT_PAID) AS AMOUNT_PAID"

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.EMP_CODE='" & pEmpCode & "'"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & pCompany_Code & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        '    SqlStr = SqlStr & vbCrLf _						
        ''        & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"						

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetChallanPaidAmount = CDbl(Trim(IIf(IsDBNull(RsTemp.Fields("AMOUNT_PAID").Value), 0, RsTemp.Fields("AMOUNT_PAID").Value)))
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
            '''Transfer Emp Data ...........						
            '        SqlStr = " SELECT * " & vbCrLf _						
            ''                & " FROM PAY_EMP_TRF_MST" & vbCrLf _						
            ''                & " WHERE " & vbCrLf _						
            ''                & " TO_COMPANY_CODE = " & mCompanyCode & "" & vbCrLf _						
            ''                & " AND TO_EMP_CODE = '" & mEmpCode & "'"						

            mToEmpCompany = pCompany_Code
            mToEmpCode = pEmpCode

SearchRow:
            SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

                SqlStr = " Select SUM(ID.AMOUNT_PAID) AS AMOUNT_PAID" & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf & " WHERE " & vbCrLf _
                    & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf _
                    & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND ID.EMP_CODE='" & mFromEmpCode & "'"

                If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & mFromEmpCompany & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDetail.EOF = False Then
                    GetChallanPaidAmount = GetChallanPaidAmount + IIf(IsDBNull(RsTemp.Fields("AMOUNT_PAID").Value), 0, RsTemp.Fields("AMOUNT_PAID").Value)
                End If
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
                GoTo SearchRow
            End If
        End If
        Exit Function
ErrPart1:
        GetChallanPaidAmount = 0
    End Function

    Private Function GetChallanEMPTDSAmount(ByRef pEmpCode As String, ByRef pCompany_Code As Integer, ByRef mFromDate As String, ByRef mToDate As String) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pCntEmpChallan As Integer

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim RsTempDetail As ADODB.Recordset

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        GetChallanEMPTDSAmount = 0
        SqlStr = " Select SUM(ID.AMOUNT) AS AMOUNT"

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.EMP_CODE='" & pEmpCode & "'"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & pCompany_Code & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        '    SqlStr = SqlStr & vbCrLf _						
        ''        & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"						

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetChallanEMPTDSAmount = CDbl(Trim(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)))
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
            '''Transfer Emp Data ...........						
            '        SqlStr = " SELECT * " & vbCrLf _						
            ''                & " FROM PAY_EMP_TRF_MST" & vbCrLf _						
            ''                & " WHERE " & vbCrLf _						
            ''                & " TO_COMPANY_CODE = " & mCompanyCode & "" & vbCrLf _						
            ''                & " AND TO_EMP_CODE = '" & mEmpCode & "'"						

            mToEmpCompany = pCompany_Code
            mToEmpCode = pEmpCode

SearchRow:
            SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

                SqlStr = " Select SUM(ID.AMOUNT) AS AMOUNT" & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf _
                    & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND ID.EMP_CODE='" & mFromEmpCode & "'"

                If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & mFromEmpCompany & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDetail.EOF = False Then
                    GetChallanEMPTDSAmount = GetChallanEMPTDSAmount + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                End If
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
                GoTo SearchRow
            End If
        End If
        Exit Function
ErrPart1:
        GetChallanEMPTDSAmount = 0
    End Function

    Private Function GetChallanSNO(ByRef pChallanMKey As String, ByRef pSectionCode As String, ByRef pBSRCode As String, ByRef pDepositDate As String, ByRef pChallanNo As String, ByRef pTotalTDS As Double, ByRef pTotalInerest As Double, ByRef pOtherAmt As Double, ByRef pTotalTaxDeposit As Double) As Integer
        On Error GoTo ErrPart1
        Dim cntRow As Integer

        GetChallanSNO = 0
        With SprdViewChallan
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 14
                If Trim(.Text) = Trim(pChallanMKey) Then
                    GetChallanSNO = cntRow
                    '                .Col = 2						
                    pSectionCode = "92B"

                    .Col = 2
                    pTotalTDS = Val(.Text)

                    .Col = 3
                    pTotalTDS = pTotalTDS + Val(.Text)

                    .Col = 4
                    pTotalTDS = pTotalTDS + Val(.Text)

                    .Col = 5
                    pTotalInerest = Val(.Text)

                    .Col = 6
                    pOtherAmt = Val(.Text)

                    .Col = 7
                    pTotalTaxDeposit = Val(.Text)

                    .Col = 9
                    pBSRCode = Trim(.Text)

                    .Col = 10
                    pDepositDate = Trim(.Text)

                    .Col = 11
                    pChallanNo = Trim(.Text)

                    Exit For
                End If
            Next
        End With



        Exit Function
ErrPart1:

    End Function
    Private Function ShowDetailChallan() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer

        cntRow = 1

        SqlStr = "Select " & vbCrLf & " AUTO_KEY_REFNO, VDATE, COMPANY_CODE, FYEAR, " & vbCrLf & " BOOKTYPE, AYEAR, CHALLANNO, CHALLANDATE, " & vbCrLf & " CHQ_NO, CHQ_DATE, BANKNAME, BSRCODE, " & vbCrLf & " TDS_AMOUNT, SURCHARGE, EDU_CESS, " & vbCrLf & " INTEREST_AMOUNT, OTHER_AMOUNT, NETAMOUNT " & vbCrLf & " FROM PAY_ITCHALLAN_HDR  " & vbCrLf & " WHERE "



        SqlStr = SqlStr & vbCrLf _
            & " VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'C'"
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY AUTO_KEY_REFNO,CHALLANDATE,COMPANY_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        With SprdViewChallan
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    .MaxRows = cntRow
                    .Row = cntRow

                    .Col = 1
                    .Text = Str(cntRow)

                    .Col = 2
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_AMOUNT").Value), 0, RsTemp.Fields("TDS_AMOUNT").Value), "0.00")

                    .Col = 3
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURCHARGE").Value), 0, RsTemp.Fields("SURCHARGE").Value), "0.00")

                    .Col = 4
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EDU_CESS").Value), 0, RsTemp.Fields("EDU_CESS").Value), "0.00")

                    .Col = 5
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INTEREST_AMOUNT").Value), 0, RsTemp.Fields("INTEREST_AMOUNT").Value), "0.00")

                    .Col = 6
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OTHER_AMOUNT").Value), 0, RsTemp.Fields("OTHER_AMOUNT").Value), "0.00")

                    .Col = 7
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("NETAMOUNT").Value), 0, RsTemp.Fields("NETAMOUNT").Value), "0.00")

                    .Col = 8
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHQ_NO").Value), "", RsTemp.Fields("CHQ_NO").Value)

                    .Col = 9
                    .Text = IIf(IsDBNull(RsTemp.Fields("BSRCODE").Value), "", RsTemp.Fields("BSRCODE").Value)

                    .Col = 10
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")

                    .Col = 11
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHALLANNO").Value), "", RsTemp.Fields("CHALLANNO").Value)

                    .Col = 12
                    .Text = "N"

                    .Col = 13
                    .Text = Str(RsTemp.Fields("COMPANY_CODE").Value)

                    .Col = 14
                    .Text = Str(RsTemp.Fields("AUTO_KEY_REFNO").Value)


                    RsTemp.MoveNext()
                    cntRow = cntRow + 1
                Loop
            End If
        End With
        ShowDetailChallan = True
        Exit Function
ErrPart1:
        '    Resume						
        ShowDetailChallan = False
    End Function
    Private Function ShowDetailAnnex2() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mDateFrom As String
        Dim mDOJ As String
        Dim mAmount331 As Double
        Dim mAmount332 As Double
        Dim mAmount333 As Double
        Dim mAmount334 As Double
        Dim mAmount335 As Double
        Dim mAmount336 As Double
        Dim mAmount337 As Double
        Dim mAmount338 As Double
        Dim mAmount339 As Double
        Dim mAmount340 As Double
        Dim mAmount341 As Double
        Dim mAmount342 As Double
        Dim mAmount343 As Double
        Dim mAmount344 As Double
        Dim mAmount345 As Double
        Dim mAmount346 As Double
        Dim mAmount347 As Double
        'Dim mAmount348 As Double						
        'Dim mAmount349 As Double						
        'Dim mAmount350 As Double						
        'Dim mAmount351 As Double						
        'Dim mAmount352 As Double						
        Dim mPANNo As String
        Dim mTaxAmount As Double
        Dim mSex As String
        Dim mEmpCode As String
        Dim mFromCompanyCode As Integer
        Dim mToCompanyCode As Integer

        Dim mIncomeAmount As Double
        Dim mTDSAmount As Double
        Dim mMax80C As Double
        Dim mEmpDOB As String
        Dim mAge As Double

        SqlStr = " Select IH.*, " & vbCrLf & " EMP.EMP_PANNO,EMP.EMP_NAME,EMP_SEX,EMP_DOJ,EMP_DOB "

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCOMP_HDR IH, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.TAX_DED>0"

        SqlStr = SqlStr & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        '    SqlStr = SqlStr & vbCrLf _						
        ''            & " AND IH.COMPANY_CODE || IH.FYEAR || IH.EMP_CODE IN ( " & vbCrLf _						
        ''            & " SELECT COMPANY_CODE || FYEAR || EMP_CODE " & vbCrLf _						
        ''            & " FROM PAY_ITCOMP_TRN " & vbCrLf _						
        ''            & " WHERE SUBROWNO=61 " & vbCrLf _						
        ''            & " AND TOTALAMOUNT>100000" & vbCrLf _						
        ''            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""						
        '						
        '    If chkConsolidated.Value = vbUnchecked Then						
        '       SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany!COMPANY_CODE & ""						
        '    End If						
        '						
        '    SqlStr = SqlStr & vbCrLf & ")"						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE || IH.EMP_CODE IN (" & GetEmpCodeQry() & ")"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_NAME,IH.COMPANY_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1

        With SprdViewAnnex2
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF

                    If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If EMPTransfer(RsTemp.Fields("EMP_CODE").Value, RsTemp.Fields("COMPANY_CODE").Value) = True Then
                            mTaxAmount = 0
                            GoTo NextRec
                        Else
                            mToCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                        End If
                    Else
                        mToCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                    End If

                    '                If RsTemp!EMP_CODE = "000887" Then MsgBox RsTemp!EMP_CODE						
                    mTaxAmount = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 75, "TOTALAMOUNT")
                    mTaxAmount = mTaxAmount + GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 76, "TOTALAMOUNT")

                    If mTaxAmount = 0 Then GoTo NextRec

                    .MaxRows = cntRow
                    .Row = cntRow
                    .Col = 1
                    .Text = CStr(cntRow)

                    .Col = 2
                    mPANNo = IIf(IsDBNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
                    If Len(mPANNo) = 10 Then
                        .Text = IIf(IsDBNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
                    ElseIf UCase(Trim(mPANNo)) = "A/F" Then
                        .Text = "PANAPPLIED"
                    ElseIf Trim(mPANNo) = "" Then
                        .Text = "PANNOTAVBL"
                    Else
                        .Text = "PANINVALID"
                    End If


                    .Col = 3
                    .Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)

                    .Col = 4
                    mEmpDOB = IIf(IsDBNull(RsTemp.Fields("EMP_DOB").Value), "M", RsTemp.Fields("EMP_DOB").Value)
                    mAge = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(mEmpDOB), CDate(txtDateTo.Text)) 'datediff("yyyy","01/04/2010","01/04/2017")						

                    mSex = IIf(IsDBNull(RsTemp.Fields("EMP_SEX").Value), "M", RsTemp.Fields("EMP_SEX").Value)
                    If mAge >= 80 Then
                        .Text = "O"
                    ElseIf mAge >= 60 Then
                        .Text = "S"
                    Else
                        .Text = IIf(mSex = "F", "W", "G")
                    End If

                    .Col = 5


                    mDOJ = GetEMPDOJ(RsTemp.Fields("EMP_CODE").Value, RsTemp.Fields("COMPANY_CODE").Value) ''Format(IIf(IsNull(RsTemp!EMP_DOJ), "", RsTemp!EMP_DOJ), "DD/MM/YYYY")						
                    mDateFrom = VB6.Format(IIf(IsDBNull(RsCompany.Fields("START_DATE").Value), "", RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")

                    If CDate(mDOJ) > CDate(mDateFrom) Then
                        .Text = mDOJ
                    Else
                        .Text = mDateFrom
                    End If

                    '                .Text = Format(IIf(IsNull(RsCompany!Start_Date), "", RsCompany!Start_Date), "DD/MM/YYYY")						
                    '                .Text = Format(IIf(IsNull(RsTemp!FROMDATE), "", RsTemp!FROMDATE), "DD/MM/YYYY")						

                    .Col = 6
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                    .Col = 7

                    ''28-04-2007						
                    '                mAmount332 = GetAmountFromDetail(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 36, "TOTALAMOUNT")						
                    mAmount332 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 42, "TOTALAMOUNT")
                    mAmount332 = System.Math.Round(mAmount332, 0)
                    mAmount332 = mAmount332 + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("TAXABLE_INCOME_PE").Value), "", RsTemp.Fields("TAXABLE_INCOME_PE").Value), "0.00"))

                    .Text = VB6.Format(mAmount332, "0.00")

                    .Col = 8
                    mAmount333 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 43, "AMOUNT1")
                    .Text = VB6.Format(mAmount333, "0.00")

                    .Col = 9
                    mAmount334 = mAmount332 - mAmount333
                    .Text = VB6.Format(mAmount334, "0.00")

                    .Col = 10
                    mAmount335 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 47, "TOTALAMOUNT") ''GetAmountFromDetail12BA(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 25)						
                    .Text = VB6.Format(mAmount335, "0.00")

                    .Col = 11
                    mAmount336 = mAmount334 + mAmount335
                    .Text = VB6.Format(mAmount336, "0.00")

                    .Col = 12
                    mMax80C = GetMaxRebate("SLAB_80C")
                    mAmount337 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 63, "AMOUNT2")
                    mAmount337 = IIf(mAmount337 > mMax80C, mMax80C, mAmount337)

                    .Text = VB6.Format(mAmount337, "0.00")

                    .Col = 13
                    mAmount338 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 50, "AMOUNT2")
                    mAmount338 = mAmount338 + GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 51, "AMOUNT2")
                    mAmount338 = mAmount338 + GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 52, "AMOUNT2")
                    '                mAmount338 = mAmount338 - GetAmountFromDetail(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 48, "AMOUNT2")						
                    .Text = VB6.Format(mAmount338, "0.00")

                    .Col = 25
                    .Text = CStr(GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 54, "AMOUNT2"))

                    .Col = 14
                    mAmount339 = mAmount337 + mAmount338
                    .Text = VB6.Format(mAmount339, "0.00")


                    .Col = 15
                    mAmount340 = mAmount336 - mAmount339
                    .Text = VB6.Format(mAmount340, "0.00")


                    .Col = 16
                    mAmount341 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 71, "TOTALAMOUNT")
                    .Text = VB6.Format(System.Math.Round(mAmount341, 0), "0.00")


                    .Col = 17
                    mAmount342 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 72, "TOTALAMOUNT")
                    .Text = VB6.Format(mAmount342, "0.00")

                    .Col = 18
                    mAmount343 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 74, "TOTALAMOUNT")
                    .Text = VB6.Format(mAmount343, "0.00")

                    .Col = 19
                    mAmount344 = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 73, "AMOUNT1")
                    .Text = VB6.Format(mAmount344, "0.00")

                    .Col = 20
                    mAmount345 = mAmount341 + mAmount342 + mAmount343 - mAmount344
                    .Text = VB6.Format(mAmount345, "0.00")

                    .Col = 21
                    mEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                    mAmount346 = GetYearlyChallanAmount(mEmpCode, mToCompanyCode) '' GetAmount26FromAnnex1(mEmpCode, mToCompanyCode)						
                    mAmount346 = mAmount346 + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_PE").Value), "", RsTemp.Fields("TDS_PE").Value), "0.00"))
                    .Text = VB6.Format(mAmount346, "0.00")

                    .Col = 22
                    mAmount347 = mAmount345 - mAmount346
                    .Text = VB6.Format(mAmount347, "0.00")

                    .Col = 23
                    .Text = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)

                    .Col = 24
                    .Text = CStr(IIf(IsDBNull(mToCompanyCode), "", mToCompanyCode))

                    .Col = 26
                    mIncomeAmount = GetAmountFromDetail(mToCompanyCode, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 42, "TOTALAMOUNT") '' GetAmountFromDetail(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 65, "TOTALAMOUNT")						
                    mIncomeAmount = System.Math.Round(mIncomeAmount, 0)
                    .Text = VB6.Format(mIncomeAmount, "0.00")

                    .Col = 27
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TAXABLE_INCOME_PE").Value), "", System.Math.Round(RsTemp.Fields("TAXABLE_INCOME_PE").Value, 0)), "0.00")

                    .Col = 28
                    mTDSAmount = GetYearlyChallanAmount(mEmpCode, mToCompanyCode) '' GetChallanEMPTDSAmount(RsTemp!EMP_CODE, mToCompanyCode, RsCompany!START_DATE, txtDateTo.Text) ''GetAmountFromDetail(mToCompanyCode, RsTemp!FYEAR, RsTemp!EMP_CODE, 76, "TOTALAMOUNT")						
                    mTDSAmount = System.Math.Round(mTDSAmount, 0)
                    .Text = VB6.Format(mTDSAmount, "0.00")

                    .Col = 29
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_PE").Value), "", System.Math.Round(RsTemp.Fields("TDS_PE").Value, 0)), "0.00")


NextRec:
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        If mTaxAmount > 0 Then
                            cntRow = cntRow + 1
                        End If
                    End If
                Loop
            End If
        End With
        ShowDetailAnnex2 = True
        Exit Function
ErrPart1:
        '    Resume						
        ShowDetailAnnex2 = False
    End Function

    Private Sub CalcForm24(ByRef pcntRow As Integer, ByRef pAmount260 As Double)
        On Error GoTo ErrPart1
        Dim mAmount205 As Double
        Dim mAmount206 As Double
        Dim mAmount207 As Double
        Dim mAmount208 As Double
        Dim mAmount209 As Double
        Dim mAmount210 As Double
        Dim mAmount211 As Double
        Dim mAmount212 As Double
        Dim mAmount213 As Double
        Dim mAmount214 As Double
        Dim mAmount215 As Double
        Dim mAmount216 As Double
        Dim mAmount217 As Double
        Dim mAmount218 As Double
        Dim mAmount219 As Double
        Dim mAmount220 As Double
        Dim mAmount221 As Double
        Dim mAmount222 As Double
        Dim mAmount223 As Double
        Dim mAmount224 As Double
        Dim mAmount225 As Double
        Dim mAmount226 As Double
        Dim mAmount227 As Double
        Dim mAmount228 As Double
        Dim mAmount229 As Double
        Dim mAmount230 As Double
        Dim mAmount231 As Double
        Dim mAmount232 As Double

        Exit Sub

        With SprdViewAnnex2
            If .MaxRows < pcntRow Then Exit Sub
            .Row = pcntRow

            .Col = 6
            mAmount205 = Val(.Text)

            .Col = 7
            mAmount206 = Val(.Text)

            .Col = 8
            mAmount207 = CDbl(VB6.Format(pAmount260, "0.00"))
            .Text = VB6.Format(mAmount207, "0.00")

            .Col = 9
            mAmount208 = Val(.Text)

            .Col = 10
            mAmount209 = mAmount205 + mAmount206 + mAmount207
            .Text = VB6.Format(mAmount209, "0.00")

            .Col = 11
            mAmount210 = Val(.Text)

            .Col = 12
            mAmount211 = mAmount209 - mAmount210
            .Text = VB6.Format(mAmount211, "0.00")

            .Col = 13
            mAmount212 = Val(.Text)

            .Col = 14
            mAmount213 = mAmount211 + mAmount212
            .Text = VB6.Format(mAmount213, "0.00")

            .Col = 15
            mAmount214 = Val(.Text)
            .Text = VB6.Format(mAmount214, "0.00")

            .Col = 16
            mAmount215 = Val(.Text)
            .Text = VB6.Format(mAmount215, "0.00")

            .Col = 17
            mAmount216 = Val(.Text)
            .Text = VB6.Format(mAmount216, "0.00")

            .Col = 18
            mAmount217 = mAmount214 + mAmount215 + mAmount216
            .Text = VB6.Format(mAmount217, "0.00")

            .Col = 19
            mAmount218 = mAmount213 - mAmount217
            .Text = VB6.Format(mAmount218, "0.00")

            .Col = 20
            mAmount219 = Val(.Text)

            .Col = 21
            mAmount220 = Val(.Text)

            .Col = 22
            mAmount221 = Val(.Text)

            .Col = 23
            mAmount222 = Val(.Text)

            .Col = 24
            mAmount223 = Val(.Text)

            .Col = 25
            '        mAmount224 = mAmount219 - (mAmount220 + mAmount221 + mAmount222 + mAmount223)						
            mAmount224 = Val(.Text)

            '        .Text = Format(mAmount224, "0.00")						

            .Col = 26
            mAmount225 = Val(.Text)

            .Col = 27
            mAmount226 = mAmount224 - mAmount225
            .Text = VB6.Format(mAmount226, "0.00")

            .Col = 28
            mAmount227 = Val(.Text)

            .Col = 29
            mAmount228 = Val(.Text)

            .Col = 30
            mAmount229 = Val(.Text)

            .Col = 31
            mAmount230 = mAmount227 + mAmount228 + mAmount229
            .Text = VB6.Format(mAmount230, "0.00")

            .Col = 32
            mAmount231 = mAmount226 - mAmount230
            .Text = VB6.Format(mAmount231, "0.00")

        End With
        Exit Sub
ErrPart1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetAmountFromDetail(ByRef mCompanyCode As Integer, ByRef mFYear As Integer, ByRef mEmpCode As String, ByRef mRow As Integer, ByRef pCalcField As String) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTempDetail As ADODB.Recordset
        Dim cntRow As Integer
        Dim mAmount As Double
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim RsTemp As ADODB.Recordset

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        GetAmountFromDetail = 0
        '    mRow = IIf(RsCompany!COMPANY_CODE = 2 And mRow > 10, mRow + 7, mRow)						

        SqlStr = " Select " & pCalcField & " AS TOTALAMOUNT " & vbCrLf & " FROM PAY_ITCOMP_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND SUBROWNO=" & mRow & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempDetail.EOF = False Then
            GetAmountFromDetail = CDbl(VB6.Format(IIf(IsDBNull(RsTempDetail.Fields("TOTALAMOUNT").Value), "", RsTempDetail.Fields("TOTALAMOUNT").Value), "0.00"))
        End If


        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
            '''Transfer Emp Data ...........						
            '        SqlStr = " SELECT * " & vbCrLf _						
            ''                & " FROM PAY_EMP_TRF_MST" & vbCrLf _						
            ''                & " WHERE " & vbCrLf _						
            ''                & " TO_COMPANY_CODE = " & mCompanyCode & "" & vbCrLf _						
            ''                & " AND TO_EMP_CODE = '" & mEmpCode & "'"						

            mToEmpCompany = mCompanyCode
            mToEmpCode = mEmpCode

SearchRow:
            SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

                SqlStr = " Select " & pCalcField & " AS TOTALAMOUNT " & vbCrLf & " FROM PAY_ITCOMP_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & mFromEmpCompany & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mFromEmpCode) & "'" & vbCrLf & " AND SUBROWNO=" & mRow & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDetail.EOF = False Then
                    GetAmountFromDetail = GetAmountFromDetail + CDbl(VB6.Format(IIf(IsDBNull(RsTempDetail.Fields("TOTALAMOUNT").Value), "", RsTempDetail.Fields("TOTALAMOUNT").Value), "0.00"))
                End If
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
                GoTo SearchRow
            End If
        End If

        Exit Function
ErrPart1:
        GetAmountFromDetail = 0
    End Function

    Private Function GetAmountFromDetail12BA(ByRef mCompanyCode As Integer, ByRef mFYear As Integer, ByRef mEmpCode As String, ByRef mRow As Integer) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTempDetail As ADODB.Recordset
        Dim cntRow As Integer
        Dim mAmount As Double

        GetAmountFromDetail12BA = 0

        SqlStr = " Select AMOUNT3 AS TOTALAMOUNT " & vbCrLf & " FROM PAY_ITFORM12BA_DET " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND SUBROW=" & mRow & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempDetail.EOF = False Then
            GetAmountFromDetail12BA = CDbl(VB6.Format(IIf(IsDBNull(RsTempDetail.Fields("TOTALAMOUNT").Value), "", RsTempDetail.Fields("TOTALAMOUNT").Value), "0.00"))
        End If
        Exit Function
ErrPart1:
        GetAmountFromDetail12BA = 0
    End Function

    Private Function GetEMPSNoFROM24(ByRef mCompanyCode As Integer, ByRef mEmpCode As String) As Integer
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim pEmpCode As String
        Dim pCompanyCode As String

        GetEMPSNoFROM24 = 0

        With SprdViewAnnex2
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 23
                pEmpCode = Trim(.Text)

                .Col = 24
                pCompanyCode = CStr(Val(.Text))

                If mEmpCode = pEmpCode And mCompanyCode = Val(pCompanyCode) Then
                    GetEMPSNoFROM24 = cntRow
                    Exit Function
                End If
            Next
        End With
        Exit Function
ErrPart1:
        GetEMPSNoFROM24 = False
    End Function
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Sub FormatSprdView()
        Call FormatSprdViewChallan()
        Call FormatSprdViewAnnex1()
        Call FormatSprdViewAnnex2()
        Call FormatSprdViewAnnex3()
    End Sub



    Private Sub FormatSprdViewAnnex2()
        Dim MainClass_Renamed As Object
        Dim i As Integer
        With SprdViewAnnex2
            .MaxCols = 29

            .set_RowHeight(0, RowHeight * 8)

            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 8)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 6)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 20)
            .ColsFrozen = 3

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            For i = 7 To 22
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            .Col = 23
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 7)

            .Col = 24
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            For i = 25 To 29
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            FillHeadingSprdViewAnnex2()
            MainClass.SetSpreadColor(SprdViewAnnex2, -1)
            MainClass.ProtectCell(SprdViewAnnex2, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdViewAnnex1()
        Dim MainClass_Renamed As Object
        Dim i As Integer
        With SprdViewAnnex1
            .MaxCols = 24

            .set_RowHeight(0, RowHeight * 5)

            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 8)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)
            .ColsFrozen = 3

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 25)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            For i = 6 To 11
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            .Col = 12
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 13
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)


            For i = 17 To 20
                .Col = i
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeMaxEditLen = 255
                .set_ColWidth(i, 10)
            Next

            For i = 21 To 24
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            FillHeadingSprdViewAnnex1()
            MainClass.SetSpreadColor(SprdViewAnnex1, -1)
            MainClass.ProtectCell(SprdViewAnnex1, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdViewAnnex3()
        Dim MainClass_Renamed As Object
        Dim i As Integer
        With SprdViewAnnex3
            .MaxCols = 18

            .set_RowHeight(0, RowHeight * 5)

            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 25)
            .ColsFrozen = 1

            .Col = 2
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 6)

            For i = 3 To 16
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            .Col = 17
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)

            .Col = 18
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)

            FillHeadingSprdViewAnnex3()
            MainClass.SetSpreadColor(SprdViewAnnex3, -1)
            MainClass.ProtectCell(SprdViewAnnex3, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub FormatSprdViewChallan()
        Dim MainClass_Renamed As Object
        Dim i As Integer
        With SprdViewChallan
            .MaxCols = 14

            .set_RowHeight(0, RowHeight * 5)

            .set_ColWidth(0, 0)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 6)

            For i = 2 To 7
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            For i = 8 To 14
                .Col = i
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeMaxEditLen = 255
                .set_ColWidth(i, 10)
            Next

            FillHeadingSprdViewChallan()
            MainClass.SetSpreadColor(SprdViewChallan, -1)
            MainClass.ProtectCell(SprdViewChallan, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub


    Private Sub FillHeadingSprdViewAnnex3()

        With SprdViewAnnex3
            .Row = 0

            .Col = 1
            .Text = "Name of Employee" & vbNewLine & "(353)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Employee's Serial No. in column 327 of Form No. 24" & vbNewLine & "(354)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Where accomodation is unfurnished" & vbNewLine & "(355)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Value as if accomodation is unfurnished" & vbNewLine & "(356)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Cost of furniture (including TV sets, radio sets, refrigerators, other house hold appliances and air-condioning plant or equipment)" & vbNewLine & "(357)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Perquisite value of furniture (10% of Columns 357)" & vbNewLine & "(358)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 7
            .Text = "Total of Columns 356 and 358" & vbNewLine & "(359)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "Rent, if any, paid by the employee" & vbNewLine & "(360)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "Value of perquisites (Columns 355 minus Columns 360 or Columns 359 minus Columns 360 as may be applicable)" & vbNewLine & "(361)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "Where any conveyance has been provided by the employer free or at a concessional rate or where the employee is allowed the use of one or more motor-cars owned or hired by the employer or where the employer incurs the running expenses of a motor var owned by employees estimated values of perquisites (give details)" & vbNewLine & "(362)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 11
            .Text = "Remunseration paid by the employer for domestic and / personal servies provided to the employee (give details)" & vbNewLine & "(363)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 12
            .Text = "Values of free or concessional passages on home leave and other travelling to the extent chargeable to tax (give details)" & vbNewLine & "(364)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 13
            .Text = "Estimated value of any other benefit or amenity provided by the employer free of cost or at concessional rate not included in the preceding Columns (Give Detail)" & vbNewLine & "(365)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 14
            .Text = "Employer's Contribution to recognised provident fund in excess of 12% of the employee's salary" & vbNewLine & "(366)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 15
            .Text = "Interest Credited to the assessee's account in recognised provident fund in excess of the rate fixed by crntral Goverment" & vbNewLine & "(367)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 16
            .Text = "Total of Columns 361 to 367 carried to column 333 of Form no. 24" & vbNewLine & "(368)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 17
            .Text = "Company Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 18
            .Text = "Emp Code"
            .Font = VB6.FontChangeBold(.Font, True)


        End With
    End Sub

    Private Sub FillHeadingSprdViewChallan()

        With SprdViewChallan
            .Row = 0

            .Col = 1
            .Text = "S. No." & vbNewLine & "(301)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "TDS Rs." & vbNewLine & "(302)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Surcharges Rs." & vbNewLine & "(303)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Education Cess Rs." & vbNewLine & "(304)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Interest Rs." & vbNewLine & "(305)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Others Rs." & vbNewLine & "(306)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 7
            .Text = "Total tax deposited Rs. (302 + 303 + 304 + 305 + 306)" & vbNewLine & "(307)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "Cheque / DD No. (is any)" & vbNewLine & "(308)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "BSR Code" & vbNewLine & "(309)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "Date on which tax deposited" & vbNewLine & "(310)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 11
            .Text = "Transder voucher / Challan Serial no." & vbNewLine & "(311)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 12
            .Text = "Whether TDS Deposited by Book Entry ? (Yes / No)" & vbNewLine & "(312)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 13
            .Text = "Company Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 14
            .Text = "Challan Ref No"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub

    Private Sub FillHeadingSprdViewAnnex2()

        With SprdViewAnnex2
            .Row = 0

            .Col = 1
            .Text = "S.No." & vbNewLine & "(327)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Permanent Account Number (PAN) of the employee " & vbNewLine & "(328)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "Name of the Employee" & vbNewLine & "(329)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Write ‘W’ for woman, ‘S’ for senior citizen and ‘G’ for others" & vbNewLine & "(330)"
            .Font = VB6.FontChangeBold(.Font, True)


            .Col = 5
            .Text = "Date From" & vbNewLine & "(331)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Date To" & vbNewLine & "(331)"
            .Font = VB6.FontChangeBold(.Font, True)

            '        .Col = 5						
            '        .Text = "Date To" & vbNewLine & "(330)"						
            '        .FontBold = True						

            .Col = 7
            .Text = "Total amount of salary (See note 4 appearing at the end of the main Form)" & vbNewLine & "(332)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "Total deduction under section 16(ii) and 16(iii) (specify each deduction separately)" & vbNewLine & "(333)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "Income chargeable under the head Salaries (Column 332 minus 333)" & vbNewLine & "(334)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "Income (including loss from house property) under any head other than the head Salaries offered for TDS [section 192(2B)]" & vbNewLine & "(335)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 11
            .Text = "Gross total income (Total of columns 334 and 335)" & vbNewLine & "(336)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 12
            .Text = "Aggregate amount of deductions under sections 80C, 80CCC and 80CCD (Total to be limited to amount specified in section 80CCE)" & vbNewLine & "(337)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 13
            .Text = "Amount deductible under any other provision(s) of Chapter VI-A" & vbNewLine & "(338)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 14
            .Text = "Total Amount deductible under Chapter VI-A (Total of columns 337 and 338)" & vbNewLine & "(339)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 15
            .Text = "Total taxable income (Columns 336 minus column 339)" & vbNewLine & "(340)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 16
            .Text = "Total tax – (i) income-tax on total income" & vbNewLine & "(341)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 17
            .Text = "(ii) surcharge" & vbNewLine & "(342)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 18
            .Text = "(iii)  education cess" & vbNewLine & "(343)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 19
            .Text = "Income tax  Relief under section 89, when salary etc., is paid in arrear or in advance" & vbNewLine & "(344)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 20
            .Text = "Net tax payable(columns 341+342+343-344)" & vbNewLine & "(345)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 21
            .Text = "Total amount of tax  deducted at source for the whole year  [aggregate of the amount in column 322 of Annexure I for all the four quarters in respect of each employee]" & vbNewLine & "(346)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 22
            .Text = "Shortfall in tax deduction(+)/Excess tax deduction(-) [column 345 minus column 346]" & vbNewLine & "(347)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 23
            .Text = "Employee Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 24
            .Text = "Company Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 25
            .Text = "80CCF"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 26
            .Text = "Taxable Amount on which tax is deducted by the current employer"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 27
            .Text = "Reported Taxable Amount on which tax is deducted by previous employer(S)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 28
            .Text = "Total Amount of tax deducted at source by the current employer for the whole year [aggregate of the amount in column 323 of Annexure I for all the four quarters in respect of each employee]"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 29
            .Text = "Reported amount of Tax deducted at source by previous employer(s)/deductor(s) (income in respect of which included in computing total taxable income in column 344)"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
    Private Sub FillHeadingSprdViewAnnex1()

        With SprdViewAnnex1
            .Row = 0

            .Col = 1
            .Text = "S.No." & vbNewLine & "(313)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Employee reference no. provided by employer" & vbNewLine & "(314)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "PAN of the employee" & vbNewLine & "(315)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Name of employee" & vbNewLine & "(316)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Date on which Amount paid / Credited to deductee" & vbNewLine & "(317)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Taxable amount on which tax deducted Rs." & vbNewLine & "(318)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 7
            .Text = "TDS" & vbNewLine & "(319)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "Surcharge" & vbNewLine & "(320)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "Educ. Cess" & vbNewLine & "(321)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "Total Tax deducted (319 + 320 + 321) Rs." & vbNewLine & "(322)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 11
            .Text = "Total Tax depostied Rs." & vbNewLine & "(323)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 12
            .Text = "Date of deduction" & vbNewLine & "(324)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 13
            .Text = "Date of Deposit" & vbNewLine & "(325)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 14
            .Text = "Reason for non-deduction / lowest deduction" & vbNewLine & "(326)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 15
            .Text = "Company Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 16
            .Text = "Challan Ref No"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 17
            .Text = "BSR CODE"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 18
            .Text = "Deposited Date"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 19
            .Text = "Challan Serial No"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 20
            .Text = "Section Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 21
            .Text = "Total TDS"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 22
            .Text = "Interest"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 23
            .Text = "Others"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 24
            .Text = "Total of the Above"
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Private Sub frmTDSeReturn24QNew_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub Clear1()
        Dim MainClass_Renamed As Object
        Dim mMonthType As String
        Dim mProvReceiptNo As String
        Dim mPhoneNo As String

        txtTDSAcNo.Text = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        txtPanNo.Text = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        txtTDSAcNo.Enabled = False
        txtPanNo.Enabled = False

        txtFYear.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
        txtAYear.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")) + 1, "0000")
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            txtReturnFiled.Text = "NO"
            txtProvReceiptNo.Text = ""
        Else
            txtReturnFiled.Text = "YES"
            mProvReceiptNo = GetProvReceiptNo()
            txtProvReceiptNo.Text = mProvReceiptNo
        End If



        optAddressChange(1).Checked = True
        optResAddChanged(1).Checked = True

        txtPersonName.Text = RsCompany.Fields("COMPANY_NAME").Value
        txtBranch.Text = ""
        txtDeductorType.Text = "Others"
        txtDeductorType.Enabled = False
        txtDesg.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
        txtFlat.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        txtBuilding.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        txtRoad.Text = ""
        txtArea.Text = ""
        txtTown.Text = IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        txtState.Text = IIf(IsDBNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
        txtPinCode.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)

        mPhoneNo = IIf(IsDBNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)

        If Len(mPhoneNo) > 4 Then
            txtPhone.Text = Mid(mPhoneNo, 5)
            txtSTDCode.Text = Trim(VB.Left(mPhoneNo, 4))
        Else
            txtPhone.Text = ""
            txtSTDCode.Text = ""
        End If


        txtEmail.Text = IIf(IsDBNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)

        txtPersonName_p.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        txtFlat_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        txtBuilding_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        txtRoad_p.Text = ""
        txtArea_p.Text = ""
        txtTown_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        txtState_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
        txtPinCode_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        txtPhone_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)
        txtEmail_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)

        txtMobileNo.Text = ""
        txtResponPANNo.Text = ""
        txtRundate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        MainClass.ClearGrid(SprdViewAnnex2, RowHeight)
        MainClass.ClearGrid(SprdViewChallan, RowHeight)
        MainClass.ClearGrid(SprdViewAnnex3, RowHeight)

    End Sub
    Private Function GetProvReceiptNo() As Object
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String


        SqlStr = "SELECT IV_QTR_NO from PAY_RTN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProvReceiptNo = IIf(IsDBNull(RsTemp.Fields("IV_QTR_NO").Value), "", RsTemp.Fields("IV_QTR_NO").Value)
        End If
        Exit Function
ErrPart:
        GetProvReceiptNo = ""
    End Function
    Private Function GetRentDetials(ByRef pCompanyCode As Integer, ByRef pEmpCode As String, ByRef mLLPAN1 As String, ByRef mLLName1 As String, ByRef mLLPAN2 As String, ByRef mLLName2 As String, ByRef mLLPAN3 As String, ByRef mLLName3 As String, ByRef mLLPAN4 As String, ByRef mLLName4 As String, ByRef mInstPaid As String, ByRef mLenderPAN1 As String, ByRef mLenderName1 As String, ByRef mLenderPAN2 As String, ByRef mLenderName2 As String, ByRef mLenderPAN3 As String, ByRef mLenderName3 As String, ByRef mLenderPAN4 As String, ByRef mLenderName4 As String, ByRef mISSF As String, ByRef mSFName As String, ByRef mFromDate As String, ByRef mToDate As String, ByRef mSF_RepaidAmount As Double, ByRef mSF_Avg_Amount As Double, ByRef mSF_RepaymentAmount As Double, ByRef mSF_GrossTotal As Double) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String

        GetRentDetials = False

        mLLPAN1 = ""
        mLLName1 = ""
        mLLPAN2 = ""
        mLLName2 = ""
        mLLPAN3 = ""
        mLLName3 = ""
        mLLPAN4 = ""
        mLLName4 = ""
        mInstPaid = "N"
        mLenderPAN1 = ""
        mLenderName1 = ""
        mLenderPAN2 = ""
        mLenderName2 = ""
        mLenderPAN3 = ""
        mLenderName3 = ""
        mLenderPAN4 = ""
        mLenderName4 = ""
        mISSF = "N"
        mSFName = ""
        mFromDate = ""
        mToDate = ""
        mSF_RepaidAmount = 0
        mSF_Avg_Amount = 0
        mSF_RepaymentAmount = 0
        mSF_GrossTotal = 0

        SqlStr = "SELECT LANDLOAD_PAN_1, LANDLOAD_NAME_1, LANDLOAD_PAN_2, LANDLOAD_NAME_2, " & vbCrLf _
            & " LANDLOAD_PAN_3, LANDLOAD_NAME_3, LANDLOAD_PAN_4, LANDLOAD_NAME_4, IS_INST_PAID," & vbCrLf _
            & " LENDER_PAN_1, LENDER_NAME_1, LENDER_PAN_2, LENDER_NAME_2, " & vbCrLf _
            & " LENDER_PAN_3, LENDER_NAME_3, LENDER_PAN_4, LENDER_NAME_4, " & vbCrLf _
            & " IS_SUPERANNUATION_FUND, SUPERANNUATION_FUND_NAME, S_FUND_FROMDATE, S_FUND_TODATE, " & vbCrLf _
            & " S_FUND_REPAID_AMOUNT, S_FUND_AVG_AMOUNT, S_FUND_REPAYMENT_AMOUNT,S_FUND_GROSS_AMOUNT " & vbCrLf _
            & " from PAY_RENT_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            mLLPAN1 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_PAN_1").Value), "", RsTemp.Fields("LANDLOAD_PAN_1").Value)
            mLLName1 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_NAME_1").Value), "", RsTemp.Fields("LANDLOAD_NAME_1").Value)
            mLLPAN2 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_PAN_2").Value), "", RsTemp.Fields("LANDLOAD_PAN_2").Value)
            mLLName2 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_NAME_2").Value), "", RsTemp.Fields("LANDLOAD_NAME_2").Value)
            mLLPAN3 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_PAN_3").Value), "", RsTemp.Fields("LANDLOAD_PAN_3").Value)
            mLLName3 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_NAME_3").Value), "", RsTemp.Fields("LANDLOAD_NAME_3").Value)
            mLLPAN4 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_PAN_4").Value), "", RsTemp.Fields("LANDLOAD_PAN_4").Value)
            mLLName4 = IIf(IsDBNull(RsTemp.Fields("LANDLOAD_NAME_4").Value), "", RsTemp.Fields("LANDLOAD_NAME_4").Value)
            mInstPaid = IIf(IsDBNull(RsTemp.Fields("IS_INST_PAID").Value), "N", RsTemp.Fields("IS_INST_PAID").Value)

            mLenderPAN1 = IIf(IsDBNull(RsTemp.Fields("LENDER_PAN_1").Value), "", RsTemp.Fields("LENDER_PAN_1").Value)
            mLenderName1 = IIf(IsDBNull(RsTemp.Fields("LENDER_NAME_1").Value), "", RsTemp.Fields("LENDER_NAME_1").Value)
            mLenderPAN2 = IIf(IsDBNull(RsTemp.Fields("LENDER_PAN_2").Value), "", RsTemp.Fields("LENDER_PAN_2").Value)
            mLenderName2 = IIf(IsDBNull(RsTemp.Fields("LENDER_NAME_2").Value), "", RsTemp.Fields("LENDER_NAME_2").Value)
            mLenderPAN3 = IIf(IsDBNull(RsTemp.Fields("LENDER_PAN_3").Value), "", RsTemp.Fields("LENDER_PAN_3").Value)
            mLenderName3 = IIf(IsDBNull(RsTemp.Fields("LENDER_NAME_3").Value), "", RsTemp.Fields("LENDER_NAME_3").Value)
            mLenderPAN4 = IIf(IsDBNull(RsTemp.Fields("LENDER_PAN_4").Value), "", RsTemp.Fields("LENDER_PAN_4").Value)
            mLenderName4 = IIf(IsDBNull(RsTemp.Fields("LENDER_NAME_4").Value), "", RsTemp.Fields("LENDER_NAME_4").Value)
            mISSF = IIf(IsDBNull(RsTemp.Fields("IS_SUPERANNUATION_FUND").Value), "N", RsTemp.Fields("IS_SUPERANNUATION_FUND").Value)
            mSFName = IIf(IsDBNull(RsTemp.Fields("SUPERANNUATION_FUND_NAME").Value), "", RsTemp.Fields("SUPERANNUATION_FUND_NAME").Value)
            mFromDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("S_FUND_FROMDATE").Value), "", RsTemp.Fields("S_FUND_FROMDATE").Value), "DD/MM/YYYY")
            mToDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("S_FUND_TODATE").Value), "", RsTemp.Fields("S_FUND_TODATE").Value), "DD/MM/YYYY")
            mSF_RepaidAmount = IIf(IsDBNull(RsTemp.Fields("S_FUND_REPAID_AMOUNT").Value), 0, RsTemp.Fields("S_FUND_REPAID_AMOUNT").Value)
            mSF_Avg_Amount = IIf(IsDBNull(RsTemp.Fields("S_FUND_AVG_AMOUNT").Value), 0, RsTemp.Fields("S_FUND_AVG_AMOUNT").Value)
            mSF_RepaymentAmount = IIf(IsDBNull(RsTemp.Fields("S_FUND_REPAYMENT_AMOUNT").Value), 0, RsTemp.Fields("S_FUND_REPAYMENT_AMOUNT").Value)
            mSF_GrossTotal = IIf(IsDBNull(RsTemp.Fields("S_FUND_GROSS_AMOUNT").Value), 0, RsTemp.Fields("S_FUND_GROSS_AMOUNT").Value)
        End If

        GetRentDetials = True
        Exit Function
ErrPart:
        GetRentDetials = False
    End Function
    Private Sub SetTextLength()
        On Error GoTo ERR1

        txtPersonName.MaxLength = 75
        txtFlat.MaxLength = 25
        txtBuilding.MaxLength = 25
        txtRoad.MaxLength = 25
        txtArea.MaxLength = 25
        txtTown.MaxLength = 25
        txtState.MaxLength = 25
        txtPinCode.MaxLength = 6



        txtPersonName.MaxLength = 75
        txtDeductorType.MaxLength = 3
        txtDesg.MaxLength = 20
        txtFlat.MaxLength = 25
        txtBuilding.MaxLength = 25
        txtRoad.MaxLength = 25
        txtArea.MaxLength = 25
        txtTown.MaxLength = 25
        txtState.MaxLength = 25
        txtPinCode.MaxLength = 6
        txtPhone.MaxLength = 25
        txtEmail.MaxLength = 25

        txtResponPANNo.MaxLength = 10
        txtPersonName_p.MaxLength = 75
        txtFlat_p.MaxLength = 25
        txtBuilding_p.MaxLength = 25
        txtRoad_p.MaxLength = 25
        txtArea_p.MaxLength = 25
        txtTown_p.MaxLength = 25
        txtState_p.MaxLength = 25
        txtPinCode_p.MaxLength = 6
        txtPhone_p.MaxLength = 25
        txtEmail_p.MaxLength = 25
        txtMobileNo.MaxLength = 10

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function ShowDosReport(ByRef pPrintMode As String, ByRef mQTR As String) As Boolean
        On Error GoTo ErrPart
        Dim pFileName As String
        Dim mLineCount As Integer
        'Dim QtrNO As String						
        Dim FilePath As String

        pFileName = mPubTDSPath & "\eRtn24Q.txt"

        FilePath = ""
        FilePath = Dir(mPubTDSPath, FileAttribute.Directory) ''   Dir(pFileName)						

        If FilePath = "" Then
            Call MkDir(mPubTDSPath)
        End If


        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        FileOpen(1, pFileName, OpenMode.Output)
        mLineCount = 1


        '						
        '    If Month(txtDateTo.Text) = 4 Or Month(txtDateTo.Text) = 5 Or Month(txtDateTo.Text) = 6 Then						
        '        QtrNO = "Q1"						
        '    ElseIf Month(txtDateTo.Text) = 7 Or Month(txtDateTo.Text) = 8 Or Month(txtDateTo.Text) = 9 Then						
        '        QtrNO = "Q2"						
        '    ElseIf Month(txtDateTo.Text) = 10 Or Month(txtDateTo.Text) = 11 Or Month(txtDateTo.Text) = 12 Then						
        '        QtrNO = "Q3"						
        '    ElseIf Month(txtDateTo.Text) = 1 Or Month(txtDateTo.Text) = 2 Or Month(txtDateTo.Text) = 3 Then						
        '        QtrNO = "Q4"						
        '    End If						

        Call PrintFH(mLineCount)
        Call PrintBH(mLineCount, mQTR)
        Call PrintCD(mLineCount)
        If mQTR = "Q4" Then
            If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                Call PrintSD(mLineCount)
            End If
        End If
        FileClose(1)


        '    If pPrintMode = "P" Then						
        '        Dim mFP As Boolean						
        '        mFP = Shell(App.path & "\PrintReport.bat", vbNormalFocus)						
        '        If mFP = False Then GoTo ErrPart						
        '    Else						
        Shell("ATTRIB +R -A " & pFileName)
        Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
        '    End If						

        ShowDosReport = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        ShowDosReport = False
        ''Resume						
        FileClose(1)
    End Function
    Private Function PrintSD16(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmpSD16Amount As Double) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String


        ''''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        ''''2						
        mString = "S16"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''3						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''4						
        mString = CStr(pcntRow)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''5						
        mString = CStr(1)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''6						
        mString = "16(ia)"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited


        '''''7						
        mString = VB6.Format(mEmpSD16Amount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '''''8						
        '    mMainString = mMainString & mDelimited						

        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), mMainString)

        PrintSD16 = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintSD16 = False
        '    Resume						
    End Function
    Private Function Print6A(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmp6AAmount As Double, ByRef mSectionRowNo As Integer, ByRef mTitle As String) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String


        ''''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        ''''2						
        mString = "C6A"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''3						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''4						
        mString = CStr(pcntRow)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''5						
        mString = CStr(mSectionRowNo) ''1						
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''6						
        mString = mTitle ''"80CCE"						
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited


        '''''7						
        mString = VB6.Format(mEmp6AAmount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '''''8						
        '    mMainString = mMainString & mDelimited						

        PrintLine(1, TAB(0), mMainString)
        mLineCount = mLineCount + 1

        Print6A = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        Print6A = False
        '    Resume						
    End Function

    Private Function PrintSD10(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmpSD10Amount As Double) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String


        ''''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        ''''2						
        mString = "S10"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''3						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''4						
        mString = CStr(pcntRow)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '    ''''5						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''6						
        mString = "10OTHERS"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''7						
        mString = VB6.Format(mEmpSD10Amount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '''''8						
        '    mMainString = mMainString & mDelimited						


        PrintLine(1, TAB(0), mMainString)
        mLineCount = mLineCount + 1
        PrintSD10 = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintSD10 = False
        '    Resume						
    End Function
    Private Function PrintSDVIA(ByRef pCompany_Code As Integer, ByRef pEmpCode As String, ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmp6AAmount As Double, ByRef mSno As Integer, ByRef m6AType As String) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String
        Dim mGrossAmount As Double
        Dim mQualifyingAmount As Double


        If m6AType = "80G" Then
            mGrossAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 51, "AMOUNT1")
            mQualifyingAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 51, "AMOUNT2")
        ElseIf m6AType = "80GG" Then
            mGrossAmount = 0
            mQualifyingAmount = 0
        Else
            mGrossAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 50, "AMOUNT1")
            mQualifyingAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 50, "AMOUNT2")

            mGrossAmount = mGrossAmount + GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 62, "AMOUNT1")
            mQualifyingAmount = mQualifyingAmount + GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 62, "AMOUNT2")
        End If

        ''''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        ''''2						
        mString = "C6A"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''3						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''4						
        mString = CStr(pcntRow)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '    ''''5						
        mString = CStr(mSno)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''6						
        mString = m6AType
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''7						
        mString = VB6.Format(mGrossAmount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '''''8						
        mString = VB6.Format(mQualifyingAmount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''9						
        mString = VB6.Format(mEmp6AAmount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''10						
        '    mMainString = mMainString & mDelimited						


        PrintLine(1, TAB(0), mMainString)

        mLineCount = mLineCount + 1

        PrintSDVIA = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintSDVIA = False
        '    Resume						
    End Function
    Private Function PrintSD88(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmp88Amount As Double, ByRef mSno As Integer, ByRef m88Type As String) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String


        ''''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        ''''2						
        mString = "S88"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''3						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''4						
        mString = CStr(pcntRow)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '    ''''5						
        mString = CStr(mSno)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''6						
        mString = m88Type
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''7						
        mString = VB6.Format(mEmp88Amount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited
        '						
        '''''8						
        mString = VB6.Format(mEmp88Amount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''9						
        mString = VB6.Format(mEmp88Amount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''10						
        '    mMainString = mMainString & mDelimited						


        PrintLine(1, TAB(0), mMainString)

        mLineCount = mLineCount + 1

        PrintSD88 = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintSD88 = False
        '    Resume						
    End Function

    Private Function PrintFH(ByRef mLineCount As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String


        ''''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        ''''2						
        mString = "FH"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''3						
        mString = "SL1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''4						
        mString = IIf(chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked, "R", "C")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''5						
        mString = VB6.Format(txtRundate.Text, "DDMMYYYY")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''''6						
        mString = CStr(mLineCount)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''7						
        mString = "D"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''8						
        mString = Trim(txtTDSAcNo.Text) & New String(" ", 10 - Len(Trim(txtTDSAcNo.Text)))
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''9						
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''10 ''NEW-14102009						
        mString = IIf(chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked, "HEILERP", "")
        '    mString = "HEILERP"						
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''11						
        mMainString = mMainString & mDelimited

        '''''12						
        mMainString = mMainString & mDelimited

        '''''13						
        mMainString = mMainString & mDelimited

        '''''14						
        mMainString = mMainString & mDelimited

        '''''15						
        mMainString = mMainString & mDelimited

        '''''16						
        mMainString = mMainString & mDelimited

        '''''17 ''15-05-2012						
        mMainString = mMainString & mDelimited


        PrintLine(1, TAB(0), mMainString)

        mLineCount = mLineCount + 1

        PrintFH = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintFH = False
        '    Resume						
    End Function

    Private Function PrintCD(ByRef mLineCount As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mRs As Double
        Dim mPaisa As Double


        Dim mTotDeductee As Double
        Dim mCompany_Code As Integer
        Dim mMkey As String
        Dim mDepositAmt As Double
        Dim mTDSAmount As Double
        Dim mSurchargeAmt As Double
        Dim mCESSAmt As Double
        Dim mNetAmount As Double
        Dim mIntAmt As Double
        Dim mOthAmt As Double
        Dim mCMkeyLineNo As Integer
        Dim mLastChallanRec As Integer

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked And (cboCorrectionType.SelectedIndex = 1 Or cboCorrectionType.SelectedIndex = 6) Then
            Exit Function
        End If

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            mLastChallanRec = GetLastChallanRecd()
        End If

        With SprdViewChallan
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = 13
                mCompany_Code = Val(.Text)

                .Col = 14
                mMkey = .Text


                If GetChallan_DedDetail(mDepositAmt, mTDSAmount, mSurchargeAmt, mCESSAmt, mNetAmount, mIntAmt, mOthAmt, mTotDeductee, mCompany_Code, mMkey) = False Then GoTo ErrPart


                .Row = cntRow

                '''1						
                .Col = 1
                mString = CStr(mLineCount)
                mMainString = mString
                mMainString = mMainString & mDelimited

                '''2						
                mString = "CD"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''3						
                mString = "1"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '            '''4						
                mString = VB6.Format(cntRow + mLastChallanRec, "0")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''5						
                If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mString = VB6.Format(mTotDeductee, "0")
                Else
                    If cboCorrectionType.SelectedIndex = 1 Then
                        mString = ""
                    ElseIf cboCorrectionType.SelectedIndex = 2 Then
                        mString = ""
                    ElseIf cboCorrectionType.SelectedIndex = 3 Then
                        mString = VB6.Format(mTotDeductee, "0")
                    ElseIf cboCorrectionType.SelectedIndex = 4 Then
                        mString = VB6.Format(mTotDeductee, "0")
                    ElseIf cboCorrectionType.SelectedIndex = 5 Then
                        mString = VB6.Format(mTotDeductee, "0")
                    End If
                End If
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''6						
                If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mString = "N"
                Else
                    mString = "N"
                End If
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''7						
                mMainString = mMainString & mDelimited

                '''8						
                mMainString = mMainString & mDelimited

                '''9						
                mMainString = mMainString & mDelimited

                '''10						
                mMainString = mMainString & mDelimited

                '''11						
                mMainString = mMainString & mDelimited

                '''12						
                .Col = 11
                mString = VB.Left(Trim(.Text), 5)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''13						
                mMainString = mMainString & mDelimited

                '''14						
                mMainString = mMainString & mDelimited

                '''15						
                mMainString = mMainString & mDelimited

                '''16						
                .Col = 9
                mString = VB.Left(Trim(.Text), 7)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''17						
                mMainString = mMainString & mDelimited

                '''18						
                .Col = 10
                mString = VB6.Format(.Text, "DDMMYYYY")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''19						
                mMainString = mMainString & mDelimited

                '''20						
                mMainString = mMainString & mDelimited

                '''21						
                If RsCompany.Fields("FYEAR").Value < 2013 Then
                    mString = "92B"
                    mMainString = mMainString & mString
                End If
                mMainString = mMainString & mDelimited

                '''22 to 27						
                For cntCol = 2 To 7
                    .Col = cntCol
                    mString = VB6.Format(System.Math.Round(Val(.Text), 0), "0.00")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited
                Next

                '''28						
                mMainString = mMainString & mDelimited


                '            '''29						
                mString = VB6.Format(mDepositAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '						
                '            '''30						
                mString = VB6.Format(mTDSAmount, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '						
                '            '''31						
                mString = VB6.Format(mSurchargeAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '						
                '            '''32						
                mString = VB6.Format(mCESSAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '						
                '            '''33						
                mNetAmount = mTDSAmount + mSurchargeAmt + mCESSAmt
                mString = VB6.Format(mNetAmount, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '						
                '            '''34						
                mString = VB6.Format(mIntAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '						
                '            '''35						
                mString = VB6.Format(mOthAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''36						
                .Col = 8
                If RsCompany.Fields("FYEAR").Value < 2013 Then
                    mString = VB.Left(Trim(.Text), 15)
                    mMainString = mMainString & mString
                End If
                mMainString = mMainString & mDelimited

                '''37						
                mString = "N"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''38						
                mMainString = mMainString & mDelimited

                '''39						
                mString = "0.00"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited
                '''40						
                mString = "200"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''41						
                '            mMainString = mMainString & mDelimited						

                PrintLine(1, TAB(0), mMainString)

                mCMkeyLineNo = cntRow
                mLineCount = mLineCount + 1

                ''Deductee Details						
                Call PrintDD(mLineCount, mCompany_Code, mMkey, mCMkeyLineNo + IIf(chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked, GetLastChallanRecd, 0))

            Next
        End With
        PrintCD = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintCD = False
        '    Resume						
    End Function

    Private Function PrintBH(ByRef mLineCount As Integer, ByRef mQTR As String) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim mRs As Double
        Dim mPaisa As Double
        Dim mCntRow As Integer
        Dim mTotChallanNo As Double
        Dim mTotDeductee As Double
        Dim mChallanAmount As Double
        Dim mDeducteeAmount As Double
        Dim mTotPerquisiteRecd As Double
        Dim mAmountPaid As Double
        Dim mTotalSDRec As Integer
        Dim mGrossTotalIncome As Double

        mCntRow = 1
        If GetChallanDetail(mTotChallanNo, mTotDeductee, mTotPerquisiteRecd, mChallanAmount, mDeducteeAmount, mAmountPaid) = False Then GoTo ErrPart

        '''1						
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        '''2						
        mString = "BH"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''3						
        mString = CStr(mCntRow)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''4						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB6.Format(mTotChallanNo, "0")
        Else
            If cboCorrectionType.SelectedIndex = 1 Then
                mString = ""
            ElseIf cboCorrectionType.SelectedIndex = 2 Then
                mString = VB6.Format(mTotChallanNo, "0")
            ElseIf cboCorrectionType.SelectedIndex = 3 Then
                mString = VB6.Format(mTotChallanNo, "0")
            ElseIf cboCorrectionType.SelectedIndex = 4 Then
                mString = VB6.Format(mTotChallanNo, "0")
            ElseIf cboCorrectionType.SelectedIndex = 5 Then
                mString = VB6.Format(mTotChallanNo, "0")
            ElseIf cboCorrectionType.SelectedIndex = 6 Then
                mString = ""
            End If
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''5						
        mString = "24Q"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''6						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = ""
        Else
            If cboCorrectionType.SelectedIndex = 1 Then
                mString = "C1"
            ElseIf cboCorrectionType.SelectedIndex = 2 Then
                mString = "C2"
            ElseIf cboCorrectionType.SelectedIndex = 3 Then
                mString = "C3"
            ElseIf cboCorrectionType.SelectedIndex = 4 Then
                mString = "C5"
            ElseIf cboCorrectionType.SelectedIndex = 5 Then
                mString = "C9"
            ElseIf cboCorrectionType.SelectedIndex = 6 Then
                mString = "Y"
            End If
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''7						
        mMainString = mMainString & mDelimited

        '''8						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = ""
        Else
            mString = Trim(txtProvReceiptNo.Text)
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''9						
        '    If chkRefilling.Value = vbUnchecked Then						
        '        mString = ""						
        '    Else						
        '        If Trim(txtCorrProvReceiptNo.Text) = "" Then						
        '            mString = Trim(txtProvReceiptNo.Text)						
        '        Else						
        '            mString = Trim(txtCorrProvReceiptNo.Text)						
        '        End If						
        '    End If						

        If mQTR = "Q1" Then
            mString = ""
        Else
            mString = Trim(txtTokenNo.Text) ''"26Q"						
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''10						
        mMainString = mMainString & mDelimited

        '''11						
        mMainString = mMainString & mDelimited

        '''12						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = ""
        Else
            mString = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''13						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked And cboCorrectionType.SelectedIndex = 4 Then
            mString = ""
        Else
            mString = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''14						
        mMainString = mMainString & mDelimited

        '''15						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''16						
        mString = VB6.Format(Year(RsCompany.Fields("END_DATE").Value), "0000") & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, RsCompany.Fields("END_DATE").Value), "YY")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 17						
        mString = VB6.Format(Year(RsCompany.Fields("START_DATE").Value), "0000") & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''18						

        If Month(CDate(txtDateTo.Text)) = 4 Or Month(CDate(txtDateTo.Text)) = 5 Or Month(CDate(txtDateTo.Text)) = 6 Then
            mString = "Q1"
        ElseIf Month(CDate(txtDateTo.Text)) = 7 Or Month(CDate(txtDateTo.Text)) = 8 Or Month(CDate(txtDateTo.Text)) = 9 Then
            mString = "Q2"
        ElseIf Month(CDate(txtDateTo.Text)) = 10 Or Month(CDate(txtDateTo.Text)) = 11 Or Month(CDate(txtDateTo.Text)) = 12 Then
            mString = "Q3"
        ElseIf Month(CDate(txtDateTo.Text)) = 1 Or Month(CDate(txtDateTo.Text)) = 2 Or Month(CDate(txtDateTo.Text)) = 3 Then
            mString = "Q4"
        End If

        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 19						
        mString = VB.Left(UCase(Trim(txtPersonName.Text)), 75)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''20						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtBranch.Text)), 75)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 21						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtFlat.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 22						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtBuilding.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 23						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtRoad.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 24						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtArea.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 25						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtTown.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 26						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = GetStateCode_TDS((txtState.Text))
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 27						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = CStr(Val(VB.Left(txtPinCode.Text, 6)))
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 28						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtEmail.Text)), 75)
            If CheckEMailValidation(mString) = False Then
                MsgInformation("Invalid Email ID.")
                txtEmail.Focus()
                PrintBH = False
                Exit Function
            End If
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 29						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSTDCode.Text) = "" Then
                mString = ""
            Else
                mString = Trim(txtSTDCode.Text)
            End If
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 30						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPhone.Text) = "" Then
                mString = ""
            Else
                mString = Trim(txtPhone.Text)
            End If
        Else
            mString = ""
        End If

        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 31						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = IIf(optAddressChange(0).Checked = True, "Y", "N")
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 32 ''New 14-1-2009						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = "K" ''For Company ''"O"						
        Else
            mString = "O"
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 33						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtPersonName_p.Text)), 75)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 34						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtDesg.Text)), 20)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 35						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtFlat_p.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 36						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtBuilding_p.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 37						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtRoad_p.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 38						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtArea_p.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 39						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtTown_p.Text)), 25)
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 40						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = GetStateCode_TDS((txtState_p.Text))
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 41						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = CStr(Val(VB.Left(txtPinCode_p.Text, 6)))
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 42						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB.Left(UCase(Trim(txtEmail_p.Text)), 75)
            If CheckEMailValidation(mString) = False Then
                MsgInformation("Invalid Email ID.")
                txtEmail_p.Focus()
                PrintBH = False
                Exit Function
            End If
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 43						
        mString = Trim(txtMobileNo.Text)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 44						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPhone_p.Text) = "" Then
                mString = ""
            Else
                mString = Trim(VB.Left(txtPhone_p.Text, 4))
            End If
        Else
            mString = ""
        End If

        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 45						


        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPhone_p.Text) = "" Then
                mString = ""
            Else
                mString = Mid(txtPhone_p.Text, 6, 7)
            End If
        Else
            mString = ""
        End If

        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 46						

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = IIf(optResAddChanged(0).Checked = True, "Y", "N")
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 47						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB6.Format(System.Math.Round(mChallanAmount, 0), "0.00")
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 48						
        mString = ""
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        Call GetSalaryDetail(mTotalSDRec, mGrossTotalIncome)
        ''' 49						
        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mString = VB6.Format(mTotalSDRec, "0")
        Else
            If cboCorrectionType.SelectedIndex = 1 Then
                mString = ""
            ElseIf cboCorrectionType.SelectedIndex = 2 Then
                mString = ""
            ElseIf cboCorrectionType.SelectedIndex = 3 Then
                mString = ""
            ElseIf cboCorrectionType.SelectedIndex = 4 Then
                mString = "0"
            ElseIf cboCorrectionType.SelectedIndex = 5 Then
                mString = ""
            ElseIf cboCorrectionType.SelectedIndex = 6 Then
                mString = ""
            End If
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '' 50						
        ''14/01/2015  ''NEW						
        If mQTR = "Q4" Then
            If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mString = VB6.Format(mGrossTotalIncome, "0.00")
            Else
                mString = ""
            End If
        Else
            mString = ""
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 51						
        mString = "N"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 52						
        If mQTR = "Q1" Then
            mString = "N"
        Else
            mString = "Y"
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 53						
        mMainString = mMainString & mDelimited

        ''' 54						
        mMainString = mMainString & mDelimited

        ''' 55						
        mMainString = mMainString & mDelimited

        ''' 56						
        mMainString = mMainString & mDelimited

        ''' 57						
        mMainString = mMainString & mDelimited

        ''' 58						
        mMainString = mMainString & mDelimited

        ''' 59						
        mString = Trim(txtResponPANNo.Text)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 60						
        mMainString = mMainString & mDelimited

        ''' 61						
        mMainString = mMainString & mDelimited

        If RsCompany.Fields("FYEAR").Value >= 2013 Then
            '''62						
            mMainString = mMainString & mDelimited

            '''63						
            mMainString = mMainString & mDelimited

            '''64						
            mMainString = mMainString & mDelimited

            '''65						
            mMainString = mMainString & mDelimited

            '''66						
            mMainString = mMainString & mDelimited

            '''67						
            mMainString = mMainString & mDelimited

            '''68						
            mMainString = mMainString & mDelimited
        End If

        ''' 69						
        mString = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        PrintLine(1, TAB(0), mMainString)

        mLineCount = mLineCount + 1

        PrintBH = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintBH = False
        '    Resume						
    End Function
    Private Function GetChallanDetail(ByRef pTotChallanNo As Double, ByRef pTotDeductee As Double, ByRef pTotPerquisiteRecd As Double, ByRef pChallanAmount As Double, ByRef pDeducteeAmount As Double, ByRef pAmountPaid As Double) As Boolean
        On Error GoTo ErrPart1
        Dim cntRow As Integer

        pTotChallanNo = 0
        pTotDeductee = 0
        pTotPerquisiteRecd = 0
        pChallanAmount = 0
        pDeducteeAmount = 0
        pAmountPaid = 0

        '    pTotChallanNo = SprdViewChallan.MaxRows						
        '    pTotDeductee = SprdViewAnnex2.MaxRows						
        '    pTotPerquisiteRecd = SprdViewAnnex3.MaxRows						

        With SprdViewChallan
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 7
                pChallanAmount = pChallanAmount + Val(.Text)

                If Val(.Text) > 0 Then
                    pTotChallanNo = pTotChallanNo + 1
                End If
            Next
        End With

        With SprdViewAnnex1
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = 6 '''02-05-2009						
                pAmountPaid = pAmountPaid + Val(.Text)
                '						
                .Col = 11
                pDeducteeAmount = pDeducteeAmount + Val(.Text)

                .Col = 2 '''02-05-2009						
                If Trim(.Text) <> "" Then
                    pTotDeductee = pTotDeductee + 1
                End If
            Next
        End With


        '    With SprdViewAnnex2						
        '        For cntRow = 1 To .MaxRows						
        '            .Row = cntRow						
        '            .Col = 7						
        '            If Trim(.Text) <> "" Then						
        '                pAmountPaid = pAmountPaid + Val(.Text)						
        '            End If						
        '						
        ''            pTotDeductee = pTotDeductee + 1						
        '        Next						
        '    End With						

        '    With SprdViewAnnex2						
        '        For cntRow = 1 To .MaxRows						
        '            .Row = cntRow						
        '            .Col = 28						
        '            If Trim(.Text) <> "" Then						
        '                pTotDeductee = pTotDeductee + 1						
        '            End If						
        '        Next						
        '    End With						

        With SprdViewAnnex3
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 18
                If Trim(.Text) <> "" Then
                    pTotPerquisiteRecd = pTotPerquisiteRecd + 1
                End If
            Next
        End With

        GetChallanDetail = True
        Exit Function
ErrPart1:
        GetChallanDetail = False
    End Function
    Private Function PrintDD(ByRef mLineCount As Integer, ByRef pCompany_Code As Integer, ByRef pMkey As String, ByRef pChallanLineNo As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim cntRow As Integer
        Dim mRs As Double
        Dim mPaisa As Double
        Dim i As Integer

        mString = ""
        With SprdViewAnnex1
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 16
                If Trim(pMkey) = Trim(.Text) Then
                    '''1						
                    mString = CStr(mLineCount)
                    mMainString = mString
                    mMainString = mMainString & mDelimited

                    '''2						
                    mString = "DD"
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''''3						
                    mString = "1"
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '            '''4						
                    mString = CStr(pChallanLineNo)
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '            '''5						
                    .Col = 1
                    mString = CStr(Val(.Text))
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    ''6						
                    mString = "O"
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    ''7						
                    .Col = 2
                    mString = VB.Left(UCase(Trim(.Text)), 9)
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''8						
                    mMainString = mMainString & mDelimited

                    '''9						
                    mMainString = mMainString & mDelimited

                    '''10						
                    .Col = 3
                    If Len(Trim(.Text)) = 10 Then
                        mString = UCase(Trim(.Text))
                    Else
                        mString = "PANINVALID"
                    End If
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''11						
                    mMainString = mMainString & mDelimited

                    '''12						
                    mMainString = mMainString & mDelimited

                    '''13						
                    .Col = 4
                    mString = VB.Left(UCase(Trim(.Text)), 75)
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''14 to 17						
                    For i = 7 To 10
                        .Col = i
                        mString = VB6.Format(.Text, "0.00")
                        mMainString = mMainString & mString
                        mMainString = mMainString & mDelimited
                    Next

                    '''18						
                    mMainString = mMainString & mDelimited

                    '''19						
                    .Col = 11
                    mString = VB6.Format(.Text, "0.00")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''20						
                    mMainString = mMainString & mDelimited

                    '''21						
                    mMainString = mMainString & mDelimited

                    '''22						
                    .Col = 6
                    mString = VB6.Format(.Text, "0.00")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''23						
                    .Col = 5
                    mString = VB6.Format(.Text, "DDMMYYYY")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''24						
                    .Col = 12
                    mString = VB6.Format(.Text, "DDMMYYYY")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''25						
                    .Col = 13
                    mString = VB6.Format(.Text, "DDMMYYYY")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''26 to 32						
                    For i = 26 To 32
                        mMainString = mMainString & mDelimited
                    Next

                    '''33						
                    If RsCompany.Fields("FYEAR").Value >= 2013 Then
                        mString = "92B"
                        mMainString = mMainString & mString
                    End If
                    mMainString = mMainString & mDelimited

                    '''34 to 38						
                    For i = 34 To 42
                        mMainString = mMainString & mDelimited
                    Next

                    PrintLine(1, TAB(0), mMainString)

                    mLineCount = mLineCount + 1
                End If
            Next
        End With
        PrintDD = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintDD = False
        '    Resume						
    End Function
    Private Function GetChallan_DedDetail(ByRef pDepositAmt As Double, ByRef pTDSAmount As Double, ByRef pSurchargeAmt As Double, ByRef pCessAmt As Double, ByRef pNetAmount As Double, ByRef pIntAmt As Double, ByRef pOthAmt As Double, ByRef pTotDeductee As Double, ByRef pCompany_Code As Integer, ByRef pMkey As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mTDSAccountCode As String

        pTotDeductee = 0

        pTotDeductee = 0
        pDepositAmt = 0
        pTDSAmount = 0
        pSurchargeAmt = 0
        pCessAmt = 0
        pNetAmount = 0
        pIntAmt = 0
        pOthAmt = 0

        SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(ID.AMOUNT) AS DEPOSIT_AMOUNT, " & vbCrLf & " SUM(ID.TDS_AMOUNT) AS TOTTDSAMOUNT, " & vbCrLf & " SUM(ID.SURCHARGE_AMT) AS TOTSURCHARGE, " & vbCrLf & " SUM(ID.CESS_AMT) AS TOTEDU_CESS, " & vbCrLf & " SUM(ID.AMOUNT) AS TOTNET_AMOUNT, " & vbCrLf & " 0 AS TOTINTEREST_AMOUNT, " & vbCrLf & " 0 AS TOTOTHER_AMOUNT "

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf _
            & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf _
            & " AND IH.AUTO_KEY_REFNO=" & Val(pMkey) & " " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='C'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE<>'C'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & pCompany_Code & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pTotDeductee = IIf(IsDBNull(RsTemp.Fields("TOTDEDUCTEE").Value), 0, RsTemp.Fields("TOTDEDUCTEE").Value)
            pTDSAmount = IIf(IsDBNull(RsTemp.Fields("TOTTDSAMOUNT").Value), 0, RsTemp.Fields("TOTTDSAMOUNT").Value)
            pSurchargeAmt = IIf(IsDBNull(RsTemp.Fields("TOTSURCHARGE").Value), 0, RsTemp.Fields("TOTSURCHARGE").Value)
            pCessAmt = IIf(IsDBNull(RsTemp.Fields("TOTEDU_CESS").Value), 0, RsTemp.Fields("TOTEDU_CESS").Value)
            pNetAmount = IIf(IsDBNull(RsTemp.Fields("TOTNET_AMOUNT").Value), 0, RsTemp.Fields("TOTNET_AMOUNT").Value)

            pDepositAmt = pTDSAmount + pSurchargeAmt + pCessAmt

            pIntAmt = IIf(IsDBNull(RsTemp.Fields("TOTINTEREST_AMOUNT").Value), 0, RsTemp.Fields("TOTINTEREST_AMOUNT").Value)
            pOthAmt = IIf(IsDBNull(RsTemp.Fields("TOTOTHER_AMOUNT").Value), 0, RsTemp.Fields("TOTOTHER_AMOUNT").Value)
        End If


        GetChallan_DedDetail = True
        Exit Function
ErrPart1:
        GetChallan_DedDetail = False
    End Function
    Private Function PrintSD(ByRef mLineCount As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim cntRow As Integer
        Dim mEmpCode As String

        Dim mEmpSD16Count As Integer
        Dim mEmp6ACount As Integer

        Dim mEmpSD16Amount As Double
        Dim mEmp6AAmount As Double

        Dim mEmp6A_IICount As Integer
        Dim mEmp6A_IIAmount As Double

        Dim mEmp6A_IIICount As Integer
        Dim mEmp6A_IIIAmount As Double

        'Dim mEmp88Count As Long						
        'Dim mSNO6A As Long						
        'Dim mSNO88 As Long						
        Dim mCompanyCode As Integer

        Dim i As Integer
        Dim CntSNo As Integer
        Dim mRentPaid As Double
        Dim mLLPANReq As String
        Dim mLLPAN1 As String
        Dim mLLName1 As String
        Dim mLLPAN2 As String
        Dim mLLName2 As String
        Dim mLLPAN3 As String
        Dim mLLName3 As String
        Dim mLLPAN4 As String
        Dim mLLName4 As String
        Dim mInstPaid As String
        Dim mLenderPAN1 As String
        Dim mLenderName1 As String
        Dim mLenderPAN2 As String
        Dim mLenderName2 As String
        Dim mLenderPAN3 As String
        Dim mLenderName3 As String
        Dim mLenderPAN4 As String
        Dim mLenderName4 As String
        Dim mISSF As String
        Dim mSFName As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mSF_RepaidAmount As Double
        Dim mSF_Avg_Amount As Double
        Dim mSF_RepaymentAmount As Double
        Dim mSF_GrossTotal As Double
        Dim mCount As Integer

        If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked And (cboCorrectionType.SelectedIndex = 1 Or cboCorrectionType.SelectedIndex = 6) Then
            Exit Function
        End If

        With SprdViewAnnex2

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = 8
                mEmpSD16Amount = Val(.Text)
                mEmpSD16Count = IIf(Val(.Text) <> 0, 1, 0)

                .Col = 12
                mEmp6AAmount = Val(.Text)
                mEmp6ACount = IIf(Val(.Text) <> 0, 1, 0)

                .Col = 13
                mEmp6A_IIAmount = Val(.Text)


                .Col = 25
                mEmp6A_IIIAmount = Val(.Text)
                mEmp6A_IIICount = IIf(Val(.Text) <> 0, 1, 0)

                mEmp6A_IICount = IIf(mEmp6A_IIAmount - mEmp6A_IIIAmount <> 0, 1, 0)


                '            If mEmp6ACount > 0 Then						
                '                Call Print6A(mLineCount, cntRow, mEmp6AAmount, CntSNo, "80CCE")						
                '                CntSNo = CntSNo + 1						
                '            End If						
                '						
                '            If mEmp6A_IIICount > 0 Then						
                '                Call Print6A(mLineCount, cntRow, mEmp6A_IIIAmount, CntSNo, "80CCF")						
                '                CntSNo = CntSNo + 1						
                '            End If						
                '						
                '            If mEmp6A_IIAmount - mEmp6A_IIIAmount > 0 Then						
                '                Call Print6A(mLineCount, cntRow, mEmp6A_IIAmount - mEmp6A_IIIAmount, CntSNo, "OTHERS")						
                '            End If						
                '						
                '						
                '        Next						
                '						
                '						
                '        For cntRow = 1 To .MaxRows						
                '''1						
                mString = CStr(mLineCount)
                mMainString = mString
                mMainString = mMainString & mDelimited

                '''2						
                mString = "SD"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''''3						
                mString = CStr(1)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''4						
                .Col = 1
                mString = CStr(Val(CStr(cntRow)))
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''5						
                mString = "A"
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited


                '''6						
                mMainString = mMainString & mDelimited

                '''7						
                .Col = 2
                If Len(Trim(.Text)) = 10 Then
                    mString = UCase(Trim(.Text))
                Else
                    mString = "PANINVALID"
                End If
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''8						
                mMainString = mMainString & mDelimited

                '''9						
                .Col = 3
                '            mEmpCode = Trim(.Text)						
                mString = VB.Left(UCase(Trim(.Text)), 75)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''10						
                .Col = 4
                mString = Trim(.Text)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''11						
                .Col = 5
                mString = VB6.Format(.Text, "DDMMYYYY")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''12						
                .Col = 6
                mString = VB6.Format(.Text, "DDMMYYYY")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''13						
                .Col = 7
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''14						
                mMainString = mMainString & mDelimited

                '''15						
                mString = VB6.Format(mEmpSD16Count, "0")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''16						
                .Col = 8
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''17						
                .Col = 9
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''18						
                .Col = 10
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''19						
                .Col = 11
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''20						
                mMainString = mMainString & mDelimited

                '''21						
                mString = VB6.Format(mEmp6ACount + mEmp6A_IICount + mEmp6A_IIICount, "0")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''22						
                .Col = 14
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''23						
                .Col = 15
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''24						
                .Col = 16
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''25						
                .Col = 17
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''26						
                .Col = 18
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''27						
                .Col = 19
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''28						
                .Col = 20
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''29						
                .Col = 21
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''30						
                .Col = 22
                mString = VB6.Format(Val(.Text), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                ''31						
                mMainString = mMainString & mDelimited

                ''32						
                mMainString = mMainString & mDelimited

                ''33						
                mMainString = mMainString & mDelimited

                '34  Taxable Amount on which tax is deducted by the current employer						
                If RsCompany.Fields("FYEAR").Value >= 2013 Then
                    .Col = 26
                    mString = VB6.Format(Val(.Text), "0.00")
                    mMainString = mMainString & mString & mDelimited
                End If

                '35  Reported Taxable Amount on which tax is deducted by previous employer(S)						
                If RsCompany.Fields("FYEAR").Value >= 2013 Then
                    .Col = 27
                    mString = VB6.Format(Val(.Text), "0.00")
                    mMainString = mMainString & mString & mDelimited
                End If

                '36  Total Amount of tax deducted at source by the current employer for the whole year [aggregate of the amount in column 323 of Annexure I for all the four quarters in respect of each employee]						
                If RsCompany.Fields("FYEAR").Value >= 2013 Then
                    .Col = 28
                    mString = VB6.Format(Val(.Text), "0.00")
                    mMainString = mMainString & mString & mDelimited
                End If

                '37  Reported amount of Tax deducted at source by previous employer(s)/deductor(s) (income in respect of which included in computing total taxable income in column 344)						
                If RsCompany.Fields("FYEAR").Value >= 2013 Then
                    .Col = 29
                    mString = VB6.Format(Val(.Text), "0.00")
                    mMainString = mMainString & mString & mDelimited
                End If

                '38  Whether tax deducted at Higher rate due to non furnishing of PAN by deductee						
                If RsCompany.Fields("FYEAR").Value >= 2013 Then
                    .Col = 2
                    If Len(Trim(.Text)) = 10 Then
                        mString = "N"
                    Else
                        mString = "Y"
                    End If
                    mMainString = mMainString & mString & mDelimited
                End If


                If RsCompany.Fields("FYEAR").Value >= 2016 Then

                    .Col = 24
                    mCompanyCode = Val(.Text)

                    .Col = 23
                    mEmpCode = Trim(.Text)
                    mRentPaid = GetAmountFromDetail(mCompanyCode, RsCompany.Fields("FYEAR").Value, mEmpCode, 28, "AMOUNT3")
                    mLLPANReq = IIf(mRentPaid > 100000, "Y", "N")

                    '39  ''Whether aggregate rent payment exceeds rupees one lakh during previous year						
                    mMainString = mMainString & mLLPANReq & mDelimited




                    If GetRentDetials(mCompanyCode, mEmpCode, mLLPAN1, mLLName1, mLLPAN2, mLLName2, mLLPAN3, mLLName3, mLLPAN4, mLLName4, mInstPaid, mLenderPAN1, mLenderName1, mLenderPAN2, mLenderName2, mLenderPAN3, mLenderName3, mLenderPAN4, mLenderName4, mISSF, mSFName, mFromDate, mToDate, mSF_RepaidAmount, mSF_Avg_Amount, mSF_RepaymentAmount, mSF_GrossTotal) = False Then GoTo ErrPart

                    '40 'Count of PAN of the landlord						
                    mCount = IIf(mLLPANReq = "N", 0, IIf(mLLPAN1 = "", 0, 1) + IIf(mLLPAN2 = "", 0, 1) + IIf(mLLPAN3 = "", 0, 1) + IIf(mLLPAN4 = "", 0, 1))
                    mMainString = mMainString & IIf(mCount = 0, "0", mCount) & mDelimited

                    '41 'PAN of landlord 1						
                    mString = IIf(mLLPANReq = "N", "", mLLPAN1)
                    mMainString = mMainString & mString & mDelimited

                    '42 'Name of landlord 1						
                    mString = IIf(mLLPANReq = "N", "", mLLName1)
                    mMainString = mMainString & mString & mDelimited

                    '43 'PAN of landlord 2						
                    mString = IIf(mLLPANReq = "N", "", mLLPAN2)
                    mMainString = mMainString & mString & mDelimited

                    '44 'Name of landlord 2						
                    mString = IIf(mLLPANReq = "N", "", mLLName2)
                    mMainString = mMainString & mString & mDelimited

                    '45 'PAN of landlord 3						
                    mString = IIf(mLLPANReq = "N", "", mLLPAN3)
                    mMainString = mMainString & mString & mDelimited

                    '46 'Name of landlord 3						
                    mString = IIf(mLLPANReq = "N", "", mLLName3)
                    mMainString = mMainString & mString & mDelimited

                    '47 'PAN of landlord 4						
                    mString = IIf(mLLPANReq = "N", "", mLLPAN4)
                    mMainString = mMainString & mString & mDelimited

                    '48 'Name of landlord 4						
                    mString = IIf(mLLPANReq = "N", "", mLLName4)
                    mMainString = mMainString & mString & mDelimited

                    '49 Whether Interest paid  to the lender under the head 'Income from house property'.						
                    mMainString = mMainString & mInstPaid & mDelimited

                    '50 Count of PAN of the lender						
                    mCount = IIf(mInstPaid = "N", 0, IIf(mLenderPAN1 = "", 0, 1) + IIf(mLenderPAN2 = "", 0, 1) + IIf(mLenderPAN3 = "", 0, 1) + IIf(mLenderPAN4 = "", 0, 1))
                    mMainString = mMainString & IIf(mCount = 0, "0", mCount) & mDelimited

                    '51 In case of deduction of interest under the head income from house property - PAN of lender 1						
                    mString = IIf(mInstPaid = "N", "", mLenderPAN1)
                    mMainString = mMainString & mString & mDelimited

                    '52 In case of deduction of interest under the head income from house property - Name of lender 1						
                    mString = IIf(mInstPaid = "N", "", mLenderName1)
                    mMainString = mMainString & mString & mDelimited

                    '53 In case of deduction of interest under the head income from house property - PAN of lender 2						
                    mString = IIf(mInstPaid = "N", "", mLenderPAN2)
                    mMainString = mMainString & mString & mDelimited

                    '54 In case of deduction of interest under the head income from house property - Name of lender 2						
                    mString = IIf(mInstPaid = "N", "", mLenderName2)
                    mMainString = mMainString & mString & mDelimited

                    '55 In case of deduction of interest under the head income from house property - PAN of lender 3						
                    mString = IIf(mInstPaid = "N", "", mLenderPAN3)
                    mMainString = mMainString & mString & mDelimited

                    '56 In case of deduction of interest under the head income from house property - Name of lender 3						
                    mString = IIf(mInstPaid = "N", "", mLenderName3)
                    mMainString = mMainString & mString & mDelimited

                    '57 In case of deduction of interest under the head income from house property - PAN of lender 4						
                    mString = IIf(mInstPaid = "N", "", mLenderPAN4)
                    mMainString = mMainString & mString & mDelimited

                    '58 In case of deduction of interest under the head income from house property - Name of lender 4						
                    mString = IIf(mInstPaid = "N", "", mLenderName4)
                    mMainString = mMainString & mString & mDelimited

                    '59 Whether contributions paid by the trustees of an approved superannuation fund						
                    mMainString = mMainString & mISSF & mDelimited

                    '60 Name of the superannuation fund						
                    mString = IIf(mISSF = "N", "", mSFName)
                    mMainString = mMainString & mString & mDelimited

                    '61 Date from which the employee has contributed to the superannuation fund						
                    mString = IIf(mISSF = "N", "", VB6.Format(mFromDate, "DDMMYYYY"))
                    mMainString = mMainString & mString & mDelimited

                    '62 Date to which the employee has contributed to the superannuation fund						
                    mString = IIf(mISSF = "N", "", VB6.Format(mToDate, "DDMMYYYY"))
                    mMainString = mMainString & mString & mDelimited

                    '63 The amount of contribution repaid on account of principal and interest from superannuation fund						
                    mString = IIf(mISSF = "N", "", mSF_RepaidAmount)
                    mMainString = mMainString & mString & mDelimited

                    '64 The average rate of deduction of tax during the preceding three years						
                    mString = IIf(mISSF = "N", "", mSF_Avg_Amount)
                    mMainString = mMainString & mString & mDelimited

                    '65 The amount of tax deducted on repayment of superannuation fund						
                    mString = IIf(mISSF = "N", "", mSF_RepaymentAmount)
                    mMainString = mMainString & mString & mDelimited

                    '66 Gross total income including contribution repaid on account of principal and interest from superannuation fund						
                    mString = IIf(mISSF = "N", "", mSF_GrossTotal)
                    mMainString = mMainString & mString & mDelimited


                End If
                PrintLine(1, TAB(0), mMainString)

                mLineCount = mLineCount + 1

                If mEmpSD16Count > 0 Then
                    Call PrintSD16(mLineCount, cntRow, mEmpSD16Amount)
                End If

                CntSNo = 1
                If mEmp6ACount > 0 Then
                    Call Print6A(mLineCount, cntRow, mEmp6AAmount, CntSNo, "80CCE")
                    CntSNo = CntSNo + 1
                End If

                If mEmp6A_IIICount > 0 Then
                    Call Print6A(mLineCount, cntRow, mEmp6A_IIIAmount, CntSNo, "80CCF")
                    CntSNo = CntSNo + 1
                End If

                If mEmp6A_IIAmount - mEmp6A_IIIAmount > 0 Then
                    Call Print6A(mLineCount, cntRow, mEmp6A_IIAmount - mEmp6A_IIIAmount, CntSNo, "OTHERS")
                End If

                '            If mEmpSD10Count > 0 Then						
                '                Call PrintSD10(mLineCount, cntRow, mEmpSD10Amount)						
                '            End If						
                '						
                '            mSNO6A = 1						
                '            mSNO88 = 1						
                '            If mEmp80GAmount > 0 Then						
                '                Call PrintSDVIA(mCompanyCode, mEmpCode, mLineCount, cntRow, mEmp80GAmount, mSNO6A, "80G")						
                '                mSNO6A = mSNO6A + 1						
                '            End If						
                '						
                '            If mEmp80GGAmount > 0 Then						
                '                Call PrintSDVIA(mCompanyCode, mEmpCode, mLineCount, cntRow, mEmp80GGAmount, mSNO6A, "80GG")						
                '                mSNO6A = mSNO6A + 1						
                '            End If						
                '						
                '            If mEmp80OthersAmount > 0 Then						
                '                Call PrintSDVIA(mCompanyCode, mEmpCode, mLineCount, cntRow, mEmp80OthersAmount, mSNO6A, "80OTHERS")						
                '            End If						
                '						
                '            If mEmp88Amount > 0 Then						
                '                Call PrintSD88(mLineCount, cntRow, mEmp88Amount, mSNO88, "88")						
                '                mSNO88 = mSNO88 + 1						
                '            End If						
                '						
                '            If mEmp88BAmount > 0 Then						
                '                Call PrintSD88(mLineCount, cntRow, mEmp88BAmount, mSNO88, "88B")						
                '                mSNO88 = mSNO88 + 1						
                '            End If						
                '						
                '            If mEmp88CAmount > 0 Then						
                '                Call PrintSD88(mLineCount, cntRow, mEmp88CAmount, mSNO88, "88C")						
                '                mSNO88 = mSNO88 + 1						
                '            End If						
                '						
                '            If mEmp88DAmount > 0 Then						
                '                Call PrintSD88(mLineCount, cntRow, mEmp88CAmount, mSNO88, "88D")						
                '            End If						


                mEmpSD16Amount = 0
                mEmpSD16Count = 0
                mEmp6AAmount = 0
                mEmp6ACount = 0

                '            mEmpSD16Amount = 0						
                '            mEmpSD10Amount = 0						
                '            mEmp80GAmount = 0						
                '            mEmp80GGAmount = 0						
                '            mEmp80OthersAmount = 0						
                '            mEmp88Amount = 0						
                '            mEmp88BAmount = 0						
                '            mEmp88CAmount = 0						
                '            mEmp88DAmount = 0						
                '						
                '            mEmpSD16Count = 0						
                '            mEmpSD10Count = 0						
                '            mEmp6ACount = 0						
                '            mEmp88Count = 0						

            Next
        End With
        PrintSD = True
        Exit Function
ErrPart:
        '    Resume						
        MsgBox(Err.Description)
        PrintSD = False
        '    Resume						
    End Function
    Private Function GetEmpDesg(ByRef pEmpCode As String, ByRef pCompanyCode As Integer) As String
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim RsDesig As ADODB.Recordset
        Dim SqlStr As String

        SqlStr = " SELECT EMP_DESG_CODE from PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesig, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDesig.EOF = False Then
            If RsDesig.Fields("EMP_DESG_CODE").Value <> "" Then
                If MainClass.ValidateWithMasterTable(RsDesig.Fields("EMP_DESG_CODE"), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                    GetEmpDesg = MasterNo
                End If
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function GetSalaryDetail(ByRef pTotalSDRec As Integer, ByRef mGrossTotalIncome As Double) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer

        mGrossTotalIncome = 0
        With SprdViewAnnex2
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 11
                mGrossTotalIncome = mGrossTotalIncome + Val(.Text)

                If Val(.Text) <> 0 Then
                    pTotalSDRec = pTotalSDRec + 1
                End If
            Next
        End With
        '    mGrossTotalIncome = Round(mGrossTotalIncome, 0)						
        GetSalaryDetail = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        GetSalaryDetail = False
        '    Resume						
    End Function

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateFrom.Text = "" Then GoTo EventExitSub
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateTo.Text = "" Then GoTo EventExitSub
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Function GetAmount26FromAnnex1(ByRef pEmpCode As String, ByRef pCompanyCode As Integer) As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer

        GetAmount26FromAnnex1 = 0

        With SprdViewAnnex1
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 15
                If Val(.Text) = pCompanyCode Then
                    .Col = 2
                    If Trim(.Text) = pEmpCode Then
                        .Col = 10
                        GetAmount26FromAnnex1 = GetAmount26FromAnnex1 + Val(.Text)
                        '                    Exit Function						
                    End If
                End If

            Next
        End With

        Exit Function
ErrPart:
        GetAmount26FromAnnex1 = 0
    End Function

    Private Function EMPTransfer(ByRef pEmpCode As String, ByRef pCompanyCode As Integer) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mLeaveDate As String

        SqlStr = " SELECT EMP_LEAVE_DATE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf _
            & " COMPANY_CODE = " & pCompanyCode & "" & vbCrLf & " AND EMP_CODE = '" & pEmpCode & "' " & vbCrLf _
            & " AND EMP_LEAVE_DATE IS NOT NULL"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = True Then
            EMPTransfer = False
            Exit Function
        Else
            mLeaveDate = IIf(IsDBNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value)
            If CDate(mLeaveDate) >= CDate(VB6.Format(txtDateTo.Text, "DD/MM/YYYY")) Then
                EMPTransfer = False
                Exit Function
            End If
        End If

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_EMP_TRF_MST" & vbCrLf & " WHERE " & vbCrLf & " FROM_COMPANY_CODE = " & pCompanyCode & "" & vbCrLf & " AND FROM_EMP_CODE = '" & pEmpCode & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            EMPTransfer = True
        Else
            EMPTransfer = False
        End If
        Exit Function
ErrPart:
        EMPTransfer = False
    End Function
    Private Function GetEMPDOJ(ByRef pEmpCode As String, ByRef pCompanyCode As Integer) As String
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mToCompanyCode As Integer
        Dim mToEmpCode As String

        '    SqlStr = " SELECT * " & vbCrLf _						
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _						
        ''            & " WHERE " & vbCrLf _						
        ''            & " TO_COMPANY_CODE = " & pCompanyCode & "" & vbCrLf _						
        ''            & " AND TO_EMP_CODE = '" & pEmpCode & "'"						


        mToCompanyCode = pCompanyCode
        mToEmpCode = pEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToCompanyCode)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mToCompanyCode = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mToEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)
            GoTo SearchRow
        Else
            '        mToCompanyCode = pCompanyCode						
            '        mToEmpCode = pEmpCode						
        End If



        SqlStr = " Select EMP_DOJ " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=" & mToCompanyCode & "" & vbCrLf & " AND EMP.EMP_CODE='" & mToEmpCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetEMPDOJ = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
        End If
        Exit Function
ErrPart:
        GetEMPDOJ = ""
    End Function

    Private Sub txtEmail_p_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmail_p.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckEMailValidation(txtEmail_p.Text) = False Then
            MsgInformation("Invalid Email ID.")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmail_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmail.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckEMailValidation(txtEmail.Text) = False Then
            MsgInformation("Invalid Email ID.")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMobileNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMobileNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
