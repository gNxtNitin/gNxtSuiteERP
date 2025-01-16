Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTDSeReturn26Q
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

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        PubDBCn.Errors.Clear()

        PrintStatus = False
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        '''''Select Record for print...							
        frmPrintTDS.ShowDialog()
        frmPrintTDS.OptFormChallan.Enabled = False
        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call InsertIntoPrintDummy()

        If frmPrintTDS.OptForm26.Checked = True Then
            If lblFormType.Text = "26Q" Then
                mTitle = "Form No. 26Q"
                mSubTitle = "(See section 193, 194, 194A, 194B, 194BB, 194C, 194D, 194EE, 194F, 194G, 194H, 194I, 194J, 194LA and rule 31A)"
            Else
                mTitle = "Form No. 27Q"
                mSubTitle = "(See section 193, 194, 194A, 194B, 194BB, 194C, 194D, 194EE, 194F, 194G, 194H, 194I, 194J, 194LA and rule 31A)"
            End If

            mReportFileName = "TDSeReturn26Q.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr, 1)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 1)
        ElseIf frmPrintTDS.OptForm27A.Checked = True Then

            mTitle = "Form No. 27A"
            mSubTitle = "[See rule 37B"

            mReportFileName = "TDSeReturn27AQ.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr, 3)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 3)
        ElseIf frmPrintTDS.OptAnnexure.Checked = True Then
            mTitle = "ANNEXURE - DEDUCTEE WISE BREAK-UP OF TDS"
            mSubTitle = ""

            mReportFileName = "TDSeReturn26QAnnx.rpt"
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr, 1)
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 2)
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
            If InsertGridDetail(SprdView26, 1, (SprdView26.MaxRows), (SprdView26.MaxCols)) = False Then GoTo ERR1
        ElseIf frmPrintTDS.OptAnnexure.Checked = True Then
            If InsertGridDetail(SprdViewAnnex, 1, (SprdViewAnnex.MaxRows), (SprdViewAnnex.MaxCols)) = False Then GoTo ERR1
        End If

        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume							
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function InsertGridDetail(ByRef mSprd As Object, ByRef mRowNo As Double, ByRef mMaxRow As Integer, ByRef mMaxCol As Integer) As Boolean
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


InsertPart:
                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3, " & vbCrLf & " Field4,Field5,Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11,Field12,Field13,Field14,Field15,Field16," & vbCrLf & " Field17,Field18,Field19,Field20,Field21,Field22,Field23," & vbCrLf & " Field24,Field25,Field26,Field27,Field28" & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol1) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol2) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol3) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol4) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol5) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol6) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol7) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol8) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol9) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol10) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol11) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol12) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol13) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol14) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol15) & "', "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol16) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol17) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol18) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol19) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol20) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol21) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol22) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol23) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol24) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol25) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol26) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol27) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol28) & "' )"

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



    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mReportNo As Integer)
        Dim mFormTitle As String
        Dim mTotAmountPaid As Double
        Dim mTotDeduct As Double
        Dim mTotPerson As Double
        Dim mTotChallanAmount As Double
        Dim mPartyName As String
        Dim mTotAnnexNo As Double

        Dim cntRow As Integer
        Dim mTANNo As String
        Dim mPANNo As String
        Dim mFormName As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mTANNo = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        mPANNo = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)

        MainClass.AssignCRptFormulas(Report1, "TANNo=""" & Trim(mTANNo) & """")
        MainClass.AssignCRptFormulas(Report1, "PANNo=""" & Trim(mPANNo) & """")


        If frmPrintTDS.OptForm26.Checked = True Then
            mFormTitle = "Quarterly statement of deduction of tax under sub-section (3) of section 200 of the Income-tax Act, 1961 in respect of payments other than salary for the quarter ended " & VB6.Format(txtDateTo.Text, "MMMM-YYYY")
        ElseIf frmPrintTDS.OptForm27A.Checked = True Then
            mFormTitle = "Form for furnishing information with the statement of deduction/ collection of tax at source(tick whichever is applicable) filed on computer media for the period (from " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " to " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ")"
        ElseIf frmPrintTDS.OptAnnexure.Checked = True Then
            mFormTitle = "Details of amount paid / Credited during the quarter ended " & VB6.Format(txtDateTo.Text, "MMMM-YYYY") & " and of tax deducted at source"
        End If

        MainClass.AssignCRptFormulas(Report1, "FYEAR=""" & txtFYear.Text & """")
        MainClass.AssignCRptFormulas(Report1, "AYEAR=""" & Trim(txtAYear.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "STATUS=""" & Trim(txtReturnFiled.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "ProReceiptNo=""" & Trim(txtProvReceiptNo.Text) & """")

        MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & Trim(txtDeductorType.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "BRANCHNAME=""" & Trim(txtBranch.Text) & """")


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

        MainClass.AssignCRptFormulas(Report1, "FormTitle=""" & mFormTitle & """")
        MainClass.AssignCRptFormulas(Report1, "AuthName=""" & Trim(txtPersonName_p.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "Designation=""" & Trim(txtDesg.Text) & """")

        If frmPrintTDS.OptForm27A.Checked = True Then
            With SprdViewAnnex
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 6
                    mTotAmountPaid = mTotAmountPaid + Val(.Text)

                    .Col = 11
                    mTotDeduct = mTotDeduct + Val(.Text)

                    .Col = 4
                    mTotPerson = mTotPerson + 1
                Next
            End With

            With SprdView26
                mPartyName = ""
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    mTotAnnexNo = 0

                    .Col = 8
                    mTotChallanAmount = mTotChallanAmount + Val(.Text)
                Next
            End With

            MainClass.AssignCRptFormulas(Report1, "TotAmountPaid=""" & VB6.Format(mTotAmountPaid, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "TotDeduct=""" & VB6.Format(mTotDeduct, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "TotPerson=""" & mTotPerson & """")
            MainClass.AssignCRptFormulas(Report1, "TotChallanAmount=""" & VB6.Format(mTotChallanAmount, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "TotAnnexNo=""" & mTotAnnexNo & """")
            mFormName = UCase(lblFormType.Text)
            MainClass.AssignCRptFormulas(Report1, "FormName=""" & mFormName & """")

        End If


        ' Report1.CopiesToPrinter = PrintCopies							
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName

        Report1.MarginLeft = 0
        Report1.MarginRight = 0

        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String, ByRef mReportNo As Integer) As String
        Dim mSection As String

        mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"


        If mReportNo = 1 Then
            '        mSqlStr = mSqlStr & " AND FIELD2='CD'"							
        ElseIf mReportNo = 2 Then
            mSqlStr = mSqlStr & " AND FIELD2='DD'"

            If frmPrintTDS.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = "'193'"
            End If
            If frmPrintTDS.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194'"
            End If
            If frmPrintTDS.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194A'"
            End If
            If frmPrintTDS.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194B'"
            End If
            If frmPrintTDS.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194BB'"
            End If
            If frmPrintTDS.chkPrintOption(5).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194C'"
            End If
            If frmPrintTDS.chkPrintOption(6).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194D'"
            End If
            If frmPrintTDS.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194EE'"
            End If
            If frmPrintTDS.chkPrintOption(8).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194F'"
            End If
            If frmPrintTDS.chkPrintOption(9).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194G'"
            End If
            If frmPrintTDS.chkPrintOption(10).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194H'"
            End If
            If frmPrintTDS.chkPrintOption(11).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194I'"
            End If
            If frmPrintTDS.chkPrintOption(12).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194J'"
            End If
            If frmPrintTDS.chkPrintOption(13).CheckState = System.Windows.Forms.CheckState.Checked Then
                mSection = IIf(mSection = "", "", mSection & ", ") & "'194K'"
            End If

            mSection = "(" & mSection & ")"
            mSqlStr = mSqlStr & " AND FIELD5 IN " & mSection & ""

        ElseIf mReportNo = 3 Then
            mSqlStr = mSqlStr & " AND FIELD2='FH'"
        End If

        '    If mReportNo = 2 Then							
        '        mSqlStr = mSqlStr & " ORDER BY  SUBROW"     ''FIELD5, FIELD8,							
        '    Else							
        mSqlStr = mSqlStr & " ORDER BY SUBROW"
        '    End If							
        FetchRecordForReport = mSqlStr

    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTDS(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearch.Click
        SearchAccounts()
    End Sub




    Private Sub cmdValidate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdValidate.Click
        Dim mFP As Boolean
        mFP = Shell(mLocalPath & "\TDS_FVU.bat", AppWinStyle.NormalFocus)
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
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
        On Error GoTo ErrPart

        If Trim(TxtAccount.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = False Then
            MsgInformation("Invalid TDS Head.")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Dim CntLst As Integer
        Dim mSectionName As String
        Dim mSectionNameStr As String


        If MainClass.ChkIsdateF(txtDateFrom) = False Then FieldsVerification = False : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then FieldsVerification = False : txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then FieldsVerification = False : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then FieldsVerification = False : txtDateTo.Focus()


        '    If Trim(TxtAccount) = "" Then							
        '        MsgInformation "Please Enter Valid TDS Account Name."							
        '        TxtAccount.SetFocus							
        '        FieldsVerification = False							
        '        Exit Function							
        '    End If							
        '							
        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = False Then							
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


        For CntLst = 0 To lstSection.Items.Count - 1
            If lstSection.GetItemChecked(CntLst) = True Then
                mSectionName = VB6.GetItemString(lstSection, CntLst)
                mSectionNameStr = IIf(mSectionNameStr = "", "'" & mSectionName & "'", mSectionNameStr & "," & "'" & mSectionName & "'")
            End If
        Next

        If mSectionNameStr = "" Then
            MsgInformation("Please Enter Valid TDS Section Name.")
            TxtAccount.Focus()
            FieldsVerification = False
            Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmTDSeReturn26Q_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "TDS e-Return (Form " & lblFormType.Text & ")"
        Call PrintStatus(False)
        FormatSprdView()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmTDSeReturn26Q_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim RS As ADODB.Recordset
        Dim SqlStr As String
        Dim CntLst As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection							
        ''PvtDBCn.Open StrConn							
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(6285)
        Me.Width = VB6.TwipsToPixelsX(10155)
        SSTab1.SelectedIndex = 0

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        txtTDSAcNo.Enabled = False
        txtPanNo.Enabled = False


        lstSection.Items.Clear()
        SqlStr = "SELECT DISTINCT NAME FROM TDS_SECTION_MST " & vbCrLf & " ORDER BY NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstSection.Items.Add(RS.Fields("Name").Value)
                lstSection.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstSection.SelectedIndex = 0


        FormatSprdView()
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSectionCode As Integer


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If ShowDetail26() = False Then GoTo ErrPart
        If ShowDetailAnnex() = False Then GoTo ErrPart


        FormatSprdView()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)

    End Sub
    Private Function ShowDetailAnnex() As Boolean
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mAddress As String
        Dim mNewAddress As String
        Dim mDeducteeCode As String
        Dim mAddress1 As String
        Dim mAddress2 As String
        Dim mAddress3 As String
        Dim mAddress4 As String
        Dim mTDSAccountCode As String
        Dim mNetAmount As Double
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

        Dim mTDSAmount As Double

        Dim CntLst As Integer
        Dim mSectionName As String
        Dim mSectionNameStr As String

        For CntLst = 0 To lstSection.Items.Count - 1
            If lstSection.GetItemChecked(CntLst) = True Then
                mSectionName = VB6.GetItemString(lstSection, CntLst)
                mSectionNameStr = IIf(mSectionNameStr = "", "'" & mSectionName & "'", mSectionNameStr & "," & "'" & mSectionName & "'")
            End If
        Next

        If mSectionNameStr = "" Then
            MsgInformation("Please Enter Valid TDS Section Name.")
            ShowDetailAnnex = False
            Exit Function
        End If





        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then							
        '        mTDSAccountCode = MasterNo							
        '    Else							
        '        MsgInformation "Please Enter Valid TDS Account Name."							
        '        ShowDetailAnnex = False							
        '        Exit Function							
        '    End If							

        SqlStr = " Select SECTIONMST.NAME, CMST.PAN_NO PANNO,CMST.SUPP_CUST_NAME,'', ISLOWERDED," & vbCrLf & " CASE WHEN TRN.VDATE<'01-OCT-2009' THEN (TRN.TDSAMOUNT*100*.100/113.3) ELSE 0 END  AS SURAMT, " & vbCrLf & " CASE WHEN TRN.VDATE<'01-OCT-2009' THEN (TRN.TDSAMOUNT*100*.033/113.3) ELSE 0 END AS CESS,  " & vbCrLf & " (TRN.TDSAMOUNT - CASE WHEN TRN.VDATE<'01-OCT-2009' THEN  ((TRN.TDSAMOUNT*100*.033/113.3)+ (TRN.TDSAMOUNT*100*.100/113.3)) ELSE 0 END) AS TDSAMT," & vbCrLf & " TRN.TDSAMOUNT, TRN.AMOUNTPAID,TRN.VDATE,TRN.TDSRATE, " & vbCrLf & " TRN.CHALLANNO ,TRN.CHALLANDATE, " & vbCrLf & " TRN.CERTIFICATENO, TRN.EXEPTIONCNO,CMST.CTYPE, " & vbCrLf & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, " & vbCrLf & " CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, " & vbCrLf & " CMST.PAN_NO,PRINTDATE,BANKCODE,TRN.COMPANY_CODE,CHALLANMKEY "

        SqlStr = SqlStr & vbCrLf & " FROM TDS_TRN TRN, TDS_SECTION_MST SECTIONMST,FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE "

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND"
        End If

        SqlStr = SqlStr & vbCrLf & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRN.PARTYCODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND TRN.CANCELLED='N' "

        ''AND ACCOUNTCODE='" & mTDSAccountCode & "'"							



        If mSectionNameStr <> "" Then
            mSectionNameStr = "(" & mSectionNameStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND SECTIONMST.NAME IN " & mSectionNameStr & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.CHALLANMKEY IN ( " & GetChallanQry(True) & ") "

        SqlStr = SqlStr & vbCrLf & " ORDER BY  CHALLANMKEY, SECTIONMST.NAME, TRN.COMPANY_CODE,  CMST.SUPP_CUST_NAME, TRN.VDATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        mChallanWiseSNo = 1

        With SprdViewAnnex
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    .Row = cntRow
                    .Col = 1
                    If mPrevChallanMkey = IIf(IsDBNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value) Then
                        mChallanWiseSNo = mChallanWiseSNo + 1
                    Else
                        mChallanWiseSNo = 1
                        mChallanMkey = IIf(IsDBNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value)
                        mChallanSNo = GetChallanSNO(mChallanMkey, mSectionCode, mBSRCode, mDepositDate, mChallanNo, mTotalTDS, mTotalInerest, mOtherAmt, mTotalTaxDeposit)
                    End If

                    .Text = CStr(mChallanWiseSNo) '''cntRow							

                    .Col = 2
                    mDeducteeCode = IIf(IsDBNull(RsTemp.Fields("CType").Value), "N", RsTemp.Fields("CType").Value)
                    .Text = IIf(mDeducteeCode = "C", "01", "02")

                    .Col = 3

                    If Len(RsTemp.Fields("PAN_NO").Value) = 10 Then
                        .Text = IIf(IsDBNull(RsTemp.Fields("PAN_NO").Value), "", RsTemp.Fields("PAN_NO").Value)
                    ElseIf Trim(RsTemp.Fields("PAN_NO").Value) = "" Or IsDBNull(RsTemp.Fields("PAN_NO").Value) Then
                        .Text = "PANNOTAVBL"
                    Else
                        .Text = "PANINVALID"
                    End If


                    .Col = 4
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    .Col = 5
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY") '' Format(IIf(IsNull(RsTemp!CHALLANDATE), "", RsTemp!CHALLANDATE), "DD/MM/YYYY")							

                    .Col = 6
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("AmountPaid").Value), "", RsTemp.Fields("AmountPaid").Value), "0.00") '' Format(IIf(IsNull(RsTemp!TDSAMOUNT), "", RsTemp!TDSAMOUNT), "0.00")							


                    .Col = 7
                    .Text = ""

                    .Col = 8
                    mTDSAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDSAMOUNT").Value), "", RsTemp.Fields("TDSAMOUNT").Value), "0.00"))
                    mTDSAmount = mTDSAmount - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURAMT").Value), "", RsTemp.Fields("SURAMT").Value), "0.00"))
                    mTDSAmount = mTDSAmount - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("CESS").Value), "", RsTemp.Fields("CESS").Value), "0.00"))

                    '                .Text = Format(IIf(IsNull(RsTemp!TDSAMT), "", RsTemp!TDSAMT), "0.00")           '''Format(IIf(IsNull(RsTemp!TDSAMOUNT), "", RsTemp!TDSAMOUNT), "0.00")							
                    .Text = VB6.Format(mTDSAmount, "0.00")

                    .Col = 9
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURAMT").Value), "", RsTemp.Fields("SURAMT").Value), "0.00") ''Format(IIf(IsNull(RsTemp!SURCHARGE), "", RsTemp!SURCHARGE), "0.00")							

                    .Col = 10
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CESS").Value), "", RsTemp.Fields("CESS").Value), "0.00") ''Format(IIf(IsNull(RsTemp!EDU_CESS), "", RsTemp!EDU_CESS), "0.00")							

                    mNetAmount = CDbl(VB6.Format(mTDSAmount, "0.00")) ''Format(IIf(IsNull(RsTemp!TDSAMT), "", RsTemp!TDSAMT), "0.00")            '''IIf(IsNull(RsTemp!TDSAMOUNT), 0, RsTemp!TDSAMOUNT)							
                    mNetAmount = mNetAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURAMT").Value), "", RsTemp.Fields("SURAMT").Value), "0.00")) ''IIf(IsNull(RsTemp!SURCHARGE), 0, RsTemp!SURCHARGE)							
                    mNetAmount = mNetAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("CESS").Value), "", RsTemp.Fields("CESS").Value), "0.00")) ''IIf(IsNull(RsTemp!EDU_CESS), 0, RsTemp!EDU_CESS)							

                    .Col = 11
                    .Text = VB6.Format(mNetAmount, "0.00")

                    .Col = 12
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDSAMOUNT").Value), "", RsTemp.Fields("TDSAMOUNT").Value), "0.00") '' Format(mNetAmount, "0.00")      '''							

                    .Col = 13
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                    .Col = 14
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDSRATE").Value), "", RsTemp.Fields("TDSRATE").Value), "0.0000")

                    .Col = 15
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ISLOWERDED").Value), "N", RsTemp.Fields("ISLOWERDED").Value))

                    .Col = 16
                    .Text = CStr(IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))

                    .Col = 17
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value)
                    mPrevChallanMkey = IIf(IsDBNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value)

                    .Col = 18
                    .Text = Trim(mBSRCode)

                    .Col = 19
                    .Text = Trim(mDepositDate)

                    .Col = 20
                    .Text = Trim(mChallanNo)

                    .Col = 21
                    .Text = Trim(mSectionCode)

                    .Col = 22
                    .Text = VB6.Format(mTotalTDS, "0.00")

                    .Col = 23
                    .Text = VB6.Format(mTotalInerest, "0.00")

                    .Col = 24
                    .Text = VB6.Format(mOtherAmt, "0.00")

                    .Col = 25
                    .Text = VB6.Format(mTotalTaxDeposit, "0.00")

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop
            End If
        End With
        ShowDetailAnnex = True
        Exit Function
