Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBudgetPosting
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColWEF As Short = 3
    Private Const ColRefAmendNo As Short = 4
    Private Const ColDiv As Short = 5
    Private Const ColDept As Short = 6
    Private Const ColPostStatus As Short = 7


    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        TxtDept.Enabled = IIf(ChkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKey As Double
        Dim mRefNo As Double
        Dim mUpdateCount As Integer
        Dim mCanPostPO As Boolean

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                mCanPostPO = False
                .Row = cntRow

                .Col = ColMKEY
                mMKey = CDbl(Trim(.Text))

                .Col = ColRefNo
                mRefNo = CDbl(Trim(.Text))



                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    '                If mCanPostPO = True Then
                    ''Closed all PO ''& " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf

                    SqlStr = "UPDATE INV_BUDGET_HDR SET BUDGET_CLOSED='Y', UPDATE_FROM='N'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_REF=" & mRefNo & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    PubDBCn.Execute(SqlStr)
                    ''open Only Current Budget
                    SqlStr = "UPDATE INV_BUDGET_HDR SET BUDGET_STATUS='Y',BUDGET_CLOSED='N', UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY=" & mMKey & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If
                '            End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Budget Posted.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = False
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtDept.Text) = "" Then
                MsgInformation("Please Select Account")
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmBudgetPosting_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmBudgetPosting_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        TxtDept.Enabled = False
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "SELECT IH.MKEY, IH.AUTO_KEY_REF, IH.AMEND_WEF_DATE, AMEND_NO,  " & vbCrLf & " ID.DIV_DESC AS Division, " & vbCrLf & " DEPT.DEPT_DESC  AS DEPARTMENT, " & vbCrLf & " '' " & vbCrLf & " FROM INV_BUDGET_HDR IH, INV_DIVISION_MST ID, PAY_DEPT_MST DEPT" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.Company_Code=DEPT.Company_Code " & vbCrLf & " AND IH.DEPT_CODE=DEPT.DEPT_CODE " & vbCrLf & " AND IH.Company_Code=ID.Company_Code " & vbCrLf & " AND IH.DIV_CODE=ID.DIV_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.BUDGET_STATUS='N' "

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtDept.Text) <> "" Then
            '        mSuppCustCode = 1
            SqlStr = SqlStr & vbCrLf & " And DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(UCase(TxtDept.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY DEPT.DEPT_DESC, ID.DIV_DESC, SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4), IH.AMEND_WEF_DATE,IH.AUTO_KEY_REF"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColPostStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 11)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRefNo, 9)

            .Col = ColWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColWEF, 9)

            .Col = ColRefAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRefAmendNo, 6)

            .Col = ColDiv
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDiv, 6)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 30)

            .Col = ColPostStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColPostStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColDept)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColRefNo
            .Text = "Ref No."

            .Col = ColWEF
            .Text = "WEF Date"

            .Col = ColRefAmendNo
            .Text = "Amend No"

            .Col = ColDiv
            .Text = "Division"

            .Col = ColDept
            .Text = "Department"

            .Col = ColPostStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmBudgetPosting_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColPostStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtDept.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        If AcName <> "" Then
            TxtDept.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim xPoNo As Double
        'Dim xAmendPONo As Double
        'Dim ss As New frmPO
        '
        '    SprdMain.Row = SprdMain.ActiveRow
        '
        '    SprdMain.Col = ColRefNo
        '    xPoNo = Val(SprdMain.Text)
        '
        '    SprdMain.Col = ColRefAmendNo
        '    xAmendPONo = Val(SprdMain.Text)
        '
        '    SqlStr = "SELECT * from INV_BUDGET_HDR WHERE AUTO_KEY_REF=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        frmPO.Show 0
        '        frmPO.lblBookType.text = RsTemp!PUR_TYPE & RsTemp!ORDER_TYPE
        '
        '        frmPO.Form_Activate
        '
        '        frmPO.txtPONo = RsTemp!AUTO_KEY_REF
        '        frmPO.txtAmendNo = RsTemp!AMEND_NO
        '
        '        frmPO.txtAmendNo_Validate False     ''txtPONO_Validate False
        '
        '
        '
        '
        '    End If
    End Sub

    Private Sub TxtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDept.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDept.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub TxtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        On Error GoTo ERR1
        If TxtDept.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtDept.Text = UCase(Trim(TxtDept.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
