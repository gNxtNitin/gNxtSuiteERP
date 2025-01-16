Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpServiceReg
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
    Private Const ColFName As Short = 4
    Private Const colDesignation As Short = 5
    Private Const ColDeptt As Short = 6
    Private Const ColDOJ As Short = 7
    Private Const ColDOL As Short = 8
    Private Const ColYear As Short = 9
    Private Const ColDays As Short = 10
    Private Const ColPaidDays As Short = 11
    Private Const ColBasicSalary As Short = 12
    Private Const ColPaidAmount As Short = 13
    Private Const ColExpYear As Short = 14
    Private Const ColCostCenter As Short = 15
    Private Const ColGroup1 As Short = 16
    Private Const ColGroup2 As Short = 17
    Private Const ColQualification As Short = 18
    'Private Const ColMKEY = 17

    Dim ColEARN As Integer
    Dim ColGrossSalary As Integer
    Dim ColPerks As Integer
    Dim ColCTCSalary As Integer
    Dim ColMKEY As Integer

    'Private Const ColHRA = 14
    'Private Const ColConv = 15
    'Private Const ColCEA = 16
    'Private Const ColOther1 = 17
    'Private Const ColGrossSalary = 18

    'Private Const ColEMP_PF = 20
    'Private Const ColMedical = 21
    'Private Const ColLTA = 22
    'Private Const ColBonus = 23
    'Private Const ColOther2 = 24
    'Private Const ColCTC = 25


    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mEmplerPFCont As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub

    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.SelectedIndexChanged
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
            CboDept.Enabled = False
        Else
            CboDept.Enabled = True
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

    Private Sub chkDOJ_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDOJ.CheckStateChanged
        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCond.Enabled = False
            txtAsOn.Enabled = False
            txtPeriod.Enabled = False
        Else
            cboCond.Enabled = True
            txtAsOn.Enabled = True
            txtPeriod.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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

        mSubTitle = ""

        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Unchecked And Val(txtPeriod.Text) >= 5 Then
            mTitle = "Employee Gratuity"
        Else
            mTitle = "Employee Service Period Register "
        End If

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Division : " & cboDivision.Text & ") "
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Dept : " & CboDept.Text & ") "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Desg : " & cboCategory.Text & ") "
        End If

        mSubTitle = mSubTitle & IIf(cboShow.SelectedIndex = 0, "", " (" & cboShow.Text & ")")

        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " AS ON " & VB6.Format(txtAsOn.Text, "DD/MM/YYYY")
        End If

        mRptFileName = IIf(OptPrint(0).Checked = True, "EmpSeviceReg.Rpt", "EmpExpReg.Rpt")


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


        MainClass.ClearGrid(SprdView, RowHeight)

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                MsgInformation("Please select the Cost Center Name.")
                cboDivision.Focus()
                Exit Sub
            End If
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If CboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                CboDept.Focus()
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

        FillHeadingSprdView()
        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, "Y")

        FormatSprd(-1)
        Call CalcTots()
        Call PrintCommand(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Dept_Change()
        Call PrintCommand(False)
    End Sub

    Private Sub frmEmpServiceReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmEmpServiceReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FillDeptCombo()

        OptName.Checked = True

        chkDOJ.CheckState = System.Windows.Forms.CheckState.Checked
        cboCond.Enabled = False
        txtAsOn.Enabled = False
        txtPeriod.Enabled = False
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        CboDept.Enabled = False
        chkDiv.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        FillHeadingSprdView()
        FormatSprd(-1)

        txtPeriod.Text = CStr(0)
        Call PrintCommand(False)
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
        Dim mField As String

        If optCheckDOJ.Checked = True Then
            mField = "EMP.EMP_DOJ"
        Else
            mField = "EMP.EMP_GROUP_DOJ"
        End If

        '     MakeSQL = " SELECT '', EMP.EMP_CODE , EMP.EMP_NAME, " & vbCrLf _
        ''            & " GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "') AS DESG_DESC, " & vbCrLf _
        ''            & " DEPT.DEPT_DESC, " & vbCrLf _
        ''            & " EMP.EMP_DOJ, EMP.EMP_LEAVE_DATE, " & vbCrLf _
        ''            & " TO_CHAR(TRUNC(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ELSE EMP.EMP_LEAVE_DATE END - EMP.EMP_DOJ)/365))  YEARS, " & vbCrLf _
        ''            & " TO_CHAR(MOD(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ELSE EMP.EMP_LEAVE_DATE END - EMP.EMP_DOJ),365))  DAYS, " & vbCrLf _
        ''            & " TO_CHAR(CASE WHEN IS_GRATUITY_PAYABLE='N' THEN 0 ELSE (TRUNC(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ELSE EMP.EMP_LEAVE_DATE END - EMP.EMP_DOJ)/365)*15)+ " & vbCrLf _
        ''            & " CASE WHEN (MOD(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ELSE EMP.EMP_LEAVE_DATE END - EMP.EMP_DOJ),365))>182 " & vbCrLf _
        ''            & " THEN 15 ELSE 0 END END) PaidDays, " & vbCrLf _
        ''            & " TO_CHAR(GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')+GETBasicPartFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')), " & vbCrLf _
        ''            & " TO_CHAR(CASE WHEN IS_GRATUITY_PAYABLE='N' THEN 0 ELSE ROUND((TRUNC(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ELSE EMP.EMP_LEAVE_DATE END - EMP.EMP_DOJ)/365) + " & vbCrLf _
        ''            & " CASE WHEN (MOD(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ELSE EMP.EMP_LEAVE_DATE END - EMP.EMP_DOJ),365))>182 " & vbCrLf _
        ''            & " THEN 1 ELSE 0 END) * " & vbCrLf _
        ''            & " 15 * (GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')+GETBasicPartFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "')/26),0) end)  PaidDays, " & vbCrLf _
        ''            & " TO_CHAR(TRUNC(EMP.EMP_TOTEXP/12) + (MOD(EMP.EMP_TOTEXP,12)/12)) AS EMP_TOTEXP," & vbCrLf _
        ''            & " 0, 0, 0, 0, " & vbCrLf _
        ''            & " TO_CHAR(GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(RunDate, "DD-MMM-YYYY") & "') + GETADD_DEDSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,'" & VB6.Format(RunDate, "DD-MMM-YYYY") & "'))," & vbCrLf _
        ''            & " EMP_QUALIFICATION, 0,0,0,0,0,0, CC.CC_DESC, "

        MakeSQL = " SELECT '', EMP.EMP_CODE , EMP.EMP_NAME, EMP.EMP_FNAME," & vbCrLf _
            & " GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf _
            & " DEPT.DEPT_DESC, " & vbCrLf _
            & " " & mField & ", EMP.EMP_LEAVE_DATE, " & vbCrLf _
            & " TO_CHAR(TRUNC(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ELSE EMP.EMP_LEAVE_DATE END - " & mField & ")/365))  YEARS, " & vbCrLf _
            & " TO_CHAR(MOD(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ELSE EMP.EMP_LEAVE_DATE END - " & mField & "),365))  DAYS, " & vbCrLf _
            & " TO_CHAR(CASE WHEN IS_GRATUITY_PAYABLE='N' THEN 0 ELSE (TRUNC(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ELSE EMP.EMP_LEAVE_DATE END - " & mField & ")/365)*15)+ " & vbCrLf _
            & " CASE WHEN (MOD(TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ELSE EMP.EMP_LEAVE_DATE END - " & mField & "),365))>182 " & vbCrLf _
            & " THEN 15 ELSE 0 END END) PaidDays, " & vbCrLf _
            & " TO_CHAR(GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))+GETBasicPartFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))),0  PaidAmount, " & vbCrLf _
            & " TO_CHAR(TRUNC(EMP.EMP_TOTEXP/12) + (MOD(EMP.EMP_TOTEXP,12)/12)) AS EMP_TOTEXP," & vbCrLf _
            & " CC.CC_DESC, "


        If OptGroup(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " '', '', "
        ElseIf OptGroup(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " DEPT.DEPT_DESC, CC.CC_DESC, "
        Else
            MakeSQL = MakeSQL & vbCrLf & " CC.CC_DESC, DEPT.DEPT_DESC, "
        End If

        MakeSQL = MakeSQL & vbCrLf & " EMP_QUALIFICATION  "


        ''From
        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, FIN_CCENTER_HDR CC"

        ''Where
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf & " AND EMP.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf & " AND EMP.COST_CENTER_CODE=CC.CC_CODE" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'"

        If optExisting.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='') "
        End If

        If chkDOJ.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND (TO_NUMBER(CASE WHEN EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='' THEN TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ELSE EMP.EMP_LEAVE_DATE END - " & mField & ")/365)  " & cboCond.Text & " " & Val(txtPeriod.Text) & ""
        End If

        ''TRUNC

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked And cboDivision.Text <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL = MakeSQL & vbCrLf & " AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""

        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And CboDept.Text <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(CboDept.Text)) & "' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_CORPORATE='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_CORPORATE='Y'"
        End If
        '    MakeSQL = MakeSQL & vbCrLf & "AND EMP.IS_GRATUITY_PAYABLE='Y'"
        '----ORDER BY

        If OptGroup(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY "
        ElseIf OptGroup(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY DEPT.DEPT_DESC, "
        Else
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY CC.CC_DESC, "
        End If

        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "EMP.EMP_NAME, EMP.EMP_CODE"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "EMP.EMP_CODE, EMP.EMP_NAME"
        ElseIf optDOJ.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "" & mField & ", EMP.EMP_CODE, EMP.EMP_NAME"
        End If



        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function

    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        'Dim cntMon As Integer
        Dim RS As ADODB.Recordset = Nothing

        CboDept.Items.Clear()
        cboDivision.Items.Clear()

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                CboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
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
        CboDept.SelectedIndex = 0

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

        cboCond.Items.Clear()
        cboCond.Items.Add("=")
        cboCond.Items.Add(">")
        cboCond.Items.Add("<")
        cboCond.Items.Add(">=")
        cboCond.Items.Add(">=")
        cboCond.Items.Add("<>")
        cboCond.SelectedIndex = 0

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
        With SprdView
            .MaxCols = ColMKEY

            .set_RowHeight(-1, RowHeight * 1.1)
            .Row = -1


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

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 18)

            .Col = colDesignation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(colDesignation, 12)

            .Col = ColDeptt
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDeptt, 12)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColDOJ, 9)

            .Col = ColYear
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMax = CInt("9999999")
            .TypeIntegerMin = CInt("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColYear, 8)

            .Col = ColDays
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMax = CInt("9999999")
            .TypeIntegerMin = CInt("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDays, 8)

            .Col = ColPaidDays
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMax = CInt("9999999")
            .TypeIntegerMin = CInt("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPaidDays, 8)

            For cntCol = ColBasicSalary To ColExpYear
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 12)
            Next


            .Col = ColQualification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColQualification, 15)

            .Col = ColCostCenter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCostCenter, 15)

            .Col = ColGroup1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGroup1, 15)
            .ColHidden = True

            .Col = ColGroup2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGroup2, 15)
            .ColHidden = True


            For cntCol = ColQualification + 1 To ColCTCSalary
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 12)
            Next

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

            FillHeadingSprdView()
        End With

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
        '    Resume
    End Sub

    Private Sub FillHeadingSprdView()

        With SprdView
            ColMKEY = ColQualification + GetSalaryHeadCol + 3
            .MaxCols = ColMKEY

            .Row = 0

            .Col = ColEmpCode
            .Text = "Emp. Code"
            '.FontBold = True

            .Col = ColEmpName
            .Text = "Name of the Employees"
            '.FontBold = True

            .Col = ColFName
            .Text = "Father Name of the Employees"
            '.FontBold = True

            .Col = colDesignation
            .Text = "Designation"
            '.FontBold = True

            .Col = ColDeptt
            .Text = "Department"
            '.FontBold = True

            .Col = ColDOJ
            .Text = "Joining Date"
            '.FontBold = True

            .Col = ColDOL
            .Text = "Date of Leaving"
            '.FontBold = True

            .Col = ColYear
            .Text = "Year"
            '.FontBold = True

            .Col = ColDays
            .Text = "Days"
            '.FontBold = True

            .Col = ColPaidDays
            .Text = "Paid Days"
            '.FontBold = True

            .Col = ColBasicSalary
            .Text = "Basic Salary"
            '.FontBold = True

            .Col = ColPaidAmount
            .Text = "Paid Amount"
            '.FontBold = True

            .Col = ColExpYear
            .Text = "Total Experience"
            '.FontBold = True

            '        .Col = ColHRA
            '        .Text = "H.R.A."
            '        .FontBold = True
            '
            '        .Col = ColConv
            '        .Text = "Conveyance"
            '        .FontBold = True
            '
            '        .Col = ColCEA
            '        .Text = "C.E.A."
            '        .FontBold = True
            '
            '        .Col = ColOther1
            '        .Text = "Others"
            '        .FontBold = True
            '
            '        .Col = ColGrossSalary
            '        .Text = "Gross Salary"
            '        .FontBold = True
            '
            '        .Col = ColQualification
            '        .Text = "Qualification"
            '        .FontBold = True
            '
            '        .Col = ColEMP_PF
            '        .Text = "Employer's PF"
            '        .FontBold = True
            '
            '        .Col = ColMedical
            '        .Text = "Medical"
            '        .FontBold = True
            '
            '        .Col = ColLTA
            '        .Text = "L.T.A."
            '        .FontBold = True
            '
            '        .Col = ColBonus
            '        .Text = "Bonus"
            '        .FontBold = True
            '
            '        .Col = ColOther2
            '        .Text = "Others"
            '        .FontBold = True
            '
            '        .Col = ColCTC
            '        .Text = "C.T.C."
            '        .FontBold = True

            .Col = ColCostCenter
            .Text = "Cost Center"
            '.FontBold = True

            .Col = ColGroup1
            .Text = "Group 1"
            '.FontBold = True

            .Col = ColGroup2
            .Text = "Group 2"
            '.FontBold = True



            .Col = ColMKEY
            .Text = "Mkey"
            '.FontBold = True

            FillSalaryHeadCol()
        End With
    End Sub
    Private Function GetSalaryHeadCol() As Integer

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDate As String



        mDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY")

        SqlStr = " SELECT count(1) AS CNTCOL From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & "  AND ISSALPART='N'"

        SqlStr = SqlStr & vbCrLf _
            & " AND CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
            & " AND ISSALPART='N' AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ISSALPART='N' AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
            & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

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


        ColEARN = ColQualification + 1
        mDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY")

        SqlStr = " SELECT NAME FROM PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " AND ISSALPART='N'"

        SqlStr = SqlStr & vbCrLf _
            & " AND CODE IN (" & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & ")" & vbCrLf _
            & " AND ISSALPART='N' AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & ")" & vbCrLf _
            & " AND ISSALPART='N' AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        With SprdView
            .Row = 0
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    .Col = ColEARN
                    .Text = IIf(IsDbNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)
                    '                .FontBold = True
                    RsTemp.MoveNext()
                    ColEARN = ColEARN + 1
                Loop
            End If
        End With

        ColGrossSalary = ColEARN
        SprdView.Row = 0
        SprdView.Col = ColGrossSalary
        SprdView.Text = "Gross Salary"
        '    SprdView.FontBold = True

        ColPerks = ColGrossSalary + 1

        SqlStr = " SELECT NAME FROM PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " AND ISSALPART='N'"

        SqlStr = SqlStr & vbCrLf _
            & " AND CODE IN (" & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
            & " AND ISSALPART='N' AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
            & " AND ISSALPART='N' AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        With SprdView
            .Row = 0
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    .Col = ColPerks
                    .Text = IIf(IsDbNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)
                    '                .FontBold = True
                    RsTemp.MoveNext()
                    ColPerks = ColPerks + 1
                Loop
            End If
        End With

        ColCTCSalary = ColPerks
        SprdView.Row = 0
        SprdView.Col = ColCTCSalary
        SprdView.Text = "C.T.C."
        '    SprdView.FontBold = True


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmEmpServiceReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub optAllEmp_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAllEmp.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub optExisting_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optExisting.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub OptName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptName.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub SprdView_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdView.DataColConfig
        SprdView.Row = -1
        SprdView.Col = eventArgs.col
        SprdView.DAutoCellTypes = True
        SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdView.TypeEditLen = 1000
    End Sub

    Private Sub txtAsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAsOn.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtAsOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAsOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtAsOn.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtAsOn.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPeriod_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPeriod.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtPeriod_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPeriod.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub CalcTotsold()
        'On Error GoTo ErrSprdTotal
        'Dim mEmpCode As String
        'Dim mBSalary As Double
        'Dim mGSalary As Double
        'Dim mLTA As Double
        'Dim mMedical As Double
        'Dim mPFEmployer As Double
        'Dim mCTC As Double
        'Dim mBonus As Double
        'Dim cntRow As Long
        'Dim mHRA As Double
        'Dim mConv As Double
        'Dim mCEA As Double
        'Dim mOthers As Double
        'Dim mOthers2 As Double
        'Dim mMedicalAllow As Double
        'Dim mPaidAmount As Double
        'Dim mPaidDays As Double
        'Dim mMonthDays As Double
        'Dim mIsGratuityPayable As String
        '
        '    Call CheckPFRates(RunDate)
        '    mMonthDays = IIf(IsNull(RsCompany!LEAVEPAIDDAYS), 0, RsCompany!LEAVEPAIDDAYS)
        '
        '    With SprdView
        '        For cntRow = 1 To .MaxRows
        '            mCTC = 0
        '
        '            .Row = cntRow
        '            .Col = ColEmpCode
        '            mEmpCode = Trim(.Text)
        '
        '            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "IS_GRATUITY_PAYABLE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                mIsGratuityPayable = MasterNo
        '            Else
        '                mIsGratuityPayable = "N"
        '            End If
        '
        '            .Col = ColBasicSalary
        '            mBSalary = Val(.Text)
        '
        '            .Col = ColPaidDays
        '            mPaidDays = Val(.Text)
        '
        '            .Col = ColPaidAmount
        '            If mMonthDays = 0 Or mIsGratuityPayable = "N" Then
        '                mPaidAmount = 0
        '            Else
        '                mPaidAmount = Round(Val(mBSalary) * Val(mPaidDays) / mMonthDays, 0)
        '            End If
        '            .Text = mPaidAmount
        '
        '            .Col = ColGrossSalary
        '            mGSalary = Val(.Text)
        '
        '            mHRA = CalcAllowance(mEmpCode, ConHRA)
        '            mConv = CalcAllowance(mEmpCode, ConConveyance)
        '            mCEA = CalcAllowance(mEmpCode, ConChildrenAllw)
        '            mOthers = CalcAllowance(mEmpCode, ConOthers)
        '            mOthers2 = CalcTourAllowance(mEmpCode)
        '
        '            mMedicalAllow = CalcAllowance(mEmpCode, ConMedicalAllw)
        '
        '            mLTA = CalcLTA(mEmpCode, mBSalary)
        '            mBonus = CalcBonus(mEmpCode, mBSalary)
        '
        '            If mMedicalAllow = 0 Then
        '                mMedical = CalcMedical(mEmpCode, mBSalary)
        '            Else
        '                mMedical = 0
        '            End If
        '
        ''            mPFEmployer = Round(mBSalary * 0.12, 0)          ''CalcPFEmployer(mEmpCode)
        '
        '            If mEmplerPFCont = "B" Then
        '                mPFEmployer = Round(mBSalary * mPFRate * 0.01, 0)        ''CalcPFEmployer(mEmpCode)
        '            Else
        '                mPFEmployer = Round(IIf(mBSalary > mPFCeiling, mPFCeiling, mBSalary) * mPFRate * 0.01, 0)
        '            End If
        '
        '            mCTC = mGSalary + mLTA + mBonus + mMedical + mPFEmployer + mOthers2
        '
        '            .Col = ColHRA
        '            .Text = Format(mHRA, "0.00")
        '
        '            .Col = ColConv
        '            .Text = Format(mConv, "0.00")
        '
        '            .Col = ColCEA
        '            .Text = Format(mCEA, "0.00")
        '
        '            .Col = ColOther1
        '            .Text = Format(mOthers, "0.00")
        '
        '            .Col = ColEMP_PF
        '            .Text = Format(mPFEmployer, "0.00")
        '
        '            .Col = ColMedical
        '            .Text = Format(mMedical, "0.00")
        '
        '            .Col = ColLTA
        '            .Text = Format(mLTA, "0.00")
        '
        '            .Col = ColBonus
        '            .Text = Format(mBonus, "0.00")
        '
        '            .Col = ColOther2
        '            .Text = Format(mOthers2, "0.00")
        '
        '
        '            .Col = ColCTC
        '            .Text = Format(mCTC, "0.00")
        '
        '        Next cntRow
        '    End With
        'Exit Sub
        '
        'ErrSprdTotal:
        '    ErrorMsg err.Description, err.Number, vbCritical
    End Sub
    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mEmpCode As String
        Dim mBSalary As Double
        Dim mGSalary As Double
        Dim mEarn As Double
        Dim mTotEarn As Double
        Dim mPerks As Double
        Dim mTotPerks As Double
        Dim mCTC As Double
        Dim cntRow As Integer
        'Dim mSalHeadName As String
        Dim cntCol As Integer

        Dim mPaidAmount As Double
        Dim mPaidDays As Double
        Dim mMonthDays As Double
        Dim mIsGratuityPayable As String

        Call CheckPFRates(RunDate)
        mMonthDays = IIf(IsDbNull(RsCompany.Fields("LEAVEPAIDDAYS").Value), 0, RsCompany.Fields("LEAVEPAIDDAYS").Value)

        With SprdView
            For cntRow = 1 To .MaxRows
                mCTC = 0
                mTotEarn = 0
                mTotPerks = 0

                .Row = cntRow
                .Col = ColEmpCode
                mEmpCode = Trim(.Text)


                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "IS_GRATUITY_PAYABLE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIsGratuityPayable = MasterNo
                Else
                    mIsGratuityPayable = "N"
                End If

                .Col = ColBasicSalary
                mBSalary = Val(.Text)

                .Col = ColPaidDays
                mPaidDays = Val(.Text)

                .Col = ColPaidAmount
                If mMonthDays = 0 Or mIsGratuityPayable = "N" Then
                    mPaidAmount = 0
                Else
                    mPaidAmount = System.Math.Round(Val(CStr(mBSalary)) * Val(CStr(mPaidDays)) / mMonthDays, 0)
                End If
                .Text = CStr(mPaidAmount)

                For cntCol = ColQualification + 1 To ColGrossSalary - 1
                    mEarn = CalcAllowance(mEmpCode, cntCol)
                    .Row = cntRow
                    .Col = cntCol
                    .Text = VB6.Format(mEarn, "0.00")
                    mTotEarn = mTotEarn + mEarn
                Next

                mGSalary = mBSalary + mTotEarn

                .Row = cntRow
                .Col = ColGrossSalary
                .Text = VB6.Format(mGSalary, "0.00")

                For cntCol = ColGrossSalary + 1 To ColCTCSalary - 1
                    mPerks = CalcAllowance(mEmpCode, cntCol)
                    .Row = cntRow
                    .Col = cntCol
                    .Text = VB6.Format(mPerks, "0.00")
                    mTotPerks = mTotPerks + mPerks
                Next

                mCTC = mGSalary + mTotPerks

                .Row = cntRow
                .Col = ColCTCSalary
                .Text = VB6.Format(mCTC, "0.00")

            Next cntRow
        End With
        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset = Nothing
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConPF & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

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
    'Private Function CalcMedical(mCode As String, mBasicSalary As Double) As Double
    'On Error GoTo ErrGetLTAAmount
    'Dim RsTemp As ADODB.Recordset = Nothing
    'Dim mFromDate As String
    'Dim mCat As String
    'Dim mEmpCat As String
    'Dim xDesgCode As String
    'Dim mLTAPer As Double
    'Dim mLTAAmt As Double
    '    mFromDate = Format(txtAsOn.Text, "DD/MM/YYYY")   ''PubCurrDate             ' RsCompany!START_DATE
    '
    '
    '    SqlStr = " SELECT BASICSALARY,EMP_DESG_CODE " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND SALARY_APP_DATE<='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "')"
    '
    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '    If RsTemp.EOF = False Then
    '       xDesgCode = IIf(IsNull(RsTemp!EMP_DESG_CODE), "", RsTemp!EMP_DESG_CODE)
    '
    '        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            mEmpCat = MasterNo
    '        End If
    '
    '        If mEmpCat = "R" Then
    '            CalcMedical = 0
    '        Else
    '            If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mCat = MasterNo
    '            End If
    '
    '            If mCat = "M" Or mCat = "D" Then    ''mBSalary
    '                CalcMedical = mBasicSalary * 10 * 0.01
    '            ElseIf mCat = "S" Then
    '                CalcMedical = 0
    '            End If
    '        End If
    '    Else
    '        CalcMedical = 0
    '    End If
    '
    '    CalcMedical = Round(CalcMedical, 0)
    '
    'Exit Function
    'ErrGetLTAAmount:
    '    CalcMedical = 0
    'End Function
    Private Function CalcBonus(ByRef mCode As String, ByRef mBasicSalary As Double) As Double
        On Error GoTo ErrCalcBonus
        Dim mBonusPer As Double
        Dim mBonusAmount As Double

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "IS_BONUS_PAYABLE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_BONUS_PAYABLE='N'") = True Then
            CalcBonus = 0
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "BONUS_PER", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBonusPer = Val(MasterNo)
        Else
            mBonusPer = 0
        End If

        CalcBonus = (mBasicSalary * mBonusPer) / 100
        CalcBonus = System.Math.Round(CalcBonus, 0)
        Exit Function