ErrPart1:
        ShowDetailAnnex = False
        '    Resume							
    End Function

    Private Function GetChallanSNO(ByRef pChallanMKey As String, ByRef pSectionCode As String, ByRef pBSRCode As String, ByRef pDepositDate As String, ByRef pChallanNo As String, ByRef pTotalTDS As Double, ByRef pTotalInerest As Double, ByRef pOtherAmt As Double, ByRef pTotalTaxDeposit As Double) As Integer
        On Error GoTo ErrPart1
        Dim cntRow As Integer

        GetChallanSNO = 0
        With SprdView26
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 15
                If Trim(.Text) = Trim(pChallanMKey) Then
                    GetChallanSNO = cntRow
                    .Col = 2
                    pSectionCode = Trim(.Text)

                    .Col = 3
                    pTotalTDS = Val(.Text)

                    .Col = 4
                    pTotalTDS = pTotalTDS + Val(.Text)

                    .Col = 5
                    pTotalTDS = pTotalTDS + Val(.Text)

                    .Col = 6
                    pTotalInerest = Val(.Text)

                    .Col = 7
                    pOtherAmt = Val(.Text)

                    .Col = 8
                    pTotalTaxDeposit = Val(.Text)

                    .Col = 10
                    pBSRCode = Trim(.Text)

                    .Col = 11
                    pDepositDate = Trim(.Text)

                    .Col = 12
                    pChallanNo = Trim(.Text)

                    Exit For
                End If
            Next
        End With



        Exit Function
