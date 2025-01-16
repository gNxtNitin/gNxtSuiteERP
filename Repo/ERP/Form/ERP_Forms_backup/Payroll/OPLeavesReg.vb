Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOPLeavesReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    'Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean
    Private Const RowHeight As Short = 20

    Private Const ColEmpCode As Short = 1
    Private Const ColEmpName As Short = 2
    Private Const colDesignation As Short = 3
    Private Const ColDeptt As Short = 4
    Private Const ColELOPLeaves As Short = 5
    Private Const ColCLOPLeaves As Short = 6
    Private Const ColSLOPLeaves As Short = 7
    Private Const ColOPLeaves As Short = 8


    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub
    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
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


        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = "For The Year : " & VB6.Format(RunDate, "YYYY")

        mTitle = "Opening Leave Register"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Dept : " & cboDept.Text & ") "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "(Desg : " & cboCategory.Text & ") "
        End If

        mRptFileName = "OPLeaveReg.Rpt"


        'Select Record for print...

        SqlStr = ""
        SqlStr = MakeSQL
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

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmOPLeavesReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmOPLeavesReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
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

        FillDeptCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        OptName.Checked = True

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ErrRefreshScreen
        Dim mDeptCode As String

        MakeSQL = " SELECT  " & vbCrLf & " EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " DESIG.DESG_DESC, DEPTT.DEPT_DESC, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN LEAVECODE=" & EARN & " THEN TRN.OPENING ELSE 0 END)) EL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN LEAVECODE=" & CASUAL & " THEN TRN.OPENING ELSE 0 END)) CL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN LEAVECODE=" & SICK & " THEN TRN.OPENING ELSE 0 END)) SL, " & vbCrLf & " TO_CHAR(SUM(TRN.OPENING)) "

        ''From
        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_OPLEAVE_TRN TRN, " & vbCrLf & " PAY_DESG_MST DESIG, PAY_DEPT_MST DEPTT" & vbCrLf
        ''Where
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PAYYEAR=" & VB6.Format(RunDate, "YYYY") & "" & vbCrLf & " AND DESIG.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPTT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CODE=TRN.EMP_CODE" & vbCrLf & " AND EMP.COMPANY_CODE=DESIG.COMPANY_CODE" & vbCrLf & " AND TRIM(EMP.EMP_DESG_CODE)=TRIM(DESIG.DESG_CODE)" & vbCrLf & " AND EMP.COMPANY_CODE=DEPTT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPTT.DEPT_CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND DEPTT.DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf & " GROUP BY EMP.EMP_CODE, EMP.EMP_NAME, DESIG.DESG_DESC, DEPTT.DEPT_DESC "

        '----ORDER BY
        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_NAME, EMP.EMP_CODE"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_CODE, EMP.EMP_NAME"
        End If

        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim cntMon As Integer

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

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
        With SprdView
            .Row = mRow
            .set_RowHeight(mRow, RowHeight * 1.1)
            .MaxCols = ColOPLeaves

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEmpCode, 6)

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpName, 22)
            .ColsFrozen = ColEmpName

            .Col = colDesignation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(colDesignation, 15)

            .Col = ColDeptt
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDeptt, 12)

            .Col = ColELOPLeaves
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColELOPLeaves, 7)

            .Col = ColCLOPLeaves
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCLOPLeaves, 7)

            .Col = ColSLOPLeaves
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSLOPLeaves, 7)

            .Col = ColOPLeaves
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOPLeaves, 7)

            MainClass.SetSpreadColor(SprdView, -1)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
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

    Private Sub SprdView_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdView.DataColConfig
        SprdView.Row = -1
        SprdView.Col = eventArgs.col
        SprdView.DAutoCellTypes = True
        SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdView.TypeEditLen = 1000
    End Sub
    Private Sub FillHeadingSprdView()

        With SprdView
            .Row = 0

            .Col = ColEmpCode
            .Text = "Emp." & vbNewLine & "Code"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColEmpName
            .Text = "Name of the Employees"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = colDesignation
            .Text = "Designation"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColDeptt
            .Text = "Department"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColELOPLeaves
            .Text = "EL" & vbNewLine & "Opening"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColCLOPLeaves
            .Text = "CL" & vbNewLine & "Opening"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColSLOPLeaves
            .Text = "SL" & vbNewLine & "Opening"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColOPLeaves
            .Text = "Total" & vbNewLine & "Opening"
            .Font = VB6.FontChangeBold(.Font, True)

        End With
    End Sub
End Class