ErrCalcBonus:
        CalcBonus = 0
    End Function
    'Private Function CalcLTA(mCode As String, pPayableSalary As Double) As Double
    'On Error GoTo ErrGetLTAAmount
    'Dim RsTemp As ADODB.Recordset = Nothing
    'Dim mFromDate As String
    'Dim mCat As String
    'Dim mEmpCat As String
    'Dim xDesgCode As String
    'Dim mLTAPer As Double
    'Dim mWLTAPer As Double
    'Dim mLTAAmt As Double
    'Dim xBaseOn As String
    '
    '
    '
    '    mFromDate = Format(txtAsOn.Text, "DD/MM/YYYY")           ''PubCurrDate             ' RsCompany!START_DATE
    '
    '
    '    SqlStr = " SELECT BASICSALARY,EMP_DESG_CODE " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND SALARY_APP_DATE<='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "')"
    '
    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '    If RsTemp.EOF = False Then
    '       xDesgCode = IIf(IsNull(RsTemp!EMP_DESG_CODE), "", RsTemp!EMP_DESG_CODE)
    '
    '       SqlStr = " SELECT * " & vbCrLf _
    ''            & " FROM PAY_LTA_MST " & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND MINLIMIT<=" & Val(pPayableSalary) & " AND MAXLIMIT>=" & Val(pPayableSalary) & " " & vbCrLf _
    ''            & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf _
    ''            & " FROM PAY_LTA_MST " & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND WEF_DATE<='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "')"
    '
    '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '        If RsTemp.EOF = False Then
    '            xBaseOn = IIf(IsNull(RsTemp!LTA_WORK_BASE_ON), "A", RsTemp!LTA_WORK_BASE_ON)
    '            mWLTAPer = IIf(IsNull(RsTemp!LTA_WORK_PER), 0, RsTemp!LTA_WORK_PER)
    '            mLTAPer = IIf(IsNull(RsTemp!LTA_PER), 0, RsTemp!LTA_PER)
    '            mLTAAmt = IIf(IsNull(RsTemp!LTAAMT), 0, RsTemp!LTAAMT)
    '            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mEmpCat = MasterNo
    '            End If
    '
    '            If mEmpCat = "R" Then
    '                If xBaseOn = "A" Then
    '                    CalcLTA = (IIf(IsNull(RsTemp!LTA_WORK_AMT), 0, RsTemp!LTA_WORK_AMT)) / 12
    '                Else
    '                    CalcLTA = pPayableSalary * mWLTAPer * 0.01
    '                End If
    '            Else
    '                If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                    mCat = MasterNo
    '                End If
    '
    '                If mCat = "M" Or mCat = "D" Then    ''mBSalary
    '                    CalcLTA = pPayableSalary * mLTAPer * 0.01
    '                ElseIf mCat = "S" Then
    '                    CalcLTA = mLTAAmt / 12
    '                End If
    '            End If
    '        Else
    '            CalcLTA = 0
    '        End If
    '    Else
    '        CalcLTA = 0
    '    End If
    '
    '    CalcLTA = Round(CalcLTA, 0)
    '
    'Exit Function
    'ErrGetLTAAmount:
    '    CalcLTA = 0
    'End Function


    Private Function CalcAllowanceOld(ByRef mCode As String, ByRef pAllow As Short) As Double
        'On Error GoTo ErrGetLTAAmount
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mFromDate As String
        '
        '    mFromDate = Format(txtAsOn.Text, "DD/MM/YYYY")       ''PubCurrDate             ' RsCompany!START_DATE
        '
        '    SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf _
        ''            & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf _
        ''            & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf _
        ''            & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
        ''            & " AND A.EMP_CODE = '" & mCode & "' AND B.ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")"
        '
        '    If pAllow = ConOthers Then
        '        SqlStr = SqlStr & vbCrLf & " AND B.TYPE NOT IN (" & ConHRA & "," & ConConveyance & "," & ConChildrenAllw & ") "
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND B.TYPE=" & pAllow & ""
        '    End If
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf _
        ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
        ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
        ''            & " AND SALARY_EFF_DATE<='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "')"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
        '
        '    If RsTemp.EOF = False Then
        '       CalcAllowance = IIf(IsNull(RsTemp!Amount), "", RsTemp!Amount)
        '    Else
        '        CalcAllowance = 0
        '    End If
        '
        '    CalcAllowance = Round(CalcAllowance, 2)
        '
        '
        'Exit Function
        'ErrGetLTAAmount:
        '    CalcAllowance = 0
    End Function
    Private Function CalcAllowance(ByRef mCode As String, ByRef pCol As Integer) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromDate As String
        Dim mSalHeadName As String

        mFromDate = CStr(PubCurrDate) ' RsCompany!START_DATE

        SprdView.Row = 0
        SprdView.Col = pCol
        mSalHeadName = Trim(SprdView.Text)
        '
        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'"

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CalcAllowance = IIf(IsDbNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        Else
            CalcAllowance = 0
        End If

        CalcAllowance = System.Math.Round(CalcAllowance, 0)

        Exit Function
ErrGetLTAAmount:
        CalcAllowance = 0
    End Function

    Private Function CalcTourAllowance(ByRef mCode As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromDate As String

        mFromDate = VB6.Format(txtAsOn.Text, "DD/MM/YYYY") ''PubCurrDate             ' RsCompany!START_DATE

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_TOUR_TRN A" & vbCrLf & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CalcTourAllowance = IIf(IsDbNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        Else
            CalcTourAllowance = 0
        End If

        CalcTourAllowance = System.Math.Round(CalcTourAllowance, 2)

        Exit Function
ErrGetLTAAmount:
        CalcTourAllowance = 0
    End Function
End Class
