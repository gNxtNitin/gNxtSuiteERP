Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpCompAssetsDetail
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    'Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean
    Private Const RowHeight As Short = 20

    Private Const ColCompanyCode As Short = 1
    Private Const ColCompanyName As Short = 2
    Private Const ColEmpCode As Short = 3
    Private Const ColEmpName As Short = 4
    Private Const ColAssetsName As Short = 5
    Private Const ColAssetsMake As Short = 6
    Private Const ColAssetsPrice As Short = 7
    Private Const ColAssetsDOP As Short = 8
    Private Const ColAssetsDOI As Short = 9
    Private Const ColAssetsRemarks As Short = 10


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

        Exit Sub

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""

        mTitle = Me.Text

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            mTitle = mTitle & " (Emp Name : " & txtEmpCode.Text & " - " & txtName.Text & ")"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Dept : " & cboDept.Text & ") "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Desg : " & cboCategory.Text & ") "
        End If

        mSubTitle = mSubTitle & IIf(cboShow.SelectedIndex = 0, "", " (" & cboShow.Text & ")")
        mRptFileName = "EmpAssetsReg.Rpt"


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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FillHeadingSprdView()

        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, sprdView, StrConn, "Y")

        FormatSprd(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
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
    Private Sub frmEmpCompAssetsDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        Me.Text = "Employee Company Assets Details"


    End Sub

    Private Sub frmEmpCompAssetsDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        FillDeptCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        OptName.Checked = True


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

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


        MakeSQL = MakeSQLAssets

        '----ORDER BY
        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by 1,3,4"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by 1,4,3"
        ElseIf optDept.Checked = True Then
            '        MakeSQL = MakeSQL & vbCrLf & "Order by EMP.COMPANY_CODE,DEPT.DEPT_DESC, EMP.EMP_CODE, EMP.EMP_NAME"
        End If

        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function

    Private Function MakeSQLAssets() As String

        On Error GoTo ErrRefreshScreen

        MakeSQLAssets = " SELECT EMP.COMPANY_CODE, GMST.COMPANY_NAME, EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " ASSETS_DESC, ASSETS_MAKE, " & vbCrLf & " ASSETS_PRICE, ASSETS_DOP, ASSETS_DOI, ASSETS_REMARKS" & vbCrLf & " FROM PAY_ASSETS_MST SH, PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, GEN_COMPANY_MST GMST, PAY_DESG_MST DESG" & vbCrLf & " WHERE " & vbCrLf & " SH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND SH.EMP_CODE=EMP.EMP_CODE" & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND EMP.COMPANY_CODE=DESG.COMPANY_CODE" & vbCrLf & " AND GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))=DESG.DESG_DESC" & vbCrLf & " AND EMP.COMPANY_CODE=GMST.COMPANY_CODE"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If optExisting.Checked = True Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='') "
        Else
            '        MakeSQLAssets = MakeSQLAssets & vbCrLf & " AND EMP.EMP_DOJ >= '" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "'"
        End If

        '    MakeSQLAssets = MakeSQLAssets & vbCrLf & " AND EMP.EMP_DOJ <= '" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And cboDept.Text <> "" Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND EMP.IS_CORPORATE='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND EMP.IS_CORPORATE='Y'"
        End If

        If chkDesgCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLAssets = MakeSQLAssets & vbCrLf & "AND DESG.DESG_CAT='" & VB.Left(cboDesgCategory.Text, 1) & "' "
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

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1


        With sprdView
            .MaxCols = ColAssetsRemarks
            .Row = mRow
            .set_RowHeight(mRow, RowHeight * 1.1)


            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyCode, 15)
            .ColHidden = True

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 15)
            .ColHidden = True

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpCode, 6)


            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpName, 18)
            .ColsFrozen = ColEmpName

            .Col = ColAssetsName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetsName, 18)

            .Col = ColAssetsMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetsMake, 12)

            .Col = ColAssetsPrice
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAssetsPrice, 8)

            .Col = ColAssetsDOP
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetsDOP, 8)

            .Col = ColAssetsDOI
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetsDOI, 8)


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
    Private Sub frmEmpCompAssetsDetail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

        With sprdView
            .MaxCols = ColAssetsRemarks

            .Row = 0

            .Col = ColCompanyCode
            .Text = "Company Code"

            .Col = ColCompanyName
            .Text = "Company Name"

            .Col = ColEmpCode
            .Text = "Emp. Code"

            .Col = ColEmpName
            .Text = "Name of the Employees"

            .Col = ColAssetsName
            .Text = "Assets Description"

            .Col = ColAssetsMake
            .Text = "Make / Serial No"

            .Col = ColAssetsPrice
            .Text = "Price"

            .Col = ColAssetsDOP
            .Text = "Date Of Purchase"

            .Col = ColAssetsDOI
            .Text = "Date Of Issue"

            .Col = ColAssetsRemarks
            .Text = "Remarks"

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
End Class
