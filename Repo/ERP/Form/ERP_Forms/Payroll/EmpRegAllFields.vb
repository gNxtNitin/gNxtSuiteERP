Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmEmpRegAllFields
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    'Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean
    Private Const RowHeight As Short = 20

    Private Const ColCOMPANY_CODE As Short = 1
    Private Const ColCOMPANY_DESC As Short = 2
    Private Const ColEMP_CODE As Short = 3
    Private Const ColEMP_NAME As Short = 4
    Private Const ColEMP_FNAME As Short = 5
    Private Const ColEMP_SPOUSE_NAME As Short = 6
    Private Const ColEMP_ADDR As Short = 7
    Private Const ColEMP_CITY As Short = 8
    Private Const ColEMP_STATE As Short = 9
    Private Const ColEMP_PIN As Short = 10
    Private Const ColISMETROCITY As Short = 11
    Private Const ColEMP_PHONE_NO As Short = 12
    Private Const ColEMP_MOBILE_NO As Short = 13
    Private Const ColEMP_EMAILID As Short = 14
    Private Const ColEMP_EMAILID_OFF As Short = 15
    Private Const ColEMP_CONTACT_PERSON As Short = 16
    Private Const ColEMP_MARITAL_STATUS As Short = 17
    Private Const ColEMP_SEX As Short = 18
    Private Const ColEMP_DOB As Short = 19
    Private Const ColEMP_DOJ As Short = 20
    Private Const ColEMP_GDOJ As Short = 21
    Private Const ColEMP_DOC As Short = 22
    'Private Const ColSALARY_TYPE = 20
    Private Const ColEMP_CATG As Short = 23
    Private Const ColEMP_DEPT_CODE As Short = 24
    Private Const ColEMP_DESG_CODE As Short = 25
    Private Const ColCOST_CENTER_CODE As Short = 26
    Private Const ColEMP_TYPE As Short = 27
    Private Const ColEMP_LEAVE_DATE As Short = 28
    Private Const ColEMP_LEAVE_REASON As Short = 29
    Private Const ColEMP_PF_ACNO As Short = 30
    Private Const ColEMP_PF_DATE As Short = 31
    Private Const ColEMP_ESI_FLAG As Short = 32
    Private Const ColEMP_ESI_NO As Short = 33
    Private Const ColESI_DISPENSARY As Short = 34
    Private Const ColUID_NO As Short = 35
    Private Const ColEMP_BANK_NAME As Short = 36
    Private Const ColEMP_BANK_IFSC As Short = 37
    Private Const ColEMP_BANK_NO As Short = 38
    Private Const ColEMP_PANNO As Short = 39
    Private Const ColEMP_LICNO As Short = 40
    Private Const ColWORKINGTIMEFROM As Short = 41
    Private Const ColWORKINGTIMETO As Short = 42
    Private Const ColWEEKLYOFF As Short = 43
    Private Const ColPaymentMode As Short = 44
    Private Const ColRGP_AUTH As Short = 45
    Private Const ColIS_GRATUITY_PAYABLE As Short = 46
    Private Const ColIS_BONUS_PAYABLE As Short = 47
    Private Const ColIS_LEAVE_ENCHASE_PAYABLE As Short = 48
    Private Const ColEMP_QUALIFICATION As Short = 49
    Private Const ColEMP_LAST_COMPANY As Short = 50
    Private Const ColJOININGDESIGN As Short = 51
    Private Const ColGrade As Short = 52
    Private Const ColPunchOption As Short = 53
    Private Const ColReportToCode As Short = 54
    Private Const ColReportToName As Short = 55

    Private Const ColSALARY_EFF_DATE As Short = 56
    Private Const ColEMP_PREEXP As Short = 57
    Private Const ColEMP_EXP As Short = 58
    Private Const ColEMP_TOTEXP As Short = 59
    Dim MaxCol As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        txtEmpCode.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearch.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub chkDesgCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDesgCategory.CheckStateChanged
        If chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDesgCategory.Enabled = False
        Else
            cboDesgCategory.Enabled = True
        End If
    End Sub


    Private Sub chkDOB_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDOB.CheckStateChanged
        If chkDOB.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDOB.Enabled = False
        Else
            cboDOB.Enabled = True
        End If
    End Sub

    Private Sub chkDOJ_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDOJ.CheckStateChanged
        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDOJ.Enabled = False
        Else
            cboDOJ.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub

    Private Sub cmdExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExport.Click
        Dim cntRow As Double
        Dim mHeadingline As Integer
        Dim exlobj As Object

        Dim ColRow As Integer
        Dim mAttachmentFile As String

        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mDeptDesc As String
        Dim mDespDesc As String
        Dim mEmpDOJ As String
        Dim mReportingTo As String
        Dim mCTC As String
        Dim mEmpGDOJ As String

        mAttachmentFile = My.Application.Info.DirectoryPath & "\Reports\" & "OrganizationChart.xls"
        exlobj = CreateObject("excel.application")
        exlobj.Visible = True
        exlobj.Workbooks.Open(mAttachmentFile)

        mHeadingline = 0
        ''EMP_CODE    EMP_NAME    DEPT_DESC   DESG_DESC   EMP_DOJ MANAGER CTC
        For cntRow = 1 To sprdView.MaxRows
            sprdView.Row = cntRow

            sprdView.Col = ColEMP_CODE
            mEmpCode = Trim(sprdView.Text)

            sprdView.Col = ColEMP_NAME
            mEmpName = Trim(sprdView.Text)

            sprdView.Col = ColEMP_DOJ
            mEmpDOJ = Trim(sprdView.Text)

            sprdView.Col = ColEMP_GDOJ
            mEmpGDOJ = Trim(sprdView.Text)

            sprdView.Col = ColEMP_DEPT_CODE
            mDeptDesc = Trim(sprdView.Text)

            sprdView.Col = ColEMP_DESG_CODE
            mDespDesc = Trim(sprdView.Text)

            sprdView.Col = ColReportToCode
            mReportingTo = Trim(sprdView.Text)

            sprdView.Col = ColReportToName
            mReportingTo = mReportingTo & " - " & Trim(sprdView.Text)

            sprdView.Col = sprdView.MaxCols
            mCTC = Trim(sprdView.Text)

            mHeadingline = cntRow + 1

            With exlobj.ActiveSheet
                .Cells(mHeadingline, 1).Value = mEmpCode & " - " & Trim(mEmpName)
                .Cells(mHeadingline, 2).Value = mEmpCode
                .Cells(mHeadingline, 3).Value = mEmpName
                .Cells(mHeadingline, 4).Value = mDeptDesc
                .Cells(mHeadingline, 5).Value = mDespDesc
                .Cells(mHeadingline, 6).Value = mEmpDOJ
                .Cells(mHeadingline, 7).Value = mReportingTo
                .Cells(mHeadingline, 8).Value = mCTC
            End With
        Next

        mAttachmentFile = "C:\Windows\OrganizationChart.xls"
        With exlobj
            .ScreenUpDating = False
            .DisplayAlerts = False
        End With

        exlobj.ActiveWorkbook.SaveAs(mAttachmentFile)
        '    exlobj.Close
        exlobj.Quit()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        '    Resume
        '    Close #1
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

        Exit Sub

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""

        If lblRegType.Text = "1" Then
            mTitle = "Employee Register "
        Else
            mTitle = "Employee Increment Due Register "
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & " (Emp Name : " & txtEmpCode.Text & " - " & txtName.Text & ")"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Dept : " & cboDept.Text & ") "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Desg : " & cboCategory.Text & ") "
        End If

        mSubTitle = mSubTitle & IIf(cboShow.SelectedIndex = 0, "", " (" & cboShow.Text & ")")


        mSubTitle = mSubTitle & "(From : " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To: " & VB6.Format(txtTo.Text, "DD/MM/YYYY") & ") "
        mRptFileName = "EmpReg.Rpt"


        'Select Record for print...

        SqlStr = ""
        If FillPrintDummyData(sprdView, 1, sprdView.MaxRows, 1, sprdView.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = FetchRecordForReport(SqlStr)
        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
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

        Dim SqlStr As String = ""


        MainClass.ClearGrid(sprdView)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboCategory.Text = "" Then
                MsgInformation("Please select the Category Name.")
                cboCategory.Focus()
                Exit Sub
            End If
        End If

        If chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDesgCategory.Text = "" Then
                MsgInformation("Please select the Desg. Category Name.")
                chkDesgCategory.Focus()
                Exit Sub
            End If
        End If

        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDOJ.Text = "" Then
                MsgInformation("Please select the Month of Joining Month.")
                chkDOJ.Focus()
                Exit Sub
            End If
        End If

        If chkDOB.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDOB.Text = "" Then
                MsgInformation("Please select the Month of Birth Month.")
                chkDOB.Focus()
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FillHeadingSprdView()

        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, sprdView, StrConn, "Y")

        FormatSprd(-1)
        If chkShowSalary.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call CalcTots()
        End If

        CmdPreview.Enabled = True
        cmdPrint.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CalcTots()

        On Error GoTo ErrSprdTotal
        Dim mEmpCode As String
        Dim mBSalary As Double
        'Dim mGSalary As Double
        'Dim mEarn As Double
        Dim mTotEarn As Double
        'Dim mPerks As Double
        Dim mTotDeduct As Double
        Dim mTotPerks As Double
        Dim mCTC As Double
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mYear As Integer
        Dim mFYDate As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xDesgCode As String
        Dim mCat As String
        Dim mCompanyCode As Integer
        Dim mDOJ As String
        Dim mGDOJ As String
        Dim mEMP_TOTEXP As Double
        Dim mEMP_EXP As Double
        Dim mEMP_PREEXP As Double
        Dim mPreCTC As Double
        Dim mPreDesg As String
        Dim mLastIncrement As String
        Dim mJoiningSalary As String
        Dim RsTrfTemp As ADODB.Recordset
        Dim mFromCompanyCode As Integer
        Dim mFromEmpCode As String

        mYear = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(txtFrom.Text), CDate(txtTo.Text)) * 3

        With sprdView
            For cntRow = 1 To .MaxRows
                mCTC = 0
                mTotEarn = 0
                mTotPerks = 0
                mTotDeduct = 0
                mBSalary = 0

                mPreCTC = 0
                mPreDesg = ""
                mEMP_PREEXP = 0
                mEMP_EXP = 0
                mEMP_TOTEXP = 0
                mFromCompanyCode = -1
                mFromEmpCode = ""
                mEmpCode = ""
                mCompanyCode = -1

                .Row = cntRow
                .Col = ColCOMPANY_CODE
                mCompanyCode = Val(.Text)

                .Col = ColEMP_CODE
                mEmpCode = Trim(.Text)

                If mEmpCode = "" Then GoTo NextRec

                If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = GetEmpTransferSQL(mEmpCode, mCompanyCode)
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTrfTemp, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsTrfTemp.EOF = False Then
                        mFromCompanyCode = IIf(IsDBNull(RsTrfTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTrfTemp.Fields("FROM_COMPANY_CODE").Value)
                        mFromEmpCode = IIf(IsDBNull(RsTrfTemp.Fields("FROM_EMP_CODE").Value), "", RsTrfTemp.Fields("FROM_EMP_CODE").Value)

                        If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & mFromCompanyCode & "") = True Then
                            mDOJ = VB6.Format(MasterNo, "DD/MM/YYYY")
                        End If

                        If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_TOTEXP", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & mFromCompanyCode & "") = True Then
                            mEMP_PREEXP = Val(MasterNo)
                        End If
                        '
                        '                .Col = ColEMP_DOJ
                        '                .Text = Format(mDOJ, "DD/MM/YYYY")
                        '                mEMP_PREEXP = IIf(IsNull(RsTrfTemp!EMP_TOTEXP), "", RsTrfTemp!EMP_TOTEXP)
                    Else
                        .Col = ColEMP_DOJ
                        mDOJ = VB6.Format(.Text, "DD/MM/YYYY")

                        .Col = ColEMP_GDOJ
                        mGDOJ = VB6.Format(.Text, "DD/MM/YYYY")

                        .Col = ColEMP_PREEXP
                        mEMP_PREEXP = Val(.Text)
                    End If
                Else
                    .Col = ColEMP_DOJ
                    mDOJ = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColEMP_GDOJ
                    mGDOJ = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColEMP_PREEXP
                    mEMP_PREEXP = Val(.Text)
                End If
                '            mJoiningSalary = GetEmpLastIncrement(mEmpCode, "SALARY_EFF_DATE", mDOJ)
                '            mLastIncrement = GetEmpLastIncrement(mEmpCode, "SALARY_EFF_DATE")
                .Col = ColSALARY_EFF_DATE
                .Text = GetEmpLastestInc(mCompanyCode, mEmpCode, "31/03/" & Year(CDate(txtTo.Text)), mDOJ)

                '            .Text = GetEmpLastIncrement(mCompanyCode, mEmpCode, "SALARY_EFF_DATE")


                mFYDate = "31/03/" & Year(CDate(txtFrom.Text))

                mEMP_EXP = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), PubCurrDate)

                mEMP_TOTEXP = mEMP_PREEXP + mEMP_EXP

                .Col = ColEMP_PREEXP
                .Text = VB6.Format(mEMP_PREEXP / 12, "0.0")

                .Col = ColEMP_EXP
                .Text = VB6.Format(mEMP_EXP / 12, "0.0")

                .Col = ColEMP_TOTEXP
                .Text = VB6.Format(mEMP_TOTEXP / 12, "0.0")



                For cntCol = ColEMP_TOTEXP + 1 To ColEMP_TOTEXP + mYear Step 3
                    .Row = cntRow
                    .Col = cntCol
                    mFYDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(mFYDate)))

                    mSqlStr = " SELECT " & vbCrLf & " TO_CHAR(GETBasicSalaryFROMMST(" & mCompanyCode & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + " & vbCrLf & " GETBasicPartFROMMST(" & mCompanyCode & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))) AS BASICSALARY," & vbCrLf & " GETEMPDESG(" & mCompanyCode & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG FROM DUAL"

                    If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mSqlStr = mSqlStr & vbCrLf & " UNION "

                        mSqlStr = mSqlStr & vbCrLf & " SELECT " & vbCrLf & " TO_CHAR(GETBasicSalaryFROMMST(" & mFromCompanyCode & ",'" & mFromEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + " & vbCrLf & " GETBasicPartFROMMST(" & mFromCompanyCode & ",'" & mFromEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))) AS BASICSALARY," & vbCrLf & " GETEMPDESG(" & mFromCompanyCode & ",'" & mFromEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG FROM PAY_EMPLOYEE_MST"

                        mSqlStr = mSqlStr & vbCrLf & " WHERE COMPANY_CODE=" & mFromCompanyCode & " AND EMP_CODE='" & mFromEmpCode & "'"

                        mSqlStr = mSqlStr & vbCrLf & " AND EMP_LEAVE_DATE>='" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "' "
                    End If



                    '                    mSqlStr = mSqlStr & vbCrLf & " WHERE COMPANY_CODE=" & mFromCompanyCode & " AND EMP_CODE='" & mFromEmpCode & "' AND EMP_LEAVE_DATE>'" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "' "


                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mBSalary = IIf(IsDBNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
                        If mBSalary = 0 Then
                            RsTemp.MoveNext()
                            If RsTemp.EOF = False Then
                                mBSalary = IIf(IsDBNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
                            Else
                                mBSalary = 0
                            End If
                        End If
                    Else
                        mBSalary = 0
                    End If
                    xDesgCode = ""

                    mTotEarn = CalcAllowance(mCompanyCode, mEmpCode, mFYDate, ConEarning)
                    If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mTotEarn = mTotEarn + CalcAllowance(mFromCompanyCode, mFromEmpCode, mFYDate, ConEarning)
                    End If

                    mTotPerks = CalcAllowance(mCompanyCode, mEmpCode, mFYDate, ConPerks)
                    If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mTotPerks = mTotPerks + CalcAllowance(mFromCompanyCode, mFromEmpCode, mFYDate, ConPerks)
                    End If

                    xDesgCode = GetDesgCode(mCompanyCode, mEmpCode, mFYDate)
                    If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & mCompanyCode & "") = True Then
                        mCat = MasterNo
                    End If

                    If CDate(mFYDate) <= CDate("31/03/2007") Then
                        mTotPerks = mTotPerks + (mBSalary * RsCompany.Fields("BonusLimit").Value / 100) ''Bonus
                        mTotPerks = mTotPerks + (mBSalary * 12 / 100) ''PF

                        If mCat = "M" Then
                            mTotPerks = mTotPerks + (mBSalary * 10 / 100) ''Medical
                            '                        mTotPerks = mTotPerks + (mBSalary * 10 / 100) ''LTA
                        End If

                        mTotPerks = mTotPerks + GetLTAAmount(mCompanyCode, mEmpCode, mFYDate, mBSalary, xDesgCode) ''LTA

                    End If

                    mCTC = mBSalary + mTotEarn + mTotPerks

                    .Row = cntRow
                    .Col = cntCol
                    .Text = VB6.Format(mCTC, "0.00")

                    .Row = cntRow
                    .Col = cntCol + 1
                    If mCTC - mPreCTC = 0 Or mPreCTC = 0 Then
                        .Text = CStr(0)
                    Else
                        .Text = VB6.Format(mCTC - mPreCTC, "0.00")
                    End If

                    .Col = cntCol + 2
                    If RsTemp.EOF = False Then
                        If Trim(UCase(mPreDesg)) = Trim(UCase(IIf(IsDBNull(RsTemp.Fields("DESG").Value), 0, RsTemp.Fields("DESG").Value))) Or mPreDesg = "0" Or mPreDesg = "" Then
                            .Text = "NO"
                        Else
                            .Text = "YES"
                        End If
                    Else
                        .Text = "NO"
                    End If
                    .Col = MaxCol - 1
                    .Text = VB6.Format(mBSalary, "0.00")

                    .Col = MaxCol
                    .Text = VB6.Format(mBSalary + mTotEarn, "0.00")

                    mPreCTC = mCTC
                    If RsTemp.EOF = False Then
                        mPreDesg = Trim(UCase(IIf(IsDBNull(RsTemp.Fields("DESG").Value), 0, RsTemp.Fields("DESG").Value)))
                    Else
                        mPreDesg = ""
                    End If
                Next
NextRec:
            Next cntRow
        End With
        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Function GetEmpLastestInc(ByRef pCompanyCode As Integer, ByRef pEmpCode As String, ByRef xWEF As String, ByRef mDOJ As String) As String


        On Error GoTo PrintDummyErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLastInc As String
        Dim mLastCTC As Double

        Dim mPreviousINC As String
        Dim mPreviousCTC As Double

        GetEmpLastestInc = ""


        SqlStr = "SELECT A.SALARY_EFF_DATE - A.ADDDAYS_IN AS SALARY_EFF_DATE, A.BASICSALARY, SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE AND B.ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf & " AND A.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND A.EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND A.SALARY_EFF_DATE="


        SqlStr = SqlStr & vbCrLf & " (SELECT MAX(SALARY_EFF_DATE) AS SALARY_EFF_DATE" & vbCrLf & " FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " GROUP BY A.SALARY_EFF_DATE, A.ADDDAYS_IN , A.BASICSALARY"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mLastInc = IIf(IsDBNull(RsTemp.Fields("SALARY_EFF_DATE").Value), "", RsTemp.Fields("SALARY_EFF_DATE").Value)
            mLastCTC = IIf(IsDBNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value) + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        SqlStr = "SELECT A.SALARY_EFF_DATE - A.ADDDAYS_IN AS SALARY_EFF_DATE, A.BASICSALARY, SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE AND B.ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf & " AND A.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND A.EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND A.SALARY_EFF_DATE="


        SqlStr = SqlStr & vbCrLf & " (SELECT MAX(SALARY_EFF_DATE) AS SALARY_EFF_DATE" & vbCrLf & " FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<TO_DATE('" & VB6.Format(mLastInc, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " GROUP BY A.SALARY_EFF_DATE, A.ADDDAYS_IN , A.BASICSALARY"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPreviousINC = IIf(IsDBNull(RsTemp.Fields("SALARY_EFF_DATE").Value), "", RsTemp.Fields("SALARY_EFF_DATE").Value)
            mPreviousCTC = IIf(IsDBNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value) + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        If mPreviousCTC = mLastCTC Then
            GetEmpLastestInc = VB6.Format(mPreviousINC, "DD/MM/YYYY")
        Else
            GetEmpLastestInc = VB6.Format(mLastInc, "DD/MM/YYYY")
        End If

        If mDOJ = GetEmpLastestInc Then
            GetEmpLastestInc = ""
        End If

        Exit Function
PrintDummyErr:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GetLTAAmount(ByRef mCompanyCode As Integer, ByRef mCode As String, ByRef mFromDate As String, ByRef mBSalary As Double, ByRef xDesgCode As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCat As String
        Dim mEmpCat As String
        Dim mLTAPer As Double
        Dim mLTAAmt As Double




        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & mCompanyCode & "" & vbCrLf & " AND MINLIMIT<=" & Val(CStr(mBSalary)) & " AND MAXLIMIT>=" & Val(CStr(mBSalary)) & " " & vbCrLf & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & mCompanyCode & "" & vbCrLf & " AND WEF_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mLTAPer = IIf(IsDBNull(RsTemp.Fields("LTA_PER").Value), 0, RsTemp.Fields("LTA_PER").Value)
            mLTAAmt = IIf(IsDBNull(RsTemp.Fields("LTAAMT").Value), 0, RsTemp.Fields("LTAAMT").Value)
            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & mCompanyCode & "") = True Then
                mEmpCat = MasterNo
            End If

            If mEmpCat = "R" Then
                GetLTAAmount = IIf(IsDBNull(RsTemp.Fields("LTA_WORK_AMT").Value), 0, RsTemp.Fields("LTA_WORK_AMT").Value)
            Else
                If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & mCompanyCode & "") = True Then
                    mCat = MasterNo
                End If

                If mCat = "M" Or mCat = "D" Then ''mBSalary
                    GetLTAAmount = mBSalary * mLTAPer * 0.01
                ElseIf mCat = "S" Then
                    GetLTAAmount = mLTAAmt / 12
                End If
            End If
        Else
            GetLTAAmount = 0
        End If



        Exit Function
ErrGetLTAAmount:
        GetLTAAmount = 0
    End Function

    Private Function CalcAllowance(ByRef mCompanyCode As Integer, ByRef mCode As String, ByRef pWEFDate As String, ByRef pADDDeduct As Integer) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLeaveDate As String
        Dim mBonusAmount As Double

        '    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & mCompanyCode & "") = True Then
        '        mLeaveDate = MasterNo
        '        If Trim(mLeaveDate) <> "" Then
        '            If CVDate(mLeaveDate) <= CVDate(pWEFDate) Then
        '                CalcAllowance = 0
        '                Exit Function
        '            End If
        '        End If
        '    End If

        mBonusAmount = 0

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE = " & mCompanyCode & "" & vbCrLf & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "'"

        '' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'
        SqlStr = SqlStr & vbCrLf & " AND B.ADDDEDUCT=" & pADDDeduct & " AND B.ISSALPART='N'"

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And pADDDeduct = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND TYPE <> " & ConBonus & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & mCompanyCode & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CalcAllowance = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        Else
            CalcAllowance = 0
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And pADDDeduct = 3 Then
            mBonusAmount = (GetBonusCeilingAmount(mCode, pWEFDate)) ' * mMonthWDays / MainClass.LastDay(Month(mFromDate), Year(mFromDate)
        End If

        CalcAllowance = CalcAllowance + mBonusAmount

        CalcAllowance = System.Math.Round(CalcAllowance, 0)

        Exit Function
ErrGetLTAAmount:
        CalcAllowance = 0
    End Function
    'Private Function GetDesgCode(mCompanyCode As Long, mCode As String, pWEFDate As String) As String
    'On Error GoTo ErrGetLTAAmount
    'Dim RsTemp As ADODB.Recordset = Nothing
    '
    '    SqlStr = " SELECT EMP_DESG_CODE " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf _
    ''            & " WHERE A.COMPANY_CODE = " & mCompanyCode & "" & vbCrLf _
    ''            & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf _
    ''            & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
    ''            & " AND A.EMP_CODE = '" & mCode & "'"
    '
    '     '' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'
    '    SqlStr = SqlStr & vbCrLf & " AND B.ISSALPART='N'"
    '    ''AND B.ADDDEDUCT=" & pADDDeduct & "
    '    SqlStr = SqlStr & vbCrLf _
    ''            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & mCompanyCode & "" & vbCrLf _
    ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND SALARY_EFF_DATE<='" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "')"
    '
    '
    '
    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '    If RsTemp.EOF = False Then
    '       GetDesgCode = IIf(IsNull(RsTemp!EMP_DESG_CODE), "", RsTemp!EMP_DESG_CODE)
    '    Else
    '        GetDesgCode = ""
    '    End If
    '
    'Exit Function
    'ErrGetLTAAmount:
    '    GetDesgCode = ""
    'End Function
    Private Sub frmEmpRegAllFields_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        Me.Text = "Employee Register"


    End Sub

    Private Sub frmEmpRegAllFields_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11775

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11775)


        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        FillDeptCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        OptName.Checked = True


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboDesgCategory.Enabled = False

        chkDOJ.CheckState = System.Windows.Forms.CheckState.Checked
        cboDOJ.Enabled = False

        chkDOB.CheckState = System.Windows.Forms.CheckState.Checked
        cboDOB.Enabled = False

        FillHeadingSprdView()
        FormatSprd(-1)
        cmdExport.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ErrRefreshScreen
        Dim mDeptCode As String

        ''GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))

        MakeSQL = " SELECT EMP.COMPANY_CODE, GMST.COMPANY_NAME, EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, EMP.EMP_SPOUSE_NAME, " & vbCrLf _
            & " EMP.EMP_ADDR, EMP.EMP_CITY, EMP.EMP_STATE, EMP.EMP_PIN, " & vbCrLf _
            & " DECODE(EMP.ISMETROCITY,'Y','YES','NO') AS ISMETROCITY, EMP.EMP_PHONE_NO, EMP.EMP_MOBILE_NO, " & vbCrLf _
            & " EMP.EMP_EMAILID,EMP.EMP_EMAILID_OFF, EMP.EMP_CONTACT_PERSON, DECODE(EMP.EMP_MARITAL_STATUS,'M','MARRIED','UNMARRIED') AS EMP_MARITAL_STATUS, " & vbCrLf _
            & " DECODE(EMP.EMP_SEX,'M','MALE','FEMALE') AS EMP_SEX, TO_CHAR(EMP.EMP_DOB,'DD/MM/YYYY') AS EMP_DOB, TO_CHAR(EMP.EMP_DOJ,'DD/MM/YYYY') AS EMP_DOJ, TO_CHAR(EMP.EMP_GROUP_DOJ,'DD/MM/YYYY') AS EMP_GROUP_DOJ, TO_CHAR(EMP.EMP_DOC,'DD/MM/YYYY') AS EMP_DOC, " & vbCrLf _
            & " CASE WHEN EMP.EMP_CATG='G' THEN 'GENERAL' WHEN EMP.EMP_CATG='P' THEN 'PRODUCTION' WHEN EMP.EMP_CATG='E' THEN 'EXPORT' WHEN EMP.EMP_CATG='R' THEN 'REG. WORKERS' WHEN EMP.EMP_CATG='S' THEN 'R & D STAFF' WHEN EMP.EMP_CATG='D' THEN 'DIRECTOR' WHEN EMP.EMP_CATG='T' THEN 'TRAINEE' END AS EMP_CATG, " & vbCrLf _
            & " DEPT.DEPT_DESC, DESG.DESG_DESC AS DESG, EMP.COST_CENTER_CODE, " & vbCrLf _
            & " DECODE(EMP.EMP_TYPE,'P','PERMANENT',DECODE(EMP.EMP_TYPE,'C','CASUAL',DECODE(EMP.EMP_TYPE,'T','TRAINEE','WORKERS'))) AS EMP_TYPE, TO_CHAR(EMP.EMP_LEAVE_DATE,'DD/MM/YYYY') AS EMP_LEAVE_DATE, EMP.EMP_LEAVE_REASON, EMP.EMP_PF_ACNO, " & vbCrLf _
            & " TO_CHAR(EMP.EMP_PF_DATE,'DD/MM/YYYY') AS EMP_PF_DATE, DECODE(EMP.EMP_ESI_FLAG,'Y','YES','NO') AS EMP_ESI_FLAG, EMP.EMP_ESI_NO, EMP.ESI_DISPENSARY, EMP.UID_NO, " & vbCrLf _
            & " EMP.EMP_BANK_NAME, EMP.EMPBANK_IFSC, EMP.EMP_BANK_NO, EMP.EMP_PANNO, EMP.EMP_LICNO, " & vbCrLf _
            & " EMP.WORKINGTIMEFROM, EMP.WORKINGTIMETO, EMP.WEEKLYOFF, " & vbCrLf _
            & " CASE WHEN EMP.PAYMENTMODE='1' THEN 'CASH' WHEN EMP.PAYMENTMODE='2' THEN 'CHEQUE' WHEN EMP.PAYMENTMODE='3' THEN 'DD' WHEN EMP.PAYMENTMODE='4' THEN 'BANK TRANSFER' END AS PAYMENTMODE, DECODE(EMP.RGP_AUTH,'Y','YES','NO') AS RGP_AUTH, DECODE(EMP.IS_GRATUITY_PAYABLE,'Y','YES','NO') AS IS_GRATUITY_PAYABLE, " & vbCrLf _
            & " DECODE(EMP.IS_BONUS_PAYABLE,'Y','YES','NO') AS IS_BONUS_PAYABLE, DECODE(EMP.IS_LEAVE_ENCHASE_PAYABLE,'Y','YES','NO') AS IS_LEAVE_ENCHASE_PAYABLE, " & vbCrLf _
            & " EMP.EMP_QUALIFICATION, EMP.EMP_LAST_COMPANY, GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,EMP.EMP_DOJ) AS JOININGDESIGN, DESG.GRADE_CODE, " & vbCrLf _
            & " CASE WHEN PUNCH_OPT='P' THEN 'PUNCH' WHEN PUNCH_OPT='M' THEN 'MANNUAL' WHEN PUNCH_OPT='S' THEN 'STOP' ELSE '' END AS PUNCH_OPT," & vbCrLf _
            & " EMP.EMP_HOD_CODE, GETEMPNAME(EMP.COMPANY_CODE,EMP.EMP_HOD_CODE) AS REPORT_TO, " & vbCrLf _
            & " EMP.SALARY_EFF_DATE LAST_INCREMENT, EMP.EMP_TOTEXP AS PREV_EXP, 0 COMPANY_EXP, 0 TOTAL_EXP"

        ''TO_NUMBER('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "' - EMP_DOJ) +

        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, GEN_COMPANY_MST GMST"


        MakeSQL = MakeSQL & vbCrLf & " , PAY_DESG_MST DESG"


        ''Where
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'"

        '    If chkDesgCategory.Value = vbUnchecked Then
        'MakeSQL = MakeSQL & vbCrLf & " AND EMP.COMPANY_CODE=DESG.COMPANY_CODE" & vbCrLf & " AND GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))=DESG.DESG_DESC"
        MakeSQL = MakeSQL & vbCrLf & " AND EMP.COMPANY_CODE=DESG.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DESG_CODE=DESG.DESG_CODE"
        '    End If

        MakeSQL = MakeSQL & vbCrLf & " AND EMP.COMPANY_CODE=GMST.COMPANY_CODE"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If optAllEmp.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND (EMP.EMP_LEAVE_DATE >= TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='') "
        ElseIf optExisting.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='') "
        ElseIf optTrf.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_CODE IN (SELECT DISTINCT EMP_CODE FROM PAY_FFSETTLE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_TRANSFER='Y')"
        Else
            '        MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_DOJ >= '" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_DOJ <= TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And cboDept.Text <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_CORPORATE='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_CORPORATE='Y'"
        End If


        If chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND DESG.DESG_CAT='" & VB.Left(cboDesgCategory.Text, 1) & "' "
        End If

        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND UPPER(TO_CHAR(EMP.EMP_DOJ,'MON'))='" & UCase(cboDOJ.Text) & "' "
        End If

        If chkDOB.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND UPPER(TO_CHAR(EMP.EMP_DOB,'MON'))='" & UCase(cboDOB.Text) & "' "
        End If

        '    If chkDesgCategory.Value = vbUnchecked Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND DESG.DESG_CAT='" & vb.Left(cboDesgCategory, 1) & "' "
        '    End If

        '    MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_CODE='000001'"
        '----ORDER BY
        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.COMPANY_CODE,EMP.EMP_NAME, EMP.EMP_CODE"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.COMPANY_CODE,EMP.EMP_CODE, EMP.EMP_NAME"
        ElseIf optDept.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.COMPANY_CODE,DEPT.DEPT_DESC, EMP.EMP_CODE, EMP.EMP_NAME"
        End If

        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim cntMon As Integer
        Dim RS As ADODB.Recordset = Nothing

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        '    cboDivision.Clear
        '
        '    SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''        & " ORDER BY DIV_DESC"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RS, adLockReadOnly
        '
        '    If RS.EOF = False Then
        '        Do While RS.EOF = False
        '            cboDivision.AddItem RS!DIV_DESC
        '            RS.MoveNext
        '        Loop
        '    End If
        '
        '    cboDivision.ListIndex = 0

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



        cboDesgCategory.Items.Clear()
        cboDesgCategory.Items.Add("Director")
        cboDesgCategory.Items.Add("Manager")
        cboDesgCategory.Items.Add("Staff")
        cboDesgCategory.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Only Plant")
        cboShow.Items.Add("Only Corporate")
        cboShow.SelectedIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        cboDOJ.Items.Clear()
        cboDOB.Items.Clear()
        For cntMon = 1 To 12
            cboDOJ.Items.Add(MonthName(cntMon, True))
            cboDOB.Items.Add(MonthName(cntMon, True))
        Next
        cboDOJ.SelectedIndex = 0
        cboDOB.SelectedIndex = 0
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mYear As Integer
        Dim mNumricCol As Boolean
        Dim mNumric2Col As Boolean
        Dim mStringCol As Boolean

        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim SqlStr As String=""=""
        'Dim mFieldCol As Long

        '    SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST WHERE 1=2"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp
        '
        '    mFieldCol = RsTemp.Fields.Count


        mYear = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(txtFrom.Text), CDate(txtTo.Text)) * 3

        MaxCol = ColEMP_TOTEXP + mYear + 2

        With sprdView
            .Row = mRow
            .set_RowHeight(mRow, RowHeight * 1.1)
            .MaxCols = MaxCol


            '        .Col = ColLocked
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColLocked) = 15
            '        .ColHidden = True
            '
            '        .Col = ColEmpCode
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '        .ColWidth(ColEmpCode) = 6
            '
            '
            '        .Col = ColEmpName
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColEmpName) = 18
            '        .ColsFrozen = ColEmpName
            '
            '        .Col = colDesignation
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .ColWidth(colDesignation) = 12
            '
            '        .Col = ColDeptt
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .ColWidth(ColDeptt) = 12
            '
            '        .Col = ColGrade
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .ColWidth(ColGrade) = 6
            '
            '        .Col = ColDOJ
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeHAlign = TypeHAlignCenter
            '        .ColWidth(ColDOJ) = 9

            mNumricCol = True
            mNumric2Col = False
            mStringCol = False

            For cntCol = ColEMP_TOTEXP + 1 To MaxCol - 2
                .Col = cntCol
                If mNumricCol = True Then
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatMax = CDbl("9999999.99")
                    .TypeFloatMin = CDbl("-9999999.99")
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                    .set_ColWidth(cntCol, 9)
                    mNumricCol = False
                    mStringCol = False
                    mNumric2Col = True
                ElseIf mNumric2Col = True Then
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatMax = CDbl("9999999.99")
                    .TypeFloatMin = CDbl("-9999999.99")
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                    .set_ColWidth(cntCol, 9)
                    mNumricCol = False
                    mStringCol = True
                    mNumric2Col = False
                Else
                    .CellType = SS_CELL_TYPE_EDIT
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                    .set_ColWidth(cntCol, 6)
                    mNumricCol = True
                    mStringCol = False
                    mNumric2Col = False
                End If
                .ColHidden = False
            Next
            '
            '        .Col = ColBankNo
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColBankNo) = 15
            '
            '        .Col = ColPFNo
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColPFNo) = 15
            '
            '        .Col = ColESINo
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColESINo) = 15
            '
            '        .Col = ColPANNo
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColPANNo) = 15
            '        .ColHidden = True
            '
            '        .Col = ColMKEY
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColMKEY) = 15
            '        .ColHidden = True


            .ColsFrozen = ColEMP_NAME

            MainClass.SetSpreadColor(sprdView, -1)
            MainClass.ProtectCell(sprdView, 1, .MaxRows, 1, .MaxCols)
            sprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            sprdView.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            sprdView.DAutoCellTypes = True
            sprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            sprdView.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        End With
        FillHeadingSprdView()

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
        '    Resume
    End Sub
    Private Sub frmEmpRegAllFields_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdView, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdView_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles sprdView.DataColConfig
        sprdView.Row = -1
        sprdView.Col = eventArgs.col
        sprdView.DAutoCellTypes = True
        sprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdView.TypeEditLen = 1000
    End Sub
    Private Sub FillHeadingSprdView()
        Dim mYear As Integer
        Dim Colcnt As Integer
        Dim ColCTC As Integer
        Dim mFYear As Integer

        mYear = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(txtFrom.Text), CDate(txtTo.Text)) * 3

        With sprdView
            MaxCol = ColEMP_TOTEXP + mYear + 2
            .MaxCols = MaxCol

            .Row = 0

            '        .Col = ColEmpCode
            '        .Text = "Emp. Code"
            '
            '        .Col = ColEmpName
            '        .Text = "Name of the Employees"
            '
            '        .Col = colDesignation
            '        .Text = "Designation"
            '
            '        .Col = ColDeptt
            '        .Text = "Department"
            '
            '        .Col = ColGrade
            '        .Text = "Grade"
            '
            '        .Col = ColDOB
            '        .Text = "Date of Birth"
            '
            '        .Col = ColDOJ
            '        .Text = "Joining Date"
            '
            '        .Col = ColDOL
            '        .Text = IIf(lblRegType.Caption = "1", "Date of Leaving", "Date of Next Inc.")
            '
            '        .Col = ColBankNo
            '        .Text = "Bank Account No"
            '
            '        .Col = ColPFNo
            '        .Text = "PF No."
            '
            '        .Col = ColESINo
            '        .Text = "ESI No."
            '
            '        .Col = ColPANNo
            '        .Text = "PAN No."


            '       ColCTC = ColEMP_TOTEXP
            mFYear = GetCurrentFYNo(PubDBCn, (txtFrom.Text))

            For Colcnt = 1 To mYear Step 3
                ColCTC = ColEMP_TOTEXP + Colcnt
                .Col = ColCTC
                .Text = "CTC" & " " & mFYear

                .Col = ColCTC + 1
                .Text = "Increment Amount - " & " " & mFYear

                .Col = ColCTC + 2
                .Text = "Promotion - " & " " & mFYear

                mFYear = mFYear + 1
            Next
            .Col = MaxCol - 1
            .Text = "Basic Salary"

            .Col = MaxCol
            .Text = "Gross Salary"
            '        FillSalaryHeadCol


        End With
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
            '    ElseIf FYChk(txtFrom.Text) = False Then
            '        Cancel = True
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
            '    ElseIf FYChk(txtTo.Text) = False Then
            Cancel = True
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub FillSalaryHeadCol()
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDate As String


        '    ColEARN = ColBasic + 1
        '    mDate = Format(PubCurrDate, "DD-MMM-YYYY")
        '
        '    SqlStr = " SELECT NAME FROM PAY_SALARYHEAD_MST  " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
        ''            & " AND TYPE <> " & ConOT & "  AND ISSALPART='N' "
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND CODE IN (" & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConEarning & ")" & vbCrLf _
        ''            & " AND ISSALPART='N' AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<='" & VB6.Format(mDate, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " UNION " & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConEarning & ")" & vbCrLf _
        ''            & " AND ISSALPART='N' AND STATUS='C' AND CLOSED_DATE>'" & VB6.Format(mDate, "DD-MMM-YYYY") & "')"
        '
        '    SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
        '
        '
        '    With SprdView
        '        .Row = 0
        '        If RsTemp.EOF = False Then
        '            Do While RsTemp.EOF = False
        '                .Col = ColEARN
        '                .Text = IIf(IsNull(RsTemp!Name), "", RsTemp!Name)
        ''                .FontBold = True
        '                RsTemp.MoveNext
        '                ColEARN = ColEARN + 1
        '            Loop
        '        End If
        '    End With
        '
        '    ColGrossSalary = ColEARN
        '    SprdView.Row = 0
        '    SprdView.Col = ColGrossSalary
        '    SprdView.Text = "Gross Salary"
        ''    SprdView.FontBold = True
        '
        '    ColPerks = ColGrossSalary + 1
        '
        '    SqlStr = " SELECT NAME FROM PAY_SALARYHEAD_MST  " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
        ''            & " AND TYPE <> " & ConOT & " AND ISSALPART='N' "
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND CODE IN (" & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
        ''            & " AND ISSALPART='N' AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<='" & VB6.Format(mDate, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " UNION " & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
        ''            & " AND ISSALPART='N' AND STATUS='C' AND CLOSED_DATE>'" & VB6.Format(mDate, "DD-MMM-YYYY") & "')"
        '
        '    SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
        '
        '
        '    With SprdView
        '        .Row = 0
        '        If RsTemp.EOF = False Then
        '            Do While RsTemp.EOF = False
        '                .Col = ColPerks
        '                .Text = IIf(IsNull(RsTemp!Name), "", RsTemp!Name)
        ''                .FontBold = True
        '                RsTemp.MoveNext
        '                ColPerks = ColPerks + 1
        '            Loop
        '        End If
        '    End With
        '
        '    ColCTCSalary = ColPerks
        '    SprdView.Row = 0
        '    SprdView.Col = ColCTCSalary
        '    SprdView.Text = "C.T.C."
        ''    SprdView.FontBold = True
        '

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub sprdView_DblClick(sender As Object, e As _DSpreadEvents_DblClickEvent) Handles sprdView.DblClick
        Dim SqlStr As String = ""
        Dim xEmpCode As String = ""
        Dim xEmpCat As String = "1"
        Dim XRIGHT As String
        Dim myxMenu As String
        Dim pCompanyCode As Long = 1



        sprdView.Row = sprdView.ActiveRow

        sprdView.Col = ColEMP_CODE
        xEmpCode = Trim(sprdView.Text)

        sprdView.Col = ColCOMPANY_CODE
        pCompanyCode = Val(sprdView.Text)

        If pCompanyCode <> RsCompany.Fields("COMPANY_CODE").Value Then
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(xEmpCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xEmpCat = Trim(MasterNo)
        Else
            xEmpCat = "1"
        End If

        If xEmpCat = "1" Then
            myxMenu = "MNUEMPMST"
        Else
            myxMenu = "MNUEMPMSTW"
        End If
        'ElseIf lblEmpType.Text = "S" Then
        'SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
        'Else
        'SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
        'End If

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myxMenu, PubDBCn)
        'If InStr(1, XRIGHT, "V", CompareMethod.Text) = 0 Then
        '    Exit Sub
        'End If
        frmEmployee.MdiParent = Me.MdiParent
        frmEmployee.Show()

        If xEmpCat = "1" Then
            frmEmployee.lblEmpType.Text = "S"
        Else
            frmEmployee.lblEmpType.Text = "W"
        End If

        frmEmployee.frmEmployee_Activated(Nothing, New System.EventArgs())

        frmEmployee.txtEmpNo.Text = xEmpCode

        frmEmployee.TxtEmpNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub
End Class
