Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpRegForm12
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
    Private Const ColEMP_NAME As Short = 3
    Private Const ColEMP_ADDR As Short = 4
    Private Const ColEMP_FNAME As Short = 5
    Private Const ColEMP_DESG_CODE As Short = 6
    Private Const ColGroupLetter As Short = 7
    Private Const ColShift As Short = 8
    Private Const ColCertificateNo As Short = 9
    Private Const ColEMP_CODE As Short = 10
    Private Const ColRemarks As Short = 11


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


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

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""

        mTitle = "Register of Adult / Child Workers"


        mSubTitle = "" ''"(Form No. 12)"
        mRptFileName = "EmpRegForm12.Rpt"


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
    Private Sub frmEmpRegForm12_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        Me.Text = "Register of Adult / Child Workers (Form No. 12)"
    End Sub

    Private Sub frmEmpRegForm12_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        MakeSQL = " SELECT EMP.COMPANY_CODE, GMST.COMPANY_NAME, " & vbCrLf & " EMP_NAME, EMP_ADDR || ', ' || EMP_CITY || ', ' || EMP_STATE || ', ' || EMP_PIN," & vbCrLf & " EMP_FNAME, " & vbCrLf & " GETEMPDESG(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG," & vbCrLf & " '', '', ''," & vbCrLf & " EMP_CODE ," & vbCrLf & " EMP_LEAVE_DATE"


        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, GEN_COMPANY_MST GMST"

        ''Where
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=GMST.COMPANY_CODE"

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If optExisting.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE ='') "
        Else
        End If

        '    MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_DOJ <= '" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "'"

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        '    MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_CODE='000001'"
        '----ORDER BY
        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.COMPANY_CODE,EMP.EMP_NAME, EMP.EMP_DOJ"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.COMPANY_CODE,EMP.EMP_DOJ, EMP.EMP_NAME"
        End If

        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim cntMon As Integer

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
            .set_RowHeight(mRow, RowHeight * 1.1)
            .MaxCols = ColRemarks

            For cntCol = ColCOMPANY_CODE To ColRemarks
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .ColHidden = False
            Next


            .ColsFrozen = ColEMP_NAME

            .set_ColWidth(ColCOMPANY_CODE, 6)
            .set_ColWidth(ColCOMPANY_DESC, 15)
            .set_ColWidth(ColEMP_NAME, 30)
            .set_ColWidth(ColEMP_ADDR, 30)
            .set_ColWidth(ColEMP_FNAME, 30)
            .set_ColWidth(ColEMP_DESG_CODE, 30)
            .set_ColWidth(ColGroupLetter, 18)
            .set_ColWidth(ColShift, 18)
            .set_ColWidth(ColCertificateNo, 18)
            .set_ColWidth(ColEMP_CODE, 18)
            .set_ColWidth(ColRemarks, 18)

            .Col = ColCOMPANY_CODE
            .ColHidden = True

            .Col = ColCOMPANY_DESC
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
    Private Sub frmEmpRegForm12_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub SprdView_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles sprdView.DataColConfig
        SprdView.Row = -1
        sprdView.Col = eventArgs.col
        SprdView.DAutoCellTypes = True
        SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdView.TypeEditLen = 1000
    End Sub
    Private Sub FillHeadingSprdView()



        With SprdView
            .MaxCols = ColRemarks

            .Row = 0

            .Col = ColCOMPANY_CODE
            .Text = "Company Code"

            .Col = ColCOMPANY_DESC
            .Text = "Company Name"

            .Col = ColEMP_NAME
            .Text = "Name of the Employees"

            .Col = ColEMP_ADDR
            .Text = "Residential Address"

            .Col = ColEMP_FNAME
            .Text = "Father's Name"

            .Col = ColEMP_DESG_CODE
            .Text = "Nature of Work"

            .Col = ColGroupLetter
            .Text = "Letter of Group as in Form-II"

            .Col = ColShift
            .Text = "Number of realy if working in shifts"

            .Col = ColCertificateNo
            .Text = "Certificate No. & Date"

            .Col = ColEMP_CODE
            .Text = "Token No. giving reference to the certificate"

            .Col = ColRemarks
            .Text = "Remarks"

        End With
    End Sub
End Class
