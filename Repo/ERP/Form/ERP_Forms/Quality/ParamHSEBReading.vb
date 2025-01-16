Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamHSEBReading
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDate As Short = 1
    Private Const ColTime As Short = 2
    Private Const ColKWH As Short = 3
    Private Const ColKWHDay As Short = 4
    Private Const ColKVARHG As Short = 5
    Private Const ColKVRHD As Short = 6
    Private Const ColKVAH As Short = 7
    Private Const ColKVAHDay As Short = 8
    Private Const ColKVAMDI As Short = 9
    Private Const ColKVA8 As Short = 10
    Private Const ColPF As Short = 11
    Private Const ColPFDay As Short = 12
    Private Const ColRemarks As Short = 13
    Private Const ColSign As Short = 14

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim RsHSEB As ADODB.Recordset

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDateCondition_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDateCondition.SelectedIndexChanged
        If cboDateCondition.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboDateCondition.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboDateCondition.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboDateCondition.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboDateCondition.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnHSEBReading(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnHSEBReading(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1

        If Trim(cboMeterNo.Text) = "" Then
            MsgBox("Meter No is Blank.")
            cboMeterNo.Focus()
            Exit Function
        End If

        If cboDateCondition.Text = "Between" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
            If Not IsDate(txtDate2.Text) Then
                MsgBox("Date2 is Blank.")
                txtDate2.Focus()
                Exit Function
            End If
        End If
        If cboDateCondition.Text = "After" Or cboDateCondition.Text = "Before" Or cboDateCondition.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub ReportOnHSEBReading(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "HSEB Reading Report"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\HSEBReading.rpt"

        If cboDateCondition.Text = "Between" Then
            mSubTitle = mSubTitle & " [Date Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboDateCondition.Text = "After" Then
            mSubTitle = mSubTitle & " [Date After  " & txtDate1.Text & " ]"
        End If
        If cboDateCondition.Text = "Before" Then
            mSubTitle = mSubTitle & " [Date Before  " & txtDate1.Text & " ]"
        End If
        If cboDateCondition.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Date On  " & txtDate1.Text & " ]"
        End If

        SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Sub frmParamHSEBReading_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "HSEB Reading Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamHSEBReading_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11565)

        Call FillCbo()

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()

        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        cboDateCondition.Items.Clear()
        cboDateCondition.Items.Add("None")
        cboDateCondition.Items.Add("Between")
        cboDateCondition.Items.Add("After")
        cboDateCondition.Items.Add("Before")
        cboDateCondition.Items.Add("On Date")
        cboDateCondition.SelectedIndex = 0

        cboMeterNo.Items.Clear()

        SqlStr = "SELECT METER_NAME FROM MAN_HSBC_METER_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY METER_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboMeterNo.Items.Add(RS.Fields("METER_NAME").Value)
                RS.MoveNext()
            Loop
            cboMeterNo.SelectedIndex = 0
        End If

        'cboMeterNo.SelectedIndex = 0

        cboCompany.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        'cboCompany.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCompany.Items.Add(RS.Fields("Company_Name").Value)
                RS.MoveNext()
            Loop
        End If
        cboCompany.Text = RsCompany.Fields("Company_Name").Value
    End Sub

    Private Sub frmParamHSEBReading_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth), 11592.4, 760)
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamHSEBReading_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        Dim I As Short

        With SprdMain
            .MaxCols = ColSign
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = True
            .set_ColWidth(ColDate, 8)

            .Col = ColTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColTime, 8)

            .Col = ColKWH
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKWH, 8)

            .Col = ColKWHDay
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKWHDay, 8)

            .Col = ColKVARHG
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKVARHG, 8)

            .Col = ColKVRHD
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKVRHD, 8)

            .Col = ColKVAH
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKVAH, 8)

            .Col = ColKVAHDay
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKVAHDay, 8)

            .Col = ColKVAMDI
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKVAMDI, 8)

            .Col = ColKVA8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColKVA8, 8)

            .Col = ColPF
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColPF, 8)

            .Col = ColPFDay
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 10
            .set_ColWidth(ColPFDay, 12)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 15)

            .Col = ColSign
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .set_ColWidth(ColSign, 8)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            '        SprdMain.DAutoCellTypes = True
            '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '        SprdMain.GridColor = &HC00000
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
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
        Dim mMeterNo As Double
        Dim mCompanyName As String
        Dim mCompanyCode As String

        If cboCompany.SelectedIndex >= 0 Then
            mCompanyName = Trim(cboCompany.Text)
            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mCompanyCode = MasterNo
            End If
        Else
            mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_HSBC_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
            mMeterNo = Trim(MasterNo)
        End If

        MakeSQL = " SELECT READING_DATE, TO_CHAR(READING_TIME,'HH24:MI'), KWH, KWH_DAY, " & vbCrLf _
            & " KVARHG, KVRHD, KVAH, KVAH_DAY, KVA_MDI, KVA8, " & vbCrLf _
            & " PF, PF_DAY, REMARKS, EMP_CODE " & vbCrLf _
            & " FROM MAN_HSEB_TRN " & vbCrLf _
            & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MakeSQL = MakeSQL & vbCrLf & " AND COMPANY_CODE='" & mCompanyCode & "'"


        MakeSQL = MakeSQL & vbCrLf & " AND METER_CODE=" & Val(CStr(mMeterNo)) & ""

        If cboDateCondition.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND READING_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboDateCondition.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND READING_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND READING_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND READING_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY READING_DATE, READING_TIME "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCompany.SelectedIndexChanged
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim mCompanyName As String
        Dim mCompanyCode As String

        If cboCompany.SelectedIndex >= 0 Then
            mCompanyName = Trim(cboCompany.Text)
            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mCompanyCode = MasterNo
            End If
        Else
            mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
        End If
        cboMeterNo.Items.Clear()

        SqlStr = "SELECT METER_NAME FROM MAN_HSBC_METER_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " ORDER BY METER_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboMeterNo.Items.Add(RS.Fields("METER_NAME").Value)
                RS.MoveNext()
            Loop
            cboMeterNo.SelectedIndex = 0
        End If


        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
End Class
