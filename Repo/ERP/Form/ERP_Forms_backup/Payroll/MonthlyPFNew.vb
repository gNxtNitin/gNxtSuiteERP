Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMonthlyPFNew
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean

    Private Const ColSNo As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColFName As Short = 3
    Private Const ColPFNo As Short = 4
    Private Const ColUAN As Short = 5
    Private Const ColWorkedDays As Short = 6
    Private Const ColGrossSalary As Short = 7
    Private Const ColWages1 As Short = 8
    Private Const ColBSalary As Short = 9
    Private Const ColEPF As Short = 10
    Private Const ColAC1 As Short = 11
    Private Const ColAC2 As Short = 12
    Private Const ColAC10 As Short = 13
    Private Const ColAC21 As Short = 14
    Private Const ColAC22 As Short = 15
    Private Const ColNetTotal As Short = 16

    Private Const ConRowHeight As Short = 12
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub FillHeading(ByRef xDate As Date)

        Dim Tempdate As String
        Dim cntCol As Integer
        Dim NewDate As Date

        MainClass.ClearGrid(SprdView)

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Call FormatSprd(-1)

    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCeiling.Enabled = False
        Else
            cboCeiling.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub cmdCTextFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCTextFile.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ShowDosReport("V")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function ShowDosReport(ByRef pPrintMode As String) As Boolean
        On Error GoTo ErrPart
        Dim pFileName As String

        pFileName = mLocalPath & "\ePF.txt"


        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        FileOpen(1, pFileName, OpenMode.Output)

        Call CreatePFTextFile()


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
    Private Function CreatePFTextFile() As Boolean

        On Error GoTo ErrPart
        Dim mTitle As String
        Dim mString As String
        Dim mMainString As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mDelimited As String

        Dim mMemberID As String
        Dim mMemberName As String
        Dim mEPFWages As String
        Dim mEPSWages As String
        Dim mEEShareDue As String
        Dim mEEShareRemitted As String
        Dim mEPSDue As String
        Dim mEPSRemitted As String
        Dim mERShareDue As String
        Dim mERShareRemitted As String
        Dim mNPCDays As String
        Dim mRefund As String
        Dim mArrearEPF As String
        Dim mArrearEE As String
        Dim mArrearER As String
        Dim mArrearEPS As String
        Dim mFName As String
        Dim mRelWithMember As String
        Dim mDOB As String
        Dim mGender As String
        Dim mDOJ As String
        'Dim mDOJEPS As String
        Dim mDOE As String
        'Dim mDOEEPS As String
        Dim mREASON As String
        Dim mCard As String

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaritalStatus As String
        Dim mYM As Integer
        Dim mWDays As Integer
        Dim mMonthDays As Integer
        Dim mCompanyPFEst As String
        Dim mGrossSalary As Double

        mDelimited = "#~#"
        mYM = CInt(VB6.Format(lblYear.Text, "YYYYMM"))
        mCompanyPFEst = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)
        With SprdView
            For cntRow = 1 To .MaxRows - 1
                mFName = ""
                mMaritalStatus = ""
                mDOB = ""
                mGender = ""
                mDOJ = ""
                mDOE = ""
                mREASON = ""
                mRelWithMember = ""
                mMonthDays = MainClass.LastDay(Month(CDate(lblYear.Text)), Year(CDate(lblYear.Text)))

                .Row = cntRow
                .Col = ColCard
                mCard = Trim(.Text)

                .Col = ColWorkedDays
                mWDays = Val(.Text)

                SqlStr = "SELECT EMP_FNAME, EMP_SEX, EMP_MARITAL_STATUS, EMP_DOB,EMP_DOJ,EMP_LEAVE_DATE,EMP_LEAVE_REASON" & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCard) & "'" & vbCrLf & " AND (TO_CHAR(EMP_DOJ,'YYYYMM')=" & mYM & " OR TO_CHAR(EMP_LEAVE_DATE,'YYYYMM')=" & mYM & ")"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then

                    If CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "YYYYMM")) = mYM Then
                        mFName = IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)
                        mMaritalStatus = IIf(IsDbNull(RsTemp.Fields("EMP_MARITAL_STATUS").Value), "", RsTemp.Fields("EMP_MARITAL_STATUS").Value)
                        mDOB = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOB").Value), "", RsTemp.Fields("EMP_DOB").Value), "DD/MM/YYYY")
                        mGender = IIf(IsDbNull(RsTemp.Fields("EMP_SEX").Value), "", RsTemp.Fields("EMP_SEX").Value)
                        mDOJ = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                        If mGender = "M" Then
                            mRelWithMember = "F"
                        Else
                            If mMaritalStatus = "M" Then
                                mRelWithMember = "H"
                            Else
                                mRelWithMember = "F"
                            End If
                        End If
                        mMonthDays = mMonthDays - (VB.Day(CDate(mDOJ)) - 1)
                    End If
                    If CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value), "YYYYMM")) = mYM Then
                        mDOE = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")
                        mREASON = "C" ''Trim(IIf(IsNull(RsTemp!EMP_LEAVE_REASON), "", RsTemp!EMP_LEAVE_REASON))
                        mMonthDays = VB.Day(CDate(mDOE))
                    End If
                End If

                mNPCDays = CStr(mMonthDays - mWDays)
                '            If mNPCDays < 0 Then
                '                MsgBox mNPCDays
                '            End If
                mMainString = ""

                '1
                .Col = ColUAN
                mMemberID = Trim(.Text) '' Mid(Trim(.Text), Len(mCompanyPFEst) + 1)
                mMainString = mMemberID '' Val(mMemberID)

                .Col = ColName
                mMemberName = Trim(.Text)
                mMainString = mMainString & mDelimited & mMemberName

                .Col = ColGrossSalary
                mGrossSalary = CDbl(VB6.Format(.Text, "0"))
                mMainString = mMainString & mDelimited & mGrossSalary

                .Col = ColBSalary
                mEPFWages = VB6.Format(.Text, "0")
                mMainString = mMainString & mDelimited & mEPFWages

                .Col = ColWages1
                mEPSWages = VB6.Format(.Text, "0")
                mMainString = mMainString & mDelimited & mEPSWages


                mMainString = mMainString & mDelimited & IIf(CDbl(mEPSWages) > 15000, 15000, mEPSWages)


                ''UAN NAME    GROSS WAGES EPF WAGES   EPS WAGES   EDLI WAGES  EE SHARE REMITTED   EPS CONTRIBUTION REMITTED   ER SHARE REMITTED   NCP DAYS    REFUNDS

                .Col = ColEPF
                mEEShareDue = VB6.Format(.Text, "0")
                mMainString = mMainString & mDelimited & mEEShareDue

                .Col = ColAC10
                mEEShareDue = VB6.Format(.Text, "0")
                mMainString = mMainString & mDelimited & mEEShareDue

                .Col = ColAC1
                mEEShareDue = VB6.Format(.Text, "0")
                mMainString = mMainString & mDelimited & mEEShareDue


                '            .Col = ColEPF
                '            mEEShareRemitted = Format(.Text, "0")
                '            mMainString = mMainString & mDelimited & mEEShareRemitted

                '            .Col = ColEmployer_8
                '            mEPSDue = Format(.Text, "0")
                '            mMainString = mMainString & mDelimited & mEPSDue
                '
                '            .Col = ColEmployer_8
                '            mEPSRemitted = Format(.Text, "0")
                '            mMainString = mMainString & mDelimited & mEPSRemitted
                '
                '            .Col = ColEmployer_3
                '            mERShareDue = Format(.Text, "0")
                '            mMainString = mMainString & mDelimited & mERShareDue

                '            .Col = ColEmployer_3
                '            mERShareRemitted = Format(.Text, "0")
                '            mMainString = mMainString & mDelimited & mERShareRemitted

                mMainString = mMainString & mDelimited & IIf(CDbl(mNPCDays) = 0, "", mNPCDays)

                mRefund = ""
                mMainString = mMainString & mDelimited & mRefund

                '            mArrearEPF = ""
                '            mMainString = mMainString & mDelimited & mArrearEPF
                '
                '            mArrearEE = ""
                '            mMainString = mMainString & mDelimited & mArrearEE
                '
                '            mArrearER = ""
                '            mMainString = mMainString & mDelimited & mArrearER
                '
                '            mArrearEPS = ""
                '            mMainString = mMainString & mDelimited & mArrearEPS
                '
                '            mMainString = mMainString & mDelimited & mFName
                '
                '            mMainString = mMainString & mDelimited & mRelWithMember
                '
                '            mMainString = mMainString & mDelimited & mDOB
                '
                '            mMainString = mMainString & mDelimited & mGender
                '
                '            mMainString = mMainString & mDelimited & mDOJ
                '
                '            mMainString = mMainString & mDelimited & mDOJ
                '
                '            mMainString = mMainString & mDelimited & mDOE
                '
                '            mMainString = mMainString & mDelimited & mDOE
                '
                '            mMainString = mMainString & mDelimited & mREASON

                PrintLine(1, TAB(0), mMainString)


            Next
        End With
        CreatePFTextFile = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreatePFTextFile = False
        '    Resume
    End Function

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


        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = "For the period : " & lblYear.Text

        If chkShow(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            mSubTitle = mSubTitle & " (ARREAR)"
        ElseIf chkShow(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            mSubTitle = mSubTitle & " (Leave Enchase)"
        ElseIf chkShow(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            mSubTitle = mSubTitle & " (Full & Final)"
        End If


        mTitle = "Provident Fund Contribution List "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboCeiling.Text = "<=6500" Then
                mTitle = mTitle & "(Basic Salary Upto 6500/-) "
            Else
                mTitle = mTitle & "(Basic Salary Above 6500/-) "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & " - " & cboCategory.Text
        End If

        mRptFileName = "PFList.Rpt"


        'Select Record for print...

        SqlStr = ""
        SqlStr = MakeSQL

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
        FillHeading(CDate(lblRunDate.Text))

        MainClass.ClearGrid(SprdView)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboCeiling.Text = "" Then
                MsgInformation("Please select Ceiling.")
                cboCeiling.Focus()
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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, "Y")

        DisplayTotals()
        '    FormatSprd -1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub frmMonthlyPFNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmMonthlyPFNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)
        FillHeading(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        OptName.Checked = True
        '    optShow(0).Value = True
        chkShow(1).CheckState = System.Windows.Forms.CheckState.Checked
        FillDeptCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmMonthlyPFNew_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Function MakeSQL() As String

        On Error GoTo ErrRefreshScreen
        Dim mShow As String
        Dim mRunDate As String
        Dim mAddAmount As Double
        Dim mBasicSalary As String
        Dim mAcct22Per As Double

        '            & " TO_CHAR(TO_NUMBER(LAST_DAY(SAL_DATE)-(ADD_MONTHS(LAST_DAY(SAL_DATE),-1))) - WDAYS) AS NPC"
        'PENSIONWAGES
        Dim mAcct2Per As Double


        mRunDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mRunDate = VB6.Format(mRunDate, "DD/MM/YYYY")

        If CDate(lblRunDate.Text) >= CDate("01/04/2017") Then
            mAcct2Per = IIf(IsDbNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value)
            mAcct22Per = IIf(IsDbNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value)
        ElseIf CDate(lblRunDate.Text) >= CDate("01/11/2015") Then
            mAcct2Per = 0.85
            mAcct22Per = CDbl("0.01")
        Else
            mAcct2Per = 1.1
            mAcct22Per = 0
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            mAddAmount = IIf(mAcct22Per = 0, 0, 1)
        Else
            mAddAmount = 0
        End If

        mBasicSalary = " GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(mRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + GETBasicPartFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(mRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MakeSQL = " SELECT  EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME," & vbCrLf & " EMP.EMP_PF_ACNO, EMP.UID_NO, "


        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(PFESI.WDAYS,'999.9'), "


        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(mRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + " & vbCrLf & " GETBasicPartFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(mRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) +  " & vbCrLf & " GETADD_DEDSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(mRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " )AS GROSSSALARY,"

        ''PFESI.PFABLEAMT '" & mBasicSalary & "

        MakeSQL = MakeSQL & vbCrLf & " PENSIONWAGES, " & vbCrLf & " PFESI.PFABLEAMT, " & vbCrLf & " ROUND(PFAMT,0) AS PFAMT, " & vbCrLf & " ROUND(EPFAMT,0) AS AC1, " & vbCrLf & " ROUND(PFESI.PFABLEAMT * " & mAcct2Per & " * 0.01, 0) AC2," & vbCrLf & " ROUND(PENSIONFUND,0) AS AC10, " & vbCrLf & " ROUND(PENSIONWAGES * 0.5 * 0.01, 0) AS AC21, " & vbCrLf & " CEIL(PFESI.PFABLEAMT * " & mAcct22Per & " * 0.01) + " & mAddAmount & " AS AC22," & vbCrLf & " ROUND(PFAMT,0) + ROUND(EPFAMT,0) +" & vbCrLf & " ROUND(PFESI.PFABLEAMT * " & mAcct2Per & " * 0.01, 0) +" & vbCrLf & " ROUND(PENSIONFUND,0) + " & vbCrLf & " ROUND(PENSIONWAGES * 0.5 * 0.01, 0) + " & vbCrLf & " CEIL(PFESI.PFABLEAMT * " & mAcct22Per & " * 0.01) + " & mAddAmount & " AS NetTotal" & vbCrLf

        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf & " AND EMP.COMPANY_CODE=PFESI.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CODE=PFESI.EMP_CODE" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'"

        MakeSQL = MakeSQL & vbCrLf & "AND PFESI.PFAMT>0"

        '     MakeSQL = MakeSQL & vbCrLf _
        ''            & " ROUND(EPFAMT,0) AS AC1, ROUND(PFESI.PFABLEAMT * " & mAcct2Per & " * 0.01, 2) AC2," & vbCrLf _
        ''            & " ROUND(PENSIONFUND,0) AS AC10, ROUND(PENSIONWAGES * 0.5 * 0.01, 2) AS AC21, ROUND(PENSIONWAGES * 0.01 * 0.01, 2) AS AC22" & vbCrLf _
        ''            & " FROM PAY_EMPLOYEE_MST EMP, PAY_PFESI_TRN PFESI " & vbCrLf _
        ''            & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf _
        ''            & " AND EMP.COMPANY_CODE=PFESI.COMPANY_CODE" & vbCrLf _
        ''            & " AND EMP.EMP_CODE=PFESI.EMP_CODE" & vbCrLf _
        ''            & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(Format(lblRunDate.Caption, "MMM-YYYY")) & "'"
        '
        '
        '    & " ROUND(PFESI.PFABLEAMT * " & mAcct2Per & " * 0.01, 0) AC2," & vbCrLf _
        ''            & " ROUND(PENSIONFUND,0) AS AC10, ROUND(PENSIONWAGES * 0.5 * 0.01, 0) AS AC21, " & vbCrLf _
        ''            & " ROUND(PENSIONWAGES * 0.01 * 0.01, 0) AS AC22, " & vbCrLf _
        ''            & " ROUND(PFESI.EPFAMT,0) +  " & vbCrLf _
        ''            & " ROUND(EPFAMT,0) + " & vbCrLf _
        ''            & " ROUND(PFESI.PFABLEAMT * " & mAcct2Per & " * 0.01, 0) + " & vbCrLf _
        ''            & " ROUND(PENSIONFUND,0) + ROUND(PENSIONWAGES * 0.5 * 0.01, 0) + " & vbCrLf _
        ''            & " ROUND(PENSIONWAGES * 0.01 * 0.01, 0) AS NetTotal" & vbCrLf _
        ''
        'ROUND(PFESI.PFABLEAMT * 1.1 * 0.01, 0)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND PFESI.BasicSalary  " & cboCeiling.Text & ""
        End If

        If chkShow(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            mShow = "'N'"
        End If

        If chkShow(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            mShow = IIf(mShow = "", "", mShow & ",") & "'Y'"
        End If

        If chkShow(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            mShow = IIf(mShow = "", "", mShow & ",") & "'E'"
        End If

        If chkShow(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            mShow = IIf(mShow = "", "", mShow & ",") & "'F'"
        End If

        If chkShow(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            mShow = IIf(mShow = "", "", mShow & ",") & "'O'"
            mShow = IIf(mShow = "", "", mShow & ",") & "'V'"
            mShow = IIf(mShow = "", "", mShow & ",") & "'X'"
        End If

        If chkShow(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            mShow = IIf(mShow = "", "", mShow & ",") & "'P'"
        End If

        If mShow <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR  IN (" & mShow & ")"
        End If
        '
        '    If optShow(0).Value = True Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR ='N'"
        '    ElseIf optShow(1).Value = True Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR='Y'"
        '    ElseIf optShow(2).Value = True Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR='E'"
        '    ElseIf optShow(3).Value = True Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR='F'"
        '    ElseIf optShow(4).Value = True Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR IN ('O','V','X')"
        '    ElseIf optShow(5).Value = True Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR='P'"
        '    End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf OptPF.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " Order by EMP.UID_NO "
        End If

        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing

        cboCeiling.Items.Clear()
        cboCeiling.Items.Add("<=15000")
        cboCeiling.Items.Add(">15000")
        cboCeiling.SelectedIndex = 0

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

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdView
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 2)
            .MaxCols = ColNetTotal

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 5)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 20)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColFName, 15)

            .Col = ColPFNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPFNo, 10)

            .Col = ColUAN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColUAN, 10)

            .Col = ColWorkedDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColWorkedDays, 4)


            For cntCol = ColGrossSalary To ColNetTotal
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next

        End With

        MainClass.ProtectCell(SprdView, 1, SprdView.MaxRows, ColCard, ColNetTotal)
        MainClass.SetSpreadColor(SprdView, mRow)

        With SprdView
            .Row = 0
            .Col = ColSNo
            .Text = "S. No."

            .Col = ColCard
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "
            .ColsFrozen = ColName

            .Col = ColFName
            .Text = "Employees' Father Name "

            .Col = ColPFNo
            .Text = "P.F. No."

            .Col = ColUAN
            .Text = "UAN"

            .Col = ColWorkedDays
            .Text = "Days"


            .Col = ColGrossSalary
            .Text = "Gross Salary"

            .Col = ColWages1
            .Text = "8.33% Wages"

            .Col = ColBSalary
            .Text = "Basic Salary"

            .Col = ColEPF
            .Text = "EPF Amount"

            .Col = ColNetTotal
            .Text = "Grand Total"

            .Col = ColAC1
            .Text = "A/c - 1"

            .Col = ColAC2
            .Text = "A/c - 2"

            .Col = ColAC10
            .Text = "A/c - 10"

            .Col = ColAC21
            .Text = "A/c - 21"

            .Col = ColAC22
            .Text = "A/c - 22"

            '         .Col = ColGSalary
            '        .Text = "Gross Salary"
            '
            '        .Col = ColESIAmount
            '        .Text = "ESI Amount"


            MainClass.ProtectCell(SprdView, 0, .MaxRows, 0, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub DisplayTotals()

        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mTot1 As Double
        Dim mTot2 As Double
        Dim mTot3 As Double
        Dim mTot4 As Double
        Dim mTot5 As Double
        Dim mTot6 As Double
        Dim mTot7 As Double
        Dim mTot8 As Double

        Dim mTot9 As Double
        Dim mTot10 As Double
        Dim mTot11 As Double
        Dim mTot12 As Double
        Dim mTot13 As Double
        Dim mGrossSalary As Double

        With SprdView
            Call MainClass.AddBlankfpSprdRow(SprdView, ColCard)
            .Row = .MaxRows

            .Col = ColCard
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '&H80FF80
            .BlockMode = False

            '        Call CalcRowTotal(SprdView, ColDebit, 1, ColDebit, .MaxRows - 1, .MaxRows, ColDebit)
            '        Call CalcRowTotal(SprdView, ColCredit, 1, ColCredit, .MaxRows - 1, .MaxRows, ColCredit)

            FormatSprd(-1)

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColGrossSalary
                mGrossSalary = mGrossSalary + Val(.Text)

                .Col = ColWages1
                mTot1 = mTot1 + Val(.Text)

                .Col = ColBSalary
                mTot2 = mTot2 + Val(.Text)

                .Col = ColEPF
                mTot3 = mTot3 + Val(.Text)

                '            .Col = ColEmployer_8
                '            mTot4 = mTot4 + Val(.Text)
                '
                '            .Col = ColEmployer_3
                '            mTot5 = mTot5 + Val(.Text)
                '
                '            .Col = ColTot_Employer
                '            mTot6 = mTot6 + Val(.Text)

                .Col = ColNetTotal
                mTot7 = mTot7 + Val(.Text)

                '            .Col = ColVPF
                '            mTot8 = mTot8 + Val(.Text)
                '
                .Col = ColAC1
                mTot9 = mTot9 + Val(.Text)

                .Col = ColAC2
                mTot10 = mTot10 + Val(.Text)

                .Col = ColAC10
                mTot11 = mTot11 + Val(.Text)

                .Col = ColAC21
                mTot12 = mTot12 + Val(.Text)

                .Col = ColAC22
                mTot13 = mTot13 + Val(.Text)
            Next


            .Row = .MaxRows


            .Col = ColGrossSalary
            .Text = VB6.Format(mGrossSalary, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColWages1
            .Text = VB6.Format(mTot1, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBSalary
            .Text = VB6.Format(mTot2, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColEPF
            .Text = VB6.Format(mTot3, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            '        .Col = ColEmployer_8
            '        .Text = Format(mTot4, "0.00")
            '        .FontBold = True
            '
            '        .Col = ColEmployer_3
            '        .Text = Format(mTot5, "0.00")
            '        .FontBold = True

            '        .Col = ColTot_Employer
            '        .Text = Format(mTot6, "0.00")
            '        .FontBold = True
            '
            .Col = ColNetTotal
            .Text = VB6.Format(mTot7, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            '        .Col = ColVPF
            '        .Text = Format(mTot8, "0.00")
            '        .FontBold = True

            .Col = ColAC1
            .Text = VB6.Format(mTot9, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColAC2
            .Text = VB6.Format(mTot10, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColAC10
            .Text = VB6.Format(mTot11, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColAC21
            .Text = VB6.Format(mTot12, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColAC22
            .Text = VB6.Format(mTot13, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .set_RowHeight(.Row, 1.25 * ConRowHeight)
            '        .RowsFrozen = .MaxRows
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
End Class
