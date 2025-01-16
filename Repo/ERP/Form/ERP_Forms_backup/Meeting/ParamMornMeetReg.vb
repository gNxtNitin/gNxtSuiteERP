Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMornMeetReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 27

    'Dim PvtDBCn As ADODB.Connection			

    Private Const ColRefNo As Short = 1
    Private Const ColRaisedDate As Short = 2
    Private Const ColRaisedBy As Short = 3
    Private Const ColExpectedFrom As Short = 4
    Private Const ColPointType As Short = 5
    Private Const ColRemarks As Short = 6
    Private Const ColExpected1 As Short = 7
    Private Const ColExpected2 As Short = 8
    Private Const ColExpected3 As Short = 9
    Private Const ColExpected4 As Short = 10
    Private Const ColExpected5 As Short = 11
    Private Const ColCurrDate As Short = 12
    Private Const ColNarration As Short = 13
    Private Const ColStatus As Short = 14


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMMM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMMM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnMMM(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String


        Report1.Reset()

        mTitle = "Morning Meeting Minutes " & IIf(lblBookType.Text = "E", "(Export)", "")

        mSubTitle = "Date : " & VB6.Format(RunDate, "DD/MM/YYYY")

        '    mSubTitle = "Raised Date From : " & vb6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")			

        SqlStr = MakeSQL()
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ParamMornMeet.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4			
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMornMeetReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Morning Meeting Minutes Register " & IIf(lblBookType.Text = "E", "(Export)", "")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMornMeetReg_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamMornMeetReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection			
        'PvtDBCn.Open StrConn			

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7020)
        Me.Width = VB6.TwipsToPixelsX(11355)

        Call PrintStatus(True)

        '    txtDateFrom = Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")			
        '    txtDateTo = Format(RunDate, "DD/MM/YYYY")			
        '			
        '    txtEDateFrom.Text = Format(RunDate, "DD/MM/YYYY")			
        '    txtEDateTo.Text = Format(RunDate, "DD/MM/YYYY")			

        cboStatus.Items.Clear()
        cboStatus.Items.Add("All")
        cboStatus.Items.Add("Open")
        cboStatus.Items.Add("Closed")
        cboStatus.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamMornMeetReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMornMeetReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xRefNo As Double


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xRefNo = Val(SprdMain.Text)


        frmMornMeeting.Show()

        frmMornMeeting.frmMornMeeting_Activated(Nothing, New System.EventArgs())
        frmMornMeeting.lblBookType.Text = lblBookType.Text
        frmMornMeeting.txtNumber.Text = CStr(xRefNo)
        frmMornMeeting.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        '    End If			
    End Sub

    Private Sub txtDatefrom_Change()
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_Change()
        Call PrintStatus(False)
    End Sub



    Private Sub FormatSprdMain(ByRef Arow As Integer)
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefNo, 6)

            .Col = ColRaisedDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColRaisedDate, 9)
            .TypeEditMultiLine = True

            .ColsFrozen = ColRaisedDate

            .Col = ColRaisedBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColRaisedBy, 15)
            .TypeEditMultiLine = True

            .Col = ColExpectedFrom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColExpectedFrom, 15)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 20)

            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 12)

            .Col = ColPointType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPointType, 12)

            For cntCol = ColExpected1 To ColCurrDate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditLen = 255
                .TypeEditMultiLine = False
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColStatus, 8)


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' = OperationModeSingle			
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean
        On Error GoTo LedgError
        Dim SqlStr As String


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************			
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim mDept As String
        Dim mEmployee As String


        'ALTER TABLE PRD_MORNMEET_TRN ADD (			
        'RAISEDBY_USERID  VARCHAR2(8) NULL,			
        'RAISEDBY_USERNAME VARCHAR2(40) NULL,			
        'EXPECTEDBY_USERID VARCHAR2(8) NULL,			
        'EXPECTEDBY_USERNAME VARCHAR2(40) NULL);			

        MakeSQL = " SELECT TO_CHAR(IH.AUTO_KEY_NO,'000000'), RAISEDDATE,  " & vbCrLf _
            & " IH.RAISEDBY_USERNAME, DECODE(ALL_DEPT,'Y','ALL HODS',EXPECTEDBY_USERNAME), " & vbCrLf _
            & " POINT_TYPE, REMARKS, " & vbCrLf & " EXPECTED_DATE1, " & vbCrLf _
            & " EXPECTED_DATE2, " & vbCrLf & " EXPECTED_DATE3, " & vbCrLf _
            & " EXPECTED_DATE4, " & vbCrLf & " EXPECTED_DATE5, " & vbCrLf _
            & " CURRENTDATE, NARRATION," & vbCrLf _
            & " DECODE(STATUS,'O','OPEN','CLOSED') As STATUS"

        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_MORNMEET_TRN IH " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND IH.COMPANY_CODE=EMP_R.COMPANY_CODE " & vbCrLf |            & " AND IH.RAISEDBY=EMP_R.EMP_CODE " & vbCrLf |            & " AND IH.COMPANY_CODE=EMP_E.COMPANY_CODE " & vbCrLf |            & " AND IH.EXPECTEDBY=EMP_E.EMP_CODE "			


        '    MakeSQL = MakeSQL & vbCrLf _			
        ''            & " AND IH.RAISEDDATE>=TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _			
        ''            & " AND IH.RAISEDDATE<=TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"			
        '			
        '    MakeSQL = MakeSQL & vbCrLf _			
        ''            & " AND IH.CURRENTDATE>=TO_DATE('" & vb6.Format(txtEDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _			
        ''            & " AND IH.CURRENTDATE<=TO_DATE('" & vb6.Format(txtEDateTo.Text, "DD-MMM-YYYY") & "')"			

        MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf
        If cboStatus.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.STATUS='" & VB.Left(cboStatus.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY CURRENTDATE"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function			
        '    If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function			
        '    If MainClass.ChkIsdateF(txtEDateFrom) = False Then Exit Function			
        '    If MainClass.ChkIsdateF(txtEDateTo) = False Then Exit Function			
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub txtdateFrom_Validate(ByRef Cancel As Boolean)
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then			
        '        txtDateFrom.SetFocus			
        '        Cancel = True			
        '        Exit Sub			
        '    End If			

    End Sub
    Private Sub txtdateTo_Validate(ByRef Cancel As Boolean)
        '    If MainClass.ChkIsdateF(txtDateTo) = False Then			
        '        txtDateTo.SetFocus			
        '        Cancel = True			
        '        Exit Sub			
        '    End If			

    End Sub

    Private Sub txtEDateFrom_Validate(ByRef Cancel As Boolean)
        '    If MainClass.ChkIsdateF(txtEDateFrom) = False Then			
        '        txtEDateFrom.SetFocus			
        '        Cancel = True			
        '        Exit Sub			
        '    End If			

    End Sub
    Private Sub txtEDateTo_Validate(ByRef Cancel As Boolean)
        '    If MainClass.ChkIsdateF(txtEDateTo) = False Then			
        '        txtEDateTo.SetFocus			
        '        Cancel = True			
        '        Exit Sub			
        '    End If			

    End Sub
    Private Sub txtEDateFrom_Change()
        Call PrintStatus(False)
    End Sub

    Private Sub txtEDateTo_Change()
        Call PrintStatus(False)
    End Sub
End Class
