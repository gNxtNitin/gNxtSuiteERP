Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmployeeCTCComp
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    'Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColEmpCode As Short = 2
    Private Const ColEmpName As Short = 3
    Private Const colDesignation As Short = 4
    Private Const ColDeptt As Short = 5
    Private Const ColGrade As Short = 6
    Private Const ColDOB As Short = 7
    Private Const ColDOJ As Short = 8
    Private Const ColDOL As Short = 9
    Private Const ColBankNo As Short = 10
    Private Const ColPFNo As Short = 11
    Private Const ColESINo As Short = 12
    Private Const ColPANNo As Short = 13
    'Dim ColCTCSalary As Long
    Dim ColMKEY As Integer


    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mEmplerPFCont As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkAllDiv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDiv.CheckStateChanged
        If chkAllDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
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

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Dept : " & cboDept.Text & ") "
        End If

        If chkAllDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Division : " & cboDivision.Text & ") "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Desg : " & cboCategory.Text & ") "
        End If

        mSubTitle = mSubTitle & "(From : " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To: " & VB6.Format(txtTo.Text, "DD/MM/YYYY") & ") "
        mRptFileName = "EmpReg.Rpt"


        'Select Record for print...

        SqlStr = ""
        If FillPrintDummyData(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols, PubDBCn) = False Then GoTo ERR1

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


        MainClass.ClearGrid(SprdView)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If chkAllDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                MsgInformation("Please select the Division Name.")
                cboDivision.Focus()
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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FillHeadingSprdView()

        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, "Y")

        FormatSprd(-1)
        Call CalcTots()
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

        Call CheckPFRates(RunDate)
        mYear = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(txtFrom.Text), CDate(txtTo.Text))

        With SprdView
            For cntRow = 1 To .MaxRows
                mCTC = 0
                mTotEarn = 0
                mTotPerks = 0
                mTotDeduct = 0
                mBSalary = 0

                .Row = cntRow
                .Col = ColEmpCode
                mEmpCode = Trim(.Text)
                mFYDate = "31/03/" & Year(CDate(txtFrom.Text))

                For cntCol = ColPANNo + 1 To ColPANNo + mYear
                    .Row = cntRow
                    .Col = cntCol
                    mFYDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(mFYDate)))

                    mSqlStr = " SELECT " & vbCrLf & " TO_CHAR(GETBasicSalaryFROMMST(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + " & vbCrLf & " GETBasicPartFROMMST(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(mFYDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))) AS BASICSALARY" & vbCrLf & " FROM DUAL"

                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mBSalary = IIf(IsDbNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
                    Else
                        mBSalary = 0
                    End If
                    xDesgCode = ""

                    mTotEarn = CalcAllowance(mEmpCode, mFYDate, ConEarning)
                    mTotPerks = CalcAllowance(mEmpCode, mFYDate, ConPerks)
                    xDesgCode = GetDesgCode(RsCompany.Fields("COMPANY_CODE").Value, mEmpCode, mFYDate)
                    If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCat = MasterNo
                    End If

                    If CDate(mFYDate) <= CDate("31/03/2007") Then
                        mTotPerks = mTotPerks + (mBSalary * 20 / 100) ''Bonus
                        mTotPerks = mTotPerks + (mBSalary * 12 / 100) ''PF

                        If mCat = "M" Then
                            mTotPerks = mTotPerks + (mBSalary * 10 / 100) ''Medical
                        End If

                        mTotPerks = mTotPerks + GetLTAAmount(mEmpCode, mFYDate, mBSalary, xDesgCode) ''LTA

                    End If

                    mCTC = mBSalary + mTotEarn + mTotPerks
                    .Text = VB6.Format(mCTC, "0.00")
                Next
            Next cntRow
        End With
        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetLTAAmount(ByRef mCode As String, ByRef mFromDate As String, ByRef mBSalary As Double, ByRef xDesgCode As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCat As String
        Dim mEmpCat As String
        Dim mLTAPer As Double
        Dim mLTAAmt As Double




        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MINLIMIT<=" & Val(CStr(mBSalary)) & " AND MAXLIMIT>=" & Val(CStr(mBSalary)) & " " & vbCrLf & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND WEF_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mLTAPer = IIf(IsDbNull(RsTemp.Fields("LTA_PER").Value), 0, RsTemp.Fields("LTA_PER").Value)
            mLTAAmt = IIf(IsDbNull(RsTemp.Fields("LTAAMT").Value), 0, RsTemp.Fields("LTAAMT").Value)
            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCat = MasterNo
            End If

            If mEmpCat = "R" Then
                GetLTAAmount = IIf(IsDbNull(RsTemp.Fields("LTA_WORK_AMT").Value), 0, RsTemp.Fields("LTA_WORK_AMT").Value)
            Else
                If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConPF & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mPFCeiling = IIf(IsDbNull(RsCeiling.Fields("ceiling").Value), 0, RsCeiling.Fields("ceiling").Value)
            mPFRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
            mPFEPFRate = IIf(IsDbNull(RsCeiling.Fields("EPF").Value), 0, RsCeiling.Fields("EPF").Value)
            mPFPensionRate = IIf(IsDbNull(RsCeiling.Fields("PFUND").Value), 0, RsCeiling.Fields("PFUND").Value)
            mEmplerPFCont = IIf(IsDbNull(RsCeiling.Fields("EMPER_CONT").Value), "B", RsCeiling.Fields("EMPER_CONT").Value)
        Else
            mPFCeiling = 6500
            mPFRate = 12
            mPFEPFRate = 3.67
            mPFPensionRate = 8.33
            mEmplerPFCont = "B"
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function CalcAllowance(ByRef mCode As String, ByRef pWEFDate As String, ByRef pADDDeduct As Integer) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBonusAmount As Double

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "'"

        '' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'
        SqlStr = SqlStr & vbCrLf & " AND B.ADDDEDUCT=" & pADDDeduct & " AND B.ISSALPART='N'"

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And pADDDeduct = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND B.TYPE <> " & ConBonus & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CalcAllowance = IIf(IsDbNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        Else
            CalcAllowance = 0
        End If

        mBonusAmount = 0

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And pADDDeduct = 3 Then
            mBonusAmount = (GetBonusCeilingAmount(mCode, pWEFDate)) ' * mMonthWDays / MainClass.LastDay(Month(mFromDate), Year(mFromDate)
        End If

        CalcAllowance = CalcAllowance + mBonusAmount
        CalcAllowance = System.Math.Round(CalcAllowance, 0)

        Exit Function
ErrGetLTAAmount:
        CalcAllowance = 0
    End Function

    Private Sub frmEmployeeCTCComp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        Me.Text = "Employee CTC Comparison"

    End Sub

    Private Sub frmEmployeeCTCComp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        FillDeptCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        OptName.Checked = True


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        chkAllDiv.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboDesgCategory.Enabled = False

        FillHeadingSprdView()
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ErrRefreshScreen
        Dim mDeptCode As String
        Dim mDivisionCode As Double

        MakeSQL = " SELECT '', EMP.EMP_CODE , EMP.EMP_NAME, " & vbCrLf & " GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf & " DEPT.DEPT_DESC, DESG.GRADE_CODE, " & vbCrLf & " EMP.EMP_DOB, EMP.EMP_DOJ, "

        '    MakeSQL = MakeSQL & vbCrLf & " EMP.EMP_LEAVE_DATE, "

        MakeSQL = MakeSQL & vbCrLf & " GETEMPNEXTINC(EMP.COMPANY_CODE, EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS NextDue,"

        MakeSQL = MakeSQL & vbCrLf & vbCrLf & " EMP.EMP_BANK_NO, EMP.EMP_PF_ACNO, EMP.EMP_ESI_NO, EMP_PANNO "

        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT"


        MakeSQL = MakeSQL & vbCrLf & " , PAY_DESG_MST DESG"


        ''Where
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'"

        '    If chkDesgCategory.Value = vbUnchecked Then
        MakeSQL = MakeSQL & vbCrLf & " AND EMP.COMPANY_CODE=DESG.COMPANY_CODE" & vbCrLf & " AND GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))=DESG.DESG_DESC"
        '    End If

        If optExisting.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='') "
        Else
            '        MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_DOJ >= '" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_DOJ <= TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And cboDept.Text <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
        End If

        If chkAllDiv.CheckState = System.Windows.Forms.CheckState.Unchecked And cboDivision.Text <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL = MakeSQL & vbCrLf & " AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If Trim(cboEmpType.Text) <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_CORPORATE='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_CORPORATE='Y'"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND DESG.DESG_CAT='" & VB.Left(cboDesgCategory.Text, 1) & "' "
        End If

        '    MakeSQL = MakeSQL & vbCrLf & "AND EMP_CAT_TYPE='2' "

        '----ORDER BY
        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_NAME, EMP.EMP_CODE"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_CODE, EMP.EMP_NAME"
        ElseIf optDept.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by DEPT.DEPT_DESC, EMP.EMP_CODE, EMP.EMP_NAME"
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



        cboDesgCategory.Items.Clear()
        cboDesgCategory.Items.Add("Director")
        cboDesgCategory.Items.Add("Manager")
        cboDesgCategory.Items.Add("Staff")
        cboDesgCategory.SelectedIndex = 0

        cboEmpType.Items.Clear()
        cboEmpType.Items.Add("ALL")
        cboEmpType.Items.Add("1 : Staff")
        cboEmpType.Items.Add("2 : Workers")
        cboEmpType.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Only Plant")
        cboShow.Items.Add("Only Corporate")
        cboShow.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mYear As Integer

        mYear = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(txtFrom.Text), CDate(txtTo.Text))

        With SprdView
            .Row = mRow
            .set_RowHeight(mRow, RowHeight * 1.1)
            .MaxCols = ColMKEY


            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEmpCode, 6)


            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpName, 18)
            .ColsFrozen = ColEmpName

            .Col = colDesignation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(colDesignation, 12)

            .Col = ColDeptt
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDeptt, 12)

            .Col = ColGrade
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColGrade, 6)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColDOJ, 9)

            For cntCol = ColPANNo + 1 To ColPANNo + mYear
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
                .ColHidden = False
            Next

            .Col = ColBankNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankNo, 15)

            .Col = ColPFNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPFNo, 15)

            .Col = ColESINo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColESINo, 15)

            .Col = ColPANNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPANNo, 15)
            .ColHidden = True

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 15)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdView, -1)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            SprdView.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdView.DAutoCellTypes = True
            SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdView.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        End With
        FillHeadingSprdView()

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
        '    Resume
    End Sub
    Private Sub frmEmployeeCTCComp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdView, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdView_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdView.DataColConfig
        SprdView.Row = -1
        SprdView.Col = eventArgs.col
        SprdView.DAutoCellTypes = True
        SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdView.TypeEditLen = 1000
    End Sub
    Private Sub FillHeadingSprdView()
        Dim mYear As Integer
        Dim ColCnt As Integer
        Dim ColCTC As Integer

        mYear = DateDiff(Microsoft.VisualBasic.DateInterval.Year, CDate(txtFrom.Text), CDate(txtTo.Text))

        With SprdView
            ColMKEY = ColPANNo + mYear + 1
            .MaxCols = ColMKEY

            .Row = 0

            .Col = ColEmpCode
            .Text = "Emp. Code"
            '        .FontBold = True

            .Col = ColEmpName
            .Text = "Name of the Employees"
            '        .FontBold = True

            .Col = colDesignation
            .Text = "Designation"
            '        .FontBold = True

            .Col = ColDeptt
            .Text = "Department"
            '        .FontBold = True

            .Col = ColGrade
            .Text = "Grade"
            '        .FontBold = True

            .Col = ColDOB
            .Text = "Date of Birth"
            '        .FontBold = True

            .Col = ColDOJ
            .Text = "Joining Date"
            '        .FontBold = True

            .Col = ColDOL
            .Text = "Date of Next Inc." ''"Date of Leaving" ' IIf(lblRegType.Caption = "1", "Date of Leaving", "Date of Next Inc.")
            '        .FontBold = True

            .Col = ColBankNo
            .Text = "Bank Account No"
            '        .FontBold = True

            .Col = ColPFNo
            .Text = "PF No."
            '        .FontBold = True

            .Col = ColESINo
            .Text = "ESI No."
            '        .FontBold = True

            .Col = ColPANNo
            .Text = "PAN No."
            '        .FontBold = True


            ColCTC = ColPANNo

            For ColCnt = 1 To mYear
                ColCTC = ColCTC + 1
                .Col = ColCTC
                .Text = "CTC" & " " & Year(CDate(txtFrom.Text)) + ColCnt - 1
            Next

            '        FillSalaryHeadCol


        End With
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
    Private Function GetSalaryHeadCol() As Integer

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDate As String



        mDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY")

        SqlStr = " SELECT count(1) AS CNTCOL From PAY_SALARYHEAD_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf & " AND TYPE <> " & ConOT & " AND ISSALPART='N' "

        SqlStr = SqlStr & vbCrLf & " AND CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf & " AND ISSALPART='N' AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf & " AND ISSALPART='N' AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetSalaryHeadCol = IIf(IsDbNull(RsTemp.Fields("cntCol").Value), 0, RsTemp.Fields("cntCol").Value)
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

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
End Class
