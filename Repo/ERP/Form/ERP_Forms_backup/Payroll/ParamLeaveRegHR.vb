Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamLeaveRegHR
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColEmpCode As Short = 4
    Private Const ColEmpName As Short = 5
    Private Const ColFromDate As Short = 6
    Private Const ColToDate As Short = 7
    Private Const ColLDays As Short = 8
    Private Const ColRecName As Short = 9
    Private Const ColAppCode As Short = 10
    Private Const ColAppEmpName As Short = 11
    Private Const ColReason As Short = 12

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColReason

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY, 12)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefNo, 6)
            .ColHidden = False

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefDate, 9)
            .ColHidden = False

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpCode, 9)
            .ColHidden = False

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpName, 30)
            .ColHidden = False

            .Col = ColFromDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColFromDate, 9)
            .ColHidden = False

            .Col = ColToDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColToDate, 9)
            .ColHidden = False

            .ColsFrozen = ColFromDate

            .Col = ColLDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColLDays, 5)
            .ColHidden = False

            .Col = ColRecName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRecName, 20)
            .ColHidden = False

            .Col = ColAppCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAppCode, 9)
            .ColHidden = True

            .Col = ColAppEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAppEmpName, 20)
            .ColHidden = False

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColReason, 15)
            .ColHidden = False


            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColReason)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        '    SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKEY"

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColRefDate
            .Text = "Ref Date"

            .Col = ColEmpCode
            .Text = "Emp Code"

            .Col = ColEmpName
            .Text = "Emp Name"

            .Col = ColFromDate
            .Text = "Leave From Date"

            .Col = ColToDate
            .Text = "Leave To Date"

            .Col = ColLDays
            .Text = "Total Leave Days"

            .Col = ColRecName
            .Text = "Recommended By"

            .Col = ColAppCode
            .Text = "Approved By"

            .Col = ColAppEmpName
            .Text = "Approved By"

            .Col = ColReason
            .Text = "Reason"

            .set_RowHeight(0, 20)
        End With
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr

        Clear1()

        Show1()
        Call FormatSprdMain(-1)

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub frmParamLeaveRegHR_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub
        txtAppEmpName.Text = ""
        '    If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        txtAppEmpName.Text = Trim(MasterNo)
        '    End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamLeaveRegHR_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamLeaveRegHR_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False

        Frame5.Enabled = True
        mDate = "01/" & VB6.Format(PubCurrDate, "MM/YYYY")

        txtFromDate.Text = VB6.Format(mDate, "DD-MMMM-YYYY")

        txtAppEmpName.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamLeaveRegHR_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        FraFront.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo ERR1

        MakeSQL = " SELECT '', " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_REF), IH.REF_DATE, " & vbCrLf _
            & " IH.EMP_CODE, CMST.EMP_NAME, IH.FROM_DATE,  IH.TO_DATE, " & vbCrLf _
            & " IH.LDAYS, RMST.EMP_NAME, IH.APP_EMP_CODE, AMST.EMP_NAME," & vbCrLf _
            & " REASON"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PAY_LEAVE_APP_TRN IH, " & vbCrLf _
            & " PAY_EMPLOYEE_MST CMST, PAY_EMPLOYEE_MST RMST, PAY_EMPLOYEE_MST AMST"
        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.EMP_CODE=CMST.EMP_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=RMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.REC_EMP_CODE=RMST.EMP_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.APP_EMP_CODE=AMST.EMP_CODE"


        If lblBookType.Text = "A" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_STATUS ='A' "
        Else
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_STATUS ='R' "
        End If

        If OptSelection(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.HR_STATUS ='O'"
        Else
            MakeSQL = MakeSQL & vbCrLf & "AND IH.HR_STATUS ='C'"
        End If


        MakeSQL = MakeSQL & vbCrLf & "AND ((IH.FROM_DATE >= TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.FROM_DATE <= TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        MakeSQL = MakeSQL & vbCrLf & "OR (IH.TO_DATE >= TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.TO_DATE <= TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"

        'ATTN_DATE> TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')

        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_EMP_CODE='" & MainClass.AllowSingleQuote(PubUserEMPCode) & "'"
        '    MakeSQL = MakeSQL & vbCrLf & "AND AMST.EMP_NAME='" & MainClass.AllowSingleQuote(txtAppEmpName.Text) & "'"


        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.FROM_DATE,IH.TO_DATE,IH.EMP_CODE,IH.AUTO_KEY_REF"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub frmParamLeaveRegHR_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        MainClass.SaveStatus(Me.cmdShow, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdShow, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xRefNo As Double
        Dim xLeaveFrom As String
        Dim xLeaveTo As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xRefNo = Val(Me.SprdMain.Text)

        SprdMain.Col = ColFromDate
        xLeaveFrom = VB6.Format(Me.SprdMain.Text, "DD/MM/YYYY")

        SprdMain.Col = ColToDate
        xLeaveTo = VB6.Format(Me.SprdMain.Text, "DD/MM/YYYY")

        If xRefNo = 0 Then Exit Sub


        frmEmpLeaveOLEntry.frmEmpLeaveOLEntry_Activated(Nothing, New System.EventArgs())

        frmEmpLeaveOLEntry.txtRefNo.Text = CStr(xRefNo)
        frmEmpLeaveOLEntry.txtLeaveFrom.Text = xLeaveFrom
        frmEmpLeaveOLEntry.txtLeaveTo.Text = xLeaveTo
        frmEmpLeaveOLEntry.txtRefNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        frmEmpLeaveOLEntry.ShowDialog()
    End Sub

    Private Sub txtAppEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppEmpName.DoubleClick
        SearchAppEmpName()
    End Sub
    Private Sub SearchAppEmpName()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster txtAppEmpName, "PAY_EMPLOYEE_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtAppEmpName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr)
        If AcName <> "" Then
            txtAppEmpName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAppEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAppEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAppEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAppEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAppEmpName()
    End Sub
    Private Sub txtAppEmpName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppEmpName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtAppEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtAppEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAppEmpName.Text = UCase(Trim(txtAppEmpName.Text))
        Else
            MsgInformation("No Such Employee in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