ErrPart1:

    End Function



    Private Function ShowDetail26() As Boolean
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mTDSAccountCode As String
        Dim CntLst As Integer
        Dim mSectionName As String
        Dim mSectionNameStr As String

        For CntLst = 0 To lstSection.Items.Count - 1
            If lstSection.GetItemChecked(CntLst) = True Then
                mSectionName = VB6.GetItemString(lstSection, CntLst)
                mSectionNameStr = IIf(mSectionNameStr = "", "'" & mSectionName & "'", mSectionNameStr & "," & "'" & mSectionName & "'")
            End If
        Next

        If mSectionNameStr = "" Then
            MsgInformation("Please Enter Valid TDS Section Name.")
            ShowDetail26 = False
            Exit Function
        End If

        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then							
        '        mTDSAccountCode = MasterNo							
        '    Else							
        '        MsgInformation "Please Enter Valid TDS Account Name."							
        '        ShowDetail26 = False							
        '        Exit Function							
        '    End If							


        '    SqlStr = "Select SECTIONMST.NAME, SECTIONMST.SECTIONCODE," & vbCrLf _							
        ''        & " TRN.COMPANY_CODE, CHALLANNO , CHALLANDATE," & vbCrLf _							
        ''        & " TDS_AMOUNT AS TDSAMOUNT," & vbCrLf _							
        ''        & " SURCHARGE, EDU_CESS, INTEREST_AMOUNT, OTHER_AMOUNT," & vbCrLf _							
        ''        & " AMOUNT AS NET_AMOUNT," & vbCrLf _							
        ''        & " BANKCODE, CHQ_NO, CHQ_DATE,MKEY" & vbCrLf _							
        ''							
        '    SqlStr = SqlStr & vbCrLf _							
        ''        & " FROM TDS_CHALLAN TRN, TDS_SECTION_MST SECTIONMST " & vbCrLf _							
        ''        & " WHERE "							
        '							
        '    SqlStr = SqlStr & vbCrLf _							
        ''        & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf _							
        ''        & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf _							
        ''        & " AND ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf _							
        ''        & " AND TRN.FROMDATE>='" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf _							
        ''        & " AND TRN.TODATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"							
        '							
        '    If chkConsolidated.Value = vbUnchecked Then							
        '       SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""							
        '    End If							
        '							
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""							
        '							
        '    SqlStr = SqlStr & vbCrLf _							
        ''        & " ORDER BY TRN.COMPANY_CODE, MKEY, SECTIONMST.NAME, MKEY, CHALLANDATE, CHALLANNO "							

        SqlStr = GetChallanQry(False)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1

        With SprdView26
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    .Row = cntRow
                    .Col = 1
                    .Text = CStr(cntRow)

                    .Col = 2
                    .Text = IIf(IsDBNull(RsTemp.Fields("SECTIONCODE").Value), "", RsTemp.Fields("SECTIONCODE").Value) ''IIf(IsNull(RsTemp!Name), "", RsTemp!Name)							

                    .Col = 3
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDSAMOUNT").Value), "", RsTemp.Fields("TDSAMOUNT").Value), "0.00")

                    .Col = 4
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURCHARGE").Value), "", RsTemp.Fields("SURCHARGE").Value), "0.00")

                    .Col = 5
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EDU_CESS").Value), "", RsTemp.Fields("EDU_CESS").Value), "0.00")

                    .Col = 6
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INTEREST_AMOUNT").Value), "", RsTemp.Fields("INTEREST_AMOUNT").Value), "0.00")

                    .Col = 7
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OTHER_AMOUNT").Value), "", RsTemp.Fields("OTHER_AMOUNT").Value), "0.00")

                    .Col = 8
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("NET_AMOUNT").Value), "", RsTemp.Fields("NET_AMOUNT").Value), "0.00")

                    .Col = 9
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHQ_NO").Value), "", RsTemp.Fields("CHQ_NO").Value)

                    .Col = 10
                    .Text = IIf(IsDBNull(RsTemp.Fields("BANKCODE").Value), "", RsTemp.Fields("BANKCODE").Value)

                    .Col = 11
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")

                    .Col = 12
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHALLANNO").Value), "", RsTemp.Fields("CHALLANNO").Value)

                    .Col = 13
                    .Text = ""

                    .Col = 14
                    .Text = CStr(IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))

                    .Col = 15
                    .Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop
            End If
        End With
        ShowDetail26 = True
        Exit Function
ErrPart1:
        ShowDetail26 = False
    End Function

    Private Function GetChallanQry(ByRef IsInquery As Boolean) As String
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mTDSAccountCode As String
        Dim mOriginalRRRNo As String
        Dim mSqlStr As String

        Dim CntLst As Integer
        Dim mSectionName As String
        Dim mSectionNameStr As String

        For CntLst = 0 To lstSection.Items.Count - 1
            If lstSection.GetItemChecked(CntLst) = True Then
                mSectionName = VB6.GetItemString(lstSection, CntLst)
                mSectionNameStr = IIf(mSectionNameStr = "", "'" & mSectionName & "'", mSectionNameStr & "," & "'" & mSectionName & "'")
            End If
        Next


        '							
        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then							
        '        mTDSAccountCode = MasterNo							
        '    End If							

        If IsInquery = False Then
            SqlStr = " Select SECTIONMST.NAME, SECTIONMST.SECTIONCODE," & vbCrLf _
                & " TRN.COMPANY_CODE, CHALLANNO , CHALLANDATE," & vbCrLf _
                & " TDS_AMOUNT AS TDSAMOUNT," & vbCrLf _
                & " SURCHARGE, EDU_CESS, INTEREST_AMOUNT, OTHER_AMOUNT," & vbCrLf & " AMOUNT AS NET_AMOUNT," & vbCrLf & " BANKCODE, CHQ_NO, CHQ_DATE,MKEY"
        Else
            SqlStr = "Select MKEY"
        End If


        SqlStr = SqlStr & vbCrLf & " FROM TDS_CHALLAN TRN, TDS_SECTION_MST SECTIONMST " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE"

        ''& " AND ACCOUNTCODE='" & mTDSAccountCode & "'"							

        If mSectionNameStr <> "" Then
            mSectionNameStr = "(" & mSectionNameStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND SECTIONMST.NAME IN " & mSectionNameStr & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.FROMDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.TODATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        If IsInquery = False Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.COMPANY_CODE, MKEY, SECTIONMST.NAME, MKEY, CHALLANDATE, CHALLANNO "
        End If

        GetChallanQry = SqlStr
        Exit Function
ErrPart1:
        GetChallanQry = ""
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
        Call FormatSprdView26()
        Call FormatSprdViewAnnex()
    End Sub



    Private Sub FormatSprdView26()
        Dim i As Integer
        With SprdView26
            .MaxCols = 15

            .set_RowHeight(0, RowHeight * 3)

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

            For i = 3 To 8
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

            .Col = 9
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 7)

            .Col = 10
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 7)

            .Col = 11
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 8)

            .Col = 12
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(.Col, 12)

            .Col = 13
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)

            .Col = 14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)


            FillHeadingSprdView26()
            MainClass.SetSpreadColor(SprdView26, -1)
            MainClass.ProtectCell(SprdView26, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdViewAnnex()
        Dim i As Integer
        With SprdViewAnnex
            .MaxCols = 25

            .set_RowHeight(0, RowHeight * 3.5)

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
            .set_ColWidth(.Col, 12)

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

            For i = 6 To 6
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

            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)


            For i = 8 To 12
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

            .Col = 13
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .set_ColWidth(.Col, 10)

            For i = 14 To 14
                .Col = i
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(i, 10)
            Next

            For i = 15 To 21
                .Col = i
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeMaxEditLen = 255
                .set_ColWidth(i, 10)
            Next

            For i = 22 To 25
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

            FillHeadingSprdViewAnnex()
            MainClass.SetSpreadColor(SprdViewAnnex, -1)
            MainClass.ProtectCell(SprdViewAnnex, 1, .MaxRows, 1, .MaxCols)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub




    Private Sub FillHeadingSprdViewAnnex()

        With SprdViewAnnex
            .Row = 0

            .Col = 1
            .Text = "S.No." & vbNewLine & "(414)" & vbNewLine & "(1)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Deductee Code (01-Company 02-Other Than company)" & vbNewLine & "(415)" & vbNewLine & "(2)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "PAN of the Deductee" & vbNewLine & "(416)" & vbNewLine & "(3)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Name of the Deductee" & vbNewLine & "(417)" & vbNewLine & "(4)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Date of payment / Credit" & vbNewLine & "(418)" & vbNewLine & "(5)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Amount paid / Credited Rs." & vbNewLine & "(419)" & vbNewLine & "(6)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 7
            .Text = "Paid by book entry or otherwise" & vbNewLine & "(420)" & vbNewLine & "(7)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "TDS" & vbNewLine & "(421)" & vbNewLine & "(8)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "Surcharge" & vbNewLine & "(422)" & vbNewLine & "(9)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "Education Cess" & vbNewLine & "(423)" & vbNewLine & "(10)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 11
            .Text = "Total tax deducted(421+422+423) Rs." & vbNewLine & "(424)" & vbNewLine & "(11)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 12
            .Text = "Total Tax deposited" & vbNewLine & "(425)" & vbNewLine & "(12)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 13
            .Text = "Date of deduction" & vbNewLine & "(426)" & vbNewLine & "(13)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 14
            .Text = "Rate at which dedicted" & vbNewLine & "(427)" & vbNewLine & "(14)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 15
            .Text = "Reason for non-deduction/lower deduction" & vbNewLine & "(428)" & vbNewLine & "(15)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 16
            .Text = "Company Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 17
            .Text = "Challan Mkey"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 18
            .Text = "BSR CODE"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 19
            .Text = "Deposited Date"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 20
            .Text = "Challan Serial No"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 21
            .Text = "Section Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 22
            .Text = "Total TDS"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 23
            .Text = "Interest"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 24
            .Text = "Others"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 25
            .Text = "Total of the Above"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub


    Private Sub FillHeadingSprdView26()

        With SprdView26
            .Row = 0

            .Col = 1
            .Text = "S.No." & vbNewLine & "(401)" & vbNewLine & "(1)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 2
            .Text = "Section Code" & vbNewLine & "(402)" & vbNewLine & "(2)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 3
            .Text = "TDS Rs." & vbNewLine & "(403)" & vbNewLine & "(3)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 4
            .Text = "Surcharge Rs." & vbNewLine & "(404)" & vbNewLine & "(4)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 5
            .Text = "Education Cess Rs." & vbNewLine & "(405)" & vbNewLine & "(5)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 6
            .Text = "Interest Rs." & vbNewLine & "(406)" & vbNewLine & "(6)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 7
            .Text = "Others Rs." & vbNewLine & "(407)" & vbNewLine & "(7)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 8
            .Text = "Total Tax deposited Rs." & vbNewLine & "(408)" & vbNewLine & "(8)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 9
            .Text = "Cheque/DD No." & vbNewLine & "(409)" & vbNewLine & "(9)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 10
            .Text = "BSR Code" & vbNewLine & "(410)" & vbNewLine & "(10)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 11
            .Text = "Date on which tax deposted" & vbNewLine & "(411)" & vbNewLine & "(11)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 12
            .Text = "Transfer Voucher/Challan Serial Number" & vbNewLine & "(412)" & vbNewLine & "(12)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 13
            .Text = "Whether TDS deposited by book entry (Y/N/)" & vbNewLine & "(413)" & vbNewLine & "(13)"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 14
            .Text = "Company Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = 15
            .Text = "MKEY"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
    Private Sub frmTDSeReturn26Q_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub Clear1()
        Dim mMonthType As String

        txtTDSAcNo.Text = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        txtPanNo.Text = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        txtTDSAcNo.Enabled = False
        txtPanNo.Enabled = False

        txtFYear.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
        txtAYear.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")) + 1, "0000")
        txtReturnFiled.Text = "NO"
        txtProvReceiptNo.Text = ""

        txtPersonName.Text = RsCompany.Fields("COMPANY_NAME").Value
        txtDeductorType.Text = "Others"
        txtBranch.Text = ""

        txtFlat.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        txtBuilding.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        txtRoad.Text = ""
        txtArea.Text = ""
        txtTown.Text = IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        txtState.Text = IIf(IsDBNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
        txtPinCode.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        txtPhone.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)
        txtEmail.Text = IIf(IsDBNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)

        txtPersonName_p.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        txtDesg.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
        txtFlat_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        txtBuilding_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        txtRoad_p.Text = ""
        txtArea_p.Text = ""
        txtTown_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        txtState_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
        txtPinCode_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        txtPhone_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)
        txtEmail_p.Text = IIf(IsDBNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)
        txtResponPANNo.Text = ""
        txtMobileNo.Text = ""
        txtUACK.Text = ""
        MainClass.ClearGrid(SprdView26, RowHeight)
        MainClass.ClearGrid(SprdViewAnnex, RowHeight)

    End Sub
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
        txtBranch.MaxLength = 40
        txtFlat.MaxLength = 25
        txtBuilding.MaxLength = 25
        txtRoad.MaxLength = 25
        txtArea.MaxLength = 25
        txtTown.MaxLength = 25
        txtState.MaxLength = 25
        txtPinCode.MaxLength = 6
        txtPhone.MaxLength = 10
        txtEmail.MaxLength = 25


        txtPersonName_p.MaxLength = 75
        txtResponPANNo.MaxLength = 10
        txtDesg.MaxLength = 20
        txtFlat_p.MaxLength = 25
        txtBuilding_p.MaxLength = 25
        txtRoad_p.MaxLength = 25
        txtArea_p.MaxLength = 25
        txtTown_p.MaxLength = 25
        txtState_p.MaxLength = 25
        txtPinCode_p.MaxLength = 6
        txtPhone_p.MaxLength = 10
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

        Dim FilePath As String




        If lblFormType.Text = "26Q" Then
            pFileName = mPubTDSPath & "\eRtn26Q.txt"
        Else
            pFileName = mPubTDSPath & "\eRtn27Q.txt"
        End If

        FilePath = ""
        FilePath = Dir(mPubTDSPath, FileAttribute.Directory) ''pFileName							

        If FilePath = "" Then
            Call MkDir(mPubTDSPath)
        End If

        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        FileOpen(1, pFileName, OpenMode.Output)
        mLineCount = 1

        Call PrintFH(mLineCount)
        Call PrintBH(mLineCount, mQTR)
        Call PrintCD(mLineCount)
        '    Call PrintDD(mLineCount)							

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

    Private Function PrintFH(ByRef mLineCount As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mString As String
        Dim mMainString As String



        '''1							
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        '''2							
        mString = "FH"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''3							
        mString = "NS1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''4							
        mString = "R"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''5							
        mString = VB6.Format(PubCurrDate, "DDMMYYYY")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''6							
        mString = CStr(mLineCount)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''7							
        mString = "D"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''8							
        mString = Trim(txtTDSAcNo.Text)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''9							
        mString = CStr(mLineCount)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''''10 ''NEW-14102009							
        mString = "HEILERP"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''11							
        mMainString = mMainString & mDelimited

        '''12							
        mMainString = mMainString & mDelimited

        '''13							
        mMainString = mMainString & mDelimited

        '''14							
        mMainString = mMainString & mDelimited

        '''15							
        mMainString = mMainString & mDelimited

        '''16							
        mMainString = mMainString & mDelimited

        '''17 ''15-05-2012							
        mMainString = mMainString & mDelimited

        '    '''18 ''15-05-2012							
        '    mMainString = mMainString & mDelimited							

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

        With SprdView26
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = 14
                mCompany_Code = Val(.Text)

                .Col = 15
                mMkey = .Text


                If GetChallan_DedDetail(mDepositAmt, mTDSAmount, mSurchargeAmt, mCESSAmt, mNetAmount, mIntAmt, mOthAmt, mTotDeductee, mCompany_Code, mMkey) = False Then GoTo ErrPart

                .Row = cntRow

                '''1							
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

                '''4							
                .Col = 1
                mString = CStr(Val(.Text))
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''5							
                mString = VB6.Format(mTotDeductee, "0")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''6							
                mString = "N"
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
                .Col = 12
                mString = Trim(.Text)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''13							
                mMainString = mMainString & mDelimited

                '''14							
                mMainString = mMainString & mDelimited

                '''15							
                mMainString = mMainString & mDelimited

                '''16							
                .Col = 10
                mString = Trim(.Text)
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''17							
                mMainString = mMainString & mDelimited

                '''18							
                .Col = 11
                mString = VB6.Format(.Text, "DDMMYYYY")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''19							
                mMainString = mMainString & mDelimited

                '''20							
                mMainString = mMainString & mDelimited

                '''21							
                If RsCompany.Fields("FYEAR").Value < 2013 Then
                    .Col = 2
                    mString = Trim(.Text) '''Mid(Trim(.Text), 2)							
                    mMainString = mMainString & mString
                End If
                mMainString = mMainString & mDelimited

                '''22 to 27							
                For cntCol = 3 To 8
                    .Col = cntCol
                    mString = VB6.Format(System.Math.Round(Val(.Text), 0), "0.00")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited
                Next

                '''28							
                mMainString = mMainString & mDelimited

                '''29							
                mString = VB6.Format(mDepositAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''30							
                mString = VB6.Format(mTDSAmount, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''31							
                mString = VB6.Format(mSurchargeAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited


                '''32							
                mString = VB6.Format(mCESSAmt, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''33							
                mNetAmount = mTDSAmount + mSurchargeAmt + mCESSAmt
                mString = VB6.Format(mNetAmount, "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''34							
                mString = VB6.Format(System.Math.Round(mIntAmt, 0), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''35							
                mString = VB6.Format(System.Math.Round(mOthAmt, 0), "0.00")
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''36							
                If RsCompany.Fields("FYEAR").Value < 2013 Then
                    .Col = 9
                    mString = CStr(Val(.Text))
                    mMainString = mMainString & mString
                End If
                mMainString = mMainString & mDelimited

                '''37							
                mString = "N" ''N							
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''38							
                mMainString = mMainString & mDelimited

                '''39							
                mString = "0.00" ''N							
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''40							
                mString = "200" ''N							
                mMainString = mMainString & mString
                mMainString = mMainString & mDelimited

                '''41							
                '            mMainString = mMainString & mDelimited							

                PrintLine(1, TAB(0), mMainString)
                mCMkeyLineNo = cntRow
                mLineCount = mLineCount + 1

                ''Deductee Details							
                Call PrintDD(mLineCount, mCompany_Code, mMkey, mCMkeyLineNo)
            Next
        End With
        PrintCD = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintCD = False
        '    Resume							
    End Function

    Private Function PrintBH(ByRef mLineCount As Integer, ByRef pQTR As String) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim cntRow As Integer
        Dim mRs As Double
        Dim mPaisa As Double

        Dim mTotChallanNo As Double
        Dim mTotDeductee As Double
        Dim mChallanAmount As Double
        Dim mDeducteeAmount As Double


        If GetChallanDetail(mTotChallanNo, mTotDeductee, mChallanAmount, mDeducteeAmount) = False Then GoTo ErrPart

        '''1							
        mString = CStr(mLineCount)
        mMainString = mString
        mMainString = mMainString & mDelimited

        '''2							
        mString = "BH"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''3							
        mString = "1"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''4							
        mString = CStr(mTotChallanNo)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited


        ''' 5							
        mString = UCase(lblFormType.Text) ''"26Q"							
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 6							
        mMainString = mMainString & mDelimited

        ''' 7							
        mMainString = mMainString & mDelimited

        ''' 8							
        mMainString = mMainString & mDelimited

        ''' 9							
        If pQTR = "Q1" Then
            mString = ""
        Else
            mString = Trim(txtTokenNo.Text) ''"26Q"							
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''' 10							
        mMainString = mMainString & mDelimited

        ''' 11							
        mMainString = mMainString & mDelimited

        ''' 12							
        mMainString = mMainString & mDelimited

        '''13							
        mString = Trim(txtTDSAcNo.Text)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        ''14							
        mMainString = mMainString & mDelimited

        '''15							
        mString = Trim(txtPanNo.Text)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''16							
        mString = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")) + 1, "00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''17							
        mString = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
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

        '''19							
        mString = VB.Left(Trim(txtPersonName.Text), 75)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''20							
        mString = VB.Left(Trim(txtBranch.Text), 75)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''21							
        mString = VB.Left(Trim(txtFlat.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''22							
        mString = VB.Left(Trim(txtBuilding.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''23							
        mString = VB.Left(Trim(txtRoad.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''24							
        mString = VB.Left(Trim(txtArea.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''25							
        mString = VB.Left(Trim(txtTown.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''26							
        mString = GetStateCode_TDS((txtState.Text))
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''27							
        mString = VB6.Format(Val(txtPinCode.Text), "000000")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''28							

        mString = VB.Left(Trim(txtEmail.Text), 75)
        If CheckEMailValidation(mString) = False Then
            MsgInformation("Invalid Email ID.")
            txtEmail.Focus()
            PrintBH = False
            Exit Function
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''29							
        If Trim(txtPhone.Text) = "" Then
            mString = ""
        Else
            mString = Trim(VB.Left(txtPhone.Text, 4))
        End If

        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''30							
        If Trim(txtPhone.Text) = "" Then
            mString = ""
        Else
            mString = Mid(txtPhone.Text, 6, 7)
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''31							
        mString = "N"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''32							
        mString = "K" ''NEW 14-10-2009 "O"							
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''33							
        mString = VB.Left(Trim(txtPersonName_p.Text), 75)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''34							
        mString = VB.Left(Trim(txtDesg.Text), 20)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''35							
        mString = VB.Left(Trim(txtFlat_p.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''36							
        mString = VB.Left(Trim(txtBuilding_p.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''37							
        mString = VB.Left(Trim(txtRoad_p.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''38							
        mString = VB.Left(Trim(txtArea_p.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''39							
        mString = VB.Left(Trim(txtTown_p.Text), 25)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''40							
        mString = GetStateCode_TDS((txtState_p.Text))
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''41							
        mString = VB6.Format(Val(txtPinCode_p.Text), "000000")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''42							
        mString = VB.Left(Trim(txtEmail_p.Text), 75)
        If CheckEMailValidation(mString) = False Then
            MsgInformation("Invalid Email ID.")
            txtEmail_p.Focus()
            PrintBH = False
            Exit Function
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''43							
        mString = Trim(txtMobileNo.Text)
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''44							
        If Trim(txtPhone_p.Text) = "" Then
            mString = ""
        Else
            mString = Trim(VB.Left(txtPhone_p.Text, 4))
        End If

        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''45							
        If Trim(txtPhone_p.Text) = "" Then
            mString = ""
        Else
            mString = Mid(txtPhone_p.Text, 6, 7)
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''46							
        mString = IIf(chkPersonAddChange.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''47							
        mString = VB6.Format(mChallanAmount, "0.00")
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''48							
        mMainString = mMainString & mDelimited

        '''49							
        mMainString = mMainString & mDelimited

        '''50							
        mMainString = mMainString & mDelimited

        '''51							
        mString = "N"
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''52							
        If pQTR = "Q1" Then
            mString = "N"
        Else
            mString = "Y"
        End If
        mMainString = mMainString & mString
        mMainString = mMainString & mDelimited

        '''53							
        mMainString = mMainString & mDelimited

        '''54							
        mMainString = mMainString & mDelimited

        '''55							
        mMainString = mMainString & mDelimited

        '''56							
        mMainString = mMainString & mDelimited

        '''57							
        mMainString = mMainString & mDelimited

        '''58							
        mMainString = mMainString & mDelimited

        '''59							
        If RsCompany.Fields("FYEAR").Value >= 2013 Then
            mString = Trim(txtResponPANNo.Text) ''IIf(IsNull(RsCompany!TAN_NO), "", RsCompany!TAN_NO)							
            mMainString = mMainString & mString
        End If
        mMainString = mMainString & mDelimited

        '''60							
        mMainString = mMainString & mDelimited

        '''61							
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

        '''69							
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
    Private Function GetChallanDetail(ByRef pTotChallanNo As Double, ByRef pTotDeductee As Double, ByRef pChallanAmount As Double, ByRef pDeducteeAmount As Double) As Boolean
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mTDSAccountCode As String
        Dim CntLst As Integer
        Dim mSectionName As String
        Dim mSectionNameStr As String




        pTotChallanNo = 0
        pTotDeductee = 0
        pChallanAmount = 0
        pDeducteeAmount = 0

        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then							
        '        mTDSAccountCode = MasterNo							
        '    Else							
        '        MsgInformation "Please Enter Valid TDS Account Name."							
        '        GetChallanDetail = False							
        '        Screen.MousePointer = 0							
        '        Exit Function							
        '    End If							

        For CntLst = 0 To lstSection.Items.Count - 1
            If lstSection.GetItemChecked(CntLst) = True Then
                mSectionName = VB6.GetItemString(lstSection, CntLst)
                mSectionNameStr = IIf(mSectionNameStr = "", "'" & mSectionName & "'", mSectionNameStr & "," & "'" & mSectionName & "'")
            End If
        Next

        If mSectionNameStr = "" Then
            MsgInformation("Please Enter Valid TDS Section Name.")
            GetChallanDetail = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If

        SqlStr = "Select COUNT(CHALLANNO) TOTCHALLANNO, SUM(ROUND(AMOUNT,0)) AS TDSAMOUNT "

        SqlStr = SqlStr & vbCrLf & " FROM TDS_CHALLAN TRN, TDS_SECTION_MST SECTIONMST " & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE"

        If mSectionNameStr <> "" Then
            mSectionNameStr = "(" & mSectionNameStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND SECTIONMST.NAME IN " & mSectionNameStr & ""
        End If

        ''& " AND ACCOUNTCODE='" & mTDSAccountCode & "'"							

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.FROMDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.TODATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            pTotChallanNo = IIf(IsDBNull(RsTemp.Fields("TOTCHALLANNO").Value), 0, RsTemp.Fields("TOTCHALLANNO").Value)
            pChallanAmount = IIf(IsDBNull(RsTemp.Fields("TDSAMOUNT").Value), 0, RsTemp.Fields("TDSAMOUNT").Value)
            pChallanAmount = System.Math.Round(pChallanAmount, 0)
        End If

        SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(TDSAMOUNT) TOTTDSAMOUNT "

        SqlStr = SqlStr & vbCrLf & " FROM TDS_TRN TRN, TDS_SECTION_MST SECTIONMST,FIN_SUPP_CUST_MST CMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE(+)" & vbCrLf & " AND TRN.PARTYNAME=CMST.SUPP_CUST_NAME(+)" & vbCrLf _
            & " AND TRN.CANCELLED='N' AND TRN.ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf _
            & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.CHALLANMKEY IN  ( " & vbCrLf & " " & GetChallanQry(True) & ")"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pTotDeductee = IIf(IsDBNull(RsTemp.Fields("TOTDEDUCTEE").Value), 0, RsTemp.Fields("TOTDEDUCTEE").Value)
            pDeducteeAmount = IIf(IsDBNull(RsTemp.Fields("TOTTDSAMOUNT").Value), 0, RsTemp.Fields("TOTTDSAMOUNT").Value)
        End If


        GetChallanDetail = True
        Exit Function
ErrPart1:
        GetChallanDetail = False
    End Function
    Private Function GetChallan_DedDetail(ByRef pDepositAmt As Double, ByRef pTDSAmount As Double, ByRef pSurchargeAmt As Double, ByRef pCessAmt As Double, ByRef pNetAmount As Double, ByRef pIntAmt As Double, ByRef pOthAmt As Double, ByRef pTotDeductee As Double, ByRef pCompany_Code As Integer, ByRef pMkey As String) As Boolean
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim mTDSAccountCode As String
        Dim CntLst As Integer
        Dim mSectionName As String
        Dim mSectionNameStr As String



        pTotDeductee = 0

        pTotDeductee = 0
        pDepositAmt = 0
        pTDSAmount = 0
        pSurchargeAmt = 0
        pCessAmt = 0
        pNetAmount = 0
        pIntAmt = 0
        pOthAmt = 0

        '    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then							
        '        mTDSAccountCode = MasterNo							
        '    Else							
        '        MsgInformation "Please Enter Valid TDS Account Name."							
        '        GetChallan_DedDetail = False							
        '        Screen.MousePointer = 0							
        '        Exit Function							
        '    End If							

        For CntLst = 0 To lstSection.Items.Count - 1
            If lstSection.GetItemChecked(CntLst) = True Then
                mSectionName = VB6.GetItemString(lstSection, CntLst)
                mSectionNameStr = IIf(mSectionNameStr = "", "'" & mSectionName & "'", mSectionNameStr & "," & "'" & mSectionName & "'")
            End If
        Next

        If mSectionNameStr = "" Then
            MsgInformation("Please Enter Valid TDS Section Name.")
            GetChallan_DedDetail = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If

        SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(TRN.AMOUNTPAID) AS DEPOSIT_AMOUNT, " & vbCrLf & " SUM(TDSAMOUNT) AS TOTTDSAMOUNT, " & vbCrLf & " SUM(SURCHARGE) AS TOTSURCHARGE, " & vbCrLf & " SUM(EDU_CESS) AS TOTEDU_CESS, " & vbCrLf & " SUM(NET_AMOUNT) AS TOTNET_AMOUNT, " & vbCrLf & " SUM(INTEREST_AMOUNT) AS TOTINTEREST_AMOUNT, " & vbCrLf & " SUM(OTHER_AMOUNT) AS TOTOTHER_AMOUNT "

        SqlStr = SqlStr & vbCrLf & " FROM TDS_TRN TRN, TDS_SECTION_MST SECTIONMST, FIN_SUPP_CUST_MST CMST "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE(+)" & vbCrLf & " AND TRN.PARTYNAME=CMST.SUPP_CUST_NAME(+)" & vbCrLf & " AND TRN.CHALLANMKEY='" & pMkey & "' " & vbCrLf & " AND TRN.CANCELLED='N' "

        If mSectionNameStr <> "" Then
            mSectionNameStr = "(" & mSectionNameStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND SECTIONMST.NAME IN " & mSectionNameStr & ""
        End If

        ''AND TRN.ACCOUNTCODE='" & mTDSAccountCode & "'"							

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & pCompany_Code & ""

        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
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
    Private Function PrintDD(ByRef mLineCount As Integer, ByRef pCompany_Code As Integer, ByRef pMkey As String, ByRef pChallanLineNo As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mRs As Double
        Dim mPaisa As Double
        Dim mDeducteeRec As Integer
        Dim mLowerDed As String
        Dim mAccountName As String

        With SprdViewAnnex
            For cntRow = 1 To .MaxRows
                mLowerDed = ""
                .Row = cntRow
                .Col = 17
                If pMkey = Trim(.Text) Then
                    '                If mLineCount = 1073 Then MsgBox "OK"							
                    '''1							
                    mString = CStr(mLineCount)
                    mMainString = mString
                    mMainString = mMainString & mDelimited

                    '''2							
                    mString = "DD"
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''3							
                    mString = "1"
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''4							
                    mString = CStr(pChallanLineNo)
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''5							
                    .Col = 1
                    mString = CStr(Val(.Text))
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''6							
                    mString = "O"
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''7							
                    mMainString = mMainString & mDelimited

                    '''8							
                    .Col = 2
                    mString = CStr(Val(.Text))
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''9							
                    mMainString = mMainString & mDelimited


                    '''10							
                    .Col = 3
                    If Len(Trim(.Text)) = 10 Then
                        mString = UCase(Trim(.Text))
                    Else
                        .Col = 14
                        If Val(.Text) < 20 Then
                            mString = "PANINVALID"
                        Else
                            mString = "PANNOTAVBL"
                        End If
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
                    For cntCol = 8 To 11
                        .Col = cntCol
                        mString = VB6.Format(.Text, "0.00")
                        mMainString = mMainString & mString
                        mMainString = mMainString & mDelimited
                    Next

                    '''18							
                    mMainString = mMainString & mDelimited

                    '''19							
                    .Col = 12
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
                    .Col = 13
                    mString = VB6.Format(.Text, "DDMMYYYY")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''25							
                    mMainString = mMainString & mDelimited

                    '''26							
                    .Col = 14
                    mString = VB6.Format(.Text, "0.0000")
                    mMainString = mMainString & mString
                    mMainString = mMainString & mDelimited

                    '''27							
                    mMainString = mMainString & mDelimited

                    '''28							
                    If RsCompany.Fields("FYEAR").Value < 2013 Then
                        mString = "N"
                        mMainString = mMainString & mString
                    End If
                    mMainString = mMainString & mDelimited

                    '''29							
                    mMainString = mMainString & mDelimited

                    '''30							
                    .Col = 14
                    If Val(.Text) < 20 Then
                        .Col = 15
                        mString = IIf(Trim(.Text) = "N", "", "A")
                        mLowerDed = IIf(Trim(.Text) = "N", "", "A")
                        mMainString = mMainString & mString
                        mMainString = mMainString & mDelimited
                    Else
                        mString = "C"
                        mMainString = mMainString & mString
                        mMainString = mMainString & mDelimited
                    End If

                    '''31							
                    mMainString = mMainString & mDelimited

                    '''32							
                    mMainString = mMainString & mDelimited

                    '''33							
                    If RsCompany.Fields("FYEAR").Value >= 2013 Then
                        .Col = 21
                        mString = Trim(.Text)
                        mMainString = mMainString & mString

                        mMainString = mMainString & mDelimited

                        '''34							
                        If mLowerDed = "A" Then
                            .Col = 4
                            mAccountName = Trim(.Text)
                            mString = ""
                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "LOWER_DED_CERT_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = Trim(MasterNo)
                            End If


                            mMainString = mMainString & mString
                        End If
                        mMainString = mMainString & mDelimited

                        '''35							
                        If lblFormType.Text = "27Q" Then
                            mMainString = mMainString & "A"
                        End If
                        mMainString = mMainString & mDelimited

                        '''36							
                        If lblFormType.Text = "27Q" Then
                            mMainString = mMainString & "21"
                        End If
                        mMainString = mMainString & mDelimited

                        '''37							
                        If lblFormType.Text = "27Q" Then
                            mMainString = mMainString & Trim(txtUACK.Text) '' "191521121110614"							
                        End If
                        mMainString = mMainString & mDelimited

                        '''38							
                        If lblFormType.Text = "27Q" Then
                            mMainString = mMainString & "120"
                        End If
                        mMainString = mMainString & mDelimited

                        If lblFormType.Text = "27Q" Then
                            '''39							
                            .Col = 4
                            mAccountName = Trim(.Text)
                            mString = ""
                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_MAILID", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = Trim(MasterNo)
                            End If


                            mMainString = mMainString & mString & mDelimited

                            '''40							
                            mString = ""
                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_MOBILE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = Trim(MasterNo)
                            End If
                            mMainString = mMainString & mString & mDelimited

                            '''41							
                            mString = ""
                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_ADDR", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = Trim(MasterNo)
                            End If

                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CITY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = mString & " " & Trim(MasterNo)
                            End If

                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = mString & " " & Trim(MasterNo)
                            End If

                            mMainString = mMainString & mString & mDelimited


                            '''42							
                            mString = ""
                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "ACCOUNT_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mString = Trim(MasterNo)
                            End If

                            mMainString = mMainString & mString & mDelimited
                        Else
                            '''39							
                            mMainString = mMainString & mDelimited

                            '''40							
                            mMainString = mMainString & mDelimited

                            '''41							
                            mMainString = mMainString & mDelimited

                            '''42							
                            mMainString = mMainString & mDelimited
                        End If
                    End If

                    '''43							
                    '                mMainString = mMainString & mDelimited							

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
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
